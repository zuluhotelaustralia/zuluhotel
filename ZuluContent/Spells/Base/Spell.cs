using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells.Second;
using System.Collections.Generic;
using Server.Engines.Magic;

namespace Server.Spells
{
    public abstract class Spell : ISpell
    {
        private Mobile m_Caster;
        private SpellInfo m_Info;
        private long m_StartCastTime;
        private bool m_SpellStrike;
        protected ElementalType m_DamageType;

        public abstract SpellCircle Circle { get; }
        public abstract SpellInfo GetSpellInfo();
        public SpellState State { get; set; }

        public Mobile Caster => m_Caster;

        public SpellInfo Info => m_Info;

        public string Name => m_Info.Name;

        public string Mantra => m_Info.Mantra;

        public Type[] Reagents => m_Info.Reagents;

        public Item Scroll { get; private set; }

        private static readonly TimeSpan NextSpellDelay = TimeSpan.FromSeconds(0.75);
        private static readonly TimeSpan AnimateDelay = TimeSpan.FromSeconds(1.5);

        public virtual SkillName CastSkill => SkillName.Magery;

        public virtual SkillName DamageSkill => SkillName.EvalInt;

        public virtual bool RevealOnCast => true;

        public virtual bool ClearHandsOnCast => true;

        public virtual bool ShowHandMovement => true;

        public virtual bool DelayedDamage => false;

        public virtual bool DelayedDamageStacking => true;
        //In reality, it's ANY delayed Damage spell Post-AoS that can't stack, but, only
        //Expo & Magic Arrow have enough delay and a short enough cast time to bring up
        //the possibility of stacking 'em.  Note that a MA & an Explosion will stack, but
        //of course, two MA's won't.

        private static Dictionary<Type, DelayedDamageContextWrapper> m_ContextTable =
            new Dictionary<Type, DelayedDamageContextWrapper>();

        public static T Create<T>(Mobile caster, Item scroll = null, bool spellStrike = false) where T : Spell
        {
            var spell = SpellRegistry.NewSpell<T>(caster, scroll);

            spell.m_Caster = caster;
            spell.Scroll = scroll;
            spell.m_SpellStrike = spellStrike;
            spell.m_Info = spell.GetSpellInfo();

            return spell;
        }

        private class DelayedDamageContextWrapper
        {
            private readonly Dictionary<Mobile, Timer> m_Contexts = new Dictionary<Mobile, Timer>();

            public void Add(Mobile m, Timer t)
            {
                if (m_Contexts.TryGetValue(m, out var oldTimer))
                {
                    oldTimer.Stop();
                    m_Contexts.Remove(m);
                }

                m_Contexts.Add(m, t);
            }

            public void Remove(Mobile m)
            {
                m_Contexts.Remove(m);
            }
        }

        public void StartDelayedDamageContext(Mobile m, Timer t)
        {
            if (DelayedDamageStacking)
                return; //Sanity

            if (!m_ContextTable.TryGetValue(GetType(), out var contexts))
            {
                contexts = new DelayedDamageContextWrapper();
                m_ContextTable.Add(GetType(), contexts);
            }

            contexts.Add(m, t);
        }

        public void RemoveDelayedDamageContext(Mobile m)
        {
            if (!m_ContextTable.TryGetValue(GetType(), out var contexts))
                return;

            contexts.Remove(m);
        }

        public void HarmfulSpell(Mobile m)
        {
            if (m is BaseCreature creature)
                creature.OnHarmfulSpell(m_Caster);
        }

        public Spell(Mobile caster, Item scroll, SpellInfo info)
        {
            m_Caster = caster;
            Scroll = scroll;
            m_Info = info;
        }

        public virtual bool IsCasting
        {
            get { return State == SpellState.Casting; }
        }

        public virtual void OnCasterHurt()
        {
            //Confirm: Monsters and pets cannot be disturbed.
            if (!Caster.Player)
                return;

            if (IsCasting)
            {
                object o = ProtectionSpell.Registry[m_Caster];
                bool disturb = true;

                if (o != null && o is double d)
                {
                    if (d > Utility.RandomDouble() * 100.0)
                        disturb = false;
                }

                if (disturb)
                    Disturb(DisturbType.Hurt, false, true);
            }
        }

        public virtual void OnCasterKilled()
        {
            Disturb(DisturbType.Kill);
        }

        public virtual void OnConnectionChanged()
        {
            FinishSequence();
        }

        public virtual bool OnCasterMoving(Direction d)
        {
            if (IsCasting && BlocksMovement)
            {
                m_Caster.SendLocalizedMessage(500111); // You are frozen and can not move.
                return false;
            }

            return true;
        }

