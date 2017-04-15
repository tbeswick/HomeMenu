using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeMenu.Models
{
    public class HomeIndexModel
    {

        public IList<MenuItemModel> MenuItems { get; set; }

        public IList<MenuIngredientModel> MenuIngredients { get; set; }




    }
}