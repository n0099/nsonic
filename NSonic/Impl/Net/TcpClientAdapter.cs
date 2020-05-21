﻿using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace NSonic.Impl.Net
{
    class TcpClientAdapter : ITcpClient
    {
        private TcpClient client;

        public bool Connected => this.client?.Connected ?? false;

        public SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1, 1);

        public virtual void Connect(string hostname, int port)
        {
            this.client?.Dispose();
            this.client = new TcpClient
            {
                ReceiveTimeout = 5000,
                SendTimeout = 5000
            };

            this.client.Connect(hostname, port);
        }

        public virtual async Task ConnectAsync(string hostname, int port)
        {
            this.client?.Dispose();
            this.client = new TcpClient
            {
                ReceiveTimeout = 5000,
                SendTimeout = 5000
            };

            await this.client.ConnectAsync(hostname, port);
        }

        public void Dispose()
        {
            this.client?.Dispose();
        }

        public virtual Stream GetStream()
        {
            return this.client.GetStream();
        }
    }
}
