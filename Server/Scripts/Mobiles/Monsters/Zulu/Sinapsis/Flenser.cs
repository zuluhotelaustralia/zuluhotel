using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
    [CorpseName( "a skeletal corpse" )]
    public class ClackinFlenser : BaseCreature
    {
	[Constructable]
	public ClackinFlenser() : base( AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.2, 0.4 )
	{
	    Name = "Clackin' Jack the Flenser";
	    Body = 57;
	    BaseSoundID = 451;

            SetStr(250, 255);
            SetDex(160, 165);
            SetInt(535, 540);

            SetHits(1225, 1250);

            SetDamage(35, 60);

            SetSkill(SkillName.Tactics, 100.0, 100.0);
	    SetSkill(SkillName.Magery, 150.0, 200.0);
            SetSkill(SkillName.Swords, 80.0, 85.0);
            SetSkill(SkillName.Macing, 80.0, 85.0);
            SetSkill(SkillName.Fencing, 80.0, 85.0);
            SetSkill(SkillName.Wrestling, 80.0, 85.0);
	    SetSkill(SkillName.EvalInt, 130.0, 130.0);
            SetSkill(SkillName.MagicResist, 130.0, 150.0);

            Fame = 5000;
            Karma = -5000;
	    
	    VirtualArmor = 90;

	}

	public override void GenerateLoot()
	{
	    AddLoot( LootPack.SuperBoss );
	    AddLoot( LootPack.UltraRich );
	}

	public override bool BleedImmune{ get{ return true; } }
	public override bool AlwaysMurderer{ get{ return true; } }
	
	public ClackinFlenser( Serial serial ) : base( serial )
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
