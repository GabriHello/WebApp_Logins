using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nome.Models
{
    public class UserProfileFormDataModel
    {
        
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public int? Citizenship { get; set; }

    }
}