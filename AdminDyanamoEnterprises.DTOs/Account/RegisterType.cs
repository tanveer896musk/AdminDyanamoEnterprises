using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.DTOs.Account
{
    public class RegisterType
    {
        [Required(ErrorMessage = "Fullname is required")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Emailid { get; set; }
        [Required(ErrorMessage = "Mobileno is required")]
        public int mobileno { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }
}
