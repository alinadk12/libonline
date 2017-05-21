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
    public class AuthorController : Controller
    {
        // GET: Author
        /*public ActionResult Index()
        {
            return View();
        }*/

        //создаем контекст данных
        LibOnlineContext db = new LibOnlineContext();

        [AllowAnonymous]
        public ActionResult Index(int? AuthorId, FavMessageId? message)
        {
            ViewBag.StatusMessage =
                message == FavMessageId.AddSuccess ? "Автор добавлен в избранное"
                : message == FavMessageId.DeleteSuccess ? "Автор удален из избранного"
                : message == FavMessageId.Error ? "Произошла ошибка"
                : "";

            Author author = db.Authors.Find(AuthorId);
            if (author == null)
            {
                return RedirectToAction("List");
            }

            ViewBag.AuthorInFav = false;

            if (User.Identity.IsAuthenticated)
            {
                var currentUser = db.Users.Find(User.Identity.GetUserId());

                if (author.User.Contains(currentUser))
                {
                    ViewBag.AuthorInFav = true;
                }
            }

            //возвращается представление
            return View(author);
        }

        //список книг
        [AllowAnonymous]
        public ActionResult List(int? page)
        {
            //получаем из БД все объекты Book
            List<Author> authors = db.Authors.ToList();

            //количество объектов на странице
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            //возвращаем представление
            return View(authors.ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        public PartialViewResult AuthorSearch(int? page, string search)
        {
            List<Author> authors = db.Authors.Where(b => b.Name.Contains(search) || b.Surname.Contains(search) || b.Patronymic.Contains(search)).Distinct().ToList();

            return PartialView("_Search", authors);
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult ShowBiography(int AuthorId)
        {
            var author = db.Authors.Find(AuthorId);
            return PartialView("_ShowBiography", author);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult Add()
        {
            ViewBag.Countries = new SelectList(db.Countries, "CountryId", "Name");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult Add(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges();

            return Redirect("/Author?AuthorId=" + author.AuthorId);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult Edit(int? AuthorId)
        {
            Author author = db.Authors.Find(AuthorId);
            if (author == null)
            {
                return RedirectToAction("List");
            }

            ViewBag.Countries = new SelectList(db.Countries, "CountryId", "Name");

            return View(author);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult Edit(Author author)
        {
            db.Entry(author).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("/Author?AuthorId=" + author.AuthorId);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult AddAuthor(int? AuthorId)
        {
            ViewBag.Books = new SelectList(db.Books, "BookId", "TitleWithYear");

            Author author = db.Authors.Find(AuthorId);
            if (author == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.Author = author;
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult AddBook(int AuthorId)
        {
            ViewBag.Books = new SelectList(db.Books, "BookId", "TitleWithYear");

            Author author = db.Authors.Find(AuthorId);
            if (author == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.Author = author;
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public ActionResult AddFavorite(int AuthorId)
        {
            Author a = db.Authors.Find(AuthorId);

            if (a == null)
            {
                return RedirectToAction("Index", new { AuthorId = a.AuthorId, message = FavMessageId.Error });
            }

            var currenUser = db.Users.Find(User.Identity.GetUserId());

            if (a.User.Contains(currenUser))
            {
                return RedirectToAction("Index", new { AuthorId = a.AuthorId, message = FavMessageId.Error });
            }
            else
            {
                a.User.Add(currenUser);
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { AuthorId = a.AuthorId, message = FavMessageId.AddSuccess });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public ActionResult DeleteFavorite(int AuthorId)
        {
            Author a = db.Authors.Find(AuthorId);
            if (a == null)
            {
                return RedirectToAction("Index", new { AuthorId = a.AuthorId, message = FavMessageId.Error });
            }

            var currentUser = db.Users.Find(User.Identity.GetUserId());

            if (a.User.Contains(currentUser))
            {
                a.User.Remove(currentUser);
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { AuthorId = a.AuthorId, message = FavMessageId.DeleteSuccess });
            }
            else
            {
                return RedirectToAction("Index", new { AuthorId = a.AuthorId, message = FavMessageId.Error });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public ActionResult Favorites()
        {
            LibOnlineUser user = db.Users.Find(User.Identity.GetUserId());

            List<Author> fa = new List<Author>();
            if (user.FavBook != null)
            {
                fa = user.FavAuthor.ToList();
            }

            return View(fa);
        }

        public enum FavMessageId
        {
            AddSuccess,
            DeleteSuccess,
            Error
        }
    }
}