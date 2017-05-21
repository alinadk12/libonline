using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using LibOnline.Models;
using System.Data.Entity;


namespace LibOnline.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        /* public ActionResult Index()
         {
             return View();
         }*/

        //создание контекста данных
        LibOnlineContext db = new LibOnlineContext();

        // GET: /Admin/
        [Authorize(Roles = "Администратор")]
        public ActionResult Index()
        {
            return RedirectToAction("UserList");
        }

        [Authorize(Roles = "Администратор")]
        public ActionResult UserList(int? page, ChangeRole? message)
        {
            ViewBag.StatusMessage =
            message == ChangeRole.AddSuccess ? "Пользователь назначен администратором."
            : message == ChangeRole.UserIsInRole ? "Пользователь уже является администратором."
            : message == ChangeRole.DelSuccess ? "Пользователь удален из администраторов."
            : message == ChangeRole.UserIsntInRole ? "Пользователь не является администратором."
            : message == ChangeRole.Error ? "Произошла ошибка."

            : "";

            List<LibOnlineUser> users = db.Users.ToList();

            //Количесство объектов на странице
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            //Возвращаем представление
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Администратор")]
        [HttpGet]
        public ActionResult AddAdmin(string userId)
        {
            List<LibOnlineUser> user = db.Users.ToList();
            if (user == null)
            {
                return RedirectToAction("UserList", new { message = ChangeRole.Error });
            }

            var userManager = new UserManager<LibOnlineUser>(new UserStore<LibOnlineUser>(db));

            if (userManager.IsInRole(userId, "Администратор"))
            {
                return RedirectToAction("UserList", new { message = ChangeRole.UserIsInRole });
            }
            else
            {
                userManager.AddToRole(userId, "Администратор");
                return RedirectToAction("UserList", new { message = ChangeRole.AddSuccess });
            }
        }

        [Authorize(Roles = "Администратор")]
        [HttpGet]
        public ActionResult DeleteAdmin(string userId)
        {
            List<LibOnlineUser> user = db.Users.ToList();
            if (user == null)
            {
                return RedirectToAction("UserList", new { message = ChangeRole.Error });
            }

            var userManager = new UserManager<LibOnlineUser>(new UserStore<LibOnlineUser>(db));

            if (userManager.IsInRole(userId, "Администратор"))
            {
                userManager.RemoveFromRole(userId, "Администратор");
                userManager.AddToRole(userId, "Пользователь");
                return RedirectToAction("UserList", new { message = ChangeRole.DelSuccess });
            }
            else
            {
                return RedirectToAction("UserList", new { message = ChangeRole.UserIsntInRole });
            }
        }

        public enum ChangeRole
        {
            UserIsInRole,
            AddSuccess,
            Error,
            UserIsntInRole,
            DelSuccess

        }
    }
}