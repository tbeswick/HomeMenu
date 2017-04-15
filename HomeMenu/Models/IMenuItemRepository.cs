using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HomeMenu.Models
{
    public interface IMenuItemRepository
    {

       Task<int> CreateItem(MenuItemModel model);
       Task<bool> UpdateItem(MenuItemModel model);
        Task<IList<MenuItemModel>> GetItems(string userId);
    }
}