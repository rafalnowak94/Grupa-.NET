using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopASP.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string CodeAndCity { get; set; }

        public string Comment { get; set; }

        public DateTime CreateDate { get; set; }

        public OrderStatus OrederStatus { get; set; }

        public decimal TotalPrice { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        public string UserId { get; internal set; }
    }

    public enum OrderStatus
    {
        New,

        Shipped
    }
}