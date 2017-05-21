using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibOnline.Models
{
    public class LibOnlineUser : IdentityUser
    {
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Обязательное поле!")]
        public string Surname { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Обязательное поле!")]
        public string Name { get; set; }

        [Display(Name = "Избранные книги")]
        public virtual ICollection<Book> FavBook { get; set; }

        [Display(Name = "Избранные авторы")]
        public virtual ICollection<Author> FavAuthor { get; set; }

        /*[Display(Name = "ID рецензии")]
        //[ForeignKey("ReviewId")]
        public int ReviewId { get; set; }

        [Display(Name = "Рецензия")]
        public virtual Review Review { get; set; }*/



        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }

        public bool isAdmin
        {
            get
            {
                LibOnlineContext db = new LibOnlineContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                if (Roles.Count > 0)
                {
                    foreach (var r in Roles)
                    {
                        if (r.RoleId == roleManager.FindByName("Администратор").Id)
                            return true;
                    }
                }
                return false;
            }
        }

        public bool ChangeName(string newName, string newSurname)
        {
            if ((newName != String.Empty) & (newSurname != String.Empty))
            {
                Name = newName;
                Surname = newSurname;
                return true;
            }
            else
                return false;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<LibOnlineUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }
}