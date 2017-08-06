using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopASP.ViewModels
{
    public class BasketRemoveViewModel
    {
        public decimal CartTotal
        {
            get;
            set;
        }

        public int CartItemsCount
        {
            get;
            set;
        }

        public int RemovedItemCount
        {
            get;
            set;
        }

        public int RemoveItemId
        {
            get;
            set;
        }
    }
}