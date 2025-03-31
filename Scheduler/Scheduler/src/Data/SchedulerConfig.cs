using System.Collections.Concurrent;
using System.Text;


namespace Scheduler.Core.src.Data
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
        public DateTime currentDate { get; private set; }
        public DateTime nextDate { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }

        //SCHEDULE CONFIG PARAMETERS
        public bool enabled { get; set; }
        public int scheduleInterval { get; set; }
        public bool scheduleOnce { get; set; }

        public SchedulerConfig()
        {
            currentDate = DateTime.Now;
            nextDate = DateTime.Now;
            startDate = DateOnly.FromDateTime(DateTime.Now);
            endDate = DateOnly.FromDateTime(DateTime.Now);

            enabled = true;
            scheduleInterval = 1;
            scheduleOnce = true;
        }
    }
}
