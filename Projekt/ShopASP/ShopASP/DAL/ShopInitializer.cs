using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            SeedAdminUser(context);
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
                new Item() {CategoryId=2, Description="Kubot czyli najlepszy przyjaciel Janusza", ImageFileName="1.jpg", IsBestseller=true, Title="Klapek Kubot", CreateDate=(new DateTime(2017,7,26)),Price=15 },
                new Item() {CategoryId=2, Description="Kubot czyli najlepszy przyjaciel Janusza", ImageFileName="1.jpg", IsBestseller=true, Title="adidas", CreateDate=(new DateTime(2017,7,26)),Price=20}
            };
   

            items.ForEach(c => context.Items.Add(c));
            context.SaveChanges();
        }

        private void SeedAdminUser(ShopContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string adminName = "admin@admin.pl";

            //role create
            var role = new IdentityRole("Admin");
            roleManager.Create(role);

            //admin account create
            var admin = new ApplicationUser { UserName = adminName, Email = adminName, UserData = new UserData() };
            userManager.Create(admin, "P@ssw0rd");

            //add admin role
            userManager.AddToRole(admin.Id, role.Name);
        }
    }
}