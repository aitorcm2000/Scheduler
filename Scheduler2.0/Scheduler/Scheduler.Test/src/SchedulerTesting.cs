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
                yield return new object[] { DateTime.MinValue, false }; // Minimum possible date  
                yield return new object[] { DateTime.MaxValue, true };  // Maximum possible date  
                yield return new object[] { DateTime.Today, false };    // Current date  
                yield return new object[] { DateTime.Today.AddMilliseconds(-1), false }; // Just before today  
                yield return new object[] { DateTime.Today.AddMilliseconds(1), true };   // Just after today  

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
                yield return new object[] { current, current, current.AddDays(-5), current, true, true, 0, false, 2 };
                yield return new object[] { null, current, current.AddDays(5), current, true, true, 0, true, 3 };
                yield return new object[] { current, null, current.AddDays(0), current, true, true, 0, true, 4 };
                yield return new object[] { current, current, null, current, true, true, 0, false, 5 };
                yield return new object[] { null, current, current.AddDays(5), current, true, true, 0, true, 6 };
                yield return new object[] { current, null, current.AddDays(0), null, true, true, 0, true, 7 };
                yield return new object[] { current, current, null, null, true, true, 0, false, 8 };
                yield return new object[] { DateTime.MinValue, current, current.AddDays(1), null, true, true, 0, true, 9 }; // Start date at MinValue  
                yield return new object[] { current, current, DateTime.MaxValue, null, true, true, 0, true, 10 }; // Expected next date at MaxValue  
                yield return new object[] { current, current, current.AddDays(1), DateTime.MinValue, true, true, 0, true, 11 }; // End date at MinValue  
                yield return new object[] { current, current, current.AddDays(1), DateTime.MaxValue, true, true, 0, true, 12 }; // End date at MaxValue  
                yield return new object[] { current, current, current.AddDays(1), null, false, true, 0, true, 13 }; // Disabled scheduling  
                yield return new object[] { current, current, current.AddDays(1), null, true, false, 0, true, 14 }; // Not a one-time schedule  

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
                yield return new object[] { DateTime.MinValue, current, current.AddDays(1), null, true, true, 1, true, 13 }; // Start date at MinValue                
                yield return new object[] { current, current, current.AddDays(1), DateTime.MinValue, true, true, 1, false, 14 }; // End date at MinValue   
                yield return new object[] { current, current, current.AddDays(1), null, false, true, 1, true, 15 }; // Disabled scheduling  
                yield return new object[] { current, current, current.AddDays(1), null, true, false, 1, true, 16 }; // Not a one-time schedule  
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        //TODO: Cambiar once y recurring para que los casos de currentDate estén bien
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
                yield return new object[] { null, current, plus1day, null, false, true, 1, false, 9 }; //!Enabled
                yield return new object[] { minus1day, null, plus1day, null, false, true, 1, false, 10 }; //!Enabled
                yield return new object[] { minus1day, current, null, null, false, true, 1, false, 11 }; //!Enabled
                yield return new object[] { null, current, plus1day, null, true, true, 1, true, 12 }; //Enabled
                yield return new object[] { minus1day, null, plus1day, null, true, true, 1, true, 13 }; //Enabled
            }
            //(start: 2025-04-28T00:00:00.0000000+02:00, next: 2025-04-29T00:00:00.0000000+02:00, expectedNext: null, end: null, enable: True, once: True, interval: 1, expected: True, iteration: 14)
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