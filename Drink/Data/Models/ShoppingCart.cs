using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drink.Data.Models
{
    public class ShoppingCart
    {
        private readonly DrinkAppDbContext _dbContext;
        public ShoppingCart(DrinkAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = service.GetService<DrinkAppDbContext>();

            string cardId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cardId);

            return new ShoppingCart(context) { ShoppingCartId = cardId };
        }

        public void AddToCart(Drink drink, int amount)
        {
            var shoppingCartItem = _dbContext.shoppingCartItems.SingleOrDefault(s => s.Drink.DrinkId == drink.DrinkId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Drink = drink,
                    Amount = amount
                };
                _dbContext.shoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _dbContext.SaveChanges();
        }
        public int RemoveFromCart(Drink drink)
        {
            var shoppingCartItem = _dbContext.shoppingCartItems
                .SingleOrDefault(s => s.Drink.DrinkId == drink.DrinkId && s.ShoppingCartId == ShoppingCartId);
            var localAmount = 0;
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _dbContext.shoppingCartItems.Remove(shoppingCartItem);

                }
                _dbContext.SaveChanges();

            }
            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _dbContext.shoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Drink)
                .ToList());
        }
        public void ClearCart()
        {
            var cartItems = _dbContext.shoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId);
            _dbContext.shoppingCartItems.RemoveRange(cartItems);
            _dbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _dbContext.shoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).
                Select(c => c.Drink.Price * c.Amount).Sum();
            return total;
        }
    }
}
