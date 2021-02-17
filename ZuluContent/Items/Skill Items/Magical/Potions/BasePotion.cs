using System;
using Server.Engines.Craft;
using System.Collections.Generic;
using System.Linq;
using Server.Network;

namespace Server.Items
{
    public enum PotionEffect
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
        MaskOfDeath, // Mask of Death is not available in OSI but does exist in cliloc files
        MaskOfDeathGreater, // included in enumeration for compatability if later enabled by OSI
        ConfusionBlast,
        ConfusionBlastGreater,
        Invisibility,
        Parasitic,
        Darkglow,
        PhandelsFineIntellect,
        PhandelsFabulousIntellect,
        PhandelsFantasticIntellect,
        LesserMegoInvulnerability,
        MegoInvulnerability,
        GreaterMegoInvulnerability,
        GrandMageRefreshElixir,
        HomericMight,
        GreaterHomericMight,
        TamlaHeal,
        TaintsTransmutation,
        TaintsMajorTransmutation,
        Totem
    }

    public abstract class BasePotion : Item, ICraftable
    {
        public PotionEffect PotionEffect { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public abstract uint PotionStrength { get; set; }
        
        public override int LabelNumber => 1041314 + (int) PotionEffect;

        public BasePotion(int itemID, PotionEffect effect) : base(itemID)
        {
            PotionEffect = effect;

            Stackable = false;
            Weight = 1.0;
        }

        public BasePotion(Serial serial) : base(serial)
        {
        }

        public virtual bool RequireFreeHand => true;
        
        public static bool HasFreeHand(Mobile m)
        {
            Item handOne = m.FindItemOnLayer(Layer.OneHanded);
            Item handTwo = m.FindItemOnLayer(Layer.TwoHanded);

            if (handTwo is BaseWeapon)
                handOne = handTwo;
            if (handTwo is BaseRanged)
            {
                BaseRanged ranged = (BaseRanged) handTwo;

                if (ranged.Balanced)
                    return true;
            }

            return handOne == null || handTwo == null;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (!from.InRange(GetWorldLocation(), 1))
            {
                from.SendLocalizedMessage(502138); // That is too far away for you to use
                return;
            }

            if (RequireFreeHand && !HasFreeHand(from))
            {
                from.SendLocalizedMessage(502172); // You must have a free hand to drink a potion.
                return;
            }

            if (this is not BaseExplosionPotion || Amount <= 1)
            {
                Drink(from);
                return;
            }

            var pot = (BasePotion) Activator.CreateInstance(GetType());

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
        
        public override void OnSingleClick(Mobile from)
        {
            base.OnSingleClick(from);
            if(from.Skills[SkillName.Alchemy].Value >= 100)
                LabelTo(from, $"Strength: {PotionStrength}");
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 2); // version

            writer.Write(PotionStrength);
            writer.Write((int) PotionEffect);
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
                    PotionEffect = (PotionEffect) reader.ReadInt();
                    break;
                }
            }

            if (version == 0)
                Stackable = false;
        }

        public abstract void Drink(Mobile from);

        public static void PlayDrinkEffect(Mobile m)
        {
            m.RevealingAction();

            m.PlaySound(0x2D6);

            if (m.Body.IsHuman && !m.Mounted)
                m.Animate(34, 5, 1, true, false, 0);
        }

        public static int EnhancePotions(Mobile m)
        {
            int skillBonus = m.Skills.Alchemy.Fixed / 330 * 10;

            return skillBonus;
        }

        public static TimeSpan Scale(Mobile m, TimeSpan v)
        {
            return v;
        }

        public static double Scale(Mobile m, double v)
        {
            return v;
        }

        public static int Scale(Mobile m, int v)
        {
            return v;
        }

        public override bool StackWith(Mobile from, Item dropped, bool playSound)
        {
            if (dropped is BasePotion potion && potion.PotionEffect == PotionEffect)
                return base.StackWith(from, potion, playSound);

            return false;
        }

        #region ICraftable Members

        public Mobile Crafter { get; set; }
        public bool PlayerConstructed { get; set; }

        public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes,
            BaseTool tool, CraftItem craftItem, int resHue)
        {
            if (craftSystem is DefAlchemy)
            {
                var pack = from.Backpack;

                if (pack != null)
                {
                    if ((int) PotionEffect >= (int) PotionEffect.TaintsMajorTransmutation)
                        return 1;

                    var kegs = pack.FindItemsByType<PotionKeg>();

                    foreach (var keg in kegs)
                    {
                        if (keg == null)
                            continue;

                        if (keg.Held <= 0 || keg.Held >= 100)
                            continue;

                        if (keg.Type != PotionEffect || keg.PotionStrength != PotionStrength)
                            continue;

                        ++keg.Held;

                        Consume();

                        DefAlchemy.RecycleBottles(from, craftItem);
                        from.SendLocalizedMessage(500282); // You create the potion and pour it into the keg.

                        return -1; // signal placed in keg
                    }
                }
            }

            return 1;
        }

        #endregion
    }
}