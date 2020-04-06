using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a royal corpse")]
    public class CastleRoyalty : BaseCreature
    {
        [Constructable]
        public CastleRoyalty()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "Castle Royalty";
            Hue = 1882;

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191; // female
            
            }
            else
            {
                this.Body = 0x190; // male
 
            }

            SetStr(150, 155);
            SetDex(60, 65);
            SetInt(35, 40);

            SetHits(225, 250);

            SetDamage(14, 20); //Uses Weapon

            VirtualArmor = 40;

            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon

            SetSkill(SkillName.Swords, 80.0, 85.0);
            SetSkill(SkillName.Macing, 80.0, 85.0);
            SetSkill(SkillName.Fencing, 80.0, 85.0);
            SetSkill(SkillName.Wrestling, 80.0, 85.0);

            SetSkill(SkillName.MagicResist, 30.0, 35.0);

            Fame = 3500;
            Karma = -3500;

            Utility.AssignRandomHair(this);

            

            Item Bandana = new Bandana();
            Bandana.Movable = false;
            Bandana.Hue = 1760;
            AddItem(Bandana);

            Item Surcoat = new Surcoat();
            Surcoat.Movable = false;
            Surcoat.Hue = 1760;
            AddItem(Surcoat);

            Item Cloak = new Cloak();
            Cloak.Movable = false;
            Cloak.Hue = 1760;
            AddItem(Cloak);

            Item PlateArms = new PlateArms();
            PlateArms.Movable = false;
            AddItem(PlateArms);

            Item PlateGloves = new PlateGloves();
            PlateGloves.Movable = false;
            AddItem(PlateGloves);

            Item PlateLegs = new PlateLegs();
            PlateLegs.Movable = false;
            AddItem(PlateLegs);

        }


        public override bool OnBeforeDeath()
        {
            AncientVampire rm = new AncientVampire();
            rm.Team = this.Team;
            rm.Combatant = this.Combatant;
            rm.NoKillAwards = true;
	    rm.AddLoot( LootPack.FilthyRich, 2 );
	    rm.AddLoot( LootPack.Rich );
	    rm.AddLoot( LootPack.GreaterNecroScrolls );
	    rm.AddLoot( LootPack.LesserNecroScrolls );
	    rm.AddLoot( LootPack.Gems, 2 );

	    Effects.PlaySound(this, Map, GetDeathSound());
            Effects.SendLocationEffect(Location, Map, 0x3709, 30, 10, 1775, 0);
            rm.MoveToWorld(Location, Map);

            Delete();

            return false;
        }

        public override void GenerateLoot()
        {

        }

        public override bool AlwaysMurderer { get { return true; } }

        public CastleRoyalty(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
