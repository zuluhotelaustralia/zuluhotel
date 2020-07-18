using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public abstract class BaseIngot : Item
    {
        public override string DefaultName => $"{CraftResources.GetName(Resource)} ingot";


        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource { get; set; }

        public override double DefaultWeight
        {
            get { return 0.1; }
        }

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
                    throw new System.Exception("Unsupported item version for ingot.");
                }
            }
        }

        public BaseIngot(CraftResource resource) : this(resource, 1)
        {
        }

        public BaseIngot(CraftResource resource, int amount) : base(0x1BF2)
        {
            Stackable = true;
            Amount = amount;
            //Hue = CraftResources.GetHue( resource );

            Resource = resource;
        }

        public BaseIngot(Serial serial) : base(serial)
        {
        }
        
    }
}