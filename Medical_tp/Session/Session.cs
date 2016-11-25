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
        private View.PrincipalWindow window;

        private Session() {
            window = new Medical_tp.View.PrincipalWindow();
            actualUser = null;
        }

        public View.PrincipalWindow GetPrincipalWindow()
        {
            return window;
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

        public string VisibilityButtons()
        {
            if (actualUser.Role.Equals("Infirmière") || actualUser.Role.Equals("Infirmiere"))
                return "Hidden";
            else
                return "Visible";
        }

        public string ReadOnlyFields()
        {
            if (actualUser.Role.Equals("Infirmière") || actualUser.Role.Equals("Infirmiere"))
                return "True";
            else
                return "False";
        }
    }
}
