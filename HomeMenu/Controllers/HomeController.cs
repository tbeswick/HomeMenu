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


        public ActionResult Index()
        {


            return View();

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