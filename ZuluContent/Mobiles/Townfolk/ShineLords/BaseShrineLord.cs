using System;
using System.Collections.Generic;
using System.Linq;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
    [Serializable(0, false)]
    public abstract partial class BaseShrineLord : BaseVendor
    {
        protected override List<SBInfo> SBInfos { get; } = new();

        [SerializableField(0)]
        [Tidy]
        [SerializableFieldAttr("[CommandProperty(AccessLevel.GameMaster)]")]
        private Dictionary<uint, ushort> _shrineCollections = new();

        public virtual Type[] ShrineAcceptTypes => null;

        public override bool IsActiveVendor => false;
        public override bool IsInvulnerable => true;

        public override bool InitialInnocent => true;

        public override void InitSBInfo()
        {
        }

        public BaseShrineLord() : base(null)
        {
            SpeechHue = 0;

            SetStr(304, 400);
            SetDex(102, 150);
            SetInt(204, 300);

            SetDamage(10, 23);

            Fame = 1000;
            Karma = 10000;
        }

        private void AcceptShrine(BaseShrine shrine)
        {
            Say(shrine is Shell
                ? $"You found Shell piece {shrine.Piece + 1} of 9."
                : $"You found Pentagram piece {shrine.Piece + 1} of 9.");

            shrine.Delete();
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (dropped is BaseShrine shrine && ShrineAcceptTypes.Any(acceptType => shrine.GetType().IsAssignableTo(acceptType)))
            {
                if (_shrineCollections.TryGetValue(from.Serial.Value, out var shrinesCollected))
                {
                    if ((shrinesCollected & (1 << shrine.Piece)) == 0)
                    {
                        shrinesCollected = (ushort) (shrinesCollected | 1 << shrine.Piece);
                        _shrineCollections[from.Serial.Value] = shrinesCollected;
                        AcceptShrine(shrine);

                        if (shrinesCollected == 0x1FF)
                        {
                            Say("Congratulations! You found all 9 pieces!");
                            from.SendGump(new ShrineLordGump(this));
                        }
                    }
                    else
                    {
                        Say("You've already given me that piece.");
                        return false;
                    }
                }
                else
                {
                    _shrineCollections[from.Serial.Value] = (ushort)(0 | 1 << shrine.Piece);
                    AcceptShrine(shrine);
                }
            }
            else
            {
                PublicOverheadMessage(MessageType.Regular, 0x3B2, 500607); // I'm not interested in that.
                return false;
            }

            return base.OnDragDrop(from, dropped);
        }
        
        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            
            list.Add(new HelpEntry(from, this));
        }
        
        private class HelpEntry : ContextMenuEntry
        {
            private readonly BaseCreature m_Mobile;

            public HelpEntry(Mobile from, BaseCreature creature) : base(-1938963, 8)
            {
                m_Mobile = creature;
            }

            public override void OnClick()
            {
                m_Mobile.Say("What to do with all 9 Pentagram/Shell pieces?");
                m_Mobile.Say("Bring them to me and drop them on me.");
                m_Mobile.Say("You do not have to bring them all at the same time.");
                m_Mobile.Say("Pentagram pieces can be found on stronger monsters.");
                m_Mobile.Say("Shell pieces can be found while fishing.");
                m_Mobile.Say("Good luck brave traveler!");
            }
        }
    }
}