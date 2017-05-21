using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibOnline.Models
{
    public class Book
    {
        //ID книги
        [Key]
        [Display(Name = "Id книги")]
        public int BookId { get; set; }

        //название
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательное поле.")]
        public string Title { get; set; }

        //количество страниц
        [Display(Name = "Количество страниц")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int NumberOfPage { get; set; }

        //год издания
        [Display(Name = "Год издания")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int YearOfPublishing { get; set; }

        //описание
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        //ID издательства
        [ForeignKey("PublishingHouseId")]
        [Display(Name = "Издательство")]
        public int PublishingHouseId;

        //издательство (один ко многим)
        //[ForeignKey("PublishingHouseId")]
        [Display(Name = "Издательство")]
        public virtual PublishingHouse PublishingHouse { get; set; }

        //жанры (много ко многим)
        [Display(Name = "Жанр")]
        public virtual ICollection<Genre> Genre { get; set; }

        //авторы (много ко многим)
        [Display(Name = "Автор")]
        public virtual ICollection<Author> Author { get; set; }

        [Display(Name = "Пользователи, добавившие книгу в избранное")]
        public virtual ICollection<LibOnlineUser> User { get; set; }

        //public virtual ICollection<Review> Reviews { get; set; }

        

        [Display(Name = "Жанры")]
        public string Genres
        {
            get
            {
                if (Genre.Count > 0)
                {
                    string str = string.Empty;

                    var listOfGenres = Genre.ToList();

                    int numbersOfGenres = listOfGenres.Count();

                    for (int i = 0; i < numbersOfGenres - 1; i++)
                    {
                        str = str + listOfGenres[i].Name + ", ";
                    }

                    str = str + listOfGenres[numbersOfGenres - 1].Name;

                    return str;
                }
                else
                    return String.Empty;
            }
        }

        [Display(Name = "Авторы")]
        public string Authors
        {
            get
            {
                if (Author.Count > 0)
                {
                    string str = string.Empty;

                    var listOfAuthors = Author.ToList();

                    int numbersOfAuthors = listOfAuthors.Count();

                    for (int i = 0; i < numbersOfAuthors - 1; i++)
                    {
                        str = str + listOfAuthors[i].Name + " " + listOfAuthors[i].Surname + ", ";
                    }

                    str = str + listOfAuthors[numbersOfAuthors - 1].Name + " " + listOfAuthors[numbersOfAuthors - 1].Surname;

                    return str;
                }
                else
                    return String.Empty;
            }
        }
    }
}