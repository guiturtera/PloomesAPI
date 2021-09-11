using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeachTenisAPI.Models
{
    public class Spreadsheet
    {
        [ForeignKey("FK_User")]
        public string User { get; set; }
        [ForeignKey("FK_Coach")]
        public string Coach { get; set; }

        [Required]
        public DateTime Day { get; set; }
        [Required]
        public double Distance { get; set; } // km
        [Required]
        public double Pace { get; set; } // km/h
    }
}
