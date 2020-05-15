using System;
using System.Collections.Generic;
using System.IO;

using Server;
using Server.Engines.Craft;
using Server.Mobiles;


namespace Server.Items
{
    public enum OldPotionEffect
    {
        Nightsight,
        CureLesser,
        Cure,
        CureGreater,
        Agility,
        AgilityGreater,
        Strength,
        StrengthGreater,
        PoisonLesser,
        Poison,
        PoisonGreater,
        PoisonDeadly,
        Refresh,
        RefreshTotal,
        HealLesser,
        Heal,
        HealGreater,
        ExplosionLesser,
        Explosion,
        ExplosionGreater,
        Conflagration,
        ConflagrationGreater,
        MaskOfDeath,        // Mask of Death is not available in OSI but does exist in cliloc files
        MaskOfDeathGreater, // included in enumeration for compatability if later enabled by OSI
        ConfusionBlast,
        ConfusionBlastGreater,
        Invisibility,
        Parasitic,
        Darkglow,
        PoisonLethal,
        Mana,
        ManaTotal
    }

    public abstract class BasePotion : Item, ICraftable, ICommodity
    {
        private PotionEffect m_PotionEffect;
        public PotionEffect PotionEffect
        {
            get
            {
                return m_PotionEffect;
            }
            set
            {
                m_PotionEffect = value;
                InvalidateProperties();
            }
        }

        private double m_CrafterSpecBonus = 1.0;
        public double CrafterSpecBonus
        {
            get { return m_CrafterSpecBonus; }
        }

        int ICommodity.DescriptionNumber { get { return LabelNumber; } }
        bool ICommodity.IsDeedable { get { return (Core.ML); } }

        public override int LabelNumber { get { return m_PotionEffect.BottleLabel; } }
        public override string DefaultName
        {
            get { return m_PotionEffect.BottleName; }
        }

        public BasePotion(int itemID, PotionEffect effect) : base(itemID)
        {
            m_PotionEffect = effect;

            Stackable = true;
            Weight = 1.0;
        }

        public BasePotion(Serial serial) : base(serial)
        {
        }

        public virtual bool RequireFreeHand { get { return true; } }

        public static bool HasFreeHand(Mobile m)
        {
            Item handOne = m.FindItemOnLayer(Layer.OneHanded);
            Item handTwo = m.FindItemOnLayer(Layer.TwoHanded);

            if (handTwo is BaseWeapon)
                handOne = handTwo;
            if (handTwo is BaseRanged)
            {
                BaseRanged ranged = (BaseRanged)handTwo;

                if (ranged.Balanced)
                    return true;
            }

            return (handOne == null || handTwo == null);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (from.InRange(this.GetWorldLocation(), 1))
            {
                if (!RequireFreeHand || HasFreeHand(from))
                {
                    if (this is BaseExplosionPotion && Amount > 1)
                    {
                        BasePotion pot = (BasePotion)Activator.CreateInstance(this.GetType());

                        if (pot != null)
                        {
                            Amount--;

                            if (from.Backpack != null && !from.Backpack.Deleted)
                            {
                                from.Backpack.DropItem(pot);
                            }
                            else
                            {
                                pot.MoveToWorld(from.Location, from.Map);
                            }
                            pot.Drink(from);
                        }
                    }
                    else
                    {
                        this.Drink(from);
                    }
                }
                else
                {
                    from.SendLocalizedMessage(502172); // You must have a free hand to drink a potion.
                }
            }
            else
            {
                from.SendLocalizedMessage(502138); // That is too far away for you to use
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2); // version

            writer.Write(m_CrafterSpecBonus);
            m_PotionEffect.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                    {
                        m_CrafterSpecBonus = reader.ReadDouble();
                        goto case 1;
                    }
                case 1:
                    {
                        m_CrafterSpecBonus = 1.0;
                        goto case 0;
                    }
                case 0:
                    {
                        m_PotionEffect = PotionEffect.Deserialize(reader);
                        break;
                    }
            }

            if (Stackable == false)
            {
                Stackable = true;
            }
        }

        public abstract void Drink(Mobile from);

        public static void PlayDrinkEffect(Mobile m)
        {
            m.RevealingAction();

            m.PlaySound(0x2D6);

            #region Dueling
            if (!Engines.ConPVP.DuelContext.IsFreeConsume(m))
                m.AddToBackpack(new Bottle());
            #endregion

            if (m.Body.IsHuman && !m.Mounted)
                m.Animate(34, 5, 1, true, false, 0);
        }

        public static int EnhancePotions(Mobile m)
        {
            int EP = AosAttributes.GetValue(m, AosAttribute.EnhancePotions);
            int skillBonus = m.Skills.Alchemy.Fixed / 330 * 10;

            if (Core.ML && EP > 50 && m.AccessLevel <= AccessLevel.Player)
                EP = 50;

            return (EP + skillBonus);
        }

        public TimeSpan Scale(Mobile m, TimeSpan v)
        {
            return TimeSpan.FromSeconds(v.TotalSeconds * m_CrafterSpecBonus);
        }

        public double Scale(Mobile m, double v)
        {
            return v * m_CrafterSpecBonus;
        }

        public int Scale(Mobile m, int v)
        {
            double product = (double)v * m_CrafterSpecBonus;
            return (int)product;
        }

        public override bool StackWith(Mobile from, Item dropped, bool playSound)
        {
            if (dropped is BasePotion && ((BasePotion)dropped).m_PotionEffect == m_PotionEffect && ((BasePotion)dropped).CrafterSpecBonus == m_CrafterSpecBonus)
            {
                return base.StackWith(from, dropped, playSound);
            }

            return false;
        }

        #region ICraftable Members

        public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue)
        {
            if (craftSystem is DefAlchemy)
            {
                if (from is PlayerMobile)
                {
                    PlayerMobile pm = from as PlayerMobile;
                    if (pm.Spec.SpecName == SpecName.Mage)
                    {
                        m_CrafterSpecBonus *= pm.Spec.Bonus;
                    }
                }

                Container pack = from.Backpack;

                if (pack != null)
                {
                    List<PotionKeg> kegs = pack.FindItemsByType<PotionKeg>();

                    for (int i = 0; i < kegs.Count; ++i)
                    {
                        PotionKeg keg = kegs[i];

                        if (keg == null)
                            continue;

                        if (keg.Held <= 0 || keg.Held >= 100)
                            continue;

                        if (keg.Type != PotionEffect)
                            continue;

                        ++keg.Held;

                        Consume();
                        from.AddToBackpack(new Bottle());

                        return -1; // signal placed in keg
                    }
                }
            }

            return 1;
        }

        #endregion
    }
}
