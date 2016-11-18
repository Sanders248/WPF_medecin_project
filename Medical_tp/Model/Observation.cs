using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_tp.Model
{
    public class Observation
    {
        #region variables
        private DateTime _date;
        private string _comment;
        private string[] _prescription;
        private Byte[][] _pictures;
        private int _weight;

        private int _bloodPressure;
        #endregion

        public Observation(DateTime date, string comment, string[] prescription, Byte[][] picture, int weight, int bloodPressure)
        {
            _date = date;
            _comment = comment;
            _prescription = prescription;
            _pictures = picture;
            _weight = weight;
            _bloodPressure = bloodPressure;
        }

        #region getter / setter

        public int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
        
        public int BloodPressure
        {
            get { return _bloodPressure; }
            set { _bloodPressure = value; }
        }
        
        public Byte[][] Pictures
        {
            get { return _pictures; }
            set { _pictures = value; }
        }
        
        public string[] Prescription
        {
            get { return _prescription; }
            set { _prescription = value; }
        }
        
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
        
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        #endregion
    }
}
