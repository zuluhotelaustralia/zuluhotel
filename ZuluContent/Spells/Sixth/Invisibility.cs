using System;
using System.Collections;
using System.Threading.Tasks;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Spells.Sixth
{
    public class InvisibilitySpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public InvisibilitySpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            if (target is BaseVendor || target is PlayerVendor || target.AccessLevel > Caster.AccessLevel)
            {
                Caster.SendLocalizedMessage(501857); // This spell won't work on that!
                return;
            }
            
            if (!Caster.CanBuff(target, icons: BuffIcon.HidingAndOrStealth))
                return;

            var duration = Caster.Skills[SkillName.Magery].Value;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref duration));
            
            target.TryAddBuff(new Invisibility
            {
                Title = "Invisible",
                Duration = TimeSpan.FromSeconds(duration),
                Start = DateTime.UtcNow,
            });
            
            SpellHelper.Turn(Caster, target);
            
            Effects.SendLocationParticles(
                EffectItem.Create(new Point3D(target.X, target.Y, target.Z + 16), Caster.Map, EffectItem.DefaultDuration), 0x376A,
                10, 15, 5045);
            target.PlaySound(0x3C4);
        }
    }
}