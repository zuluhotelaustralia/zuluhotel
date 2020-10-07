using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using MessagePack;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Server;
using Server.Network;
using Scripts.Zulu.Utilities;
using Server.Items;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Hooks
{
    public class TcpServerHook
    {
        private static IPAddress[] _listeningAddresses;

        public static void Initialize()
        {
            // Install();
            DoSomething();
        }

        private static void DoSomething()
        {
            // var types = typeof(IEnchantValue).GetInheritedClasses();
            //
            // var enchants = new List<object>();
            //
            // foreach (var type in types)
            // {
            //     var x = Activator.CreateInstance(type, true);
            //     enchants.Add(x);
            // }
            
            // var dict = new EnchantmentDictionary();
            //
            // dict.Set(new AirProtection { Value = 50});
            // dict.Set(new NecroProtection { Value = 50, Cursed = true});
            // dict.Set(new MagicalWeapon { Value = MagicalWeaponType.Swift });
            //
            //
            // var bin = MessagePackSerializer.Serialize(dict);
            //
            // Console.WriteLine(MessagePackSerializer.ConvertToJson(bin));
            //
            // AirProtection.EnchantmentInfo.ToString();
            //
            // Environment.Exit(0);
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
                .ConfigureServices(
                    services => { services.AddSingleton<IMessagePumpService>(new MessagePumpService()); })
                .UseKestrel(
                    options =>
                    {
                        foreach (var ipep in ServerConfiguration.Listeners)
                        {
                            options.Listen(ipep,
                                builder => { builder.UseConnectionHandler<ServerConnectionHandlerHook>(); });
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