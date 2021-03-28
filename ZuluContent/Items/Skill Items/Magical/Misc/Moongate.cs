using System;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Regions;
using Server.Spells;

namespace Server.Items
{
    [DispellableFieldAttribute]
    public class Moongate : Item, IDispellable
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D Target { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Map TargetMap { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Dispellable { get; set; }

        public virtual bool ShowFeluccaWarning => false;
        
        [Constructible]
        public Moongate() : this(Point3D.Zero, null)
        {
            Dispellable = true;
        }


        [Constructible]
        public Moongate(bool bDispellable) : this(Point3D.Zero, null)
        {
            Dispellable = bDispellable;
        }


        [Constructible]
        public Moongate(Point3D target, Map targetMap) : base(0xF6C)
        {
            Movable = false;
            Light = LightType.Circle300;

            Target = target;
            TargetMap = targetMap;
        }

        [Constructible]
        public Moongate(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.Player)
                return;

            if (from.InRange(GetWorldLocation(), 1))
                CheckGate(from, 1);
            else
                from.SendLocalizedMessage(500446); // That is too far away.
        }

        public override bool OnMoveOver(Mobile mobile)
        {
            if (mobile.Player)
                CheckGate(mobile, 0);

            return true;
        }

        public virtual void CheckGate(Mobile m, int range)
        {
            new DelayTimer(m, this, range).Start();
        }

        public virtual void OnGateUsed(Mobile m)
        {
        }

        public virtual void UseGate(Mobile m)
        {
            ClientFlags flags = m.NetState == null ? ClientFlags.None : m.NetState.Flags;

            if (TargetMap == Map.Felucca && m is PlayerMobile && ((PlayerMobile) m).Young)
            {
                m.SendLocalizedMessage(1049543); // You decide against traveling to Felucca while you are still young.
            }
            else if (m.Kills >= 5 && TargetMap != Map.Felucca)
            {
                m.SendLocalizedMessage(1019004); // You are not allowed to travel there.
            }
            else if (m.Spell != null)
            {
                m.SendLocalizedMessage(1049616); // You are too busy to do that at the moment.
            }
            else if (TargetMap != null && TargetMap != Map.Internal)
            {
                BaseCreature.TeleportPets(m, Target, TargetMap);

                m.MoveToWorld(Target, TargetMap);

                if (m.AccessLevel == AccessLevel.Player || !m.Hidden)
                    m.PlaySound(0x1FE);

                OnGateUsed(m);
            }
            else
            {
                m.SendMessage("This moongate does not seem to go anywhere.");
            }
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version

            writer.Write(Target);
            writer.Write(TargetMap);

            // Version 1
            writer.Write(Dispellable);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            Target = reader.ReadPoint3D();
            TargetMap = reader.ReadMap();

            if (version >= 1)
                Dispellable = reader.ReadBool();
        }

        public virtual bool ValidateUse(Mobile from, bool message)
        {
            if (from.Deleted || Deleted)
                return false;

            if (from.Map != Map || !from.InRange(this, 1))
            {
                if (message)
                    from.SendLocalizedMessage(500446); // That is too far away.

                return false;
            }

            return true;
        }

        public virtual void BeginConfirmation(Mobile from)
        {
            if (IsInTown(from.Location, from.Map) && !IsInTown(Target, TargetMap) ||
                @from.Map != Map.Felucca && TargetMap == Map.Felucca && ShowFeluccaWarning)
            {
                if (from.AccessLevel == AccessLevel.Player || !from.Hidden)
                    from.SendSound(0x20E, from.Location);
                from.CloseGump<MoongateConfirmGump>();
                ;
                from.SendGump(new MoongateConfirmGump(from, this));
            }
            else
            {
                EndConfirmation(from);
            }
        }

        public virtual void EndConfirmation(Mobile from)
        {
            if (!ValidateUse(from, true))
                return;

            UseGate(from);
        }

        public virtual void DelayCallback(Mobile from, int range)
        {
            if (!ValidateUse(from, false) || !from.InRange(this, range))
                return;

            if (TargetMap != null)
                BeginConfirmation(from);
            else
                from.SendMessage("This moongate does not seem to go anywhere.");
        }

        public static bool IsInTown(Point3D p, Map map)
        {
            if (map == null)
                return false;

            GuardedRegion reg = (GuardedRegion) Region.Find(p, map).GetRegion(typeof(GuardedRegion));

            return reg != null && !reg.IsDisabled();
        }

        private class DelayTimer : Timer
        {
            private Mobile m_From;
            private Moongate m_Gate;
            private int m_Range;

            public DelayTimer(Mobile from, Moongate gate, int range) : base(TimeSpan.FromSeconds(1.0))
            {
                m_From = from;
                m_Gate = gate;
                m_Range = range;
            }

            protected override void OnTick()
            {
                m_Gate.DelayCallback(m_From, m_Range);
            }
        }
    }

