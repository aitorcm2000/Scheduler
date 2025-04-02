using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.src
{
    internal interface IDateChecker
    {        
        bool Check(DateTime date);
        //bool Check(DateOnly date);
        //bool Check(string date);
    }
}
