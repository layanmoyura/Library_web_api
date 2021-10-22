using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Model
{
    public class Customer:IdentityUser
    {
       
      
        public String FirstName { get; set; }
        public String LastName { get; set; }
    }
}
