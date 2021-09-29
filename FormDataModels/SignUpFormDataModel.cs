using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nome.FormDataModels
{
    public class SignUpFormDataModel
    {

        [EmailAddress]
        [Required]
        public string Email { set; get; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { set; get; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { set; get; }

    }
}