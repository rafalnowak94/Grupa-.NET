using ShopASP.DAL;
using ShopASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopASP.Infastructure
{
    public class CartManager
    {
        private ShopContext db;

        private ISessionFuncs session;

        public const string CartSessionKey = "CartData";
        public CartManager (ISessionFuncs session, ShopContext db)
        {
            this.session = session;
            this.db = db;
        }

        public void AddToCart(int itemid)
        {
            //get acutal cart session
            var cart = this.GetCart();

            //get items 
            var cartItem = cart.Find(c => c.Item.ItemId== itemid);

            if (cartItem != null)
                cartItem.Quantity++;
            else
            {
                // Find item and add it to cart
                var itemToAdd = db.Items.Where(a => a.ItemId == itemid).SingleOrDefault();
                if (itemToAdd != null)
                {
                    var newCartItem = new CartItem()
                    {
                        Item = itemToAdd,
                        Quantity = 1,
                        TotalPrice = itemToAdd.Price
                    };

                    cart.Add(newCartItem);
                }
            }

            session.Set(CartSessionKey, cart);
        }

        public List<CartItem> GetCart()
        {
            List<CartItem> cart;

            if (session.Get<List<CartItem>>(CartSessionKey) == null)
            {
                cart = new List<CartItem>();
            }
            else
            {
                cart = session.Get<List<CartItem>>(CartSessionKey) as List<CartItem>;
            }

            return cart;
        }

        public int RemoveFromCart(int itemid)
        {
            var cart = this.GetCart();

            var cartItem = cart.Find(c => c.Item.ItemId == itemid);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    return cartItem.Quantity;
                }
                else
                    cart.Remove(cartItem);
            }

            // Return count of removed item currently inside cart
            return 0;
        }


        public decimal GetCartTotalPrice()
        {
            var cart = this.GetCart();
            return cart.Sum(c => (c.Quantity * c.Item.Price));
        }

        public int GetCartItemsCount()
        {
            var cart = this.GetCart();
            int count = cart.Sum(c => c.Quantity);

            return count;
        }

        public Order CreateOrder(Order newOrder, string userId)
        {
            var cart = this.GetCart();

            newOrder.CreateDate = DateTime.Now;
            newOrder.UserId = userId;

            this.db.Orders.Add(newOrder);

            if (newOrder.OrderItems == null)
                newOrder.OrderItems = new List<OrderItem>();

            decimal cartTotal = 0;

            foreach (var cartItem in cart)
            {
                var newOrderItem = new OrderItem()
                {
                    ItemId = cartItem.Item.ItemId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Item.Price
                };

                cartTotal += (cartItem.Quantity * cartItem.Item.Price);

                newOrder.OrderItems.Add(newOrderItem);
            }

            newOrder.TotalPrice = cartTotal;

            this.db.SaveChanges();

            return newOrder;
        }

        public void EmptyCart()
        {
            session.Set<List<CartItem>>(CartSessionKey, null);
        }

    }
}
