using Server.Spells.Fourth;

namespace Server.Items
{
    public class GreaterHealWand : BaseWand
	{

		[Constructible]
public GreaterHealWand() : base( WandEffect.GreaterHeal, 1, 5 )
		{
		}

		[Constructible]
public GreaterHealWand( Serial serial ) : base( serial )
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

		public override void OnWandUse( Mobile from )
		{
			Cast( new GreaterHealSpell( from, this ) );
		}
	}
}
