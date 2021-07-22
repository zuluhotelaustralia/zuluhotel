using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
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
                    from.SendLocalizedMessage(502079); // The instrument played its last tune.

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

        public static int GetBardRange(Mobile bard, SkillName skill)
        {
            return 8 + (int) (bard.Skills[skill].Value / 15);
        }

        public static void PickInstrument(Mobile from, InstrumentPickedCallback callback)
        {
            var instrument = GetInstrument(from);

            if (instrument != null)
            {
                callback?.Invoke(from, instrument);
            }
            else
            {
                from.SendLocalizedMessage(500617); // What instrument shall you play?
                from.BeginTarget(1, false, TargetFlags.None, OnPickedInstrument, callback);
            }
        }

        public static async Task<BaseInstrument> PickInstrumentAsync(Mobile from)
        {
            var instrument = GetInstrument(from);
            if (instrument != null)
                return instrument;
            
            from.SendLocalizedMessage(500617); // What instrument shall you play?
            var target = new AsyncTarget<Item>(from, new TargetOptions
            {
                Range = 1,
            });
            
            from.Target = target;

            var (item, _) = await target;

            switch (item)
            {
                case null:
                    break;
                case BaseInstrument targeted:
                    SetInstrument(from, targeted);
                    return targeted;
                default:
                    from.SendLocalizedMessage(500619); // That is not a musical instrument.
                    break;
            }

            return null;
        }

        public static void OnPickedInstrument(Mobile from, object targeted, object state)
        {
            if (!(targeted is BaseInstrument instrument))
            {
                from.SendLocalizedMessage(500619); // That is not a musical instrument.
            }
            else
            {
                SetInstrument(from, instrument);

                if (state is InstrumentPickedCallback callback)
                    callback(from, instrument);
            }
        }

        public static bool IsMageryCreature(BaseCreature bc)
        {
            return bc != null && bc.AI == AIType.AI_Mage && bc.Skills[SkillName.Magery].Base > 5.0;
        }

        public static bool IsFireBreathingCreature(BaseCreature bc)
        {
            if (bc == null)
                return false;

            return bc.HasBreath;
        }

        public static bool IsPoisonImmune(BaseCreature bc)
        {
            return bc != null && bc.PoisonImmune != null;
        }

        public static int GetPoisonLevel(BaseCreature bc)
        {
            if (bc == null)
                return 0;

            Poison p = bc.HitPoison;

            if (p == null)
                return 0;

            return p.Level + 1;
        }

        public static double GetBaseDifficulty(Mobile targ)
        {
            /* Difficulty TODO: Add another 100 points for each of the following abilities:
              - Radiation or Aura Damage (Heat, Cold etc.)
              - Summoning Undead
            */

            double val = targ.HitsMax * 1.6 + targ.StamMax + targ.ManaMax;

            val += targ.SkillsTotal / 10;

            if (val > 700)
                val = 700 + (int) ((val - 700) * (3.0 / 11));

            BaseCreature bc = targ as BaseCreature;

            if (IsMageryCreature(bc))
                val += 100;

            if (IsFireBreathingCreature(bc))
                val += 100;

            if (IsPoisonImmune(bc))
                val += 100;

            val += GetPoisonLevel(bc) * 20;

            val /= 10;

            return val;
        }

        public double GetDifficultyFor(Mobile targ)
        {
            double val = GetBaseDifficulty(targ);

            if (m_Slayer != SlayerName.None)
            {
                SlayerEntry entry = SlayerGroup.GetEntryByName(m_Slayer);

                if (entry != null)
                {
                    if (entry.Slays(targ))
                        val -= 10.0; // 20%
                    else if (entry.Group.OppositionSuperSlays(targ))
                        val += 10.0; // -20%
                }
            }

            if (m_Slayer2 != SlayerName.None)
            {
                SlayerEntry entry = SlayerGroup.GetEntryByName(m_Slayer2);

                if (entry != null)
                {
                    if (entry.Slays(targ))
                        val -= 10.0; // 20%
                    else if (entry.Group.OppositionSuperSlays(targ))
                        val += 10.0; // -20%
                }
            }

            return val;
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

                // Delay of 7 second before beign able to play another instrument again
                new InternalTimer(from).Start();

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

        public static bool CheckMusicianship(Mobile m)
        {
            return m.ShilCheckSkill(SkillName.Musicianship);
        }

        public void PlayInstrumentWell(Mobile from)
        {
            from.PlaySound(m_WellSound);
        }

        public void PlayInstrumentBadly(Mobile from)
        {
            from.PlaySound(m_BadlySound);
        }

        private class InternalTimer : Timer
        {
            private Mobile m_From;

            public InternalTimer(Mobile from) : base(TimeSpan.FromSeconds(6.0))
            {
                m_From = from;
            }

            protected override void OnTick()
            {
                m_From.EndAction(typeof(BaseInstrument));
            }
        }
    }
}