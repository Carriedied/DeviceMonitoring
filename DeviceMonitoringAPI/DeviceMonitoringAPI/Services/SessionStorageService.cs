using DeviceMonitoringAPI.Interfaces;
using DeviceMonitoringAPI.Models;
using System.Collections.Concurrent;
using System.Text.Json;

namespace DeviceMonitoringAPI.Services
{
    public class SessionStorageService : ISessionStorageService
    {
        private readonly ConcurrentDictionary<string, DeviceSession> _sessions;

        public SessionStorageService()
        {
            _sessions = new ConcurrentDictionary<string, DeviceSession>();
        }

        public IEnumerable<DeviceSession> GetAllSessions() => _sessions.Values;

        public IEnumerable<DeviceSession> GetSessionsByDeviceId(string deviceId)
        {
            return _sessions.Values.Where(s => s.Id == deviceId);
        }

        public bool AddSession(DeviceSession session)
        {
            return _sessions.TryAdd(session.Id, session);
        }

        public bool DeleteOlderThan(DateTime cutoff)
        {
            var removed = 0;
            var keysToRemove = _sessions
                .Where(kvp => kvp.Value.EndTime < cutoff)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var key in keysToRemove)
            {
                if (_sessions.TryRemove(key, out _))
                    removed++;
            }

            return removed > 0;
        }

        public void BackupToFile(string filePath)
        {
            var json = JsonSerializer.Serialize(_sessions.Values.ToList(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
