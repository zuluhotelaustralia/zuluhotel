using System;
using System.Collections.Generic;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
    public class Scribe : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override NpcGuild NpcGuild => NpcGuild.MagesGuild;

        private DateTime m_NextShush;
        public static readonly TimeSpan ShushDelay = TimeSpan.FromMinutes(1);


        [Constructible]
        public Scribe() : base("the Scribe")
        {
            SetSkill(SkillName.EvalInt, 60.0);
            SetSkill(SkillName.Inscribe, 90.0);
            SetSkill(SkillName.Magery, 70.0);
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBScribe());
            if (zuluStyleSell)
                m_SBInfos.Add(new SAll());
        }

        public override VendorShoeType ShoeType => Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals;

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new Robe(Utility.RandomNeutralHue()));
        }

        public override bool HandlesOnSpeech(Mobile from)
        {
            return from.Player;
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            base.OnSpeech(e);

            if (!e.Handled && m_NextShush <= DateTime.Now && InLOS(e.Mobile))
            {
                Direction = GetDirectionTo(e.Mobile);

                PlaySound(Female ? 0x32F : 0x441);
                PublicOverheadMessage(MessageType.Regular, 0x3B2, 1073990); // Shhhh!

                m_NextShush = DateTime.Now + ShushDelay;
                e.Handled = true;
            }
        }

        [Constructible]
        public Scribe(Serial serial) : base(serial)
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