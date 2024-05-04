using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using CPA = Server.CommandPropertyAttribute;

namespace Server.Commands
{
    public static class Tame
    {
        public static void Initialize()
        {
            CommandSystem.Register("Tame", AccessLevel.Counselor, Tame_OnCommand);
        }

        [Usage("Tame [creature]")]
        [Description("Tames a targeted creature.")]
        private static void Tame_OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new PropsTarget();
            e.Mobile.SendMessage("Select a creature to tame.");
        }

        private class PropsTarget : Target
        {
            public PropsTarget() : base(-1, true, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is BaseCreature creature)
                {
                    // It seems to accept you as master.
                    creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502799, from.NetState);
                    creature.Owners.Add(from);
                    creature.SetControlMaster(from);
                }
                else
                {
                    from.SendMessage("That's not a creature.");
                }
            }
        }
    }
}