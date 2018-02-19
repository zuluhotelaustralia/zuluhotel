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
        private static SpellInfo m_Info = new SpellInfo(
                "Shapeshift", "Mude Minha Forma",
                221,
                9002,
                typeof( WyrmsHeart ),
                typeof( Blackmoor ),
                typeof( BatWing ));

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 100.0; } }
        public override int RequiredMana{ get{ return 15; } }

        public ShapeshiftSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
        }

        public override void OnCast()
        {
            if ( ! CheckSequence() )
            {
                goto Return;
            }

            if ( ! m.BeginAction( typeof( ShapeshiftSpell ) ) ) {
                goto Return;
            }

        Return:
            FinishSequence();
        }
    }

    public class ShapeshiftGump : Gump
    {
        private ShapeshiftSpell m_Owner;

        private class Entry {
            public int BodyID;
            public int ArtID;
            public int Difficulty;
            public int Points;
            public string Description;

            public Entry( string desc, int argid, int bodyid, int difficulty, int points ) {
                BodyID = bodyid;
                ArtID = artid;
                Description = description;
                Points = points;
                Difficulty = difficulty;
            }
        }

        private Entry[] Entries = {
            new Entry("a bird", 0x6, 0x20ee, 10, 100 ),
            new Entry("an eagle", 0x5, 0x211d, 20, 200 ),
            new Entry("a dog", 0xd9, 0x211c, 30, 300 ),
            new Entry("a wolf", 0xe1, 0x20ea, 40, 400 ),
            new Entry("deer", 0xed, 0x20d4, 45, 450 ),
            new Entry("a panther", 0xd6, 0x2119, 50, 500 ),
            new Entry("a bear", 0xd3, 0x2118, 50, 500 ),
            new Entry("a grizzly", 0xd4, 0x211e, 60, 600 ),
            new Entry("a polar bear", 0xd5, 0x20e1, 65, 650 ),
            new Entry("a giant serpent", 0x15, 0x20fe, 70, 700 ),
            new Entry("an earth elemental", 0xe, 0x20d7, 80, 800 ),
            new Entry("a fire elemental", 0xf, 0x20f3, 90, 900 ),
            new Entry("a water elemental", 0x10, 0x210b, 95, 950 ),
            new Entry("an air elemental", 0xd, 0x20ed, 97, 970 ),
            new Entry("a dragon", 0x3b, 0x20d6, 100, 1000 ),
            new Entry("a reaper", 0x2f, 0x20fa, 110, 1100 ),
            new Entry("a wisp", 0x3a, 0x2100, 120, 1200 )
        };

        public ShapeshiftGump( ShapeshiftSpell owner ) : base( 50, 50 )
        {
            m_Owner = owner;

            int x,y;
            AddPage( 0 );
            AddBackground( 0, 0, 585, 393, 5054 );
            AddBackground( 195, 36, 387, 275, 3000 );
            AddHtmlLocalized( 0, 0, 510, 18, 1015234, false, false ); // <center>Polymorph Selection Menu</center>
            AddHtmlLocalized( 60, 355, 150, 18, 1011036, false, false ); // OKAY
            AddButton( 25, 355, 4005, 4007, 1, GumpButtonType.Reply, 1 );
            AddHtmlLocalized( 320, 355, 150, 18, 1011012, false, false ); // CANCEL
            AddButton( 285, 355, 4005, 4007, 0, GumpButtonType.Reply, 2 );

            y = 35;
            // TODO: This gump is probably trash, test it.
            for ( int i = 0 ; i < Options.Length ; i++ ) {
                PolymorphEntry entry = (PolymorphEntry)cat.Entries[c];
                x = 5;

                AddHtml(  x,      y, 100, 18, entry.Description, false, false );
                AddItem(  x + 20, y + 25, entry.ArtID );
                AddRadio( x,      y + 20, 210, 211, false, i );
                y += 35;
            }
        }

        public override void OnResponse( NetState state, RelayInfo info ) {
            if ( info.ButtonID == 1 && info.Switches.Length > 0 ) {
                int ent = info.Switchess[0];
                if ( ent >= 0 && ent < Options[ent].Length ) {
                    m_Owner.OnSelect( Entries[ent].BodyID );
                }
            }
        }
    }
}
