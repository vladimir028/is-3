using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccine.Domain.Domain;
using Vaccine.Repository.Interface;
using Vaccine.Service.Interface;

namespace Vaccine.Service.Implementation
{
    public class PatientService : IPatientService
    {

        private readonly IRepository<Patient> patientRepository;

        public PatientService(IRepository<Patient> patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public void CreateNewPatient(Patient p)
        {
            patientRepository.Insert(p);
        }

        public void DeletePatient(Guid id)
        {
            patientRepository.Delete(patientRepository.Get(id));
        }

        public List<Patient> GetAllPatients()
        {
            return patientRepository.GetAll().ToList();
        }

        public Patient GetDetailsForPatient(Guid? id)
        {
            return patientRepository.Get(id);
        }

        public void UpdeteExistingPatient(Patient p)
        {
            patientRepository.Update(p);
        }
    }

}
