using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nome.EntityFrameworkModel;
using Nome.FormDataModels;

namespace Nome.Controllers
{
    public class StudentsUniController : Controller
    {
        // GET: StudentsUni
        public ActionResult Index()
        {
            List<StudentUniFormDataModel> students = null;

            using (var DbCtx = new UniversityContext())
            {
                DbCtx.StudentsUni.Select(
                    x => new StudentUniFormDataModel
                    {
                        Id = x.Id,
                        Firstname = x.Firstname,
                        Lastname = x.Lastname,
                        IdNumber = x.IdNumber
                    }
                    ).ToList();
            };


            return View();
        }

        // GET: StudentsUni/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentsUni/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentsUni/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentsUni/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentsUni/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentsUni/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentsUni/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
