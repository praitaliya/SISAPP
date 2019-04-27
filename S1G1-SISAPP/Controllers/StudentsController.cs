using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using S1G1_SISAPP;

namespace S1G1_SISAPP.Controllers
{
    public class StudentsController : Controller
    {
        private Entities db = new Entities();

        // GET: Students
        public ActionResult Index(string sortOrder, string searchString)

        {

            ViewBag.FirstNameParm = String.IsNullOrEmpty(sortOrder) ? "first_name_desc" : "";

            ViewBag.LastNameParm = sortOrder == "last_name" ? "last_name_desc" : "last_name";

            ViewBag.PhoneSortParm = sortOrder == "phone_no" ? "phone_no_desc" : "phone_no";


            var students = from s in db.Students

                           select s;

            switch (sortOrder)

            {

                case "first_name_desc":

                    students = students.OrderByDescending(s => s.StudentFirstName);

                    break;

                case "last_name":

                    students = students.OrderBy(s => s.StudentLastName);

                    break;

                case "last_name_desc":

                    students = students.OrderByDescending(s => s.StudentLastName);

                    break;

                case "phone_no":

                    students = students.OrderBy(s => s.StudentPhone);

                    break;

                case "phone_no_desc":

                    students = students.OrderByDescending(s => s.StudentPhone);

                    break;

                default:

                    students = students.OrderBy(s => s.StudentFirstName);

                    break;

            }

            return View(students.ToList());

        }


        // GET: Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,StudentFirstName,StudentLastName,StudentEnrollmentDate,StudentPhone")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,StudentFirstName,StudentLastName,StudentEnrollmentDate,StudentPhone")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
