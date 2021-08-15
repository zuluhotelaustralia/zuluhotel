using System;
using System.Collections;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Targeting;
using ZuluContent.Zulu.Skills;

namespace Server.SkillHandlers
{
     public class Stealing : BaseSkillHandler
    {
        public override SkillName Skill => SkillName.Stealing;

        private static readonly TargetOptions TargetOptions = new()
        {
            Range = 1,
            AllowNonLocal = true
        };
        
        public static readonly bool ClassicMode = false;
        
        private static bool IsInnocentTo(Mobile from, Mobile to)
        {
            return Notoriety.Compute(from, to) == Notoriety.Innocent;
        }
        
        private static bool IsEmptyHanded(Mobile from)
        {
            return (from.FindItemOnLayer(Layer.OneHanded) ?? from.FindItemOnLayer(Layer.TwoHanded)) == null;
        }
        
        private static Item TryStealItem(Mobile from, Item toSteal, ref bool caught)
            {
                Item stolen = null;

                object root = toSteal.RootParent;

                if (!IsEmptyHanded(from))
                {
                    from.SendFailureMessage(1005584); // Both hands must be free to steal.
                }
                else if (root is BaseVendor { IsInvulnerable: true })
                {
                    from.SendFailureMessage(1005598); // You can't steal from shopkeepers.
                }
                else if (root is PlayerVendor)
                {
                    from.SendFailureMessage(502709); // You can't steal from vendors.
                }
                else if (!from.CanSee(toSteal))
                {
                    from.SendFailureMessage(500237); // Target can not be seen.
                }
                else if (from.Backpack == null || !from.Backpack.CheckHold(from, toSteal, false, true))
                {
                    from.SendFailureMessage(1048147); // Your backpack can't hold anything else.
                }
                else if (toSteal.Parent == null || !toSteal.Movable)
                {
                    from.SendFailureMessage(502710); // You can't steal that!
                }
                else if (toSteal.LootType == LootType.Newbied || toSteal.CheckBlessed((Mobile)root))
                {
                    from.SendFailureMessage(502710); // You can't steal that!
                }
                else if (!from.InRange(toSteal.GetWorldLocation(), 1))
                {
                    from.SendFailureMessage(502703); // You must be standing next to an item to steal it.
                }
                else if (toSteal.Parent is Mobile)
                {
                    from.SendFailureMessage(1005585); // You cannot steal items which are equipped.
                }
                else if (root == from)
                {
                    from.SendFailureMessage(502704); // You catch yourself red-handed.
                }
                else if (root is Mobile { AccessLevel: > AccessLevel.Player })
                {
                    from.SendFailureMessage(502710); // You can't steal that!
                }
                else if (root is Mobile innocentVictim && !from.CanBeHarmful(innocentVictim))
                {
                    from.SendLocalizedMessage(502710); // You can't steal that!
                }
                else if (root is Mobile victim)
                {
                    var weight = toSteal.Weight + toSteal.TotalWeight;
                    
                    var victimDifficulty = (int)(victim.Dex +
                                                 Math.Max(victim.Skills[SkillName.Stealing].Value,
                                                     victim.Int / 2));

                    var difficulty = victimDifficulty - from.Dex + weight / 2 + 30;
                    difficulty = Math.Max(difficulty, 0);
                    
                    if (from.HandArmor is not ThiefGloves)
                        difficulty += 20.0;

                    if (from.ShilCheckSkill(SkillName.Stealing, (int) difficulty, (int) (difficulty * 20.0)))
                    {
                        from.SendSuccessMessage(502724); // You successfully steal the item.
                        stolen = toSteal;
                    }
                    else
                    {
                        from.SendFailureMessage(502723); // You fail to steal the item.
                        caught = true;

                        var lossKarma = from.Karma switch
                        {
                            > -625 => -Utility.RandomMinMax(1, 300),
                            > -2500 => -Utility.RandomMinMax(1, 100),
                            > -5000 => -Utility.RandomMinMax(1, 20),
                            _ => 0
                        };
                        Titles.AwardKarma(from, lossKarma, true);
                    }
                }

                return stolen;
            }

        public override async Task<TimeSpan> OnUse(Mobile from)
        {
            if (!IsEmptyHanded(from))
            {
                from.SendFailureMessage(1005584); // Both hands must be free to steal.
                return Delay;
            }
            
            var target = new AsyncTarget<Item>(from, TargetOptions);
            from.Target = target;
                
            from.RevealingAction();
            from.SendSuccessMessage(502698); // Which item do you want to steal?

            var (targeted, _) = await target;

            if (targeted == null)
            {
                from.SendFailureMessage(502710); // You can't steal that!
                return Delay;
            }
            
            from.RevealingAction();

            var caught = false;
            var stolen = TryStealItem(from, targeted, ref caught);;
            var root = targeted.RootParent;
            
            if (stolen != null)
            {
                from.AddToBackpack(stolen);
            }

            if (caught)
            {
                if (root == null)
                {
                    from.CriminalAction(false);
                }
                else if (root is Corpse corpse && corpse.IsCriminalAction(from))
                {
                    from.CriminalAction(false);
                }
                else if (root is Mobile mobRoot)
                {
                    if (IsInnocentTo(from, mobRoot))
                        from.CriminalAction(false);

                    var message = $"You notice {from.Name} trying to steal from {mobRoot.Name}.";

                    var range = (int) (15 - from.Skills[SkillName.Stealth].Value / 10);
                    var eable = from.GetClientsInRange(range);
                    
                    foreach (var ns in eable)
                        if (ns.Mobile != from)
                            ns.Mobile.SendMessage(message);

                    eable.Free();
                    
                    from.SendFailureMessage("You are noticed trying to steal!");
                }
            }

            return Delay;
        }
    }
}