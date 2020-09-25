using Server;

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
        MagicalWeaponType,
        Unused1, // was Mystical 
        Unused2, // was Stygian
        Durability,
        Accuracy,
        Quality,
        Slayer,
        CraftResource,
        PoisonLevel,
        PoisonCharges,
        MeditationAllowance,
        Identified,
        PlayerConstructed
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