using Server.Network;
using Server.Targeting;
using Server.Gumps;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Items
{
    public class PetClaimTicket : Item
    {
        public BaseCreature Stabled { get; set; }

        [Constructible]
        public PetClaimTicket() : base(0x14EF)
        {
            Weight = 1.0;
        }

        [Constructible]
        public PetClaimTicket(Serial serial) : base(serial)
        {
        }

        public override bool DisplayLootType
        {
            get => false;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version

            writer.Write(Stabled);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            Stabled = reader.ReadEntity<BaseCreature>();
        }

        public override void Delete()
        {
            Stabled?.Delete();

            base.Delete();
        }
    }
}