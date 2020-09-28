using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Logging;
using Scripts.Zulu.Packets;
using Server;
using Server.Network;

namespace ZuluContent.Zulu.Hooks
{
    public class ServerConnectionHandlerHook : ConnectionHandler
    {
        private readonly ILogger<ServerConnectionHandler> m_Logger;
        private readonly IMessagePumpService m_MessagePumpService;

        public ServerConnectionHandlerHook(
            IMessagePumpService messagePumpService,
            ILogger<ServerConnectionHandler> logger
        )
        {
            m_MessagePumpService = messagePumpService;
            m_Logger = logger;
        }

        public override async Task OnConnectedAsync(ConnectionContext connection)
        {
            if (!VerifySocket(connection))
            {
                Release(connection);
                return;
            }

            var ns = new PolAsciiNetState(connection);
            TcpServer.Instances.Add(ns);
            Console.WriteLine($"Client: {ns}: Connected. [{TcpServer.Instances.Count} Online]");

            await ns.ProcessIncoming(m_MessagePumpService).ConfigureAwait(false);
        }

        private static bool VerifySocket(ConnectionContext connection)
        {
            try
            {
                var args = new SocketConnectEventArgs(connection);

                EventSink.InvokeSocketConnect(args);

                return args.AllowConnection;
            }
            catch (Exception ex)
            {
                NetState.TraceException(ex);
                return false;
            }
        }

        private static void Release(ConnectionContext connection)
        {
            try
            {
                connection.Abort(new ConnectionAbortedException("Failed socket verification."));
            }
            catch (Exception ex)
            {
                NetState.TraceException(ex);
            }

            try
            {
                connection.DisposeAsync();
            }
            catch (Exception ex)
            {
                NetState.TraceException(ex);
            }
        }
    }
}