using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using HomeMenu.Models;
using System.Configuration;
using System.IO;

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
        public async Task<ActionResult> Index()
        {

            if (User.Identity.IsAuthenticated == false)
            {
                Response.Redirect("Account/Login");
            }

            string userId = User.Identity.GetUserId();

            MenuItemRepository murep = new MenuItemRepository(sqlConnection);
            HomeIndexModel homeIndex = new HomeIndexModel();
            homeIndex.MenuItems = await murep.GetItems(userId);


            return View(homeIndex);
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
            md.CurrentIngredients = await rep.GetCurrentItemIngredients(ModelId);

            return View(md);
        }

        [Route("Menu/AddImage")]
        [HttpPost]
        public async Task<ActionResult> AddImageAsync(HttpPostedFileBase file, int itemid)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/assets/img/"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);

                    MenuItemRepository rep = new MenuItemRepository(sqlConnection);
                    MenuItemModel md = await rep.GetItemById(itemid);
                    md.Image = file.FileName;
                    md.Modified = DateTime.UtcNow;
                    await rep.UpdateItem(md);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return Redirect("/Menu/Edit/" + itemid.ToString());
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


        [Route("Menu/AddIngredient")]
        [HttpPost]
        public async Task<ActionResult> AddIngredientAsync(AddIngredientToItemModel model)
        {
            MenuItemRepository rep = new MenuItemRepository(sqlConnection);
            await rep.AddIngredientToMenuItem(model);


            return null;

        }


        [Route("Menu/DeleteIngredient")]
        [HttpPost]
        public async Task<ActionResult> DeleteIngredientAsync(AddIngredientToItemModel model)
        {
            MenuItemRepository rep = new MenuItemRepository(sqlConnection);
            await rep.DeleteIngredientToMenuItem(model);


            return null;

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