using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeMenu.Models;
using System.Configuration;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace HomeMenu.Controllers
{
    public class IngredientsController : Controller
    {

        private string sqlConnection;

        public IngredientsController()
        {
            sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }



        // GET: Ingredients
        public async Task<ActionResult> Index()
        {

            MenuIngredientRepository rep = new MenuIngredientRepository(sqlConnection);
            string id = User.Identity.GetUserId();
            IngredientIndexModel md = new IngredientIndexModel();
            md.Ingredients = await rep.GetItems(id);
             

            return View(md);
        }
    }
}