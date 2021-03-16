using System.Threading.Tasks;
using Server.Items;
using Server.Misc;
using Server.Targeting;

namespace Server.Spells.Fifth
{
    public class DispelFieldSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public DispelFieldSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var target = SpellHelper.GetSurfaceTop(response.Target);
            SpellHelper.Turn(Caster, target);

            var range = Caster.Skills[SkillName.Magery].Value / 15;

            var eable = Caster.Map.GetItemsInRange(target, (int) range);
            foreach (var item in eable)
            {
                if (!item.GetType().IsDefined(typeof(DispellableFieldAttribute), false))
                    continue;

                if (item is Moongate {Dispellable: false})
                    continue;

                Effects.SendLocationParticles(EffectItem.Create(item.Location, item.Map, EffectItem.DefaultDuration),
                    0x376A, 9, 20, 5042);

                item.Delete();
            }
            eable.Free();

            Effects.PlaySound(target, Caster.Map, 0x201);
        }
    }
}