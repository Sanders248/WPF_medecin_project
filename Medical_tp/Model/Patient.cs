using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_tp.Model
{
    public class Patient
    {
        #region variables
        private string _name;
        private string _firstname;
        private DateTime _birthday;
        private int _id;
        private List<Observation> _observations;
        #endregion

        public Patient(string name, string firstname, DateTime birthday, int id, List<Observation> observations)
        {
            _name = name;
            _firstname = firstname;
            _birthday = birthday;
            _id = id;
            _observations = observations;
        }

        #region getter / setter

        public List<Observation> Observations
        {
            get { return _observations; }
            set { _observations = value; }
        }
        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        public string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }
        
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        #endregion
    }
}
