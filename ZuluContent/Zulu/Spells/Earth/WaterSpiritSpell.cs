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
  public class WaterSpiritSpell : AbstractEarthSpell
  {
    public override SpellInfo GetSpellInfo() => m_Info;

    private static SpellInfo m_Info = new SpellInfo(
      "Water Spirit", "Chame O Agua Elemental",
      269, 9010,
      Reagent.WyrmsHeart, Reagent.NoxCrystal, Reagent.EyeOfNewt
    );

    public override TimeSpan CastDelayBase
    {
      get { return TimeSpan.FromSeconds(0); }
    }

    public override double RequiredSkill
    {
      get { return 120.0; }
    }

    public override int RequiredMana
    {
      get { return 20; }
    }

    public WaterSpiritSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
    {
    }

    public override void OnCast()
    {
      if (!CheckSequence())
      {
        goto Return;
      }

      if (Caster.Followers + 3 > Caster.FollowersMax)
      {
        Caster.SendLocalizedMessage(1049611); // get i18n shredded fgt
        goto Return;
      }

      TimeSpan duration = TimeSpan.FromSeconds(2 * Caster.Skills[DamageSkill].Fixed / 4);

      SpellHelper.Summon(new WaterElementalLord(), Caster, 0x217, duration, false, false);

      Return:
      FinishSequence();
    }
  }
}
