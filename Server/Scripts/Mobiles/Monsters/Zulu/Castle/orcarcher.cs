using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName( "an orcish corpse" )]
    public class OrcArcher : BaseCreature
    {
	public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Orc; } }

	[Constructable]
	public OrcArcher() : base( AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
            Name = "an orc archer";
            Body = 17;
	    BaseSoundID = 0x45A;

            SetStr(95, 100);
            SetDex(70, 80);
            SetInt(30, 35);

            SetHits(185, 220);

            SetDamage(8, 16); //Uses Weapon

            VirtualArmor = 15;

            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon

            SetSkill(SkillName.Archery, 75.0, 80.0);
            SetSkill(SkillName.Wrestling, 75.0, 80.0);

            SetSkill(SkillName.MagicResist, 30.0, 35.0);

            SetSkill(SkillName.Hiding, 95.0, 100.0);
            SetSkill(SkillName.Stealth, 95.0, 100.0);

            Fame = 1500;
            Karma = -1500;



            switch (Utility.Random(2))
            {
                case 0: AddItem(new Bow()); break;
                case 1: AddItem(new Crossbow()); break;
            }

            Utility.AssignRandomHair(this);
        }

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Average );
        }

        public override bool CanRummageCorpses { get { return true; } }

        public override int Meat { get { return 1; } }

        public override OppositionGroup OppositionGroup
        {
            get { return OppositionGroup.SavagesAndOrcs; }
        }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Player && m.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
                return false;

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            if (aggressor.HueMod == 1451)
            {
                aggressor.Damage(50, this, DamageType.Fire, AttackType.Physical);
                aggressor.BodyMod = 0;
                aggressor.HueMod = -1;
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
                aggressor.SendMessage("Your skin is scorched as the orcish paint burns away!"); // Your skin is scorched as the tribal paint burns away!
            }
        }


        public OrcArcher(Serial serial)
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
