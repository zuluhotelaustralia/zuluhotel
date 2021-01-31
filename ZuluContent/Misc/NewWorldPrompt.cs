using System;
using System.Collections.Generic;
using System.Linq;
using Server;
using Server.Commands;
using Server.Engines.Spawners;
using Server.Items;

// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedType.Global

namespace ZuluContent.Misc
{
    public class NewWorldPrompt
    {
        private static readonly bool AutoSetupNewWorld;
        
        static NewWorldPrompt()
        {
            AutoSetupNewWorld = 
                ServerConfiguration.GetOrUpdateSetting("newWorldPrompt.autoSetup", false);
        }

        public static void Initialize()
        {
            if (World.Items.Any(kv => kv.Value is BaseDoor))
                return;

            ConsoleKey key;
            if (AutoSetupNewWorld)
            {
                Console.WriteLine("This appears to be a new world, automatically setting it up.");
                key = ConsoleKey.Y;
            }
            else
            {
                Console.WriteLine("This appears to be a new world, do you want generate signs/doors/decoration? (y/n)");
                key = Console.ReadKey(true).Key;
            }
            
            if (key == ConsoleKey.Y)
            {
                Console.Write("Generating... ");
                DoorGenerator.Generate();
                Decorate.Generate();
                SignGenerator.Generate();
                GenerateSpawners.Generate("felucca.json");

                World.Save();
                Console.WriteLine("New world generation complete.");
            }
            
        }
    }
}