        public bool OnCasterEquipping(Item item)
        {
          return true;
        }

        public bool OnCasterUsingObject(IEntity entity)
        {
          return true;
        }

        public virtual bool OnCasterEquiping(Item item)
        {
            if (IsCasting)
                Disturb(DisturbType.EquipRequest);

            return true;
        }

        public virtual bool OnCasterUsingObject(object o)
        {
            if (State == SpellState.Sequencing)
                Disturb(DisturbType.UseRequest);

            return true;
        }

        public virtual bool OnCastInTown(Region r)
        {
            return m_Info.AllowTown;
        }

        public virtual bool ConsumeReagents()
        {
            if (Scroll != null || !m_Caster.Player)
                return true;

            Container pack = m_Caster.Backpack;

            if (pack == null)
                return false;

            if (pack.ConsumeTotal(m_Info.Reagents, m_Info.Amounts) == -1)
                return true;

            return false;
        }

        public virtual double GetResistSkill(Mobile m)
        {
            return m.Skills[SkillName.MagicResist].Value;
        }


        public virtual double GetResistPercentForCircle(Mobile target, SpellCircle circle)
        {
            double firstPercent = target.Skills[SkillName.MagicResist].Value / 5.0;
            double secondPercent = target.Skills[SkillName.MagicResist].Value -
                                   ((Caster.Skills[CastSkill].Value - 20.0) / 5.0 + (1 + (int) circle) * 5.0);

            return (firstPercent > secondPercent ? firstPercent : secondPercent) /
                   2.0; // Seems should be about half of what stratics says.
        }

        public virtual double GetResistPercent(Mobile target)
        {
            return GetResistPercentForCircle(target, Circle);
        }

        public virtual bool CheckResisted(Mobile target)
        {
            double n = GetResistPercent(target);

            n /= 100.0;

            if (n <= 0.0)
                return false;

            if (n >= 1.0)
                return true;

            int maxSkill = (1 + (int) Circle) * 10;
            maxSkill += (1 + (int) Circle / 6) * 25;

            if (target.Skills[SkillName.MagicResist].Value < maxSkill)
                target.CheckSkill(SkillName.MagicResist, 0.0, target.Skills[SkillName.MagicResist].Cap);

            return n >= Utility.RandomDouble();
        }

        public virtual double GetDamageScalar(Mobile target)
        {
            double scalar = 1.0;

            double casterEI = m_Caster.Skills[DamageSkill].Value;
            double targetRS = target.Skills[SkillName.MagicResist].Value;

            //m_Caster.CheckSkill( DamageSkill, 0.0, 120.0 );

            if (casterEI > targetRS)
                scalar = 1.0 + (casterEI - targetRS) / 500.0;
            else
                scalar = 1.0 + (casterEI - targetRS) / 200.0;

            // magery damage bonus, -25% at 0 skill, +0% at 100 skill, +5% at 120 skill
            scalar += (m_Caster.Skills[CastSkill].Value - 100.0) / 400.0;

            if (!target.Player && !target.Body.IsHuman /*&& !Core.AOS*/)
                scalar *= 2.0; // Double magery damage to monsters/animals if not AOS

            target.Region.SpellDamageScalar(m_Caster, target, ref scalar);

            return scalar;
        }

        public virtual double GetSlayerDamageScalar(Mobile defender)
        {
            Spellbook atkBook = Spellbook.FindEquippedSpellbook(m_Caster);

            double scalar = 1.0;
            if (atkBook != null)
            {
                SlayerEntry atkSlayer = SlayerGroup.GetEntryByName(atkBook.Slayer);
                SlayerEntry atkSlayer2 = SlayerGroup.GetEntryByName(atkBook.Slayer2);

                if (atkSlayer != null && atkSlayer.Slays(defender) || atkSlayer2 != null && atkSlayer2.Slays(defender))
                {
                    defender.FixedEffect(0x37B9, 10, 5); //TODO: Confirm this displays on OSIs
                    scalar = 2.0;
                }


                if (scalar != 1.0)
                    return scalar;
            }

            ISlayer defISlayer = Spellbook.FindEquippedSpellbook(defender);

            if (defISlayer == null)
                defISlayer = defender.Weapon as ISlayer;

            if (defISlayer != null)
            {
                SlayerEntry defSlayer = SlayerGroup.GetEntryByName(defISlayer.Slayer);
                SlayerEntry defSlayer2 = SlayerGroup.GetEntryByName(defISlayer.Slayer2);

                if (defSlayer != null && defSlayer.Group.OppositionSuperSlays(m_Caster) ||
                    defSlayer2 != null && defSlayer2.Group.OppositionSuperSlays(m_Caster))
                    scalar = 2.0;
            }

            return scalar;
        }

