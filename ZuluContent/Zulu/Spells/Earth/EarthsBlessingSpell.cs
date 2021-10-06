using System;
using System.Collections;
using System.Threading.Tasks;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Scripts.Zulu.Engines.Classes;
using Server;
using Server.Targeting;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Scripts.Zulu.Spells.Earth
{
    public class EarthsBlessingSpell : EarthSpell, IAsyncSpell
    {
        public EarthsBlessingSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task CastAsync()
        {
            if (!Caster.CanBuff(Caster, true, BuffIcon.Curse, BuffIcon.Bless, BuffIcon.GiftOfRenewal))
                return;

            var modAmount = (int) (SpellHelper.GetModAmount(Caster, Caster, StatType.All) * 1.5);
            var duration = SpellHelper.GetDuration(Caster, Caster) * 1.5;
            
            Caster.TryAddBuff(new StatBuff(StatType.All)
            {
                Title = "Earth's Blessing",
                Icon = BuffIcon.GiftOfRenewal,
                Value = modAmount,
                Duration = duration
            });

            Caster.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
            Caster.PlaySound(0x1EA);
            
            if (!Caster.CanBuff(Caster, true, BuffIcon.Protection, BuffIcon.ArchProtection, BuffIcon.Resilience))
                return;
            
            Caster.TryAddBuff(new ArmorBuff
            {
                Title = "Earth's Blessing Armor",
                Icon = BuffIcon.Resilience,
                ArmorMod = (int) (modAmount * 0.75 + 1),
                Duration = duration
            });
        }
    }
}