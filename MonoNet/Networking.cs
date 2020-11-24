using Lidgren.Network;
using System;
using System.Collections.Generic;

namespace MonoNet
{
    public static class Networking
    {
        public static string AppName { get; set; } = "MonoApp";

        static NetPeer peer;

        static NetConnection connection;

        public static bool Connected => connection != null;

        #region Send Message

        public static NetOutgoingMessage CreateMessage() => peer.CreateMessage();

        public static void SendMessage(NetOutgoingMessage message, 
                                       NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered,
                                       int channel = 0)
        {
            if (connection != null)
            {
                peer.SendMessage(message, connection, method, channel);
            }
        }

        public static void SendUnconnectedMessage(NetOutgoingMessage message,
                                                  string ip,
                                                  int port)
        {
            if (connection != null)
            {
                peer.SendUnconnectedMessage(message, ip, port);
            }
        }
        #endregion

        #region Events

        public static Action OnConnect;
        public static Action<string> OnDisconnect;
        public static Action<string> OnError;

        #endregion

        public static bool IsServer { get; private set; }

        public static void SetupServer(int port)
        {
            var config = new NetPeerConfiguration(AppName)
            {
                Port = port,
                MaximumConnections = 1
            };
            
            peer = new NetServer(config);

            try
            {
                peer.Start();
                IsServer = true;
            }
            catch (Exception e)
            {
                Stop();
                OnError?.Invoke(e.Message);
            }
        }

        public static void SetupClient(string ipAddr, int port)
        {
            IsServer = false;

            var config = new NetPeerConfiguration(AppName);
            peer = new NetClient(config);

            peer.Start();
            
            try
            {
                peer.Connect(host: ipAddr, port: port);
                IsServer = false;
            }
            catch (Exception e)
            {
                Stop();
                OnError?.Invoke(e.Message);
            }
        }

        public static void Stop(string message = "Application exited.")
        {
            peer?.Shutdown(message);
            connection = null;
        }

        static readonly Queue<NetIncomingMessage> messageQueue = new Queue<NetIncomingMessage>();

        public static NetIncomingMessage ReceiveMessage()
        {
            if (messageQueue.Count > 0)
            {
                return messageQueue.Dequeue();
            } else { return null; }
        }

        public static void Update()
        {
            if (peer != null)
            {
                NetIncomingMessage message;

                while ((message = peer.ReadMessage()) != null)
                {
                    switch (message.MessageType)
                    {
                        case NetIncomingMessageType.Data:

                            messageQueue.Enqueue(message);
                            break;

                        case NetIncomingMessageType.StatusChanged:

                            switch (message.SenderConnection.Status)
                            {
                                case NetConnectionStatus.Connected:

                                    connection = message.SenderConnection;
                                    OnConnect?.Invoke();
                                    break;
                                case NetConnectionStatus.Disconnected:
                                    OnDisconnect?.Invoke(connection != null ? "The connection was lost."
                                                                            : "Failed to connect to player.");
                                    
                                    Stop();
                                    
                                    break;
                            }

                            break;

                        case NetIncomingMessageType.ErrorMessage:
                            OnError?.Invoke(message.ReadString());
                            break;
                    }
                }
            }
        }
    }

    public interface ICanReceive
    {
        void ReceiveMessage(NetIncomingMessage message);
    }
}
