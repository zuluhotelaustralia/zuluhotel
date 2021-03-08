using System;
using Scripts.Zulu.Utilities;
using Server.Engines.Craft;
using Server.Mobiles;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;

namespace Server.Items
{
    public class Bridle : Item, ICraftable, IResource
    {
        private CraftResource m_Resource;
        private EnchantmentDictionary m_Enchantments;

        public EnchantmentDictionary Enchantments
        {
            get => m_Enchantments ??= new EnchantmentDictionary();
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public MarkQuality Mark
        {
            get => Enchantments.Get((ItemMark e) => (MarkQuality) e.Value);
            set { Enchantments.Set((ItemMark e) => e.Value = (int) value); }
        }

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

            Mark = MarkQuality.Regular;
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

        public override void OnSingleClick(Mobile from)
        {
            HandleSingleClick(this, from);
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

            ICraftable.Serialize(writer, this);

            Enchantments.Serialize(writer);

            writer.WriteEncodedInt((int) m_Resource);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            ICraftable.Deserialize(reader, this);

            m_Enchantments = EnchantmentDictionary.Deserialize(reader);

            m_Resource = (CraftResource) reader.ReadEncodedInt();
        }
    }
}