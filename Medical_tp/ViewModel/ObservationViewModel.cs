using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_tp.Model;

namespace Medical_tp.ViewModel
{
    class ObservationViewModel : DockPanelViewModel
    {
        private Patient _current_patient;

        public Patient Current_patient
        {
            get { return _current_patient; }
            set { _current_patient = value; }
        }
        public ObservationViewModel(Patient patient)
        {
            _current_patient = patient;
        }
    }
}
