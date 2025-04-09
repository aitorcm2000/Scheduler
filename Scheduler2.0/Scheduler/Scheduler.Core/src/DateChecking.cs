using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.src
{
    public class DateChecking
    {
        public bool isFutureDateCorrect(DateTime date)
        {
            SchedulerConfig.updateCurrentDate();
            bool Result = false;

            if (date > SchedulerConfig.NextDate)
            {
                Result = true;
            }

            return Result;
        }

    }
}
