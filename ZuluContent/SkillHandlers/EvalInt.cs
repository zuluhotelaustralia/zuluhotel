using System;
using Scripts.Zulu.Engines.Classes;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.SkillHandlers
{
    public static class EvalInt
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.EvalInt].Callback = OnUse;
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.Target = new InternalTarget();

            m.SendLocalizedMessage(500906); // What do you wish to evaluate?

            return ZhConfig.Skills.Entries[SkillName.EvalInt].Delay;
        }

        private class InternalTarget : Target
        {
            public InternalTarget() : base(8, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (from == targeted)
                {
                    // Hmm, that person looks really silly.
                    from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 500910);
                    return;
                }

                switch (targeted)
                {
                    case TownCrier crier:
                        // He looks smart enough to remember the news.  Ask him about it.
                        crier.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 500907, from.NetState);
                        break;
                    case BaseVendor {IsInvulnerable: true} vendor:
                        // That person could probably calculate the cost of what you buy from them.
                        vendor.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 500909, from.NetState); 
                        break;
                    case Item item:
                        // It looks smarter than a rock, but dumber than a piece of wood.
                        item.SendLocalizedMessageTo(from, 500908);
                        break;
                    case Mobile target:
                    {
                        var intMod = target.Int switch
                        {
                            > 150 => 10, // "Superhumanly intelligent in a manner you cannot comprehend."
                            > 135 => 9,  // "Like a definite genius."
                            > 120 => 8,  // "Like a formidable intellect, well beyond even the extraordinary."
                            > 105 => 7,  // "Extraordinarily intelligent."
                            > 90 => 6,   // "Extremely intelligent."
                            > 75 => 5,   // "Very intelligent."
                            > 60 => 4,   // "Moderately intelligent."
                            > 45 => 3,   // "About Average."
                            > 30 => 2,   // "Not the brightest."
                            > 15 => 1,   // "Fairly Stupid."
                            _ => 0,      // "Slightly less intelligent than a rock."
                        };
                        
                        var mana = target.Mana * 100 / Math.Max(target.ManaMax, 1) / 10;
                        var manaMod = mana switch
                        {
                            > 10 => 10,
                            < 0 => 0,
                            _ => mana
                        };

                        int body;
                        if (target.Body.IsHuman)
                            body = target.Female ? 11 : 0;
                        else
                            body = 22;

                        if (from.ShilCheckSkill(SkillName.EvalInt))
                        {
                            // He/She/It looks [slightly less intelligent than a rock.]  [Of Average intellect] [etc...]
                            target.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1038169 + intMod + body, from.NetState);
                            
                            if (from.Skills[SkillName.EvalInt].Base >= 90.0)
                            {
                                // That being is at [10,20,...] percent mental strength.
                                target.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1038202 + manaMod, from.NetState);
                            }
                        }
                        else
                        {
                            // You cannot judge his/her/its mental abilities.
                            target.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1038166 + body / 11, from.NetState);
                        }
                        break;
                    }
                }
            }
        }
    }
}