        public virtual void DoFizzle()
        {
            m_Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, 502632); // The spell fizzles.

            if (m_Caster.Player)
            {
                m_Caster.FixedEffect(0x3735, 6, 30);

                m_Caster.PlaySound(0x5C);
            }
        }

        private CastTimer m_CastTimer;
        private AnimTimer m_AnimTimer;

        public void Disturb(DisturbType type)
        {
            Disturb(type, true, false);
        }

        public virtual bool CheckDisturb(DisturbType type, bool firstCircle, bool resistable)
        {
            if (resistable && IsWand())
                return false;

            return true;
        }

        public void Disturb(DisturbType type, bool firstCircle, bool resistable)
        {
            if (!CheckDisturb(type, firstCircle, resistable))
                return;

            switch (State)
            {
                case SpellState.Casting when !firstCircle && this is MagerySpell && ((MagerySpell) this).Circle == SpellCircle.First:
                    return;
                case SpellState.Casting:
                {
                    State = SpellState.None;
                    m_Caster.Spell = null;

                    OnDisturb(type, true);

                    m_CastTimer?.Stop();

                    m_AnimTimer?.Stop();

                    m_Caster.NextSpellTime = Core.TickCount + (int)GetDisturbRecovery().TotalMilliseconds;
                    break;
                }
                case SpellState.Sequencing when !firstCircle && this is MagerySpell && ((MagerySpell) this).Circle == SpellCircle.First:
                    return;
                case SpellState.Sequencing:
                    State = SpellState.None;
                    m_Caster.Spell = null;

                    OnDisturb(type, false);

                    Target.Cancel(m_Caster);
                    break;
            }
        }

        public virtual void DoHurtFizzle()
        {
            m_Caster.FixedEffect(0x3735, 6, 30);
            m_Caster.PlaySound(0x5C);
        }

        public virtual void OnDisturb(DisturbType type, bool message)
        {
            if (message)
                m_Caster.SendLocalizedMessage(500641); // Your concentration is disturbed, thus ruining thy spell.
        }

        public virtual bool CheckCast()
        {
            return true;
        }

        public virtual bool IsWand()
        {
            return Scroll is BaseWand;
        }

        public virtual void SayMantra()
        {
            if (IsWand() || m_SpellStrike)
                return;

            if (!string.IsNullOrEmpty(m_Info.Mantra) &&
                ((m_Caster is BaseCreature creature && creature.SaySpellMantra) || m_Caster.Player))
            {
                m_Caster.PublicOverheadMessage(MessageType.Spell, m_Caster.SpeechHue, true, m_Info.Mantra, false);
            }
        }

        public virtual bool BlocksMovement
        {
            get { return true; }
        }

        public virtual bool CheckNextSpellTime
        {
            get { return !IsWand() || !m_SpellStrike; }
        }

