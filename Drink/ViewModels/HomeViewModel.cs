using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drink.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Drink.Data.Models.Drink> PreferredDrinks { get; set; }

    }
}
