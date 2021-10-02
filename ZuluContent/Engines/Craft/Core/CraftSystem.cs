using System;
using System.Collections.Generic;
using System.Linq;
using Server.Items;
using Server.Misc;
using Server.Spells;
using ZuluContent.Configuration.Types;
using ZuluContent.Configuration.Types.Crafting;

namespace Server.Engines.Craft
{
    public abstract class CraftSystem
    {
        public readonly CraftSettings Settings;
        public int MinCraftEffect { get; }
        public int MaxCraftEffect { get; }
        public double Delay { get; }

        public CraftItemCol CraftItems { get; }
        public CraftGroupCol CraftGroups { get; }
        public CraftSubResCol CraftSubRes { get; }
        public CraftSubResCol CraftSubRes2 { get; }

        public SkillName MainSkill => Settings.MainSkill;

        public int GumpTitleNumber => Settings.GumpTitleId;
        
        public int CraftWorkSound => Settings.CraftWorkSound;
        
        public int CraftEndSound => Settings.CraftEndSound;

        public virtual string GumpTitleString => "";

        private readonly Dictionary<Mobile, CraftContext> m_ContextTable = new();

        public virtual bool RetainsColorFrom(CraftItem item, Type type)
        {
            return false;
        }

        public CraftContext GetContext(Mobile m)
        {
            if (m == null)
                return null;

            if (m.Deleted)
            {
                m_ContextTable.Remove(m);
                return null;
            }

            CraftContext c = null;
            m_ContextTable.TryGetValue(m, out c);

            if (c == null)
                m_ContextTable[m] = c = new CraftContext();

            return c;
        }

        public bool Resmelt { get; set; }

        public bool Repair { get; set; }

        public bool Fortify { get; set; }

        public bool MarkOption { get; set; }

        public bool CanEnhance { get; set; }

        public CraftSystem(CraftSettings settings)
        {
            Settings = settings;
            
            MinCraftEffect = settings.MinCraftDelays;
            MaxCraftEffect = settings.MaxCraftDelays;
            Delay = settings.Delay;

            CraftItems = new CraftItemCol();
            CraftGroups = new CraftGroupCol();
            CraftSubRes = new CraftSubResCol();
            CraftSubRes2 = new CraftSubResCol();

            InitCraftList();
        }

        public virtual int GetCraftSkillRequired(int itemSkillRequired, Type craftResourceType)
        {
            return itemSkillRequired;
        }

        public virtual int GetCraftPoints(int itemSkillRequired, int materialAmount)
        {
            return itemSkillRequired;
        }

        public virtual bool ConsumeOnFailure(Mobile from, Type resourceType, CraftItem craftItem)
        {
            return true;
        }

        public virtual void CreateItem(Mobile from, Type type, Type typeRes, BaseTool tool, CraftItem craftItem)
        {
            var ignored = false;
            CreateItem(from, type, typeRes, tool, craftItem, ref ignored);
        }

        public virtual void CreateItem(Mobile from, Type type, Type typeRes, BaseTool tool, CraftItem craftItem,
            ref bool success)
        {
            // Verify if the type is in the list of the craftable item
            if (CraftItems.SearchFor(type) != null)
            {
                // The item is in the list, try to create it
                craftItem.Craft(from, this, typeRes, tool, ref success);
            }
        }

        public int AddCraft(Type typeItem, TextDefinition group, TextDefinition name, double minSkill, double maxSkill,
            Type typeRes, TextDefinition nameRes, int amount, TextDefinition message)
        {
            return AddCraft(typeItem, group, name, MainSkill, minSkill, maxSkill, typeRes, nameRes, amount, message);
        }

        public int AddCraft(Type typeItem, TextDefinition group, TextDefinition name, SkillName skillToMake,
            double minSkill, double maxSkill, Type typeRes, TextDefinition nameRes, int amount, TextDefinition message)
        {
            CraftItem craftItem = new CraftItem(typeItem, group, name);
            craftItem.AddRes(typeRes, nameRes, amount, message);
            craftItem.AddSkill(skillToMake, minSkill, maxSkill);

            DoGroup(group, craftItem);
            return CraftItems.Add(craftItem);
        }


        private void DoGroup(TextDefinition groupName, CraftItem craftItem)
        {
            int index = CraftGroups.SearchFor(groupName);

            if (index == -1)
            {
                CraftGroup craftGroup = new CraftGroup(groupName);
                craftGroup.AddCraftItem(craftItem);
                CraftGroups.Add(craftGroup);
            }
            else
            {
                CraftGroups.GetAt(index).AddCraftItem(craftItem);
            }
        }

        public void SetManaReq(int index, int mana)
        {
            CraftItem craftItem = CraftItems.GetAt(index);
            craftItem.Mana = mana;
        }

        public void SetUseAllRes(int index, bool useAll)
        {
            CraftItem craftItem = CraftItems.GetAt(index);
            craftItem.UseAllRes = useAll;
        }
        
