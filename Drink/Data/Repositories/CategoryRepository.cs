using Drink.Data.interfaces;
using Drink.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drink.Data.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly DrinkAppDbContext _dbContext;
        public CategoryRepository(DrinkAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<Category> Categories => _dbContext.Categories;
    }
}
