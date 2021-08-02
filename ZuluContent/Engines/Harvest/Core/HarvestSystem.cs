using System;
using System.Collections.Generic;
using Scripts.Zulu.Engines.Classes;
using Server.Items;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;

namespace Server.Engines.Harvest
{
    public abstract class HarvestSystem
    {
        private List<HarvestDefinition> m_Definitions;

        public List<HarvestDefinition> Definitions
        {
            get { return m_Definitions; }
        }

        public HarvestSystem()
        {
            m_Definitions = new List<HarvestDefinition>();
        }

        public virtual bool CheckTool(Mobile from, Item tool)
        {
            var wornOut = tool == null || tool.Deleted ||
                          tool is IUsesRemaining && ((IUsesRemaining) tool).UsesRemaining <= 0;
            var checkEquip = !(tool is Shovel);
            var equipped = tool?.Parent == from;

            if (wornOut)
                from.SendLocalizedMessage(1044038); // You have worn out your tool!

            if (checkEquip && !equipped)
                from.SendLocalizedMessage(502641); // You must equip this item to use it.

            return wornOut || !checkEquip || equipped;
        }

        public virtual bool CheckHarvest(Mobile from, Item tool)
        {
            return CheckTool(from, tool);
        }

        public virtual bool CheckHarvest(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            return CheckTool(from, tool);
        }

        public virtual bool CheckRange(Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, bool timed)
        {
            bool inRange = @from.Map == map && @from.InRange(loc, def.MaxRange);

            if (!inRange)
                def.SendMessageTo(from, timed ? def.TimedOutOfRangeMessage : def.OutOfRangeMessage);

            return inRange;
        }

        public virtual bool CheckResources(Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc,
            bool timed)
        {
            HarvestBank bank = def.GetBank(map, loc.X, loc.Y);
            bool available = bank != null && bank.Current >= GetConsumeAmount(from, def);

            if (!available)
                def.SendMessageTo(from, timed ? def.DoubleHarvestMessage : def.NoResourcesMessage);

            return available;
        }

        public virtual int GetConsumeAmount(Mobile from, HarvestDefinition def)
        {
            var skillValue = from.Skills[def.Skill].Value;
            return def.ConsumedPerHarvest(skillValue);
        }

        public virtual int GetChanceForColored(Mobile from, Item tool, HarvestDefinition def)
        {
            var skillValue = from.Skills[def.Skill].Value;
            var chanceForColored = (int) (skillValue / 5) + 35;

            if (tool is IEnchanted enchantedTool && enchantedTool.Enchantments.Get((HarvestBonus e) => e.Value) > 0)
            {
                var toolBonusChanceForColored = 10;
                from.FireHook(h => h.OnToolHarvestBonus(from, ref toolBonusChanceForColored));
                chanceForColored += toolBonusChanceForColored;
            }

            if (chanceForColored > 75)
                chanceForColored = 75;

            from.FireHook(h => h.OnHarvestColoredChance(from, ref chanceForColored));

            return chanceForColored;
        }

        public virtual void OnBadHarvestTarget(Mobile from, Item tool, object toHarvest)
        {
        }

        public virtual object GetLock(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            /* Here we prevent multiple harvesting.
             * 
             * Some options:
             *  - 'return tool;' : This will allow the player to harvest more than once concurrently, but only if they use multiple tools. This seems to be as OSI.
             *  - 'return GetType();' : This will disallow multiple harvesting of the same type. That is, we couldn't mine more than once concurrently, but we could be both mining and lumberjacking.
             *  - 'return typeof( HarvestSystem );' : This will completely restrict concurrent harvesting.
             */

            return typeof(HarvestSystem);
        }

        public virtual void OnConcurrentHarvest(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
        }

        public virtual void OnHarvestStarted(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
        }

        public virtual bool BeginHarvesting(Mobile from, Item tool)
        {
            if (!CheckHarvest(from, tool))
                return false;

            from.Target = new HarvestTarget(tool, this);
            return true;
        }

        public virtual async void FinishHarvesting(Mobile from, Item tool, HarvestDefinition def, object toHarvest,
            object locked)
        {
            from.EndAction(locked);

            if (!CheckHarvest(from, tool))
                return;

            int tileID;
            Map map;
            Point3D loc;

            if (!GetHarvestDetails(from, tool, toHarvest, out tileID, out map, out loc))
            {
                OnBadHarvestTarget(from, tool, toHarvest);
                return;
            }
            else if (!def.Validate(tileID))
            {
                OnBadHarvestTarget(from, tool, toHarvest);
                return;
            }

