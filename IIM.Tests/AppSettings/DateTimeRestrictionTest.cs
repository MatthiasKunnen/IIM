using System;
using System.Collections.Generic;
using IIM.Models.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IIM.Tests.AppSettings
{
    [TestClass]
    public class DateTimeRestrictionTest
    {

        [TestMethod]
        public void TestDayOfWeek()
        {
            var restriction = new DateTimeRestriction()
            {
                DaysOfWeek = new List<DayOfWeek>() { DayOfWeek.Monday },
                Type = DateTimeRestriction.RestrictionType.Allow
            };
            var someMonday = new DateTime(2016, 5, 2);
            Assert.AreEqual(DayOfWeek.Monday, someMonday.DayOfWeek, "Test subject is not a monday, the test is not representative!");
            Assert.AreEqual(true, restriction.IsValid(someMonday));
            restriction.Type = DateTimeRestriction.RestrictionType.Deny;
            Assert.AreEqual(false, restriction.IsValid(someMonday));
            var someTuesday = someMonday.AddDays(1);
            Assert.IsNull(restriction.IsValid(someTuesday));
        }

        [TestMethod]
        public void TestTime()
        {
            //Dates will only be valid when between 08:00:00 and 10:00:00.
            var restriction = new DateTimeRestriction()
            {
                TimeStart = new DateTime(2016, 5, 2, 8, 0, 0), //2016-05-02 08:00:00
                Type = DateTimeRestriction.RestrictionType.Allow
            };
            if (restriction.TimeStart == null)
            {
                Assert.Fail("Restriction.TimeStart is null");
            }
            restriction.TimeEnd = restriction.TimeStart.Value.AddHours(2); //Exactly two more hours than TimeStart
            var testDate = DateTime.Today.AddHours(8); //Today 08:00:00
            Assert.AreEqual(true, restriction.IsValid(testDate)); //08:00:00 should be allowed
            Assert.AreEqual(true, restriction.IsValid(testDate.AddHours(2))); //10:00:00 should be allowed
            Assert.IsNull(restriction.IsValid(testDate.AddMinutes(-1))); //The restriction is not applicable for 07:59:00
            Assert.IsNull(restriction.IsValid(testDate.AddHours(2).AddSeconds(1))); //The restriction is not applicable for 10:00:01
            restriction.Type = DateTimeRestriction.RestrictionType.Deny;
            Assert.AreEqual(false, restriction.IsValid(testDate)); //08:00:00 should be denied
            Assert.AreEqual(false, restriction.IsValid(testDate.AddHours(2))); //10:00:00 should be denied
        }

        [TestMethod]
        public void TestDates()
        {
            var dates = new List<DateTime>()
            {
                new DateTime(2016, 5, 2),
                new DateTime(2016, 5, 3)
            };
            var restriction = new DateTimeRestriction()
            {
                Dates = new List<DateTime>(dates),
                Type = DateTimeRestriction.RestrictionType.Allow
            };
            dates.ForEach(d => Assert.AreEqual(true, restriction.IsValid(d)));
            Assert.IsNull(restriction.IsValid(new DateTime(2016, 5, 4)));
            restriction.Type = DateTimeRestriction.RestrictionType.Deny;
            dates.ForEach(d => Assert.AreEqual(false, restriction.IsValid(d)));
        }

        [TestMethod]
        public void TestDatesAndTimeRange()
        {
            var dates = new List<DateTime>()
            {
                new DateTime(2016, 5, 2),
                new DateTime(2016, 5, 3)
            };
            var restriction = new DateTimeRestriction()
            {
                Dates = new List<DateTime>(dates),
                TimeStart = new DateTime(2016, 1, 1, 8, 4, 0), //2016-01-01 08:04:00
                Type = DateTimeRestriction.RestrictionType.Allow
            };
            if (restriction.TimeStart == null)
            {
                Assert.Fail("Restriction.TimeStart is null");
            }
            restriction.TimeEnd = restriction.TimeStart.Value.AddMinutes(26);
            dates.ForEach(d => Assert.AreEqual(true, restriction.IsValid(d.AddHours(8).AddMinutes(4)))); //08:04:00
            dates.ForEach(d => Assert.AreEqual(true, restriction.IsValid(d.AddHours(8).AddMinutes(30)))); //08:30:00
            dates.ForEach(d => Assert.IsNull(restriction.IsValid(d.AddHours(8).AddMinutes(4).AddSeconds(-1)))); //08:03:59
            dates.ForEach(d => Assert.IsNull(restriction.IsValid(d.AddHours(8).AddMinutes(31)))); //08:31:00
            Assert.IsNull(restriction.IsValid(new DateTime(2015,5,7).AddHours(8).AddMinutes(4))); //Test allowed time with non-matching date
        }

        [TestMethod]
        public void EmptyDateRestrictionTest()
        {
            Assert.IsNull(new DateTimeRestriction().IsValid(DateTime.Now));
        }
    }
}