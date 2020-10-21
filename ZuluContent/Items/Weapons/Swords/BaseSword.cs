using Server.Targets;

namespace Server.Items
{
    public abstract class BaseSword : BaseMeleeWeapon
	{
		public override SkillName DefaultSkill{ get{ return SkillName.Swords; } }
		public override WeaponType DefaultWeaponType{ get{ return WeaponType.Slashing; } }
		public override WeaponAnimation DefaultAnimation{ get{ return WeaponAnimation.Slash1H; } }

		public BaseSword( int itemID ) : base( itemID )
		{
		}

		public BaseSword( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendLocalizedMessage( 1010018 ); // What do you want to use this item on?

			from.Target = new BladedItemTarget( this );
		}
    }
}
