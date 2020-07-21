namespace Server.Mobiles
{
    [CorpseName( "a llama corpse" )]
	public class Llama : BaseCreature
	{

		[Constructible]
public Llama() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a llama";
			Body = 0xDC;
			BaseSoundID = 0x3F3;

			SetStr( 21, 49 );
			SetDex( 36, 55 );
			SetInt( 16, 30 );

			SetHits( 15, 27 );
			SetMana( 0 );

			SetDamage( 3, 5 );

    		SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 19.2, 29.0 );
			SetSkill( SkillName.Wrestling, 19.2, 29.0 );

			Fame = 300;
			Karma = 0;

			VirtualArmor = 16;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 35.1;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		[Constructible]
public Llama(Serial serial) : base(serial)
		{
		}

		public override void Serialize(IGenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}