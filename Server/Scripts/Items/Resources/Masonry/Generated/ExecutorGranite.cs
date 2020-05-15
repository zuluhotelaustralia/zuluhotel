// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class ExecutorGranite : BaseGranite
    {
        [Constructable]
        public ExecutorGranite() : base(CraftResource.Executor)
        {
            this.Hue = 2766;
        }

        public ExecutorGranite(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public override void OnSingleClick(Mobile from)
        {
            from.SendMessage("Executor Granite");
        }
    }
}
