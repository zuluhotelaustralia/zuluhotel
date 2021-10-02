using System;
using Server.Items;
using ZuluContent.Configuration.Types.Crafting;

namespace Server.Engines.Craft
{
    public class DefInscription : CraftSystem
    {
        public static CraftSystem CraftSystem => new DefInscription(ZhConfig.Crafting.Inscription);

        private DefInscription(CraftSettings settings) : base(settings)
        {
        }

        public override int GetCraftPoints(int itemSkillRequired, int materialAmount)
        {
            return itemSkillRequired * 15;
        }

        public override int CanCraft(Mobile from, BaseTool tool, Type typeItem)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!

            if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            if (typeItem != null)
            {
                var o = Activator.CreateInstance(typeItem);

                if (o is SpellScroll spellScroll)
                {
                    if (spellScroll is EarthSpellScroll earthSpellScroll)
                    {
                        var hasEarthSpell = from.Backpack.FindItemByType(typeof(Earthbook)) is Earthbook earthBook &&
                                            earthBook.HasSpell(earthSpellScroll.SpellEntry);

                        earthSpellScroll.Delete();

                        return hasEarthSpell ? 0 : 1042404; // null : You don't have that spell!
                    }

                    if (spellScroll is NecroSpellScroll necroSpellScroll)
                    {
                        var hasNecroSpell = from.Backpack.FindItemByType(typeof(Codex)) is Codex codex &&
                                            codex.HasSpell(necroSpellScroll.SpellEntry);

                        necroSpellScroll.Delete();

                        return hasNecroSpell ? 0 : 1042404; // null : You don't have that spell!
                    }

                    var book = Spellbook.Find(from, spellScroll.SpellEntry);

                    var hasSpell = book != null && book.HasSpell(spellScroll.SpellEntry);

                    spellScroll.Delete();

                    return hasSpell ? 0 : 1042404; // null : You don't have that spell!
                }

                if (o is Item item)
                    item.Delete();
            }

            return 0;
        }

        private static readonly Type typeofSpellScroll = typeof(SpellScroll);

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality,
            bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

            if (!typeofSpellScroll.IsAssignableFrom(item.ItemType)) //  not a scroll
            {
                if (failed)
                {
                    if (lostMaterial)
                        return 1044043; // You failed to create the item, and some of your materials are lost.
                    return 1044157; // You failed to create the item, but no materials were lost.
                }

                if (quality == 0)
                    return 502785; // You were barely able to make this item.  It's quality is below average.
                if (makersMark && quality == 2)
                    return 1044156; // You create an exceptional quality item and affix your maker's mark.
                if (quality == 2)
                    return 1044155; // You create an exceptional quality item.
                return 1044154; // You create the item.
            }

            if (failed)
                return 501630; // You fail to inscribe the scroll, and the scroll is ruined.
            return 501629; // You inscribe the spell and put the scroll in your backpack.
        }

        public override void InitCraftList()
        {
            base.InitCraftList();

            MarkOption = true;
        }
    }
}