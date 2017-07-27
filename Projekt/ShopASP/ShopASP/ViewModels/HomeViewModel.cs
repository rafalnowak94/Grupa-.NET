using ShopASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopASP.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Item> Bestsellers { get; set; }
        public IEnumerable<Item> NewItems { get; set; }
    }

}