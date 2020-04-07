using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "an elder water elemental corpse" )]
    public class ElderWaterElemental : BaseCreature
    {
	public override double DispelDifficulty{ get{ return 117.5; } }
	public override double DispelFocus{ get{ return 45.0; } }

	[Constructable]
	public ElderWaterElemental () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = "an elder water elemental";
	    Body = 16;
	    BaseSoundID = 278;
	    Hue = 0x5F;

	    SetStr( 150, 155 );
	    SetDex( 40, 45 );
	    SetInt( 800, 1000 );

            SetHits( 400, 450);
            SetMana( 800, 1000); 

            SetDamage(7, 14);

            VirtualArmor = 5;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.Meditation, 100.0, 100.0);
            SetSkill(SkillName.EvalInt, 80.0, 85.0);
            SetSkill(SkillName.Magery, 80.0, 85.0);

            SetSkill(SkillName.MagicResist, 195.0, 200.0);

            SetSkill(SkillName.Wrestling, 70.0, 75.0);

	    Fame = 4500;
	    Karma = -4500;
			
	    ControlSlots = 3;
	    CanSwim = true;
	}

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich, 2 );
	    AddLoot( LootPack.Rich );
	    AddLoot( LootPack.HighEarthScrolls );
	    if( Utility.RandomDouble() > 0.98 ){
		PackItem( new SpellweavingBook() );
	    }
        }
		
	public override bool BleedImmune{ get{ return true; } }
		

	public ElderWaterElemental( Serial serial ) : base( serial )
	{
	}

	public override void Serialize( GenericWriter writer )
	{
	    base.Serialize( writer );
	    writer.Write( (int) 0 );
	}

	public override void Deserialize( GenericReader reader )
	{
	    base.Deserialize( reader );
	    int version = reader.ReadInt();
	}
    }
}
