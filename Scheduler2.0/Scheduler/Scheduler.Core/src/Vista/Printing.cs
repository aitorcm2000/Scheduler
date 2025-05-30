﻿using System.Text;
using Scheduler.Core.src.Modelo;

namespace Scheduler.Core.src.Vista
{
    public class Printing
    {
        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            if (SchedulerConfig.ScheduleOnce)
            {
                sb.Append("Occurs once. ");
            }
            else
            {
                sb.Append("Occurs every day. ");
            }

            sb.Append($"Schedule will be used on {PrintDateWithFormat(SchedulerConfig.NextDate)} ");
            sb.Append($"starting on {PrintDateWithFormat(SchedulerConfig.StartDate)}");

            return sb.ToString();
        }

        public string PrintDateWithFormat(DateTime date) => date.ToString("dddd,dd-MMMM-yyyy  HH:mm:ss");
    }
}
