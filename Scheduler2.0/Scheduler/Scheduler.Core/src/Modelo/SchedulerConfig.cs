namespace Scheduler.Core.src.Modelo
{
    public static class SchedulerConfig
    {
        public static DateTime StartDate { get; set; } = DateTime.MinValue;
        public static DateTime CurrentDate { get; set; } = DateTime.Today;
        public static DateTime NextDate { get; set; } = DateTime.Today;
        public static DateTime? EndDate { get; set; }

        public static bool Enabled { get; set; } = true;
        public static bool ScheduleOnce { get; set; } = true;
        public static int ScheduleInterval { get; set; } = 0;

        public static void updateCurrentDate()
        {
            CurrentDate = DateTime.Today;
        }
    }
}
