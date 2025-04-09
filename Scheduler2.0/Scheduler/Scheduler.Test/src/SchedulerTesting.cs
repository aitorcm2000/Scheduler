using Scheduler.Core.src.Control;
using Scheduler.Core.src.Modelo;
using System;
using System.Collections;
using System.Text;
using Xunit;

namespace Scheduler.Test.src
{
    public class SchedulerTesting
    {
        internal class FutureDateTestingData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {

                yield return new object[] { DateTime.Today.AddDays(10), true };
                yield return new object[] { DateTime.Today.AddDays(-10), false };

            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        [Theory]
        [ClassData(typeof(FutureDateTestingData))]
        public void isFutureDateTesting(DateTime futureDate, bool expected)
        {
            DateChecking check = new DateChecking();
            bool result = check.isFutureDate(futureDate);

            Assert.Equal(expected, result);
        }

        internal class TrySchedulingOnceTestingData : IEnumerable<object[]>
        {
            public DateTime current = DateTime.Today;
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { current, current, current.AddDays(5), current, true, true, 0, true, 1 };
                yield return new object[] { current, current, current.AddDays(0), current, true, true, 0, true, 2 };
                yield return new object[] { current, current, current.AddDays(-5), current, true, true, 0, false, 3 };
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        [Theory]
        [ClassData(typeof(TrySchedulingOnceTestingData))]
        public void TrySchedulingOnceTestingDateUpdate(DateTime start, DateTime next, DateTime expectedNext, DateTime? end, bool enable, bool once, int interval, bool expected, int iteration)
        {
            SchedulingTestDataSetup(start, next, end, enable, once, interval);
            NextDateHandler handler = new NextDateHandler();
            handler.TrySchedulingOnce(expectedNext);

            bool result = false;
            if (SchedulerConfig.NextDate == expectedNext)
            {
                result = true;
            }

            Assert.Equal(result, expected);
        }


        [Theory]
        [ClassData(typeof(TrySchedulingOnceTestingData))]
        public void TrySchedulingOnceTestingMethodOutput(DateTime start, DateTime next, DateTime expectedNext, DateTime? end, bool enable, bool once, int interval, bool expected, int iteration)
        {
            SchedulingTestDataSetup(start, next, end, enable, once, interval);
            NextDateHandler handler = new NextDateHandler();
            bool result = handler.TrySchedulingOnce(expectedNext);

            Assert.Equal(result, expected);
        }

        internal class TrySchedulingRecurringTestingData : IEnumerable<object[]>
        {
            public DateTime current = DateTime.Today;
            public DateTime plus1day = DateTime.Today.AddDays(1);
            public DateTime plus2days = DateTime.Today.AddDays(2);
            public DateTime minus1day = DateTime.Today.AddDays(-1);

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { minus1day, current, plus1day, null, true, true, 1, true, 1 };
                yield return new object[] { minus1day, current, plus1day, null, true, true, -1, false, 2 };
                yield return new object[] { current, current, plus1day, null, true, true, 1, true, 3 };
                yield return new object[] { current, current, plus1day, null, true, true, -1, false, 4 };
                yield return new object[] { plus1day, current, plus2days, null, true, true, 1, true, 5 };
                yield return new object[] { plus1day, current, plus2days, null, true, true, -1, false, 6 };
                yield return new object[] { minus1day, current, plus1day, current, true, true, 1, false, 7 };
                yield return new object[] { minus1day, current, plus1day, current, true, true, -1, false, 8 };
                yield return new object[] { current, current, plus1day, plus1day, true, true, 1, true, 9 };
                yield return new object[] { current, current, plus1day, plus1day, true, true, -1, false, 10 };
                yield return new object[] { plus1day, current, plus2days, minus1day, true, true, 1, false, 11 };
                yield return new object[] { plus1day, current, plus2days, minus1day, true, true, -1, false, 12 };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        [Theory]
        [ClassData(typeof(TrySchedulingRecurringTestingData))]
        public void TrySchedulingRecurringTestingDateUpdate(DateTime start, DateTime next, DateTime expectedNext, DateTime? end, bool enable, bool once, int interval, bool expected, int iteration)
        {
            SchedulingTestDataSetup(start, next, end, enable, once, interval);
            NextDateHandler handler = new NextDateHandler();
            handler.TrySchedulingRecurring();
            bool result = false;

            if (SchedulerConfig.NextDate == expectedNext)
            {
                result = true;
            }

            Assert.Equal(result, expected);
        }

        [Theory]
        [ClassData(typeof(TrySchedulingRecurringTestingData))]
        public void TrySchedulingRecurringTestingMethodOutput(DateTime start, DateTime next, DateTime expectedNext, DateTime? end, bool enable, bool once, int interval, bool expected, int iteration)
        {
            SchedulingTestDataSetup(start, next, end, enable, once, interval);
            NextDateHandler handler = new NextDateHandler();
            bool result = handler.TrySchedulingRecurring();

            Assert.Equal(result, expected);
        }


        internal class TrySchedulingTestingData : IEnumerable<object[]>
        {
            public DateTime current = DateTime.Today;
            public DateTime plus1day = DateTime.Today.AddDays(1);
            public DateTime plus2days = DateTime.Today.AddDays(2);
            public DateTime minus1day = DateTime.Today.AddDays(-1);

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { minus1day, current, plus1day, null, false, true, 1, false, 1 }; //!Enabled
                yield return new object[] { minus1day, current, plus1day, null, false, false, 1, false, 2 };
                yield return new object[] { minus1day, current, plus1day, null, true, true, 1, true, 3 }; //Enabled
                yield return new object[] { minus1day, current, plus1day, null, true, false, 1, true, 4 };
                yield return new object[] { minus1day, current, null, null, true, true, 1, false, 5 }; //Date null
                yield return new object[] { minus1day, current, null, null, true, false, 1, false, 6 };
                yield return new object[] { minus1day, current, plus1day, null, true, true, 1, true, 7 }; //Date != null
                yield return new object[] { minus1day, current, plus1day, null, true, false, 1, true, 8 };

            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
        [Theory]
        [ClassData(typeof(TrySchedulingTestingData))]
        public void TrySchedulingTestingDateUpdate(DateTime start, DateTime next, DateTime expectedNext, DateTime? end, bool enable, bool once, int interval, bool expected, int iteration)
        {
            SchedulingTestDataSetup(start, next, end, enable, once, interval);
            NextDateHandler handler = new NextDateHandler();
            handler.TryScheduling(expectedNext);

            bool result = false;
            if (SchedulerConfig.NextDate == expectedNext)
            {
                result = true;
            }

            Assert.Equal(result, expected);
        }



        public void SchedulingTestDataSetup(DateTime start, DateTime? next, DateTime? end, bool enable, bool once, int interval)
        {
            SchedulerConfig.StartDate = start;
            if (next != null) SchedulerConfig.NextDate = next.Value;
            SchedulerConfig.EndDate = end;
            SchedulerConfig.ScheduleOnce = once;
            SchedulerConfig.Enabled = enable;
            SchedulerConfig.ScheduleInterval = interval;

        }

        

    }
}