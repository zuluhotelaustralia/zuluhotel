using System;
using Server.Items;
using Server.Mobiles;
using Server.Spells;

namespace Server.Mobiles {
    public class DamageTester : BaseCreature {
	[Constructable]
	public DamageTester() : base(AIType.AI_Melee, FightMode.Closest, 15, 1, 0.2, 0.6) {
	    this.Body = 400;
	    this.Hue = Utility.RandomSkinHue();
	    this.CantWalk = true;
	    this.Str = 250;
	    this.Hits = 250;
	    Container pack = new Backpack();
	    pack.Movable = false;
	    AddItem( pack );

	}

	public DamageTester( Serial serial ) : base( serial ){}

	public override void Serialize( GenericWriter writer )
	{
	    base.Serialize( writer );
	    
	    writer.Write( (int) 0 ); // version
	}
	
	public override void Deserialize( GenericReader reader )
	{
	    base.Deserialize( reader );
	    
	    int version = reader.ReadInt();
	}

	public override void OnDamage( int amount, DamageType type, Mobile from, bool willKill ){
	    Say("{0}", amount);

	    base.OnDamage(amount, type, from, willKill);
	}
    }
}
