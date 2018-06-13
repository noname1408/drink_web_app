using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drink.Data
{
    public class DrinkAppDbContext:DbContext
    {
        public DrinkAppDbContext(DbContextOptions<DrinkAppDbContext> options) : base(options)
        {

        }
        public DbSet<Models.Drink> Drinks { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.ShoppingCartItem> shoppingCartItems { get; set; }
    }
}
