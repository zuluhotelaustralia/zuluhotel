namespace Server.Items
{
    public abstract class BaseHide : Item
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource { get; set; }

        public override string DefaultName => CraftResources.GetName(Resource).Length > 0
            ? $"{CraftResources.GetName(Resource)} hide"
            : "hide";

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
                case 0:
                {
                    OreInfo info = new OreInfo(reader.ReadInt(), reader.ReadInt(), reader.ReadString());

                    Resource = CraftResources.GetFromOreInfo(info);
                    break;
                }
            }
        }

        public BaseHide(CraftResource resource) : this(resource, 1)
        {
        }

        public BaseHide(CraftResource resource, int amount) : base(0x1079)
        {
            Stackable = true;
            Weight = 5.0;
            Amount = amount;
            Hue = CraftResources.GetHue(resource);

            Resource = resource;
        }

        public BaseHide(Serial serial) : base(serial)
        {
        }

        public override int LabelNumber
        {
            get { return 1047023; }
        }
    }
}