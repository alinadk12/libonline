using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibOnline.Models
{
    public class Genre
    {
        //ID жанра
        [Key]
        [Display(Name = "ID жанра")]
        public int GenreId { get; set; }

        //название
        [Display(Name = "Название жанра")]
        public string Name { get; set; }

        //описание
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        //книги этого жанра (много ко многим)
        [Display(Name = "Книги этого жанра")]
        public virtual ICollection<Book> Books { get; set; }
    }
}