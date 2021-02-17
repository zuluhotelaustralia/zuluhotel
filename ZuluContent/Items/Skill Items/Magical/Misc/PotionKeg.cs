using System;

namespace Server.Items
{
    public class PotionKeg : Item, IDyable
    {
        private int m_Held;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Held
        {
            get { return m_Held; }
            set
            {
                if (m_Held != value)
                {
                    m_Held = value;
                    UpdateWeight();
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public PotionEffect Type { get; set; }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public uint PotionStrength { get; set; }

        public static void Initialize()
        {
            TileData.ItemTable[0x1940].Height = 4;
        }

        [Constructible]
        public PotionKeg() : base(0x1940)
        {
            UpdateWeight();
        }

        public virtual void UpdateWeight()
        {
            int held = Math.Max(0, Math.Min(m_Held, 100));

            Weight = 20 + held * 80 / 100;
        }

        [Constructible]
        public PotionKeg(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 2); // version

            writer.Write(PotionStrength);
            writer.Write((int) Type);
            writer.Write((int) m_Held);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                {
                    PotionStrength = reader.ReadUInt();
                    goto case 1;
                }
                case 1:
                case 0:
                {
                    Type = (PotionEffect) reader.ReadInt();
                    m_Held = reader.ReadInt();

                    break;
                }
            }

            if (version < 1)
                Timer.DelayCall(TimeSpan.Zero, UpdateWeight);
        }

        public override int LabelNumber
        {
            get
            {
                if (m_Held > 0 && (int) Type >= (int) PotionEffect.Conflagration)
                {
                    return 1072658 + (int) Type - (int) PotionEffect.Conflagration;
                }

                return m_Held > 0 ? 1041620 + (int) Type : 1041641;
            }
        }

        public override void OnSingleClick(Mobile from)
        {
            base.OnSingleClick(from);

            int number = m_Held switch
            {
                <= 0 => 502246,
                < 5 => 502248,
                < 20 => 502249,
                < 30 => 502250,
                < 40 => 502251,
                < 47 => 502252,
                < 54 => 502254,
                < 70 => 502253,
                < 80 => 502255,
                < 96 => 502256,
                < 100 => 502257,
                _ => 502258
            };
            
            if(from.Skills[SkillName.Alchemy].Value >= 100 && m_Held > 0)
                LabelTo(from, $"Strength: {PotionStrength}");

            LabelTo(from, number);
            
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(GetWorldLocation(), 2))
            {
                from.LocalOverheadMessage(Network.MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
                return;
            }

            if (m_Held <= 0)
            {
                from.SendLocalizedMessage(502246); // The keg is empty.
                return;
            }

            var pack = from.Backpack;

            if (pack != null && pack.ConsumeTotal(typeof(Bottle), 1))
            {
                // You pour some of the keg's contents into an empty bottle...
                from.SendLocalizedMessage(502242);

                var pot = FillBottle(Type);
                pot.PotionStrength = PotionStrength;

                if (pack.TryDropItem(from, pot, false))
                {
                    // ...and place it into your backpack.
                    from.SendLocalizedMessage(502243);
                    from.PlaySound(0x240);

                    if (--Held == 0)
                    {
                        from.SendLocalizedMessage(502245); // The keg is now empty.
                        Name = null;
                    }
                }
                else
                {
                    // ...but there is no room for the bottle in your backpack.
                    from.SendLocalizedMessage(502244);
                    pot.Delete();
                }
            }
            else
            {
                // TODO: Target a bottle
            }
        }

        public override bool OnDragDrop(Mobile from, Item item)
        {
            if (item is not BasePotion pot)
            {
                from.SendLocalizedMessage(502232); // The keg is not designed to hold that type of object.
                return false;
            }

            int toHold = Math.Min(100 - m_Held, pot.Amount);

            if (toHold <= 0)
            {
                from.SendLocalizedMessage(502233); // The keg will not hold any more!
                return false;
            }

            if (m_Held == 0)
            {
                if (GiveBottle(from, toHold))
                {
                    PotionStrength = pot.PotionStrength;
                    Type = pot.PotionEffect;
                    if ((int) Type >= (int) PotionEffect.PhandelsFineIntellect)
                        Name = $"A keg of {pot.Name}s"; // Assumes the name can be plural
                    
                    Held = toHold;

                    from.PlaySound(0x240);

                    from.SendLocalizedMessage(502237); // You place the empty bottle in your backpack.

                    pot.Consume(toHold);

                    if (!pot.Deleted)
                        pot.Bounce(from);

                    return true;
                }

                from.SendLocalizedMessage(502238); // You don't have room for the empty bottle in your backpack.
                return false;
            }

            if (pot.PotionEffect != Type)
            {
                // You decide that it would be a bad idea to mix different types of potions.
                from.SendLocalizedMessage(502236);
                return false;
            }

            if (PotionStrength != pot.PotionStrength)
            {
                from.SendAsciiMessage("You decide it would be a bad idea to mix different potencies of potions.");
                return false;
            }

            if (GiveBottle(from, toHold))
            {
                Held += toHold;

                from.PlaySound(0x240);
                from.SendLocalizedMessage(502237); // You place the empty bottle in your backpack.
                pot.Consume(toHold);

                if (!pot.Deleted)
                    pot.Bounce(from);

                return true;
            }

            from.SendLocalizedMessage(502238); // You don't have room for the empty bottle in your backpack.
            return false;
        }

        private static bool GiveBottle(Mobile m, int amount)
        {
            Container pack = m.Backpack;

            Bottle bottle = new Bottle(amount);

            if (pack == null || !pack.TryDropItem(m, bottle, false))
            {
                bottle.Delete();
                return false;
            }

            return true;
        }

        private static BasePotion FillBottle(PotionEffect type)
        {
            BasePotion potion = type switch
            {
                PotionEffect.Invisibility => new InvisibilityPotion(),
                PotionEffect.Nightsight => new NightSightPotion(),
                PotionEffect.CureLesser => new LesserCurePotion(),
                PotionEffect.Cure => new CurePotion(),
                PotionEffect.CureGreater => new GreaterCurePotion(),
                PotionEffect.Agility => new AgilityPotion(),
                PotionEffect.AgilityGreater => new GreaterAgilityPotion(),
                PotionEffect.Strength => new StrengthPotion(),
                PotionEffect.StrengthGreater => new GreaterStrengthPotion(),
                PotionEffect.PoisonLesser => new LesserPoisonPotion(),
                PotionEffect.Poison => new PoisonPotion(),
                PotionEffect.PoisonGreater => new GreaterPoisonPotion(),
                PotionEffect.PoisonDeadly => new DeadlyPoisonPotion(),
                PotionEffect.Refresh => new RefreshPotion(),
                PotionEffect.RefreshTotal => new TotalRefreshPotion(),
                PotionEffect.HealLesser => new LesserHealPotion(),
                PotionEffect.Heal => new HealPotion(),
                PotionEffect.HealGreater => new GreaterHealPotion(),
                PotionEffect.ExplosionLesser => new LesserExplosionPotion(),
                PotionEffect.Explosion => new ExplosionPotion(),
                PotionEffect.ExplosionGreater => new GreaterExplosionPotion(),
                // Alchemy plus
                PotionEffect.PhandelsFineIntellect => new PhandelsFineIntellectPotion(),
                PotionEffect.PhandelsFabulousIntellect => new PhandelsFabulousIntellectPotion(),
                PotionEffect.PhandelsFantasticIntellect => new PhandelsFantasticIntellectPotion(),
                PotionEffect.LesserMegoInvulnerability => new LesserMegoInvulnerabilityPotion(),
                PotionEffect.MegoInvulnerability => new MegoInvulnerabilityPotion(),
                PotionEffect.GreaterMegoInvulnerability => new GreaterMegoInvulnerabilityPotion(),
                PotionEffect.GrandMageRefreshElixir => new GrandMageRefreshElixir(),
                PotionEffect.HomericMight => new HomericMightPotion(),
                PotionEffect.GreaterHomericMight => new GreaterHomericMightPotion(),
                PotionEffect.TamlaHeal => new TamlaHealPotion(),
                PotionEffect.TaintsTransmutation => new TaintsTransmutationPotion(),
                PotionEffect.TaintsMajorTransmutation => new TaintsMajorTransmutationPotion(),
                _ => null
            };


            return potion;
        }

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted) return false;
            Hue = sender.DyedHue;
            return true;
        }
    }
}