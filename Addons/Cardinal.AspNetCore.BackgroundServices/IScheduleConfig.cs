using Quartz;

namespace Cardinal.AspNetCore.BackgroundServices
{
    public interface IScheduleConfig<T> where T : IJob
    {
        string Name { get; set; }

        string Group { get; set; }

        string Description { get; set; }
    }
}
