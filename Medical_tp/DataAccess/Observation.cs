using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_tp.DataAccess
{
    class Observation
    {
        public static ServiceObservation.ServiceObservationClient serviceObservationCient = new ServiceObservation.ServiceObservationClient();

        public Observation()
        {
        }

        /*
        public Model.Observation getObservation()
        {
            ServiceObservation.Observation obsService = serviceObservationCient.
            Model.Observation obs = new Model.Observation();

            return obs;
        }

        public Model.Patient getPatientFromList(List<Model.Patient> listPatient, int id)
        {
            foreach (Model.Patient p in listPatient)
                if (p.Id.Equals(id))
                    return p;

            return null;
        }*/

    }
}
