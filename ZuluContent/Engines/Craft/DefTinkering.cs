using System;
using System.Linq;
using Server.Items;
using Server.Targeting;
using static Server.Configurations.ResourceConfiguration;

namespace Server.Engines.Craft
{
    public class DefTinkering : CraftSystem
    {
        public override SkillName MainSkill => SkillName.Tinkering;

        public override int GumpTitleNumber => 1044007; // <CENTER>TINKERING MENU</CENTER>

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefTinkering();

                return m_CraftSystem;
            }
        }

        private DefTinkering() : base(3, 3, 2)
        {
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            if (item.NameNumber == 1044258 || item.NameNumber == 1046445) // potion keg and faction trap removal kit
                return 0.5; // 50%

            return 0.0; // 0%
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
            typeof(BaseTool)
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

        public override void PlayCraftEffect(Mobile from)
        {
            from.PlaySound( 0x241 );
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

        private void AddJewelrySet(GemType gemType, Type itemType)
        {
            var offset = (int) gemType - 1;
            
            var index = AddCraft(typeof(SilverBeadNecklace), 1044049, 1044185 + offset, 70.0, 70.0, typeof(IronIngot),
                1044036, 4, 1044037);
            AddRes(index, itemType, 1044231 + offset, 1, 1044240);

            index = AddCraft(typeof(GoldBracelet), 1044049, 1044221 + offset, 70.0, 70.0, typeof(IronIngot), 1044036, 3,
                1044037);
            AddRes(index, itemType, 1044231 + offset, 1, 1044240);
            
            index = AddCraft(typeof(GoldEarrings), 1044049, 1044203 + offset, 70.0, 70.0, typeof(IronIngot), 1044036, 2,
                1044037);
            AddRes(index, itemType, 1044231 + offset, 1, 1044240);
            
            index = AddCraft(typeof(GoldNecklace), 1044049, 1044194 + offset, 70.0, 70.0, typeof(IronIngot), 1044036, 6,
                1044037);
            AddRes(index, itemType, 1044231 + offset, 1, 1044240);
            
            index = AddCraft(typeof(GoldBeadNecklace), 1044049, 1044212 + offset, 70.0, 70.0, typeof(IronIngot),
                1044036, 6, 1044037);
            AddRes(index, itemType, 1044231 + offset, 1, 1044240);
            
            index = AddCraft(typeof(GoldRing), 1044049, 1044176 + offset, 70.0, 70.0, typeof(IronIngot), 1044036, 4,
                1044037);
            AddRes(index, itemType, 1044231 + offset, 1, 1044240);
        }

        public override void InitCraftList()
        {
            int index = -1;

            #region Wooden Items

            index = AddCraft(typeof(JointingPlane), 1044042, 1024144, 40.0, 40.0, typeof(Log), 1044041, 4, 1044351);
            SetUseSubRes2(index, true);
            
            index = AddCraft(typeof(MouldingPlane), 1044042, 1024140, 40.0, 40.0, typeof(Log), 1044041, 4, 1044351);
            SetUseSubRes2(index, true);

            index = AddCraft(typeof(SmoothingPlane), 1044042, 1024146, 28.0, 28.0, typeof(Log), 1044041, 4, 1044351);
            SetUseSubRes2(index, true);

            index = AddCraft(typeof(BlankScroll), 1044042, 1023636, 85.0, 85.0, typeof(Log), 1044041, 3, 1044351);
            SetUseSubRes2(index, true);

            index = AddCraft(typeof(Spellbook), 1044042, 1023643, 90.0, 90.0, typeof(Log), 1044041, 10, 1044351);
            SetUseSubRes2(index, true);

            // Tarot cards
            index = AddCraft(typeof(BlankMap), 1044042, "blank map", 85.0, 85.0, typeof(Log), 1044041, 5, 1044351);
            SetUseSubRes2(index, true);

            // Wig stand
            index = AddCraft(typeof(DyeTub), 1044042, 1024011, 65.0, 65.0, typeof(Log), 1044041, 10, 1044351);
            SetUseSubRes2(index, true);

            index = AddCraft(typeof(Globe), 1044042, 1011215, 85.0, 85.0, typeof(Log), 1044041, 15, 1044351);
            SetUseSubRes2(index, true);

            index = AddCraft(typeof(ClockFrame), 1044042, 1024173, 40.0, 40.0, typeof(Log), 1044041, 6, 1044351);
            SetUseSubRes2(index, true);

            index = AddCraft(typeof(Axle), 1044042, 1024187, 20.0, 20.0, typeof(Log), 1044041, 2, 1044351);
            SetUseSubRes2(index, true);

            // Ship model
            // Glass mugs
            // Tankards
            
            #endregion

            #region Tools
            
            AddCraft(typeof(MortarPestle), 1044046, 1023739, 67.0, 67.0, typeof(IronIngot), 1044036, 5, 1044037);
            AddCraft(typeof(Shovel), 1044046, 1023898, 51.0, 51.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(SewingKit), 1044046, 1023997, 28.0, 28.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(Scissors), 1044046, 1023998, 15.0, 15.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(Tongs), 1044046, 1024028, 44.0, 44.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(ScribesPen), 1044046, 1044168, 60.0, 60.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(DovetailSaw), 1044046, 1024136, 42.0, 42.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(Hammer), 1044046, 1024138, 43.0, 43.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(Saw), 1044046, 1024148, 41.0, 41.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(DrawKnife), 1044046, 1024324, 42.0, 42.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(Froe), 1044046, 1024325, 43.0, 43.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(Inshave), 1044046, 1024326, 43.0, 43.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(Scorp), 1044046, 1024327, 43.0, 43.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(SmithHammer), 1044046, 1025091, 51.0, 51.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(Lockpick), 1044046, 1025371, 58.0, 58.0, typeof(IronIngot), 1044036, 1, 1044037);
            // Flask stand
            AddCraft(typeof(HeatingStand), 1044046, 1026217, 70.0, 70.0, typeof(IronIngot), 1044036, 6, 1044037);
            // Scales
            AddCraft(typeof(TinkerTools), 1044046, 1044164, 25.0, 25.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(FletcherTools), 1044046, 1044166, 25.0, 25.0, typeof(IronIngot), 1044036, 3, 1044037);
            
            AddCraft(typeof(MagicWand), 1044046, 1017085, 70.0, 70.0, typeof(IronIngot), 1044036, 7, 1044037);
            AddCraft(typeof(Pickaxe), 1044046, 1023718, 52.0, 52.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(Pitchfork), 1044046, 1023719, 75.0, 75.0, typeof(IronIngot), 1044036, 15, 1044037);
            AddCraft(typeof(Cleaver), 1044046, 1023778, 33.0, 33.0, typeof(IronIngot), 1044036, 3, 1044037);
            AddCraft(typeof(SkinningKnife), 1044046, 1023781, 33.0, 33.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(Hatchet), 1044046, 1023907, 44.0, 44.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(SledgeHammer), 1044046, 1024021, 59.0, 59.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(ButcherKnife), 1044046, 1025110, 36.0, 36.0, typeof(IronIngot), 1044036, 2, 1044037);

            #endregion

            #region Parts
            
            AddCraft(typeof(BarrelTap), 1044047, 1024100, 60.0, 60.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(ClockParts), 1044047, 1024175, 40.0, 40.0, typeof(IronIngot), 1044036, 1, 1044037);
            AddCraft(typeof(Gears), 1044047, 1024179, 5.0, 5.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(Hinge), 1044047, 1024181, 10.0, 10.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(SextantParts), 1044047, 1024185, 40.0, 40.0, typeof(IronIngot), 1044036, 5, 1044037);
            AddCraft(typeof(Springs), 1044047, 1024189, 3.0, 3.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(BarrelHoops), 1044047, 1024321, 35.0, 35.0, typeof(IronIngot), 1044036, 5, 1044037);

            #endregion

            #region Utensils

            AddCraft(typeof(Cauldron), 1044048, 1022420, 50.0, 50.0, typeof(IronIngot), 1044036, 10, 1044037);
            AddCraft(typeof(Frypan), 1044048, 1022431, 40.0, 40.0, typeof(IronIngot), 1044036, 5, 1044037);
            AddCraft(typeof(Silverware), 1044048, "silverware", 40.0, 40.0, typeof(IronIngot), 1044036, 5, 1044037);
            AddCraft(typeof(Kettle), 1044048, 1022541, 40.0, 40.0, typeof(IronIngot), 1044036, 6, 1044037);

            #endregion

            #region Light
            
            AddCraft(typeof(WallSconce), 1076185, "wall sconce", 65.0, 65.0, typeof(IronIngot), 1044036, 10, 1044037);
            AddCraft(typeof(Lantern), 1076185, 1022597, 65.0, 65.0, typeof(IronIngot), 1044036, 12, 1044037);
            AddCraft(typeof(CandleLarge), 1076185, 1022575, 60.0, 60.0, typeof(IronIngot), 1044036, 10, 1044037);
            AddCraft(typeof(Candelabra), 1076185, 1022599, 70.0, 70.0, typeof(IronIngot), 1044036, 12, 1044037);
            AddCraft(typeof(CandelabraStand), 1076185, 1022599, 75.0, 75.0, typeof(IronIngot), 1044036, 15, 1044037);

            #endregion

            #region Jewelry

            AddJewelrySet(GemType.StarSapphire, typeof(StarSapphire));
            AddJewelrySet(GemType.Emerald, typeof(Emerald));
            AddJewelrySet(GemType.Sapphire, typeof(Sapphire));
            AddJewelrySet(GemType.Ruby, typeof(Ruby));
            AddJewelrySet(GemType.Citrine, typeof(Citrine));
            AddJewelrySet(GemType.Amethyst, typeof(Amethyst));
            AddJewelrySet(GemType.Tourmaline, typeof(Tourmaline));
            AddJewelrySet(GemType.Amber, typeof(Amber));
            AddJewelrySet(GemType.Diamond, typeof(Diamond));

            #endregion

            #region Misc
            
            AddCraft(typeof(Brazier), 1044050, 1023633, 75.0, 75.0, typeof(IronIngot), 1044036, 25, 1044037);
            AddCraft(typeof(MetalChest), 1044050, 1023648, 65.0, 65.0, typeof(IronIngot), 1044036, 15, 1044037);
            AddCraft(typeof(MetalBox), 1044050, 1022472, 68.0, 68.0, typeof(IronIngot), 1044036, 10, 1044037);
            AddCraft(typeof(Key), 1044050, 1024112, 50.0, 50.0, typeof(IronIngot), 1044036, 3, 1044037);
            AddCraft(typeof(KeyRing), 1044050, 1024113, 95.0, 95.0, typeof(IronIngot), 1044036, 5, 1044037);
            AddCraft(typeof(Shackles), 1044050, 1024706, 75.0, 75.0, typeof(IronIngot), 1044036, 15, 1044037);
            
            AddCraft(typeof(Pitcher), 1044050, 1022471, 30.0, 30.0, typeof(RawGlass), 1044036, 4, 1044037);
            AddCraft(typeof(Goblet), 1044050, 1022458, 75.0, 75.0, typeof(RawGlass), 1044036, 3, 1044037);
            AddCraft(typeof(CrystalBall), 1044050, 1023629, 70.0, 70.0, typeof(RawGlass), 1044036, 15, 1044037);
            AddCraft(typeof(Spyglass), 1044050, 1025365, 80.0, 80.0, typeof(RawGlass), 1044036, 10, 1044037);
            AddCraft(typeof(Hourglass), 1044050, 1026160, 75.0, 75.0, typeof(RawGlass), 1044036, 10, 1044037);
            AddCraft(typeof(Glass), 1044050, 1028065, 30.0, 30.0, typeof(RawGlass), 1044036, 2, 1044037);
            AddCraft(typeof(Bottle), 1044050, 1023835, 30.0, 30.0, typeof(RawGlass), 1044036, 2, 1044037);
            // Empty flask
            // Flask
            // Empty flask 2
            // Empty flask 3
            // Empty vials
            
            AddCraft(typeof(AlchemicalSymbol1), 1044050, 1026173, 75.0, 75.0, typeof(Clay), 1044036, 25, 1044037);
            AddCraft(typeof(AlchemicalSymbol2), 1044050, 1026173, 80.0, 80.0, typeof(Clay), 1044036, 25, 1044037);
            AddCraft(typeof(AlchemicalSymbol3), 1044050, 1026173, 85.0, 85.0, typeof(Clay), 1044036, 25, 1044037);
            AddCraft(typeof(AlchemicalSymbol4), 1044050, 1026173, 90.0, 90.0, typeof(Clay), 1044036, 25, 1044037);
            AddCraft(typeof(AlchemicalSymbol5), 1044050, 1026173, 95.0, 95.0, typeof(Clay), 1044036, 25, 1044037);
            AddCraft(typeof(AlchemicalSymbol6), 1044050, 1026173, 100.0, 100.0, typeof(Clay), 1044036, 25, 1044037);
            AddCraft(typeof(AlchemicalSymbol7), 1044050, 1026173, 105.0, 105.0, typeof(Clay), 1044036, 25, 1044037);
            AddCraft(typeof(AlchemicalSymbol8), 1044050, 1026173, 110.0, 110.0, typeof(Clay), 1044036, 25, 1044037);
            AddCraft(typeof(AlchemicalSymbol9), 1044050, 1026173, 115.0, 115.0, typeof(Clay), 1044036, 25, 1044037);
            AddCraft(typeof(AlchemicalSymbol10), 1044050, 1026173, 125.0, 125.0, typeof(Clay), 1044036, 25, 1044037);
            AddCraft(typeof(AlchemicalSymbol11), 1044050, 1026173, 135.0, 135.0, typeof(Clay), 1044036, 25, 1044037);
            
            AddCraft(typeof(RecallRune), 1044050, 1027956, 90.0, 90.0, typeof(Clay), 1044036, 7, 1044037);

            #endregion

            #region Multi-Component Items

            index = AddCraft(typeof(AxleGears), 1044051, 1024177, 40.0, 40.0, typeof(Axle), 1044169, 1, 1044253);
            AddRes(index, typeof(Gears), 1044254, 1, 1044253);

            index = AddCraft(typeof(ClockParts), 1044051, 1024175, 40.0, 40.0, typeof(AxleGears), 1044170, 1, 1044253);
            AddRes(index, typeof(Springs), 1044171, 1, 1044253);

            index = AddCraft(typeof(SextantParts), 1044051, 1024185, 40.0, 40.0, typeof(AxleGears), 1044170, 1, 1044253);
            AddRes(index, typeof(Hinge), 1044172, 1, 1044253);

            index = AddCraft(typeof(ClockRight), 1044051, 1044257, 40.0, 40.0, typeof(ClockFrame), 1044174, 1, 1044253);
            AddRes(index, typeof(ClockParts), 1044173, 1, 1044253);

            index = AddCraft(typeof(ClockLeft), 1044051, 1044256, 40.0, 40.0, typeof(ClockFrame), 1044174, 1, 1044253);
            AddRes(index, typeof(ClockParts), 1044173, 1, 1044253);

            AddCraft(typeof(Sextant), 1044051, 1024183, 40.0, 40.0, typeof(SextantParts), 1044175, 1, 1044253);

            index = AddCraft(typeof(PotionKeg), 1044051, 1044258, 75.0, 75.0, typeof(Keg), 1044255, 1, 1044253);
            AddRes(index, typeof(Bottle), 1044250, 10, 1044253);
            AddRes(index, typeof(BarrelLid), 1044251, 1, 1044253);
            AddRes(index, typeof(BarrelTap), 1044252, 1, 1044253);

            #endregion

            // Set the overridable material
            SetSubRes(typeof(IronIngot), 1044022);

            // Add every material you want the player to be able to choose from
            // This will override the overridable material
            ZhConfig.Resources.Ores.Entries.ToList()
                .ForEach(e => AddSubRes(e.SmeltType, e.Name, e.CraftSkillRequired, 1160300, e.Name));
            
            SetSubRes2(typeof(Log), 1027136);

            LogConfiguration.Entries.ToList()
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
                int trapLevel = (int) (From.Skills.Tinkering.Value / 10);

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