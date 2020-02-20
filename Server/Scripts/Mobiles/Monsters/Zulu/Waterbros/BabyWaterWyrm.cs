using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a baby water wyrm corpse" )]
    public class BabyWaterWyrm : BaseCreature
    {
	[Constructable]
	public BabyWaterWyrm () : base( AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = "a baby water wyrm";
	    Body = Utility.RandomList( 60, 61 );
	    Hue = 1346;
	    BaseSoundID = 362;

            SetStr(450, 500);
            SetDex(70, 75);
            SetInt(25, 30);

            SetHits(500, 600);

            SetDamage(14, 20);

            VirtualArmor = 20;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 195.0, 200.0);

            SetSkill(SkillName.Wrestling, 75.0, 80.0);

	    Fame = 5500;
	    Karma = -5500;			

	    Tamable = true;
	    ControlSlots = 2;
	    MinTameSkill = 90;
	}

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Rich, 2 );
	    
            PackItem(new Bone());
            PackItem(new Bone());                  
        }
		
	public override bool ReacquireOnMovement{ get{ return true; } }
	public override bool HasBreath{ get{ return true; } } // fire breath enabled
	public override int Meat{ get{ return 10; } }
	public override int Hides{ get{ return 20; } }
	public override HideType HideType{ get{ return HideType.Horned; } }
	public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }

	public BabyWaterWyrm( Serial serial ) : base( serial )
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
