using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A3THH.Controllers
{
    public class MediaTypesController : Controller
    {
        // GET: MediaTypes
        private Manager m = new Manager();

        public ActionResult Index()
        {
            return View(m.MediaTypeGetAll());
        }
    }
}
