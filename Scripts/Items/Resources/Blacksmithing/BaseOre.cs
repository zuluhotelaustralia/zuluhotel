// GENERATED FILE DO NOT MODIFY BY HAND.

using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Engines.Craft;
using Server.Mobiles;

namespace Server.Items
{
    public abstract class BaseOre : Item
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource { get; set; }
        
        public override string DefaultName => $"{CraftResources.GetName(Resource)} ore";
        
        public abstract BaseIngot GetIngot();

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version

            writer.Write((int) Resource);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                {
                    Resource = (CraftResource) reader.ReadInt();
                    break;
                }
                case 0:
                {
                    throw new System.Exception("Unsupported ore item, skipping.");
                    break;
                }
            }
        }

        private static int RandomSize()
        {
            /*  double rand = Utility.RandomDouble();

            if ( rand < 0.12 )
            return 0x19B7;
            else if ( rand < 0.18 )
            return 0x19B8;
            else if ( rand < 0.25 )
            return 0x19BA;
            else */
            return 0x19B9;
        }

        public BaseOre(CraftResource resource) : this(resource, 1)
        {
        }

        public BaseOre(CraftResource resource, int amount) : base(RandomSize())
        {
            Stackable = true;
            Amount = amount;
            Hue = CraftResources.GetHue(resource);

            Resource = resource;
        }

        public BaseOre(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (RootParent is BaseCreature)
            {
                from.SendLocalizedMessage(500447); // That is not accessible
            }
            else if (from.InRange(this.GetWorldLocation(), 2))
            {
                from.SendLocalizedMessage(
                    501971); // Select the forge on which to smelt the ore, or another pile of ore with which to combine it.
                from.Target = new InternalTarget(this);
            }
            else
            {
                from.SendLocalizedMessage(501976); // The ore is too far away.
            }
        }

        private class InternalTarget : Target
        {
            private BaseOre m_Ore;

            public InternalTarget(BaseOre ore) : base(2, false, TargetFlags.None)
            {
                m_Ore = ore;
            }

            private bool IsForge(object obj)
            {
                if (obj.GetType().IsDefined(typeof(ForgeAttribute), false))
                    return true;

                int itemID = 0;

                if (obj is Item)
                    itemID = ((Item) obj).ItemID;
                else if (obj is StaticTarget)
                    itemID = ((StaticTarget) obj).ItemID;

                return (itemID == 4017 || (itemID >= 6522 && itemID <= 6569));
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Ore.Deleted)
                    return;

                if (!from.InRange(m_Ore.GetWorldLocation(), 2))
                {
                    from.SendLocalizedMessage(501976); // The ore is too far away.
                    return;
                }

                #region Combine Ore

                if (targeted is BaseOre)
                {
                    BaseOre ore = (BaseOre) targeted;

                    if (!ore.Movable)
                    {
                        return;
                    }
                    else if (m_Ore == ore)
                    {
                        from.SendLocalizedMessage(501972); // Select another pile or ore with which to combine this.
                        from.Target = new InternalTarget(ore);
                        return;
                    }
                    else if (ore.Resource != m_Ore.Resource)
                    {
                        from.SendLocalizedMessage(501979); // You cannot combine ores of different metals.
                        return;
                    }

                    int worth = ore.Amount;

                    if (ore.ItemID == 0x19B9)
                        worth *= 8;
                    else if (ore.ItemID == 0x19B7)
                        worth *= 2;
                    else
                        worth *= 4;

                    int sourceWorth = m_Ore.Amount;

                    if (m_Ore.ItemID == 0x19B9)
                        sourceWorth *= 8;
                    else if (m_Ore.ItemID == 0x19B7)
                        sourceWorth *= 2;
                    else
                        sourceWorth *= 4;

                    worth += sourceWorth;

                    int plusWeight = 0;
                    int newID = ore.ItemID;

                    if (ore.DefaultWeight != m_Ore.DefaultWeight)
                    {
                        if (ore.ItemID == 0x19B7 || m_Ore.ItemID == 0x19B7)
                        {
                            newID = 0x19B7;
                        }
                        else if (ore.ItemID == 0x19B9)
                        {
                            newID = m_Ore.ItemID;
                            plusWeight = ore.Amount * 2;
                        }
                        else
                        {
                            plusWeight = m_Ore.Amount * 2;
                        }
                    }

                    if ((ore.ItemID == 0x19B9 && worth > 120000) ||
                        ((ore.ItemID == 0x19B8 || ore.ItemID == 0x19BA) && worth > 60000) ||
                        (ore.ItemID == 0x19B7 && worth > 30000))
                    {
                        from.SendLocalizedMessage(1062844); // There is too much ore to combine.
                        return;
                    }
                    else if (ore.RootParent is Mobile && (plusWeight + ((Mobile) ore.RootParent).Backpack.TotalWeight) >
                        ((Mobile) ore.RootParent).Backpack.MaxWeight)
                    {
                        from.SendLocalizedMessage(501978); // The weight is too great to combine in a container.
                        return;
                    }

                    ore.ItemID = newID;

                    if (ore.ItemID == 0x19B9)
                        ore.Amount = worth / 8;
                    else if (ore.ItemID == 0x19B7)
                        ore.Amount = worth / 2;
                    else
                        ore.Amount = worth / 4;

                    m_Ore.Delete();
                    return;
                }

                #endregion

                if (IsForge(targeted))
                {
                    double difficulty;

                    switch (m_Ore.Resource)
                    {
                        default:
                            difficulty = 50.0;
                            break;
                        case CraftResource.Iron:
                            difficulty = 0.0;
                            break;
                        case CraftResource.Gold:
                            difficulty = 1.0;
                            break;
                        case CraftResource.Spike:
                            difficulty = 5.0;
                            break;
                        case CraftResource.Fruity:
                            difficulty = 10.0;
                            break;
                        case CraftResource.Bronze:
                            difficulty = 15.0;
                            break;
                        case CraftResource.IceRock:
                            difficulty = 20.0;
                            break;
                        case CraftResource.BlackDwarf:
                            difficulty = 25.0;
                            break;
                        case CraftResource.DullCopper:
                            difficulty = 30.0;
                            break;
                        case CraftResource.Platinum:
                            difficulty = 35.0;
                            break;
                        case CraftResource.SilverRock:
                            difficulty = 40.0;
                            break;
                        case CraftResource.DarkPagan:
                            difficulty = 45.0;
                            break;
                        case CraftResource.Copper:
                            difficulty = 50.0;
                            break;
                        case CraftResource.Mystic:
                            difficulty = 55.0;
                            break;
                        case CraftResource.Spectral:
                            difficulty = 60.0;
                            break;
                        case CraftResource.OldBritain:
                            difficulty = 65.0;
                            break;
                        case CraftResource.Onyx:
                            difficulty = 70.0;
                            break;
                        case CraftResource.RedElven:
                            difficulty = 75.0;
                            break;
                        case CraftResource.Undead:
                            difficulty = 80.0;
                            break;
                        case CraftResource.Pyrite:
                            difficulty = 85.0;
                            break;
                        case CraftResource.Virginity:
                            difficulty = 90.0;
                            break;
                        case CraftResource.Malachite:
                            difficulty = 95.0;
                            break;
                        case CraftResource.Lavarock:
                            difficulty = 97.0;
                            break;
                        case CraftResource.Azurite:
                            difficulty = 98.0;
                            break;
                        case CraftResource.Dripstone:
                            difficulty = 100.0;
                            break;
                        case CraftResource.Executor:
                            difficulty = 104.0;
                            break;
                        case CraftResource.Peachblue:
                            difficulty = 108.0;
                            break;
                        case CraftResource.Destruction:
                            difficulty = 112.0;
                            break;
                        case CraftResource.Anra:
                            difficulty = 116.0;
                            break;
                        case CraftResource.Crystal:
                            difficulty = 119.0;
                            break;
                        case CraftResource.Doom:
                            difficulty = 122.0;
                            break;
                        case CraftResource.Goddess:
                            difficulty = 125.0;
                            break;
                        case CraftResource.NewZulu:
                            difficulty = 129.0;
                            break;
                        case CraftResource.EbonTwilightSapphire:
                            difficulty = 130.0;
                            break;
                        case CraftResource.DarkSableRuby:
                            difficulty = 130.0;
                            break;
                        case CraftResource.RadiantNimbusDiamond:
                            difficulty = 140.0;
                            break;
                    }

                    double minSkill = difficulty - 25.0;
                    double maxSkill = difficulty + 25.0;

                    if (difficulty > 50.0 && difficulty > from.Skills[SkillName.Mining].Value)
                    {
                        from.SendLocalizedMessage(501986); // You have no idea how to smelt this strange ore!
                        return;
                    }

                    if (m_Ore.ItemID == 0x19B7 && m_Ore.Amount < 2)
                    {
                        from.SendLocalizedMessage(
                            501987); // There is not enough metal-bearing ore in this pile to make an ingot.
                        return;
                    }

                    if (from.CheckTargetSkill(SkillName.Mining, targeted, minSkill, maxSkill))
                    {
                        int toConsume = m_Ore.Amount;

                        if (toConsume <= 0)
                        {
                            from.SendLocalizedMessage(
                                501987); // There is not enough metal-bearing ore in this pile to make an ingot.
                        }
                        else
                        {
                            if (toConsume > 30000)
                                toConsume = 30000;

                            int ingotAmount;

                            if (m_Ore.ItemID == 0x19B7)
                            {
                                ingotAmount = toConsume / 2;

                                if (toConsume % 2 != 0)
                                    --toConsume;
                            }
                            else if (m_Ore.ItemID == 0x19B9)
                            {
                                ingotAmount = toConsume * 2;
                            }
                            else
                            {
                                ingotAmount = toConsume;
                            }

                            BaseIngot ingot = m_Ore.GetIngot();
                            ingot.Amount = ingotAmount;

                            m_Ore.Consume(toConsume);
                            from.AddToBackpack(ingot);
                            //from.PlaySound( 0x57 );

                            from.SendLocalizedMessage(
                                501988); // You smelt the ore removing the impurities and put the metal in your backpack.
                        }
                    }
                    else
                    {
                        if (m_Ore.Amount < 2)
                        {
                            if (m_Ore.ItemID == 0x19B9)
                                m_Ore.ItemID = 0x19B8;
                            else
                                m_Ore.ItemID = 0x19B7;
                        }
                        else
                        {
                            m_Ore.Amount /= 2;
                        }

                        from.SendLocalizedMessage(
                            501990); // You burn away the impurities but are left with less useable metal.
                    }
                }
            }
        }
    }
}