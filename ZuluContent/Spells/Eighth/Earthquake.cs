using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Tls;
using Server.Items;
using Server.Multis;
using Server.Network;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Eighth
{
    public class EarthquakeSpell : MagerySpell, IAsyncSpell
    {
        public override bool DelayedDamage { get; } = true;

        public EarthquakeSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }

        public override void OnCast()
        {
            if (SpellHelper.CheckTown(Caster, Caster) && CheckSequence())
            {
                var targets = new List<Mobile>();

                var map = Caster.Map;

                if (map != null)
                    foreach (var m in Caster.GetMobilesInRange(
                        1 + (int) (Caster.Skills[SkillName.Magery].Value / 15.0)))
                        if (Caster != m && SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false))
                            targets.Add(m);

                Caster.PlaySound(0x220);

                foreach (var m in targets)
                {
                    var damage = m.Hits * 6 / 10;

                    if (!m.Player && damage < 10)
                        damage = 10;
                    else if (damage > 75)
                        damage = 75;

                    Caster.DoHarmful(m);
                    SpellHelper.Damage(damage, m, Caster, this, TimeSpan.Zero);
                }
            }

            FinishSequence();
        }

        public async Task CastAsync()
        {
            var range = Caster.Skills[SkillName.Magery].Value / 20.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref range));

            var mobiles = Caster.GetMobilesInRange((int) range);

            var casterHouse = BaseHouse.FindHouseAt(Caster);
            Caster.PlaySound(544);

            foreach (var target in mobiles)
            {
                if (target == Caster ||
                    !SpellHelper.ValidIndirectTarget(Caster, target) ||
                    !Caster.CanBeHarmful(target, false)
                )
                    continue;

                if (BaseHouse.FindHouseAt(target) is { } targetHouse && 
                    casterHouse == targetHouse &&
                    Caster.Location.Z != target.Location.Z && 
                    target.Location.Z >= Caster.Location.Z + 10
                )
                    continue;

                SpellHelper.Damage(SpellHelper.CalcSpellDamage(Caster, target, this, true), target, Caster, this);
                target.PrivateOverheadMessage(
                    MessageType.Regular,
                    0x3B2,
                    true,
                    "You are tossed about as the earth trembles below your feet!",
                    target.NetState
                );
            }
            mobiles.Free();

            await Timer.Pause(2000);
            Caster.PlaySound(546);
            
            // Wall destroyer
            var eable = Caster.GetItemsInRange((int) range);
            foreach (var item in eable)
            {
                if (item is not IDispellable {Dispellable: true})
                    continue;

                Effects.SendLocationParticles(
                    EffectItem.Create(item.Location, item.Map, EffectItem.DefaultDuration),
                    0x376A, 
                    9, 
                    20, 
                    5042
                );

                item.Delete();
            }
            eable.Free();
        }
    }
}