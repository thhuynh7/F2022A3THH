using F2022A3THH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A3THH.Controllers
{
    public class PlaylistController : Controller
    {
        // GET: Playlist
        private Manager m = new Manager();

        public ActionResult Index()
        {
            return View(m.PlaylistGetAll());
        }


        // GET: Playlist/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.PlaylistGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                o.Tracks = o.Tracks.OrderBy(t => t.Name);
                return View(o);
            }
        }


        // GET: Playlist/Edit/5
        public ActionResult EditPlaylists(int? id)
        {
            // Attempt to fetch the matching object
            var o = m.PlaylistGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form, based on the fetched matching object
                var form = m.mapper.Map<PlaylistWithDetailViewModel, PlaylistEditTracksFormViewModel>(o);

                // For the multi select list, configure the "selected" items
                // Notice the use of the Select() method, 
                // which allows us to select/return/use only some properties from the source
                var selectedValues = o.Tracks.Select(e => e.TrackId);
                form.Tracks = o.Tracks.OrderBy(t => t.Name);

                // For clarity, use the named parameter feature of C#
                form.TrackList = new MultiSelectList
                    (items: m.TrackGetAllWithDetail(),
                    dataValueField: "TrackId",
                    dataTextField: "NameFull",
                    selectedValues: selectedValues);

                return View(form);
            }
        }


        // POST: Playlist/Edit/5
        [HttpPost]
        public ActionResult EditPlaylists(int? id, PlaylistEditTracksViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("Edit", new { id = newItem.PlaylistId });
            }

            if (id.GetValueOrDefault() != newItem.PlaylistId)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            try
            {
                // TODO: Add update logic here
                var editedItem = m.PlaylistEditTracks(newItem);
                return RedirectToAction("details", new { id = newItem.PlaylistId });
            }
            catch
            {
                return RedirectToAction("Edit", new { id = newItem.PlaylistId });
            }
        }
    }
}
