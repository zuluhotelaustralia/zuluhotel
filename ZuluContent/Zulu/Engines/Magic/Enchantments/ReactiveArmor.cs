using MessagePack;
using Server;
using Server.Items;
using Server.Spells.First;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class ReactiveArmor : Enchantment<ReactiveArmorInfo>
    {
        [IgnoreMember]
        public override string AffixName => string.Empty;

        [Key(1)]
        public int Value { get; set; } = 0;

        public override void OnAbsorbMeleeDamage(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
            if (!attacker.Alive || !defender.Alive || Value <= 0 || damage <= 0)
                return;
            
            var reflected = damage / 2.0;
            attacker.Damage((int)reflected, defender);
            
            attacker.FixedEffect(0x3749, 10, 10);
            attacker.PlaySound(0x1F1);
            
            Value--;

            if (Value == 0 && defender is IEnchanted enchanted)
            {
                enchanted.Enchantments.Remove(this);
                if (!defender.CanBeginAction<ReactiveArmorSpell>())
                {
                    defender.SendLocalizedMessage(1005556); // Your reactive armor spell has been nullified.
                    defender.EndAction<ReactiveArmorSpell>();
                }
            }
        }
    }

    public class ReactiveArmorInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Reactive Armor";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;
        public override string[,] Names { get; protected set; } = { };

        public override string GetName(int index, CurseType curse = CurseType.None)
        {
            return string.Empty;
        }
    }
}