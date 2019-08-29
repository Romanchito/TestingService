using System;
using System.Web.Mvc;
using System.Web.Security;
using TestingService.BLL.DTO;
using TestingService.BLL.Interfaces;
using TestingService.Models.AccountModels;

namespace TestingService.Controllers
{
    public class AccountController : Controller
    {
        private IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDTO = null;

                userDTO = userService.GetUserByEmail(model.Email);

                if (userDTO == null)
                {
                    userService.Create(new UserDTO
                    {
                        Email = model.Email,
                        Password = model.Password,
                        Name = model.Name,
                        Surname = model.Surname,
                        Patronomic = model.Patronomic,
                        RoleId = Convert.ToInt32(model.Role)
                    });

                    userDTO = userService.GetUserByEmail(model.Email);

                    if (userDTO != null)
                    {
                        if(userDTO.RoleId == 1)
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("AdminPanel", "Admin");
                        }

                        if (userDTO.RoleId == 2)
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("TeacherPanel", "Teacher");
                        }

                        if (userDTO.RoleId == 3)
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("StudentPanel", "Student");
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("Main", "Home");
                        }
                        
                    }
                }

                ModelState.AddModelError("", "Пользователь с таким адресом уже существует");
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDTO = null;
                userDTO = userService.GetUserByEmail(model.Email);

                if (userDTO != null)
                {
                    if (userDTO.Password.Equals(model.Password))
                    {
                        if (userDTO.RoleId == 1)
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("AdminPanel", "Admin");
                        }

                        if (userDTO.RoleId == 2)
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("TeacherPanel", "Teacher");
                        }

                        if (userDTO.RoleId == 3)
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("StudentPanel", "Student");
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("Main", "Home");
                        }
                    }
                }

                ModelState.AddModelError("", "Неверный логин или пароль");
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Main", "Home");
        }
    }
}