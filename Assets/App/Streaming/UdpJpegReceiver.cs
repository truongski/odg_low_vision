using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace App.Streaming
{
    public class UdpJpegReceiver : IDisposable
    {
        private UdpClient listenSocket;
        private int listenPort;
        private bool running;
        private Thread receiveThread;
        public event Action<byte[]> onDataReceived;


        public UdpJpegReceiver(int listenPort)
        {
            this.listenPort = listenPort;

            receiveThread = new Thread(new ThreadStart(ReceiveTask));
            receiveThread.Start();
        }

        private void ReceiveTask()
        {
            listenSocket = new UdpClient(listenPort);
            listenSocket.Client.ReceiveTimeout = 1000;
            IPEndPoint listenEP = new IPEndPoint(IPAddress.Any, listenPort);
            running = true;
            while (running)
            {
                try
                {
                    byte[] received = listenSocket.Receive(ref listenEP);
                    if (received != null && received.Length > 0)
                    {
                        if (onDataReceived != null)
                        {
                            onDataReceived(received);
                        }
                    }
                }
                catch (SocketException ex)
                {
                    // timeout
                }
            }
            listenSocket.Close();
            listenSocket = null;
        }

        private void Stop()
        {
            running = false;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    running = false;
                    receiveThread.Abort();
                    if (listenSocket != null)
                    {
                        listenSocket.Close();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UdpJpegReceiver() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}