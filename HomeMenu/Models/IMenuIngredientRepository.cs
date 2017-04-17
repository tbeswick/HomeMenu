using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMenu.Models
{
    public interface IMenuIngredientRepository
    {
        Task<int> CreateItem(MenuIngredientModel model);
        Task<bool> UpdateItem(MenuIngredientModel model);
        Task<IList<MenuIngredientModel>> GetItems(string userid);

    }
}
