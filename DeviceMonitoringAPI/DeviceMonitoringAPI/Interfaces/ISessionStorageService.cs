using DeviceMonitoringAPI.Models;

namespace DeviceMonitoringAPI.Interfaces
{
    public interface ISessionStorageService
    {
        IEnumerable<DeviceSession> GetAllSessions();
        IEnumerable<DeviceSession> GetSessionsByDeviceId(string deviceId);
        bool AddSession(DeviceSession session);
        bool DeleteOlderThan(DateTime cutoff);
        void BackupToFile(string filePath);
    }
}
