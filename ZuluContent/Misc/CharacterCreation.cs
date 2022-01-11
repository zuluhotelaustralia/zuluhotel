using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Accounting;
using Server.Spells;
using Server.Utilities;

namespace Server.Misc
{
    public class CharacterCreation
    {
        public static void Initialize()
        {
            // Register our event handler
            EventSink.CharacterCreated += EventSink_CharacterCreated;
        }

        private static void AddBackpack(Mobile m)
        {
            Container pack = m.Backpack;

            if (pack == null)
            {
                pack = new Backpack();
                pack.Movable = false;

                m.AddItem(pack);
            }

            PackItem(new RedBook("a book", m.Name, 20, true));
            PackItem(new SkillTrainingDeed(m, 1000)); // Starting gold can be customized here
            PackItem(new Dagger());
            PackItem(new Candle());
        }

        private static Item MakeNewbie(Item item)
        {
            item.LootType = LootType.Newbied;

            return item;
        }

        private static void FillBankbox(Mobile m)
        {
            BankBox bank = m.BankBox;

            // Full spellbook
            Spellbook book = new Spellbook();

            book.Content = ulong.MaxValue;

            bank.DropItem(book);

            // Bag containing 50 of each reagent
            for (int i = 0; i < 5; ++i)
                bank.DropItem(new BagOfReagents(3));

            // Craft tools
            bank.DropItem(MakeNewbie(new Scissors()));
            bank.DropItem(MakeNewbie(new SewingKit(1000)));
            bank.DropItem(MakeNewbie(new SmithHammer(1000)));
            bank.DropItem(MakeNewbie(new FletcherTools(1000)));
            bank.DropItem(MakeNewbie(new DovetailSaw(1000)));
            bank.DropItem(MakeNewbie(new MortarPestle(1000)));
            bank.DropItem(MakeNewbie(new ScribesPen(1000)));
            bank.DropItem(MakeNewbie(new TinkerTools(1000)));

            // A few dye tubs
            bank.DropItem(new Dyes());
            bank.DropItem(new DyeTub());
            bank.DropItem(new DyeTub());
            bank.DropItem(new BlackDyeTub());

            DyeTub darkRedTub = new DyeTub();

            darkRedTub.DyedHue = 0x485;
            darkRedTub.Redyable = false;

            bank.DropItem(darkRedTub);

            // Some food
            bank.DropItem(MakeNewbie(new Apple(1000)));

            // Resources
            bank.DropItem(MakeNewbie(new Feather(1000)));
            bank.DropItem(MakeNewbie(new BoltOfCloth(1000)));
            bank.DropItem(MakeNewbie(new BlankScroll(1000)));
            bank.DropItem(MakeNewbie(new Hide(1000)));
            bank.DropItem(MakeNewbie(new Bandage(1000)));
            bank.DropItem(MakeNewbie(new Bottle(1000)));
            bank.DropItem(MakeNewbie(new Log(1000)));

            bank.DropItem(MakeNewbie(new IronIngot(5000)));
            bank.DropItem(MakeNewbie(new SpikeIngot(5000)));
            bank.DropItem(MakeNewbie(new FruityIngot(5000)));
            bank.DropItem(MakeNewbie(new BronzeIngot(5000)));
            bank.DropItem(MakeNewbie(new IceRockIngot(5000)));
            bank.DropItem(MakeNewbie(new BlackDwarfIngot(5000)));
            bank.DropItem(MakeNewbie(new DullCopperIngot(5000)));
            bank.DropItem(MakeNewbie(new PlatinumIngot(5000)));
            bank.DropItem(MakeNewbie(new SilverRockIngot(5000)));
            bank.DropItem(MakeNewbie(new DarkPaganIngot(5000)));
            bank.DropItem(MakeNewbie(new CopperIngot(5000)));
            bank.DropItem(MakeNewbie(new MysticIngot(5000)));
            bank.DropItem(MakeNewbie(new SpectralIngot(5000)));
            bank.DropItem(MakeNewbie(new OldBritainIngot(5000)));
            bank.DropItem(MakeNewbie(new OnyxIngot(5000)));
            bank.DropItem(MakeNewbie(new RedElvenIngot(5000)));
            bank.DropItem(MakeNewbie(new UndeadIngot(5000)));
            bank.DropItem(MakeNewbie(new PyriteIngot(5000)));
            bank.DropItem(MakeNewbie(new VirginityIngot(5000)));
            bank.DropItem(MakeNewbie(new MalachiteIngot(5000)));
            bank.DropItem(MakeNewbie(new LavarockIngot(5000)));
            bank.DropItem(MakeNewbie(new AzuriteIngot(5000)));
            bank.DropItem(MakeNewbie(new DripstoneIngot(5000)));
            bank.DropItem(MakeNewbie(new ExecutorIngot(5000)));
            bank.DropItem(MakeNewbie(new PeachblueIngot(5000)));
            bank.DropItem(MakeNewbie(new DestructionIngot(5000)));
            bank.DropItem(MakeNewbie(new AnraIngot(5000)));
            bank.DropItem(MakeNewbie(new CrystalIngot(5000)));
            bank.DropItem(MakeNewbie(new DoomIngot(5000)));
            bank.DropItem(MakeNewbie(new GoddessIngot(5000)));
            bank.DropItem(MakeNewbie(new NewZuluIngot(5000)));
            bank.DropItem(MakeNewbie(new DarkSableRubyIngot(5000)));
            bank.DropItem(MakeNewbie(new EbonTwilightSapphireIngot(5000)));
            bank.DropItem(MakeNewbie(new RadiantNimbusDiamondIngot(5000)));


            // Some extra starting gold
            bank.DropItem(MakeNewbie(new Gold(9000)));

            // 5 blank recall runes
            for (int i = 0; i < 5; ++i)
                bank.DropItem(MakeNewbie(new RecallRune()));
        }

