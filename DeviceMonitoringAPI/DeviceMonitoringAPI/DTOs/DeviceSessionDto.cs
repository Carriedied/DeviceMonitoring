namespace DeviceMonitoringAPI.DTOs
{
    public class DeviceSessionDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Version { get; set; }
    }
}
