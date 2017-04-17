using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeMenu.Models
{
    public class EditIngredientModel
    {
        public IList<IngredientsCatergoriesModel> IngredientCatergories { get; set; }

        public MenuItemModel MenuIngredient { get; set; }

    }
}