        public bool Cast()
        {
            m_StartCastTime = Core.TickCount;

            if (!m_Caster.CheckAlive())
            {
                return false;
            }
            else if (IsWand() && m_Caster.Spell != null && m_Caster.Spell.IsCasting)
            {
                m_Caster.SendLocalizedMessage(502643); // You can not cast a spell while frozen.
            }
            else if (m_Caster.Spell != null && m_Caster.Spell.IsCasting)
            {
                m_Caster.SendLocalizedMessage(502642); // You are already casting a spell.
            }
            else if (!(IsWand()) && (m_Caster.Paralyzed || m_Caster.Frozen))
            {
                m_Caster.SendLocalizedMessage(502643); // You can not cast a spell while frozen.
            }
            else if (CheckNextSpellTime && Core.TickCount < m_Caster.NextSpellTime)
            {
                m_Caster.SendLocalizedMessage(502644); // You have not yet recovered from casting a spell.
            }
            else if (m_Caster is PlayerMobile && ((PlayerMobile) m_Caster).PeacedUntil > DateTime.Now)
            {
                m_Caster.SendLocalizedMessage(1072060); // You cannot cast a spell while calmed.
            }
            else if (m_Caster.Mana >= GetMana() || m_SpellStrike)
            {
                if (m_Caster.Spell == null && m_Caster.CheckSpellCast(this) && CheckCast() &&
                    m_Caster.Region.OnBeginSpellCast(m_Caster, this))
                {
                    State = SpellState.Casting;
                    m_Caster.Spell = this;

                    if (!(IsWand()) && RevealOnCast)
                        m_Caster.RevealingAction();

                    SayMantra();

                    TimeSpan castDelay = m_SpellStrike ? TimeSpan.Zero : GetCastDelay();

                    if (ShowHandMovement && (m_Caster.Body.IsHuman || m_Caster.Player && m_Caster.Body.IsMonster))
                    {
                        int count = (int) Math.Ceiling(castDelay.TotalSeconds / AnimateDelay.TotalSeconds);

                        if (count != 0)
                        {
                            m_AnimTimer = new AnimTimer(this, count);
                            m_AnimTimer.Start();
                        }

                        if (m_Info.LeftHandEffect > 0)
                            Caster.FixedParticles(0, 10, 5, m_Info.LeftHandEffect, EffectLayer.LeftHand);

                        if (m_Info.RightHandEffect > 0)
                            Caster.FixedParticles(0, 10, 5, m_Info.RightHandEffect, EffectLayer.RightHand);
                    }

                    if (ClearHandsOnCast)
                        m_Caster.ClearHands();

                    m_CastTimer = new CastTimer(this, castDelay);
                    //m_CastTimer.Start();

                    OnBeginCast();

                    if (castDelay > TimeSpan.Zero)
                    {
                        m_CastTimer.Start();
                    }
                    else
                    {
                        m_CastTimer.Tick();
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                m_Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 502625); // Insufficient mana
            }

            return false;
        }

        public abstract void OnCast();

        public virtual void OnBeginCast()
        {
        }

        public virtual void GetCastSkills(out double min, out double max)
        {
            min = max = 0; //Intended but not required for overriding.
        }

        public virtual bool CheckFizzle()
        {
            if (IsWand())
                return true;

            double minSkill, maxSkill;

            GetCastSkills(out minSkill, out maxSkill);

            if (DamageSkill != CastSkill)
                Caster.CheckSkill(DamageSkill, 0.0, Caster.Skills[DamageSkill].Cap);

            return Caster.CheckSkill(CastSkill, minSkill, maxSkill);
        }

        public abstract int GetMana();

        public virtual TimeSpan GetDisturbRecovery()
        {
          double delay = 1.0 - Math.Sqrt((Core.TickCount - m_StartCastTime) / 1000.0 / GetCastDelay().TotalSeconds);

          if (delay < 0.2)
            delay = 0.2;

          return TimeSpan.FromSeconds(delay);
        }

        public virtual TimeSpan GetCastRecovery()
        {
            return NextSpellDelay;
        }

        public abstract TimeSpan CastDelayBase { get; }

        public virtual double CastDelayFastScalar
        {
            get { return 1; }
        }

        public virtual double CastDelaySecondsPerTick
        {
            get { return 0.25; }
        }

        public virtual TimeSpan CastDelayMinimum
        {
            get { return TimeSpan.FromSeconds(0.25); }
        }

        public virtual TimeSpan GetCastDelay()
        {
            if (IsWand())
                return TimeSpan.Zero;

            TimeSpan baseDelay = CastDelayBase;

            TimeSpan fcDelay = TimeSpan.FromSeconds(-(CastDelayFastScalar * CastDelaySecondsPerTick));

            //int delay = CastDelayBase + circleDelay + fcDelay;
            TimeSpan delay = baseDelay + fcDelay;

            if (delay < CastDelayMinimum)
                delay = CastDelayMinimum;

            //return TimeSpan.FromSeconds( (double)delay / CastDelayPerSecond );
            return delay;
        }

        public virtual void FinishSequence()
        {
            State = SpellState.None;

            if (m_Caster.Spell == this)
                m_Caster.Spell = null;
        }

        public virtual int ComputeKarmaAward()
        {
            return 0;
        }

        public virtual bool CheckSequence()
        {
            if (m_SpellStrike)
                return true;

            int mana = GetMana();

            if (m_Caster.Deleted || !m_Caster.Alive || m_Caster.Spell != this || State != SpellState.Sequencing)
            {
                DoFizzle();
            }
            else if (Scroll != null && !(Scroll is Runebook) &&
                     (Scroll.Amount <= 0 || Scroll.Deleted || Scroll.RootParent != m_Caster ||
                      IsWand() && (((BaseWand) Scroll).Charges <= 0 || Scroll.Parent != m_Caster)))
            {
                DoFizzle();
            }
            else if (!ConsumeReagents())
            {
                m_Caster.LocalOverheadMessage(MessageType.Regular, 0x22,
                    502630); // More reagents are needed for this spell.
            }
            else if (m_Caster.Mana < mana)
            {
                m_Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 502625); // Insufficient mana for this spell.
            }
            else if (m_Caster is PlayerMobile && ((PlayerMobile) m_Caster).PeacedUntil > DateTime.Now)
            {
                m_Caster.SendLocalizedMessage(1072060); // You cannot cast a spell while calmed.
                DoFizzle();
            }
            else if (CheckFizzle())
            {
                m_Caster.Mana -= mana;

                if (Scroll is SpellScroll)
                    Scroll.Consume();
                else if (IsWand())
                {
                    ((BaseWand) Scroll).ConsumeCharge(m_Caster);
                    m_Caster.RevealingAction();
                }

                if (IsWand())
                {
                    bool m = Scroll.Movable;

                    Scroll.Movable = false;

                    if (ClearHandsOnCast)
                        m_Caster.ClearHands();

                    Scroll.Movable = m;
                }
                else
                {
                    if (ClearHandsOnCast)
                        m_Caster.ClearHands();
                }

                int karma = ComputeKarmaAward();

                if (karma != 0)
                    Misc.Titles.AwardKarma(Caster, karma, true);

                return true;
            }
            else
            {
                DoFizzle();
            }

            return false;
        }

