using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class SmithBag : Bag
    {
        [Constructable]
        public SmithBag() : this(5000)
        {
        }

        [Constructable]
        public SmithBag(int amount)
        {
            DropItem(new IronIngot(amount));
            DropItem(new GoldIngot(amount));
            DropItem(new SpikeIngot(amount));
            DropItem(new FruityIngot(amount));
            DropItem(new BronzeIngot(amount));
            DropItem(new IceRockIngot(amount));
            DropItem(new BlackDwarfIngot(amount));
            DropItem(new DullCopperIngot(amount));
            DropItem(new PlatinumIngot(amount));
            DropItem(new SilverRockIngot(amount));
            DropItem(new DarkPaganIngot(amount));
            DropItem(new CopperIngot(amount));
            DropItem(new MysticIngot(amount));
            DropItem(new SpectralIngot(amount));
            DropItem(new OldBritainIngot(amount));
            DropItem(new OnyxIngot(amount));
            DropItem(new RedElvenIngot(amount));
            DropItem(new UndeadIngot(amount));
            DropItem(new PyriteIngot(amount));
            DropItem(new VirginityIngot(amount));
            DropItem(new MalachiteIngot(amount));
            DropItem(new LavarockIngot(amount));
            DropItem(new AzuriteIngot(amount));
            DropItem(new DripstoneIngot(amount));
            DropItem(new ExecutorIngot(amount));
            DropItem(new PeachblueIngot(amount));
            DropItem(new DestructionIngot(amount));
            DropItem(new AnraIngot(amount));
            DropItem(new CrystalIngot(amount));
            DropItem(new DoomIngot(amount));
            DropItem(new GoddessIngot(amount));
            DropItem(new NewZuluIngot(amount));
            DropItem(new EbonTwilightSapphireIngot(amount));
            DropItem(new DarkSableRubyIngot(amount));
            DropItem(new RadiantNimbusDiamondIngot(amount));

            DropItem(new Tongs(amount));
            DropItem(new TinkerTools(amount));

        }

        public SmithBag(Serial serial) : base(serial)
        {
        }

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
    }
}
