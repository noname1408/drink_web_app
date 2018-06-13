using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drink.Data.interfaces;
using Drink.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Drink.Controllers
{
    public class DrinksController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDrinkRepository _drinkRepository;
        public DrinksController(ICategoryRepository categoryRepository,IDrinkRepository drinkRepository)
        {
            this._categoryRepository = categoryRepository;
            this._drinkRepository = drinkRepository;
        }
        public ViewResult List(string category)
        {
            string _category = category;
            IEnumerable<Drink.Data.Models.Drink> drinks;

            string currentCategory = string.Empty;
            if (string.IsNullOrEmpty(category))
            {
                drinks = _drinkRepository.Drinks.OrderBy(x => x.DrinkId);
                currentCategory = "All drinks";
            }
            else
            {
                if (string.Equals("Alcoholic", _category, StringComparison.OrdinalIgnoreCase))
                {
                    drinks = _drinkRepository.Drinks.Where(x => x.Category.CategoryName.Equals("Alcoholic")).OrderBy(x => x.DrinkId);
                }
                else
                {
                    drinks = _drinkRepository.Drinks.Where(x => x.Category.CategoryName.Equals("Non-alcoholic")).OrderBy(x => x.DrinkId);
                }
                currentCategory = _category;
            }
            var drinkListViewModel = new DrinkListViewModel
            {
                Drinks = drinks,
                CurrentCategory = currentCategory
            };
            return View(drinkListViewModel);
        }
    }
}