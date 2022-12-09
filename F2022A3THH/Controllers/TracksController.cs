using F2022A3THH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A3THH.Controllers
{
    public class TracksController : Controller
    {
        // Reference to the data manager
        private Manager m = new Manager();

        // GET: Tracks
        public ActionResult Index()
        {
            return View(m.TrackGetAllWithDetail());
        }

        // GET: Tracks/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.TrackGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);
            }
        }

        // GET: Tracks/Create
        public ActionResult Create()
        {
            // Create a form
            var form = new TrackAddFormViewModel();

            // Configure the SelectList for the item-selection element on the HTML Form
            form.AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title", 156);

            // Configure the SelectList for the item-selection element on the HTML Form
            form.MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name", 1);

            return View(form);
        }

        // POST: Tracks/Create
        [HttpPost]
        public ActionResult Create(TrackAddViewModel newTrack)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newTrack);
            }

            // Process the input
            var addedTrack = m.TrackAdd(newTrack);

            if (addedTrack == null)
            {
                return View(newTrack);
            }
            else
            {
                return RedirectToAction("details", new { id = addedTrack.TrackId });
            }
        }

    }
}
