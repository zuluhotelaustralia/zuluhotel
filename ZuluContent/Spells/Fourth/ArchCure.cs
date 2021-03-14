using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Fourth
{
    public class ArchCureSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public ArchCureSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }

        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var target = SpellHelper.GetSurfaceTop(response.Target);

            SpellHelper.Turn(Caster, target);

            var targets = new List<Mobile>();
            var map = Caster.Map;
            
            IPooledEnumerable eable = map.GetMobilesInRange(target, 2);
            targets.AddRange(eable.Cast<Mobile>().Where(m => Caster.CanBeBeneficial(m, false)));
            eable.Free();

            if (targets.Count <= 0)
                return;
            
            Effects.PlaySound(target, Caster.Map, 0x299);

            foreach (var mobile in targets)
            {
                var poison = mobile.Poison;
                if (poison is null)
                    continue;
                
                Caster.DoBeneficial(mobile);

                double difficulty = poison.Level * 15 + 60;
                Caster.FireHook(h => h.OnCure(Caster, mobile, poison, this, ref difficulty));

                if (difficulty < 10)
                    difficulty = 10;

                if (Caster.ShilCheckSkill(SkillName.Magery, (int) difficulty, 0) && mobile.CurePoison(Caster))
                {
                    mobile.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1010058, Caster.NetState);
                    mobile.FixedParticles(0x373A, 10, 15, 5012, EffectLayer.Waist);
                    mobile.PlaySound(0x1E0);
                }
            }
        }
    }
}