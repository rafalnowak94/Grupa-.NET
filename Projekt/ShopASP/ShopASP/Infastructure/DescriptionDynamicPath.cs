using MvcSiteMapProvider;
using ShopASP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopASP.Infastructure
{
    public class DescriptionDynamicPath : DynamicNodeProviderBase
    {
        private ShopContext db = new ShopContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var returnValue = new List<DynamicNode>();
            foreach (var item in db.Items)
            {
                DynamicNode n = new DynamicNode();
                n.Title = item.Title;
                n.Key = "Item" + item.ItemId;
                n.ParentKey = "Category_" + item.CategoryId;
                n.RouteValues.Add("id", item.ItemId);
                returnValue.Add(n);
            }

            return returnValue;
        }
    }
}