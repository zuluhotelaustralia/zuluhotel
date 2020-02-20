using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a crazed lumberjack corpse")]
    public class CrazedLumberjack : BaseCreature
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
		"With the right varnish this wood will shine like diamonds.",
		"You'd think this would be a better weapon",
		"I wonder where they lost their eyes...",
		"Wood wood wood wood wood.",
		"There are whispers of automated lumberjacks from beyond the stars!!",
		"They say beyond these mountains there is only darkness!",
		"Look at the pretty trees!"
	    };

        private static string[] RandomEmotes = new string[]
	    {
		"*laughs maniacally*",
		"*shouts at a nearby tree*",
		"*mutters something about wood*",
		"*winks at a nearby tree*",
		"*giggles hysterically*"

	    };

        private TalkTimer mTalkTimer;

        [Constructable]
        public CrazedLumberjack()
            : base(AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "a crazed lumberjack";
            Hue = 2155;

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191;
            }
            else
            {
                this.Body = 0x190;
            }

            Item shirt = new Shirt();
            shirt.Movable = false;
            shirt.Hue = 2155;
            AddItem(shirt);

            Item longpants = new LongPants();
            longpants.Movable = false;
            longpants.Hue = 2155;
            AddItem(longpants);

            Item boots = new Boots();
            boots.Movable = false;
            boots.Hue = 2155;
            AddItem(boots);            

            Item leathergloves = new LeatherGloves();
            leathergloves.Movable = false;
            leathergloves.Hue = 2155;
            AddItem(leathergloves);
            
            Utility.AssignRandomHair(this, 2155);

            SetStr(125, 130);
            SetDex(60, 65);
            SetInt(30, 35);

            SetHits(150, 175);

            SetDamage(7, 14); //Uses Weapon

            VirtualArmor = 0;

            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon

            SetSkill(SkillName.MagicResist, 30.0, 35.0); 

            SetSkill(SkillName.Swords, 70.0, 75); 

            Fame = 3000;
            Karma = -3000;

            switch (Utility.Random(3))
            {
                case 0: AddItem(new Hatchet()); break;
                case 1: AddItem(new Axe()); break;
                case 2: AddItem(new TwoHandedAxe()); break;
            }

            mTalkTimer = new TalkTimer(this, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(1.0));
            mTalkTimer.Start();
        }        

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Average );
            PackItem(new Log());
            PackItem(new Saw());
        }

        public override bool AlwaysMurderer { get { return true; } }

        public CrazedLumberjack(Serial serial)
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
            if (emote)
                i = Utility.Random(RandomEmotes.Length);
            else
                i = Utility.Random(RandomSpeech.Length);
            if (emote && i < RandomEmotes.Length)
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
            private CrazedLumberjack mLumberjack;

            public TalkTimer(CrazedLumberjack lumberjack, TimeSpan delay, TimeSpan interval)
                : base(delay, interval)
            {
                mLumberjack = lumberjack;
            }

            protected override void OnTick()
            {
                base.OnTick();

                mLumberjack.SayRandomSpeech();
            }
        }
    }
}
