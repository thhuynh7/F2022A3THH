using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A3THH.Models
{
    public class PlaylistWithDetailViewModel : PlaylistBaseViewModel
    {
        public PlaylistWithDetailViewModel()
        {
            Tracks = new List<TrackBaseViewModel>();
        }

        [Display(Name = "Tracks on the Playlist")]
        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }
    }
}