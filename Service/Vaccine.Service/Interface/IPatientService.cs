using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccine.Domain.Domain;

namespace Vaccine.Service.Interface
{
    public interface IPatientService
    {
        List<Patient> GetAllPatients();
        Patient GetDetailsForPatient(Guid? id);
        void CreateNewPatient(Patient p);
        void UpdeteExistingPatient(Patient p);
        void DeletePatient(Guid id);
    }

}
