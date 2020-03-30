using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a water wyrm corpse" )]
    public class WaterWyrm : BaseCreature
    {
	[Constructable]
	public WaterWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Body = (Utility.Random(1,2)==1) ? 180 :  49;
	    Name = "a water wyrm";
	    Hue = 1346;
	    BaseSoundID = 362;

            SetStr(500, 505);
            SetDex(70, 75);
            SetInt(500, 600);

            SetHits(800, 900);
            SetMana(500, 600);

            SetDamage(20, 30);

            VirtualArmor = 20;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.Meditation, 100.0, 100.0);
            SetSkill(SkillName.EvalInt, 30.0, 35.0);
            SetSkill(SkillName.Magery, 30.0, 35.0);

            SetSkill(SkillName.MagicResist, 195.0, 200.0);

            SetSkill(SkillName.Wrestling, 75.0, 80.0);

	    Fame = 18000;
	    Karma = -18000;			

	    Tamable = true;
	    ControlSlots = 3;

	    MinTameSkill = 99.0;            
	}

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich );
	    AddLoot( LootPack.PaganReagentsPack, 10 );
	    AddLoot( LootPack.LowEarthScrolls );
		     
            PackItem(new BonePile());
            PackItem(new BonePile());
        }

	public override bool ReacquireOnMovement{ get{ return true; } }
        public override bool HasBreath { get { return true; } } // fire breath enabled
	public override int Meat{ get{ return 19; } }
	public override int Hides{ get{ return 20; } }
	public override HideType HideType{ get{ return HideType.Wyrm; } }
	public override ScaleType ScaleType{ get{ return ScaleType.White; } }
	public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Gold; } }
		
	public WaterWyrm( Serial serial ) : base( serial )
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

	    if ( Core.AOS && Body == 49 )
		Body = 180;
	}
    }
}
