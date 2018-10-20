using System.Collections.Generic;

namespace ClassLibrary1.Orders
{
    public class OrderRequestData
    {
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public ICollection<string> Items { get; set; }
    }
}