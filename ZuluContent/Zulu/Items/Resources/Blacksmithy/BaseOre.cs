using System.Linq;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Engines.Craft;
using Server.Mobiles;
using static Server.Configurations.ResourceConfiguration;

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

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (RootParent is BaseCreature)
            {
                from.SendLocalizedMessage(500447); // That is not accessible
                return;
            }

            if (!from.InRange(GetWorldLocation(), 2))
            {
                from.SendLocalizedMessage(501976); // The ore is too far away.
                return;
            }

            if (!DefBlacksmithy.CheckForge(from, 1))
            {
                from.SendFailureMessage("You must be near a forge to smelt ore!");
                return;
            }

            var oreType = GetType();

            var oreEntry = ZhConfig.Resources.Ores.Entries.Single(e => e.ResourceType == oreType);

            var difficulty = oreEntry.SmeltSkillRequired;

            if (!from.ShilCheckSkill(SkillName.Mining, (int) difficulty, 0))
            {
                Consume(1);
                from.SendFailureMessage("You destroy some ore.");
                return;
            }

            var ingot = GetIngot();
            ingot.Amount = Amount;
            from.PlaySound(0x2B);
            from.AddToBackpack(ingot);
            from.SendSuccessMessage($"You create {Amount} ingots and place them in your pack.");
            Delete();
            return;
        }
    }
}