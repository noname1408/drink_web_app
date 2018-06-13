using Drink.Data.interfaces;
using Drink.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drink.Data.Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly DrinkAppDbContext _dbContext;
        public DrinkRepository(DrinkAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IEnumerable<Models.Drink> Drinks => _dbContext.Drinks.Include(c => c.Category);

        public IEnumerable<Models.Drink> PreferredDrinks => _dbContext.Drinks.Where(p=>p.IsPreferredDrink).Include(c=>c.Category);

        public Models.Drink GetDrinkById(int drinkId) => _dbContext.Drinks.FirstOrDefault(x => x.DrinkId == drinkId);
    }
}
