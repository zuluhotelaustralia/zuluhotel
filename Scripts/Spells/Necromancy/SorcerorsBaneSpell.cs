using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells.Necromancy
{
    public class SorcerorsBaneSpell : NecromancerSpell
    {
	protected const int waterfallEW = 13591; //0x3517
	protected const int waterfallNS = 13561; //0x34f9

	protected int[] pool;
	protected int[] waterfall;
	
	private static SpellInfo m_Info = new SpellInfo(
							"Sorceror's Bane", "Fluctus Perturbo Magus Navitas",
							227, 9031,
							Reagent.VolcanicAsh, Reagent.WyrmsHeart, Reagent.DaemonBone,
							Reagent.Pumice, Reagent.DragonsBlood, Reagent.DeadWood
							);

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1 ); } }

        public override double RequiredSkill{ get{ return 120.0; } }
        public override int RequiredMana{ get{ return 100; } }

        public SorcerorsBaneSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
	    // these indices in decimal
	    //pool = new int[] {6054, 6051, 6047, 6056, 6039, 6053, 6049, 6045, 6055};
	    pool = new int[] {6054, 6047, 6053, 6051, 6039, 6045, 6056, 6049, 6055};

	    //0,0 is the north-west corner, so:
	    /*
	      {           x--y--, 
	              x--y,     xy--,
	        x--y++,     xy,     x++y--,
	              xy++,     x++y, 
	                  x++y++            }
	    */

	    //in hex:             { 3517, 34f9 } which are oriented: { \, /}
	    waterfall = new int[] {13591, 13561};
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget( this );
        }

        public void Target( Mobile m )
        {
	    // this is the classic "waterfall spell"
            // as usual, in zhf this spell does 3d5 + ( magery / 4 ) which is then divided by 2 and
	    //then multiplied by your class bonus.  so, for a spec 4 mage @ 10%/level and 130 magery
	    //it looks like an average of 20.5*1.4 = ~29 damage
	    // however there is a cap of (spellcircle * (13+spellcircle))
	    // zulu being overcomplicated means for single-target spells you subtract 3 from the circle.
	    // why?  because.
	    // so the cap on damage in this case ends up being 20*33=660 because that's a reasonable cap amirite :^)
	    // that first half damage is dealt as elemental fire because everyone knows water is fire.
	    //THEN, it attempts to "deal" (targets_mana * casters_class_bonus) amount of damage,
	    //modified by both caster's and target's spec bonuses and target's resist skill, "damage"
	    //is then floored and stolen from the target's mana pool and given to the caster
	    //THEN it applies the other half of the real damage from the dice roll as elemental water.

	    //What the literal fuck.
	    
            if ( ! Caster.CanSee( m ) )
            {
                // Seems like this should be responsibility of the targetting system.  --daleron
                Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
                goto Return;
            }

            if ( ! CheckSequence() )
            {
                goto Return;
            }

            SpellHelper.Turn( Caster, m );

            m.PlaySound( 0x209 ); //sorc's bane sound
	    //play 0x218 on the target when you steal the mana

            Caster.DoHarmful( m );

	    //do the water column first
	    for ( int z=10; z>=0; z-- )
	    {
		Point3D loc = new Point3D( m.X, m.Y, m.Z+(z*10) );
		//bool canFit = SpellHelper.AdjustField( ref loc, m.Map, 100, false );

		m.PlaySound(0x11);

		//if ( !canFit )
		//    continue;

		new InternalItem( loc, m.Map, Caster, waterfall[0], 1);
		new InternalItem( loc, m.Map, Caster, waterfall[1], 1);
	    }
	    //now the pool
	    int poolnumber = 0;
	    for (int y=-1; y<=1; y++){
		for (int x=-1; x<=1; x++){
		    Point3D loc = new Point3D(m.X+x, m.Y+y, m.Z);
		    new InternalItem( loc, m.Map, Caster, pool[poolnumber], 10); //yuck
		    poolnumber++;
		}
	    }

	    double ss = Caster.Skills[DamageSkill].Value;
	    int bonus = (int)ss / 4;
	    
	    double dmg = (double)Utility.Dice(3, 5, bonus);
	    dmg /= 2;

	    //we really should put this idiom into SpellHelper, I'm fucking tired of writing it --sith
	    if( Caster is PlayerMobile && ((PlayerMobile)Caster).Spec.SpecName == SpecName.Mage ){
		dmg *= ((PlayerMobile)Caster).Spec.Bonus;
	    }

	    //sith: change this, see issue tracker on gitlab
	    if ( CheckResisted( m ) )
	    {
		dmg *= 0.75;
		m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
		goto Return;
	    }

	    m.Damage((int)dmg, m, DamageType.Water);

	    int mana = (int)dmg;

	    if( m.Mana >= mana ) {
		Caster.Mana += mana;
		m.Mana -= mana;
	    }
	    else {
		Caster.Mana += m.Mana;
		m.Mana = 0;
	    }
	    
        Return:
            FinishSequence();
        }

	private class InternalItem : Item
	{
	    private Timer m_Timer;
	    private DateTime m_End;
	    private Mobile m_Caster;
	    
	    public override bool BlocksFit{ get{ return true; } }

	    public InternalItem( Point3D loc, Map map, Mobile caster, int id, int lifespan ) : base( id )
	    {
		Visible = true;
		Movable = false;

		MoveToWorld( loc, map );

		m_Caster = caster;

		m_Timer = new InternalTimer( this, TimeSpan.FromSeconds( lifespan ) );
		m_Timer.Start();

		m_End = DateTime.UtcNow + TimeSpan.FromSeconds( lifespan );
	    }

	    public InternalItem( Serial serial ) : base( serial )
	    {
	    }


	    public override void OnAfterDelete()
	    {
		base.OnAfterDelete();

		if ( m_Timer != null )
		    m_Timer.Stop();
	    }
	    public override void Serialize( GenericWriter writer )
	    {
		base.Serialize( writer );

		writer.Write( (int) 1 ); // version

		writer.WriteDeltaTime( m_End );
	    }

	    public override void Deserialize( GenericReader reader )
	    {
		base.Deserialize( reader );

		int version = reader.ReadInt();

		switch ( version )
		{
		    case 1:
			{
			    m_End = reader.ReadDeltaTime();

			    m_Timer = new InternalTimer( this, m_End - DateTime.UtcNow );
			    m_Timer.Start();

			    break;
			}
		    case 0:
			{
			    TimeSpan duration = TimeSpan.FromSeconds( 10.0 );

			    m_Timer = new InternalTimer( this, duration );
			    m_Timer.Start();

			    m_End = DateTime.UtcNow + duration;

			    break;
			}
		}
	    }


	    private class InternalTimer : Timer
	    {
		private InternalItem m_Item;

		public InternalTimer( InternalItem item, TimeSpan duration ) : base( duration )
		{
		    Priority = TimerPriority.OneSecond;
		    m_Item = item;
		}

		protected override void OnTick()
		{
		    m_Item.Delete();
		}
	    }
	}

        private class InternalTarget : Target
        {
            private SorcerorsBaneSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget( SorcerorsBaneSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
            {
                m_Owner = owner;
            }

            protected override void OnTarget( Mobile from, object o )
            {
                if ( o is Mobile )
                    m_Owner.Target( (Mobile) o );
            }

            protected override void OnTargetFinish( Mobile from )
            {
                m_Owner.FinishSequence();
            }
        }

    }
}
