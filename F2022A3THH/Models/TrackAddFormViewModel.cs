using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A3THH.Models
{
    public class TrackAddFormViewModel : TrackBaseViewModel
    {
        [Display(Name = "Album")]
        public SelectList AlbumList { get; set; }

        [Display(Name = "Media type")]
        public SelectList MediaTypeList { get; set; }
    }
}