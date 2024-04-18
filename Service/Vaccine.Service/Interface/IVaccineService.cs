using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccine.Domain.Domain;

namespace Vaccine.Service.Interface
{
    public interface IVaccineService
    {
        bool addVaccine(Vaccines v);
        List<Vaccines> getAll();
    }
}
