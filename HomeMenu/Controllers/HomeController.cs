using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Threading.Tasks;
using HomeMenu.Models;
using Microsoft.AspNet.Identity;

namespace HomeMenu.Controllers
{
    public class HomeController : Controller
    {

        private string sqlConnection;

        public HomeController()
        {
            sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }


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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}