using System;
using ConsoleApp.Logger;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace lab3
{
    class Program
    {
        private static readonly IFormatProvider culture;

        public static void Main()
        {
           {
                DateTime time = DateTime.Now;
                DateTime utcTime = DateTime.UtcNow;

                Console.WriteLine(time.ToString("o", culture));
           }
            {
                DateTime time = DateTime.Now;

                Console.WriteLine(time.ToString("yyyy-MM-ddTHH:mm:sszzz"));

                DateTime utcTime = DateTime.UtcNow;

                Console.WriteLine(utcTime.ToString("yyyy-MM-ddTHH:mm:ssZ"));

                {
                    string path = @"C:\path\to\file.txt";

                    using (FileStream stream = new FileStream(path, FileMode.Append))
                    {
                        using (TextWriter writer = new StreamWriter(stream, Encoding.UTF8))
                        {
                            writer.Write("Some text here ...\n");
                            writer.Write("Some text here ...\n");
                            writer.Write("Some text here ...\n");
                            writer.Flush();

                        }
                    }

                    ILogger[] loggers = new ILogger[]
                    {
                    new ConsoleLogger(),
                    new FileLogger("log.txt"),
                    new SocketLogger("google.com", 80)
                    };

                    using (ILogger logger = new CommonLogger(loggers))
                    {
                        logger.Log("Example message 1 ...");
                        logger.Log("Example message 2 ...");
                        logger.Log("Example message 3 ...", "value 1", "value 2", "value 3");
                    }
                }
            }
        }
        
        public interface ILogger : IDisposable
        {
            void Log(params String[] messages);
        }
        public abstract class WriterLogger : ILogger
        {
            protected TextWriter writer;

            public virtual void Log(params string[] messages)
            {
                // Uzupełnić to miejsce o logikę zapisu opartą o TextWriter ...
            }

            public abstract void Dispose();
        }

        public class ClientSocket : IDisposable
        {
            public static void Main()
            {
                string host = "some-domain.com";
                int port = 80;

                using (ClientSocket clientSocket = new ClientSocket(host, port))
                {
                    // request:

                    string requestText = "Message to sent ...";
                    byte[] requestBytes = Encoding.UTF8.GetBytes(requestText);

                    clientSocket.Send(requestBytes);

                    // response:

                    byte[] responseBuffer = new byte[1024];
                    int responseSize = clientSocket.Receive(responseBuffer);

                    string responseText = Encoding.UTF8.GetString(responseBuffer, 0, responseSize); // received message

                    // ...

                    // cleaning:

                    clientSocket.Close();
                }
            }

            private void Close()
            {
                throw new NotImplementedException();
            }

            private int Receive(byte[] responseBuffer)
            {
                throw new NotImplementedException();
            }

            private void Send(byte[] requestBytes)
            {
                throw new NotImplementedException();
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
        public class ClientSocket2 : IDisposable
        {
            private bool disposed;

            private Socket socket;

            public ClientSocket2(string host, int port)
            {
                IPHostEntry entry = Dns.GetHostEntry(host);

                this.socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    this.socket.Connect(entry.AddressList, port);
                }
                catch (SocketException ex)
                {
                    this.socket.Dispose();

                    throw ex;
                }
            }

            ~ClientSocket2()
            {
                this.Dispose(false);
            }

            public int Send(byte[] buffer)
            {
                return this.socket.Send(buffer, SocketFlags.None);
            }

            public int Send(byte[] buffer, int offset, int size)
            {
                return this.socket.Send(buffer, offset, size, SocketFlags.None);
            }

            public int Receive(byte[] buffer)
            {
                return this.socket.Receive(buffer, SocketFlags.None);
            }

            public int Receive(byte[] buffer, int offset, int size)
            {
                return this.socket.Receive(buffer, offset, size, SocketFlags.None);
            }

            public void Close()
            {
                this.socket.Shutdown(SocketShutdown.Both);
                this.socket.Close();
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                        this.socket.Dispose();

                    this.disposed = true;
                }
            }

            public void Dispose()
            {
                this.Dispose(disposing: true);

                GC.SuppressFinalize(this);
            }
        }
    }
}
