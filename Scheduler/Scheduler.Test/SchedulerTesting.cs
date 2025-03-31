using System;
using Xunit;

namespace Scheduler.Test    
{
    public class SchedulerTesting
    {
        //DATE CHECKER
        [Fact]
        public void DateCheckerDateTimeWrongCase()
        {
            var scheduler = new SchedulerConfig();
            DateTime date = DateTime.Today.AddDays(-1);            
            
            bool result = scheduler.DateChecker(date);

            Assert.False(result);
        }

        [Fact]
        public void DateCheckerDateTimeRightCase()
        {
            var scheduler = new SchedulerConfig();
            DateTime date = DateTime.Today.AddDays(1);

            bool result = scheduler.DateChecker(date);

            Assert.True(result);
        }

        [Fact]
        public void DateCheckerDateOnlyWrongCase()
        {
            var scheduler = new SchedulerConfig();
            DateOnly date = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));

            bool result = scheduler.DateChecker(date);

            Assert.False(result);
        }

        [Fact]
        public void DateCheckerDateOnlyRightCase()
        {
            var scheduler = new SchedulerConfig();
            DateOnly date = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

            bool result = scheduler.DateChecker(date);

            Assert.True(result);
        }

        //
    }
}