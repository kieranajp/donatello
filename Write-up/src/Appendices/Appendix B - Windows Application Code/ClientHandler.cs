using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Donatello
{
    /// <summary>
    /// ClientHandler object.
    /// Contains methods handling communication between this application's TCP server and a connected client.
    /// </summary>
    public class ClientHandler
    {
        #region Attributes & Constructor
        private TcpClient _Client;
        private string receivedData;
        /// <summary>
        /// Constructor for this class. Initialises the class then calls a method to handle communication.
        /// </summary>
        /// <param name="client">TcpClient: The socket connection that has been made.</param>
        public ClientHandler(TcpClient client)
        {
            _Client = client;
            Communicate();
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Method handling the network communication between client and server.
        /// Receives a byte containing the location of a download to begin, then spawns an instance of the Download class to initiate the download process.
        /// </summary>
        private void Communicate()
        {
            byte[] receivedBytes = new byte[65536];

            NetworkStream ns = _Client.GetStream();
            int size = (int)_Client.ReceiveBufferSize;
            ns.Read(receivedBytes, 0, size);
            receivedData = System.Text.Encoding.ASCII.GetString(receivedBytes).Substring(0, 32);

            Download d = new Download(receivedData);
        }
        #endregion
    }
}
