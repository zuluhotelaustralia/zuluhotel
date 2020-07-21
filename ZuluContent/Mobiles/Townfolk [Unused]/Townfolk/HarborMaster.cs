using Server.Items;

namespace Server.Mobiles
{
    public class HarborMaster : BaseCreature
	{
		public override bool CanTeach { get { return false; } }


		[Constructible]
public HarborMaster()
			: base( AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4 )
		{
			InitStats( 31, 41, 51 );

			SetSkill( SkillName.Mining, 36, 68 );


			SpeechHue = Utility.RandomDyedHue();
			Hue = Race.RandomSkinHue();
			Blessed = true;


			if( this.Female = Utility.RandomBool() )
			{
				this.Body = 0x191;
				this.Name = NameList.RandomName( "female" );
				Title = "the Harbor Mistress";
			}
			else
			{
				this.Body = 0x190;
				this.Name = NameList.RandomName( "male" );
				Title = "the Harbor Master";
			}
			AddItem( new Shirt( Utility.RandomDyedHue() ) );
			AddItem( new Boots() );
			AddItem( new LongPants( Utility.RandomNeutralHue() ) );
			AddItem( new QuarterStaff() );

			Utility.AssignRandomHair( this );

			Container pack = new Backpack();

			pack.DropItem( new Gold( 250, 300 ) );

			pack.Movable = false;

			AddItem( pack );
		}

		public override bool ClickTitle { get { return false; } }


		[Constructible]
public HarborMaster( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
