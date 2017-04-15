using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMenu.Models
{
    public interface IMenuIngredientRepository
    {
        Task<bool> CreateItem(MenuIngredientModel model);
        Task<bool> UpdateItem(MenuIngredientModel model);
        Task<IList<MenuIngredientModel>> GetItems(string userid);

    }
}
