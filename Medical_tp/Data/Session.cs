using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_tp.Data
{
    class Session
    {
        private static Session instance;
        private Model.User actualUser;

        private Session() {
        }

        public static Session Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Session();
                }
                return instance;
            }
        }

        public Model.User ActualUser
        {
            get { return actualUser; }
            set { actualUser = value; }
        }

    }
}
