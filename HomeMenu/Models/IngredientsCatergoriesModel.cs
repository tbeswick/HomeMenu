﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeMenu.Models
{
    public class IngredientsCatergoriesModel
    {

        public long Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime Added { get; set; }
        public DateTime Modified { get; set; }

    }
}