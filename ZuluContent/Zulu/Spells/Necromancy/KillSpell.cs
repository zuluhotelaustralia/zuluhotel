using System;
using System.Collections;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Scripts.Zulu.Engines.Classes;
using Server;
using Server.Spells;

namespace Scripts.Zulu.Spells.Necromancy
{
  public class KillSpell : NecromancerSpell
  {
    public override SpellInfo GetSpellInfo() => m_Info;

    private static SpellInfo m_Info = new SpellInfo(
      "Kill", "Ulties Manum Necarent",
      227, 9031,
      Reagent.DaemonBone, Reagent.ExecutionersCap, Reagent.VialOfBlood,
      Reagent.WyrmsHeart
    );

    public override TimeSpan CastDelayBase
    {
      get { return TimeSpan.FromSeconds(1); }
    }

    public override double RequiredSkill
    {
      get { return 140.0; }
    }

    public override int RequiredMana
    {
      get { return 130; }
    }

    public KillSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
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

      if (!CheckSequence())
      {
        goto Return;
      }

      SpellHelper.Turn(Caster, m);

      // TODO: Spell graphical and sound effects

      Caster.DoHarmful(m);
      //a spec 4 mage with 130.0 spirit speak will instakill anyone with less than ~91 hp
      // if they have more than that they get a chance to resist and take half damage, otherwise
      // they take 90% of the instakill threshhold as damage
      double power = Caster.Skills[DamageSkill].Value / 3;
      if (Spec.GetSpec(Caster).SpecName == SpecName.Mage && Spec.GetSpec(Caster).SpecLevel != 0)
      {
        power *= Spec.GetSpec(Caster).Bonus;
      }

      double safetymargin = power * 0.25;
      power -= safetymargin;

      if (m.Hits <= (int) power)
      {
        m.Kill();
      }
      else
      {
        double damage = 0.5 * Caster.Hits;

        if (CheckResisted(m))
        {
          damage *= 0.5;

          m.SendLocalizedMessage(501783); //you resist the blah blah blah
        }

        //m.Damage((int)damage, Caster, m_DamageType);
        SpellHelper.Damage(this, TimeSpan.Zero, m, Caster, damage, ElementalType.Necro);
      }

      Return:
      FinishSequence();
    }

    private class InternalTarget : Target
    {
      private KillSpell m_Owner;

      // TODO: What is thie Core.ML stuff, is it needed?
      public InternalTarget(KillSpell owner) : base(12, false, TargetFlags.Harmful)
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
