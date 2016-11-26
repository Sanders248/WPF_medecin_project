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


        public static ServiceObservation.Observation[] getServiceObs(List<Model.Observation> obs)
        {
            List<ServiceObservation.Observation> listServObs = new List<ServiceObservation.Observation>();

            foreach (Model.Observation oTmp in obs)
            {
                ServiceObservation.Observation servObs = new ServiceObservation.Observation();
                servObs.BloodPressure = oTmp.BloodPressure;
                servObs.Comment = oTmp.Comment;
                servObs.Date = oTmp.Date;
                servObs.Pictures = oTmp.Pictures;
                servObs.Prescription = Tools.stringToTabString(oTmp.Prescription);
                servObs.Weight = oTmp.Weight;

                listServObs.Add(servObs);
            }

            return listServObs.ToArray();
        }
        
    }
}
