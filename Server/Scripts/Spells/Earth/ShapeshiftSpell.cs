using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Targeting;

namespace Server.Spells.Earth
{
    // polymorph into a variety of animals, one of which is the little bird
    public class ShapeshiftSpell : AbstractEarthSpell
    {
        private Entry[] m_Entries = {
            new Entry("a bird", 0x6, 0x20ee, 10),
            new Entry("an eagle", 0x5, 0x211d, 20),
            new Entry("a dog", 0xd9, 0x211c, 30),
            new Entry("a wolf", 0xe1, 0x20ea, 40),
            new Entry("a deer", 0xed, 0x20d4, 45),
            new Entry("a panther", 0xd6, 0x2119, 50),
            //new Entry("a bear", 0xd3, 0x2118, 50),
            new Entry("a grizzly", 0xd4, 0x211e, 60),
            new Entry("a polar bear", 0xd5, 0x20e1, 65),
            new Entry("a giant serpent", 0x15, 0x20fe, 70),
            new Entry("an earth elemental", 0xe, 0x20d7, 80),
            new Entry("a fire elemental", 0xf, 0x20f3, 90),
            new Entry("a water elemental", 0x10, 0x210b, 95),
            new Entry("an air elemental", 0xd, 0x20ed, 97),
            new Entry("a dragon", 0x3b, 0x20d6, 100),
            new Entry("a reaper", 0x2f, 0x20fa, 110),
            new Entry("a wisp", 0x3a, 0x2100, 120)
        };

        public class Entry {
            public int BodyID;
            public int ArtID;
            public double Difficulty;
            public string Description;

            public Entry( string desc, int artid, int bodyid, double difficulty ) {
                BodyID = bodyid;
                ArtID = artid;
                Description = desc;
                Difficulty = difficulty;
            }
        }

        private static SpellInfo m_Info = new SpellInfo(
							"Shapeshift", "Mude Minha Forma",
							221,
							9002,
							typeof( WyrmsHeart ),
							typeof( Blackmoor ),
							typeof( BatWing ));

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }

        public override double RequiredSkill {
            get {
                if ( m_NewBody == null ) return m_Entries[0].Difficulty;
                return m_NewBody.Difficulty;
            }
        }

        public override int RequiredMana{ get{ return 15; } }

        private Entry m_NewBody = null;

