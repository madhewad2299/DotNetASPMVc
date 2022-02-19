using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationApi.Models
{
    public class Student
    {
        [Key]
        public int StudId { get; set; }
        public string StudName { get; set; }
        public string StudGender { get; set; }
    }
}