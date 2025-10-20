using AutoMapper;
using DeviceMonitoringAPI.DTOs;
using DeviceMonitoringAPI.Interfaces;
using DeviceMonitoringAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeviceMonitoringAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly ISessionStorageService _storage;
        private readonly IMapper _mapper;
        private readonly ILoggerService<DeviceController> _logger;

        public DeviceController(ISessionStorageService storage, IMapper mapper, ILoggerService<DeviceController> logger)
        {
            _storage = storage;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult PostSession([FromBody] CreateSessionDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for CreateSessionDto");

                return BadRequest(ModelState);
            }

            var session = _mapper.Map<DeviceSession>(dto);

            if (!_storage.AddSession(session))
            {
                _logger.LogWarning("Failed to add session with ID: {Id}", dto.Id);

                return StatusCode(500, "Could not save session");
            }

            _logger.LogInformation("Session received and stored: {Id}", dto.Id);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllDevices()
        {
            var sessions = _storage.GetAllSessions();
            var deviceIds = sessions.Select(s => new { s.Id, s.UserName }).Distinct().ToList();

            _logger.LogInformation("Fetched {Count} unique devices", deviceIds.Count);

            return Ok(deviceIds);
        }

        [HttpGet("device/{id}")]
        public IActionResult GetSessionsForDevice(string id)
        {
            var sessions = _storage.GetSessionsByDeviceId(id);
            if (!sessions.Any())
                return NotFound();

            var dtos = sessions.Select(_mapper.Map<DeviceSessionDto>).ToList();
            _logger.LogInformation("Fetched {Count} sessions for device: {Id}", dtos.Count, id);

            return Ok(dtos);
        }

        [HttpDelete("cleanup")]
        public IActionResult CleanupOldData([FromQuery] int days = 30)
        {
            var cutoff = DateTime.UtcNow.AddDays(-days);
            _storage.DeleteOlderThan(cutoff);
            _logger.LogInformation("Cleanup completed for entries older than {Days} days", days);

            return Ok(new { Message = $"Cleanup completed. Removed entries older than {days} days." });
        }

        [HttpPost("backup")]
        public IActionResult BackupData()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "backups", $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.json");
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            _storage.BackupToFile(path);
            _logger.LogInformation("Backup saved to {Path}", path);

            return Ok(new { FilePath = path });
        }
    }
}
