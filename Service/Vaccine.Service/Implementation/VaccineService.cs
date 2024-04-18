using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Vaccine.Domain.Domain;
using Vaccine.Repository.Interface;
using Vaccine.Service.Interface;

namespace Vaccine.Service.Implementation
{
    public class VaccineService : IVaccineService
    {
        private readonly IRepository<Vaccines> vaccineRepository;
        private readonly IRepository<VaccinationCenter> vaccinationCenterRepository;
        private readonly IRepository<Patient> patientRepository;    

        public VaccineService(IRepository<Vaccines> vaccineRepository,
             IRepository<VaccinationCenter> vaccinationCenterRepository,
             IRepository<Patient> patientRepository)
        {
            this.vaccineRepository = vaccineRepository;
            this.vaccinationCenterRepository = vaccinationCenterRepository;
            this.patientRepository = patientRepository;
        }

        public bool addVaccine(Vaccines vaccine)
        {

            var centerId = vaccine.VaccinationCenter;
            var patientId = vaccine.PatientId;


            var newVaccine = new Vaccines
            {
                Id = Guid.NewGuid(),
                Certificate = Guid.NewGuid(),
                Center = vaccinationCenterRepository.Get(centerId),
                DateTaken = vaccine.DateTaken,
                Manufacturer = vaccine.Manufacturer,
                PatientFor = patientRepository.Get(patientId),
                PatientId = vaccine.PatientId,
                VaccinationCenter = vaccine.VaccinationCenter
            };

        
            vaccineRepository.Insert(newVaccine);

            var center = vaccinationCenterRepository.Get(centerId);
            //var patient = patientRepository.Get(patientId);
           

            //if (center.MaxCapacity <= 0)
            //{
            //    return false;
            //}


            //if(center.Vaccines == null)
            //{
            //    center.Vaccines = new List<Vaccines>();
            //}
            //center.Vaccines.Add(newVaccine);
            center.MaxCapacity -= 1;



            //if (patient.VaccinationSchedule == null)
            //{
            //    patient.VaccinationSchedule = new List<Vaccines>();
            //}

            //patient.VaccinationSchedule.Add(newVaccine);


            //patientRepository.Update(patient);
            vaccinationCenterRepository.Update(center);
            ////vaccineRepository.Update(newVaccine);
            return true;
        }

        public List<Vaccines> getAll()
        {
            return vaccineRepository.GetAll().ToList();
        }
    }
}
