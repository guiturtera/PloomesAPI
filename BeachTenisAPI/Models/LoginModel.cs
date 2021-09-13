using System;
using System.ComponentModel.DataAnnotations;

namespace BeachTenisAPI
{
    /// <summary>
    /// Schema for realizing login.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// CPF of an existing user.
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]{11}")]
        public string CPF { get; set; }

        /// <summary>
        /// Password of an existing user.
        /// </summary>
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Please verify if you entered a valid role in `EnumUserRoles` Scheme!")]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
