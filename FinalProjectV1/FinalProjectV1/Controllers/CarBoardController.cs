using FinalProjectV1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectV1.Controllers
{
    public class CarBoardController : Controller
    {
        // GET: CarBoard
        public ActionResult Index()
        {
            List<Advertisment> ads = new List<Advertisment>();
            ads = DBHelper.returnAdvertisments();


            return View();
        }
    }
}