using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeMenu.Models
{
    public class EditMenuItemModel
    {

        public IList<ItemCatergoriesModel> ItemCatergories { get; set; }

        public IList<MenuIngredientModel> AvailableIngredients { get; set; }

        public IList<MenuIngredientModel> CurrentIngredients { get; set; }

        public MenuItemModel MenuItem { get; set; }

    }
}