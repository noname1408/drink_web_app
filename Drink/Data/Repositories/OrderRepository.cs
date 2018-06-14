using Drink.Data.interfaces;
using Drink.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drink.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DrinkAppDbContext _dbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(DrinkAppDbContext dbContext,ShoppingCart shoppingCart)
        {
            _dbContext = dbContext;
            _shoppingCart = shoppingCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced=DateTime.Now;
            _dbContext.Orders.Add(order);

            var shoppingCartItem = _shoppingCart.ShoppingCartItems;

            foreach (var item in shoppingCartItem)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = item.Amount,
                    DrinkId = item.Drink.DrinkId,
                    OrderId = order.OrderId,
                    Price = item.Drink.Price
                };
                _dbContext.OrderDetails.Add(orderDetail);
            }
            _dbContext.SaveChanges();
        }
    }
}
