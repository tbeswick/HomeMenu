using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeMenu.Models
{
    public class MenuItemModel
    {

        public long Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Catergory {get;set;}
        public string Image { get; set; }
        public DateTime Added { get; set; }

        public DateTime Modified { get; set; }

        public IList<MenuIngredientModel> Ingredients { get; set; }
    }
}