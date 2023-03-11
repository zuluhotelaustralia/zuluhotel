using System;
using System.Collections.Generic;
using ModernUO.Serialization;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;

namespace Server.Mobiles
{
    [SerializationGenerator(0, false)]
    public partial class PoisonElementShrineLord : BaseShrineLord
    {
        public override bool CanTeach => false;

        public override Type[] ShrineAcceptTypes => new[] { typeof(PoisonElementalPentagram) };

        [Constructible]
        public PoisonElementShrineLord()
        {
            Title = "the Poison Element Shrine Lord";

            Body = 0x0d;

            Hue = 264;
        }

        public override void InitBody()
        {
            base.InitBody();
            Name += " the Poison Element Shrine Lord";
        }

        public override bool ClickTitle => false; // Do not display title in OnSingleClick
    }
}