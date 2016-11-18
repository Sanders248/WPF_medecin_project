using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_tp.DataAccess
{
    class Patient
    {
        public static ServicePatient.ServicePatientClient servicePatient = new ServicePatient.ServicePatientClient();

        public Patient()
        {
        }

        public Model.Patient getPatientFromList(List<Model.Patient> listPatient, int id)
        {
            foreach (Model.Patient p in listPatient)
                if (p.Id.Equals(id))
                    return p;

            return null;
        }

    }
}
