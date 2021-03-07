using System;
using System.Collections.Generic;
using Server.Network;

namespace Server.Items
{
    public enum TrapType
    {
        None,
        MagicTrap,
        ExplosionTrap,
        DartTrap,
        PoisonTrap
    }

    public abstract class TrapableContainer : BaseContainer, ITelekinesisable
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public TrapType TrapType { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int TrapStrength { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int TrapLevel { get; set; }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile TrappedBy { get; set; }

        public virtual bool TrapOnOpen => true;

        public TrapableContainer(int itemID) : base(itemID)
        {
        }

        public TrapableContainer(Serial serial) : base(serial)
        {
        }

        private void SendMessageTo(Mobile to, int number, int hue)
        {
            if (Deleted || !to.CanSee(this))
                return;

            to.NetState.SendMessageLocalized(Serial, ItemID, MessageType.Regular, hue, 3, number);
        }

        private void SendMessageTo(Mobile to, string text, int hue)
        {
            if (Deleted || !to.CanSee(this))
                return;

            to.NetState.SendMessage(Serial, ItemID, MessageType.Regular, hue, 3, false, "ENU", "", text);
        }

        public virtual bool ExecuteTrap(Mobile from)
        {
            if (TrapType != TrapType.None)
            {
                Point3D loc = GetWorldLocation();
                Map facet = Map;

                if (from.AccessLevel >= AccessLevel.GameMaster)
                {
                    SendMessageTo(from, "That is trapped, but you open it with your godly powers.", 0x3B2);
                    return false;
                }
                
                SendMessageTo(from, 502999, 0x3B2); // You set off a trap!

                switch (TrapType)
                {
                    case TrapType.ExplosionTrap:
                    {

                        int damage = Utility.RandomMinMax(0, 15) * TrapStrength * 2;
                        
                        Effects.SendLocationEffect(loc, facet, 0x36BD, 15, 10);
                        Effects.PlaySound(loc, facet, 0x307);

                        IPooledEnumerable eable = facet.GetMobilesInRange(new Point3D(loc), TrapStrength);

                        foreach (Mobile m in eable)
                        {
                            if (m.InRange(loc, TrapStrength))
                            {
                                m.Damage(damage);
                                // Your skin blisters from the heat!
                                m.LocalOverheadMessage(MessageType.Regular, 0x2A, 503000);
                                
                                Effects.SendLocationEffect(new Point3D(m), facet, 0x36BD, 15, 10);
                                Effects.PlaySound(new Point3D(m), facet, 0x307);
                            }
                        }

                        eable.Free();
                        
                        break;
                    }
                    case TrapType.MagicTrap:
                    {
                        int damage = Utility.RandomMinMax(0, 15) * TrapStrength * 2;
                        if (from.InRange(loc, 1))
                            from.Damage(damage);

                        Effects.PlaySound(loc, Map, 0x307);

                        Effects.SendLocationEffect(new Point3D(loc.X - 1, loc.Y, loc.Z), Map, 0x36BD, 15);
                        Effects.SendLocationEffect(new Point3D(loc.X + 1, loc.Y, loc.Z), Map, 0x36BD, 15);

                        Effects.SendLocationEffect(new Point3D(loc.X, loc.Y - 1, loc.Z), Map, 0x36BD, 15);
                        Effects.SendLocationEffect(new Point3D(loc.X, loc.Y + 1, loc.Z), Map, 0x36BD, 15);

                        Effects.SendLocationEffect(new Point3D(loc.X + 1, loc.Y + 1, loc.Z + 11), Map, 0x36BD, 15);

                        break;
                    }
                    case TrapType.DartTrap:
                    {
                        if (from.InRange(loc, 3))
                        {
                            int damage = Utility.RandomMinMax(0, 15) * TrapStrength * 2;

                            from.Damage(damage);

                            // A dart imbeds itself in your flesh!
                            from.LocalOverheadMessage(MessageType.Regular, 0x62, 502998);
                        }

                        Effects.PlaySound(loc, facet, 0x223);

                        break;
                    }
                    case TrapType.PoisonTrap:
                    {
                        if (from.InRange(loc, 3))
                        {
                            var poison = Poison.GetPoison(Math.Min(TrapStrength, Poison.Poisons.Count - 1));

                            from.ApplyPoison(from, poison);

                            // You are enveloped in a noxious green cloud!
                            from.LocalOverheadMessage(MessageType.Regular, 0x44, 503004);
                        }

                        Effects.SendLocationEffect(loc, facet, 0x113A, 10, 20);
                        Effects.PlaySound(loc, facet, 0x231);

                        break;
                    }
                }

                TrapType = TrapType.None;
                TrapStrength = 0;
                TrapLevel = 0;
                return true;
            }

            return false;
        }

        public virtual void OnTelekinesis(Mobile from)
        {
            Effects.SendLocationParticles(EffectItem.Create(Location, Map, EffectItem.DefaultDuration), 0x376A, 9, 32,
                5022);
            Effects.PlaySound(Location, Map, 0x1F5);

            if (TrapOnOpen)
            {
                ExecuteTrap(from);
            }
        }

        public override void Open(Mobile from)
        {
            if (!TrapOnOpen || !ExecuteTrap(from))
                base.Open(from);
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(3); // version

            writer.Write(TrappedBy);
            writer.Write(TrapLevel);
            writer.Write(TrapStrength);
            writer.Write((int) TrapType);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 3:
                {
                    TrappedBy = reader.ReadEntity<Mobile>();
                    goto case 2;
                }
                case 2:
                {
                    TrapLevel = reader.ReadInt();
                    goto case 1;
                }
                case 1:
                {
                    TrapStrength = reader.ReadInt();
                    goto case 0;
                }
                case 0:
                {
                    TrapType = (TrapType) reader.ReadInt();
                    break;
                }
            }
        }
    }
}