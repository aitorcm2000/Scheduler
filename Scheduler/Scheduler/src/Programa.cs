using Scheduler.Core.src.Data;
using Scheduler.Core.src.Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.src
{
    public class Programa
    {
        public static void Main (string[] args)
        {
            SchedulerConfig config = new SchedulerConfig();
            Printing print = new(config);

            Console.WriteLine(print.Print());
        }
    }
}
