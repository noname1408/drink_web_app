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
        public ViewResult List()
        {
            DrinkListViewModel vm = new DrinkListViewModel();
            vm.Drinks = _drinkRepository.Drinks;
            vm.CurrentCategory = "DrinkCategory";
            return View(vm);
        }
    }
}