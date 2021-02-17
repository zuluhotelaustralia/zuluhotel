using System;
using Server.Spells;

namespace Server.Items
{
    public abstract class BaseStrengthPotion : BasePotion
	{
		public abstract int StrOffset{ get; }
		public abstract TimeSpan Duration{ get; }

		public BaseStrengthPotion( PotionEffect effect ) : base( 0xF09, effect )
		{
		}

		public BaseStrengthPotion( Serial serial ) : base( serial )
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

		public bool DoStrength( Mobile from )
        {
            var mod = Utility.Dice(PotionStrength * 2, 5, 0);
            var duration = TimeSpan.FromSeconds(PotionStrength * 120);

            if (SpellHelper.AddStatBonus(from, from, StatType.Str, mod, duration))
            {
				from.FixedEffect( 0x375A, 10, 15 );
				from.PlaySound( 0x1E7 );
				return true;
			}

			from.SendLocalizedMessage( 502173 ); // You are already under a similar effect.
			return false;
		}

		public override void Drink( Mobile from )
		{
			if ( DoStrength( from ) )
			{
				PlayDrinkEffect( from );

				Consume();
			}
		}
	}
}
