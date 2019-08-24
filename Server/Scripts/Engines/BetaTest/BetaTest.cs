using Server;
using Server.Commands;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;

using System;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items {
    public class BetaStone : Item {
	// I guess this is UO's version of the Phonetic Alphabet? lol
	private static String[] _alphabet = {
	    "An", "Bal", "Corp", "Des",
	    "Ex", "Flam", "Grav", "Hur",
	    "In", "Jux", "Kal", "Lor",
	    "Mani", "Nox", "Ort", "Por",
	    "Quas", "Rel", "Sanct", "Tym",
	    "Uus", "Vas", "Xen", "Wis",
	    "Ylem", "Zu"
	};
	
	/* each key will be a 4-word string of UO's words of power, forming a 
	   phonetic alphabet.  The value will be a bool where True denotes
	   a fresh (unused) key and false denotes one that has been used.
	*/
	private Dictionary<String, bool> _betakeys;
	public Dictionary<String, bool> BetaKeys {
	    get { return _betakeys; }
	}

	private static BetaStone _stone;
	public static BetaStone Stone{
	    get { return _stone; }
	}

	public String RequestNewKey( Mobile from ){
	    int numTerms = 4;
	    String s = "";
	    
	    for( int i=0; i < numTerms; i++ ){
		s += _alphabet[ Utility.RandomMinMax( 0, _alphabet.Length ) ];

		// don't put a trailing space
		if( i < (numTerms-1) ){
		    s += " ";
		}
	    }

	    _betakeys.Add(s, true);
	    
	    return s;
	}

	public bool CheckKeyExists( String key ){
	    if( _betakeys.ContainsKey( key ) ){
		return true;
	    }

	    return false;
	}

	public bool CheckKeyValidity( String key){
	    return _betakeys[key];
	}

	public void ConsumeKey( String key ){
	    _betakeys[key] = false;
	}
	    
	[Constructable]
	public BetaStone() : base( 0xED4 ) {
	    this.Name = "The Enchanted Stone of Beta Testing";
	    this.Hue = 2774; //spectral

	    _betakeys = new Dictionary<String, bool>();

	    _stone = this;
	}
	
	public BetaStone( Serial serial ) : base( serial ){
	    this.Name = "The Enchanted Stone of Beta Testing";
	    this.Hue = 2774;

	    _stone = this;
	}

	public override void Serialize(GenericWriter writer){
	    base.Serialize(writer);
	    writer.Write( (int) 0 ); //version

	    writer.Write( _betakeys.Count );

	    foreach( KeyValuePair<String, bool> entry in _betakeys ){
		//see comments for _betakeys
		writer.Write( entry.Key );
		writer.Write( entry.Value );
	    }

	}

	public override void Deserialize(GenericReader reader){
	    base.Deserialize(reader);

	    int version = reader.ReadInt();
	    int buckets = reader.ReadInt();

	    _betakeys = new Dictionary<String, bool>();
	    
	    for( int i=0; i < buckets; i++ ) {
		_betakeys.Add( reader.ReadString(), reader.ReadBool() );
	    }
	}
    }

    public class BetaInviteGump : Gump {
	public static void Initialize() {
	    CommandSystem.Register("Beta", AccessLevel.Player, new CommandEventHandler(Beta_OnCommand) );
	}
	
	public static void Beta_OnCommand( CommandEventArgs e ) {
	    e.Mobile.SendGump(new BetaInviteGump( e.Mobile ));
	}

	public enum Buttons {
	    Exit,
	    SubmitButton
	}

	private Mobile m_From;

	public BetaInviteGump( Mobile from ) : base( 100, 100 ){
	    m_From = from;
	    
	    Closable = true;
	    Disposable = true;
	    Dragable = true;
	    Resizable = false;

	    AddPage(0);
	    AddBackground( 0, 0, 480, 320, 9300 );
	    AddHtml( 10, 10, 460, 100, "<h2>Beta Invite Code Entry</h2>", false, false );
	    AddHtml( 10, 50, 460, 140, "If you have a Beta Invite Code, you can try to enter it here:", false, false );

	    AddImageTiled( 10, 100, 460, 22, 0 );
	    AddTextEntry( 20, 100, 460, 50, 49, 0, "");

	    AddButton( 10, 280, 247, 248, (int)Buttons.SubmitButton, GumpButtonType.Reply, 2);
	    AddImageTiled( 10, 280, 68, 22, 2624);
	    AddLabel( 20, 282, 49, @"Submit");
	}

	public override void OnResponse( NetState state, RelayInfo info ){
	    Mobile from = state.Mobile;

	    switch( info.ButtonID ){
		case (int)Buttons.SubmitButton:
		    {
			string text = info.GetTextEntry(0).Text;
			BetaStone stone = BetaStone.Stone;

			// see if the key is actually in the hash table
			if( stone.CheckKeyExists( text ) ){
			    //see if the value of the key is true
			    if( stone.CheckKeyValidity( text ) ){
				from.SendMessage("You entered a valid invite code and have been granted access to the Beta!");
				from.MoveToWorld( new Point3D(3033, 3406, 20), Map.Felucca ); //serpent's hold
			    }
			    else{
				from.SendMessage("They key you entered is valid but has already been used.");
				return;
			    }
			}
			else {
			    from.SendMessage("The key you entered does not appear to be a proper key.  Please check for typos.");
			    return;
			}
			break;
		    }
	    }
	}
    }
}
