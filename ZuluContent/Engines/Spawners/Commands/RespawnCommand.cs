using System.Collections.Generic;
using Server.Commands.Generic;
using Server.Network;

namespace Server.Engines.Spawners
{
    public class RespawnCommand : BaseCommand
    {
        public static void Initialize()
        {
            TargetCommands.Register(new RespawnCommand());
        }

        public RespawnCommand()
        {
            AccessLevel = AccessLevel.GameMaster;
            Supports = CommandSupport.Complex | CommandSupport.Simple;
            Commands = new[] { "Respawn" };
            ObjectTypes = ObjectTypes.Items;
            Usage = "Respawn";
            Description = "Respawns the given the spawners.";
            ListOptimized = true;
        }

        public override void ExecuteList(CommandEventArgs e, List<object> list)
        {
            if (list.Count == 0)
            {
                LogFailure("No matching objects found.");
                return;
            }

            e.Mobile.SendMessage("Respawning...");

            NetState.FlushAll();

            foreach (var obj in list)
            {
                if (obj is ISpawner spawner)
                {
                    spawner.Respawn();
                }
            }

            e.Mobile.SendMessage("Respawn completed.");
        }
    }
}