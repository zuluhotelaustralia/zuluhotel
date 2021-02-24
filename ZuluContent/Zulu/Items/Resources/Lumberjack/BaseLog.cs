namespace Server.Items
{
    [FlipableAttribute(0x1bdd, 0x1be0)]
    public class BaseLog : Item
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource { get; set; }

        public override string DefaultName => CraftResources.GetName(Resource).Length > 0
            ? $"{CraftResources.GetName(Resource)} log"
            : "log";

        [Constructible]
        public BaseLog(CraftResource resource, int amount)
            : base(0x1BDD)
        {
            Stackable = true;
            Weight = 2.0;
            Amount = amount;

            Resource = resource;
            Hue = CraftResources.GetHue(resource);
        }

        [Constructible]
        public BaseLog(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version

            writer.Write((int) Resource);
        }

        public override void Deserialize(IGenericReader reader)
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
            }

            if (version == 0)
                Resource = CraftResource.RegularWood;
        }
    }
}