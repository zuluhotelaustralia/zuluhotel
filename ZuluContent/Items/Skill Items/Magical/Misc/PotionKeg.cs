using System;

namespace Server.Items
{
    public class PotionKeg : Item
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

            writer.Write((int) 1); // version

            writer.Write((int) Type);
            writer.Write((int) m_Held);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
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

            Container pack = from.Backpack;

            if (pack != null && pack.ConsumeTotal(typeof(Bottle), 1))
            {
                // You pour some of the keg's contents into an empty bottle...
                from.SendLocalizedMessage(502242);

                BasePotion pot = FillBottle();

                if (pack.TryDropItem(from, pot, false))
                {
                    // ...and place it into your backpack.
                    from.SendLocalizedMessage(502243);
                    from.PlaySound(0x240);

                    if (--Held == 0)
                        from.SendLocalizedMessage(502245); // The keg is now empty.
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
            if (item is BasePotion pot)
            {
                int toHold = Math.Min(100 - m_Held, pot.Amount);

                if (toHold <= 0)
                {
                    from.SendLocalizedMessage(502233); // The keg will not hold any more!
                    return false;
                }
                else if (m_Held == 0)
                {
                    #region Mondain's Legacy

                    if ((int) pot.PotionEffect >= (int) PotionEffect.Invisibility)
                    {
                        from.SendLocalizedMessage(502232); // The keg is not designed to hold that type of object.
                        return false;
                    }

                    #endregion

                    if (GiveBottle(from, toHold))
                    {
                        Type = pot.PotionEffect;
                        Held = toHold;

                        from.PlaySound(0x240);

                        from.SendLocalizedMessage(502237); // You place the empty bottle in your backpack.

                        pot.Consume(toHold);

                        if (!pot.Deleted)
                            pot.Bounce(from);

                        return true;
                    }
                    else
                    {
                        from.SendLocalizedMessage(502238); // You don't have room for the empty bottle in your backpack.
                        return false;
                    }
                }
                else if (pot.PotionEffect != Type)
                {
                    from.SendLocalizedMessage(
                        502236); // You decide that it would be a bad idea to mix different types of potions.
                    return false;
                }
                else
                {
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
                    else
                    {
                        from.SendLocalizedMessage(502238); // You don't have room for the empty bottle in your backpack.
                        return false;
                    }
                }
            }
            else
            {
                from.SendLocalizedMessage(502232); // The keg is not designed to hold that type of object.
                return false;
            }
        }

        public bool GiveBottle(Mobile m, int amount)
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

        public BasePotion FillBottle()
        {
            switch (Type)
            {
                default:
                case PotionEffect.Nightsight: return new NightSightPotion();
                case PotionEffect.CureLesser: return new LesserCurePotion();
                case PotionEffect.Cure: return new CurePotion();
                case PotionEffect.CureGreater: return new GreaterCurePotion();

                case PotionEffect.Agility: return new AgilityPotion();
                case PotionEffect.AgilityGreater: return new GreaterAgilityPotion();

                case PotionEffect.Strength: return new StrengthPotion();
                case PotionEffect.StrengthGreater: return new GreaterStrengthPotion();

                case PotionEffect.PoisonLesser: return new LesserPoisonPotion();
                case PotionEffect.Poison: return new PoisonPotion();
                case PotionEffect.PoisonGreater: return new GreaterPoisonPotion();
                case PotionEffect.PoisonDeadly: return new DeadlyPoisonPotion();

                case PotionEffect.Refresh: return new RefreshPotion();
                case PotionEffect.RefreshTotal: return new TotalRefreshPotion();

                case PotionEffect.HealLesser: return new LesserHealPotion();
                case PotionEffect.Heal: return new HealPotion();
                case PotionEffect.HealGreater: return new GreaterHealPotion();

                case PotionEffect.ExplosionLesser: return new LesserExplosionPotion();
                case PotionEffect.Explosion: return new ExplosionPotion();
                case PotionEffect.ExplosionGreater: return new GreaterExplosionPotion();
            }
        }
    }
}