        public void SetUseSubRes2(int index, bool useSubRes)
        {
            CraftItem craftItem = CraftItems.GetAt(index);
            craftItem.UseSubRes2 = useSubRes;
        }

        public void SetNeedHeat(int index, bool needHeat)
        {
            CraftItem craftItem = CraftItems.GetAt(index);
            craftItem.NeedHeat = needHeat;
        }

        public void SetNeedOven(int index, bool needOven)
        {
            CraftItem craftItem = CraftItems.GetAt(index);
            craftItem.NeedOven = needOven;
        }

        public void SetNeedMill(int index, bool needMill)
        {
            CraftItem craftItem = CraftItems.GetAt(index);
            craftItem.NeedMill = needMill;
        }

        public void AddRes(int index, Type type, TextDefinition name, int amount, TextDefinition message)
        {
            CraftItem craftItem = CraftItems.GetAt(index);
            craftItem.AddRes(type, name, amount, message);
        }

        public void AddSkill(int index, SkillName skillToMake, double minSkill, double maxSkill)
        {
            CraftItem craftItem = CraftItems.GetAt(index);
            craftItem.AddSkill(skillToMake, minSkill, maxSkill);
        }

        public void SetSubRes(Type type, int name)
        {
            CraftSubRes.ResType = type;
            CraftSubRes.NameNumber = name;
            CraftSubRes.Init = true;
        }

        public void AddSubRes(Type type, int name, double reqSkill, object message)
        {
            CraftSubRes craftSubRes = new CraftSubRes(type, name, reqSkill, message);
            CraftSubRes.Add(craftSubRes);
        }

        public void AddSubRes(Type type, int name, double reqSkill, int genericName, object message)
        {
            CraftSubRes craftSubRes = new CraftSubRes(type, name, reqSkill, genericName, message);
            CraftSubRes.Add(craftSubRes);
        }

        public void AddSubRes(Type type, string name, double reqSkill, int genericName, object message)
        {
            CraftSubRes craftSubRes = new CraftSubRes(type, name, reqSkill, genericName, message);
            CraftSubRes.Add(craftSubRes);
        }
        
        public void SetSubRes2(Type type, int name)
        {
            CraftSubRes2.ResType = type;
            CraftSubRes2.NameNumber = name;
            CraftSubRes2.Init = true;
        }

        public void AddSubRes2(Type type, int name, double reqSkill, object message)
        {
            var craftSubRes = new CraftSubRes(type, name, reqSkill, message);
            CraftSubRes2.Add(craftSubRes);
        }

        public void AddSubRes2(Type type, int name, double reqSkill, int genericName, object message)
        {
            var craftSubRes = new CraftSubRes(type, name, reqSkill, genericName, message);
            CraftSubRes2.Add(craftSubRes);
        }

        public void AddSubRes2(Type type, string name, double reqSkill, object message)
        {
            var craftSubRes = new CraftSubRes(type, name, reqSkill, message);
            CraftSubRes2.Add(craftSubRes);
        }

        public virtual void InitCraftList()
        {
            if (Settings == null)
                return;
            
            foreach (var entry in Settings.CraftEntries)
            {
                var firstResource = entry.Resources.FirstOrDefault();

                if (firstResource == null)
                    throw new ArgumentNullException($"Tinkering entry {entry.ItemType} must have at least one resource");

                var idx = AddCraft(
                    entry.ItemType,
                    entry.GroupName,
                    entry.Name,
                    entry.Skill,
                    entry.Skill,
                    firstResource.ItemType,
                    firstResource.Name,
                    firstResource.Amount,
                    firstResource.Message
                );

                if (entry.SecondarySkill != null && entry.Skill2 != null)
                    AddSkill(idx, (SkillName) entry.SecondarySkill, (double) entry.Skill2, (double) entry.Skill2);
                
                foreach (var c in entry.Resources.Skip(1))
                {
                    AddRes(idx, c.ItemType, c.Name, c.Amount, c.Message);
                }

                if (typeof(SpellScroll).IsAssignableFrom(entry.ItemType))
                {
                    var scroll = (SpellScroll) Activator.CreateInstance(entry.ItemType);

                    if (scroll != null)
                    {
                        var spellInfo = SpellRegistry.GetInfo(scroll.SpellEntry);
                        SetManaReq(idx, spellInfo.Circle.Mana);
                    }
                }
                
                if (entry.UseAllRes)
                    SetUseAllRes(idx, true);
                
                if (entry.NeedHeat)
                    SetNeedHeat(idx, true);
                
                if (entry.NeedOven)
                    SetNeedOven(idx, true);
                
                if (entry.NeedMill)
                    SetNeedMill(idx, true);
            }
        }

        public virtual void PlayCraftEffect(Mobile from)
        {
            from.PlaySound(CraftWorkSound);
        }

        public abstract int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality,
            bool makersMark, CraftItem item);

        public abstract int CanCraft(Mobile from, BaseTool tool, Type itemType);
    }
}