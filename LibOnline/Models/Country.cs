using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibOnline.Models
{
    public class Country
    {
        //ID страны
        [Key]
        [Display(Name = "ID страны")]
        public string CountryId { get; set; }

        //название
        [Display(Name = "Название")]
        public string Name { get; set; }

        /*public static implicit operator Country(List<Country> v)
        {
            throw new NotImplementedException();
        }*/
    }
}