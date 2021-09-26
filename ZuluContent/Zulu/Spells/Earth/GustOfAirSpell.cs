using System;
using System.Collections;
using System.Threading.Tasks;
using Server;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Spells;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Scripts.Zulu.Spells.Earth
{
    public class GustOfAirSpell : EarthSpell, ITargetableAsyncSpell<Mobile>
    {
        public GustOfAirSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);
            
            target.FixedParticles(0x37CC, 30, 30, 5028, EffectLayer.LeftFoot);
            target.PlaySound(0x0107);
            target.PlaySound(0x0108);

            var magery = Caster.Skills[SkillName.Magery].Value / 10;
            
            // TODO: Check for cursed to amplify magery?

            var kickbackX = magery > 0 ? Utility.Random((int) magery) - (int) (magery / 2) : 0;
            var kickbackY = magery > 0 ? Utility.Random((int) magery) - (int) (magery / 2) : 0;
            
            var newTargetLocation = new Point3D(target.Location.X + kickbackX, target.Location.Y + kickbackY,
                target.Location.Z);
            var map = target.Map;

            if (map.LineOfSight(Caster, newTargetLocation) && map.CanSpawnMobile(newTargetLocation))
                target.MoveToWorld(newTargetLocation, map);

            var damage = SpellHelper.CalcSpellDamage(Caster, target, this);
            SpellHelper.Damage(damage, target, Caster, this);
        }
    }
}