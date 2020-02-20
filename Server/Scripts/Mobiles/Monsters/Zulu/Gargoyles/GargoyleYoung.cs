using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a young gargoyle corpse" )]
    public class GargoyleYoung : BaseCreature
    {
	[Constructable]
	public GargoyleYoung() : base( AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = "a young gargoyle";
	    Body = 74;
	    Hue = 2113;
	    BaseSoundID = 422;

	    SetStr( 91, 115 );
	    SetDex( 45, 45 );
	    SetInt( 200, 300 );

	    SetHits( 80, 100 );

	    SetDamage( 3, 6 );

            VirtualArmor = 25;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.EvalInt, 45, 50.0);
            SetSkill(SkillName.Magery, 45.0, 50.0);
            SetSkill(SkillName.Meditation, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 50.0, 55.0);

            SetSkill(SkillName.Wrestling, 50.0, 55.0);

	    Fame = 2500;
	    Karma = -2500;	
	}

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Average );
        }		

	public override int Meat{ get{ return 1; } }
	public override int Hides{ get{ return 6; } }
	public override HideType HideType{ get{ return HideType.Spined; } }
	public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
	public override PackInstinct PackInstinct{ get{ return PackInstinct.Daemon; } }

	public GargoyleYoung( Serial serial ) : base( serial )
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
