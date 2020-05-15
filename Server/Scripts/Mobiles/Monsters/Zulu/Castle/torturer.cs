using System;
using Server.Misc;
using Server.Network;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
    public class Torturer : BaseCreature
    {
        public override bool ClickTitle { get { return false; } }
        public override bool ShowFameTitle { get { return false; } }

        [Constructable]
        public Torturer()
            : base(AIType.AI_Berserk, FightMode.Weakest, 10, 1, 0.1, 0.4)
        {
            Body = 0x190;
            Name = "Castle Torturer";
            Hue = 1882;

            SetStr(351, 400);
            SetDex(70, 80);
            SetInt(35, 40);

            SetHits(600, 750);

            SetDamage(16, 22); //Uses Weapon

            VirtualArmor = 20;

            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon

            SetSkill(SkillName.Swords, 75.0, 80.0);
            SetSkill(SkillName.Macing, 75.0, 80.0);
            SetSkill(SkillName.Fencing, 75.0, 80.0);
            SetSkill(SkillName.Wrestling, 75.0, 80.0);

            SetSkill(SkillName.Parry, 45.0, 50.0);

            SetSkill(SkillName.MagicResist, 30.0, 35.0);

            Fame = 500;
            Karma = -500;



            Item Hood = new Hood();
            Hood.Movable = false;
            Hood.Hue = 2019;
            AddItem(Hood);

            Item Surcoat = new Surcoat();
            Surcoat.Movable = false;
            Surcoat.Hue = 2019;
            AddItem(Surcoat);

            Item ThighBoots = new ThighBoots();
            ThighBoots.Movable = false;
            ThighBoots.Hue = 2019;
            AddItem(ThighBoots);

            Item StuddedGloves = new StuddedGloves();
            StuddedGloves.Movable = false;
            StuddedGloves.Hue = 2019;
            AddItem(StuddedGloves);



            Utility.AssignRandomHair(this);

        }

        public override void GenerateLoot()
        {
        }

        public override bool AlwaysMurderer { get { return true; } }
        public override bool Unprovokable { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Deadly; } }

        public Torturer(Serial serial)
            : base(serial)
        {
        }

        public override bool OnBeforeDeath()
        {
            BloodthirstyVampire rm = new BloodthirstyVampire();
            rm.Team = this.Team;
            rm.Combatant = this.Combatant;
            rm.NoKillAwards = true;
            rm.AddLoot(LootPack.FilthyRich, 2);


            Effects.PlaySound(this, Map, GetDeathSound());
            Effects.SendLocationEffect(Location, Map, 0x3709, 30, 10, 1775, 0);
            rm.MoveToWorld(Location, Map);

            Delete();

            return false;
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
