using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class BagOfAllReagents : Bag
    {

        [Constructible]
public BagOfAllReagents() : this(50)
        {
        }


        [Constructible]
public BagOfAllReagents(int amount)
        {
            DropItem(new BlackPearl(amount));
            DropItem(new Bloodmoss(amount));
            DropItem(new Garlic(amount));
            DropItem(new Ginseng(amount));
            DropItem(new MandrakeRoot(amount));
            DropItem(new Nightshade(amount));
            DropItem(new SulfurousAsh(amount));
            DropItem(new SpidersSilk(amount));
            DropItem(new BatWing(amount));
            DropItem(new DaemonBlood(amount));
            DropItem(new NoxCrystal(amount));
            DropItem(new PigIron(amount));
            DropItem(new Blackmoor(amount));
            DropItem(new Bloodspawn(amount));
            DropItem(new Brimstone(amount));
            DropItem(new DaemonBone(amount));
            DropItem(new DragonsBlood(amount));
            DropItem(new EyeOfNewt(amount));
            DropItem(new Obsidian(amount));
            DropItem(new Pumice(amount));
            DropItem(new VialOfBlood(amount));
            DropItem(new VolcanicAsh(amount));
            DropItem(new WyrmsHeart(amount));
            DropItem(new ExecutionersCap(amount));
            DropItem(new Bone(amount));
            DropItem(new DeadWood(amount));
            DropItem(new FertileDirt(amount));
        }

        [Constructible]
public BagOfAllReagents(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
