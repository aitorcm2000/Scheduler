using System.Collections.Concurrent;
using System.Text;


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
        public DateTime currentDate { get; private set; }
        public DateTime nextDate { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }

        //SCHEDULE CONFIG PARAMETERS
        public bool enabled { get; set; } = true;
        public int scheduleInterval { get; set; } = 1;
        public bool scheduleOnce { get; set; } = true;

        public SchedulerConfig()
        {
            currentDate = DateTime.Now;
            nextDate = DateTime.Now;
            startDate = DateOnly.FromDateTime(DateTime.Now);
            endDate = DateOnly.FromDateTime(DateTime.Now);
        }

        /**
         * No puede haber fechas menores que la fecha actual
         */
        public bool DateChecker(DateTime date)
        {
            DateOnly dateOnly = DateOnly.FromDateTime(date);
            return dateOnly >= DateOnly.FromDateTime(currentDate);
        }

        public bool DateChecker(DateOnly date)
        {
            return date >= DateOnly.FromDateTime(currentDate);
        }

        /**
         * Devuelve un string determinado si esta activado el recordatorio de una vez (Once), 
         * si no se cumple se devuelve otro string de mensaje para el recordatorio diario (Daily).
         */
        public string DescriptionGiver()
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

        /**
         * Comprueba si l
         */
        public bool NextDateCheckerOnce()
        {            
            bool condition = false ;

            if (scheduleOnce)
            {
                if (DateChecker(startDate))
                {
                    condition = true;
                }                
            }            
            
            return condition;
        }

        public bool NextDateCheckerDaily()
        {
            bool condition = false ;
            if (!scheduleOnce)
            {
                if (DateChecker(endDate))
                {
                    condition = true;
                }
            }

            return condition;
        }
    }
}
