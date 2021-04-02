using System.Threading.Tasks;
using Server.Items;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Second
{
    public class MagicTrapSpell : MagerySpell, ITargetableAsyncSpell<TrapableContainer>
    {
        public MagicTrapSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<TrapableContainer> response)
        {
            if (!response.HasValue)
            {
                Caster.SendLocalizedMessage(502942); // You can't trap this!
                return;
            }
            
            var item = response.Target;

            if (item.TrapType != TrapType.None && item.TrapType != TrapType.MagicTrap)
            {
                DoFizzle();
                return;
            }

            var strength = Caster.Skills[SkillName.Magery].Value / 30;
            Caster.FireHook(h => h.OnTrap(Caster, item, ref strength));

            if (strength < 0)
                strength = 1;
            
            SpellHelper.Turn(Caster, item);

            item.TrapType = TrapType.MagicTrap;
            item.TrapStrength = (int)strength;
            item.TrapLevel = 0;

            var loc = item.GetWorldLocation();

            Effects.SendLocationParticles(
                EffectItem.Create(new Point3D(loc.X + 1, loc.Y, loc.Z), item.Map, EffectItem.DefaultDuration),
                0x376A, 9, 10, 9502);
            Effects.SendLocationParticles(
                EffectItem.Create(new Point3D(loc.X, loc.Y - 1, loc.Z), item.Map, EffectItem.DefaultDuration),
                0x376A, 9, 10, 9502);
            Effects.SendLocationParticles(
                EffectItem.Create(new Point3D(loc.X - 1, loc.Y, loc.Z), item.Map, EffectItem.DefaultDuration),
                0x376A, 9, 10, 9502);
            Effects.SendLocationParticles(
                EffectItem.Create(new Point3D(loc.X, loc.Y + 1, loc.Z), item.Map, EffectItem.DefaultDuration),
                0x376A, 9, 10, 9502);
            Effects.SendLocationParticles(
                EffectItem.Create(new Point3D(loc.X, loc.Y, loc.Z), item.Map, EffectItem.DefaultDuration), 0, 0, 0,
                5014);

            Effects.PlaySound(loc, item.Map, 0x1EF);
        }
    }
}