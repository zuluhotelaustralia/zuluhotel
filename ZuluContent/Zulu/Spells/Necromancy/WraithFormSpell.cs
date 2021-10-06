using System;
using System.Linq;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Server;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class WraithFormSpell : NecromancerSpell, IAsyncSpell
    {
        public WraithFormSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task CastAsync()
        {
            if (!Caster.CanBuff(Caster, true, BuffIcon.Polymorph, BuffIcon.LichForm))
                return;

            var magery = Caster.Skills.Magery.Value;
            var duration = magery / 15.0;
            var range = magery / 30.0;
            var powerLevel = magery / 15.0;

            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref duration));
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref range));
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref powerLevel));

            var bonus = Caster.GetClassModifier(SkillName.Magery);
            var sleepTime = 5.0;

            if (bonus > 1.0)
                sleepTime /= bonus;

            Caster.PlaySound(0x20F);

            void OnTick()
            {
                if (!Caster.Alive) return;

                var eable = Caster.GetMobilesInRange((int) range);

                foreach (var mobile in eable)
                {
                    if (Caster == mobile || !SpellHelper.ValidIndirectTarget(Caster, mobile) ||
                        !Caster.CanBeHarmful(mobile, false))
                    {
                        continue;
                    }

                    var damage = Utility.RandomMinMax(2, 2 * (int) powerLevel);

                    SpellHelper.Damage(damage, mobile, Caster, this);

                    Caster.Mana += damage;

                    mobile.FixedParticles(0x3749, 7, 16, 5013, EffectLayer.Waist);
                    mobile.PlaySound(0x1F9);
                }

                eable.Free();
            }

            if (Caster is IBuffable {BuffManager: { } manager})
            {
                var buff = manager.HasBuff<WraithForm>()
                    ? manager.Values.FirstOrDefault(b => b is WraithForm) as WraithForm
                    : new WraithForm
                    {
                        Title = "Wraith Form",
                        Icon = BuffIcon.WraithForm,
                        BodyMods = (body: 0x1a, bodyHue: 0x482),
                        Duration = TimeSpan.FromSeconds(duration),
                        StatMods = (0, 0, 0)
                    };

                manager.TryAddBuff(buff);
                buff?.AddStack(TimeSpan.FromSeconds(duration), TimeSpan.FromSeconds(sleepTime), OnTick);
            }
        }
    }
}