using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        private HospitalDEPIEntities db = new HospitalDEPIEntities();

        // GET: Appointments
        //public ActionResult Index()
        //{
        //    var bookings = db.Bookings
        //        .Include(b => b.Patient)
        //        .Include(b => b.Doctor)
        //        .Select(b => new BookingViewModel
        //        {
        //            BookingID = b.BookingID,
        //            PatientName = b.Patient.PatientName,
        //            DoctorName = b.Doctor.DoctorName,
        //            AppointmentDateTime = b.AppointmentDateTime,
        //            AppointmentReason = b.AppointmentReason
        //        })
        //        .ToList();

        //    return View(bookings.ToList());
        //}

        // GET: Booking/Create
        public ActionResult Create()
        {
            var doctors = db.Doctors.ToList();
            var model = new BookingViewModel
            {
                AvailableDoctors = doctors
            };
            return View(model);
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appointment = new Appointment
                {
                    PatientID = model.PatientID,
                    DoctorID = model.DoctorID,
                    AppointmentDateTime = model.AppointmentDateTime,
                    AppointmentReason = model.AppointmentReason,
                    Status = "Pending"
                };

                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Success");
            }
            return View(model);
        }
    }
}