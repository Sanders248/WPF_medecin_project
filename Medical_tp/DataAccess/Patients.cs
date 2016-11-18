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
            foreach (ServicePatient.Patient p in servicePatient.GetListPatient())
            {
                List<Model.Observation> obsList = new List<Model.Observation>();

                foreach (ServicePatient.Observation o in p.Observations)
                    obsList.Add(new Model.Observation(o.Date, o.Comment, o.Prescription, o.Pictures, o.Weight, o.BloodPressure));

                _listPatient.Add(new Model.Patient(p.Name, p.Firstname, p.Birthday, p.Id, obsList));
            }
        }

        public List<Model.Patient> getPatients()
        {
            return _listPatient;
        }

    }
}
