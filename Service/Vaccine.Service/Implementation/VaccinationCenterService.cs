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
    public class VaccinationCenterService : IVaccinationCenterService
    {

        private readonly IRepository<VaccinationCenter> vaccinationCenterRepository;

        public VaccinationCenterService(IRepository<VaccinationCenter> vaccinationCenterRepository)
        {
            this.vaccinationCenterRepository = vaccinationCenterRepository;
        }

        public void CreateNewVaccinationCenter(VaccinationCenter v)
        {
            vaccinationCenterRepository.Insert(v);
        }

        public void DeleteVaccinationCenter(Guid id)
        {
            vaccinationCenterRepository.Delete(vaccinationCenterRepository.Get(id));    
        }

        public List<VaccinationCenter> GetAllVaccinationCenters()
        {
            return vaccinationCenterRepository.GetAll().ToList();
        }

        public VaccinationCenter GetDetailsForVaccinationCenter(Guid? id)
        {
            return vaccinationCenterRepository.Get(id);
        }

        public void UpdeteExistingVaccinationCenter(VaccinationCenter v)
        {
            vaccinationCenterRepository.Update(v);
        }
    }
}
