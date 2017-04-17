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
            md.IngredientCatergories = await rep.GetCatergories(id);
            md.Ingredients = await rep.GetItems(id);
             

            return View(md);
        }

        public async Task<ActionResult> Add()
        {
            MenuIngredientRepository rep = new MenuIngredientRepository(sqlConnection);
            EditIngredientModel md = new EditIngredientModel();

            md.IngredientCatergories = await rep.GetCatergories(User.Identity.GetUserId());


            return View(md);
        }



        [Route("Ingredients/AddItem")]
        [HttpPost]
        public async Task<ActionResult> AddItemAsync(string ingredientItemName, string menuIngredientCatergory)
        {

            MenuIngredientRepository rep = new MenuIngredientRepository(sqlConnection);
            MenuIngredientModel model = new MenuIngredientModel();


            model.UserId = User.Identity.GetUserId();
            model.Name = ingredientItemName;
            model.Catergory = menuIngredientCatergory;
            model.Added = DateTime.UtcNow;
            model.Modified = model.Added;

            await rep.CreateItem(model);

            return Redirect("/Ingredients");


        }

    }
}