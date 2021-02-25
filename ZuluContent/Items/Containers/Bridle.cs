using System;
using Scripts.Zulu.Utilities;
using Server.Engines.Craft;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
    public class Bridle : Item, ICraftable
    {
        private CraftResource m_Resource;

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get { return m_Resource; }
            set
            {
                if (m_Resource != value)
                {
                    m_Resource = value;
                    Hue = CraftResources.GetHue(m_Resource);
                }
            }
        }

        [Constructible]
        public Bridle() : base(0x1374)
        {
            Weight = 1;
        }

        [Constructible]
        public Bridle(Serial serial) : base(serial)
        {
        }

        public Mobile Crafter { get; set; }

        public bool PlayerConstructed { get; set; }

        public virtual int OnCraft(int mark, double quality, bool makersMark, Mobile from, CraftSystem craftSystem,
            Type typeRes,
            BaseTool tool, CraftItem craftItem, int resHue)
        {
            Type resourceType = typeRes;

            if (resourceType == null)
                resourceType = craftItem.Resources[0].ItemType;

            Resource = CraftResources.GetFromType(resourceType);

            CraftContext context = craftSystem.GetContext(from);

            if (context != null && context.DoNotColor)
                Hue = 0;

            return mark;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (!from.InRange(GetWorldLocation(), 2))
            {
                from.SendLocalizedMessage(1076766); // That is too far away.
                return;
            }

            from.Target = new InternalTarget(this);
            from.SendSuccessMessage("Target your pet horse.");
        }

        private class InternalTarget : Target
        {
            private Item m_Bridal;

            public InternalTarget(Item bridal) : base(2, false, TargetFlags.None)
            {
                m_Bridal = bridal;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Horse horse)
                {
                    if (horse.ControlMaster == null)
                    {
                        horse.Say(true, "The wild beast refuses the bridle!");
                        return;
                    }

                    if (horse.ControlMaster != from)
                    {
                        horse.SayTo(from, true, "This isn't your animal!");
                        return;
                    }

                    var oldHorseLocation = horse.Location;
                    var oldHorseControlOrder = horse.ControlOrder;

                    m_Bridal.Delete();
                    horse.Internalize();
                    horse.Delete();

                    var packHorse = new PackHorse();
                    packHorse.SetControlMaster(from);
                    packHorse.ControlTarget = from;
                    packHorse.ControlOrder = oldHorseControlOrder;
                    packHorse.MoveToWorld(oldHorseLocation, from.Map);
                }
                else if (targeted is BaseCreature creature)
                {
                    creature.Say(true, "The beast refuses!");
                }
            }
        }


        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}