using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.src.ScheduleChecking
{
    public interface IScheduleChecking
    {
        bool CorrectNextDate ();
        void UpdateNextDate();
    }
}
