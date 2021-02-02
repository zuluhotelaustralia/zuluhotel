using System;
using System.Collections;
using Scripts.Zulu.Engines.Classes;
using static Scripts.Zulu.Engines.Classes.ZuluClass;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using static Scripts.Zulu.Engines.Classes.SkillCheck;

namespace Server.SkillHandlers
{
    public class AnimalTaming
    {
        private static readonly int MaxDistance = 20;
        private static readonly int PrevTamedMinus = 20;
        private static readonly TimeSpan DelayBetweenSpeech = TimeSpan.FromSeconds(3.0);
        private static readonly int PointMultiplier = 15;
        private static readonly TimeSpan UnresponsiveTime = TimeSpan.FromSeconds(300.0);


        private static Hashtable m_BeingTamed = new Hashtable();

        public static void Initialize()
        {
            SkillInfo.Table[(int) SkillName.AnimalTaming].Callback = OnUse;
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.RevealingAction();

            m.Target = new InternalTarget();
            m.RevealingAction();

            m.SendLocalizedMessage(502789); // Tame which animal?

            return Configs[SkillName.AnimalTaming].Delay;
        }

        public static bool AngerBeast(BaseCreature creature, Mobile from, bool isClassedRanger)
        {
            var chance = 75;
            if (isClassedRanger)
                chance = (int) (chance / ClasseBonus);

            if (Utility.Random(100) < chance)
            {
                creature.PublicOverheadMessage(MessageType.Regular, 0x3B2, true,
                    $"{from.Name} has angered the beast!");
                creature.PlaySound(creature.GetAngerSound());
                creature.Direction = creature.GetDirectionTo(from);
                creature.BardEndTime = DateTime.Now;
                creature.BardPacified = false;
                creature.DoHarmful(from, true);
                return true;
            }

            return false;
        }

        public static void CalmBeast(BaseCreature creature, Mobile from)
        {
            PacifyBeast(creature, from);
            creature.PublicOverheadMessage(MessageType.Regular, 0x3B2, true,
                $"{from.Name} has calmed the beast!");
        }

        public static void PacifyBeast(BaseCreature creature, Mobile from)
        {
            from.Combatant = null;
            creature.Combatant = null;
            creature.Warmode = false;
            creature.Pacify(from, DateTime.Now + TimeSpan.FromSeconds(1.0));
        }

        private class InternalTarget : Target
        {
            public InternalTarget() : base(MaxDistance, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                from.RevealingAction();

                if (!(targeted is BaseCreature))
                {
                    from.SendAsciiMessage(33, "You cannot tame this!");
                    return;
                }

                BaseCreature creature = (BaseCreature) targeted;

                if (!creature.Tamable)
                {
                    from.SendAsciiMessage(33, "You can't tame that!");
                    return;
                }

                if (creature.Controlled)
                {
                    from.SendAsciiMessage(33, "That creature looks pretty tame already.");
                    return;
                }

                var difficulty = creature.MinTameSkill;

                if (from.Skills[SkillName.AnimalTaming].Value < difficulty)
                {
                    from.SendAsciiMessage(33, "You have no chance of taming this creature!");
                    return;
                }

                if (m_BeingTamed.Contains(targeted))
                {
                    creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502802,
                        from.NetState); // Someone else is already taming this.
                    return;
                }

                difficulty += 10;

                var timesPreviouslyTamed = creature.Owners.Count;

                difficulty -= PrevTamedMinus * timesPreviouslyTamed;
                if (difficulty < 1.0)
                    difficulty = 1.0;

                var calmingDifficulty = (int) difficulty + 10;

                var isClassedRanger = (from as PlayerMobile)?.ZuluClass.Type == ZuluClassType.Ranger;

                if (creature.UnresponsiveToTamingEndTime < DateTime.Now)
                {
                    if ((from as IShilCheckSkill)?.CheckSkill(SkillName.AnimalLore, calmingDifficulty,
                        0) == true && creature.Warmode)
                    {
                        CalmBeast(creature, from);
                    }
                    else if (creature.CreatureType == CreatureType.Dragonkin &&
                             AngerBeast(creature, from, isClassedRanger))
                        return;
                }
                else
                {
                    AngerBeast(creature, from, isClassedRanger);
                    from.SendAsciiMessage(33, "The creature is unresponsive to taming at this time.");
                    return;
                }

                m_BeingTamed[targeted] = from;

                new InternalTimer(from, creature, isClassedRanger, difficulty).Start();
            }

            private class InternalTimer : Timer
            {
                private static readonly int MaxCount = 4;
                private Mobile m_Tamer;
                private bool m_IsClassedRanger;
                private BaseCreature m_Creature;
                private int m_Count;
                private double m_Difficulty;

