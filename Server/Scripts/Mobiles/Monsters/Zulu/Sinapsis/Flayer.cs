using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
    [CorpseName( "a skeletal corpse" )]
    public class ClackinFlayer : BaseCreature
    {
	[Constructable]
	public ClackinFlayer() : base( AIType.AI_Melee, FightMode.Weakest, 10, 1, 0.2, 0.4 )
	{
	    Name = "Clackin' Jack the Flayer";
	    Body = 57;
	    BaseSoundID = 451;

            SetStr(250, 255);
            SetDex(160, 165);
            SetInt(135, 140);

            SetHits(5225, 7250);

            SetDamage(35, 60);

            SetSkill(SkillName.Tactics, 145.0, 150.0); //Uses Weapon

            SetSkill(SkillName.Swords, 180.0, 185.0);
            SetSkill(SkillName.Macing, 180.0, 185.0);
            SetSkill(SkillName.Fencing, 180.0, 185.0);
            SetSkill(SkillName.Wrestling, 180.0, 185.0);

            SetSkill(SkillName.MagicResist, 30.0, 50.0);

            Fame = 5000;
            Karma = -5000;
	    
	    VirtualArmor = 90;

	}

	public override void GenerateLoot()
	{
	    AddLoot( LootPack.UltraRich );
	    AddLoot( LootPack.FilthyRich );
	}

	public override bool BleedImmune{ get{ return true; } }
	public override bool AlwaysMurderer{ get{ return true; } }
	public override bool BardImmune { get { return true; } }
	
	public ClackinFlayer( Serial serial ) : base( serial )
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
