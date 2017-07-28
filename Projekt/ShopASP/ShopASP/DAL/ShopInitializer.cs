using ShopASP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopASP.DAL
{
    public class ShopInitializer : DropCreateDatabaseAlways<ShopContext>
    {
        protected override void Seed(ShopContext context)
        {
            SeedShopData(context);
            base.Seed(context);
        }

        private void SeedShopData(ShopContext context)
        {
            var categories = new List<Category>
            {
                new Category() {Name="Odzież"},
                new Category() {Name="Obuwie"},
                new Category() {Name="Sport"},
                new Category() {Name="Akcesoria"}
            };

            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            var items = new List<Item>
            {
                new Item() {CategoryId=2, Description="Kubot czyli najlepszy przyjaciel Janusza", ImageFileName="1.jpg", IsBestseller=true, Title="Klapek Kubot", CreateDate=(new DateTime(2017,7,26)) },
                new Item() {CategoryId=2, Description="Kubot czyli najlepszy przyjaciel Janusza", ImageFileName="1.jpg", IsBestseller=true, Title="adidas", CreateDate=(new DateTime(2017,7,26)) }
            };

            items.ForEach(c => context.Items.Add(c));
            context.SaveChanges();
        }
    }
}