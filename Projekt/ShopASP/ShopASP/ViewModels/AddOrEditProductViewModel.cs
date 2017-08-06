using ShopASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopASP.ViewModels
{
    public class AddOrEditProductViewModel
    {
        public Item Item { get; set; }

        public IEnumerable<Category> Category { get; set; }

        public bool? ConfirmSuccess { get; set; }


    }
}