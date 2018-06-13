
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drink.ViewModels
{
    public class DrinkListViewModel
    {
        public IEnumerable<Data.Models.Drink> Drinks { get; set; }
        public string CurrentCategory { get; set; }
    }
}
