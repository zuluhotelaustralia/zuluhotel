using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a steam elemental corpse" )]
    public class SteamElemental : BaseCreature
    {
	public override double DispelDifficulty{ get{ return 117.5; } }
	public override double DispelFocus{ get{ return 45.0; } }

	[Constructable]
	public SteamElemental () : base( AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = "a steam elemental";
	    Body = 13;
	    Hue = 2101;
	    BaseSoundID = 655;

	    SetStr( 200, 205 );
	    SetDex( 75, 75 );
	    SetInt( 500, 600 );

            SetHits( 175, 200);
            SetMana( 500, 600);

            SetDamage(7, 14);

            VirtualArmor = 5;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.Meditation, 100.0, 100.0);
            SetSkill(SkillName.EvalInt, 65.0, 70.0);
            SetSkill(SkillName.Magery, 65.0, 70.0);

            SetSkill(SkillName.MagicResist, 95.0, 100.0);

            SetSkill(SkillName.Wrestling, 80.0, 85.0);

	    Fame = 4500;
	    Karma = -4500;			

	    ControlSlots = 2;
	}

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Rich, 2 );
	    AddLoot( LootPack.LowEarthScrolls );
        }

	public override bool BleedImmune{ get{ return true; } }
		

	public SteamElemental( Serial serial ) : base( serial )
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

	    if ( BaseSoundID == 263 )
		BaseSoundID = 655;
	}
    }
}
