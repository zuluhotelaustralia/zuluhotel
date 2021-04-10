using System;
using Server.Items;
using ZuluContent.Configuration;

namespace Server.Engines.Craft
{
    public class DefInscription : CraftSystem
	{
        public static CraftSystem CraftSystem => new DefInscription(ZhConfig.Crafting.Inscription);

        private DefInscription(CraftSettings settings) : base(settings) // 1 1 1.25
		{
		}

		public override int CanCraft(Mobile from, BaseTool tool, Type typeItem)
		{
			if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
				return 1044038; // You have worn out your tool!
			else if (!BaseTool.CheckAccessible(tool, from))
				return 1044263; // The tool must be on your person to use.

			if (typeItem != null)
			{
				object o = Activator.CreateInstance(typeItem);

				if (o is SpellScroll)
				{
					SpellScroll scroll = (SpellScroll)o;
					Spellbook book = Spellbook.Find(from, scroll.SpellEntry);

					bool hasSpell = book != null && book.HasSpell(scroll.SpellEntry);

					scroll.Delete();

					return hasSpell ? 0 : 1042404; // null : You don't have that spell!
				}
				else if (o is Item)
				{
					((Item)o).Delete();
				}
			}

			return 0;
		}

        private static Type typeofSpellScroll = typeof(SpellScroll);

		public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
		{
			if (toolBroken)
				from.SendLocalizedMessage(1044038); // You have worn out your tool

			if (!typeofSpellScroll.IsAssignableFrom(item.ItemType)) //  not a scroll
			{
				if (failed)
				{
					if (lostMaterial)
						return 1044043; // You failed to create the item, and some of your materials are lost.
					else
						return 1044157; // You failed to create the item, but no materials were lost.
				}
				else
				{
					if (quality == 0)
						return 502785; // You were barely able to make this item.  It's quality is below average.
					else if (makersMark && quality == 2)
						return 1044156; // You create an exceptional quality item and affix your maker's mark.
					else if (quality == 2)
						return 1044155; // You create an exceptional quality item.
					else
						return 1044154; // You create the item.
				}
			}
			else
			{
				if (failed)
					return 501630; // You fail to inscribe the scroll, and the scroll is ruined.
				else
					return 501629; // You inscribe the spell and put the scroll in your backpack.
			}
		}

        public override void InitCraftList()
		{
			base.InitCraftList();

			MarkOption = true;
        }
	}
}