using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopASP.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public string ImageFileName { get; set; }
        public bool IsBestseller { get; set; }
        public bool IsHidden { get; set; }

        public virtual Category Category { get; set; }
    }
}