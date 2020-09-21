using System;
using System.Collections.Generic;
using Scripts.Engines.Magic;
using Server.Network;
using Server.Engines.Craft;
using ZuluContent.Zulu.Engines.Magic;
using AMA = Server.Items.ArmorMeditationAllowance;
using AMT = Server.Items.ArmorMaterialType;

namespace Server.Items
{
    public abstract class BaseArmor : Item, IScissorable, ICraftable, IWearableDurability
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
        private int m_MaxHitPoints;
        private int m_HitPoints;
        private Mobile m_Crafter;
        private CraftResource m_Resource;

        // Overridable values. These values are provided to override the defaults which get defined in the individual armor scripts.
        private int m_ArmorBase = -1;
        private int m_StrBonus = -1, m_DexBonus = -1, m_IntBonus = -1;
        private int m_StrReq = -1, m_DexReq = -1, m_IntReq = -1;
        private AMA m_Meditate = (AMA) (-1);

        #region Magical Properties

        private MagicalProperties m_MagicProps;

        public MagicalProperties MagicProps
        {
            get => m_MagicProps ??= new MagicalProperties(this);
        }

        #endregion


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

        public virtual bool CanFortify
        {
            get { return true; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public AMA MeditationAllowance
        {
            get => MagicProps.GetAttr(DefaultMedAllowance);
            set => MagicProps.SetAttr(value);
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

        public double BaseArmorRatingScaled
        {
            get { return BaseArmorRating * ArmorScalar; }
        }

        public virtual double ArmorRating
        {
            get
            {
                double ar = BaseArmorRating;

                if (ProtectionLevel != ArmorProtectionLevel.Regular)
                    ar += 10 + 5 * (int) ProtectionLevel;

                switch (m_Resource)
                {
                    case CraftResource.Spike:
                        ar += 5;
                        break;
                    case CraftResource.Fruity:
                        ar += 5;
                        break;
                    case CraftResource.Bronze:
                        ar += 5;
                        break;
                    case CraftResource.IceRock:
                        ar += 10;
                        break;
                    case CraftResource.BlackDwarf:
                        ar += 10;
                        break;
                    case CraftResource.DullCopper:
                        ar += 10;
                        break;
                    case CraftResource.Platinum:
                        ar += 10;
                        break;
                    case CraftResource.SilverRock:
                        ar += 11;
                        break;
                    case CraftResource.DarkPagan:
                        ar += 12;
                        break;
                    case CraftResource.Copper:
                        ar += 13;
                        break;
                    case CraftResource.Mystic:
                        ar += 14;
                        break;
                    case CraftResource.Spectral:
                        ar += 15;
                        break;
                    case CraftResource.OldBritain:
                        ar += 16;
                        break;
                    case CraftResource.Onyx:
                        ar += 17;
                        break;
                    case CraftResource.RedElven:
                        ar += 18;
                        break;
                    case CraftResource.Undead:
                        ar += 19;
                        break;
                    case CraftResource.Pyrite:
                        ar += 20;
                        break;
                    case CraftResource.Virginity:
                        ar += 20;
                        break;
                    case CraftResource.Malachite:
                        ar += 20;
                        break;
                    case CraftResource.Lavarock:
                        ar += 20;
                        break;
                    case CraftResource.Azurite:
                        ar += 21;
                        break;
                    case CraftResource.Dripstone:
                        ar += 22;
                        break;
                    case CraftResource.Executor:
                        ar += 23;
                        break;
                    case CraftResource.Peachblue:
                        ar += 24;
                        break;
                    case CraftResource.Destruction:
                        ar += 25;
                        break;
                    case CraftResource.Anra:
                        ar += 26;
                        break;
                    case CraftResource.Crystal:
                        ar += 27;
                        break;
                    case CraftResource.Doom:
                        ar += 28;
                        break;
                    case CraftResource.Goddess:
                        ar += 29;
                        break;
                    case CraftResource.NewZulu:
                        ar += 30;
                        break;
                    case CraftResource.EbonTwilightSapphire:
                        ar += 25;
                        break;
                    case CraftResource.DarkSableRuby:
                        ar += 25;
                        break;
                    case CraftResource.RadiantNimbusDiamond:
                        ar += 25;
                        break;

                    case CraftResource.RatLeather:
                        ar *= 1.15;
                        break;
                    case CraftResource.WolfLeather:
                        ar *= 1.20;
                        break;
                    case CraftResource.BearLeather:
                        ar *= 1.25;
                        break;
                    case CraftResource.SerpentLeather:
                        ar *= 1.30;
                        break;
                    case CraftResource.LizardLeather:
                        ar *= 1.35;
                        break;
                    case CraftResource.TrollLeather:
                        ar *= 1.40;
                        break;
                    case CraftResource.OstardLeather:
                        ar *= 1.45;
                        break;
                    case CraftResource.NecromancerLeather:
                        ar *= 1.50;
                        break;
                    case CraftResource.LavaLeather:
                        ar *= 1.55;
                        break;
                    case CraftResource.LicheLeather:
                        ar *= 1.60;
                        break;
                    case CraftResource.IceCrystalLeather:
                        ar *= 1.65;
                        break;
                    case CraftResource.DragonLeather:
                        ar *= 1.70;
                        break;
                    case CraftResource.WyrmLeather:
                        ar *= 1.80;
                        break;
                    case CraftResource.BalronLeather:
                        ar *= 1.90;
                        break;
                    case CraftResource.GoldenDragonLeather:
                        ar *= 2.0;
                        break;
                }

                ar += -8 + 8 * (int) Quality;
                return ScaleArmorByDurability(ar);
            }
        }

        public double ArmorRatingScaled
        {
            get { return ArmorRating * ArmorScalar; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int StrBonus
        {
            get => MagicProps.TryGetMod(StatType.Str, out MagicStatMod mod) ? mod.Offset : 0;
            set => MagicProps.AddMod(new MagicStatMod(StatType.Str, value, Parent));
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int DexBonus
        {
            get => MagicProps.TryGetMod(StatType.Dex, out MagicStatMod mod) ? mod.Offset : 0;
            set => MagicProps.AddMod(new MagicStatMod(StatType.Dex, value, Parent));
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int IntBonus
        {
            get => MagicProps.TryGetMod(StatType.Int, out MagicStatMod mod) ? mod.Offset : 0;
            set => MagicProps.AddMod(new MagicStatMod(StatType.Int, value, Parent));
        }

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
        public bool Identified
        {
            get => MagicProps.GetAttr(MagicProp.Identified, false);
            set => MagicProps.SetAttr(MagicProp.Identified, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PlayerConstructed
        {
            get => MagicProps.GetAttr(MagicProp.PlayerConstructed, false);
            set => MagicProps.SetAttr(MagicProp.PlayerConstructed, value);
        }

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
                int pos = (int) BodyPosition;

                if (pos >= 0 && pos < ArmorScalars.Length)
                    return ArmorScalars[pos];

                return 1.0;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxHitPoints
        {
            get { return m_MaxHitPoints; }
            set { m_MaxHitPoints = value; }
        }

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
        public ArmorQuality Quality
        {
            get => MagicProps.GetAttr(MagicProp.Quality, ArmorQuality.Regular);
            set
            {
                UnscaleDurability();
                MagicProps.SetAttr(MagicProp.Quality, value);
                Invalidate();
                ScaleDurability();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public ArmorDurabilityLevel Durability
        {
            get => MagicProps.GetAttr(MagicProp.Durability, ArmorDurabilityLevel.Regular);
            set
            {
                UnscaleDurability();
                MagicProps.SetAttr(MagicProp.Durability, value);
                ScaleDurability();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public ArmorProtectionLevel ProtectionLevel
        {
            get => MagicProps.GetAttr(MagicProp.ArmorProtection, ArmorProtectionLevel.Regular);
            set
            {
                MagicProps.SetAttr(MagicProp.ArmorProtection, value);
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
                    Layer.InnerLegs => ArmorBodyType.Legs,
                    Layer.OuterLegs => ArmorBodyType.Legs,
                    Layer.Pants => ArmorBodyType.Legs,
                    Layer.InnerTorso => ArmorBodyType.Chest,
                    Layer.OuterTorso => ArmorBodyType.Chest,
                    Layer.Shirt => ArmorBodyType.Chest,
                    _ => ArmorBodyType.Gorget
                };
            }
        }

        public void UnscaleDurability()
        {
            int scale = 100 + GetDurabilityBonus();

            m_HitPoints = (m_HitPoints * 100 + (scale - 1)) / scale;
            m_MaxHitPoints = (m_MaxHitPoints * 100 + (scale - 1)) / scale;
        }

        public void ScaleDurability()
        {
            int scale = 100 + GetDurabilityBonus();

            m_HitPoints = (m_HitPoints * scale + 99) / 100;
            m_MaxHitPoints = (m_MaxHitPoints * scale + 99) / 100;
        }

        public int GetDurabilityBonus()
        {
            int bonus = Durability switch
            {
                ArmorDurabilityLevel.Durable => 20,
                ArmorDurabilityLevel.Substantial => 50,
                ArmorDurabilityLevel.Massive => 70,
                ArmorDurabilityLevel.Fortified => 100,
                ArmorDurabilityLevel.Indestructible => 120,
                _ => 0
            };

            if (Quality == ArmorQuality.Exceptional)
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

            if (item != null && item.Resources.Count == 1 && item.Resources.GetAt(0).Amount >= 2)
            {
                try
                {
                    Item res = (Item) Activator.CreateInstance(CraftResources.GetInfo(m_Resource).ResourceTypes[0]);

                    ScissorHelper(from, res, PlayerConstructed ? item.Resources.GetAt(0).Amount / 2 : 1);
                    return true;
                }
                catch
                {
                }
            }

            from.SendLocalizedMessage(502440); // Scissors can not be used on that to produce anything.
            return false;
        }

        public static double[] ArmorScalars { get; set; } = {0.07, 0.07, 0.14, 0.15, 0.22, 0.35};

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
            int scale = 100;

            if (m_MaxHitPoints > 0 && m_HitPoints < m_MaxHitPoints)
                scale = 50 + 50 * m_HitPoints / m_MaxHitPoints;

            return armor * scale / 100;
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
            Quality = 0x00000800,
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
            PlayerConstructed = 0x01000000
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 8); // version

            SaveFlag flags = SaveFlag.None;

            SetSaveFlag(ref flags, SaveFlag.NewMagicalProperties, true);

            // Legacy property saving
            if (!GetSaveFlag(flags, SaveFlag.NewMagicalProperties))
            {
                SetSaveFlag(ref flags, SaveFlag.Identified, Identified != false);
                SetSaveFlag(ref flags, SaveFlag.Quality, Quality != ArmorQuality.Regular);
                SetSaveFlag(ref flags, SaveFlag.Durability, Durability != ArmorDurabilityLevel.Regular);
                SetSaveFlag(ref flags, SaveFlag.Protection, ProtectionLevel != ArmorProtectionLevel.Regular);
                SetSaveFlag(ref flags, SaveFlag.MedAllowance, m_Meditate != (AMA) (-1));
                SetSaveFlag(ref flags, SaveFlag.PlayerConstructed, PlayerConstructed != false);
                
                SetSaveFlag(ref flags, SaveFlag.StrBonus, m_StrBonus != -1);
                SetSaveFlag(ref flags, SaveFlag.DexBonus, m_DexBonus != -1);
                SetSaveFlag(ref flags, SaveFlag.IntBonus, m_IntBonus != -1);
            }

            SetSaveFlag(ref flags, SaveFlag.MaxHitPoints, m_MaxHitPoints != 0);
            SetSaveFlag(ref flags, SaveFlag.HitPoints, m_HitPoints != 0);
            SetSaveFlag(ref flags, SaveFlag.Crafter, m_Crafter != null);
            SetSaveFlag(ref flags, SaveFlag.Resource, m_Resource != DefaultResource);
            SetSaveFlag(ref flags, SaveFlag.BaseArmor, m_ArmorBase != -1);
            SetSaveFlag(ref flags, SaveFlag.StrReq, m_StrReq != -1);
            SetSaveFlag(ref flags, SaveFlag.DexReq, m_DexReq != -1);
            SetSaveFlag(ref flags, SaveFlag.IntReq, m_IntReq != -1);

            writer.WriteEncodedInt((int) flags);
            
            if (GetSaveFlag(flags, SaveFlag.NewMagicalProperties))
                MagicProps.Serialize(writer);

            if (GetSaveFlag(flags, SaveFlag.MaxHitPoints))
                writer.WriteEncodedInt((int) m_MaxHitPoints);

            if (GetSaveFlag(flags, SaveFlag.HitPoints))
                writer.WriteEncodedInt((int) m_HitPoints);

            if (GetSaveFlag(flags, SaveFlag.Crafter))
                writer.Write((Mobile) m_Crafter);

            if (GetSaveFlag(flags, SaveFlag.Quality))
                writer.WriteEncodedInt((int) Quality);

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
                case 8:
                    if (GetSaveFlag(flags, SaveFlag.NewMagicalProperties))
                        m_MagicProps = MagicalProperties.Deserialize(reader, this);
                    goto case 7;
                case 7:
                case 6:
                case 5:
                {
                    if (GetSaveFlag(flags, SaveFlag.Identified))
                        Identified = version >= 7 || reader.ReadBool();

                    if (GetSaveFlag(flags, SaveFlag.MaxHitPoints))
                        m_MaxHitPoints = reader.ReadEncodedInt();

                    if (GetSaveFlag(flags, SaveFlag.HitPoints))
                        m_HitPoints = reader.ReadEncodedInt();

                    if (GetSaveFlag(flags, SaveFlag.Crafter))
                        m_Crafter = reader.ReadMobile();

                    if (GetSaveFlag(flags, SaveFlag.Quality))
                        Quality = (ArmorQuality) reader.ReadEncodedInt();
                    else
                        Quality = ArmorQuality.Regular;

                    if (version == 5 && Quality == ArmorQuality.Low)
                        Quality = ArmorQuality.Regular;

                    if (GetSaveFlag(flags, SaveFlag.Durability))
                    {
                        Durability = (ArmorDurabilityLevel) reader.ReadEncodedInt();

                        if (Durability > ArmorDurabilityLevel.Indestructible)
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
                    m_MaxHitPoints = reader.ReadInt();
                    m_HitPoints = reader.ReadInt();
                    m_Crafter = reader.ReadMobile();
                    Quality = (ArmorQuality) reader.ReadInt();
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

                    if (m_MaxHitPoints == 0 && m_HitPoints == 0)
                        m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax(InitMinHits, InitMaxHits);

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
            
            Quality = ArmorQuality.Regular;
            Durability = ArmorDurabilityLevel.Regular;
            m_Crafter = null;

            m_Resource = DefaultResource;
            Hue = CraftResources.GetHue(m_Resource);

            m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax(InitMinHits, InitMaxHits);

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
                MagicProps.OnMobileEquip();
                m.CheckStatTimers();
                m.Delta(MobileDelta.WeaponDamage);
                m.Delta(MobileDelta.Armor); 
            }
        }

        public override void OnRemoved(IEntity parent)
        {
            if (parent is Mobile m)
            {
                MagicProps.OnMobileRemoved();
                m.Delta(MobileDelta.Armor); // Tell them armor rating has changed
                m.CheckStatTimers();
            }

            base.OnRemoved(parent);
        }

        public virtual int OnHit(BaseWeapon weapon, int damageTaken)
        {
            double HalfAr = ArmorRating / 2.0;
            int Absorbed = (int) (HalfAr + HalfAr * Utility.RandomDouble());

            damageTaken -= Absorbed;
            if (damageTaken < 0)
                damageTaken = 0;

            if (Absorbed < 2)
                Absorbed = 2;

            if (25 > Utility.Random(100)) // 25% chance to lower durability
            {
                int wear;

                if (weapon.Type == WeaponType.Bashing)
                    wear = Absorbed / 2;
                else
                    wear = Utility.Random(2);

                if (wear > 0 && m_MaxHitPoints > 0)
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
                        if (m_MaxHitPoints > wear)
                        {
                            MaxHitPoints -= wear;

                            if (Parent is Mobile)
                                ((Mobile) Parent).LocalOverheadMessage(MessageType.Regular, 0x3B2,
                                    1061121); // Your equipment is severely damaged.
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
            string name = Name;

            if (name == null)
                name = $"#{LabelNumber}";

            return name;
        }

        [Hue, CommandProperty(AccessLevel.GameMaster)]
        public override int Hue
        {
            get { return base.Hue; }
            set { base.Hue = value; }
        }

        public override bool AllowEquippedCast(Mobile from)
        {
            if (base.AllowEquippedCast(from))
                return true;

            return false;
        }

        public override void OnSingleClick(Mobile from)
        {
            List<EquipInfoAttribute> attrs = new List<EquipInfoAttribute>();

            if (DisplayLootType)
            {
                if (LootType == LootType.Blessed)
                    attrs.Add(new EquipInfoAttribute(1038021)); // blessed
                else if (LootType == LootType.Cursed)
                    attrs.Add(new EquipInfoAttribute(1049643)); // cursed
            }

            if (Quality == ArmorQuality.Exceptional)
                attrs.Add(new EquipInfoAttribute(1018305 - (int) Quality));

            if (Identified || from.AccessLevel >= AccessLevel.GameMaster)
            {
                if (Durability != ArmorDurabilityLevel.Regular)
                    attrs.Add(new EquipInfoAttribute(1038000 + (int) Durability));

                if (ProtectionLevel > ArmorProtectionLevel.Regular && ProtectionLevel <= ArmorProtectionLevel.Invulnerability)
                    attrs.Add(new EquipInfoAttribute(1038005 + (int) ProtectionLevel));
            }
            else if (Durability != ArmorDurabilityLevel.Regular || ProtectionLevel > ArmorProtectionLevel.Regular &&
                ProtectionLevel <= ArmorProtectionLevel.Invulnerability)
                attrs.Add(new EquipInfoAttribute(1038000)); // Unidentified

            int number;

            if (Name == null)
            {
                number = LabelNumber;
            }
            else
            {
                LabelTo(from, Name);
                number = 1041000;
            }

            if (attrs.Count == 0 && Crafter == null && Name != null)
                return;

            EquipmentInfo eqInfo = new EquipmentInfo(number, m_Crafter, false, attrs.ToArray());

            from.Send(new DisplayEquipmentInfo(this, eqInfo));
        }

        #region ICraftable Members

        public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes,
            BaseTool tool, CraftItem craftItem, int resHue)
        {
            Quality = (ArmorQuality) quality;

            if (makersMark)
                Crafter = from;

            Type resourceType = typeRes;

            if (resourceType == null)
                resourceType = craftItem.Resources.GetAt(0).ItemType;

            Resource = CraftResources.GetFromType(resourceType);
            PlayerConstructed = true;

            CraftContext context = craftSystem.GetContext(from);

            if (context != null && context.DoNotColor)
                Hue = 0;

            return quality;
        }

        #endregion
    }
}