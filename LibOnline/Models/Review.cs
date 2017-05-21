/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibOnline.Models
{
    public class Review
    {
        [Key]
        [Display(Name = "ID рецензии")]
        public int ReviewId { get; set; }

        [Display(Name = "Текст рецензии")]
        public string Text { get; set; }

        [Display(Name = "Пользователи, оставившие рецензии")]
        public virtual ICollection<LibOnlineUser> User { get; set; }
    }
}*/