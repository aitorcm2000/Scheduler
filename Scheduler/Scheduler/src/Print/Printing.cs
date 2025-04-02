using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Core.src.Data;

namespace Scheduler.Core.src.Print
{
    internal class Printing
    {
        SchedulerConfig config;

        public Printing(SchedulerConfig config)
        {
            this.config = config;
        }

        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            if (config.scheduleOnce)
            {
                sb.Append("Occurs once. ");
                sb.Append($"Schedule will be used on {PrintDateWithFormat(config.nextDate)} ");
                sb.Append($"starting on {PrintDateWithFormat(config.startDate)}");
            }
            else
            {
                sb.Append("Occurs every day. ");
                sb.Append($"Schedule will be used on {PrintDateWithFormat(config.nextDate)} ");
                sb.Append($"starting on {PrintDateWithFormat(config.startDate)}");
            }

            return sb.ToString();
        }

        public string PrintDateWithFormat(DateTime date)
        {
            return date.ToString("dddd,dd-MMMM-yyyy  HH:mm:ss");
        }
    }
}
