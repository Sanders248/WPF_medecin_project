using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_tp.Model
{
    class Live
    {
        private double _hearthData;
        private double _tempData;

        public Live(double heartVaue, double tempValue)
        {
            _hearthData = heartVaue;
            _tempData = tempValue;
        }

        public Live()
        {
            _hearthData = 0;
            _tempData = 0;
        }

        public double HearthData
        {
            get { return _hearthData; }
            set { _hearthData = value; }
        }

        public double TempData
        {
            get { return _tempData; }
            set { _tempData = value; }
        }
    }
}