            if (!CheckRange(from, tool, def, map, loc, true))
                return;
            else if (!CheckResources(from, tool, def, map, loc, true))
                return;
            else if (!CheckHarvest(from, tool, def, toHarvest))
                return;

            HarvestBank bank = def.GetBank(map, loc.X, loc.Y);

            if (bank == null)
                return;

            Type type = null;

            var consumeAmount = GetConsumeAmount(from, def);

            bank.Consume(consumeAmount, from);

            if (from.ShilCheckSkill(def.Skill))
            {
                var chanceForColored = 0;
                var harvestAmount = consumeAmount;
                HarvestVein vein;

                if (def.Resources?.Length > 0)
                    chanceForColored = GetChanceForColored(from, tool, def);

                from.FireHook(h => h.OnToolHarvestBonus(from, ref harvestAmount));

                if (chanceForColored > 0 && Utility.Random(1, 100) <= chanceForColored)
                {
                    harvestAmount = def.ModifyHarvestAmount(from, tool, harvestAmount);

                    vein = def.GetColoredVein(from, tool, ref harvestAmount);
                    if (vein == null)
                    {
                        var veinName = def.Skill == SkillName.Lumberjacking ? "logs" : "ores";
                        def.SendMessageTo(from, $"You fail to find any colored {veinName}.");
                        return;
                    }
                }
                else
                {
                    vein = def.DefaultVein;
                    from.FireHook(h => h.OnHarvestAmount(from, ref harvestAmount));
                }

                var resource = vein.Resource;

                type = GetResourceType(from, tool, def, map, loc, resource);

                if (type != null)
                    type = MutateType(type, from, tool, def, map, loc, resource);

                if (type != null)
                {
                    Item item = Construct(type, from);

                    if (item == null)
                    {
                        type = null;
                    }
                    else
                    {
                        if (item.Stackable)
                        {
                            item.Amount = harvestAmount;
                        }

                        if (Give(from, item, def.PlaceAtFeetIfFull))
                        {
                            SendSuccessTo(from, item, resource);
                        }
                        else
                        {
                            SendPackFullTo(from, item, def, resource);
                            item.Delete();
                        }
                    }
                }
            }

            if (type == null)
                def.SendMessageTo(from, def.FailMessage);

            await Timer.Pause(1000);

            if (type != null)
                def.BonusEffect?.Invoke(from, tool);

            OnHarvestFinished(from, tool, def, bank, toHarvest);
        }

        public virtual async void OnHarvestFinished(Mobile from, Item tool, HarvestDefinition def,
            HarvestBank bank, object harvested)
        {
            // Loop continuously until the player moves or their tool breaks
            await Timer.Pause(TimeSpan.FromSeconds(ZhConfig.Crafting.AutoLoop.Delay));

            StartHarvesting(from, tool, harvested);
        }

        public virtual Item Construct(Type type, Mobile from)
        {
            try
            {
                return Activator.CreateInstance(type) as Item;
            }
            catch
            {
                return null;
            }
        }

        public virtual void SendSuccessTo(Mobile from, Item item, HarvestResource resource)
        {
            resource.SendSuccessTo(from, item.Amount);
        }

        public virtual void SendPackFullTo(Mobile from, Item item, HarvestDefinition def, HarvestResource resource)
        {
            def.SendMessageTo(from, def.PackFullMessage);
        }

        public virtual bool Give(Mobile m, Item item, bool placeAtFeet)
        {
            if (m.PlaceInBackpack(item))
                return true;

            if (!placeAtFeet)
                return false;

            Map map = m.Map;

            if (map == null)
                return false;

            List<Item> atFeet = new List<Item>();

            foreach (Item obj in m.GetItemsInRange(0))
                atFeet.Add(obj);

            for (int i = 0; i < atFeet.Count; ++i)
            {
                Item check = atFeet[i];

                if (check.StackWith(m, item, false))
                    return true;
            }

            item.MoveToWorld(m.Location, map);
            return true;
        }

        public virtual Type MutateType(Type type, Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc,
            HarvestResource resource)
        {
            return from.Region.GetResource(type);
        }

        public virtual Type GetResourceType(Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc,
            HarvestResource resource)
        {
            if (resource.Types.Length > 0)
                return resource.Types[Utility.Random(resource.Types.Length)];

            return null;
        }

