using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A3THH.Models
{
    public class PlaylistEditTracksFormViewModel : PlaylistEditTracksViewModel
    {
        public PlaylistEditTracksFormViewModel()
        {
            Tracks = new List<TrackBaseViewModel>();
        }

        public int TracksCount { get; set; }

        [Display(Name = "All tracks")]
        public MultiSelectList TrackList { get; set; }

        public IEnumerable<TrackBaseViewModel> Tracks;
    }





}