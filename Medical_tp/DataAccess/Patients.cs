using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_tp.DataAccess
{
    class Patients
    {

        private List<Model.Patient> _listPatient;
        public static ServicePatient.ServicePatientClient servicePatient = new ServicePatient.ServicePatientClient();

        public Patients()
        {
            _listPatient = new List<Model.Patient>();
            LoadPatients();
        }

        private void LoadPatients()
        {
            try
            {
                foreach (ServicePatient.Patient p in servicePatient.GetListPatient())
                {
                    List<Model.Observation> obsList = new List<Model.Observation>();

                    try
                    {
                        foreach (ServicePatient.Observation o in p.Observations)
                            obsList.Add(new Model.Observation(o.Date, o.Comment, Tools.tabStringToString(o.Prescription), o.Pictures, o.Weight, o.BloodPressure, true));
                    }
                    catch { }

                    _listPatient.Add(new Model.Patient(p.Name, p.Firstname, p.Birthday, p.Id, obsList));
                }
            }
            catch { }
        }

        public List<Model.Patient> getPatients()
        {
            return _listPatient;
        }

        public Model.Patient addNewPatient()
        {
            Model.Patient p = new Model.Patient(_listPatient[_listPatient.Count - 1].Id + 1);

            _listPatient.Add(p);

            ServicePatient.Patient servPatient = new ServicePatient.Patient();
            servPatient.Firstname = p.Firstname;
            servPatient.Name = p.Name;
            servPatient.Birthday = p.Birthday;
            servPatient.Id = p.Id;
            servPatient.Observations = getServiceObs(p.Observations);

            servicePatient.AddPatient(servPatient);

            return p;
        }

        public static ServicePatient.Observation[] getServiceObs(List<Model.Observation> obs)
        {
            List<ServicePatient.Observation> listServObs = new List<ServicePatient.Observation>();

            if (obs == null)
                return listServObs.ToArray();


            foreach (Model.Observation oTmp in obs)
            {
                ServicePatient.Observation servObs = new ServicePatient.Observation();
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

        public void removePatient(Model.Patient patient)
        {
            try
            {
                servicePatient.DeletePatient(patient.Id);
                _listPatient.Remove(patient);
            }
            catch
            { }
        }

        public void updatePatient(Model.Patient patient)
        {
            ServicePatient.Patient ptmp = servicePatient.GetPatient(patient.Id);

            servicePatient.DeletePatient(patient.Id);

            ServicePatient.Patient servPatient = new ServicePatient.Patient();
            servPatient.Id = patient.Id;
            servPatient.Name = patient.Name;
            servPatient.Firstname = patient.Firstname;
            servPatient.Observations = getServiceObs(patient.Observations);
            servPatient.Birthday = patient.Birthday;

            servicePatient.AddPatient(servPatient);
        }

    }
}
