using System;
using System.Collections.Generic;
using System.Linq;

namespace IIM.Models.Domain
{
    public class DateTimeRestriction
    {
        public List<DateTime> Dates { get; set; }

        public RestrictionType Type { get; set; }

        public List<DayOfWeek> DaysOfWeek { get; set; }

        public DateTime? TimeStart { get; set; }

        public DateTime? TimeEnd { get; set; }

        public DateTimeRestriction(List<DayOfWeek> daysOfWeek, DateTime timeStart, DateTime timeEnd, List<DateTime> dates, RestrictionType restrictionType)
        {
            DaysOfWeek = daysOfWeek;
            TimeStart = timeStart;
            TimeEnd = timeEnd;
            Dates = dates;
            Type = restrictionType;
        }

        public DateTimeRestriction()
        {
        }

        /// <summary>
        /// Check if a date is valid according to this restriction.
        /// </summary>
        /// <param name="dateTime">The date to check.</param>
        /// <returns>True if the date is valid, False if the date is not valid and null if the restriction does not apply to the given date.</returns>
        public bool? IsValid(DateTime dateTime)
        {
            if (Dates == null && DaysOfWeek == null && (TimeStart == null || TimeEnd == null))
                return null;
            return (Dates?.Contains(dateTime.Date) ?? true)
                    && (DaysOfWeek?.Contains(dateTime.DayOfWeek) ?? true)
                    && ((TimeStart == null || TimeEnd == null) 
                        || (TimeStart.Value.TimeOfDay <= dateTime.TimeOfDay && TimeEnd.Value.TimeOfDay >= dateTime.TimeOfDay))
                ? (bool?)(Type == RestrictionType.Allow)
                : null; //Restriction is not applicable
        }

        public enum RestrictionType
        {
            Allow, Deny
        }
    }
}