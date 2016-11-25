using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_tp.DataAccess
{
    class Live : ServiceLive.IServiceLiveCallback
    {
        private Model.Live _liveObs;

        public delegate void delegateUpdateLive();
        private delegateUpdateLive _delegateLive;

        public Live(delegateUpdateLive delegateLive)
        {
            _liveObs = new Model.Live();
            _delegateLive = delegateLive;
        }

        public void PushDataHeart(double requestData)
        {
            _liveObs.HearthData = requestData;
            _delegateLive.Invoke();
        }

        public void PushDataTemp(double requestData)
        {
            _liveObs.TempData = requestData;
            _delegateLive.Invoke();
        }

        public Model.Live LiveObs
        {
            get { return _liveObs; }
            set { _liveObs = value; }
        }
    }
}
