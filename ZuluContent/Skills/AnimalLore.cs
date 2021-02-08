using System;
using Scripts.Zulu.Engines.Classes;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using static Scripts.Zulu.Engines.Classes.SkillCheck;

namespace Server.SkillHandlers
{
    public class AnimalLore
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int) SkillName.AnimalLore].Callback = OnUse;
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.Target = new InternalTarget();

            m.SendLocalizedMessage(500328); // What animal should I look at?

            return Configs[SkillName.AnimalTaming].Delay;
        }

        private class InternalTarget : Target
        {
            public InternalTarget() : base(-1, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!from.Alive)
                {
                    from.SendLocalizedMessage(500331); // The spirits of the dead are not the province of animal lore.
                }
                else if (targeted is BaseCreature creature)
                {
                    if (creature.Body.IsAnimal || creature.Body.IsMonster || creature.Body.IsSea)
                    {
                        if (creature.Controlled)
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true,
                                "Looks like someone's pet.", from.NetState);
                            return;
                        }

                        if (!from.ShilCheckSkill(SkillName.AnimalLore))
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true,
                                "Hmm... you're not sure...", from.NetState);
                            return;
                        }


                        from.CloseGump<AnimalLoreGump>();
                        from.SendGump(new AnimalLoreGump(creature));
                    }
                    else
                    {
                        from.SendLocalizedMessage(500329); // That's not an animal!
                    }
                }
                else
                {
                    from.SendLocalizedMessage(500329); // That's not an animal!
                }
            }
        }
    }

    public class AnimalLoreGump : Gump
    {
        private static string FormatSkill(BaseCreature c, SkillName name)
        {
            Skill skill = c.Skills[name];

            if (skill.Base < 10.0)
                return "<div align=right>---</div>";

            return $"<div align=right>{skill.Value:F1}</div>";
        }

        private static string FormatAttributes(int cur, int max)
        {
            if (max == 0)
                return "<div align=right>---</div>";

            return $"<div align=right>{cur}/{max}</div>";
        }

        private static string RangeFormatAttributes(int min, int max)
        {
            if (max == 0)
                return "<div align=right>---</div>";

            return $"<div align=right>{min} - {max}</div>";
        }

        private static string FormatStat(int val)
        {
            if (val == 0)
                return "<div align=right>---</div>";

            return $"<div align=right>{val}</div>";
        }

        private static string FormatDouble(double val)
        {
            if (val == 0)
                return "<div align=right>---</div>";

            return $"<div align=right>{val:F1}</div>";
        }

        private static string FormatElement(int val)
        {
            if (val <= 0)
                return "<div align=right>---</div>";

            return $"<div align=right>{val}%</div>";
        }

        private static string GetStrength(BaseCreature creature)
        {
            return creature.HitsMax switch
            {
                > 1000 => "tremendously strong",
                > 500 => "very strong",
                > 300 => "strong",
                > 150 => "fairly strong",
                > 100 => "not so strong",
                > 75 => "fairly weak",
                _ => "very weak"
            };
        }

        private static string GetDescription(BaseCreature creature)
        {
            var description = "";
            if (creature.HasBreath)
            {
                description = "fire-breathing ";
            }
            else if (creature.AI == AIType.AI_Mage)
            {
                description = "spell-casting ";
            }

            return description;
        }

        private const int HeaderColor = 590;

        private const int LabelColor = 550;

        public AnimalLoreGump(BaseCreature c) : base(250, 150)
        {
            AddPage(0);

            AddBackground(-20, -20, 290, 360, 83);
            AddBackground(0, 0, 250, 320, 9390);

            AddLabel((250 / 2) - (c.Name.Length * 3), 5, HeaderColor, c.Name);

            var pages = 2;
            var page = 0;
            var description = $"Looks like a {GetStrength(c)} {GetDescription(c)}creature.";


            #region Attributes

            AddPage(++page);

            AddItem(30, 50, ShrinkTable.Lookup(c));
            AddHtml(80, 45, 140, 54, description, false, false);

            AddImage(30, 112, 2086);
            AddLabel(50, 110, HeaderColor, "Attributes");

            AddLabel(50, 128, LabelColor, "Hits");
            AddHtml(140, 128, 75, 18, FormatAttributes(c.Hits, c.HitsMax), false, false);

            AddLabel(50, 146, LabelColor, "Damage");
            AddHtml(140, 146, 75, 18, RangeFormatAttributes(c.DamageMin, c.DamageMax), false, false);

            AddLabel(50, 164, LabelColor, "Mana");
            AddHtml(140, 164, 75, 18, FormatAttributes(c.Mana, c.ManaMax), false, false);

            AddLabel(50, 182, LabelColor, "Str");
            AddHtml(140, 182, 75, 18, FormatStat(c.Str), false, false);

            AddLabel(50, 200, LabelColor, "Dex");
            AddHtml(140, 200, 75, 18, FormatStat(c.Dex), false, false);

            AddLabel(50, 218, LabelColor, "Int");
            AddHtml(140, 218, 75, 18, FormatStat(c.Int), false, false);

            AddLabel(50, 236, LabelColor, "Min Taming");
            AddHtml(140, 236, 75, 18, FormatDouble(c.MinTameSkill), false, false);

            AddLabel(50, 254, LabelColor, "Armor");
            AddHtml(140, 254, 75, 18, FormatStat(c.VirtualArmor), false, false);

            AddButton(135, 300, 5601, 5605, 0, GumpButtonType.Page, page + 1);
            AddButton(105, 300, 5603, 5607, 0, GumpButtonType.Page, pages);

            #endregion

            #region Resistances

            AddPage(++page);

            AddImage(30, 52, 2086);
            AddLabel(50, 50, HeaderColor, "Resistances");

            AddLabel(50, 68, LabelColor, "Magic Resist");
            AddHtml(140, 68, 75, 18, FormatSkill(c, SkillName.MagicResist), false, false);

            AddLabel(50, 86, LabelColor, "Air Resist");
            AddHtml(140, 86, 75, 18, FormatStat(c.ElementalAirResist), false, false);

            AddLabel(50, 104, LabelColor, "Earth Resist");
            AddHtml(140, 104, 75, 18, FormatStat(c.ElementalEarthResist), false, false);

            AddLabel(50, 122, LabelColor, "Fire Resist");
            AddHtml(140, 122, 75, 18, FormatStat(c.ElementalFireResist), false, false);

            AddLabel(50, 140, LabelColor, "Necro Resist");
            AddHtml(140, 140, 75, 18, FormatStat(c.ElementalNecroResist), false, false);

            AddLabel(50, 158, LabelColor, "Water Resist");
            AddHtml(140, 158, 75, 18, FormatStat(c.ElementalWaterResist), false, false);

            AddLabel(50, 176, LabelColor, "Poison Protection");
            AddHtml(140, 176, 75, 18, FormatStat(c.ElementalPoisonResist), false, false);

            AddLabel(50, 194, LabelColor, "Physical Protection");
            AddHtml(140, 194, 75, 18, FormatStat(c.ElementalPhysicalResist), false, false);

            AddLabel(50, 212, LabelColor, "Magic Immunity");
            AddHtml(140, 212, 75, 18, FormatStat(c.PermMagicImmunity), false, false);

            AddImage(30, 244, 2086);
            AddLabel(50, 242, HeaderColor, "Preferred Foods");

            int foodPref = 3000340;

            if ((c.FavoriteFood & FoodType.FruitsAndVegies) != 0)
                foodPref = 1049565; // Fruits and Vegetables
            else if ((c.FavoriteFood & FoodType.GrainsAndHay) != 0)
                foodPref = 1049566; // Grains and Hay
            else if ((c.FavoriteFood & FoodType.Fish) != 0)
                foodPref = 1049568; // Fish
            else if ((c.FavoriteFood & FoodType.Meat) != 0)
                foodPref = 1049564; // Meat
            else if ((c.FavoriteFood & FoodType.Eggs) != 0)
                foodPref = 1044477; // Eggs

            AddHtmlLocalized(50, 260, 160, 18, foodPref, 0x0, false, false);

            AddButton(135, 300, 5601, 5605, 0, GumpButtonType.Page, 1);
            AddButton(105, 300, 5603, 5607, 0, GumpButtonType.Page, page - 1);

            #endregion
        }
    }
}