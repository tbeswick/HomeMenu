using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using HomeMenu.Models;
using System.Configuration;

namespace HomeMenu.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {

        private string sqlConnection;

        public MenuController()
        {
            sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }


        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> Add()
        {
            MenuItemRepository rep = new MenuItemRepository(sqlConnection);
            EditMenuItemModel md = new EditMenuItemModel();

            md.ItemCatergories = await rep.GetItemCatergories(User.Identity.GetUserId());


            return View(md);
        }


        [Route("Menu/Edit/{ModelId}")]
        public async Task<ActionResult> Edit(long ModelId)
        {

            MenuItemRepository rep = new MenuItemRepository(sqlConnection);
            MenuIngredientRepository igrep = new MenuIngredientRepository(sqlConnection);
            EditMenuItemModel md = new EditMenuItemModel();

            md.MenuItem = await rep.GetItemById(ModelId);
            md.ItemCatergories = await rep.GetItemCatergories(User.Identity.GetUserId());
            md.AvailableIngredients = await igrep.GetItems(User.Identity.GetUserId());
            md.CurrentIngredients = new List<MenuIngredientModel>(); // rep.GetCurrentItemIngredients(User.Identity.GetUserId());

            return View(md);
        }


        [Route("Menu/AddItem")]
        [HttpPost]
        public async Task<ActionResult> AddItemAsync(string menuItemName, string menuItemCatergory)
        {

            MenuItemRepository rep = new MenuItemRepository(sqlConnection);
            MenuItemModel model = new MenuItemModel();


            model.UserId = User.Identity.GetUserId();
            model.Name = menuItemName;
            model.Catergory = menuItemCatergory;
            model.Added = DateTime.UtcNow;
            model.Modified = model.Added;

            int idret = await rep.CreateItem(model);
            if(idret < 0)
            {
                 return Redirect("/Menu/Add");
            }
            else
            {
               return Redirect("/Menu/Edit/" + idret);
            }



        }

        [Route("Menu/EditItem")]
        [HttpPost]
        public async Task<ActionResult> EditItem(MenuItemModel model)
        {

            MenuItemRepository rep = new MenuItemRepository(sqlConnection);
            model.Modified = DateTime.UtcNow;
            await rep.UpdateItem(model);

            return Redirect("/");
        }
    }
}