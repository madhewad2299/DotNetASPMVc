using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoginPage.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [DataType (DataType.Text)]
        [Required(ErrorMessage = "Please Enter Name")]
        [StringLength(15, ErrorMessage ="The {0} length cannot exceed {1} characters.." )]
        public string LoginName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        public string Fullname { get; set; }

        //[EmailAddress]
        [Required(ErrorMessage = "Email Id is required")]
        //[StringLength(0, ErrorMessage = "Email Id is required")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        //[Phone]
        [Required(ErrorMessage = "Mobile no. is required")]
        public string Phone { get; set; }

    }
}