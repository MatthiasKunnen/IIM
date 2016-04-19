using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IIM.Models.Domain
{
    public class DateTimeRestriction
    {
        public List<DayOfWeek> DaysOfWeek { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public List<DateTime> Dates { get; private set; }
        public RestrictionType Type { get; set; }

        public DateTimeRestriction()
        {
            
        }

        public DateTimeRestriction(List<DayOfWeek> daysOfWeek, DateTime timeStart, DateTime timeEnd, List<DateTime> dates, RestrictionType restrictionType)
        {
            DaysOfWeek = daysOfWeek;
            TimeStart = timeStart;
            TimeEnd = timeEnd;
            Dates = dates;
            Type = restrictionType;
        }

        public bool? IsValid(DateTime dateTime)
        {
            return Dates.All(d => dateTime.Date == d.Date) && DaysOfWeek.Contains(dateTime.DayOfWeek) &&
                   (TimeStart.TimeOfDay <= dateTime.TimeOfDay && TimeEnd.TimeOfDay >= dateTime.TimeOfDay)
                ? (bool?) (Type == RestrictionType.Allow)
                : null; //Restriction is not applicable
        }

        public enum RestrictionType
        {
            Allow, Deny
        }
    }
}