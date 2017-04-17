using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeMenu.Models
{
    public class IngredientToItemModel
    {
        public long Id { get; set; }
        public long MenuItem { get; set;}
        public long MenuIngredient { get; set; }
    }
}