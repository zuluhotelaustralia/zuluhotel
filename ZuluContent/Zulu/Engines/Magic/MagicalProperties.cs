using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Scripts.Engines.Magic;
using Server;
using Server.Engines.Magic;
using Server.Items;


namespace ZuluContent.Zulu.Engines.Magic
{
    public enum MagicProp
    {
        Skill = 0,
        Stat,
        ElementalResist,
        Damage,
        ArmorProtection, // Fortified, Massive etc
        ArmorBonus, // Obsidian, Meteoric Steel
        MagicReflect,
        Healing,
        HealthRegen,
        ManaRegen,
        StamRegen,
        Swift,
        Mystical,
        Stygian,
        Durability,
        Accuracy,
        Quality,
        Slayer,
        CraftResource,
        PoisonLevel,
        PoisonCharges,
        MeditationAllowance,
        Identified,
        PlayerConstructed,
        StrBonus,
        DexBonus,
        IntBonus,
    }


    public class MagicalProperties : MagicalPropertyDictionary
    {
        
        public MagicalProperties(Item parent) : base(parent)
        {
        }

        public static MagicalProperties Deserialize(IGenericReader reader, Item item)
        {
            var mp = Deserialize(reader, new MagicalProperties(item));
            
            mp.OnMobileEquip();
            
            return mp;
        }


    }

}