using System;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Targeting;
using Server.Items;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Engines.Craft
{
    public class Repair
    {
        public Repair()
        {
        }

        public static void Do(Mobile from, CraftSystem craftSystem, BaseTool tool)
        {
            from.Target = new InternalTarget(craftSystem, tool);
            from.SendLocalizedMessage(1044276); // Target an item to repair.
        }

        private class InternalTarget : Target
        {
            private CraftSystem m_CraftSystem;
            private BaseTool m_Tool;

            public InternalTarget(CraftSystem craftSystem, BaseTool tool) : base(2, false, TargetFlags.None)
            {
                m_CraftSystem = craftSystem;
                m_Tool = tool;
            }

            private void DamageItem(Mobile from, int damageAmount, IRepairable repairable, ref int number)
            {
                damageAmount = Math.Max(damageAmount, 1);

                if (damageAmount >= repairable.HitPoints)
                {
                    number = 500424; // You destroyed the item.
                    from.SendFailureMessage(number);
                    repairable.Delete();
                }
                else
                {
                    number = 500039; // Failed!
                    from.SendFailureMessage("You fail in your repairing attempt and damage the item!");
                    repairable.HitPoints -= damageAmount;
                }
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                var number = 500425; // You repair the item.

                if (m_CraftSystem.CanCraft(from, m_Tool, targeted.GetType()) == 1044266)
                {
                    number = 1044266; // You must be near an anvil.
                }
                else if (targeted is IRepairable repairable)
                {
                    var damageAmount = repairable.MaxHitPoints - repairable.HitPoints;
                    var armsLoreValue = from.Skills[SkillName.ArmsLore].Value;
                    var difficulty = damageAmount + 20;

                    from.FireHook(h => h.OnArmsLoreBonus(from, ref armsLoreValue));

                    if (repairable.HitPoints == repairable.MaxHitPoints)
                    {
                        number = 500423; // That is already in full repair.
                        from.SendSuccessMessage(number);
                        from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, number));
                        return;
                    }

                    if (armsLoreValue < damageAmount)
                    {
                        var damage = damageAmount / 4;

                        DamageItem(from, damage, repairable, ref number);

                        from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, number));

                        return;
                    }

                    if (!from.ShilCheckSkill(m_CraftSystem.MainSkill, difficulty, 0))
                    {
                        var damage = damageAmount / 2;

                        DamageItem(from, damage, repairable, ref number);

                        from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, number));

                        return;
                    }

                    var chance = Utility.Random(100);

                    if (chance < 5)
                    {
                        var damage = damageAmount / 8;

                        DamageItem(from, damage, repairable, ref number);
                    }
                    else if (chance == 99)
                    {
                        from.SendSuccessMessage("Your chance and skill improved the quality of the item!");
                        repairable.Quality += 0.05;
                        repairable.HitPoints = repairable.MaxHitPoints;
                    }
                    else
                    {
                        var repairAmount = Utility.RandomMinMax(1, (int) armsLoreValue) / 2;
                        if (repairAmount >= damageAmount)
                        {
                            from.SendSuccessMessage("You repair the item completely.");
                            repairable.HitPoints = repairable.MaxHitPoints;
                        }
                        else
                        {
                            from.SendSuccessMessage("You successfully repair some of the damage.");
                            repairable.HitPoints += repairAmount;
                        }
                    }
                }
                else
                {
                    number = 500426; // You can't repair that.
                }

                from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, number));
            }
        }
    }
}