                public InternalTimer(Mobile tamer, BaseCreature creature, bool isClassedRanger,
                    double difficulty) : base(
                    TimeSpan.FromSeconds(0.0),
                    DelayBetweenSpeech, MaxCount)
                {
                    m_Difficulty = difficulty;
                    m_Tamer = tamer;
                    m_IsClassedRanger = isClassedRanger;
                    m_Creature = creature;
                    Priority = TimerPriority.TwoFiftyMS;
                }

                protected override void OnTick()
                {
                    m_Count++;

                    if (!m_Tamer.InRange(m_Creature, MaxDistance))
                    {
                        m_BeingTamed.Remove(m_Creature);
                        m_Tamer.SendAsciiMessage(33, "You are too far away to continue taming.");
                        Stop();
                    }
                    else if (!m_Tamer.CheckAlive())
                    {
                        m_BeingTamed.Remove(m_Creature);
                        m_Tamer.SendAsciiMessage(33, "You are dead and cannot continue taming.");
                        Stop();
                    }
                    else if (!m_Tamer.CanSee(m_Creature) || !m_Tamer.InLOS(m_Creature) || !CanPath())
                    {
                        m_BeingTamed.Remove(m_Creature);
                        m_Tamer.SendAsciiMessage(33,
                            "You do not have a clear path to the animal you are taming, and must cease your attempt.");
                        Stop();
                    }
                    else if (!m_Creature.Tamable)
                    {
                        m_BeingTamed.Remove(m_Creature);
                        m_Tamer.SendAsciiMessage(33, "That creature cannot be tamed.");
                        Stop();
                    }
                    else if (m_Creature.Controlled)
                    {
                        m_BeingTamed.Remove(m_Creature);
                        m_Tamer.SendAsciiMessage(33, $"{m_Creature.Name} belongs to someone else!");
                        Stop();
                    }
                    else if (m_Count == 1)
                    {
                        m_Tamer.PublicOverheadMessage(MessageType.Regular, 55, true,
                            $"What a nice {m_Creature.Name}");
                    }
                    else if (m_Count == 2)
                    {
                        m_Tamer.PublicOverheadMessage(MessageType.Regular, 55, true,
                            $"I've always wanted a {m_Creature.Name} like you.");
                    }
                    else if (m_Count == 3)
                    {
                        m_Tamer.PublicOverheadMessage(MessageType.Regular, 55, true,
                            $"{m_Creature.Name}, will you be my friend?");
                    }
                    else
                    {
                        if (DateTime.Now < m_Creature.UnresponsiveToTamingEndTime)
                        {
                            m_BeingTamed.Remove(m_Creature);
                            m_Tamer.SendAsciiMessage(33, $"You failed to tame the creature.");
                        }
                        else
                        {
                            m_Creature.UnresponsiveToTamingEndTime = DateTime.Now;

                            if ((m_Tamer as IShilCheckSkill)?.CheckSkill(SkillName.AnimalTaming, (int) m_Difficulty,
                                (int) m_Difficulty * PointMultiplier) == false)
                            {
                                m_BeingTamed.Remove(m_Creature);
                                m_Tamer.SendAsciiMessage(33, $"You failed to tame the creature.");
                                var chance = 80 -
                                             (int) ((m_Tamer.Skills[SkillName.AnimalTaming].Value - m_Difficulty + 20) *
                                                    2);
                                if (m_IsClassedRanger)
                                    chance = (int) (chance / ClasseBonus);


                                if (chance < 1)
                                    chance = 1;


                                if (Utility.Random(100) <= chance)
                                {
                                    m_Tamer.SendAsciiMessage(33, $"And have made the creature unresponsive to taming.");
                                    m_Creature.UnresponsiveToTamingEndTime = DateTime.Now + UnresponsiveTime;
                                }
                            }
                            else
                            {
                                m_Tamer.RevealingAction();
                                m_BeingTamed.Remove(m_Creature);
                                m_Tamer.SendAsciiMessage(55, $"You successfully tame the {m_Creature.Name}");
                                m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502799,
                                    m_Tamer.NetState); // It seems to accept you as master.
                                m_Creature.Owners.Add(m_Tamer);
                                m_Creature.SetControlMaster(m_Tamer);
                                if (m_Creature.Warmode)
                                    PacifyBeast(m_Creature, m_Tamer);
                            }
                        }
                    }
                }

                private bool CanPath()
                {
                    IPoint3D p = m_Tamer as IPoint3D;

                    if (p == null)
                        return false;

                    if (m_Creature.InRange(new Point3D(p), 1))
                        return true;

                    MovementPath path = new MovementPath(m_Creature, new Point3D(p));
                    return path.Success;
                }
            }
        }
    }
}