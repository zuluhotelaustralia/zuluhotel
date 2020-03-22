using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a crazed miner corpse")]
    public class CrazedMiner : BaseCreature
    {
        public override bool ShowFameTitle
        {
            get
            {
                return false;
            }
        }

        private static string[] RandomSpeech = new string[] 
        {
            "Oooh shiny",
            "That rock has been eyeing me all day",
            "I'm covered in spiders!",
            "ore...ore...INGOTS...ore?",
            "There are whispers of automated miners from beyond the stars!!",
            "They say beyond these mountains there is only darkness!",
            "What came first? Ingots or ore?!?"
        };

        private static string[] RandomEmotes = new string[]
        {
            "*laughs maniacally*",
            "*shouts at a nearby rock*",
            "*mutters something about ore*",
            "*winks at a nearby rock*",
            "*giggles hysterically*"

        };

        private TalkTimer mTalkTimer;

        [Constructable]
        public CrazedMiner()
            : base(AIType.AI_Berserk, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "a crazed miner";
            Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191;
            }
            else
            {
                this.Body = 0x190;
            }

	    int clothinghue = Utility.RandomDyedHue();
	    
            Item shirt = new Shirt();
            shirt.Movable = false;
            shirt.Hue = clothinghue;
            AddItem(shirt);

            Item shortpants = new ShortPants();
            shortpants.Movable = false;
            shortpants.Hue = clothinghue;
            AddItem(shortpants);

            Item pickaxe = new Pickaxe();
            pickaxe.Movable = false;
	    AddItem(pickaxe);

            Item leathergloves = new LeatherGloves();
            leathergloves.Movable = false;
            leathergloves.Hue = clothinghue;
            AddItem(leathergloves);

            Item halfapron = new HalfApron();
            halfapron.Movable = false;
            halfapron.Hue = clothinghue;
            AddItem(halfapron);

            Utility.AssignRandomHair(this, 2155);

            SetStr(125, 130);
            SetDex(60, 65);
            SetInt(30, 35);

            SetHits(150, 175);

            SetDamage(7, 14); //Uses Weapon

            VirtualArmor = 0;

            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon
            
            SetSkill(SkillName.MagicResist, 30.0, 35.0);

            SetSkill(SkillName.Macing, 70.0, 75);
            SetSkill(SkillName.Swords, 70.0, 75); 

            Fame = 3000;
            Karma = -3000;

            mTalkTimer = new TalkTimer(this, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(1.0));
            mTalkTimer.Start();
        }       

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Average, 2 );
	    
            PackItem(new IronOre());
            PackItem(new Shovel());
        }

        public override bool AlwaysMurderer { get { return true; } }

        public CrazedMiner(Serial serial)
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

            mTalkTimer = new TalkTimer(this, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(1.0));
            mTalkTimer.Start();
        }

        public void SayRandomSpeech()
        {
            bool emote = Utility.RandomBool();

            int i = 0;
            if(emote)
                i = Utility.Random(RandomEmotes.Length);
            else
                i = Utility.Random(RandomSpeech.Length);
            if(emote && i < RandomEmotes.Length)
            {
                Emote(RandomEmotes[i]);
            }
            else if (i < RandomSpeech.Length)
            {
                Say(RandomSpeech[i]);
            }
        }
        private class TalkTimer : Timer
        {
            private CrazedMiner mMiner;

            public TalkTimer(CrazedMiner miner, TimeSpan delay, TimeSpan interval)
                : base(delay, interval)
            {
                mMiner = miner;
            }

            protected override void OnTick()
            {
                base.OnTick();

                mMiner.SayRandomSpeech();
            }
        }
    }
}
