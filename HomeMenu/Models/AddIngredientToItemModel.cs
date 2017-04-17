using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeMenu.Models
{
    public class AddIngredientToItemModel
    {
        public string UserId { get; set; }
        public long ItemId { get; set; }
        public long IngredientId { get; set; }
    }
}