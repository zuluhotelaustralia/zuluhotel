using System;
using System.Collections.Generic;
using Server.Network;
using Server.Engines.Craft;
using Server.Engines.Magic;
using Server.Mobiles;
using ZuluContent.Zulu;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;
using AMA = Server.Items.ArmorMeditationAllowance;
using AMT = Server.Items.ArmorMaterialType;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;


namespace Server.Items
{
    public abstract class BaseArmor : BaseEquippableItem, IScissorable, ICraftable, IWearableDurability, IArmorRating,
        IRepairable, IResource
    {
        /* Armor internals work differently now (Jun 19 2003)
         *
         * The attributes defined below default to -1.
         * If the value is -1, the corresponding virtual 'Aos/Old' property is used.
         * If not, the attribute value itself is used. Here's the list:
         *  - ArmorBase
         *  - StrBonus
         *  - DexBonus
         *  - IntBonus
         *  - StrReq
         *  - DexReq
         *  - IntReq
         *  - MeditationAllowance
         */

        // Instance values. These values must are unique to each armor piece.
        private int m_HitPoints;
        private Mobile m_Crafter;
        private CraftResource m_Resource;

        // Overridable values. These values are provided to override the defaults which get defined in the individual armor scripts.
        private int m_ArmorBase = -1;
        private int m_StrBonus = -1, m_DexBonus = -1, m_IntBonus = -1;
        private int m_StrReq = -1, m_DexReq = -1, m_IntReq = -1;
        private AMA m_Meditate = (AMA) (-1);

        public virtual bool AllowMaleWearer
        {
            get { return true; }
        }

        public virtual bool AllowFemaleWearer
        {
            get { return true; }
        }

        public abstract AMT MaterialType { get; }

        public virtual int RevertArmorBase
        {
            get { return ArmorBase; }
        }

        public virtual int ArmorBase
        {
            get { return 0; }
        }

        public virtual AMA DefMedAllowance
        {
            get { return AMA.None; }
        }

        public virtual AMA DefaultMedAllowance
        {
            get { return DefMedAllowance; }
        }


        public virtual int DefaultStrBonus
        {
            get { return 0; }
        }

        public virtual int DefaultDexBonus
        {
            get { return 0; }
        }

        public virtual int DefaultIntBonus
        {
            get { return 0; }
        }

        public virtual int DefaultStrReq
        {
            get { return 0; }
        }

        public virtual int DefaultDexReq
        {
            get { return 0; }
        }

        public virtual int DefaultIntReq
        {
            get { return 0; }
        }

        public virtual double DefaultMagicEfficiencyPenalty
        {
            get { return 0.0; }
        }

        public virtual bool CanFortify
        {
            get { return true; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public CreatureType CreatureProtection
        {
            get => Enchantments.Get((SlayerHit e) => e.Type);
            set => Enchantments.Set((SlayerHit e) => e.Type = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int BaseArmorRating
        {
            get { return m_ArmorBase == -1 ? ArmorBase : m_ArmorBase; }
            set
            {
                m_ArmorBase = value;
                Invalidate();
            }
        }

        public double BaseArmorRatingScaled => BaseArmorRating * ArmorScalar;

        public virtual double ArmorRating
        {
            get
            {
                var ar = BaseArmorRating;

                if (ProtectionLevel != ArmorProtectionLevel.Regular)
                    ar += 10 + 5 * (int) ProtectionLevel;

                ar = (int) (ar * Quality);

                return ScaleArmorByDurability(ar);
            }
        }

        public double ArmorRatingScaled => ArmorRating * ArmorScalar;


        [CommandProperty(AccessLevel.GameMaster)]
        public int StrRequirement
        {
            get { return m_StrReq == -1 ? DefaultStrReq : m_StrReq; }
            set { m_StrReq = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int DexRequirement
        {
            get { return m_DexReq == -1 ? DefaultDexReq : m_DexReq; }
            set { m_DexReq = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int IntRequirement
        {
            get { return m_IntReq == -1 ? DefaultIntReq : m_IntReq; }
            set { m_IntReq = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PlayerConstructed { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get { return m_Resource; }
            set
            {
                if (m_Resource != value)
                {
                    UnscaleDurability();

                    m_Resource = value;

                    if (CraftItem.RetainsColor(GetType()))
                    {
                        Hue = CraftResources.GetHue(m_Resource);
                    }

                    Invalidate();

                    ScaleDurability();
                }
            }
        }

        public virtual double ArmorScalar
        {
            get
            {
                if (!ArmorScalars.TryGetValue(BodyPosition, out var scalar))
                    scalar = 1.0;
                return scalar;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxHitPoints { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int HitPoints
        {
            get { return m_HitPoints; }
            set
            {
                if (value != m_HitPoints && MaxHitPoints > 0)
                {
                    m_HitPoints = value;

                    if (m_HitPoints < 0)
                        Delete();
                    else if (m_HitPoints > MaxHitPoints)
                        m_HitPoints = MaxHitPoints;
                }
            }
        }


        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Crafter
        {
            get { return m_Crafter; }
            set { m_Crafter = value; }
        }


        [CommandProperty(AccessLevel.GameMaster)]
        public MarkQuality Mark
        {
            get => Enchantments.Get((ItemMark e) => (MarkQuality) e.Value);
            set
            {
                UnscaleDurability();
                Enchantments.Set((ItemMark e) => e.Value = (int) value);
                Invalidate();
                ScaleDurability();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public double Quality
        {
            get => Enchantments.Get((ItemQuality e) => e.Value);
            set => Enchantments.Set((ItemQuality e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public ArmorDurabilityLevel Durability
        {
            get => Enchantments.Get((DurabilityBonus e) => (ArmorDurabilityLevel) e.Value);
            set
            {
                UnscaleDurability();
                Enchantments.Set((DurabilityBonus e) => e.Value = (int) value);
                ScaleDurability();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public ArmorProtectionLevel ProtectionLevel
        {
            get => Enchantments.Get((ArmorProtection e) => e.Value);
            set
            {
                Enchantments.Set((ArmorProtection e) => e.Value = value);
                Invalidate();
            }
        }

        public int ComputeStatReq(StatType type)
        {
            return type switch
            {
                StatType.Str => StrRequirement,
                StatType.Dex => DexRequirement,
                _ => IntRequirement
            };
        }

        public int ComputeStatBonus(StatType type)
        {
            return type switch
            {
                StatType.Str => StrBonus,
                StatType.Dex => DexBonus,
                _ => IntBonus
            };
        }

        public virtual int InitMinHits
        {
            get { return 0; }
        }

        public virtual int InitMaxHits
        {
            get { return 0; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public ArmorBodyType BodyPosition
        {
            get
            {
                return Layer switch
                {
                    Layer.Neck => ArmorBodyType.Gorget,
                    Layer.TwoHanded => ArmorBodyType.Shield,
                    Layer.Gloves => ArmorBodyType.Gloves,
                    Layer.Helm => ArmorBodyType.Helmet,
                    Layer.Arms => ArmorBodyType.Arms,
                    Layer.InnerLegs => ArmorBodyType.InnerLegs,
                    Layer.OuterLegs => ArmorBodyType.OuterLegs,
                    Layer.Pants => ArmorBodyType.Pants,
                    Layer.InnerTorso => ArmorBodyType.InnerChest,
                    Layer.OuterTorso => ArmorBodyType.OuterChest,
                    Layer.Shirt => ArmorBodyType.Shirt,
                    _ => ArmorBodyType.Necklace
                };
            }
        }

        public void UnscaleDurability()
        {
            int scale = 100 + GetDurabilityBonus();

            m_HitPoints = (m_HitPoints * 100 + (scale - 1)) / scale;
            MaxHitPoints = (MaxHitPoints * 100 + (scale - 1)) / scale;
        }

        public void ScaleDurability()
        {
            int scale = 100 + GetDurabilityBonus();

            m_HitPoints = (m_HitPoints * scale + 99) / 100;
            MaxHitPoints = (MaxHitPoints * scale + 99) / 100;
        }

        public int GetDurabilityBonus()
        {
            int bonus = Durability switch
            {
                ArmorDurabilityLevel.Durable => 20,
                ArmorDurabilityLevel.Substantial => 50,
                ArmorDurabilityLevel.Massive => 70,
                ArmorDurabilityLevel.Fortified => 100,
                ArmorDurabilityLevel.Tempered => 120,
                _ => 0
            };

            if (Mark == MarkQuality.Exceptional)
                bonus += 20;

            return bonus;
        }

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack.
                return false;
            }

            CraftSystem system = DefTailoring.CraftSystem;

            CraftItem item = system.CraftItems.SearchFor(GetType());

            if (item != null && item.Resources.Count == 1 && item.Resources[0].Amount >= 2)
            {
                try
                {
                    Item res = (Item) Activator.CreateInstance(CraftResources.GetInfo(m_Resource).ResourceTypes[0]);

                    ScissorHelper(from, res, PlayerConstructed ? item.Resources[0].Amount / 2 : 1);
                    return true;
                }
                catch
                {
                }
            }

            from.SendLocalizedMessage(502440); // Scissors can not be used on that to produce anything.
            return false;
        }

        public static Dictionary<ArmorBodyType, double> ArmorScalars = new()
        {
            {ArmorBodyType.InnerChest, 0.4},
            {ArmorBodyType.Arms, 0.15},
            {ArmorBodyType.InnerLegs, 0.15},
            {ArmorBodyType.Gorget, 0.07},
            {ArmorBodyType.Gloves, 0.07},
            {ArmorBodyType.Helmet, 0.15},
            {ArmorBodyType.Shield, 0.56}
        };

        public static void ValidateMobile(Mobile m)
        {
            for (int i = m.Items.Count - 1; i >= 0; --i)
            {
                if (i >= m.Items.Count)
                    continue;

                Item item = m.Items[i];

                if (item is BaseArmor)
                {
                    BaseArmor armor = (BaseArmor) item;

                    if (armor.RequiredRace != null && m.Race != armor.RequiredRace)
                    {
                        if (armor.RequiredRace == Race.Elf)
                            m.SendLocalizedMessage(1072203); // Only Elves may use this.
                        else
                            m.SendMessage("Only {0} may use this.", armor.RequiredRace.PluralName);

                        m.AddToBackpack(armor);
                    }
                    else if (!armor.AllowMaleWearer && !m.Female && m.AccessLevel < AccessLevel.GameMaster)
                    {
                        if (armor.AllowFemaleWearer)
                            m.SendLocalizedMessage(1010388); // Only females can wear this.
                        else
                            m.SendMessage("You may not wear this.");

                        m.AddToBackpack(armor);
                    }
                    else if (!armor.AllowFemaleWearer && m.Female && m.AccessLevel < AccessLevel.GameMaster)
                    {
                        if (armor.AllowMaleWearer)
                            m.SendLocalizedMessage(1063343); // Only males can wear this.
                        else
                            m.SendMessage("You may not wear this.");

                        m.AddToBackpack(armor);
                    }
                }
            }
        }

        public virtual double ScaleArmorByDurability(double armor)
        {
            return armor * ((double) HitPoints / (double) MaxHitPoints);
        }

        protected void Invalidate()
        {
            if (Parent is Mobile mp)
                mp.Delta(MobileDelta.Armor); // Tell them armor rating has changed
        }

        public BaseArmor(Serial serial) : base(serial)
        {
        }

        private static void SetSaveFlag(ref SaveFlag flags, SaveFlag toSet, bool setIf)
        {
            if (setIf)
                flags |= toSet;
        }

        private static bool GetSaveFlag(SaveFlag flags, SaveFlag toGet)
        {
            return (flags & toGet) != 0;
        }

        [Flags]
        private enum SaveFlag
        {
            None = 0x00000000,
            Attributes = 0x00000001,
            ArmorAttributes = 0x00000002,
            PhysicalBonus = 0x00000004,
            FireBonus = 0x00000008,
            ColdBonus = 0x00000010,
            PoisonBonus = 0x00000020,
            EnergyBonus = 0x00000040,
            Identified = 0x00000080,
            MaxHitPoints = 0x00000100,
            HitPoints = 0x00000200,
            Crafter = 0x00000400,
            Mark = 0x00000800,
            Durability = 0x00001000,
            Protection = 0x00002000,
            Resource = 0x00004000,
            BaseArmor = 0x00008000,
            StrBonus = 0x00010000,
            DexBonus = 0x00020000,
            IntBonus = 0x00040000,
            StrReq = 0x00080000,
            DexReq = 0x00100000,
            IntReq = 0x00200000,
            MedAllowance = 0x00400000,
            NewMagicalProperties = 0x00800000,
            PlayerConstructed = 0x01000000,
            ICraftable = 0x02000000
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 9); // version

            SaveFlag flags = SaveFlag.None;

            SetSaveFlag(ref flags, SaveFlag.ICraftable, true);
            SetSaveFlag(ref flags, SaveFlag.NewMagicalProperties, true);

            if (!GetSaveFlag(flags, SaveFlag.ICraftable))
            {
                SetSaveFlag(ref flags, SaveFlag.PlayerConstructed, PlayerConstructed);
                SetSaveFlag(ref flags, SaveFlag.Crafter, Crafter != null);
            }

            // Legacy property saving
            if (!GetSaveFlag(flags, SaveFlag.NewMagicalProperties))
            {
                SetSaveFlag(ref flags, SaveFlag.Identified, Identified != false);
                SetSaveFlag(ref flags, SaveFlag.Mark, Mark != MarkQuality.Regular);
                SetSaveFlag(ref flags, SaveFlag.Durability, Durability != ArmorDurabilityLevel.Regular);
                SetSaveFlag(ref flags, SaveFlag.Protection, ProtectionLevel != ArmorProtectionLevel.Regular);
                SetSaveFlag(ref flags, SaveFlag.MedAllowance, m_Meditate != (AMA) (-1));

                SetSaveFlag(ref flags, SaveFlag.StrBonus, m_StrBonus != -1);
                SetSaveFlag(ref flags, SaveFlag.DexBonus, m_DexBonus != -1);
                SetSaveFlag(ref flags, SaveFlag.IntBonus, m_IntBonus != -1);
            }

            SetSaveFlag(ref flags, SaveFlag.MaxHitPoints, MaxHitPoints != 0);
            SetSaveFlag(ref flags, SaveFlag.HitPoints, m_HitPoints != 0);
            SetSaveFlag(ref flags, SaveFlag.Resource, m_Resource != DefaultResource);
            SetSaveFlag(ref flags, SaveFlag.BaseArmor, m_ArmorBase != -1);
            SetSaveFlag(ref flags, SaveFlag.StrReq, m_StrReq != -1);
            SetSaveFlag(ref flags, SaveFlag.DexReq, m_DexReq != -1);
            SetSaveFlag(ref flags, SaveFlag.IntReq, m_IntReq != -1);

            writer.WriteEncodedInt((int) flags);

            if (GetSaveFlag(flags, SaveFlag.ICraftable))
                ICraftable.Serialize(writer, this);

            if (GetSaveFlag(flags, SaveFlag.MaxHitPoints))
                writer.WriteEncodedInt((int) MaxHitPoints);

            if (GetSaveFlag(flags, SaveFlag.HitPoints))
                writer.WriteEncodedInt((int) m_HitPoints);

            if (GetSaveFlag(flags, SaveFlag.Crafter))
                writer.Write((Mobile) m_Crafter);

            if (GetSaveFlag(flags, SaveFlag.Mark))
                writer.WriteEncodedInt((int) Mark);

            if (GetSaveFlag(flags, SaveFlag.Durability))
                writer.WriteEncodedInt((int) Durability);

            if (GetSaveFlag(flags, SaveFlag.Protection))
                writer.WriteEncodedInt((int) ProtectionLevel);

            if (GetSaveFlag(flags, SaveFlag.Resource))
                writer.WriteEncodedInt((int) m_Resource);

            if (GetSaveFlag(flags, SaveFlag.BaseArmor))
                writer.WriteEncodedInt((int) m_ArmorBase);

            if (GetSaveFlag(flags, SaveFlag.StrBonus))
                writer.WriteEncodedInt((int) m_StrBonus);

            if (GetSaveFlag(flags, SaveFlag.DexBonus))
                writer.WriteEncodedInt((int) m_DexBonus);

            if (GetSaveFlag(flags, SaveFlag.IntBonus))
                writer.WriteEncodedInt((int) m_IntBonus);

            if (GetSaveFlag(flags, SaveFlag.StrReq))
                writer.WriteEncodedInt((int) m_StrReq);

            if (GetSaveFlag(flags, SaveFlag.DexReq))
                writer.WriteEncodedInt((int) m_DexReq);

            if (GetSaveFlag(flags, SaveFlag.IntReq))
                writer.WriteEncodedInt((int) m_IntReq);

            if (GetSaveFlag(flags, SaveFlag.MedAllowance))
                writer.WriteEncodedInt((int) m_Meditate);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            SaveFlag flags = (SaveFlag) reader.ReadEncodedInt();

            switch (version)
            {
                case 9:
                    if (GetSaveFlag(flags, SaveFlag.ICraftable))
                        ICraftable.Deserialize(reader, this);
                    goto case 8;
                case 8:
                case 7:
                case 6:
                case 5:
                {
                    if (GetSaveFlag(flags, SaveFlag.Identified))
                        Identified = version >= 7 || reader.ReadBool();

                    if (GetSaveFlag(flags, SaveFlag.MaxHitPoints))
                        MaxHitPoints = reader.ReadEncodedInt();

                    if (GetSaveFlag(flags, SaveFlag.HitPoints))
                        m_HitPoints = reader.ReadEncodedInt();

                    if (GetSaveFlag(flags, SaveFlag.Crafter))
                        m_Crafter = reader.ReadEntity<Mobile>();

                    if (GetSaveFlag(flags, SaveFlag.Mark))
                        Mark = (MarkQuality) reader.ReadEncodedInt();
                    else if (!GetSaveFlag(flags, SaveFlag.NewMagicalProperties))
                        Mark = MarkQuality.Regular;

                    if (version == 5 && Mark == MarkQuality.Low)
                        Mark = MarkQuality.Regular;

                    if (GetSaveFlag(flags, SaveFlag.Durability))
                    {
                        Durability = (ArmorDurabilityLevel) reader.ReadEncodedInt();

                        if (Durability > ArmorDurabilityLevel.Tempered)
                            Durability = ArmorDurabilityLevel.Durable;
                    }

                    if (GetSaveFlag(flags, SaveFlag.Protection))
                    {
                        ProtectionLevel = (ArmorProtectionLevel) reader.ReadEncodedInt();

                        if (ProtectionLevel > ArmorProtectionLevel.Invulnerability)
                            ProtectionLevel = ArmorProtectionLevel.Defense;
                    }

                    if (GetSaveFlag(flags, SaveFlag.Resource))
                        m_Resource = (CraftResource) reader.ReadEncodedInt();
                    else
                        m_Resource = DefaultResource;

                    if (m_Resource == CraftResource.None)
                        m_Resource = DefaultResource;

                    if (GetSaveFlag(flags, SaveFlag.BaseArmor))
                        m_ArmorBase = reader.ReadEncodedInt();
                    else
                        m_ArmorBase = -1;

                    if (GetSaveFlag(flags, SaveFlag.StrBonus))
                        m_StrBonus = reader.ReadEncodedInt();
                    else
                        m_StrBonus = -1;

                    if (GetSaveFlag(flags, SaveFlag.DexBonus))
                        m_DexBonus = reader.ReadEncodedInt();
                    else
                        m_DexBonus = -1;

                    if (GetSaveFlag(flags, SaveFlag.IntBonus))
                        m_IntBonus = reader.ReadEncodedInt();
                    else
                        m_IntBonus = -1;

                    if (GetSaveFlag(flags, SaveFlag.StrReq))
                        m_StrReq = reader.ReadEncodedInt();
                    else
                        m_StrReq = -1;

                    if (GetSaveFlag(flags, SaveFlag.DexReq))
                        m_DexReq = reader.ReadEncodedInt();
                    else
                        m_DexReq = -1;

                    if (GetSaveFlag(flags, SaveFlag.IntReq))
                        m_IntReq = reader.ReadEncodedInt();
                    else
                        m_IntReq = -1;

                    if (GetSaveFlag(flags, SaveFlag.MedAllowance))
                        m_Meditate = (AMA) reader.ReadEncodedInt();
                    else
                        m_Meditate = (AMA) (-1);

                    if (GetSaveFlag(flags, SaveFlag.PlayerConstructed))
                        PlayerConstructed = true;
                    break;
                }
                case 4:
                case 3:
                case 2:
                case 1:
                {
                    Identified = reader.ReadBool();
                    goto case 0;
                }
                case 0:
                {
                    m_ArmorBase = reader.ReadInt();
                    MaxHitPoints = reader.ReadInt();
                    m_HitPoints = reader.ReadInt();
                    m_Crafter = reader.ReadEntity<Mobile>();
                    Mark = (MarkQuality) reader.ReadInt();
                    Durability = (ArmorDurabilityLevel) reader.ReadInt();
                    ProtectionLevel = (ArmorProtectionLevel) reader.ReadInt();

                    AMT mat = (AMT) reader.ReadInt();

                    if (m_ArmorBase == RevertArmorBase)
                        m_ArmorBase = -1;

                    if (version >= 2)
                    {
                        m_Resource = (CraftResource) reader.ReadInt();
                    }
                    else
                    {
                        OreInfo info;

                        switch (reader.ReadInt())
                        {
                            default:
                            case 0:
                                info = OreInfo.Iron;
                                break;
                            case 1:
                                info = OreInfo.DullCopper;
                                break;
                            case 2:
                                info = OreInfo.ShadowIron;
                                break;
                            case 3:
                                info = OreInfo.Copper;
                                break;
                            case 4:
                                info = OreInfo.Bronze;
                                break;
                            case 5:
                                info = OreInfo.Gold;
                                break;
                            case 6:
                                info = OreInfo.Agapite;
                                break;
                            case 7:
                                info = OreInfo.Verite;
                                break;
                            case 8:
                                info = OreInfo.Valorite;
                                break;
                        }

                        m_Resource = CraftResources.GetFromOreInfo(info, mat);
                    }

                    m_StrBonus = reader.ReadInt();
                    m_DexBonus = reader.ReadInt();
                    m_IntBonus = reader.ReadInt();
                    m_StrReq = reader.ReadInt();
                    m_DexReq = reader.ReadInt();
                    m_IntReq = reader.ReadInt();

                    if (m_StrBonus == DefaultStrBonus)
                        m_StrBonus = -1;

                    if (m_DexBonus == DefaultDexBonus)
                        m_DexBonus = -1;

                    if (m_IntBonus == DefaultIntBonus)
                        m_IntBonus = -1;

                    if (m_StrReq == DefaultStrReq)
                        m_StrReq = -1;

                    if (m_DexReq == DefaultDexReq)
                        m_DexReq = -1;

                    if (m_IntReq == DefaultIntReq)
                        m_IntReq = -1;

                    m_Meditate = (AMA) reader.ReadInt();

                    if (m_Meditate == DefaultMedAllowance)
                        m_Meditate = (AMA) (-1);

                    if (m_Resource == CraftResource.None)
                    {
                        if (mat == ArmorMaterialType.Studded || mat == ArmorMaterialType.Leather)
                            m_Resource = CraftResource.RegularLeather;
                        else
                            m_Resource = CraftResource.Iron;
                    }

                    if (MaxHitPoints == 0 && m_HitPoints == 0)
                        m_HitPoints = MaxHitPoints = Utility.RandomMinMax(InitMinHits, InitMaxHits);

                    break;
                }
            }

            if (Parent is Mobile m)
                m.CheckStatTimers();

            if (version < 7)
                PlayerConstructed = true; // we don't know, so, assume it's crafted
        }

        public virtual CraftResource DefaultResource
        {
            get { return CraftResource.Iron; }
        }

        public BaseArmor(int itemID) : base(itemID)
        {
            StrBonus = DefaultStrBonus;
            DexBonus = DefaultDexBonus;
            IntBonus = DefaultIntBonus;

            MagicEfficiencyPenalty = DefaultMagicEfficiencyPenalty;

            Mark = MarkQuality.Regular;
            Quality = 1.0;
            Durability = ArmorDurabilityLevel.Regular;
            m_Crafter = null;

            m_Resource = DefaultResource;
            Hue = CraftResources.GetHue(m_Resource);

            m_HitPoints = MaxHitPoints = Utility.RandomMinMax(InitMinHits, InitMaxHits);

            Layer = (Layer) ItemData.Quality;
        }

        public virtual Race RequiredRace
        {
            get { return null; }
        }

        public override bool CanEquip(Mobile from)
        {
            if (from.AccessLevel < AccessLevel.GameMaster)
            {
                if (RequiredRace != null && from.Race != RequiredRace)
                {
                    if (RequiredRace == Race.Elf)
                        from.SendLocalizedMessage(1072203); // Only Elves may use this.
                    else
                        from.SendMessage("Only {0} may use this.", RequiredRace.PluralName);

                    return false;
                }
                else if (!AllowMaleWearer && !from.Female)
                {
                    if (AllowFemaleWearer)
                        from.SendLocalizedMessage(1010388); // Only females can wear this.
                    else
                        from.SendMessage("You may not wear this.");

                    return false;
                }
                else if (!AllowFemaleWearer && from.Female)
                {
                    if (AllowMaleWearer)
                        from.SendLocalizedMessage(1063343); // Only males can wear this.
                    else
                        from.SendMessage("You may not wear this.");

                    return false;
                }
                else
                {
                    int strBonus = ComputeStatBonus(StatType.Str), strReq = ComputeStatReq(StatType.Str);
                    int dexBonus = ComputeStatBonus(StatType.Dex), dexReq = ComputeStatReq(StatType.Dex);
                    int intBonus = ComputeStatBonus(StatType.Int), intReq = ComputeStatReq(StatType.Int);

                    if (from.Dex < dexReq || @from.Dex + dexBonus < 1)
                    {
                        from.SendLocalizedMessage(502077); // You do not have enough dexterity to equip this item.
                        return false;
                    }
                    else if (from.Str < strReq || @from.Str + strBonus < 1)
                    {
                        from.SendLocalizedMessage(500213); // You are not strong enough to equip that.
                        return false;
                    }
                    else if (from.Int < intReq || @from.Int + intBonus < 1)
                    {
                        from.SendMessage("You are not smart enough to equip that.");
                        return false;
                    }
                }
            }

            return base.CanEquip(from);
        }

        public override bool CheckPropertyConflict(Mobile m)
        {
            if (base.CheckPropertyConflict(m))
                return true;

            if (Layer == Layer.Pants)
                return m.FindItemOnLayer(Layer.InnerLegs) != null;

            if (Layer == Layer.Shirt)
                return m.FindItemOnLayer(Layer.InnerTorso) != null;

            return false;
        }

        public override void OnAdded(IEntity parent)
        {
            base.OnAdded(parent);

            if (parent is Mobile m)
            {
                m.CheckStatTimers();
                m.Delta(MobileDelta.WeaponDamage);
                m.Delta(MobileDelta.Armor);
            }
        }

        public override void OnRemoved(IEntity parent)
        {
            if (parent is Mobile m)
            {
                m.Delta(MobileDelta.Armor); // Tell them armor rating has changed
                m.CheckStatTimers();
            }

            base.OnRemoved(parent);
        }

        public virtual int OnHit(BaseWeapon weapon, int damageTaken)
        {
            double halfAr = ArmorRating / 2.0;
            int absorbed = (int) (halfAr + halfAr * Utility.RandomDouble());

            damageTaken -= absorbed;
            if (damageTaken < 0)
                damageTaken = 0;

            if (absorbed < 2)
                absorbed = 2;

            if (25 > Utility.Random(100)) // 25% chance to lower durability
            {
                int wear;

                if (weapon.Type == WeaponType.Bashing)
                    wear = absorbed / 2;
                else
                    wear = Utility.Random(2);

                if (wear > 0 && MaxHitPoints > 0)
                {
                    if (m_HitPoints >= wear)
                    {
                        HitPoints -= wear;
                        wear = 0;
                    }
                    else
                    {
                        wear -= HitPoints;
                        HitPoints = 0;
                    }

                    if (wear > 0)
                    {
                        if (MaxHitPoints > wear)
                        {
                            MaxHitPoints -= wear;

                            if (Parent is Mobile mobile) // Your equipment is severely damaged.
                                mobile.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1061121);
                        }
                        else
                        {
                            Delete();
                        }
                    }
                }
            }

            return damageTaken;
        }

        private string GetNameString()
        {
            return Name ?? $"#{LabelNumber}";
        }

        [Hue, CommandProperty(AccessLevel.GameMaster)]
        public override int Hue
        {
            get { return base.Hue; }
            set { base.Hue = value; }
        }
        
        public override void OnSingleClick(Mobile from)
        {
            HandleSingleClick(this, from);
        }

        #region ICraftable Members

        public int OnCraft(int mark, double quality, bool makersMark, Mobile from, CraftSystem craftSystem,
            Type typeRes,
            BaseTool tool, CraftItem craftItem, int resHue)
        {
            Mark = (MarkQuality) mark;

            if (makersMark)
                Crafter = from;

            var resourceType = typeRes;

            if (resourceType == null)
                resourceType = craftItem.Resources[0].ItemType;

            Resource = CraftResources.GetFromType(resourceType);

            PlayerConstructed = true;

            var resEnchantments = CraftResources.GetEnchantments(Resource);
            foreach (var (key, value) in resEnchantments)
            {
                Enchantments.SetFromResourceType(key, value);
            }

            Quality = quality;

            MaxHitPoints = (int) (MaxHitPoints * quality);
            HitPoints = MaxHitPoints;

            CraftContext context = craftSystem.GetContext(from);

            if (context != null && context.DoNotColor)
                Hue = 0;

            return mark;
        }

        #endregion
    }
}