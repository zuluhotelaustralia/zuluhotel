using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a gargoyle corpse" )]
    public class GargoyleConjurer : BaseCreature
    {
	[Constructable]
	public GargoyleConjurer() : base( AIType.AI_Mage, FightMode.Strongest, 10, 1, 0.2, 0.4 )
	{
	    Name = "a gargoyle conjurer";
	    Body = 0x2F3;
	    BaseSoundID = 0x174;

	    SetStr( 400, 405 );
	    SetDex( 60, 65 );
	    SetInt( 800, 1000 );

	    SetHits( 700, 800 );
            SetMana( 800, 1000);

	    SetDamage( 8, 16 );

            VirtualArmor = 25;

            SetSkill( SkillName.Tactics, 100.0, 100.0);
           
            SetSkill( SkillName.EvalInt, 90.0, 95.0);
            SetSkill( SkillName.Magery, 90.0, 95.0);
            SetSkill( SkillName.Meditation, 100.0, 100.0);

            SetSkill( SkillName.MagicResist, 90.0, 95.0);

            SetSkill( SkillName.Wrestling, 70.0, 75.0);

	    Fame = 10000;
	    Karma = -10000;	
	}

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich );
        }
		
	public override int Meat{ get{ return 1; } }

	public GargoyleConjurer( Serial serial ) : base( serial )
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
