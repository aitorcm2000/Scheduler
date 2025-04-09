using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.src
{
    public class NextDateHandler
    {
        private DateChecking check;
        public NextDateHandler()
        {
            check = new DateChecking();
        }

        public void UpdateNextDate(DateTime? date)
        {
            SchedulerConfig.updateCurrentDate();
            DateTime _startDate = SchedulerConfig.StartDate;
            DateTime _currentDate = SchedulerConfig.CurrentDate;
            DateTime _nextDate = SchedulerConfig.NextDate;
            DateTime _endDate = SchedulerConfig.EndDate;

            bool _enabled = SchedulerConfig.Enabled;
            bool _once = SchedulerConfig.ScheduleOnce;
            int _interval = SchedulerConfig.ScheduleInterval;

            if (_enabled)
            {
                if (_once)
                {
                    if(date != null)
                    {
                        if (check.isFutureDateCorrect(date.Value))
                        {
                            _nextDate = date.Value;
                            SchedulerConfig.NextDate = _nextDate;
                        }
                        else
                        {
                            //La fecha introducida es menor que la fecha actual
                        }
                    }
                    else
                    {
                        //Once seleccionado pero no se ha introducido una fecha
                    }
                }
                else
                {
                    
                }
            
        }
    }
}
