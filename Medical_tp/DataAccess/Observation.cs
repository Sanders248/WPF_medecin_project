using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_tp.DataAccess
{
    class Observation
    {
        private List<Model.Observation> _listObservation;
        public static ServiceObservation.ServiceObservationClient serviceObservation = new ServiceObservation.ServiceObservationClient();

        public Observation()
        {
            _listObservation = new List<Model.Observation>();
         //   LoadObservations();
        }

       /* private void LoadObservations()
        {
            try
            {
                foreach (ServiceObservation.Observation o in serviceObservation. GetListObservation())
                {
                    List<Model.Observation> obsList = new List<Model.Observation>();

                    try
                    {
                        foreach (ServicePatient.Observation o in p.Observations)
                            obsList.Add(new Model.Observation(o.Date, o.Comment, o.Prescription, o.Pictures, o.Weight, o.BloodPressure));
                    }
                    catch { }

                    _listPatient.Add(new Model.Patient(p.Name, p.Firstname, p.Birthday, p.Id, obsList));
                }
            }
            catch { }
        }*/


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