        private static void AddShirt(Mobile m, int shirtHue)
        {
            int hue = Utility.ClipDyedHue(shirtHue & 0x3FFF);

            switch (Utility.Random(3))
            {
                case 0:
                    EquipItem(new Shirt(hue), true);
                    break;
                case 1:
                    EquipItem(new FancyShirt(hue), true);
                    break;
                case 2:
                    EquipItem(new Doublet(hue), true);
                    break;
            }
        }

        private static void AddPants(Mobile m, int pantsHue)
        {
            int hue = Utility.ClipDyedHue(pantsHue & 0x3FFF);

            if (m.Female)
            {
                switch (Utility.Random(2))
                {
                    case 0:
                        EquipItem(new Skirt(hue), true);
                        break;
                    case 1:
                        EquipItem(new Kilt(hue), true);
                        break;
                }
            }
            else
            {
                switch (Utility.Random(2))
                {
                    case 0:
                        EquipItem(new LongPants(hue), true);
                        break;
                    case 1:
                        EquipItem(new ShortPants(hue), true);
                        break;
                }
            }
        }

        private static void AddShoes(Mobile m)
        {
            EquipItem(new Shoes(), true);
        }

        private static Mobile CreateMobile(Account a)
        {
            if (a.Count >= a.Limit)
                return null;

            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] == null)
                    return a[i] = new PlayerMobile();
            }

            return null;
        }

        private static void EventSink_CharacterCreated(CharacterCreatedEventArgs args)
        {
            NetState state = args.State;

            if (state == null)
                return;

            HandleCharacterCreation(args);
        }

        public static void HandleCharacterCreation(CharacterCreatedEventArgs args)
        {
            if (!VerifyProfession(args.Profession))
                args.Profession = 0;

            Mobile newChar = CreateMobile(args.Account as Account);

            if (newChar == null)
            {
                Console.WriteLine("Login: {0}: Character creation failed, account full", args.State);
                return;
            }

            args.Mobile = newChar;
            m_Mobile = newChar;

            newChar.Player = true;
            newChar.AccessLevel = args.Account.AccessLevel;
            newChar.Female = args.Female;
            //newChar.Body = newChar.Female ? 0x191 : 0x190;

            newChar.Race = Race.DefaultRace;

            //newChar.Hue = Utility.ClipSkinHue( args.Hue & 0x3FFF ) | 0x8000;
            newChar.Hue = newChar.Race.ClipSkinHue(args.Hue & 0x3FFF) | 0x8000;

            newChar.Hunger = 20;

            bool young = false;

            if (newChar is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile) newChar;

                pm.Profession = args.Profession;

                if (pm.AccessLevel == AccessLevel.Player && ((Account) pm.Account).Young)
                    young = pm.Young = true;
            }

            SetName(newChar, args.Name);

            AddBackpack(newChar);

            SetStats(newChar, args.State, args.Stats, args.Profession);
            SetSkills(newChar, args.Skills, args.Profession);

            Race race = newChar.Race;

            if (race.ValidateHair(newChar, args.HairID))
            {
                newChar.HairItemID = args.HairID;
                newChar.HairHue = race.ClipHairHue(args.HairHue & 0x3FFF);
            }

            if (race.ValidateFacialHair(newChar, args.BeardID))
            {
                newChar.FacialHairItemID = args.BeardID;
                newChar.FacialHairHue = race.ClipHairHue(args.BeardHue & 0x3FFF);
            }

            if (args.Profession <= 3)
            {
                AddShirt(newChar, args.ShirtHue);
                AddPants(newChar, args.PantsHue);
                AddShoes(newChar);
            }

            if (TestCenter.Enabled)
                FillBankbox(newChar);

            // TODO: Should we make a custom race selection room in the Map?
            newChar.MoveToWorld(new Point3D(5197, 1171, 0), Map.Felucca);
            if (args.State != null)
            {
                Console.WriteLine("Login: {0}: New character being created (account={1})", args.State,
                    args.Account.Username);
                Console.WriteLine(" - Character: {0} (serial={1})", newChar.Name, newChar.Serial);

                new WelcomeTimer(newChar).Start();
            }
        }

        public static bool VerifyProfession(int profession)
        {
            if (profession < 0)
                return false;
            return profession < 4;
        }

        private class BadStartMessage : Timer
        {
            Mobile m_Mobile;
            int m_Message;

            public BadStartMessage(Mobile m, int message) : base(TimeSpan.FromSeconds(3.5))
            {
                m_Mobile = m;
                m_Message = message;
                Start();
            }

            protected override void OnTick()
            {
                m_Mobile.SendLocalizedMessage(m_Message);
            }
        }

        private static void FixStat(ref int stat, int diff, int max)
        {
            stat += diff;

            if (stat < 0)
                stat = 0;
            else if (stat > max)
                stat = max;
        }

        private static void SetStats(Mobile m, NetState state, StatNameValue[] stats, int prof)
        {
            var maxStats = state is { NewCharacterCreation: true } ? 90 : 80;

            var str = 0;
            var dex = 0;
            var intel = 0;

            if (prof > 0)
            {
                stats = ProfessionInfo.Professions[prof]?.Stats ?? stats;
            }

            for (var i = 0; i < stats.Length; i++)
            {
                var (statType, value) = stats[i];
                switch (statType)
                {
                    case StatType.Str: str = value; break;
                    case StatType.Dex: dex = value; break;
                    case StatType.Int: intel = value; break;
                }
            }

            if (str is < 10 or > 60 || dex is < 10 or > 60 || intel is < 10 or > 60 || str + dex + intel != maxStats)
            {
                str = 10;
                dex = 10;
                intel = 10;
            }

            m.InitStats(str, dex, intel);
        }

        private static void SetName(Mobile m, string name)
        {
            name = name.Trim();

            if (!NameVerification.Validate(name, 2, 16, true, false, true, 1, NameVerification.SpaceDashPeriodQuote))
                name = "Generic Player";

            m.Name = name;
        }

        private static bool ValidSkills(SkillNameValue[] skills)
        {
            int total = 0;

            for (int i = 0; i < skills.Length; ++i)
            {
                if (skills[i].Value < 0 || skills[i].Value > 50)
                    return false;

                total += skills[i].Value;

                for (int j = i + 1; j < skills.Length; ++j)
                {
                    if (skills[j].Value > 0 && skills[j].Name == skills[i].Name)
                        return false;
                }
            }

            return total == 100 || total == 120;
        }

        private static Mobile m_Mobile;

        private static void SetSkills(Mobile m, SkillNameValue[] skills, int prof)
        {
            switch (prof)
            {
                case 1: // Warrior
                {
                    skills = new[]
                    {
                        new SkillNameValue(SkillName.Anatomy, 30),
                        new SkillNameValue(SkillName.Healing, 45),
                        new SkillNameValue(SkillName.Swords, 35),
                        new SkillNameValue(SkillName.Tactics, 50)
                    };

                    break;
                }
                case 2: // Magician
                {
                    skills = new[]
                    {
                        new SkillNameValue(SkillName.EvalInt, 30),
                        new SkillNameValue(SkillName.Wrestling, 30),
                        new SkillNameValue(SkillName.Magery, 50),
                        new SkillNameValue(SkillName.Meditation, 50)
                    };

                    break;
                }
                case 3: // Blacksmith
                {
                    skills = new[]
                    {
                        new SkillNameValue(SkillName.Mining, 30),
                        new SkillNameValue(SkillName.ArmsLore, 30),
                        new SkillNameValue(SkillName.Blacksmith, 50),
                        new SkillNameValue(SkillName.Tinkering, 50)
                    };

                    break;
                }
                default:
                {
                    if (!ValidSkills(skills))
                        return;

                    break;
                }
            }

            var addSkillItems = true;

            switch (prof)
            {
                case 1: // Warrior
                {
                    EquipItem(new LeatherChest());

                    break;
                }
            }

            for (int i = 0; i < skills.Length; ++i)
            {
                SkillNameValue snv = skills[i];

                if (snv.Value > 0 && (snv.Name != SkillName.Stealth || prof == 7) && snv.Name != SkillName.RemoveTrap &&
                    snv.Name != SkillName.Spellweaving)
                {
                    Skill skill = m.Skills[snv.Name];

                    if (skill != null)
                    {
                        skill.BaseFixedPoint = snv.Value * 10;

                        if (addSkillItems)
                            AddSkillItems(snv.Name, m);
                    }
                }
            }
        }

        private static void EquipItem(Item item)
        {
            EquipItem(item, false);
        }

        private static void EquipItem(Item item, bool mustEquip)
        {
            item.LootType = LootType.Newbied;

            if (m_Mobile != null && m_Mobile.EquipItem(item))
                return;

            var pack = m_Mobile.Backpack;

            if (!mustEquip && pack != null)
                pack.DropItem(item);
            else
                item.Delete();
        }

        private static void PackItem(Item item)
        {
            item.LootType = LootType.Newbied;

            var pack = m_Mobile.Backpack;

            if (pack != null)
                pack.DropItem(item);
            else
                item.Delete();
        }

        private static void PackInstrument()
        {
            switch (Utility.Random(6))
            {
                case 0:
                    PackItem(new Drums());
                    break;
                case 1:
                    PackItem(new Harp());
                    break;
                case 2:
                    PackItem(new LapHarp());
                    break;
                case 3:
                    PackItem(new Lute());
                    break;
                case 4:
                    PackItem(new Tambourine());
                    break;
                case 5:
                    PackItem(new TambourineTassel());
                    break;
            }
        }

        private static void PackScroll(int circle)
        {
            switch (Utility.Random(8) * (circle + 1))
            {
                case 0:
                    PackItem(new ClumsyScroll());
                    break;
                case 1:
                    PackItem(new CreateFoodScroll());
                    break;
                case 2:
                    PackItem(new FeeblemindScroll());
                    break;
                case 3:
                    PackItem(new HealScroll());
                    break;
                case 4:
                    PackItem(new MagicArrowScroll());
                    break;
                case 5:
                    PackItem(new NightSightScroll());
                    break;
                case 6:
                    PackItem(new ReactiveArmorScroll());
                    break;
                case 7:
                    PackItem(new WeakenScroll());
                    break;
                case 8:
                    PackItem(new AgilityScroll());
                    break;
                case 9:
                    PackItem(new CunningScroll());
                    break;
                case 10:
                    PackItem(new CureScroll());
                    break;
                case 11:
                    PackItem(new HarmScroll());
                    break;
                case 12:
                    PackItem(new MagicTrapScroll());
                    break;
                case 13:
                    PackItem(new MagicUnTrapScroll());
                    break;
                case 14:
                    PackItem(new ProtectionScroll());
                    break;
                case 15:
                    PackItem(new StrengthScroll());
                    break;
                case 16:
                    PackItem(new BlessScroll());
                    break;
                case 17:
                    PackItem(new FireballScroll());
                    break;
                case 18:
                    PackItem(new MagicLockScroll());
                    break;
                case 19:
                    PackItem(new PoisonScroll());
                    break;
                case 20:
                    PackItem(new TelekinisisScroll());
                    break;
                case 21:
                    PackItem(new TeleportScroll());
                    break;
                case 22:
                    PackItem(new UnlockScroll());
                    break;
                case 23:
                    PackItem(new WallOfStoneScroll());
                    break;
            }
        }

        private static void AddSkillItems(SkillName skill, Mobile m)
        {
            bool elf = m.Race == Race.Elf;

            switch (skill)
            {
                case SkillName.Alchemy:
                {
                    PackItem(Reagent.NormalReagents.RandomElement().CreateInstance<Item>());
                    PackItem(Reagent.NormalReagents.RandomElement().CreateInstance<Item>());
                    PackItem(Reagent.NormalReagents.RandomElement().CreateInstance<Item>());
                    PackItem(Reagent.NormalReagents.RandomElement().CreateInstance<Item>());
                    PackItem(new Bottle(5));
                    PackItem(new MortarPestle());
                    EquipItem(new Robe(Utility.RandomPinkHue()));

                    break;
                }
                case SkillName.Anatomy:
                {
                    PackItem(new Bandage(3));
                    EquipItem(new Robe(Utility.RandomYellowHue()));

                    break;
                }
                case SkillName.AnimalLore:
                {
                    EquipItem(new ShepherdsCrook());
                    EquipItem(new Robe(Utility.RandomBlueHue()));

                    break;
                }
                case SkillName.Archery:
                {
                    PackItem(new Arrow(25));
                    EquipItem(new Bow());

                    break;
                }
                case SkillName.ArmsLore:
                {
                    switch (Utility.Random(3))
                    {
                        case 0:
                            EquipItem(new Kryss());
                            break;
                        case 1:
                            EquipItem(new Katana());
                            break;
                        case 2:
                            EquipItem(new Club());
                            break;
                    }

                    break;
                }
                case SkillName.Begging:
                {
                    EquipItem(new GnarledStaff());

                    break;
                }
                case SkillName.Blacksmith:
                {
                    PackItem(new Tongs());
                    EquipItem(new HalfApron(Utility.RandomYellowHue()));
                    break;
                }
                case SkillName.Bushido:
                {
                    break;
                }
                case SkillName.Fletching:
                {
                    PackItem(new Log(14));
                    PackItem(new Feather(5));
                    PackItem(new Shaft(5));
                    break;
                }
                case SkillName.Camping:
                {
                    PackItem(new Bedroll());
                    PackItem(new Kindling(5));
                    break;
                }
                case SkillName.Carpentry:
                {
                    PackItem(new Log(10));
                    PackItem(new Saw());
                    EquipItem(new HalfApron(Utility.RandomYellowHue()));
                    break;
                }
                case SkillName.Cartography:
                {
                    PackItem(new BlankMap());
                    PackItem(new BlankMap());
                    PackItem(new BlankMap());
                    PackItem(new BlankMap());
                    PackItem(new Sextant());
                    break;
                }
                case SkillName.Cooking:
                {
                    PackItem(new Kindling(2));
                    PackItem(new RawLambLeg());
                    PackItem(new RawChickenLeg());
                    PackItem(new RawFishSteak());
                    PackItem(new SackFlour());
                    PackItem(new Pitcher(BeverageType.Water));
                    break;
                }
                case SkillName.Chivalry:
                {
                    break;
                }
                case SkillName.DetectHidden:
                {
                    EquipItem(new Cloak(0x76C));
                    break;
                }
                case SkillName.Discordance:
                {
                    PackInstrument();
                    break;
                }
                case SkillName.Fencing:
                {
                    EquipItem(new Kryss());

                    break;
                }
                case SkillName.Fishing:
                {
                    EquipItem(new FishingPole());
                    EquipItem(new FloppyHat(Utility.RandomYellowHue()));

                    break;
                }
                case SkillName.Healing:
                {
                    PackItem(new Bandage(5));
                    PackItem(new Scissors());
                    break;
                }
                case SkillName.Herding:
                {
                    EquipItem(new ShepherdsCrook());

                    break;
                }
                case SkillName.Hiding:
                {
                    EquipItem(new Cloak(0x076C));
                    break;
                }
                case SkillName.Inscribe:
                {
                    PackItem(new BlankScroll(2));
                    break;
                }
                case SkillName.ItemID:
                {
                    EquipItem(new GnarledStaff());

                    break;
                }
                case SkillName.Lockpicking:
                {
                    PackItem(new Lockpick(5));
                    break;
                }
                case SkillName.Lumberjacking:
                {
                    EquipItem(new Hatchet());
                    break;
                }
                case SkillName.Macing:
                {
                    EquipItem(new Mace());

                    break;
                }
                case SkillName.Magery:
                {
                    PackItem(Reagent.NormalReagents.RandomElement().CreateInstance<Item>());
                    PackItem(Reagent.NormalReagents.RandomElement().CreateInstance<Item>());
                    PackItem(Reagent.NormalReagents.RandomElement().CreateInstance<Item>());

                    PackScroll(0);
                    PackScroll(1);
                    PackScroll(2);

                    var book = new Spellbook((ulong) 0x382A8C38)
                    {
                        LootType = LootType.Blessed
                    };
                    EquipItem(book);
                    
                    EquipItem(new WizardsHat());
                    EquipItem(new Robe(Utility.RandomBlueHue()));

                    break;
                }
                case SkillName.Mining:
                {
                    PackItem(new Pickaxe());
                    break;
                }
                case SkillName.Musicianship:
                {
                    PackInstrument();
                    break;
                }
                case SkillName.Necromancy:
                {
                    break;
                }
                case SkillName.Ninjitsu:
                {
                    break;
                }
                case SkillName.Parry:
                {
                    EquipItem(new WoodenShield());
                    break;
                }
                case SkillName.Peacemaking:
                {
                    PackInstrument();
                    break;
                }
                case SkillName.Poisoning:
                {
                    PackItem(new LesserPoisonPotion());
                    PackItem(new LesserPoisonPotion());
                    break;
                }
                case SkillName.Provocation:
                {
                    PackInstrument();
                    break;
                }
                case SkillName.Snooping:
                {
                    PackItem(new Lockpick(4));
                    PackItem(new ThiefGloves(ThiefGlovesHue.Blue));
                    break;
                }
                case SkillName.SpiritSpeak:
                {
                    EquipItem(new Cloak(0x076C));
                    break;
                }
                case SkillName.Stealing:
                {
                    PackItem(new Lockpick(4));
                    PackItem(new ThiefGloves(ThiefGlovesHue.Red));
                    break;
                }
                case SkillName.Swords:
                {
                    EquipItem(new Katana());

                    break;
                }
                case SkillName.Tactics:
                {
                    break;
                }
                case SkillName.Tailoring:
                {
                    PackItem(new BoltOfCloth(10));
                    PackItem(new SewingKit());
                    break;
                }
                case SkillName.TasteID:
                {
                    PackItem(new LesserPoisonPotion());
                    PackItem(new LesserExplosionPotion());
                    PackItem(new LesserHealPotion());
                    break;
                }
                case SkillName.Tinkering:
                {
                    PackItem(new TinkersTools());
                    PackItem(new HalfApron(0x021E));
                    break;
                }
                case SkillName.Tracking:
                {
                    if (m_Mobile != null)
                    {
                        Item shoes = m_Mobile.FindItemOnLayer(Layer.Shoes);

                        if (shoes != null)
                            shoes.Delete();
                    }

                    EquipItem(new Boots());
                    EquipItem(new SkinningKnife());
                    break;
                }
                case SkillName.Veterinary:
                {
                    PackItem(new Bandage(5));
                    PackItem(new Scissors());
                    break;
                }
                case SkillName.Wrestling:
                {
                    EquipItem(new LeatherGloves());
                    break;
                }
            }
        }
    }
}