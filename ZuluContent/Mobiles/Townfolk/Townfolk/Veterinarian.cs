using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Veterinarian : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;


        [Constructible]
        public Veterinarian() : base("the Vet")
        {
            SetSkill(SkillName.AnimalLore, 100.0);
            SetSkill(SkillName.Veterinary, 100.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBVeterinarian());
        }

        [Constructible]
        public Veterinarian(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}