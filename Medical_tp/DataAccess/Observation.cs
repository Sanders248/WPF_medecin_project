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

        public static void AddObservation(Model.Patient patient, Model.Observation obs)
        {
            ServiceObservation.Observation servObs = new ServiceObservation.Observation();
            servObs.BloodPressure = obs.BloodPressure;
            servObs.Comment = obs.Comment;
            servObs.Date = obs.Date;
            servObs.Pictures = obs.Pictures;
            servObs.Prescription = Tools.stringToTabString(obs.Prescription);
            servObs.Weight = obs.Weight;

            serviceObservation.AddObservation(patient.Id, servObs);
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
