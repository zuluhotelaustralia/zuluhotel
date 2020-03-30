using System;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName( "a bunny corpse" )]
    public class EasterBunny : BaseCreature
    {
	[Constructable]
	public EasterBunny() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
	{
	    Name = "an easter bunny";
	    Body = 205;

	    Hue = Utility.RandomAnimalHue();

	    SetStr( 30, 40 );
	    SetDex( 26, 38 );
	    SetInt( 6, 14 );

	    SetHits( 40, 60 );
	    SetMana( 0 );

	    SetDamage( 1 );

	    SetDamageType( ResistanceType.Physical, 100 );

	    SetResistance( ResistanceType.Physical, 5, 10 );

	    SetSkill( SkillName.MagicResist, 5.0 );
	    SetSkill( SkillName.Tactics, 5.0 );
	    SetSkill( SkillName.Wrestling, 5.0 );

	    Fame = 1000;
	    Karma = 0;

	    VirtualArmor = 6;

	    Tamable = true;
	    ControlSlots = 1;
	    MinTameSkill = -18.9;
	}

	public override int Meat{ get{ return 1; } }
	public override int Hides{ get{ return 1; } }
	public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies; } }

	public override bool OnBeforeDeath()
        {
	    EasterBunnyFriend friend = new EasterBunnyFriend();
            friend.Team = this.Team;
            friend.Combatant = this.Combatant;
            friend.NoKillAwards = true;
	    
	    Effects.PlaySound(this, Map, GetDeathSound());
            Effects.SendLocationEffect(Location, Map, 0x3709, 30, 10, Hue = 3 + (Utility.Random( 20 ) * 5), 0);
            friend.MoveToWorld(Location, Map);

            Delete();

            return false;
        }
	
	public EasterBunny(Serial serial) : base(serial)
	{
	}

	public override int GetAttackSound() 
	{ 
	    return 0xC9; 
	} 

	public override int GetHurtSound() 
	{ 
	    return 0xCA; 
	} 

	public override int GetDeathSound() 
	{ 
	    return 0xCB; 
	} 

	public override void Serialize(GenericWriter writer)
	{
	    base.Serialize(writer);

	    writer.Write((int) 0);
	}

	public override void Deserialize(GenericReader reader)
	{
	    base.Deserialize(reader);

	    int version = reader.ReadInt();
	}
    }
}
