using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Scripts.Zulu.Utilities;
using Server;
using Server.Network;
using Server.Items;
using Server.Spells;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class DarknessSpell : NecromancerSpell, ITargetableAsyncSpell<Mobile>
    {
        public DarknessSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                // Seems like this should be responsibility of the targetting system.  --daleron
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
                goto Return;
            }

            if (!CheckSequence()) goto Return;

            SpellHelper.Turn(Caster, m);

            if (!m.BeginAction(typeof(DarknessSpell))) goto Return;

            var level = LightCycle.DungeonLevel;
            m.LightLevel = level;

            // TODO: Spell graphical and sound effects.
            m.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
            m.PlaySound(0x1e4);
            Caster.DoHarmful(m);

            // TODO: Spell action ( buff/debuff/damage/etc. )

            new DarknessTimer(m).Start();

            Return:
            FinishSequence();
        }
        
        private class DarknessTimer : Timer
        {
            private Mobile m_Owner;

            public DarknessTimer(Mobile owner) : base(TimeSpan.FromMinutes(Utility.Random(25, 35)))
            {
                m_Owner = owner;
                Priority = TimerPriority.OneMinute;
            }

            protected override void OnTick()
            {
                m_Owner.EndAction(typeof(DarknessSpell));
                m_Owner.EndAction(typeof(LightCycle));
                m_Owner.LightLevel = 0;
            }
        }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            SpellHelper.Turn(Caster, target);

            if (!Caster.CanBuff(target, icons: BuffIcon.NightSight))
                return;

            var level = 20.0 + Caster.Skills.Magery.Value / 15.0; 
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref level));
            var duration = SpellHelper.GetDuration(Caster, target).TotalSeconds;

            if (level > 30)
                level = 30;

            var protection = target.GetResist(Info.DamageType);
            if (protection > 0)
            {
                var modifier = 100.0 - protection;
                duration *= modifier / 100;
                
                if (duration < 1)
                {
                    target.PrivateOverheadMessage(
                        MessageType.Regular, 
                        0x3B2, 
                        true, 
                        "The nature of the target prevents it from being affected by that spell!", 
                        Caster.NetState
                    );
                    return;
                }
            }

            target.TryAddBuff(new NightSight
            {
                Title = "Darkness",
                Value = LightCycle.DungeonLevel,
                Duration = TimeSpan.FromSeconds(duration),
            });

            target.FixedParticles(0x373B, 10, 10, 5007, EffectLayer.Waist);
            target.PlaySound(0x1E3);
        }
    }
}