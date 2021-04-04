using System;
using System.Collections;
using System.Threading.Tasks;
using Scripts.Zulu.Utilities;
using Server;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Scripts.Zulu.Spells.Earth
{
    public class NaturesTouchSpell : EarthSpell, ITargetableAsyncSpell<Mobile>
    {
        public NaturesTouchSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);
            var healed = (double) Utility.Dice(6, 8, 30);
            
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref healed));

            SpellHelper.Heal((int) healed, target, Caster, this);

            target.FixedParticles(0x376A, 9, 32, 5030, EffectLayer.Waist);
            target.PlaySound(0x202);
            
            Caster.SendSuccessMessage($"Nature's touch has restored {(int) healed} damage.");
            
            if (target != Caster)
                target.SendSuccessMessage($"Nature's touch has restored {(int) healed} damage.");
        }
    }
}