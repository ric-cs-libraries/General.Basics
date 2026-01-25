using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.Intervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Basics._Extensions.Dates
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Retourne true si : lowerDateTime &lt;= value &lt;= higherDateTime.
        /// </summary>
        /// <exception cref="ValueShouldBeLowerOrEqualToException{T}"></exception>
        public static bool IsBetween(this DateTime dateTime, DateTime? lowerDateTime, DateTime? higherDateTime)
        {
            DateTimesInterval dateTimesInterval = new(lowerDateTime, higherDateTime);

            bool response = dateTime.IsInto(dateTimesInterval);
            return response;
        }

        /// <summary>
        /// Retourne true si : dateTimeInterval.MinValue &lt;= value &lt;= dateTimeInterval.MaxValue.
        /// </summary>
        public static bool IsInto(this DateTime dateTime, DateTimesInterval dateTimesInterval)
        {
            bool response = dateTimesInterval.Contains(dateTime);
            return response;
        }
    }
}
