using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PrescriptionsController : Controller
    {
        private HospitalDEPIEntities db = new HospitalDEPIEntities();

        // GET: Prescriptions
        public ActionResult Index()
        {
            var prescriptions = db.Prescriptions.Include(p => p.Appointment).Include(p => p.Doctor).Include(p => p.Drug).Include(p => p.Patient);
            return View(prescriptions.ToList());
        }

        // GET: Prescriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // GET: Prescriptions/Create
        public ActionResult Create()
        {
            ViewBag.AppointmentID = new SelectList(db.Appointments, "AppointmentID", "AppointmentReason");
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorName");
            ViewBag.DrugID = new SelectList(db.Drugs, "DrugID", "DrugName");
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientName");
            return View();
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrescriptionID,AppointmentID,DoctorID,PatientID,DrugID,Dosage,Frequency,StartDate,EndDate,Notes")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                db.Prescriptions.Add(prescription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AppointmentID = new SelectList(db.Appointments, "AppointmentID", "AppointmentReason", prescription.AppointmentID);
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorName", prescription.DoctorID);
            ViewBag.DrugID = new SelectList(db.Drugs, "DrugID", "DrugName", prescription.DrugID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientName", prescription.PatientID);
            return View(prescription);
        }

        // GET: Prescriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppointmentID = new SelectList(db.Appointments, "AppointmentID", "AppointmentReason", prescription.AppointmentID);
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorName", prescription.DoctorID);
            ViewBag.DrugID = new SelectList(db.Drugs, "DrugID", "DrugName", prescription.DrugID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientName", prescription.PatientID);
            return View(prescription);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrescriptionID,AppointmentID,DoctorID,PatientID,DrugID,Dosage,Frequency,StartDate,EndDate,Notes")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AppointmentID = new SelectList(db.Appointments, "AppointmentID", "AppointmentReason", prescription.AppointmentID);
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorName", prescription.DoctorID);
            ViewBag.DrugID = new SelectList(db.Drugs, "DrugID", "DrugName", prescription.DrugID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientName", prescription.PatientID);
            return View(prescription);
        }

        // GET: Prescriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prescription prescription = db.Prescriptions.Find(id);
            db.Prescriptions.Remove(prescription);
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
