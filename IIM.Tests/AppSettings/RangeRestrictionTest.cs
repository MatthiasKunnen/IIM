using System;
using System.Collections.Generic;
using System.Linq;
using IIM.App_Start;
using IIM.Models.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IIM.Tests.AppSettings
{
    [TestClass]
    public class RangeRestrictionTest
    {
        private List<DateTimeRestriction> _restrictions;
        private RangeRestriction _rangeRestriction;

        [TestInitialize]
        public void SetUp()
        {
            /*
            Summary
            Allowed: 
                Friday, Monday
                2016-05-07
            Denied: 
                Saturday, Sunday
                2016-05-01, 2016-05-05
            Neutral: Tuesday - Thursday
            */
            _restrictions = new List<DateTimeRestriction>()
            {
                new DateTimeRestriction()
                {
                    DaysOfWeek = new List<DayOfWeek>()
                    {
                        DayOfWeek.Friday,
                        DayOfWeek.Monday
                    },
                    Type = DateTimeRestriction.RestrictionType.Allow
                },
                new DateTimeRestriction()
                {
                    Dates = new List<DateTime>()
                    {
                        new DateTime(2016, 5, 7)
                    },
                    Type = DateTimeRestriction.RestrictionType.Allow
                },
                new DateTimeRestriction()
                {
                    DaysOfWeek = new List<DayOfWeek>()
                    {
                        DayOfWeek.Saturday,
                        DayOfWeek.Sunday
                    },
                    Type = DateTimeRestriction.RestrictionType.Deny
                },
                new DateTimeRestriction()
                {
                    Dates = new List<DateTime>()
                    {
                        new DateTime(2016, 5, 1),
                        new DateTime(2016, 5, 5)
                    },
                    Type = DateTimeRestriction.RestrictionType.Deny
                }
            };
            _rangeRestriction = new RangeRestriction()
            {
                DefaultRestrictionType = DateTimeRestriction.RestrictionType.Allow,
                Restrictions = _restrictions
            };
        }

        [TestMethod]
        public void TestDefaultDenied()
        {
            _rangeRestriction.DefaultRestrictionType = DateTimeRestriction.RestrictionType.Deny;
            Assert.IsTrue(_rangeRestriction.IsDateValid(new DateTime(2016, 5, 6)));
            Assert.IsTrue(_rangeRestriction.IsDateValid(new DateTime(2016, 5, 7)));
            Assert.IsFalse(_rangeRestriction.IsDateValid(new DateTime(2016, 3, 1))); //Tuesday
            var aTuesday = new DateTime(2016, 1, 5);
            while (aTuesday.DayOfWeek != DayOfWeek.Friday)
            {
                Assert.IsFalse(_rangeRestriction.IsDateValid(aTuesday));
                aTuesday = aTuesday.AddDays(1);
            }
        }

        [TestMethod]
        public void TestDefaultAllowed()
        {
            _rangeRestriction.DefaultRestrictionType = DateTimeRestriction.RestrictionType.Allow;
            Assert.IsTrue(_rangeRestriction.IsDateValid(new DateTime(2016, 5, 6)));
            Assert.IsFalse(_rangeRestriction.IsDateValid(new DateTime(2016, 5, 7)));
            Assert.IsTrue(_rangeRestriction.IsDateValid(new DateTime(2016, 3, 1))); //Tuesday
        }
    }
}