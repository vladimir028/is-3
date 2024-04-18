using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccine.Domain.Domain;

namespace Vaccine.Service.Interface
{
    public interface IVaccinationCenterService
    {
        List<VaccinationCenter> GetAllVaccinationCenters();
        VaccinationCenter GetDetailsForVaccinationCenter(Guid? id);
        void CreateNewVaccinationCenter(VaccinationCenter v);
        void UpdeteExistingVaccinationCenter(VaccinationCenter v);
        void DeleteVaccinationCenter(Guid id);
    }
}
