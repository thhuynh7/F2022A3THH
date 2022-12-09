using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A3THH.Models
{
    public class TrackAddViewModel : TrackBaseViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Album name exceeds valid length")]
        public int AlbumId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Media type exceeds valid length")]
        public int MediaTypeId { get; set; }
    }
}