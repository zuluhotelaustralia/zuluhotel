using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;
using Server.Engines.Craft;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;

namespace Server.Items
{
    public delegate void InstrumentPickedCallback(Mobile from, BaseInstrument instrument);

    public abstract class BaseInstrument : Item, ISlayer, ICraftable, IResource
    {
        private int m_WellSound, m_BadlySound;
        private SlayerName m_Slayer, m_Slayer2;
        private int m_UsesRemaining;
        private CraftResource m_Resource;

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
                    m_Resource = value;

                    if (CraftItem.RetainsColor(GetType()))
                    {
                        Hue = CraftResources.GetHue(m_Resource);
                    }
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public MarkQuality Mark { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Crafter { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SuccessSound
        {
            get { return m_WellSound; }
            set { m_WellSound = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int FailureSound
        {
            get { return m_BadlySound; }
            set { m_BadlySound = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public SlayerName OldSlayer
        {
            get { return m_Slayer; }
            set { m_Slayer = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public SlayerName OldSlayer2
        {
            get { return m_Slayer2; }
            set { m_Slayer2 = value; }
        }

        public virtual int InitMinUses
        {
            get { return 350; }
        }

        public virtual int InitMaxUses
        {
            get { return 450; }
        }

        public virtual TimeSpan ChargeReplenishRate
        {
            get { return TimeSpan.FromMinutes(5.0); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int UsesRemaining
        {
            get
            {
                CheckReplenishUses();
                return m_UsesRemaining;
            }
            set { m_UsesRemaining = value; }
        }

        private DateTime m_LastReplenished;

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime LastReplenished
        {
            get { return m_LastReplenished; }
            set
            {
                m_LastReplenished = value;
                CheckReplenishUses();
            }
        }

        private bool m_ReplenishesCharges;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ReplenishesCharges
        {
            get { return m_ReplenishesCharges; }
            set
            {
                if (value != m_ReplenishesCharges && value)
                    m_LastReplenished = DateTime.UtcNow;


                m_ReplenishesCharges = value;
            }
        }

        public void CheckReplenishUses()
        {
            if (!m_ReplenishesCharges || m_UsesRemaining >= InitMaxUses)
                return;

            if (m_LastReplenished + ChargeReplenishRate < DateTime.Now)
            {
                TimeSpan timeDifference = DateTime.Now - m_LastReplenished;

                m_UsesRemaining = Math.Min(m_UsesRemaining + (int) (timeDifference.Ticks / ChargeReplenishRate.Ticks),
                    InitMaxUses); //How rude of TimeSpan to not allow timespan division.
                m_LastReplenished = DateTime.UtcNow;
            }
        }

        public void ScaleUses()
        {
            UsesRemaining = UsesRemaining * GetUsesScalar() / 100;
            //InvalidateProperties();
        }

        public void UnscaleUses()
        {
            UsesRemaining = UsesRemaining * 100 / GetUsesScalar();
        }

        public int GetUsesScalar()
        {
            return 100;
        }

        public void ConsumeUse(Mobile from)
        {
            // TODO: Confirm what must happen here?

            if (UsesRemaining > 1)
            {
                --UsesRemaining;
            }
            else
            {
                if (from != null)
                    from.SendFailureMessage(502079); // The instrument played its last tune.

                Delete();
            }
        }

        private static readonly Hashtable Instruments = new Hashtable();

        public static BaseInstrument GetInstrument(Mobile from)
        {
            if (!(Instruments[from] is BaseInstrument item))
                return null;

            if (!item.IsChildOf(from.Backpack))
            {
                Instruments.Remove(from);
                return null;
            }

            return item;
        }
        
        public static BaseInstrument FindInstrument(Mobile from)
        {
            var backpack = from.Backpack;

            var instrumentItem = backpack.FindItemByType(typeof(BaseInstrument));

            if (instrumentItem is BaseInstrument instrument)
            {
                SetInstrument(from, instrument);
                return instrument;
            }

            return null;
        }

        public static int GetBardRange(Mobile bard, SkillName skill)
        {
            return 8 + (int) (bard.Skills[skill].Value / 15);
        }

        public static BaseInstrument PickInstrument(Mobile from)
        {
            var instrument = GetInstrument(from);

            if (instrument != null)
                return instrument;

            return FindInstrument(from);
        }

        public static double GetDifficulty(BaseCreature creature)
        {
            var difficulty = creature.ProvokeSkillOverride;
            
            if (difficulty <= 0)
                difficulty = creature.GetCreatureScore();

            return difficulty;
        }

        public static void SetInstrument(Mobile from, BaseInstrument item)
        {
            Instruments[from] = item;
        }

        public BaseInstrument(int itemID, int wellSound, int badlySound) : base(itemID)
        {
            m_WellSound = wellSound;
            m_BadlySound = badlySound;
            UsesRemaining = Utility.RandomMinMax(InitMinUses, InitMaxUses);
        }

        public BaseInstrument(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 4); // version

            ICraftable.Serialize(writer, this);

            writer.WriteEncodedInt((int) m_Resource);

            writer.Write(m_ReplenishesCharges);
            if (m_ReplenishesCharges)
                writer.Write(m_LastReplenished);

            writer.WriteEncodedInt((int) m_Slayer);
            writer.WriteEncodedInt((int) m_Slayer2);

            writer.WriteEncodedInt((int) UsesRemaining);

            writer.WriteEncodedInt((int) m_WellSound);
            writer.WriteEncodedInt((int) m_BadlySound);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 4:
                {
                    ICraftable.Deserialize(reader, this);

                    m_Resource = (CraftResource) reader.ReadEncodedInt();

                    goto case 3;
                }
                case 3:
                {
                    m_ReplenishesCharges = reader.ReadBool();

                    if (m_ReplenishesCharges)
                        m_LastReplenished = reader.ReadDateTime();

                    goto case 2;
                }
                case 2:
                {
                    m_Slayer = (SlayerName) reader.ReadEncodedInt();
                    m_Slayer2 = (SlayerName) reader.ReadEncodedInt();

                    UsesRemaining = reader.ReadEncodedInt();

                    m_WellSound = reader.ReadEncodedInt();
                    m_BadlySound = reader.ReadEncodedInt();

                    break;
                }
                case 1:
                {
                    m_Slayer = (SlayerName) reader.ReadEncodedInt();

                    UsesRemaining = reader.ReadEncodedInt();

                    m_WellSound = reader.ReadEncodedInt();
                    m_BadlySound = reader.ReadEncodedInt();

                    break;
                }
                case 0:
                {
                    m_WellSound = reader.ReadInt();
                    m_BadlySound = reader.ReadInt();
                    UsesRemaining = Utility.RandomMinMax(InitMinUses, InitMaxUses);

                    break;
                }
            }

            CheckReplenishUses();
        }

        public override void OnSingleClick(Mobile from)
        {
            HandleSingleClick(this, from);
            from.NetState.SendMessage(Serial, ItemID, MessageType.Label, 0, 3, true, null, "",
                $"{UsesRemaining} uses remaining");
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(GetWorldLocation(), 1))
            {
                from.SendLocalizedMessage(500446); // That is too far away.
            }
            else if (from.BeginAction(typeof(BaseInstrument)))
            {
                SetInstrument(from, this);
                
                var prevLocation = from.Location;

                // Delay of 7 second before beign able to play another instrument again
                new InternalTimer(from, prevLocation, this).Start();
                
                from.SendSuccessMessage("You begin playing...");

                if (CheckMusicianship(from))
                    PlayInstrumentWell(from);
                else
                    PlayInstrumentBadly(from);
            }
            else
            {
                from.SendLocalizedMessage(500119); // You must wait to perform another action
            }
        }

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

            var context = craftSystem.GetContext(from);

            if (context != null && context.DoNotColor)
                Hue = 0;

            return mark;
        }

        public bool CheckMusicianship(Mobile m)
        {
            var difficulty = (int) m.Skills[SkillName.Musicianship].Value - 10;
            difficulty = Math.Max(difficulty, 10);

            var points = difficulty * 2;

            if (this is Harp)
                points *= 2;
            
            return m.ShilCheckSkill(SkillName.Musicianship, difficulty, points);
        }

        public void PlayInstrumentWell(Mobile from)
        {
            from.PlaySound(m_WellSound);
        }

        public void PlayInstrumentBadly(Mobile from)
        {
            from.PlaySound(m_BadlySound);
        }

        public void PlayMusicEffect(Mobile musician, int hue)
        {
            new MusicEffectTimer(musician, ItemID, hue).Start();
        }

        private class MusicEffectTimer : Timer
        {
            private Mobile m_From;
            private int m_ItemID;
            private int m_Hue;

            public MusicEffectTimer(Mobile from, int itemID, int hue) : base(TimeSpan.Zero, TimeSpan.FromMilliseconds(50), 30)
            {
                m_From = from;
                m_ItemID = itemID;
                m_Hue = hue;
            }

            protected override void OnTick()
            {
                var map = m_From.Map;
                var fromLocation = new Point3D(m_From.Location.X - 1, m_From.Location.Y, m_From.Location.Z);
                var x = fromLocation.X - Utility.RandomMinMax(5, 15);
                var y = fromLocation.Y + Utility.RandomMinMax(-10, 10);
                var z = fromLocation.Z + 20;
                var toLocation = new Point3D(x, y, z);

                Effects.SendMovingEffect(map, m_ItemID, fromLocation, toLocation, 10, 10, false, false, m_Hue);
            }
        }

        private class InternalTimer : Timer
        {
            private Mobile m_From;
            private Point3D m_PrevLocation;
            private BaseInstrument m_Instrument;

            public InternalTimer(Mobile from, Point3D prevLocation, BaseInstrument instrument) : base(TimeSpan.FromSeconds(6.0))
            {
                m_From = from;
                m_PrevLocation = prevLocation;
                m_Instrument = instrument;
            }

            protected override void OnTick()
            {
                m_From.EndAction(typeof(BaseInstrument));
                
                if (m_From.Location == m_PrevLocation)
                    m_Instrument.OnDoubleClick(m_From);
                else
                    m_From.SendSuccessMessage("You stop playing...");
            }
        }
    }
}