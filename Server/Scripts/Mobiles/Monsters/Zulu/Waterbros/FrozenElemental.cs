using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a frozen elemental corpse" )]
    public class FrozenElemental : BaseCreature
    {
	[Constructable]
	public FrozenElemental () : base( AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = "a frozen elemental";
	    Body = 161;
	    BaseSoundID = 268;
	    Hue = 696;

	    SetStr( 200, 205 );
	    SetDex( 40, 45 );
	    SetInt( 500, 600 );

            SetHits( 225, 250);
            SetMana( 500, 600);

            SetDamage(14, 18);

            VirtualArmor = 20;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.Meditation, 100.0, 100.0);
            SetSkill(SkillName.EvalInt, 50.0, 55.0);
            SetSkill(SkillName.Magery, 50.0, 55.0);

            SetSkill(SkillName.MagicResist, 95.0, 100.0);

            SetSkill(SkillName.Wrestling, 75.0, 80.0);

	    Fame = 4000;
	    Karma = -4000;            		
	}

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich );
	    AddLoot( LootPack.HighEarthScrolls );
        }

	public override bool BleedImmune{ get{ return true; } }

	public FrozenElemental( Serial serial ) : base( serial )
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
