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
        public DateTime currentDate { get; set; }
        public DateTime nextDate { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        //SCHEDULE CONFIG PARAMETERS
        public bool enabled { get; set; }
        public int scheduleInterval { get; set; }
        public bool scheduleOnce { get; set; }

        public SchedulerConfig()
        {
            currentDate = DateTime.Today;
            nextDate = DateTime.Today;
            startDate = DateTime.Today;
            endDate = DateTime.Today;

            enabled = true;
            scheduleInterval = 1;
            scheduleOnce = true;
        }
        
    }
}
