using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Donatello
{
    public partial class Listener : Form
    {
        TcpListener serverSocket = new TcpListener(IPAddress.Any, 31337);
        TcpClient clientSocket = default(TcpClient);

        public Listener()
        {
            InitializeComponent();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.RunWorkerAsync();
        }

        private delegate void WriteLogDelegate(string add);
        private void WriteLog(string add)
        {
            if (this.txt_log.InvokeRequired)
            {
                this.txt_log.Invoke(new WriteLogDelegate(this.WriteLog), add);
            }
            else
            {
                this.txt_log.Text += add + Environment.NewLine;
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            int counter = 0;
            WriteLog("Starting server...");
            serverSocket.Start();
            System.Threading.Thread.Sleep(500);
            WriteLog("Server started!");

            while (true)
            {
                counter++;
                clientSocket = serverSocket.AcceptTcpClient();
                WriteLog("Connection " + counter.ToString() + " made!");
                ClientHandler ch = new ClientHandler();
                ch.StartClient(clientSocket, counter);
            }
        }
    }

    public class ClientHandler
    {
        TcpClient clientSocket;
        int clientNo;
        string receivedData;

        public void StartClient(TcpClient client, int clientNumber)
        {
            this.clientSocket = client;
            this.clientNo = clientNumber;
            Communicate();
        }

        private void Communicate()
        {
            int requestCount = 0;
            byte[] receivedBytes = new byte[65536];

            try
            {
                requestCount++;
                NetworkStream ns = clientSocket.GetStream();
                int size = (int)clientSocket.ReceiveBufferSize;
                ns.Read(receivedBytes, 0, size);
                receivedData = System.Text.Encoding.ASCII.GetString(receivedBytes).Substring(0, 32);
                
                Downloader d = new Downloader();
                d.Get(receivedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
