using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Zulu.Engines.Classes;
using Server.Items;
using Server.Mobiles;
using Server.Commands;
using Server.Utilities;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Engines.Craft
{
    public enum ConsumeType
    {
        All,
        Half,
        None
    }

    public interface ICraftable
    {
        private const int Version = 1;
        public Mobile Crafter { get; set; }
        public bool PlayerConstructed { get; set; }

        public static void Serialize(IGenericWriter writer, ICraftable item)
        {
            writer.Write(Version);
            writer.Write(item.PlayerConstructed);

            writer.Write(item.Crafter != null);
            if (item.Crafter != null)
            {
                writer.Write(item.Crafter);
            }
        }

        public static void Deserialize(IGenericReader reader, ICraftable item)
        {
            var version = reader.ReadInt();
            item.PlayerConstructed = reader.ReadBool();

            if (reader.ReadBool())
                item.Crafter = reader.ReadEntity<Mobile>();
        }

        int OnCraft(int mark, double quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes,
            BaseTool tool,
            CraftItem craftItem, int resHue);
    }

    public class CraftItem
    {
        private BeverageType m_RequiredBeverage;

        public bool ForceNonExceptional { get; set; }

        public Expansion RequiredExpansion { get; set; }

        private static Dictionary<Type, int> _itemIds = new Dictionary<Type, int>();

        public static int ItemIDOf(Type type)
        {
            int itemId;

            if (!_itemIds.TryGetValue(type, out itemId))
            {
                if (itemId == 0)
                {
                    object[] attrs = type.GetCustomAttributes(typeof(CraftItemIDAttribute), false);

                    if (attrs.Length > 0)
                    {
                        CraftItemIDAttribute craftItemID = (CraftItemIDAttribute) attrs[0];
                        itemId = craftItemID.ItemID;
                    }
                }

                if (itemId == 0)
                {
                    Item item = null;

                    try
                    {
                        item = Activator.CreateInstance(type) as Item;
                    }
                    catch
                    {
                    }

                    if (item != null)
                    {
                        itemId = item.ItemID;
                        item.Delete();
                    }
                }

                _itemIds[type] = itemId;
            }

            return itemId;
        }

        public CraftItem(Type type, TextDefinition groupName, TextDefinition name)
        {
            Resources = new List<CraftRes>();
            Skills = new List<CraftSkill>();

            ItemType = type;

            GroupNameString = groupName;
            NameString = name;

            GroupNameNumber = groupName;
            NameNumber = name;

            m_RequiredBeverage = BeverageType.Water;
        }

        public BeverageType RequiredBeverage
        {
            get { return m_RequiredBeverage; }
            set { m_RequiredBeverage = value; }
        }

        public void AddRes(Type type, TextDefinition name, int amount)
        {
            AddRes(type, name, amount, "");
        }

        public void AddRes(Type type, TextDefinition name, int amount, TextDefinition message)
        {
            CraftRes craftRes = new CraftRes(type, name, amount, message);
            Resources.Add(craftRes);
        }


        public void AddSkill(SkillName skillToMake, double minSkill, double maxSkill)
        {
            CraftSkill craftSkill = new CraftSkill(skillToMake, minSkill, maxSkill);
            Skills.Add(craftSkill);
        }

        public int Mana { get; set; }

        public int Hits { get; set; }

        public int Stam { get; set; }

        public bool UseSubRes2 { get; set; }

        public bool UseAllRes { get; set; }

        public bool NeedHeat { get; set; }

        public bool NeedOven { get; set; }

        public bool NeedMill { get; set; }

        public Type ItemType { get; }

        public int ItemHue { get; set; }

        public string GroupNameString { get; }

        public int GroupNameNumber { get; }

        public string NameString { get; }

        public int NameNumber { get; }

        public List<CraftRes> Resources { get; }

        public List<CraftSkill> Skills { get; }

        public bool ConsumeAttributes(Mobile from, ref object message, bool consume)
        {
            bool consumMana = false;
            bool consumHits = false;
            bool consumStam = false;

            if (Hits > 0 && from.Hits < Hits)
            {
                message = "You lack the required hit points to make that.";
                return false;
            }
            else
            {
                consumHits = consume;
            }

            if (Mana > 0 && from.Mana < Mana)
            {
                message = "You lack the required mana to make that.";
                return false;
            }
            else
            {
                consumMana = consume;
            }

            if (Stam > 0 && from.Stam < Stam)
            {
                message = "You lack the required stamina to make that.";
                return false;
            }
            else
            {
                consumStam = consume;
            }

            if (consumMana)
                from.Mana -= Mana;

            if (consumHits)
                from.Hits -= Hits;

            if (consumStam)
                from.Stam -= Stam;

            return true;
        }

        #region Tables

        private static int[] m_HeatSources = new[]
        {
            0x461, 0x48E, // Sandstone oven/fireplace
            0x92B, 0x96C, // Stone oven/fireplace
            0xDE3, 0xDE9, // Campfire
            0xFAC, 0xFAC, // Firepit
            0x184A, 0x184C, // Heating stand (left)
            0x184E, 0x1850, // Heating stand (right)
            0x398C, 0x399F, // Fire field
            0x2DDB, 0x2DDC, //Elven stove
            0x19AA, 0x19BB, // Veteran Reward Brazier
            0x197A, 0x19A9, // Large Forge 
            0x0FB1, 0x0FB1, // Small Forge
            0x2DD8, 0x2DD8 // Elven Forge
        };

        private static int[] m_Ovens = new[]
        {
            0x461, 0x46F, // Sandstone oven
            0x92B, 0x93F, // Stone oven
            0x2DDB, 0x2DDC //Elven stove
        };

        private static int[] m_Mills = new[]
        {
            0x1920, 0x1921, 0x1922, 0x1923, 0x1924, 0x1295, 0x1926, 0x1928,
            0x192C, 0x192D, 0x192E, 0x129F, 0x1930, 0x1931, 0x1932, 0x1934
        };

        private static Type[][] m_TypesTable = new[]
        {
            new[] {typeof(Log), typeof(Log)},
            new[] {typeof(Leather), typeof(Hides)},
            new[] {typeof(BlankMap), typeof(BlankScroll)},
            new[] {typeof(Cloth), typeof(UncutCloth)},
            new[] {typeof(CheeseWheel), typeof(CheeseWedge)},
            new[] {typeof(Pumpkin), typeof(SmallPumpkin)},
            new[] {typeof(WoodenBowlOfPeas), typeof(PewterBowlOfPeas)}
        };

        private static Type[] m_ColoredItemTable = new[]
        {
            typeof(BaseWeapon), typeof(BaseArmor), typeof(BaseClothing),
            typeof(BaseJewel)
        };

        private static Type[] m_ColoredResourceTable = new[]
        {
            typeof(BaseIngot), typeof(BaseOre),
            typeof(BaseLeather), typeof(BaseHides),
            typeof(UncutCloth), typeof(Cloth)
        };

        private static Type[] m_MarkableTable = new[]
        {
            typeof(BaseArmor),
            typeof(BaseWeapon),
            typeof(BaseClothing),
            typeof(BaseInstrument),
            typeof(BaseTool),
            typeof(BaseHarvestTool),
            typeof(Spellbook), typeof(Runebook)
        };

        private static Type[] m_NeverColorTable = new[]
        {
            typeof(OrcHelm)
        };

        #endregion

        public bool IsMarkable(Type type)
        {
            if (ForceNonExceptional) //Don't even display the stuff for marking if it can't ever be exceptional.
                return false;

            for (int i = 0; i < m_MarkableTable.Length; ++i)
            {
                if (type == m_MarkableTable[i] || type.IsSubclassOf(m_MarkableTable[i]))
                    return true;
            }

            return false;
        }

        public static bool RetainsColor(Type type)
        {
            bool neverColor = false;

            for (int i = 0; !neverColor && i < m_NeverColorTable.Length; ++i)
                neverColor = type == m_NeverColorTable[i] || type.IsSubclassOf(m_NeverColorTable[i]);

            if (neverColor)
                return false;

            bool inItemTable = false;

            for (int i = 0; !inItemTable && i < m_ColoredItemTable.Length; ++i)
                inItemTable = type == m_ColoredItemTable[i] || type.IsSubclassOf(m_ColoredItemTable[i]);

            return inItemTable;
        }

        public bool RetainsColorFrom(CraftSystem system, Type type)
        {
            if (system.RetainsColorFrom(this, type))
                return true;

            bool inItemTable = RetainsColor(ItemType);

            if (!inItemTable)
                return false;

            bool inResourceTable = false;

            for (int i = 0; !inResourceTable && i < m_ColoredResourceTable.Length; ++i)
                inResourceTable = type == m_ColoredResourceTable[i] || type.IsSubclassOf(m_ColoredResourceTable[i]);

            return inResourceTable;
        }

        public bool Find(Mobile from, int[] itemIDs)
        {
            Map map = from.Map;

            if (map == null)
                return false;

            IPooledEnumerable eable = map.GetItemsInRange(from.Location, 2);

            foreach (Item item in eable)
            {
                if (item.Z + 16 > from.Z && from.Z + 16 > item.Z && Find(item.ItemID, itemIDs))
                {
                    eable.Free();
                    return true;
                }
            }

            eable.Free();

            for (int x = -2; x <= 2; ++x)
            {
                for (int y = -2; y <= 2; ++y)
                {
                    int vx = from.X + x;
                    int vy = from.Y + y;

                    StaticTile[] tiles = map.Tiles.GetStaticTiles(vx, vy, true);

                    for (int i = 0; i < tiles.Length; ++i)
                    {
                        int z = tiles[i].Z;
                        int id = tiles[i].ID;

                        if (z + 16 > from.Z && from.Z + 16 > z && Find(id, itemIDs))
                            return true;
                    }
                }
            }

            return false;
        }

        public bool Find(int itemID, int[] itemIDs)
        {
            bool contains = false;

            for (int i = 0; !contains && i < itemIDs.Length; i += 2)
                contains = itemID >= itemIDs[i] && itemID <= itemIDs[i + 1];

            return contains;
        }

        public bool IsQuantityType(Type[][] types)
        {
            for (int i = 0; i < types.Length; ++i)
            {
                Type[] check = types[i];

                for (int j = 0; j < check.Length; ++j)
                {
                    if (typeof(IHasQuantity).IsAssignableFrom(check[j]))
                        return true;
                }
            }

            return false;
        }

        public int ConsumeQuantity(Container cont, Type[][] types, int[] amounts)
        {
            if (types.Length != amounts.Length)
                throw new ArgumentException();

            Item[][] items = new Item[types.Length][];
            int[] totals = new int[types.Length];

            for (int i = 0; i < types.Length; ++i)
            {
                items[i] = cont.FindItemsByType(types[i], true);

                for (int j = 0; j < items[i].Length; ++j)
                {
                    IHasQuantity hq = items[i][j] as IHasQuantity;

                    if (hq == null)
                    {
                        totals[i] += items[i][j].Amount;
                    }
                    else
                    {
                        if (hq is BaseBeverage && ((BaseBeverage) hq).Content != m_RequiredBeverage)
                            continue;

                        totals[i] += hq.Quantity;
                    }
                }

                if (totals[i] < amounts[i])
                    return i;
            }

            for (int i = 0; i < types.Length; ++i)
            {
                int need = amounts[i];

                for (int j = 0; j < items[i].Length; ++j)
                {
                    Item item = items[i][j];
                    IHasQuantity hq = item as IHasQuantity;

                    if (hq == null)
                    {
                        int theirAmount = item.Amount;

                        if (theirAmount < need)
                        {
                            item.Delete();
                            need -= theirAmount;
                        }
                        else
                        {
                            item.Consume(need);
                            break;
                        }
                    }
                    else
                    {
                        if (hq is BaseBeverage && ((BaseBeverage) hq).Content != m_RequiredBeverage)
                            continue;

                        int theirAmount = hq.Quantity;

                        if (theirAmount < need)
                        {
                            hq.Quantity -= theirAmount;
                            need -= theirAmount;
                        }
                        else
                        {
                            hq.Quantity -= need;
                            break;
                        }
                    }
                }
            }

            return -1;
        }

        public int GetQuantity(Container cont, Type[] types)
        {
            Item[] items = cont.FindItemsByType(types, true);

            int amount = 0;

            for (int i = 0; i < items.Length; ++i)
            {
                IHasQuantity hq = items[i] as IHasQuantity;

                if (hq == null)
                {
                    amount += items[i].Amount;
                }
                else
                {
                    if (hq is BaseBeverage && ((BaseBeverage) hq).Content != m_RequiredBeverage)
                        continue;

                    amount += hq.Quantity;
                }
            }

            return amount;
        }

        public bool ConsumeRes(Mobile from, Type typeRes, CraftSystem craftSystem, ref int resHue, ref int maxAmount,
            ConsumeType consumeType, ref object message)
        {
            return ConsumeRes(from, typeRes, craftSystem, ref resHue, ref maxAmount, consumeType, ref message, false);
        }

        public bool ConsumeRes(Mobile from, Type typeRes, CraftSystem craftSystem, ref int resHue, ref int maxAmount,
            ConsumeType consumeType, ref object message, bool isFailure)
        {
            Container ourPack = from.Backpack;

            if (ourPack == null)
                return false;

            if (NeedHeat && !Find(from, m_HeatSources))
            {
                message = 1044487; // You must be near a fire source to cook.
                return false;
            }

            if (NeedOven && !Find(from, m_Ovens))
            {
                message = 1044493; // You must be near an oven to bake that.
                return false;
            }

            if (NeedMill && !Find(from, m_Mills))
            {
                message = 1044491; // You must be near a flour mill to do that.
                return false;
            }

            Type[][] types = new Type[Resources.Count][];
            int[] amounts = new int[Resources.Count];

            maxAmount = int.MaxValue;

            CraftSubResCol resCol = UseSubRes2 ? craftSystem.CraftSubRes2 : craftSystem.CraftSubRes;

            for (int i = 0; i < types.Length; ++i)
            {
                CraftRes craftRes = Resources[i];
                Type baseType = craftRes.ItemType;

                // Resource Mutation
                if (baseType == resCol.ResType && typeRes != null)
                {
                    baseType = typeRes;

                    CraftSubRes subResource = resCol.SearchFor(baseType);

                    if (subResource != null && from.Skills[craftSystem.MainSkill].Base < subResource.RequiredSkill)
                    {
                        message = subResource.Message;
                        return false;
                    }
                }
                // ******************

                for (int j = 0; types[i] == null && j < m_TypesTable.Length; ++j)
                {
                    if (m_TypesTable[j][0] == baseType)
                        types[i] = m_TypesTable[j];
                }

                if (types[i] == null)
                    types[i] = new[] {baseType};

                amounts[i] = craftRes.Amount;

                // For stackable items that can ben crafted more than one at a time
                if (UseAllRes)
                {
                    int tempAmount = ourPack.GetAmount(types[i]);
                    tempAmount /= amounts[i];
                    if (tempAmount < maxAmount)
                    {
                        maxAmount = tempAmount;

                        if (maxAmount == 0)
                        {
                            CraftRes res = Resources[i];

                            if (res.MessageNumber > 0)
                                message = res.MessageNumber;
                            else if (!String.IsNullOrEmpty(res.MessageString))
                                message = res.MessageString;
                            else
                                message = 502925; // You don't have the resources required to make that item.

                            return false;
                        }
                    }
                }
                // ****************************

                if (isFailure && !craftSystem.ConsumeOnFailure(from, types[i][0], this))
                    amounts[i] = 0;
            }

            // We adjust the amount of each resource to consume the max posible
            if (UseAllRes)
            {
                for (int i = 0; i < amounts.Length; ++i)
                    amounts[i] *= maxAmount;
            }
            else
                maxAmount = -1;

            Item consumeExtra = null;

            if (NameNumber == 1041267)
            {
                // Runebooks are a special case, they need a blank recall rune

                List<RecallRune> runes = ourPack.FindItemsByType<RecallRune>();

                for (int i = 0; i < runes.Count; ++i)
                {
                    RecallRune rune = runes[i];

                    if (rune != null && !rune.Marked)
                    {
                        consumeExtra = rune;
                        break;
                    }
                }

                if (consumeExtra == null)
                {
                    message = 1044253; // You don't have the components needed to make that.
                    return false;
                }
            }

            int index = 0;

            // Consume ALL
            if (consumeType == ConsumeType.All)
            {
                m_ResHue = 0;
                m_ResAmount = 0;
                m_System = craftSystem;

                if (IsQuantityType(types))
                    index = ConsumeQuantity(ourPack, types, amounts);
                else
                    index = ourPack.ConsumeTotalGrouped(types, amounts, true, OnResourceConsumed, CheckHueGrouping);

                resHue = m_ResHue;
            }

            // Consume Half ( for use all resource craft type )
            else if (consumeType == ConsumeType.Half)
            {
                for (int i = 0; i < amounts.Length; i++)
                {
                    amounts[i] /= 2;

                    if (amounts[i] < 1)
                        amounts[i] = 1;
                }

                m_ResHue = 0;
                m_ResAmount = 0;
                m_System = craftSystem;

                if (IsQuantityType(types))
                    index = ConsumeQuantity(ourPack, types, amounts);
                else
                    index = ourPack.ConsumeTotalGrouped(types, amounts, true, OnResourceConsumed, CheckHueGrouping);

                resHue = m_ResHue;
            }

            else // ConstumeType.None ( it's basicaly used to know if the crafter has enough resource before starting the process )
            {
                index = -1;

                if (IsQuantityType(types))
                {
                    for (int i = 0; i < types.Length; i++)
                    {
                        if (GetQuantity(ourPack, types[i]) < amounts[i])
                        {
                            index = i;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < types.Length; i++)
                    {
                        if (ourPack.GetBestGroupAmount(types[i], true, CheckHueGrouping) < amounts[i])
                        {
                            index = i;
                            break;
                        }
                    }
                }
            }

            if (index == -1)
            {
                if (consumeType != ConsumeType.None)
                    if (consumeExtra != null)
                        consumeExtra.Delete();

                return true;
            }
            else
            {
                CraftRes res = Resources[index];

                if (res.MessageNumber > 0)
                    message = res.MessageNumber;
                else if (res.MessageString != null && res.MessageString != String.Empty)
                    message = res.MessageString;
                else
                    message = 502925; // You don't have the resources required to make that item.

                return false;
            }
        }

        private int m_ResHue;
        private int m_ResAmount;
        private CraftSystem m_System;

        private void OnResourceConsumed(Item item, int amount)
        {
            if (!RetainsColorFrom(m_System, item.GetType()))
                return;

            if (amount >= m_ResAmount)
            {
                m_ResHue = item.Hue;
                m_ResAmount = amount;
            }
        }

        private int CheckHueGrouping(Item a, Item b)
        {
            return b.Hue.CompareTo(a.Hue);
        }

        public double GetExceptionalChance(Mobile from, CraftSystem system, ref int exceptionalDifficulty)
        {
            if (ForceNonExceptional)
                return 0.0;

            var innerExceptionalDifficulty = exceptionalDifficulty;

            var exceptionalChance = 10.0;

            from.FireHook(h => h.OnExceptionalChance(from, ref exceptionalChance, ref innerExceptionalDifficulty));

            if (innerExceptionalDifficulty < 90)
                innerExceptionalDifficulty = 90;

            exceptionalDifficulty = innerExceptionalDifficulty;

            return exceptionalChance;
        }

        public bool CheckSkills(Mobile from, Type typeRes, CraftSystem craftSystem, ref int mark, ref double quality,
            ref bool allRequiredSkills)
        {
            return CheckSkills(from, typeRes, craftSystem, ref mark, ref quality, ref allRequiredSkills, true);
        }

        public bool CheckSkills(Mobile from, Type typeRes, CraftSystem craftSystem, ref int mark, ref double quality,
            ref bool allRequiredSkills, bool gainSkills)
        {
            var craftSkillRequired = GetCraftSkillRequired(from, typeRes, craftSystem);
            var exceptionalDifficulty = craftSkillRequired;
            var points = gainSkills ? GetCraftPoints(from, typeRes, craftSystem) : 0;
            var exceptionalChance = GetExceptionalChance(from, craftSystem, ref exceptionalDifficulty);
            var resource = CraftResources.GetFromType(typeRes);
            var resQuality = CraftResources.GetQuality(resource);

            quality = resQuality;

            if (exceptionalChance > Utility.RandomDouble() &&
                from.ShilCheckSkill(craftSystem.MainSkill, exceptionalDifficulty, 0))
            {
                mark = 2;

                quality = (double) (int) (quality * GetQualityBonus(from)) / 100;
            }

            return from.ShilCheckSkill(craftSystem.MainSkill, craftSkillRequired, points);
        }

        public int GetQualityBonus(Mobile from)
        {
            var armsLoreValue = from.Skills[SkillName.ArmsLore].Value;
            var multiplier = 5 + (int) (armsLoreValue / 10);

            from.FireHook(h => h.OnQualityBonus(from, ref multiplier));

            multiplier += 100;

            return multiplier;
        }

        public int GetCraftItemSkillRequired(Mobile from, CraftSystem craftSystem)
        {
            var craftSkill = Skills.FirstOrDefault(sk => sk.SkillToMake == craftSystem.MainSkill);
            var maxSkill = (int) (craftSkill?.MaxSkill ?? 0);
            return maxSkill;
        }

        public int GetCraftSkillRequired(Mobile from, Type typeRes, CraftSystem craftSystem)
        {
            var itemSkillRequired = GetCraftItemSkillRequired(from, craftSystem);
            var craftResourceType = typeRes != null ? typeRes : Resources[0].ItemType;
            var craftSkillRequired = craftSystem.GetCraftSkillRequired(itemSkillRequired, craftResourceType);

            if (craftSkillRequired < 1)
                craftSkillRequired = 1;
            else if (craftSkillRequired > 140)
                craftSkillRequired = 140;

            return craftSkillRequired;
        }

        public int GetCraftPoints(Mobile from, Type typeRes, CraftSystem craftSystem)
        {
            var itemSkillRequired = GetCraftItemSkillRequired(from, craftSystem);
            var points = craftSystem.GetCraftPoints(itemSkillRequired, Resources[0].Amount);

            return points;
        }

        public double GetSuccessChance(Mobile from, Type typeRes, CraftSystem craftSystem, bool gainSkills,
            ref bool allRequiredSkills)
        {
            var craftSkillRequired = GetCraftSkillRequired(from, typeRes, craftSystem);
            var chance = SkillCheck.GetSkillCheckChance(from, craftSystem.MainSkill, craftSkillRequired);

            return (double) chance / 100;
        }

        public void Craft(Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, ref bool success)
        {
            if (from.BeginAction(typeof(CraftSystem)))
            {
                bool allRequiredSkills = true;
                double chance = GetSuccessChance(from, typeRes, craftSystem, false, ref allRequiredSkills);

                if (allRequiredSkills && chance >= 0.0)
                {
                    var badCraft = craftSystem.CanCraft(from, tool, ItemType);

                    if (badCraft <= 0)
                    {
                        int resHue = 0;
                        int maxAmount = 0;
                        object message = null;

                        if (ConsumeRes(from, typeRes, craftSystem, ref resHue, ref maxAmount, ConsumeType.None,
                            ref message))
                        {
                            message = null;

                            if (ConsumeAttributes(from, ref message, false))
                            {
                                CraftContext context = craftSystem.GetContext(from);

                                context?.OnMade(this);

                                int iMin = craftSystem.MinCraftEffect;
                                int iMax = craftSystem.MaxCraftEffect - iMin + 1;
                                int iRandom = Utility.Random(iMax);
                                iRandom += iMin + 1;
                                success = true;

                                tool.OnBeginCraft(from, this, craftSystem);
                                new InternalTimer(from, craftSystem, this, typeRes, tool, iRandom).Start();
                            }
                            else
                            {
                                success = false;
                                from.EndAction(typeof(CraftSystem));
                                from.SendGump(new CraftGump(from, craftSystem, tool, message));
                            }
                        }
                        else
                        {
                            success = false;
                            from.EndAction(typeof(CraftSystem));
                            from.SendGump(new CraftGump(from, craftSystem, tool, message));
                        }
                    }
                    else
                    {
                        success = false;
                        from.EndAction(typeof(CraftSystem));
                        from.SendGump(new CraftGump(from, craftSystem, tool, badCraft));
                    }
                }
                else
                {
                    success = false;
                    from.EndAction(typeof(CraftSystem));
                    // You don't have the required skills to attempt this item.
                    from.SendGump(new CraftGump(from, craftSystem, tool, 1044153));
                }
            }
            else
            {
                success = false;
                from.SendLocalizedMessage(500119); // You must wait to perform another action
            }
        }

        //Eventually convert to TextDefinition, but that requires that we convert all the gumps to ues it too.  Not that it wouldn't be a bad idea.
        private object RequiredExpansionMessage(Expansion expansion)
        {
            return expansion switch
            {
                Expansion.SE => 1063307 // The "Samurai Empire" expansion is required to attempt this item.
                ,
                Expansion.ML => 1072650 // The "Mondain's Legacy" expansion is required to attempt this item.
                ,
                _ => $"The \"{ExpansionInfo.GetInfo(expansion).Name}\" expansion is required to attempt this item."
            };
        }

        public void CompleteCraft(int mark, double quality, bool makersMark, Mobile from, CraftSystem craftSystem,
            Type typeRes,
            BaseTool tool, CustomCraft customCraft)
        {
            int badCraft = craftSystem.CanCraft(from, tool, ItemType);

            if (badCraft > 0)
            {
                if (tool != null && !tool.Deleted && tool.UsesRemaining > 0)
                    from.SendGump(new CraftGump(from, craftSystem, tool, badCraft));
                else
                    from.SendLocalizedMessage(badCraft);

                return;
            }

            int checkResHue = 0, checkMaxAmount = 0;
            object checkMessage = null;

            // Not enough resource to craft it
            if (!ConsumeRes(from, typeRes, craftSystem, ref checkResHue, ref checkMaxAmount, ConsumeType.None,
                ref checkMessage))
            {
                if (tool != null && !tool.Deleted && tool.UsesRemaining > 0)
                    from.SendGump(new CraftGump(from, craftSystem, tool, checkMessage));
                else if (checkMessage is int && (int) checkMessage > 0)
                    from.SendLocalizedMessage((int) checkMessage);
                else if (checkMessage is string)
                    from.SendMessage((string) checkMessage);

                return;
            }
            else if (!ConsumeAttributes(from, ref checkMessage, false))
            {
                if (tool != null && !tool.Deleted && tool.UsesRemaining > 0)
                    from.SendGump(new CraftGump(from, craftSystem, tool, checkMessage));
                else if (checkMessage is int && (int) checkMessage > 0)
                    from.SendLocalizedMessage((int) checkMessage);
                else if (checkMessage is string)
                    from.SendMessage((string) checkMessage);

                return;
            }

            var toolBroken = false;

            var ignored1 = 1;
            var ignored2 = 1.0;
            var endmark = 1;

            var allRequiredSkills = true;

            if (CheckSkills(from, typeRes, craftSystem, ref ignored1, ref ignored2, ref allRequiredSkills))
            {
                // Resource
                int resHue = 0;
                int maxAmount = 0;

                object message = null;

                // Not enough resource to craft it
                if (!ConsumeRes(from, typeRes, craftSystem, ref resHue, ref maxAmount, ConsumeType.All, ref message))
                {
                    if (tool != null && !tool.Deleted && tool.UsesRemaining > 0)
                        from.SendGump(new CraftGump(from, craftSystem, tool, message));
                    else if (message is int i && i > 0)
                        from.SendLocalizedMessage(i);
                    else if (message is string s)
                        from.SendMessage(s);

                    return;
                }
                else if (!ConsumeAttributes(from, ref message, true))
                {
                    if (tool != null && !tool.Deleted && tool.UsesRemaining > 0)
                        from.SendGump(new CraftGump(from, craftSystem, tool, message));
                    else if (message is int i && i > 0)
                        from.SendLocalizedMessage(i);
                    else if (message is string s)
                        from.SendMessage(s);

                    return;
                }

                tool.UsesRemaining--;

                if (tool.UsesRemaining < 1 && tool.BreakOnDepletion)
                    toolBroken = true;

                if (toolBroken)
                    tool.Delete();

                int num = 0;

                Item item;
                if (customCraft != null)
                {
                    item = customCraft.CompleteCraft(out num);
                }
                else if (typeof(MapItem).IsAssignableFrom(ItemType) && from.Map != Map.Felucca)
                {
                    item = new IndecipherableMap();
                    from.SendLocalizedMessage(1070800); // The map you create becomes mysteriously indecipherable.
                }
                else
                {
                    item = Activator.CreateInstance(ItemType) as Item;
                }

                if (item != null)
                {
                    from.FireHook(h => h.OnCraftItemCreated(from, craftSystem, this, tool, item));

                    if (item is ICraftable craftable)
                    {
                        endmark = craftable.OnCraft(mark, quality, makersMark, from, craftSystem, typeRes, tool,
                            this, resHue);

                        craftable.PlayerConstructed = true;
                        craftable.Crafter = from;
                    }
                    else if (item.Hue == 0)
                        item.Hue = resHue;

                    if (maxAmount > 0)
                    {
                        if (!item.Stackable && item is IUsesRemaining remaining)
                            remaining.UsesRemaining *= maxAmount;
                        else
                            item.Amount = maxAmount;
                    }

                    from.AddToBackpack(item);
                    from.FireHook(h => h.OnCraftItemAddToBackpack(from, craftSystem, this, tool, item));

                    if (from.AccessLevel > AccessLevel.Player)
                    {
                        CommandLogging.WriteLine(
                            from,
                            $"Crafting {CommandLogging.Format(item)} with craft system {craftSystem.GetType()}"
                        );
                    }

                    //from.PlaySound( 0x57 );
                }

                if (num == 0)
                    num = craftSystem.PlayEndingEffect(from, false, true, toolBroken, endmark, makersMark, this);

                if (tool != null && !tool.Deleted && tool.UsesRemaining > 0)
                {
                    from.SendGump(new CraftGump(from, craftSystem, tool, num));
                }
                else if (num > 0)
                    from.SendLocalizedMessage(num);
            }
            else if (!allRequiredSkills)
            {
                if (tool != null && !tool.Deleted && tool.UsesRemaining > 0)
                    from.SendGump(new CraftGump(from, craftSystem, tool, 1044153));
                else
                    from.SendLocalizedMessage(1044153); // You don't have the required skills to attempt this item.
            }
            else
            {
                ConsumeType consumeType = UseAllRes ? ConsumeType.Half : ConsumeType.All;
                int resHue = 0;
                int maxAmount = 0;

                object message = null;

                // Not enough resource to craft it
                if (!ConsumeRes(from, typeRes, craftSystem, ref resHue, ref maxAmount, consumeType, ref message, true))
                {
                    if (tool != null && !tool.Deleted && tool.UsesRemaining > 0)
                        from.SendGump(new CraftGump(from, craftSystem, tool, message));
                    else if (message is int cliloc && cliloc > 0)
                        from.SendLocalizedMessage(cliloc);
                    else if (message is string msgString)
                        from.SendMessage(msgString);

                    return;
                }

                tool.UsesRemaining--;

                if (tool.UsesRemaining < 1 && tool.BreakOnDepletion)
                    toolBroken = true;

                if (toolBroken)
                    tool.Delete();

                // SkillCheck failed.
                int num = craftSystem.PlayEndingEffect(from, true, true, toolBroken, endmark, false, this);

                if (tool != null && !tool.Deleted && tool.UsesRemaining > 0)
                {
                    from.SendGump(new CraftGump(from, craftSystem, tool, num));
                }
                else if (num > 0)
                    from.SendLocalizedMessage(num);
            }

            tool?.OnEndCraft(from, this, craftSystem);
        }

        private class InternalTimer : Timer
        {
            private Mobile m_From;
            private int m_iCount;
            private int m_iCountMax;
            private CraftItem m_CraftItem;
            private CraftSystem m_CraftSystem;
            private Type m_TypeRes;
            private BaseTool m_Tool;

            public InternalTimer(Mobile from, CraftSystem craftSystem, CraftItem craftItem, Type typeRes, BaseTool tool,
                int iCountMax) : base(TimeSpan.Zero, TimeSpan.FromSeconds(craftSystem.Delay), iCountMax)
            {
                m_From = from;
                m_CraftItem = craftItem;
                m_iCount = 0;
                m_iCountMax = iCountMax;
                m_CraftSystem = craftSystem;
                m_TypeRes = typeRes;
                m_Tool = tool;
            }

            protected override void OnTick()
            {
                m_iCount++;

                m_From.DisruptiveAction();

                if (m_iCount < m_iCountMax)
                {
                    m_CraftSystem.PlayCraftEffect(m_From);
                }
                else
                {
                    m_From.EndAction(typeof(CraftSystem));

                    var badCraft = m_CraftSystem.CanCraft(m_From, m_Tool, m_CraftItem.ItemType);

                    if (badCraft > 0)
                    {
                        if (m_Tool != null && !m_Tool.Deleted && m_Tool.UsesRemaining > 0)
                            m_From.SendGump(new CraftGump(m_From, m_CraftSystem, m_Tool, badCraft));
                        else
                            m_From.SendLocalizedMessage(badCraft);

                        return;
                    }

                    var mark = 1;
                    var quality = 1.0;
                    var allRequiredSkills = true;

                    CraftContext context = m_CraftSystem.GetContext(m_From);

                    m_CraftItem.CheckSkills(
                        m_From,
                        m_TypeRes,
                        m_CraftSystem,
                        ref mark,
                        ref quality,
                        ref allRequiredSkills,
                        false
                    );

                    if (context == null)
                        return;

                    if (typeof(CustomCraft).IsAssignableFrom(m_CraftItem.ItemType))
                    {
                        // TODO: CustomCrafts, i.e. traps don't seem to be able to fail in RunUO?
                        var cc = m_CraftItem.ItemType.CreateInstance<CustomCraft>(
                            m_CraftItem.ItemType,
                            m_From,
                            m_CraftItem,
                            m_CraftSystem,
                            m_TypeRes,
                            m_Tool,
                            mark
                        );

                        cc?.EndCraftAction();

                        return;
                    }

                    bool makersMark = false;

                    if (mark == 2 && m_From.Skills[m_CraftSystem.MainSkill].Base >= 100.0)
                        makersMark = m_CraftItem.IsMarkable(m_CraftItem.ItemType);

                    if (makersMark && context.MarkOption == CraftMarkOption.PromptForMark)
                    {
                        m_From.SendGump(new QueryMakersMarkGump(mark, quality, m_From, m_CraftItem, m_CraftSystem,
                            m_TypeRes,
                            m_Tool));
                    }
                    else
                    {
                        if (context.MarkOption == CraftMarkOption.DoNotMark)
                            makersMark = false;

                        m_CraftItem.CompleteCraft(mark, quality, makersMark, m_From, m_CraftSystem, m_TypeRes, m_Tool,
                            null);
                    }
                }
            }
        }
    }
}