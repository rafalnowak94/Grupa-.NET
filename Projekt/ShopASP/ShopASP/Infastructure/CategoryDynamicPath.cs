using MvcSiteMapProvider;
using ShopASP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopASP.Infastructure
{
    public class CategoryDynamicPath : DynamicNodeProviderBase
    {
        private ShopContext db = new ShopContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var returnValue = new List<DynamicNode>();
            foreach (var category in db.Categories)
            {
                DynamicNode n = new DynamicNode();
                n.Title = category.Name;
                n.Key = "Category_" + category.CategoryId;
                n.RouteValues.Add("categoryname", category.Name);
                returnValue.Add(n);
            }

            return returnValue;
        }
    }
}