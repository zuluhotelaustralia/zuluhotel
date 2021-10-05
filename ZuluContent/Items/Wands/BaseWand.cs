using System;
using System.Collections;
using System.Collections.Generic;
using Server.Network;
using Server.Targeting;
using Server.Spells;

namespace Server.Items
{
    public enum WandEffect
    {
        //First Circle
        Clumsiness,
        CreateFood,
        Feeblemindedness,
        Healing,
        MagicArrow,
        NightSight,
        ReactiveArmor,
        Weakness,
        //Second Circle
        Agility,
        Cunning,
        Cure,
        Harm,
        MagicTrap,
        MagicUntrap,
        Protection,
        Strength,
        // Third Circle
        Bless,
        Fireball,
        MagicLock,
        Poison,
        Telekinesis,
        Teleport,
        Unlock,
        WallOfStone,
        // Fourth Circle
        ArchCure,
        ArchProtection,
        Curse,
        FireField,
        GreaterHeal,
        Lightning,
        ManaDrain,
        Recall,
        // Fifith Circle
        BladeSpirits,
        DispelField,
        Incognito,
        MagicReflection,
        MindBlast,
        Paralyze,
        PoisonField,
        SummCreature,
        // Sixth Circle
        Dispel,
        EnergyBolt,
        Explosion,
        Invisibility,
        Mark,
        MassCurse,
        ParalyzeField,
        Reveal,
        // Seventh Circle
        ChainLightning,
        EnergyField,
        Flamestrike,
        GateTravel,
        ManaVampire,
        MassDispel,
        MeteorSwarm,
        Polymorph,
        // Eighth Circle
        Earthquake,
        EnergyVortex,
        Resurrection,
        AirElemental,
        SummonDaemon,
        EarthElemental,
        FireElemental,
        WaterElemental,       

        Identification        
    }

    public abstract class BaseWand : BaseBashing
    {
        public override int DefaultStrengthReq => 0;

        public override int DefaultMinDamage => 2;

        public override int DefaultMaxDamage => 6;

        public override int DefaultSpeed => 35;

        public override int InitMinHits => 31;

        public override int InitMaxHits => 110;

        public virtual TimeSpan GetUseDelay => TimeSpan.FromSeconds(4.0);

        [CommandProperty(AccessLevel.GameMaster)]
        public WandEffect Effect { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Charges { get; set; }

        public BaseWand(WandEffect effect, int minCharges, int maxCharges) : base(
            Utility.RandomList(0xDF2, 0xDF3, 0xDF4, 0xDF5))
        {
            Weight = 1.0;
            Effect = effect;
            Charges = Utility.RandomMinMax(minCharges, maxCharges);
        }

        public void ConsumeCharge(Mobile from)
        {
            --Charges;

            if (Charges == 0)
                from.SendLocalizedMessage(1019073); // This item is out of charges.

            ApplyDelayTo(from);
        }

        public BaseWand(Serial serial) : base(serial)
        {
        }

        public virtual void ApplyDelayTo(Mobile from)
        {
            from.BeginAction(typeof(BaseWand));
            Timer.StartTimer(GetUseDelay, () => ReleaseWandLock_Callback(from));
        }

        public virtual void ReleaseWandLock_Callback(object state)
        {
            ((Mobile) state).EndAction(typeof(BaseWand));
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.CanBeginAction(typeof(BaseWand)))
            {
                from.SendLocalizedMessage(1070860); // You must wait a moment for the wand to recharge.
                return;
            }

            if (Parent == from)
            {
                if (Charges > 0)
                    OnWandUse(from);
                else
                    from.SendLocalizedMessage(1019073); // This item is out of charges.
            }
            else
            {
                from.SendLocalizedMessage(502641); // You must equip this item to use it.
            }
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version

            writer.Write((int) Effect);
            writer.Write((int) Charges);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                {
                    Effect = (WandEffect) reader.ReadInt();
                    Charges = (int) reader.ReadInt();

                    break;
                }
            }
        }

