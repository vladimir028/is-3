using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vaccine.Domain.Domain;
using Vaccine.Repository;
using Vaccine.Repository.Interface;
using Vaccine.Service.Interface;

namespace Vaccine.Web.Controllers
{
    public class VaccinesController : Controller
    {
        private readonly IVaccineService vaccineService;
        private readonly IPatientService patientService;
        private readonly IVaccinationCenterService vaccinationCenterService;

        public VaccinesController(IVaccineService vaccineService, IPatientService patientService, IVaccinationCenterService vaccinationCenterService)
        {
            this.vaccineService = vaccineService;
            this.patientService = patientService;
            this.vaccinationCenterService = vaccinationCenterService;
        }



        // GET: Vaccines
        public IActionResult Index()
        {
           
            return View(vaccineService.getAll());
        }

        // GET: Vaccines/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var vaccines = await _context.Vaccines
        //        .Include(v => v.PatientFor)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (vaccines == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(vaccines);
        //}

        // GET: Vaccines/Create/5
        // id -> na vaccineCentar
        public IActionResult Create(Guid id)
        {

            if(vaccinationCenterService.GetDetailsForVaccinationCenter(id).MaxCapacity <=0) {
                return RedirectToAction("ErrorView");
            }

            ViewData["PatientId"] = new SelectList(patientService.GetAllPatients(), "Id", "FirstName");
            ViewBag.VaccinationCenterId = id;

            List<String> manufacturers = generateManufacturers();
           
            ViewData["Manufacturer"] = new SelectList(manufacturers);
            Vaccines vaccine = new Vaccines();
            vaccine.VaccinationCenter = id;
            var a = vaccine;
            return View(vaccine);
        }

        private List<string> generateManufacturers()
        {
            List<String> manufacturers = new List<String>();
            manufacturers.Add("Astra Zeneka");
            manufacturers.Add("Phizer");
            manufacturers.Add("Makedonska Vakcina");
            return manufacturers;
        }

        // POST: Vaccines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Manufacturer,DateTaken,PatientId,Id,VaccinationCenter")] Vaccines vaccine)
        {

            var centerId = vaccine.VaccinationCenter;
            var res = vaccineService.addVaccine(vaccine);
            
            return res == true ? RedirectToAction("Details", "VaccinationCenters", new { id = centerId }) : RedirectToAction("ErrorView");

        }

        public IActionResult ErrorView()
        {
            return View();
        }

        // GET: Vaccines/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var vaccines = await _context.Vaccines.FindAsync(id);
        //    if (vaccines == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", vaccines.PatientId);
        //    return View(vaccines);
        //}

        //// POST: Vaccines/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Manufacturer,Certificate,DateTaken,PatientId,VaccinationCenter,Id")] Vaccines vaccines)
        //{
        //    if (id != vaccines.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(vaccines);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!VaccinesExists(vaccines.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", vaccines.PatientId);
        //    return View(vaccines);
        //}

        //// GET: Vaccines/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var vaccines = await _context.Vaccines
        //        .Include(v => v.PatientFor)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (vaccines == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(vaccines);
        //}

        //// POST: Vaccines/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var vaccines = await _context.Vaccines.FindAsync(id);
        //    if (vaccines != null)
        //    {
        //        _context.Vaccines.Remove(vaccines);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool VaccinesExists(Guid id)
        //{
        //    return _context.Vaccines.Any(e => e.Id == id);
        //}
    }
}
