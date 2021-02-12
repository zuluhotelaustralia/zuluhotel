using Scripts.Zulu.Engines.Classes;
using Server.Targeting;
using Server.Engines.Craft;
using Server.Mobiles;
using static Server.Configurations.MessageHueConfiguration;

namespace Server.Items
{
    public abstract class BaseOre : Item
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource { get; set; }

        public override string DefaultName => $"{CraftResources.GetName(Resource)} ore";

        public abstract BaseIngot GetIngot();

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
                    throw new System.Exception("Unsupported ore item, skipping.");
                    break;
                }
            }
        }

        public BaseOre(CraftResource resource) : this(resource, 1)
        {
        }

        public BaseOre(CraftResource resource, int amount) : base(0x19B9)
        {
            Stackable = true;
            Amount = amount;
            Hue = CraftResources.GetHue(resource);

            Resource = resource;
        }

        public BaseOre(Serial serial) : base(serial)
        {
        }

        private bool IsForge(object obj)
        {
            if (obj.GetType().IsDefined(typeof(ForgeAttribute), false))
                return true;

            int itemID = 0;

            if (obj is Item)
                itemID = ((Item) obj).ItemID;
            else if (obj is StaticTarget)
                itemID = ((StaticTarget) obj).ItemID;

            return (itemID == 4017 || (itemID >= 6522 && itemID <= 6569));
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (RootParent is BaseCreature)
            {
                from.SendLocalizedMessage(500447); // That is not accessible
                return;
            }

            if (!from.InRange(this.GetWorldLocation(), 2))
            {
                from.SendLocalizedMessage(501976); // The ore is too far away.
                return;
            }

            var difficulty = Resource switch
            {
                CraftResource.Iron => 10.0,
                CraftResource.Spike => 15.0,
                CraftResource.Fruity => 20.0,
                CraftResource.IceRock => 25.0,
                CraftResource.BlackDwarf => 30.0,
                CraftResource.Bronze => 35.0,
                CraftResource.DarkPagan => 40.0,
                CraftResource.SilverRock => 45.0,
                CraftResource.Platinum => 50.0,
                CraftResource.DullCopper => 55.0,
                CraftResource.Mystic => 60.0,
                CraftResource.Copper => 65.0,
                CraftResource.Spectral => 70.0,
                CraftResource.Onyx => 75.0,
                CraftResource.OldBritain => 80.0,
                CraftResource.RedElven => 84.0,
                CraftResource.Undead => 88.0,
                CraftResource.Pyrite => 91.0,
                CraftResource.Virginity => 94.0,
                CraftResource.Malachite => 95.0,
                CraftResource.Lavarock => 97.0,
                CraftResource.Azurite => 98.0,
                CraftResource.Dripstone => 100.0,
                CraftResource.Executor => 103.0,
                CraftResource.Peachblue => 106.0,
                CraftResource.Destruction => 109.0,
                CraftResource.Anra => 112.0,
                CraftResource.Crystal => 115.0,
                CraftResource.Doom => 118.0,
                CraftResource.Goddess => 121.0,
                CraftResource.NewZulu => 125.0,
                CraftResource.EbonTwilightSapphire => 125.0,
                CraftResource.DarkSableRuby => 125.0,
                CraftResource.RadiantNimbusDiamond => 125.0,
                _ => 0.0
            };

            IPooledEnumerable eable = from.Map.GetItemsInRange(from.Location, 1);
            foreach (Item nearbyItem in eable)
            {
                if (IsForge(nearbyItem))
                {
                    if (!from.ShilCheckSkill(SkillName.Mining, (int) difficulty, 0))
                    {
                        Consume(1);
                        from.SendAsciiMessage(MessageFailureHue, "You destroy some ore.");
                        return;
                    }

                    var ingot = GetIngot();
                    ingot.Amount = Amount;
                    from.PlaySound(0x2B);
                    from.AddToBackpack(ingot);
                    from.SendAsciiMessage(MessageSuccessHue,
                        $"You create {Amount} ingots and place them in your pack.");
                    Delete();
                    return;
                }
            }

            from.SendAsciiMessage(MessageFailureHue, "You must be near a forge to smelt ore!");
        }
    }
}