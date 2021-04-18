using System;
using System.Text;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Server.Mobiles;
using Server.Targeting;
using Server.Items;
using Server.Network;
using ZuluContent.Zulu.Skills;

namespace Server.SkillHandlers
{
    public class Forensics : BaseSkillHandler
    {
        private static readonly TargetOptions TargetOptions = new()
        {
            Range = 2,
        };

        public override SkillName Skill { get; } = SkillName.Forensics;

        public override async Task<TimeSpan> OnUse(Mobile from)
        {
            from.RevealingAction();
            from.SendLocalizedMessage(501000); // Select what you want to examine.
            
            var asyncTarget = new AsyncTarget<IEntity>(from, TargetOptions);
            from.Target = asyncTarget;

            var (target, responseType) = await asyncTarget;

            if (responseType != TargetResponseType.Success)
                return Delay;

            if (target == null)
                return Delay;
            
            from.Direction = from.GetDirectionTo(target);
            
            if (!from.ShilCheckSkill(Skill))
            {
                from.SendLocalizedMessage(501001); //You cannot determine anything useful.
                return Delay;
            }
            
            switch (target)
            {
                case Mobile:
                {
                    from.SendLocalizedMessage(target is PlayerMobile {NpcGuild: NpcGuild.ThievesGuild}
                            ? 501004 // That individual is a thief!
                            : 501003 // You notice nothing unusual.
                    );
                    break;
                }
                case Corpse corpse:
                {
                    // The forensicist  ~1_NAME~ has already discovered that:
                    if (corpse.Forensicist != null)
                        from.SendLocalizedMessage(1042750, corpse.Forensicist);
                    else
                        corpse.Forensicist = from.Name;

                    // BodyId is stored in Amount for the corpse item id as per UO packet protocol
                    if (new Body(corpse.Amount).IsHuman)
                    {
                        // This person was killed by ~1_KILLER_NAME~
                        from.SendLocalizedMessage(1042751, corpse.Killer == null
                            ? "no one"
                            : corpse.Killer.Name 
                        );
                    }

                    if (corpse.Looters.Count > 0)
                    {
                        var sb = new StringBuilder();
                        for (var i = 0; i < corpse.Looters.Count; i++)
                        {
                            if (i > 0)
                                sb.Append(", ");
                            sb.Append(corpse.Looters[i].Name);
                        }

                        // This body has been disturbed by ~1_PLAYER_NAMES~
                        from.SendLocalizedMessage(1042752, sb.ToString()); 
                    }
                    else
                    {
                        from.SendLocalizedMessage(501002); //The corpse has not be desecrated.
                    }

                    break;
                }
                case ILockpickable lockpickable:
                {
                    var p = lockpickable;
                    if (p.Picker != null)
                        from.SendLocalizedMessage(1042749, p.Picker.Name); //This lock was opened by ~1_PICKER_NAME~
                    else
                        from.SendLocalizedMessage(501003); //You notice nothing unusual.
                    break;
                }
            }

            return Delay;
        }
    }
}