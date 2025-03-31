using Scheduler.Core.src.Data;
using Scheduler.Core.src.DateChecking;
using Scheduler.Core.src.ScheduleChecking;
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
            var dateChecker = new DateChecker(scheduler);

            DateTime date = DateTime.Today.AddDays(-1);                 
            
            bool result = dateChecker.Check(date);

            Assert.False(result);
        }

        [Fact]
        public void DateCheckerDateTimeRightCase()
        {
            var scheduler = new SchedulerConfig();
            var dateChecker = new DateChecker(scheduler);

            DateTime date = DateTime.Today.AddDays(1);

            bool result = dateChecker.Check(date);

            Assert.True(result);
        }

        [Fact]
        public void DateCheckerDateOnlyWrongCase()
        {
            var scheduler = new SchedulerConfig();
            var dateChecker = new DateChecker(scheduler);

            DateOnly date = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));

            bool result = dateChecker.Check(date);

            Assert.False(result);
        }

        [Fact]
        public void DateCheckerDateOnlyRightCase()
        {
            var scheduler = new SchedulerConfig();
            var dateChecker = new DateChecker(scheduler);
            DateOnly date = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

            bool result = dateChecker.Check(date);

            Assert.True(result);
        }

        [Fact]
        public void DateCheckerStringWrongCase()
        {
            var scheduler = new SchedulerConfig();
            var dateChecker = new DateChecker(scheduler);
            string date = DateTime.Today.AddDays(-1).ToString();

            bool result = dateChecker.Check(date);
            Assert.False(result);
        }

        [Fact]
        public void DateCheckerStringRightCase()
        {
            var scheduler = new SchedulerConfig();
            var dateChecker = new DateChecker(scheduler);
            string date = DateTime.Today.AddDays(1).ToString();

            bool result = dateChecker.Check(date);
            Assert.True(result);
        }

        //
        [Fact]
        public void CorrectNextDateWrongCase() 
        {
            SchedulerConfig config = new SchedulerConfig();
            ScheduleChecking checking = new ScheduleChecking(config);

            config.endDate = DateOnly.FromDateTime( config.currentDate.AddDays(20));
            config.nextDate = config.endDate.AddDays(1).ToDateTime(TimeOnly.MinValue);
            bool resultado = checking.CorrectNextDate();

            Assert.False(resultado);
        }

        [Fact]
        public void CorrectNextDateRightCase()
        {
            SchedulerConfig config = new SchedulerConfig();
            ScheduleChecking checking = new ScheduleChecking(config);

            config.endDate = DateOnly.FromDateTime(config.currentDate.AddDays(20));
            config.nextDate = config.endDate.AddDays(-1).ToDateTime(TimeOnly.MinValue);
            bool resultado = checking.CorrectNextDate();

            Assert.True(resultado);
        }
    }
}