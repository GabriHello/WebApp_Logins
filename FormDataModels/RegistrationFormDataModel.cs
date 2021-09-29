using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Nome.FormDataModels
{
    public class RegistrationFormDataModel
    {
        [Required(ErrorMessage = "{0} cannot be empty! <-- CUSTOM ERROR STRING")]
        public string FirstName { set; get; }

        [Required]
        public string LastName { set; get; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { set; get; }

        [EmailAddress]
        [Required]
        public string Email { set; get; }

        public bool? IsMale { get; set; }

        [Required]
        [Range(0, 600)]
        public float Weight { get; set; }

        [Required]
        [Range(0, 300)]
        public float Height { get; set; }

    }
}