using System;

namespace ClassLibrary1.Delivery
{
    public interface IDateTimeHelper
    {
        DateTime GetCurrentDateTime();
    }

    /// <summary>
    /// A wrapper to help mock date time generation
    /// </summary>
    /// <seealso cref="ClassLibrary1.Delivery.IDateTimeHelper" />
    public class DateTimeHelper : IDateTimeHelper
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}