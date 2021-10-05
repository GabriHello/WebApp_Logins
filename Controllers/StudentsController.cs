using Nome.Models;
using Nome.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Nome.Models.StudentModel;

namespace Nome.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentsService service;
        public StudentsController()
        {
            this.service = new StudentsService();
        }
        // GET: Students
        public ActionResult StudentsList()
        {
            var studentsView = new StudentModel();
            studentsView.StudentsList = service.getStudentsList();
            return View(studentsView);
        }

        [HttpPost]
        public ActionResult InsertStudent(StudentModel model)
        {
            int counter = service.InsertAStudent(model.StudentToInsert);
            if (!ModelState.IsValid || counter == 0)
                return View();

            return RedirectToAction("StudentsList");
        }

        public ActionResult DeleteStudent(Student model)
        {
            if (!ModelState.IsValid)
                return View();
            service.DeleteAStudent(model);
            return RedirectToAction("StudentsList");
        }
    }
}