using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xunit.Sdk;

namespace Scheduler
{
    /*
        currentDate
        startDate
        endDate    
        Type of Schedule
        scheduleFrequency 
        scheduleInterval
        enabled                
        
     */
    public class SchedulerConfig
    {
        //Declaracion de los datos pertinentes del formulario.

        //DATES
        public DateTime currentDate { get; private set; } = DateTime.Now;
        public DateTime nextDate { get; set; } = DateTime.Now;
        public DateOnly startDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly endDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        //SCHEDULE CONFIG PARAMETERS
        public bool enabled { get; set; } = true;
        public int scheduleInterval { get; set; } = 1;
        public bool scheduleOnce { get; set; } = true;

        public SchedulerConfig()
        {

        }

        public bool dateChecker(DateTime date)
        {
            return date > this.currentDate;
        }

        public string descriptionGiver()
        {
            StringBuilder sb = new StringBuilder();
            if (scheduleOnce)
            {
                sb.Append("Occurs once. ");
                sb.Append($"Schedule will be used on {DateOnly.FromDateTime(nextDate)} at {TimeOnly.FromDateTime(nextDate)} ");
                sb.Append($"starting on {startDate}");
            }
            else
            {
                sb.Append("Occurs every day. ");
                sb.Append($"Schedule will be used on {DateOnly.FromDateTime(nextDate)} at {TimeOnly.FromDateTime(nextDate)} ");
                sb.Append($"starting on {startDate}");
            }
            return sb.ToString();
        }

        public DateTime nextDateChecker()
        {
            
            return nextDate;
        }
    }
}
