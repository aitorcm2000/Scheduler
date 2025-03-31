using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Core.src.Data;

namespace Scheduler.Core.src.DateChecking
{
    public class DateChecker : IDateChecker
    {
        SchedulerConfig config;
        public DateChecker(SchedulerConfig config)
        {
            this.config = config;
        }

        public bool Check(DateTime date)
        {
            Console.WriteLine(date.ToString());
            return date >= config.currentDate;
        }

        public bool Check(DateOnly date)
        {
            DateOnly currentDate = DateOnly.FromDateTime(config.currentDate);
            return date >= currentDate;
        }

        public bool Check(string date)
        {
            bool result = false;
            if (DateTime.TryParse(date,out DateTime parsed))
            {
                if (parsed >= config.currentDate) result = true;
            }
            return result;
        }
    }
}
