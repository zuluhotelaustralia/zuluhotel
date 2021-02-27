using System;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Targeting;
using Server.Items;
using static Server.Configurations.ResourceConfiguration;

namespace Server.Engines.Craft
{
    public enum FortifyResult
    {
        Success,
        Invalid,
        NoSkill
    }

    public class Fortify
    {
        public Fortify()
        {
        }

        public static void Do(Mobile from, CraftSystem craftSystem, BaseTool tool)
        {
            int num = craftSystem.CanCraft(from, tool, null);

            if (num > 0)
            {
                from.SendGump(new CraftGump(from, craftSystem, tool, num));
            }
            else
            {
                from.Target = new InternalTarget1(from, craftSystem, tool);
                from.SendSuccessMessage("Select a hat to fortify.");
            }
        }

        private class InternalTarget1 : Target
        {
            private Mobile m_From;
            private CraftSystem m_CraftSystem;
            private BaseTool m_Tool;

            public InternalTarget1(Mobile from, CraftSystem craftSystem, BaseTool tool) : base(2, false,
                TargetFlags.None)
            {
                m_From = from;
                m_CraftSystem = craftSystem;
                m_Tool = tool;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                int num = m_CraftSystem.CanCraft(from, m_Tool, null);

                if (num > 0)
                {
                    from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, num));
                    return;
                }

                if (targeted is BaseHat hat)
                {
                    if (!hat.IsChildOf(from.Backpack))
                    {
                        from.SendFailureMessage("The hat has to be in your backpack.");
                        return;
                    }

                    if (hat.Fortified == HatFortification.Fortified)
                    {
                        from.SendFailureMessage("That is fortified already!");
                        return;
                    }

                    if (hat.HitPoints != hat.MaxHitPoints)
                    {
                        from.SendFailureMessage("You must repair that first.");
                        return;
                    }

                    from.Target = new InternalTarget2(m_From, m_CraftSystem, m_Tool, hat);
                    from.SendSuccessMessage("Select the helm to fortify this hat with.");
                }
                else
                {
                    from.SendFailureMessage("That's not a hat.");
                }
            }
        }

        private class InternalTarget2 : Target
        {
            private Mobile m_From;
            private CraftSystem m_CraftSystem;
            private BaseTool m_Tool;
            private BaseHat m_Hat;

            public InternalTarget2(Mobile from, CraftSystem craftSystem, BaseTool tool, BaseHat hat) : base(2, false,
                TargetFlags.None)
            {
                m_From = from;
                m_CraftSystem = craftSystem;
                m_Tool = tool;
                m_Hat = hat;
            }

            private static bool IsHelm(object targeted)
            {
                return targeted is Bascinet || targeted is CloseHelm || targeted is Helmet || targeted is NorseHelm ||
                       targeted is PlateHelm || targeted is ChainCoif || targeted is BoneHelm || targeted is LeatherCap;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                int num = m_CraftSystem.CanCraft(from, m_Tool, null);

                if (num > 0)
                {
                    from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, num));
                    return;
                }

                if (targeted is BaseArmor helm && IsHelm(helm))
                {
                    if (!helm.IsChildOf(from.Backpack))
                    {
                        from.SendFailureMessage("The helm has to be in your backpack.");
                        return;
                    }

                    if (helm.HitPoints != helm.MaxHitPoints)
                    {
                        from.SendFailureMessage("You must repair that first.");
                        return;
                    }

                    if (!from.ShilCheckSkill(m_CraftSystem.MainSkill, -1, 250))
                    {
                        from.SendFailureMessage("You fail and ruin both, the hat and the helm.");
                        m_Hat.Delete();
                        helm.Delete();
                    }

                    m_Hat.Fortify(helm);

                    m_Hat.OnSingleClick(m_From);
                }
                else
                {
                    from.SendFailureMessage("That's not a helm.");
                }
            }
        }
    }
}