using Scheduler.Core.src.Data;
using Scheduler.Core.src.DateChecking;
using Scheduler.Core.src.ScheduleChecking;
using System;
using System.Collections;
using Xunit;

namespace Scheduler.Test
{
    public class SchedulerTesting
    {
        //TEST DATECHECKER DATETIME

        internal class DateTimeTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {

                yield return new object[] { new DateTime(2025, 4, 2, 10, 10, 0), new DateTime(2025, 4, 10), true };
                yield return new object[] { new DateTime(2025, 4, 2, 10, 10, 0), new DateTime(2025, 4, 2, 10, 10, 1), true };
                yield return new object[] { new DateTime(2025, 4, 2, 10, 10, 0), new DateTime(2025, 3, 10), false };
                yield return new object[] { new DateTime(2025, 4, 2, 10, 10, 0), new DateTime(2025, 4, 2, 9, 59, 59), false };

            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        [Theory]
        [ClassData(typeof(DateTimeTestData))]
        public void DateCheckerDateTime(DateTime current, DateTime test, bool expected)
        {
            var scheduler = new SchedulerConfig();
            var dateChecker = new DateChecker(scheduler);

            scheduler.currentDate = current;

            bool result = dateChecker.Check(test);

            Assert.Equal(expected, result);
        }

        internal class CorrectNextDateTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                                                //startDate,                    currentDate,            nextDate,                   endDate,    expected    //casos
                yield return new object[] { new DateTime(2025, 4, 1), new DateTime(2025, 4, 2), new DateTime(2025, 4, 3), new DateTime(2025, 4, 4), true }; //standard
                yield return new object[] { new DateTime(2025, 4, 2), new DateTime(2025, 4, 2), new DateTime(2025, 4, 2), new DateTime(2025, 4, 2), true }; //todos iguales
                yield return new object[] { new DateTime(2025, 4, 3), new DateTime(2025, 4, 2), new DateTime(2025, 4, 3), new DateTime(2025, 4, 4), true }; //startDate adelantado
                yield return new object[] { new DateTime(2025, 4, 1), new DateTime(2025, 4, 2), new DateTime(2025, 4, 1), new DateTime(2025, 4, 4), false }; //nextDate mal
                yield return new object[] { new DateTime(2025, 4, 1), new DateTime(2025, 4, 2), new DateTime(2025, 4, 3), new DateTime(2025, 4, 1), false }; //endDate mal
                yield return new object[] { new DateTime(2025, 4, 3), new DateTime(2025, 4, 2), new DateTime(2025, 4, 1), new DateTime(2025, 4, 4), false }; //startDate y nextDate mal                

            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
        //
        [Theory]
        [ClassData(typeof(CorrectNextDateTestData))]
        public void CorrectNextDate(DateTime startDate, DateTime currrent, DateTime next, DateTime end, bool expected)
        {
            SchedulerConfig config = new SchedulerConfig();
            ScheduleChecking checking = new ScheduleChecking(config);
            config.startDate = startDate;
            config.currentDate = currrent;
            config.endDate = end;
            config.nextDate = next;
            bool result = checking.CorrectNextDate();

            Assert.Equal(expected, result);
        }

        internal class NextDateUpdateTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {                                 //startDate,                    currentDate,            nextDate,                   endDate,    interval enable ,once,expected    //casos
                yield return new object[] { new DateTime(2025, 4, 1), new DateTime(2025, 4, 2), new DateTime(2025, 4, 3), new DateTime(2025, 4, 10), 1, true, true, true }; //standard
                yield return new object[] { new DateTime(2025, 4, 1), new DateTime(2025, 4, 2), new DateTime(2025, 4, 3), new DateTime(2025, 4, 10), 1, true, false, true };
                yield return new object[] { new DateTime(2025, 4, 1), new DateTime(2025, 4, 2), new DateTime(2025, 4, 3), new DateTime(2025, 4, 10), 2, false, true, false };                
                yield return new object[] { new DateTime(2025, 4, 1), new DateTime(2025, 4, 2), new DateTime(2025, 4, 3), new DateTime(2025, 3, 10), 1, true, true, false };
                yield return new object[] { new DateTime(2025, 4, 5), new DateTime(2025, 4, 2), new DateTime(2025, 4, 5), new DateTime(2025, 4, 10), 1, true, true, true };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        [Theory]
        [ClassData(typeof(NextDateUpdateTestData))]
        public void UpdateNextDateOnce(DateTime start, DateTime current, DateTime next, DateTime end, int interval, bool enabled, bool once, bool expected)
        {
            SchedulerConfig config = new SchedulerConfig();
            ScheduleChecking checking = new ScheduleChecking(config);
            config.startDate = start;
            config.currentDate = current;            
            config.endDate = end;
            config.enabled = enabled;
            config.scheduleInterval = interval;
            config.scheduleOnce = once;

            bool result = false;

            DateTime resultNext = config.nextDate;
            if (resultNext == current.AddDays(interval))
            {
                result = true;
            }

            Assert.Equal(resultNext, next);
            Assert.Equal(expected, result);
        }
    }
}