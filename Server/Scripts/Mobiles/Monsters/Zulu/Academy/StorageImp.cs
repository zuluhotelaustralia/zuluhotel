using System;
using System.Collections.Generic;
using System.Text;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an imp corpse")]
    public class StorageImp : BaseCreature
    {
        [Constructable]
	public StorageImp() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = "a storage imp";
	    Body = 74;
	    BaseSoundID = 422;

//            SpellCastAnimation = 20;
//            SpellCastFrameCount = 3;

	    SetStr( 91, 115 );
	    SetDex( 61, 80 );
	    SetInt( 86, 105 );

	    SetHits( 275, 300 );
            SetMana( 400, 500);

	    SetDamage( 5, 10 );

            VirtualArmor = 5;

            SetSkill( SkillName.Tactics, 100.0, 100.0);			

	    SetSkill( SkillName.EvalInt, 70.0, 75.0 );
	    SetSkill( SkillName.Magery, 70.0, 75.0 );
            SetSkill( SkillName.Meditation, 100.0, 100.0);

	    SetSkill( SkillName.MagicResist, 200.0, 200.0 );
			
	    SetSkill( SkillName.Wrestling, 65.0, 70.0 );

	    Fame = 2500;
	    Karma = -2500;
        }

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Average );
	    AddLoot( LootPack.PaganReagentsPack, 10 );
	    AddLoot( LootPack.Gems, 4 );
	    AddLoot( LootPack.HighScrolls );
	    AddLoot( LootPack.NecroBookPack );
	    AddLoot( LootPack.AmmunitionPack, 20 );
	    AddLoot( LootPack.Potions );
            PackItem(new WoodenChest());
        }

	public override int Meat{ get{ return 1; } }
	public override int Hides{ get{ return 6; } }
	public override HideType HideType{ get{ return HideType.Spined; } }
	public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
	public override PackInstinct PackInstinct{ get{ return PackInstinct.Daemon; } }

	public StorageImp( Serial serial ) : base( serial )
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
