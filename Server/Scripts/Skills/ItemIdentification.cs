using System;
using Server;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
    public class ItemIdentification
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.ItemID].Callback = new SkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile from)
        {
            from.SendLocalizedMessage(500343); // What do you wish to appraise and identify?
            from.Target = new InternalTarget();

            return TimeSpan.FromSeconds(1.0);
        }

        [PlayerVendorTarget]
        private class InternalTarget : Target
        {
            public InternalTarget() : base(8, false, TargetFlags.None)
            {
                AllowNonlocal = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Item)
                {
                    if (from.CheckTargetSkill(SkillName.ItemID, o, 0, 130))
                    {
                        if (o is BaseWeapon)
                        {
                            ((BaseWeapon)o).Identified = true;
                        }
                        else if (o is BaseJewel)
                        {
                            BaseJewel j = o as BaseJewel;
                            j.Identified = true;
                            if (j.VirtualArmorMod > 0)
                            {
                                j.Hue = 2406;
                            }
                        }
                        else if (o is BaseClothing)
                        {
                            BaseClothing c = o as BaseClothing;
                            c.Identified = true;
                            if (c.VirtualArmorMod > 0)
                            {
                                c.Hue = 2406;
                            }
                            else if (c.Prot.Level > 0)
                            {
                                c.Hue = MagicClothing.DecideHue(c.Prot.Element);
                            }
                        }
                        else if (o is BaseArmor)
                        {
                            ((BaseArmor)o).Identified = true;
                        }

                        if (!Core.AOS)
                        {
                            ((Item)o).OnSingleClick(from);
                        }
                    }
                    else
                    {
                        from.SendLocalizedMessage(500353); // You are not certain...
                    }
                }
                else if (o is Mobile)
                {
                    ((Mobile)o).OnSingleClick(from);
                }
                else
                {
                    from.SendLocalizedMessage(500353); // You are not certain...
                }
            }
        }
    }
}
