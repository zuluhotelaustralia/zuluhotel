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
        Clumsiness,
        Identification,
        Healing,
        Feeblemindedness,
        Weakness,
        MagicArrow,
        Harming,
        Fireball,
        GreaterHealing,
        Lightning,
        ManaDraining
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
            Timer.DelayCall(GetUseDelay, ReleaseWandLock_Callback, from);
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
                    WandEffect.Clumsiness => 3002011,
                    WandEffect.Identification => 1044063,
                    WandEffect.Healing => 3002014,
                    WandEffect.Feeblemindedness => 3002013,
                    WandEffect.Weakness => 3002018,
                    WandEffect.MagicArrow => 3002015,
                    WandEffect.Harming => 3002022,
                    WandEffect.Fireball => 3002028,
                    WandEffect.GreaterHealing => 3002039,
                    WandEffect.Lightning => 3002040,
                    WandEffect.ManaDraining => 3002041,
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