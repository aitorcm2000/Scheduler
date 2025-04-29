using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Core.src.Modelo;

namespace Scheduler.Core.src.Control
{
    public class NextDateHandler
    {
        private DateChecking check;
        public NextDateHandler()
        {
            check = new DateChecking();
        }

        public bool TryScheduling(DateTime? date)
        {
            bool _enabled = SchedulerConfig.Enabled;
            bool _once = SchedulerConfig.ScheduleOnce;
            int _interval = SchedulerConfig.ScheduleInterval;

            if (!_enabled)
            {
                return false;
            }

            if (_once && date != null)
            {
                return TrySchedulingOnce(date.Value);
            }
            else if (!_once)
            {
                return TrySchedulingRecurring();
            }
            else
            {
                return false;
            }

        }

        public bool TrySchedulingOnce(DateTime date)
        {
            bool result = false;
            if (check.isFutureDate(date))
            {
                SchedulerConfig.NextDate = date;
                result = true;
            }

            return result;
        }

        public bool TrySchedulingRecurring()
        {
            DateTime _startDate = SchedulerConfig.StartDate;
            DateTime _currentDate = SchedulerConfig.CurrentDate;
            DateTime _nextDate = SchedulerConfig.NextDate;
            DateTime? _endDate = SchedulerConfig.EndDate;
            int _interval = SchedulerConfig.ScheduleInterval;
            bool result = false;


            if (_startDate > _currentDate)
            {
                _nextDate = _startDate;
            }
            else
            {
                _nextDate = _currentDate;
            }

            if (_interval > 0)
            {
                if (_endDate != null)
                {
                    if (_startDate < _endDate && check.isFutureDate(_endDate.Value) && _nextDate.AddDays(_interval) <= _endDate)
                    {
                        _nextDate = _nextDate.AddDays(_interval);
                        SchedulerConfig.NextDate = _nextDate;
                        result = true;
                    }
                }
                else
                {
                    _nextDate = _nextDate.AddDays(_interval);
                    SchedulerConfig.NextDate = _nextDate;
                    result = true;
                }
            }

            return result;
        }


    }
}


