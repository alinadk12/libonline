using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibOnline.Models
{
    public class Author
    {
        //ID автора
        [Key]
        [Display(Name = "ID автора")]
        public int AuthorId { get; set; }

        //имя
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Name { get; set; }

        //отчество
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        //фамилия
        [Display( Name = "Фамилия")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Surname { get; set; }

        //дата рождения
        [Display(Name = "Дата рождения")]
        [Required(ErrorMessage = "Обязательное поле")]
        public Nullable<System.DateTime> Birthday { get; set; }

        //дата смерти
        [Display(Name = "Дата смерти")]
        public Nullable<System.DateTime> Obit { get; set; }

        //краткая биография
        [Display(Name = "Краткая биография")]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        //[ForeignKey("CountryId")]
        [Display(Name = "Страна")]
        public string CountryId { get; set; }

        //страна (один к многим)
        [Display(Name = "Страна")]
        public virtual Country Country { get; set; }

        //написанные книги (много ко многим)
        [Display(Name = "Книги")]
        public virtual ICollection<Book> Books { get; set; }

        [Display(Name = "Пользователи, добавившие автора в избранное")]
        public virtual ICollection<LibOnlineUser> User { get; set; }

        //полное имя
        [Display(Name = "Полное имя")]
        public string Fullname
        {
            get
            {
                if (Patronymic == null)
                    return string.Format("{0} {1}", Name, Surname);
                else
                    return string.Format("{0} {1} {2}", Name, Patronymic, Surname);
            }
        }

        //полное имя с годами
        public string FullnameWithYears
        {
            get
            {
                //если нет отчества
                if (Patronymic == null)
                    if (Birthday != null & Obit == null)
                        return string.Format("{0} {1} ({2})", Name, Surname, Birthday.Value.Year);
                    else
                        if (Birthday != null & Obit != null)
                        return string.Format("{0} {1} ({2} - {3})", Name, Surname, Birthday.Value.Year, Obit.Value.Year);
                    else
                            if (Birthday == null & Obit != null)
                        return string.Format("{0} {1} ( - {2})", Name, Surname, Obit.Value.Year);
                    else
                        return string.Format("{0} {1}", Name, Surname);
                else
                    if (Birthday != null & Obit == null)
                    return string.Format("{0} {1} {2} ({3})", Name, Patronymic, Surname, Birthday.Value.Year);
                else
                        if (Birthday != null & Obit != null)
                    return string.Format("{0} {1} {2} ({3} - {4})", Name, Patronymic, Surname, Birthday.Value.Year, Obit.Value.Year);
                else
                            if (Birthday == null & Obit != null)
                    return string.Format("{0} {1} {2} ( - {3}", Name, Patronymic, Surname, Obit.Value.Year);
                else
                    return string.Format("{0} {1} {2}", Name, Patronymic, Surname);
            }
        }

        //возраст
        [Display(Name = "Возраст")]
        public int? Age
        {
            get
            {
                if (Birthday != null & Obit == null)
                {
                    int age = DateTime.Now.Year - Birthday.Value.Year;
                    if (Birthday > DateTime.Now.AddYears(-age))
                        age--;
                    return age;
                }
                else
                    if (Birthday != null & Obit != null)
                {
                    int age = Obit.Value.Year - Birthday.Value.Year;
                    if (Birthday > Obit.Value.AddYears(-age))
                        age--;
                    return age;
                }
                else
                    return null;
            }
        }
    } 
}