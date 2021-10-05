using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Nome.FormDataModels;
using Nome.Services;
using System.Configuration;
using Nome.Models;
using System.Web.UI;

namespace Nome.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationService service;

        public AuthenticationController()
        {
            this.service = new AuthenticationService();

        }

        // GET: Authentication
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpFormDataModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!service.CheckUserEmail(formData.Email)) //significa che il conteggio delle mail è diverso da zero,quindi gia presente
            {
                ModelState.AddModelError("Email", "Email already exists in the system.");
                return View();
            }
            var user = service.CreateUser(formData);
            if (user == null)
            {
                ModelState.AddModelError("", "Cannot register, DB problems.");

                return View();
            }
            Session["user"] = user;

            return View("SignUpSuccess");


        }

        public ActionResult Login()
        {
            var user = Session["user"];
            if (user != null)
                return RedirectToAction("Index", "Home");

            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginFormDataModel formData)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = service.GetUserByEmail(formData);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View();
            }

            Session["user"] = user;


            return RedirectToAction("Index", "Home");

        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Profile()
        {
            var user = Session["user"] as UserModel;
            if (user == null)
            {
               
                return RedirectToAction("Login");
            }
            var profile = service.GetProfileById(user.Id);
           
            return View(profile);
        }

        public ActionResult EditProfile()
        {
            var user = Session["user"] as UserModel;
            var profile = service.GetProfileById(user.Id);
            var profileForm = new UserProfileFormDataModel
            {
                Firstname = profile.Firstname,
                Lastname = profile.Lastname,
                BirthDate = profile.BirthDate,
                Citizenship = profile.Citizenship
            };
            return View(profileForm);
        }

        [HttpPost]
        public ActionResult EditProfile(UserProfileFormDataModel formData)
        {
            var user = Session["user"] as UserModel;
            service.UpdateProfileInfo(formData, user.Id);
            return RedirectToAction("Profile");
        }

    }
}