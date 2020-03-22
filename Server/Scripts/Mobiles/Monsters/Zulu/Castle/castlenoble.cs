using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a noble corpse")]
    public class CastleNoble : BaseCreature
    {
        [Constructable]
        public CastleNoble()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "Castle Noble";
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

            

            Item ThighBoots = new ThighBoots();
            ThighBoots.Movable = false;
            ThighBoots.Hue = 0;
            AddItem(ThighBoots);

            Item Tunic = new Tunic();
            Tunic.Movable = false;
            Tunic.Hue = 1644;
            AddItem(Tunic);

            Item Cap = new Cap();
            Cap.Movable = false;
            Cap.Hue = 2306;
            AddItem(Cap);

        }


        public override bool OnBeforeDeath()
        {
            Vampire rm = new Vampire();
            rm.Team = this.Team;
            rm.Combatant = this.Combatant;
            rm.NoKillAwards = true;
	    rm.AddLoot( LootPack.FilthyRich );
           
            


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

        public CastleNoble(Serial serial)
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
