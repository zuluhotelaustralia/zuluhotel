using MessagePack;
using Server;
using Server.Engines.Magic;
using Server.Items;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class PoisonHit : Enchantment<PoisonHitInfo>
    {
        [IgnoreMember]
        public override string AffixName => EnchantmentInfo.GetName(Level > 0 ? 1 : 0, Cursed);

        [IgnoreMember]
        public Poison Poison => Poison.GetPoison((int)Level);

        [Key(1)] public PoisonLevel Level { get; set; } = 0;
        
        [Key(2)] public int Charges { get; set; } = 0;
        
        [Key(3)] public double Chance { get; set; } = 0;

        [CallPriority(1)]
        public override bool GetShouldDye() => true;
        
        public override void OnMeleeHit(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
            if (Charges > 0 && Level > 0 && Poison != null && Chance > Utility.RandomDouble())
            {
                defender.ApplyPoison(attacker, Poison);
                Charges--;
            }
        }
    }
    
    public class PoisonHitInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Poison on hit";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        
        public override int Hue { get; protected set; } = 1169;
        public override int CursedHue { get; protected set; } = 1169;
        
        public override string[,] Names { get; protected set; } = {
            {string.Empty, string.Empty}, // None
            {"Poisoned", "Envenomed"}
        };
    }
}