using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Model
{
    public class AdminLogin
    {
        [Required]
        public string ID { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
