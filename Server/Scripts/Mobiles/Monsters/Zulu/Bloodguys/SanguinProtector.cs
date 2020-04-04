using System;
using Server.Misc;
using Server.Network;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
    public class SanguinProtector : BaseCreature
    {
	public override bool ClickTitle{ get{ return false; } }
	public override bool ShowFameTitle{ get{ return false; } }

	[Constructable]
	public SanguinProtector(): base( AIType.AI_Berserk, FightMode.Weakest, 10, 1, 0.1, 0.3 )
	{
	    Body = 0x190;
	    Name = "Protector of Sanguin";
	    Hue = 902;

	    SetStr( 351, 400 );
	    SetDex( 70, 80 );
	    SetInt( 35, 40 );

	    SetHits( 600, 750 );

	    SetDamage( 16, 22 ); //Uses Weapon

            VirtualArmor = 20;
            
            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon

            SetSkill(SkillName.Swords, 75.0, 80.0);
            SetSkill(SkillName.Macing, 75.0, 80.0);
            SetSkill(SkillName.Fencing, 75.0, 80.0);
            SetSkill(SkillName.Wrestling, 75.0, 80.0);

            SetSkill(SkillName.Parry, 45.0, 50.0);

            SetSkill(SkillName.MagicResist, 30.0, 35.0);

	    Fame = 10000;
	    Karma = -10000;			

	    VikingSword weapon = new VikingSword();
	    weapon.Movable = false;
	    AddItem( weapon );

	    MetalShield shield = new MetalShield();
	    shield.Movable = false;
	    AddItem( shield );

	    BoneHelm helm = new BoneHelm();
	    helm.Hue = 1775;
            helm.Movable = false;
	    AddItem( helm );

	    BoneArms arms = new BoneArms();
	    arms.Hue = 1775;
            arms.Movable = false;
	    AddItem( arms );

	    BoneGloves gloves = new BoneGloves();
	    gloves.Hue = 1775;
            gloves.Movable = false;
	    AddItem( gloves );

	    BoneChest tunic = new BoneChest();
	    tunic.Hue = 1775;
            tunic.Movable = false;
	    AddItem( tunic );

	    BoneLegs legs = new BoneLegs();
	    legs.Hue = 1775;
            legs.Movable = false;
	    AddItem( legs );

	    AddItem( new Boots() );
	}

        public override void GenerateLoot()
        {
        }

        public override bool AlwaysMurderer { get { return true; } }
        public override bool Unprovokable { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Deadly; } }

        public SanguinProtector(Serial serial)
            : base(serial)
        {
        }

        public override bool OnBeforeDeath()
        {
            Balron rm = new Balron();
            rm.Team = this.Team;
            rm.Combatant = this.Combatant;
            rm.NoKillAwards = true;

            //Final Loot
	    rm.AddLoot( LootPack.FilthyRich, 2 );
	    rm.AddLoot( LootPack.Rich );
	    rm.AddLoot( LootPack.MedScrolls, 2 );
	    rm.AddLoot( LootPack.GreaterNecroScrolls, 2);
	    if( Utility.RandomDouble() >= 0.98 ){
		rm.PackItem( new NecromancerSpellbook() );
	    }

	    
            Effects.PlaySound(this, Map, GetDeathSound());
            Effects.SendLocationEffect(Location, Map, 0x3709, 30, 10, 1775, 0);
            rm.MoveToWorld(Location, Map);

            Delete();

            return false;
        }

	public override int GetIdleSound()
	{
	    return 0x184;
	}

	public override int GetAngerSound()
	{
	    return 0x286;
	}

	public override int GetDeathSound()
	{
	    return 0x288;
	}

	public override int GetHurtSound()
	{
	    return 0x19F;
	}        

	public override void Serialize( GenericWriter writer )
	{
	    base.Serialize( writer );

	    writer.Write( (int) 0 ); // version
	}

	public override void Deserialize( GenericReader reader )
	{
	    base.Deserialize( reader );

	    int version = reader.ReadInt();
	}
    }
}
