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

        ////TEST DATACHECKER DATEONLY
        //internal class DateOnlyTestData : IEnumerable<object[]>
        //{
        //    public IEnumerator<object[]> GetEnumerator()
        //    {

        //        yield return new object[] { new DateTime(2025, 4, 2, 10, 10, 0), new DateOnly(2025, 4, 10), true };
        //        yield return new object[] { new DateTime(2025, 4, 2, 10, 10, 0), new DateOnly(2025, 3, 10), false };

        //    }

        //    IEnumerator IEnumerable.GetEnumerator()
        //    {
        //        return GetEnumerator();
        //    }
        //}

        //[Theory]
        //[ClassData(typeof(DateOnlyTestData))]
        //public void DateCheckerDateOnly(DateTime current, DateOnly test, bool expected)
        //{
        //    var scheduler = new SchedulerConfig();
        //    var dateChecker = new DateChecker(scheduler);     
        //    scheduler.currentDate = current;

        //    bool result = dateChecker.Check(test);

        //    Assert.Equal(expected, result);
        //}

        //internal class DateTimeToStringTestData : IEnumerable<object[]>
        //{
        //    public IEnumerator<object[]> GetEnumerator()
        //    {

        //        yield return new object[] { new DateTime(2025, 4, 2, 10, 10, 0), new DateTime(2025, 4, 10).ToString(), true };
        //        yield return new object[] { new DateTime(2025, 4, 2, 10, 10, 0), new DateTime(2025, 3, 10).ToString(), false };

        //    }

        //    IEnumerator IEnumerable.GetEnumerator()
        //    {
        //        return GetEnumerator();
        //    }
        //}


        ////TEST DATACHECKER TOSTRING()
        //[Theory]
        //[ClassData(typeof(DateTimeToStringTestData))]
        //public void DateCheckerString(DateTime current, string test, bool expected)
        //{
        //    var scheduler = new SchedulerConfig();
        //    var dateChecker = new DateChecker(scheduler);
        //    scheduler.currentDate = current;
            
        //    bool result = dateChecker.Check(test);
        //    Assert.Equal(expected, result);
        //}

        internal class CorrectNextDateTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {

                yield return new object[] { new DateTime(2025, 4, 2, 10, 10, 0), new DateTime(2026, 6, 15), true };
                yield return new object[] { new DateTime(2025, 4, 2, 10, 10, 0), new DateTime(2024, 6, 15), false };

            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
        //
        [Theory]
        [ClassData(typeof(CorrectNextDateTestData))]
        public void CorrectNextDate(DateTime next, DateTime end, bool expected)
        {
            SchedulerConfig config = new SchedulerConfig();
            ScheduleChecking checking = new ScheduleChecking(config);

            config.endDate = end ;
            config.nextDate = next;
            bool result = checking.CorrectNextDate();

            Assert.Equal(expected, result);
        }
    }
}