using System;
using System.Linq;
using Server.Items;
using Server.Targeting;
using ZuluContent.Configuration;

namespace Server.Engines.Craft
{
    public sealed class DefTinkering : CraftSystem
    {
        public static CraftSystem CraftSystem => new DefTinkering(ZhConfig.Crafting.Tinkering);

        private DefTinkering(CraftSettings settings) : base(settings)
        {
        }
        
        public override int GetCraftSkillRequired(int itemSkillRequired, Type craftResourceType)
        {
            var resource = CraftResources.GetFromType(craftResourceType);
            return itemSkillRequired + (int) (CraftResources.GetCraftSkillRequired(resource) / 4);
        }

        public override int GetCraftPoints(int itemSkillRequired, int materialAmount)
        {
            return (itemSkillRequired + materialAmount) * 10;
        }

        public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
            else if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            return 0;
        }

        private static Type[] m_TinkerColorables = new[]
        {
            typeof(Goblet), typeof(PewterMug),
            typeof(KeyRing),
            typeof(Candelabra),
            typeof(Key), typeof(Globe),
            typeof(Spyglass),
            typeof(HeatingStand), typeof(BaseLight),
            typeof(BaseTool), typeof(BaseTinkerItem)
        };

        public override bool RetainsColorFrom(CraftItem item, Type type)
        {
            if (!type.IsSubclassOf(typeof(BaseIngot)))
                return false;

            type = item.ItemType;

            bool contains = false;

            for (int i = 0; !contains && i < m_TinkerColorables.Length; ++i)
                contains = m_TinkerColorables[i] == type;

            return contains;
        }

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality,
            bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

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

        public override void InitCraftList()
        {
            base.InitCraftList();
            
            // Set the overridable material
            SetSubRes(typeof(IronIngot), 1044022);

            // Add every material you want the player to be able to choose from
            // This will override the overridable material
            ZhConfig.Resources.Ores.Entries.ToList()
                .ForEach(e => AddSubRes(e.SmeltType, e.Name, e.CraftSkillRequired, 1160300, e.Name));
            
            SetSubRes2(typeof(Log), 1027136);

            ZhConfig.Resources.Logs.Entries.ToList()
                .ForEach(e => AddSubRes2(e.ResourceType, e.Name.Length > 0 ? e.Name : "Log", e.CraftSkillRequired, e.Name));

            MarkOption = true;
            Repair = true;
            CanEnhance = false;
        }
    }

    public abstract class TrapCraft : CustomCraft
    {
        private LockableContainer m_Container;

        public LockableContainer Container
        {
            get { return m_Container; }
        }

        public abstract TrapType TrapType { get; }

        public TrapCraft(Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool,
            int mark, double quality) : base(from, craftItem, craftSystem, typeRes, tool, mark, quality)
        {
        }

        private int Verify(LockableContainer container)
        {
            if (container == null || container.KeyValue == 0)
                return 1005638; // You can only trap lockable chests.
            if (From.Map != container.Map || !From.InRange(container.GetWorldLocation(), 2))
                return 500446; // That is too far away.
            if (!container.Movable)
                return 502944; // You cannot trap this item because it is locked down.
            if (!container.IsAccessibleTo(From))
                return 502946; // That belongs to someone else.
            if (container.Locked)
                return 502943; // You can only trap an unlocked object.
            if (container.TrapType != TrapType.None)
                return 502945; // You can only place one trap on an object at a time.

            return 0;
        }

        private bool Acquire(object target, out int message)
        {
            LockableContainer container = target as LockableContainer;

            message = Verify(container);

            if (message > 0)
            {
                return false;
            }
            else
            {
                m_Container = container;
                return true;
            }
        }

        public override void EndCraftAction()
        {
            From.SendLocalizedMessage(502921); // What would you like to set a trap on?
            From.Target = new ContainerTarget(this);
        }

        private class ContainerTarget : Target
        {
            private TrapCraft m_TrapCraft;

            public ContainerTarget(TrapCraft trapCraft) : base(-1, false, TargetFlags.None)
            {
                m_TrapCraft = trapCraft;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                int message;

                if (m_TrapCraft.Acquire(targeted, out message))
                    m_TrapCraft.CraftItem.CompleteCraft(m_TrapCraft.Mark, m_TrapCraft.Quality, 0, false,
                        m_TrapCraft.From,
                        m_TrapCraft.CraftSystem, m_TrapCraft.TypeRes, m_TrapCraft.Tool, m_TrapCraft);
                else
                    Failure(message);
            }

            protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
            {
                if (cancelType == TargetCancelType.Canceled)
                    Failure(0);
            }

            private void Failure(int message)
            {
                Mobile from = m_TrapCraft.From;
                BaseTool tool = m_TrapCraft.Tool;

                if (tool != null && !tool.Deleted && tool.UsesRemaining > 0)
                    from.SendGump(new CraftGump(from, m_TrapCraft.CraftSystem, tool, message));
                else if (message > 0)
                    from.SendLocalizedMessage(message);
            }
        }

        public override Item CompleteCraft(out int message)
        {
            message = Verify(Container);

            if (message == 0)
            {
                var trapLevel = (int) (From.Skills.Tinkering.Value / 10);

                Container.TrapType = TrapType;
                Container.TrapStrength = trapLevel * 9;
                Container.TrapLevel = trapLevel;
                Container.TrapOnLockpick = true;

                message = 1005639; // Trap is disabled until you lock the chest.
            }

            return null;
        }
    }

    [CraftItemID(0x1BFC)]
    public class DartTrapCraft : TrapCraft
    {
        public override TrapType TrapType
        {
            get { return TrapType.DartTrap; }
        }

        public DartTrapCraft(Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool,
            int mark, double quality) : base(from, craftItem, craftSystem, typeRes, tool, mark, quality)
        {
        }
    }

    [CraftItemID(0x113E)]
    public class PoisonTrapCraft : TrapCraft
    {
        public override TrapType TrapType
        {
            get { return TrapType.PoisonTrap; }
        }

        public PoisonTrapCraft(Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool,
            int mark,
            double quality) : base(from, craftItem, craftSystem, typeRes, tool, mark, quality)
        {
        }
    }

    [CraftItemID(0x370C)]
    public class ExplosionTrapCraft : TrapCraft
    {
        public override TrapType TrapType
        {
            get { return TrapType.ExplosionTrap; }
        }

        public ExplosionTrapCraft(Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes,
            BaseTool tool, int mark, double quality) : base(from, craftItem, craftSystem, typeRes, tool, mark, quality)
        {
        }
    }
}