using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab13_Server
{
    class Connection        
    {
        String server = "192.168.0.16";
        NetworkStream stream;
        TcpClient client;
        private byte[] data;
        bool state = false;

        public bool State()
        {
            return state;
        }

        public String Listen()
        {
            data = new Byte[256];
            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Debug.Print("Received: {0}", responseData);
            return responseData;
        }

        public void Send(String message)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Debug.Print("Sent: {0}", message);
        }

        public bool Close()
        {
            stream.Close();
            client.Close();
            state = false;
            return true;
        }
        public void Connect()
        {
            try
            {
                Int32 port = 13000;
                client = new TcpClient(server, port);
                stream = client.GetStream();
                state = true;
            }
            catch (ArgumentNullException e)
            {
                Debug.Print("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Debug.Print("SocketException: {0}", e);
            }
        }
    }
}
