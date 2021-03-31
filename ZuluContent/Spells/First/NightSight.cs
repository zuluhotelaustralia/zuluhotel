using System.Threading.Tasks;
using Server.Targeting;
#pragma warning disable 1998

namespace Server.Spells.First
{
    public class NightSightSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public NightSightSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var mobile = response.Target;

            SpellHelper.Turn(Caster, mobile);

            if (!mobile.BeginAction(typeof(LightCycle)))
            {
                Caster.SendMessage("{0} already has nightsight.", Caster == mobile ? "You" : "They");
                return;
            }

            new LightCycle.NightSightTimer(mobile).Start();
            var level = (int) (LightCycle.DungeonLevel * (Caster.Skills[SkillName.Magery].Value / 100));

            if (level < 0)
                level = 0;

            mobile.LightLevel = level;

            mobile.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
            mobile.PlaySound(0x1E3);
        }
    }
}