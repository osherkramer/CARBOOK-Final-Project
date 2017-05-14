using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProjectV1.Models;
using FinalProjectV1.Helpers;

namespace FinalProjectV1.Controllers
{
    public class CarBoardController : Controller
    {

        // GET: CarBoard
        public ActionResult Index()
        {
           
            List<Advertisement> ads = new List<Advertisement>();
            ads = DBHelper.returnAdvertisments();


            return View();
        }
    }
}