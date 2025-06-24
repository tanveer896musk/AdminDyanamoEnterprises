using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.DTOs
{
    public class LoginType
    {
        public int Userid { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? Emailid { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
       
    }
}
