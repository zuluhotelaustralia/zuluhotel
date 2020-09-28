using System;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Server;
using Server.Network;

namespace ZuluContent.Zulu.Hooks
{
    public class TcpServerHook
    {
        private static IPAddress[] _listeningAddresses;
        
        public static void Initialize()
        {
            // Install();
        }

        public static void Install()
        {
            EventSink.ServerStarted += () =>
            {
                var builder = CreateWebHostBuilder();
                
                var tcpHost = builder.Build();
                
                // Run indefinitely and block
                tcpHost.RunAsync(Core.ClosingTokenSource.Token).Wait();
                Environment.Exit(0);
            };
        }
        
        public static IWebHostBuilder CreateWebHostBuilder(string[] args = null) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSetting(WebHostDefaults.SuppressStatusMessagesKey, "True")
                .ConfigureServices(services => { services.AddSingleton<IMessagePumpService>(new MessagePumpService()); })
                .UseKestrel(
                    options =>
                    {
                        foreach (var ipep in ServerConfiguration.Listeners)
                        {
                            options.Listen(ipep, builder => { builder.UseConnectionHandler<ServerConnectionHandlerHook>(); });
                            _listeningAddresses = TcpServer.GetListeningAddresses(ipep);
                            DisplayListener(ipep);
                        }

                        // Webservices here
                    }
                )
                .UseLibuv()
                .UseStartup<ServerStartup>();
        
        private static void DisplayListener(IPEndPoint ipep)
        {
            if (ipep.Address.Equals(IPAddress.Any) || ipep.Address.Equals(IPAddress.IPv6Any))
            {
                foreach (var ip in _listeningAddresses)
                {
                    Console.WriteLine("Listening: {0}:{1}", ip, ipep.Port);
                }
            }
            else
            {
                Console.WriteLine("Listening: {0}:{1}", ipep.Address, ipep.Port);
            }
        }
    }
}