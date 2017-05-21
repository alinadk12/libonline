using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibOnline.Models;

namespace LibOnline.Controllers
{
    public class HomeController : Controller
    {
        //создается контекст данных
        LibOnlineContext db = new LibOnlineContext();

        public ActionResult Index()
        {
            //получает из БД все объекты Book
            IEnumerable<Book> books = db.Books;
            //передаются все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books.OrderByDescending(b => b.BookId).Take(3).ToList();

            //получает из БД все объекты Author
            IEnumerable<Author> authors = db.Authors;
            //передаются все объекты в динамическое свойство Authors в ViewBag
            ViewBag.Authors = authors;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}