        public override void OnSingleClick(Mobile from)
        {
            var attrs = new List<EquipInfoAttribute>();

            if (DisplayLootType)
            {
                if (LootType == LootType.Blessed)
                    attrs.Add(new EquipInfoAttribute(1038021)); // blessed
                else if (LootType == LootType.Cursed)
                    attrs.Add(new EquipInfoAttribute(1049643)); // cursed
            }

            if (!Identified)
            {
                attrs.Add(new EquipInfoAttribute(1038000)); // Unidentified
            }
            else
            {
                int num = Effect switch
                {
                    // Frist Circle
                    WandEffect.Clumsiness => 3002011,
                    WandEffect.CreateFood => 3002012,
                    WandEffect.Feeblemindedness => 3002013,
                    WandEffect.Healing => 3002014,
                    WandEffect.MagicArrow => 3002015,
                    WandEffect.NightSight => 3002016,
                    WandEffect.ReactiveArmor => 3002017,
                    WandEffect.Weakness => 3002018,
                    //Second Circle
                    WandEffect.Agility => 3002019,
                    WandEffect.Cunning => 3002020,
                    WandEffect.Cure => 3002021,
                    WandEffect.Harm => 3002022,
                    WandEffect.MagicTrap => 3002023,
                    WandEffect.MagicUntrap => 3002024,
                    WandEffect.Protection => 3002025,
                    WandEffect.Strength => 3002026,
                    // Third Circle
                    WandEffect.Bless => 3002027,
                    WandEffect.Fireball => 3002028,
                    WandEffect.MagicLock => 3002029,
                    WandEffect.Poison => 3002030,
                    WandEffect.Telekinesis => 3002031,
                    WandEffect.Teleport => 3002032,
                    WandEffect.Unlock => 3002033,
                    WandEffect.WallOfStone => 3002034,
                    // Fourth Circle
                    WandEffect.ArchCure => 3002035,
                    WandEffect.ArchProtection => 3002036,
                    WandEffect.Curse => 3002037,
                    WandEffect.FireField => 3002038,
                    WandEffect.GreaterHeal => 3002039,
                    WandEffect.Lightning => 3002040,
                    WandEffect.ManaDrain => 3002041,
                    WandEffect.Recall => 3002042,
                    // Fifith Circle
                    WandEffect.BladeSpirits => 3002043,
                    WandEffect.DispelField => 3002044,
                    WandEffect.Incognito => 3002045,
                    WandEffect.MagicReflection => 3002046,
                    WandEffect.MindBlast => 3002047,
                    WandEffect.Paralyze => 3002048,
                    WandEffect.PoisonField => 3002049,
                    WandEffect.SummCreature => 3002050,
                    // Sixth Circle
                    WandEffect.Dispel => 3002051,
                    WandEffect.EnergyBolt => 3002052,
                    WandEffect.Explosion => 3002053,
                    WandEffect.Invisibility => 3002054,
                    WandEffect.Mark => 3002055,
                    WandEffect.MassCurse => 3002056,
                    WandEffect.ParalyzeField => 3002057,
                    WandEffect.Reveal => 3002058,
                    // Seventh Circle
                    WandEffect.ChainLightning => 3002059,
                    WandEffect.EnergyField => 3002060,
                    WandEffect.Flamestrike => 3002061,
                    WandEffect.GateTravel => 3002062,
                    WandEffect.ManaVampire => 3002063,
                    WandEffect.MassDispel => 3002064,
                    WandEffect.MeteorSwarm => 3002065,
                    WandEffect.Polymorph => 3002066,
                    // Eighth Circle
                    WandEffect.Earthquake => 3002067,
                    WandEffect.EnergyVortex => 3002068,
                    WandEffect.Resurrection => 3002069,
                    WandEffect.AirElemental => 3002070,
                    WandEffect.SummonDaemon => 3002071,
                    WandEffect.EarthElemental => 3002072,
                    WandEffect.FireElemental => 3002073,
                    WandEffect.WaterElemental => 3002074,

                    WandEffect.Identification => 1044063,
                                _ => 0
                };

                if (num > 0)
                    attrs.Add(new EquipInfoAttribute(num, Charges));
            }

            int number;

            if (Name == null)
            {
                number = 1017085;
            }
            else
            {
                LabelTo(from, Name);
                number = 1041000;
            }

            if (attrs.Count == 0 && Crafter == null && Name != null)
                return;

            from.NetState.SendDisplayEquipmentInfo(Serial, number, Crafter?.RawName, false, attrs);
        }

        public void Cast(Spell spell)
        {
            bool m = Movable;

            Movable = false;
            spell.Cast();
            Movable = m;
        }
        
        public override bool AllowEquippedCast(Mobile from) => true;

        public virtual void OnWandUse(Mobile from)
        {
            from.Target = new WandTarget(this);
        }

        public virtual void DoWandTarget(Mobile from, object o)
        {
            if (Deleted || Charges <= 0 || Parent != from || o is StaticTarget || o is LandTarget)
                return;

            if (OnWandTarget(from, o))
                ConsumeCharge(from);
        }

        public virtual bool OnWandTarget(Mobile from, object o)
        {
            return true;
        }
    }
}