using System;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Seventh
{
    public class GateTravelSpell : MagerySpell
    {
        private readonly RunebookEntry m_Entry;

        public GateTravelSpell(Mobile caster, Item spellItem) : this(caster, spellItem, null)
        {
        }

        public GateTravelSpell(Mobile caster, Item spellItem, RunebookEntry entry) : base(caster, spellItem)
        {
            m_Entry = entry;
        }


        public override void OnCast()
        {
            if (m_Entry == null)
                Caster.Target = new InternalTarget(this);
            else
                Effect(m_Entry.Location, m_Entry.Map, true);
        }

        private bool GateExistsAt(Map map, Point3D loc)
        {
            var _gateFound = false;

            IPooledEnumerable eable = map.GetItemsInRange(loc, 0);
            foreach (Item item in eable)
                if (item is Moongate || item is PublicMoongate)
                {
                    _gateFound = true;
                    break;
                }

            eable.Free();

            return _gateFound;
        }

        public void Effect(Point3D loc, Map map, bool checkMulti)
        {
            if (map == null || Caster.Map != map)
            {
                Caster.SendLocalizedMessage(1005570); // You can not gate to another facet.
            }
            else if (!SpellHelper.CheckTravel(Caster, TravelCheckType.GateFrom))
            {
            }
            else if (!SpellHelper.CheckTravel(Caster, map, loc, TravelCheckType.GateTo))
            {
            }
            else if (map == Map.Felucca && Caster is PlayerMobile && ((PlayerMobile) Caster).Young)
            {
                Caster.SendLocalizedMessage(
                    1049543); // You decide against traveling to Felucca while you are still young.
            }
            else if (Caster.Kills >= 5 && map != Map.Felucca)
            {
                Caster.SendLocalizedMessage(1019004); // You are not allowed to travel there.
            }
            else if (Caster.Criminal)
            {
                Caster.SendLocalizedMessage(1005561, "", 0x22); // Thou'rt a criminal and cannot escape so easily.
            }
            else if (SpellHelper.CheckCombat(Caster))
            {
                Caster.SendLocalizedMessage(1005564, "", 0x22); // Wouldst thou flee during the heat of battle??
            }
            else if (!map.CanSpawnMobile(loc.X, loc.Y, loc.Z))
            {
                Caster.SendLocalizedMessage(501942); // That location is blocked.
            }
            else if (checkMulti && SpellHelper.CheckMulti(loc, map))
            {
                Caster.SendLocalizedMessage(501942); // That location is blocked.
            }
            else if (CheckSequence())
            {
                Caster.SendLocalizedMessage(501024); // You open a magical gate to another location

                Effects.PlaySound(Caster.Location, Caster.Map, 0x20E);

                var firstGate = new InternalItem(loc, map);
                firstGate.MoveToWorld(Caster.Location, Caster.Map);

                Effects.PlaySound(loc, map, 0x20E);

                var secondGate = new InternalItem(Caster.Location, Caster.Map);
                secondGate.MoveToWorld(loc, map);
            }

            FinishSequence();
        }

        [DispellableField]
        private class InternalItem : Moongate
        {
            public InternalItem(Point3D target, Map map) : base(target, map)
            {
                Map = map;

                if (ShowFeluccaWarning && map == Map.Felucca)
                    ItemID = 0xDDA;

                Dispellable = true;

                var t = new InternalTimer(this);
                t.Start();
            }

            public InternalItem(Serial serial) : base(serial)
            {
            }

            public override bool ShowFeluccaWarning
            {
                get { return false; }
            }

            public override void Serialize(IGenericWriter writer)
            {
                base.Serialize(writer);
            }

            public override void Deserialize(IGenericReader reader)
            {
                base.Deserialize(reader);

                Delete();
            }

            private class InternalTimer : Timer
            {
                private readonly Item m_Item;

                public InternalTimer(Item item) : base(TimeSpan.FromSeconds(30.0))
                {
                    Priority = TimerPriority.OneSecond;
                    m_Item = item;
                }

                protected override void OnTick()
                {
                    m_Item.Delete();
                }
            }
        }

        private class InternalTarget : Target
        {
            private readonly GateTravelSpell m_Owner;

            public InternalTarget(GateTravelSpell owner) : base(12, false, TargetFlags.None)
            {
                m_Owner = owner;

                owner.Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, 501029); // Select Marked item.
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is RecallRune)
                {
                    var rune = (RecallRune) o;

                    if (rune.Marked)
                        m_Owner.Effect(rune.Target, rune.TargetMap, true);
                    else
                        from.SendLocalizedMessage(501803); // That rune is not yet marked.
                }
                else if (o is Runebook)
                {
                    var e = ((Runebook) o).Default;

                    if (e != null)
                        m_Owner.Effect(e.Location, e.Map, true);
                    else
                        from.SendLocalizedMessage(502354); // Target is not marked.
                }
                else if (o is Key && ((Key) o).KeyValue != 0 && ((Key) o).Link is BaseBoat)
                {
                    var boat = ((Key) o).Link as BaseBoat;

                    if (!boat.Deleted && boat.CheckKey(((Key) o).KeyValue))
                        m_Owner.Effect(boat.GetMarkedLocation(), boat.Map, false);
                    else
                        from.NetState.SendMessageLocalized(from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 501030,
                            from.Name, ""); // I can not gate travel from that object.
                }
                else
                {
                    from.NetState.SendMessageLocalized(from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 501030,
                        from.Name, ""); // I can not gate travel from that object.
                }
            }

            protected override void OnNonlocalTarget(Mobile from, object o)
            {
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}