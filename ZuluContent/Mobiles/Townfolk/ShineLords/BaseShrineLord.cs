using System;
using System.Collections.Generic;
using System.Linq;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
    [Serializable(0, false)]
    public abstract partial class BaseShrineLord : BaseVendor
    {
        protected override List<SBInfo> SBInfos { get; } = new();
        
        /*[SerializableField(0)]
        private Dictionary<Serial, List<BaseShrine>> _shrineCollections { get; set; }*/
        
        protected Type ShrineAcceptType { get; set; }

        public override bool IsActiveVendor => false;
        public override bool IsInvulnerable => true;

        public override void InitSBInfo()
        {
        }

        public BaseShrineLord() : base(null)
        {
            SpeechHue = 0;

            Body = 0xC7;

            SetStr(304, 400);
            SetDex(102, 150);
            SetInt(204, 300);

            SetDamage(10, 23);

            Fame = 1000;
            Karma = 10000;
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            /*if (dropped is BaseShrine shrine && shrine.GetType() == ShrineAcceptType)
            {
                if (_shrineCollections.TryGetValue(from.Serial, out var shrines))
                {
                    var existingShrine = shrines.FirstOrDefault(existing => existing.Piece == shrine.Piece);

                    if (existingShrine == null)
                    {
                        shrines.Add(shrine);
                        // _shrineCollections[from.Serial] = shrines;

                        if (shrines.Count == 9)
                        {
                            Say("Congratulations! You collected all 9!");
                        }
                    }
                }
                else
                {
                    _shrineCollections[from.Serial] = new List<BaseShrine> { shrine };
                }
            }
            else
            {
                PublicOverheadMessage(MessageType.Regular, 0x3B2, 500607); // I'm not interested in that.
                return false;
            }*/

            return base.OnDragDrop(from, dropped);
        }
    }
}