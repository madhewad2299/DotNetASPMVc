using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ACTSPaperWithoutEntity.Models
{
    public class BookModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "Please Enter Author")]
        public string BookAuthor { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please Enter Price")]
        public decimal BookPrice { get; set; }

    }
}