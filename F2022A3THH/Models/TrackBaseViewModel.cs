using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A3THH.Models
{
    public class TrackBaseViewModel
    {
        [Key]
        public int TrackId { get; set; }

        [Required(ErrorMessage = "Track name is required")]
        [StringLength(200)]
        [Display(Name = "Track name")]
        public string Name { get; set; }

        [StringLength(220, ErrorMessage = "Composer name exceeds valid length")]
        [Required(ErrorMessage = "Composer is required")]
        public string Composer { get; set; }

        [Display(Name = "Length (ms)")]
        [Range(0.01, float.MaxValue, ErrorMessage = "Length is out of valid range")]
        public int Milliseconds { get; set; }

        [Display(Name = "Unit price")]
        [Range(0.01, float.MaxValue, ErrorMessage = "Unit price is out of valid range")]
        public decimal UnitPrice { get; set; }


        // Composed read-only property to display full name. 
        public string NameFull
        {
            get
            {
                var ms = Math.Round((((double)Milliseconds / 1000) / 60), 1);

                var composer = string.IsNullOrEmpty(Composer) ? "" : ", composer " + Composer;
                var trackLength = (ms > 0) ? ", " + ms.ToString() + " minutes" : "";
                var unitPrice = (UnitPrice > 0) ? ", $ " + UnitPrice.ToString() : "";

                return string.Format("{0}{1}{2}{3}", Name, composer, trackLength, unitPrice);
            }
        }

        // Composed read-only property to display short name. 
        public string NameShort
        {
            get
            {
                var ms = Math.Round((((double)Milliseconds / 1000) / 60), 1);
                var trackLength = (ms > 0) ? ms.ToString() + " minutes" : "";
                var unitPrice = (UnitPrice > 0) ? " $ " + UnitPrice.ToString() : "";

                return string.Format("{0} - {1} - {2}", Name, trackLength, unitPrice);
            }
        }
    }
}