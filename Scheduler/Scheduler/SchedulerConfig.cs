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
        public DateTime startDate { get; set; } = DateTime.Now;
        public DateTime endDate {  get; set; } = DateTime.Now;

        //SCHEDULE CONFIG PARAMETERS
        public bool enabled { get; set; } = true;
        public int scheduleInterval { get; set; } = 1;
        public enum scheduleFrequency
        {
            once = 0,
            daily = 1
        }

        public SchedulerConfig() 
        {
            
        }

        public bool 
    }
}
