using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeachTenisAPI.Models
{
    /// <summary>
    /// Spreadsheet for users.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// User CPF is used as a FK, and builds a Composite PK with `Day`.
        /// </summary>
        [ForeignKey("FK_User")]
        [Required]
        public string User { get; set; }
        /// <summary>
        /// The CPF of the COACH.
        /// </summary>
        [ForeignKey("FK_Coach")]
        [Required]
        public string Coach { get; set; }

        /// <summary>
        /// The day of the Spreadsheet in the format `yyyy-mm-dd`. There is only only Spreadsheet in a day.
        /// </summary>
        [Required][DataType(DataType.Date)]
        public DateTime Day { get; set; }
        /// <summary>
        /// Distance to do in kilometers.
        /// </summary>
        [Required]
        public double Distance { get; set; }
        /// <summary>
        /// Pace to keep for the distance, in km
        /// </summary>
        [Required]
        public double Pace { get; set; }
    }
}
