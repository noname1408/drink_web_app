using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drink.Data.interfaces
{
    public interface IDrinkRepository
    {
        IEnumerable<Models.Drink> Drinks { get;  }
        IEnumerable<Models.Drink> PreferredDrinks { get; }
        Models.Drink GetDrinkById(int drinkId);
    }
}