        public virtual bool OnHarvesting(Mobile from, Item tool, HarvestDefinition def, object toHarvest, object locked,
            bool last)
        {
            if (!CheckHarvest(from, tool))
            {
                from.EndAction(locked);
                return false;
            }

            int tileID;
            Map map;
            Point3D loc;

            if (!GetHarvestDetails(from, tool, toHarvest, out tileID, out map, out loc))
            {
                from.EndAction(locked);
                OnBadHarvestTarget(from, tool, toHarvest);
                return false;
            }
            else if (!def.Validate(tileID))
            {
                from.EndAction(locked);
                OnBadHarvestTarget(from, tool, toHarvest);
                return false;
            }
            else if (!CheckRange(from, tool, def, map, loc, true))
            {
                from.EndAction(locked);
                return false;
            }
            else if (!CheckResources(from, tool, def, map, loc, true))
            {
                from.EndAction(locked);
                return false;
            }
            else if (!CheckHarvest(from, tool, def, toHarvest))
            {
                from.EndAction(locked);
                return false;
            }

            DoHarvestingEffect(from, tool, def, map, loc);

            new HarvestSoundTimer(from, tool, this, def, toHarvest, locked, last).Start();

            return !last;
        }

        public virtual void DoHarvestingSound(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            if (def.EffectSounds.Length > 0)
                from.PlaySound(Utility.RandomList(def.EffectSounds));
        }

        public virtual void DoHarvestingEffect(Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc)
        {
            from.Direction = from.GetDirectionTo(loc);

            var effectAction = from.Mounted ? 26 : Utility.RandomList(def.EffectActions);

            from.Animate(effectAction, 5, 1, true, false, 0);
        }

        public virtual HarvestDefinition GetDefinition(int tileID)
        {
            HarvestDefinition def = null;

            for (int i = 0; def == null && i < m_Definitions.Count; ++i)
            {
                HarvestDefinition check = m_Definitions[i];

                if (check.Validate(tileID))
                    def = check;
            }

            return def;
        }

        public virtual void StartHarvesting(Mobile from, Item tool, object toHarvest)
        {
            if (!CheckHarvest(from, tool))
                return;

            int tileID;
            Map map;
            Point3D loc;

            if (!GetHarvestDetails(from, tool, toHarvest, out tileID, out map, out loc))
            {
                OnBadHarvestTarget(from, tool, toHarvest);
                return;
            }

            HarvestDefinition def = GetDefinition(tileID);

            if (def == null)
            {
                OnBadHarvestTarget(from, tool, toHarvest);
                return;
            }

            if (!CheckRange(from, tool, def, map, loc, false))
                return;
            else if (!CheckResources(from, tool, def, map, loc, false))
                return;
            else if (!CheckHarvest(from, tool, def, toHarvest))
                return;

            object toLock = GetLock(from, tool, def, toHarvest);

            if (!from.BeginAction(toLock))
            {
                OnConcurrentHarvest(from, tool, def, toHarvest);
                return;
            }

            var timer = new HarvestTimer(from, tool, this, def, toHarvest, toLock);
            timer.Start();
            from.NextSkillTime = Core.TickCount + (int) timer.Interval.TotalMilliseconds * timer.Count +
                                 (int) TimeSpan.FromSeconds(ZhConfig.Crafting.AutoLoop.Delay + 1.0).TotalMilliseconds;
            OnHarvestStarted(from, tool, def, toHarvest);
        }

        public virtual bool GetHarvestDetails(Mobile from, Item tool, object toHarvest, out int tileID, out Map map,
            out Point3D loc)
        {
            if (toHarvest is Static && !((Static) toHarvest).Movable)
            {
                Static obj = (Static) toHarvest;

                tileID = (obj.ItemID & 0x3FFF) | 0x4000;
                map = obj.Map;
                loc = obj.GetWorldLocation();
            }
            else if (toHarvest is StaticTarget)
            {
                StaticTarget obj = (StaticTarget) toHarvest;

                tileID = (obj.ItemID & 0x3FFF) | 0x4000;
                map = from.Map;
                loc = obj.Location;
            }
            else if (toHarvest is LandTarget)
            {
                LandTarget obj = (LandTarget) toHarvest;

                tileID = obj.TileID;
                map = from.Map;
                loc = obj.Location;
            }
            else
            {
                tileID = 0;
                map = null;
                loc = Point3D.Zero;
                return false;
            }

            return map != null && map != Map.Internal;
        }
    }
}

namespace Server
{
    public interface IChopable
    {
        void OnChop(Mobile from);
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class FurnitureAttribute : Attribute
    {
        public static bool Check(Item item)
        {
            return item != null && item.GetType().IsDefined(typeof(FurnitureAttribute), false);
        }

        public FurnitureAttribute()
        {
        }
    }
}