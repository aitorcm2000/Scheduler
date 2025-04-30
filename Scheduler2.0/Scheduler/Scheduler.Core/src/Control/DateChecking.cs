using Scheduler.Core.src.Modelo;

namespace Scheduler.Core.src.Control
{
    public class DateChecking
    {
        public bool isFutureDate(DateTime date)
        {
            SchedulerConfig.updateCurrentDate();
            bool Result = false;

            if (date != new DateTime ()) 
            {
                if (date > SchedulerConfig.NextDate)
                {
                    Result = true;
                }
            }
            
            return Result;
        }

    }
}
