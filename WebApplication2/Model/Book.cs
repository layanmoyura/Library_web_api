using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Model
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Editon { get; set; }
        public string LastEditDate { get; set; }
        public string Link { get; set; }
        public string CoverImage { get; set; }
    } 
}
