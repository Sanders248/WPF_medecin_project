using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Medical_tp.DataAccess
{
    class User
    {

        public static ServiceUser.ServiceUserClient serviceClient = new ServiceUser.ServiceUserClient();
       // private ServiceUser.User _User = null; // warning be sure that is the correct way
        private bool connected;

        /// <summary>
        /// construteur
        /// </summary>
        public User()
        {
            connected = false;
        }

        public bool connexion(string login, string pwd)
        {
            //if ((connected = serviceClient.Connect(login, pwd)) == true)
            //     _User = serviceClient.GetUser(login);

            if ((connected = serviceClient.Connect(login, pwd)) == true)
            {
               /* OperationContext operationContext = OperationContext.Current;
                InstanceContext instanceContext = operationContext.InstanceContext;
                ServiceLive.ServiceLiveClient live = new ServiceLive.ServiceLiveClient(instanceContext);

                live.Subscribe();*/

                connected = true;
            }
            return connected;
        }

    }
}
