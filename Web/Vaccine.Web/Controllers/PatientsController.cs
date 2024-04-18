using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaccine.Domain.Domain;
using Vaccine.Service.Interface;

namespace Vaccine.Web.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientService patientService;
        private readonly IVaccineService vaccineService;

        public PatientsController(IPatientService patientService, IVaccineService vaccineService)
        {
            this.patientService = patientService;
            this.vaccineService = vaccineService;
        }

        // GET: Patients
        public IActionResult Index()
        {
            return View(patientService.GetAllPatients());
        }

        // GET: Patients/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = patientService.GetDetailsForPatient(id);
            List<Vaccines> scheduledAppointments = vaccineService.getAll().Where(i => i.PatientId.Equals(id)).ToList();
            ViewBag.Appointments = scheduledAppointments;
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Embg,FirstName,LastName,PhoneNumber,Email,Id")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                patientService.CreateNewPatient(patient);
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = patientService.GetDetailsForPatient(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Embg,FirstName,LastName,PhoneNumber,Email,Id")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    patientService.UpdeteExistingPatient(patient);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = patientService.GetDetailsForPatient(id);
            if (patient != null)
            {
                patientService.DeletePatient(id);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var patient = patientService.GetDetailsForPatient(id);
            if (patient != null)
            {
                patientService.DeletePatient(id);
            }


            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(Guid id)
        {
            return patientService.GetDetailsForPatient(id) != null;
        }
    }

}
