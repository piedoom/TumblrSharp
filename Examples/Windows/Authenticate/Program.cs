using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Interfaces;

namespace Authenticate
{

    class Program : IArgsReceiver
    {
        /// <summary>
        /// Named Pipe Binding
        /// </summary>
        private static  NetNamedPipeBinding _binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.Transport);
        /// <summary>
        /// IPC Adresse
        /// </summary>
        private static string _pipeName = @"net.pipe://localhost/{B19E75EF-1F5E-4e09-A0FF-5650879AF497}";

        /// <summary>
        /// mainform
        /// </summary>
        private static FrAuthenticate frMain;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Mutex for one instance there app
            using (Mutex mu = new Mutex(true, "99E0A5F9-5A8F-4BE1-BBBD-EAB2A575EED4", out bool firstInstance))
            {
                if (firstInstance)
                {
                    // Create Host for Service
                    ServiceHost host = new ServiceHost(typeof(Program));

                    // Create a named pipe endpoint
                    host.AddServiceEndpoint(typeof(IArgsReceiver), _binding, _pipeName);
                    
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    frMain = new FrAuthenticate(host);

                    Application.Run(frMain);
                }
                else
                {                    
                    try
                    {
                        // Create a channel to connect serviceendpoint
                        IArgsReceiver proxy = ChannelFactory<IArgsReceiver>.CreateChannel(_binding, new EndpointAddress(_pipeName));

                        // Transfer of data and channel disposal
                        using (proxy as IDisposable)
                        {
                            proxy.PassArgs(args);
                        }
                    }
                    catch (CommunicationException exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }
            }
        }       


        #region IArgsReceiver Member

        public void PassArgs(string[] args)
        {
            // analyzing the data passed 
            string arg = args[0]; 
            arg = arg.Replace("-i=", "");

            // passing to mainForm
            frMain.SetAccessUrl(arg);

            // bring the main form to the foreground
            frMain.Activate();
             
        }

        #endregion
    }
}
