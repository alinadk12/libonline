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
    public class BookController : Controller
    {
        //контекстная БД
        LibOnlineContext db = new LibOnlineContext();

        // GET: Book
        /*public ActionResult Index()
        {
            return View();
        }*/

        //Список книг
        [AllowAnonymous]
        public ActionResult List(int? page)
        {
            //получаем из БД все объекты Book
            List<Book> books = db.Books.ToList();

            //количество объектов на странице
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            //возвращается представление
            return View(books.ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        public PartialViewResult BookSearch(string search)
        {
            List<Book> books = db.Books.Where(b => b.Title.Contains(search) || b.Description.Contains(search)).Distinct().ToList();

            return PartialView("_Search", books);
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult ShowDescription(int BookId)
        {
            var book = db.Books.Find(BookId);
            return PartialView("_ShowDescription", book);
        }

        [AllowAnonymous]
        public ActionResult Index(int? BookId, FavMessageId? message)
        {
            ViewBag.StatusMessage = 
                message == FavMessageId.AddSuccess ? "Книга добавлена в избранное"
                : message == FavMessageId.DeleteSuccess ? "Книга удалена из избранного"
                : message == FavMessageId.Error ? "Произошла ошибка"
                : "";

            Book book = db.Books.Find(BookId);
            if (book == null)
            {
                return RedirectToAction("List");
            }

            ViewBag.BookInFav = false;

            if (User.Identity.IsAuthenticated)
            {
                var currentUser = db.Users.Find(User.Identity.GetUserId());

                if (book.User.Contains(currentUser))
                {
                    ViewBag.BookInFav = true;
                }
            }

            //возвращаем представление
            return View(book);
        }

        [HttpGet]
        [Authorize(Roles ="Администратор")]
        public ActionResult Edit(int? BookId)
        {
            Book book = db.Books.Find(BookId);
            if (book == null)
            {
                return RedirectToAction("List");
            }

            ViewBag.Genres = db.Genres.ToList();
            ViewBag.Authors = db.Authors.ToList();
            ViewBag.PublishingHouses = new SelectList(db.PublishingHouses, "PublishingHouseId", "Name");

            return View(book);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult Edit(Book book, int[] selectedGenres, int[] selectedAuthors)
        {
            Book newBook = db.Books.Find(book.BookId);
            newBook.Title = book.Title;
            newBook.Description = book.Description;
            newBook.NumberOfPage = book.NumberOfPage;
            newBook.YearOfPublishing = book.YearOfPublishing;
            newBook.PublishingHouse = db.PublishingHouses.Find(2);

            newBook.Genre.Clear();

            if (selectedGenres != null)
            {
                //получаем выбранные жанры
                foreach (var g in db.Genres.Where(gen => selectedGenres.Contains(gen.GenreId)))
                {
                    newBook.Genre.Add(g);
                }
            }

            newBook.Author.Clear();
            if (selectedAuthors != null)
            {
                //Получаем выбраных авторов
                foreach (var c in db.Authors.Where(co => selectedAuthors.Contains(co.AuthorId)))
                {
                    newBook.Author.Add(c);
                }
            }

            db.Entry(newBook).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("/Book?BookId=" + book.BookId);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult Add()
        {
            //Получаем все жанры, авторов и издательства
            ViewBag.Genres = db.Genres.ToList();
            ViewBag.Authors = db.Authors.ToList();
            ViewBag.PublishingHouses = new SelectList(db.PublishingHouses, "PublishingHouseID", "Name");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult Add(Book book, int[] selectedGenres, int[] selectedAuthors)
        {
            book.Genre = new List<Genre>();

            if (selectedGenres != null)
            {
                //получаем выбранные жанры
                foreach (var g in db.Genres.Where(gen => selectedGenres.Contains(gen.GenreId)))
                {
                    book.Genre.Add(g);
                }
            }

            book.Author = new List<Author>();

            if (selectedAuthors != null)
            {
                //получаем выбранные жанры
                foreach (var a in db.Authors.Where(aut => selectedAuthors.Contains(aut.AuthorId)))
                {
                    book.Author.Add(a);
                }
            }

            book.PublishingHouse = db.PublishingHouses.Find(2);
            db.Entry(book).State = EntityState.Added;
            db.SaveChanges();

            return Redirect("/Book?BookId=" + book.BookId);
        }
    

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public ActionResult AddFavorite(int bookId)
        {
            Book b = db.Books.Find(bookId);
            if (b == null)
            {
                return RedirectToAction("Index", new { bookId = b.BookId, message = FavMessageId.Error });
            }

            var currentUser = db.Users.Find(User.Identity.GetUserId());

            if (b.User.Contains(currentUser))
            {
                return RedirectToAction("Index", new { bookId = b.BookId, message = FavMessageId.Error });
            }
            else
            {
                b.User.Add(currentUser);
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { bookId = b.BookId, message = FavMessageId.AddSuccess });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public ActionResult DeleteFavorite(int bookId)
        {
            Book b = db.Books.Find(bookId);
            if (b == null)
            {
                return RedirectToAction("Index", new { bookId = b.BookId, message = FavMessageId.Error });
            }

            var currentUser = db.Users.Find(User.Identity.GetUserId());

            if (b.User.Contains(currentUser))
            {
                b.User.Remove(currentUser);
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { bookId = b.BookId, message = FavMessageId.DeleteSuccess });
            }
            else
            {
                return RedirectToAction("Index", new { bookId = b.BookId, message = FavMessageId.Error });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public ActionResult Favorites()
        {
            LibOnlineUser user = db.Users.Find(User.Identity.GetUserId());

            List<Book> bk = new List<Book>();
            if (user.FavBook != null)
            {
                bk = user.FavBook.ToList();
            }

            return View(bk);
        }

        public enum FavMessageId
        {
            AddSuccess,
            DeleteSuccess,
            Error
        }
    }
}