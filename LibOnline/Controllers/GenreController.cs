using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LibOnline.Models;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace LibOnline.Controllers
{
    public class GenreController : Controller
    {
        // GET: Genre
        /*public ActionResult Index()
        {
            return View();
        }*/

        LibOnlineContext db = new LibOnlineContext();

        //Список жанров
        [AllowAnonymous]
        public ActionResult List()
        {
            //получаем из БД все объекты Book
            List<Genre> genres = db.Genres.ToList();

            //возвращается представление
            return View(genres);
        }

        [AllowAnonymous]
        public ActionResult Index(int? GenreId)
        {
            Genre genre = db.Genres.Find(GenreId);
            if (genre == null)
            {
                return RedirectToAction("List");
            }

            return View(genre);
        }

        [AllowAnonymous]
        public PartialViewResult ShowDescription(int GenreId)
        {
            var genre = db.Genres.Find(GenreId);
            return PartialView("_ShowDescription", genre);
        }
    }
}