using System;
using Server.Engines.Craft;

namespace Server.Items
{
    public class HeatingStand : BaseTool
    {
        public override CraftSystem CraftSystem => DefAlchemy.PlusCraftSystem;
        public int LitItemId { get; } = 0x184A;
        public int UnlitItemId { get; } = 0x1849;
        
        public override int ItemID 
        {
            get => base.ItemID;
            set
            {
                Light = value == LitItemId ? LightType.Circle150 : LightType.Empty;
                base.ItemID = value;
            }
        }
        
        [Constructible]
        public HeatingStand(int uses) : base(uses, 0xE9B)
        {
            Weight = 1.0;
        }
        
        [Constructible]
        public HeatingStand() : base(0x1849)
        {
            Light = LightType.Empty;
            Weight = 1.0;
        }
        
        [Constructible]
        public HeatingStand(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            ItemID = UnlitItemId;
        }

        public override void OnBeginCraft(Mobile from, CraftItem item, CraftSystem system)
        {
            if (ItemID == UnlitItemId)
            {
                ItemID = LitItemId;
                Effects.PlaySound(GetWorldLocation(), Map, 0x47);
            }
        }

        public override void OnEndCraft(Mobile @from, CraftItem craftItem, CraftSystem craftSystem)
        {
            if (!Deleted)
            {
                ItemID = UnlitItemId;
                Effects.PlaySound(GetWorldLocation(), Map, 0x3be);
            }
        }
    }
}