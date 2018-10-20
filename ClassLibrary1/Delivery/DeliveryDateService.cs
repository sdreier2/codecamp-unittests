using System;

namespace ClassLibrary1.Delivery
{
    public interface IDeliveryDateService
    {
        string GetNextAvailableDeliveryDate();
    }

    public class DeliveryDateService : IDeliveryDateService
    {
        #region Hidden
        private readonly IDateTimeHelper _dateTimeHelper;
        public DeliveryDateService(IDateTimeHelper dateTimeHelper)
        {
            _dateTimeHelper = dateTimeHelper;
        }

        public string GetNextAvailableDeliveryDate()
        {
            //cutoff time for is 4:30PM for next-day delivery
            TimeSpan cutoff = new TimeSpan(16, 30, 0);
            DateTime now = _dateTimeHelper.GetCurrentDateTime();
            string format = "MM/dd/yyyy";
            if (now.TimeOfDay > cutoff)
            {
                return now.AddDays(2).ToString(format);
            }

            return now.AddDays(1).ToString(format);
        }
        #endregion Hidden

        //public string GetNextAvailableDeliveryDate()
        //{
        //    //cutoff time for is 4:30PM for next-day delivery
        //    TimeSpan cutoff = new TimeSpan(16, 30, 0);
        //    DateTime now = DateTime.Now;
        //    string format = "MM/dd/yyyy";
        //    if (now.TimeOfDay > cutoff)
        //    {
        //        return now.AddDays(2).ToString(format);
        //    }

        //    return now.AddDays(1).ToString(format);
        //}
    }
}