        public ShapeshiftSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
        }

        public ShapeshiftSpell( Mobile caster, Item scroll, Entry entry ) : base ( caster, scroll, m_Info ) {
            m_NewBody = entry;
        }

        public override bool CheckCast() {

	    if ( Factions.Sigil.ExistsOn( Caster ) )
	    {
		Caster.SendLocalizedMessage( 1010521 ); // You cannot polymorph while you have a Town Sigil
		return false;
	    }
	    else if( TransformationSpellHelper.UnderTransformation( Caster ) )
	    {
		Caster.SendLocalizedMessage( 1061633 ); // You cannot polymorph while in that form.
		return false;
	    }
	    else if ( !Caster.CanBeginAction( typeof( ShapeshiftSpell ) ) )
	    {
		Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
		return false;
	    }
	    else if ( DisguiseTimers.IsDisguised( Caster ) )
	    {
		Caster.SendLocalizedMessage( 502167 ); // You cannot polymorph while disguised.
		return false;
	    }
	    else if ( Caster.BodyMod == 183 || Caster.BodyMod == 184 )
	    {
		Caster.SendLocalizedMessage( 1042512 ); // You cannot polymorph while wearing body paint
		return false;
	    }
            else if ( m_NewBody == null ) {
                Caster.SendGump( new ShapeshiftGump( Caster, Scroll, m_Entries ) );
                return false;
            }
            return true;
	    
        }

        public override void OnCast()
        {
            if ( ! CheckSequence() )
            {
                goto Return;
            }

            if ( m_NewBody == null ) {
                // This shouldn't happen.
                goto Return;
            }

	    if ( Caster.BeginAction( typeof( ShapeshiftSpell ) ) )
	    {
		if ( m_NewBody != null )
		{
		    // body and art IDs appear to be swapped in the struct
		    if ( !((Body)m_NewBody.ArtID).IsHuman )
		    {
			Mobiles.IMount mt = Caster.Mount;
			
			if ( mt != null )
			    mt.Rider = null;
		    }
		    
		    Caster.BodyMod = m_NewBody.ArtID;
		    
		    if ( m_NewBody.ArtID == 400 || m_NewBody.ArtID == 401 )
			Caster.HueMod = Utility.RandomSkinHue();
		    else
			Caster.HueMod = 0;
		    
		    BaseArmor.ValidateMobile( Caster );
		    BaseClothing.ValidateMobile( Caster );
		    			
		    Timer t = new InternalTimer( Caster );
		    t.Start();
		}
	    }
	    else {
		Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
		goto Return;
	    }
	
	Return:
            FinishSequence();
        }

	private static void EndShapeshift( Mobile m )
	{
	    if( !m.CanBeginAction( typeof( ShapeshiftSpell ) ) )
	    {
		m.BodyMod = 0;
		m.HueMod = -1;
		m.EndAction( typeof( ShapeshiftSpell ) );

		BaseArmor.ValidateMobile( m );
		BaseClothing.ValidateMobile( m );
	    }
	}

	private class InternalTimer : Timer
	{
	    private Mobile m_Owner;

	    public InternalTimer( Mobile owner ) : base( TimeSpan.FromSeconds( 0 ) )
	    {
		m_Owner = owner;

		int val = (int)owner.Skills[SkillName.Meditation].Value;

		val *= 5;
		
		if ( val > 300 )
		    val = 300;

		Delay = TimeSpan.FromSeconds( val );
		Priority = TimerPriority.OneSecond;
	    }

	    protected override void OnTick()
	    {
		EndShapeshift( m_Owner );
	    }
	}

        public class ShapeshiftGump : Gump
        {
            Mobile m_Caster;
            Item m_Scroll;
            Entry[] m_Entries;

            public ShapeshiftGump( Mobile caster, Item scroll, Entry[] entries ) : base( 50, 50 )
            {
                m_Caster = caster;
                m_Scroll = scroll;
                m_Entries = entries;

                int x,y;
                AddPage( 0 );
                AddBackground( 0, 0, 400, 550, 5054 );
                //AddBackground( 195, 36, 387, 275, 3000 );
                AddHtmlLocalized( 0, 0, 400, 18, 1015234, false, false ); // <center>Polymorph Selection Menu</center>
                AddHtmlLocalized( 60, 485, 150, 18, 1011036, false, false ); // OKAY
                AddButton( 25, 485, 4005, 4007, 1, GumpButtonType.Reply, 1 );
                AddHtmlLocalized( 235, 485, 150, 18, 1011012, false, false ); // CANCEL
                AddButton( 200, 485, 4005, 4007, 0, GumpButtonType.Reply, 2 );

                y = 35;
                // TODO: This gump is probably trash, test it.
		// yeah it's trash let's fix that
                for ( int i = 0 ; i < entries.Length ; i++ ) {
                    Entry entry = entries[i];

		    if( i > 7 ){
			x = 200;
		    }
		    else{
			x = 25;
		    }
		    if( i == 8 ){
			y = 35;
		    }
        
                    AddHtml(  x,      y, 120, 18, entry.Description, false, false );
                    AddItem(  x + 80, y, entry.BodyID );
                    AddRadio( x,      y + 20, 210, 211, false, i );

		    y += 50;
	        }
            }

            public override void OnResponse( NetState state, RelayInfo info ) {
                if ( info.ButtonID == 1 && info.Switches.Length > 0 ) {
                    int ent = info.Switches[0];
                    if ( ent >= 0 && ent < m_Entries.Length ) {
                        new ShapeshiftSpell( m_Caster, m_Scroll, m_Entries[ent] ).Cast();
                    }
                }
            }
        }
    }
}
