using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibOnline.Models
{
    public class PublishingHouse
    {
        //ID издательства
        [Key]
        [Display(Name = "Издательство")]
        public int PublishingHouseId { get; set; }

        //название
        [Display(Name = "Издательство")]
        public string Name { get; set; }

        /*public static implicit operator PublishingHouse(List<PublishingHouse> v)
        {
            throw new NotImplementedException();
        }*/
    }
}