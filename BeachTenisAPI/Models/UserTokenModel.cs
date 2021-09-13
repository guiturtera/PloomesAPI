using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeachTenisAPI.Models
{
    /// <summary>
    /// Used for return of the login
    /// </summary>
    public class UserTokenModel
    {
        /// <summary>
        /// The current login of the user without its key.
        /// </summary>
        [Required]
        public User User { get; set; }

        /// <summary>
        /// Token of a user, available for 2 hours.
        /// </summary>
        [Required]
        public string Token { get; set; }
    }
}
