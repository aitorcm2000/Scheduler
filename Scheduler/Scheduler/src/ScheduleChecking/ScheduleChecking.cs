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
            DateTime nextDate = config.nextDate;
            DateTime endDate = config.endDate;
            bool resultado = false;

            if (nextDate <= endDate)
            {
                if (dateChecker.Check(nextDate) && dateChecker.Check(endDate)) resultado = true;
            }
            return resultado;
        }

        public bool UpdateNextDate()
        {
            DateTime now = DateTime.Now;
            DateTime nextDate = config.nextDate;
            bool enabled = config.enabled;
            bool resultado = false;

            if (dateChecker.Check(nextDate))
            {
                if (enabled)
                {
                    if(config.scheduleOnce)
                    {
                        config.nextDate = config.currentDate.AddDays(config.scheduleInterval);
                        resultado = true;
                    }
                    else
                    {
                        if (CorrectNextDate())
                        {
                            DateTime newNextDate = config.currentDate.AddDays(config.scheduleInterval);

                            if (dateChecker.Check(newNextDate))
                            {
                                config.nextDate = newNextDate;
                                resultado = true;
                            }
                        }
                        
                    }
                }
                else
                {
                    config.nextDate = config.currentDate.AddDays(1);
                    resultado = true;
                }

            }


            return resultado;
        }
    }
}
