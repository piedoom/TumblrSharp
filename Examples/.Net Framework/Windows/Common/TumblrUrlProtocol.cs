using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class TumblrUrlProtocol
    {
        #region private variables
              
        private string applicationPath;
        private string parameterChar;
        private string name;

        #endregion

        #region private methode

        private void CleanRegistration(string name, string applicationPath, string parameterChar)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("Software\\Classes\\" + name + "shell\\open\\command", true);
            rk.DeleteValue("");
            rk.Close();

            rk = Registry.CurrentUser.OpenSubKey("Software\\Classes\\" + name, true);
            rk.DeleteValue("URL Protocol");
            rk.DeleteValue("");
            rk.Close();

            rk = Registry.CurrentUser.OpenSubKey("Software\\Classes\\", true);
            rk.DeleteSubKeyTree(name);
            rk.Close();

        }

        private void Registration(string name, string applicationPath, string parameterChar)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("Software\\Classes\\", true);

            rk = rk.CreateSubKey(name, true);
            rk.SetValue("URL Protocol", "");
            rk.SetValue("", "URL:" + name + " " + "Protocol");

            rk = rk.CreateSubKey("shell\\open\\command");
            rk.SetValue("", "\"" + applicationPath + "\"" + " -" + parameterChar + "=\"%1\"");

            rk.Close();
        }

        #endregion

        public TumblrUrlProtocol(string protocolName, string applicationPath, string parameterChar = "i")
        {
            name = protocolName;
            this.applicationPath = applicationPath;
            this.parameterChar = parameterChar;

            if (IsRegistered() == false)
            {
                Registration(Name, ApplicationPath, ParameterChar);
            }
        }

        #region property
        public string ApplicationPath
        {
            get
            {
                return applicationPath;
            }
            set
            {
                if (applicationPath != value && value != string.Empty)
                {
                    CleanRegistration(Name, applicationPath, ParameterChar);

                    applicationPath = value;

                    Registration(Name, applicationPath, ParameterChar);
                }
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value && value != string.Empty)
                {
                    CleanRegistration(name, ApplicationPath, ParameterChar);

                    name = value;

                    Registration(name, ApplicationPath, ParameterChar);
                }
            }
        }

        public string ParameterChar
        {
            get
            {
                return parameterChar;
            }
            set
            {
                if (parameterChar != value && value != string.Empty)
                {
                    CleanRegistration(Name, ApplicationPath, parameterChar);

                    parameterChar = value;

                    Registration(Name, ApplicationPath, parameterChar);
                }

            }
        }
        #endregion

        #region methode
        public bool IsRegistered()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("Software\\Classes\\" + Name);

            if (rk == null)
            {
                return false;
            }
            else
            {
                string protocol = (string)rk.GetValue("");
                if (protocol != "URL:" + Name + " " + "Protocol")
                {
                    return false;
                }
                else
                {
                    rk = rk.OpenSubKey("shell\\open\\command");

                    string command = (string)rk.GetValue("");

                    if (command != "\"" + ApplicationPath + "\"" + " -" + ParameterChar + "=\"%1\"")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        #endregion
    }
}
