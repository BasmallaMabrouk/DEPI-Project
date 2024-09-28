using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class DiagnosisController : Controller
    {
        // GET: Diagnosis
        private HospitalDEPIEntities db = new HospitalDEPIEntities();

        // GET: Diagnosis/Edit/5
        public ActionResult Edit(int id)
        {
            var appointment = db.Appointments
                .Include("Patient")
                .Include("Doctor")
                .FirstOrDefault(a => a.AppointmentID == id);

            if (appointment == null)
            {
                return HttpNotFound();
            }

            var model = new DiagnosisViewModel
            {
                AppointmentID = appointment.AppointmentID,
                PatientID = (int)appointment.PatientID,
                PatientName = appointment.Patient.PatientName,
                DoctorName = appointment.Doctor.DoctorName,
                AppointmentDateTime = appointment.AppointmentDateTime
            };

            return View(model);
        }

        // POST: Diagnosis/Edit/5
        [HttpPost]
        public ActionResult Edit(DiagnosisViewModel model)
        {
            if (ModelState.IsValid)
            {
                var prescription = new Prescription
                {
                    AppointmentID = model.AppointmentID,
                    DoctorID = db.Appointments.First(a => a.AppointmentID == model.AppointmentID).DoctorID,
                    PatientID = model.PatientID,
                    DrugID = model.DrugIDs.FirstOrDefault(), // Adjust as per drug selection logic
                    Dosage = model.Dosage,
                    Frequency = model.Frequency,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Notes = model.Notes
                };

                db.Prescriptions.Add(prescription);
                db.SaveChanges();
                return RedirectToAction("Success");
            }

            return View(model);
        }
    }
}