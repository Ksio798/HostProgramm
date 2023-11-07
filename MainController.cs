using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Windows.Threading;

namespace Host
{
    internal class MainController
    {
        public MainWindow window;       
        TcpListener server = null;
        Int32 port = 8888;

        public MainController(MainWindow w)
        {          
            window = w;
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            SetHostName();
            Task task = Task.Factory.StartNew(Connect);
        }

        void Connect()
        {
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Task task = Task.Factory.StartNew(() => { HandleClient(client); });
            }
        }

        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            while (client != null)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    StreamReader reader = new StreamReader(stream);
                    string request = reader.ReadLine();
                    window.Dispatcher.Invoke(() => { window.UpdateLabel(request); });
                }
                catch { break; }
            }

            window.Dispatcher.Invoke(() => { window.UpdateLabel("end"); });

            client.Close();
        }      
        
        void SetHostName()
        {
            window.SetName(Dns.GetHostName());
        }
    }
}
