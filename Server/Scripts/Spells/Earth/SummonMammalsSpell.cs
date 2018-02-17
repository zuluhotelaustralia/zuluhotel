using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells.Earth
{
    public class SummonMammalsSpell : AbstractEarthSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                                                        "Summon Mammals", "Chame O Mamifero Agora",
                                                        16, false,
                                                        typeof( SerpentsScales ),
                                                        typeof( PigIron ),
                                                        typeof( EyeOfNewt ));

        private static Type[] m_Mammals = {
            typeof ( GreyWolf ),
            typeof ( TimberWolf ),
            typeof ( Horse ),
            typeof ( Couger ),
            typeof ( Panther ),
            typeof ( BrownBear ),
            typeof ( GrizzlyBear ),
            typeof ( ForestOstard )
        };

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 4 ); } }

        public override double RequiredSkill{ get{ return 60.0; } }
        public override int RequiredMana{ get{ return 5; } }

        public SummonMammalsSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info ) { }

        public override void OnCast()
        {
            if ( ! CheckSequence() ) {
                goto Return;
            }

            if ( ! m.BeginAction( typeof( SummonMammalsSpell ) ) ) {
                goto Return;
            }

            double effectiveness = SpellHelper.GetEffectiveness( Caster );

            int count = (int)(3 * effectiveness);

            if ( Caster.Spec.SpecName == SpecName.Mage ) {
                count++;
                bonus = 1;
            }

            for ( int i = 0 ; i < count ; i++ ) {
                double roll = 0.8 * Utility.RandomDouble() + 0.2 * effectiveness;
                int mammal = (int)Math.Max(m_Mammals.length - 1,
                                           Math.Floor(m_Mammals.length * roll));

                BaseCreature creature = (BaseCreature)Activator.CreateInstance( m_Mammals[mammal] );
                TimeSpan duration = TimeSpan.FromSeconds((int)(5 * 60 * effectiveness));

                SpellHelper.Summon( creature, Caster, 0x215, duration, false, false );
            }

        Return:
            FinishSequence();
        }
    }
}