        public bool CheckBSequence(Mobile target)
        {
            return CheckBSequence(target, false);
        }

        public bool CheckBSequence(Mobile target, bool allowDead)
        {
            if (!target.Alive && !allowDead)
            {
                m_Caster.SendLocalizedMessage(501857); // This spell won't work on that!
                return false;
            }
            else if (Caster.CanBeBeneficial(target, true, allowDead) && CheckSequence())
            {
                Caster.DoBeneficial(target);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckHSequence(Mobile target)
        {
            if (!target.Alive)
            {
                m_Caster.SendLocalizedMessage(501857); // This spell won't work on that!
                return false;
            }
            else if (Caster.CanBeHarmful(target) && CheckSequence())
            {
                Caster.DoHarmful(target);
                return true;
            }
            else
            {
                return false;
            }
        }

        private class AnimTimer : Timer
        {
            private Spell m_Spell;

            public AnimTimer(Spell spell, int count) : base(TimeSpan.Zero, AnimateDelay, count)
            {
                m_Spell = spell;

                Priority = TimerPriority.FiftyMS;
            }

            protected override void OnTick()
            {
                if (m_Spell.State != SpellState.Casting || m_Spell.m_Caster.Spell != m_Spell)
                {
                    Stop();
                    return;
                }

                if (!m_Spell.Caster.Mounted && m_Spell.m_Info.Action >= 0)
                {
                    if (m_Spell.Caster.Body.IsHuman)
                        m_Spell.Caster.Animate(m_Spell.m_Info.Action, 7, 1, true, false, 0);
                    else if (m_Spell.Caster.Player && m_Spell.Caster.Body.IsMonster)
                        m_Spell.Caster.Animate(12, 7, 1, true, false, 0);
                }

                if (!Running)
                    m_Spell.m_AnimTimer = null;
            }
        }

        private class CastTimer : Timer
        {
            private Spell m_Spell;

            public CastTimer(Spell spell, TimeSpan castDelay) : base(castDelay)
            {
                m_Spell = spell;

                Priority = TimerPriority.TwentyFiveMS;
            }

            protected override void OnTick()
            {
                if (m_Spell == null || m_Spell.m_Caster == null)
                {
                    return;
                }
                else if (m_Spell.State == SpellState.Casting && m_Spell.m_Caster.Spell == m_Spell)
                {
                    m_Spell.State = SpellState.Sequencing;
                    m_Spell.m_CastTimer = null;
                    m_Spell.m_Caster.OnSpellCast(m_Spell);
                    if (m_Spell.m_Caster.Region != null)
                        m_Spell.m_Caster.Region.OnSpellCast(m_Spell.m_Caster, m_Spell);
                    m_Spell.m_Caster.NextSpellTime = Core.TickCount + (int)m_Spell.GetCastRecovery().TotalMilliseconds; // Spell.NextSpellDelay;

                    Target originalTarget = m_Spell.m_Caster.Target;

                    m_Spell.OnCast();

                    if (m_Spell.m_Caster.Player && m_Spell.m_Caster.Target != originalTarget &&
                        m_Spell.Caster.Target != null)
                        m_Spell.m_Caster.Target.BeginTimeout(m_Spell.m_Caster, TimeSpan.FromSeconds(30.0));

                    m_Spell.m_CastTimer = null;
                }
            }

            public void Tick()
            {
                OnTick();
            }
        }
    }
}