    public class ConfirmationMoongate : Moongate
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public int GumpWidth { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int GumpHeight { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int TitleColor { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MessageColor { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int TitleNumber { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MessageNumber { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public string MessageString { get; set; }


        public ConfirmationMoongate() : this(Point3D.Zero, null)
        {
        }


        public ConfirmationMoongate(Point3D target, Map targetMap) : base(target, targetMap)
        {
        }

        public ConfirmationMoongate(Serial serial) : base(serial)
        {
        }

        public override void BeginConfirmation(Mobile from)
        {
            if (GumpWidth > 0 && GumpHeight > 0 && TitleNumber > 0 &&
                (MessageNumber > 0 || MessageString != null))
            {
                void Callback(bool okay)
                {
                    if (okay) EndConfirmation(@from);
                }

                from.CloseGump<WarningGump>();
                from.SendGump(new WarningGump(TitleNumber, TitleColor,
                    MessageString == null ? (object) MessageNumber : (object) MessageString, MessageColor,
                    GumpWidth, GumpHeight, Callback));
            }
            else
            {
                base.BeginConfirmation(from);
            }
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version

            writer.WriteEncodedInt(GumpWidth);
            writer.WriteEncodedInt(GumpHeight);

            writer.WriteEncodedInt(TitleColor);
            writer.WriteEncodedInt(MessageColor);

            writer.WriteEncodedInt(TitleNumber);
            writer.WriteEncodedInt(MessageNumber);

            writer.Write(MessageString);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                {
                    GumpWidth = reader.ReadEncodedInt();
                    GumpHeight = reader.ReadEncodedInt();

                    TitleColor = reader.ReadEncodedInt();
                    MessageColor = reader.ReadEncodedInt();

                    TitleNumber = reader.ReadEncodedInt();
                    MessageNumber = reader.ReadEncodedInt();

                    MessageString = reader.ReadString();

                    break;
                }
            }
        }
    }
    
    public class BlackMoongate : Moongate
    {
        public BlackMoongate() : this(Point3D.Zero, null)
        {
        }


        public BlackMoongate(Point3D target, Map targetMap) : base(target, targetMap)
        {
            ItemID = 0x1FD4;
        }

        public BlackMoongate(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class MoongateConfirmGump : Gump
    {
        private Mobile m_From;
        private Moongate m_Gate;

        public MoongateConfirmGump(Mobile from, Moongate gate) : base(20, 30)
        {
            m_From = from;
            m_Gate = gate;

            AddPage(0);

            AddBackground(0, 0, 420, 400, 5054);
            AddBackground(10, 10, 400, 380, 3000);

            AddHtml(20, 40, 380, 60,
                @"Dost thou wish to step into the moongate? Continue to enter the gate, Cancel to stay here", false,
                false);

            AddHtmlLocalized(55, 110, 290, 20, 1011012, false, false); // CANCEL
            AddButton(20, 110, 4005, 4007, 0, GumpButtonType.Reply, 0);

            AddHtmlLocalized(55, 140, 290, 40, 1011011, false, false); // CONTINUE
            AddButton(20, 140, 4005, 4007, 1, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 1)
                m_Gate.EndConfirmation(m_From);
        }
    }
}