using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;

namespace Scripts.Zulu.Spells.Earth
{
  public class NaturesTouchSpell : AbstractEarthSpell
  {
    public override SpellInfo GetSpellInfo() => m_Info;

    private static SpellInfo m_Info = new SpellInfo(
      "Natures Touch", "Guerissez Par Terre",
      204,
      9061,
      Reagent.Pumice, Reagent.VialOfBlood, Reagent.Obsidian
    );

    public override TimeSpan CastDelayBase
    {
      get { return TimeSpan.FromSeconds(0); }
    }

    public override double RequiredSkill
    {
      get { return 80.0; }
    }

    public override int RequiredMana
    {
      get { return 10; }
    }

    public NaturesTouchSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
    {
    }

    public override void OnCast()
    {
      Caster.Target = new InternalTarget(this);
    }

    public void Target(Mobile m)
    {
      if (!Caster.CanSee(m))
      {
        // Seems like this should be responsibility of the targetting system.  --daleron
        Caster.SendLocalizedMessage(500237); // Target can not be seen.
        goto Return;
      }

      if (m is BaseCreature && ((BaseCreature) m).IsAnimatedDead)
      {
        Caster.SendLocalizedMessage(1061654); // You cannot heal that which is not alive.
        goto Return;
      }

      if (!CheckSequence())
      {
        goto Return;
      }

      SpellHelper.Turn(Caster, m);

      m.FixedParticles(0x376A, 9, 32, 5030, EffectLayer.Waist);
      m.PlaySound(0x202);

      //original spell just healed 6d8+30 points of damage and scaled that by tgt's healing bonus if any
      // i think this is better --sith
      int amount = (int) (Caster.Skills[DamageSkill].Value * 0.6);
      amount += (int) (m.Skills[SkillName.Healing].Value * 0.4);

      SpellHelper.Heal(amount, m, Caster);

      Return:
      FinishSequence();
    }

    private class InternalTimer : Timer
    {
      private Mobile m_Target;

      public InternalTimer(Mobile target, Mobile caster) : base(TimeSpan.FromSeconds(0))
      {
        m_Target = target;

        // TODO: Compute a reasonable duration, this is stolen from ArchProtection
        double time = caster.Skills[SkillName.Magery].Value * 1.2;
        if (time > 144)
          time = 144;
        Delay = TimeSpan.FromSeconds(time);
        Priority = TimerPriority.OneSecond;
      }

      protected override void OnTick()
      {
        m_Target.EndAction(typeof(NaturesTouchSpell));
      }
    }

    private class InternalTarget : Target
    {
      private NaturesTouchSpell m_Owner;

      // TODO: What is thie Core.ML stuff, is it needed?
      public InternalTarget(NaturesTouchSpell owner) : base(12, false, TargetFlags.Harmful)
      {
        m_Owner = owner;
      }

      protected override void OnTarget(Mobile from, object o)
      {
        if (o is Mobile)
          m_Owner.Target((Mobile) o);
      }

      protected override void OnTargetFinish(Mobile from)
      {
        m_Owner.FinishSequence();
      }
    }
  }
}
