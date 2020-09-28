using Quartz;

namespace Cardinal.AspNetCore.BackgroundServices
{
    public class ScheduleConfig<T> : IScheduleConfig<T> where T : IJob
    {
        public string Name { get; set; } = typeof(T).Name;

        public string Group { get; set; } = "Default";

        public string Description { get; set; } = string.Empty;
    }
}
