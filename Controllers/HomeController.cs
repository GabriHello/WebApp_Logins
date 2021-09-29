using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nome.FormDataModels;

namespace Nome.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
        
        public ActionResult Roulette()
        {
            ViewBag.Message = "Roulette page";

            return View(new EntryFormDataModel {
                PlayerName = "Qui ci va il tuo nome", 
                Quantity = 100 });
        }

        [HttpPost]
        public ActionResult Roulette(EntryFormDataModel formData)
        {
            
            if (formData.Quantity > 1000 || formData.Quantity <= 0)
            {
                ViewBag.Errore = "Quantita' non valida";
                return View();
            }
            else
            {
                ViewBag.Message = $"The Game Begins... you have {formData.Quantity}$";
                return View("RouletteEntryResult");
            }


        }
            
    }
}