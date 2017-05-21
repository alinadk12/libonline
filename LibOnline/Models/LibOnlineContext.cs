using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibOnline.Models
{
    public class LibOnlineContext : IdentityDbContext<LibOnlineUser>
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PublishingHouse> PublishingHouses { get; set; }

        public LibOnlineContext()
            : base ("LibOnlineContext")
        { }

        public static LibOnlineContext Create()
        {
            return new LibOnlineContext();
        }
    } 
}