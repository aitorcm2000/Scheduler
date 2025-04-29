using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Core.src.Modelo;

namespace Scheduler.Core.src.Control
{
    public class DateChecking
    {
        public bool isFutureDate(DateTime date)
        {
            SchedulerConfig.updateCurrentDate();
            bool Result = false;

            if (date >= SchedulerConfig.NextDate)
            {
                Result = true;
            }

            return Result;
        }

    }
}
