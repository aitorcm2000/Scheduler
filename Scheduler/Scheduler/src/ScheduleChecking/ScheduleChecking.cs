using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Core.src.Data;
using Scheduler.Core.src.DateChecking;


namespace Scheduler.Core.src.ScheduleChecking
{
    public class ScheduleChecking : IScheduleChecking
    {
        SchedulerConfig config;
        DateChecker dateChecker;
        public ScheduleChecking(SchedulerConfig config)
        {
            this.config = config;
            this.dateChecker = new DateChecker(config);
        }

        public bool CorrectNextDate()
        {
            DateTime startDate = config.startDate;
            DateTime nextDate = config.nextDate;
            DateTime endDate = config.endDate;
            bool resultado = false;

            if ((nextDate <= endDate) && (nextDate >= startDate))
            {
                if (dateChecker.Check(nextDate) && dateChecker.Check(endDate)) resultado = true;
            }
            return resultado;
        }

        public void UpdateNextDate()
        {
            bool enabled = config.enabled;
            bool scheduleOnce = config.scheduleOnce;

            if (enabled)
            {
                if (!scheduleOnce) 
                {
                    if (config.scheduleInterval >= 1)
                    {
                        if (CorrectNextDate())
                        {
                            DateTime newNextDate;
                            if (config.startDate > config.currentDate)
                            {
                                newNextDate = config.startDate;
                            }
                            else
                            {
                                newNextDate = config.currentDate.AddDays(config.scheduleInterval);
                            }

                            if (dateChecker.Check(newNextDate))
                            {
                                config.nextDate = newNextDate;

                            }
                        }
                    }                    
                }                
            }
        }
    }
}
