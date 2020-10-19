using System;
using System.Collections.Generic;
using System.Linq;
using Server.Engines.Magic;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Spells.Second;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells
{
    public abstract class Spell : ISpell
    {
        private static readonly TimeSpan NextSpellDelay = TimeSpan.FromSeconds(0.75);

        private static readonly TimeSpan AnimateDelay = TimeSpan.FromSeconds(1.5);
        //In reality, it's ANY delayed Damage spell Post-AoS that can't stack, but, only
        //Expo & Magic Arrow have enough delay and a short enough cast time to bring up
        //the possibility of stacking 'em.  Note that a MA & an Explosion will stack, but
        //of course, two MA's won't.

        private static readonly Dictionary<Type, DelayedDamageContextWrapper> ContextTable =
            new Dictionary<Type, DelayedDamageContextWrapper>();

        private AnimTimer m_AnimTimer;

        private CastTimer m_CastTimer;
        private bool m_SpellStrike;
        private long m_StartCastTime;

        public Spell(Mobile caster, Item scroll, bool spellStrike = false)
        {
            Caster = caster;
            Scroll = scroll;
            m_SpellStrike = false;
        }

        public SpellCircle Circle => Info.Circle;

        public SpellState State { get; set; }

        public Mobile Caster { get; private set; }

        public SpellInfo Info => SpellRegistry.SpellInfos[GetType()];

        public string Name => Info.Name;

        public string Mantra => Info.Mantra;

        public Type[] Reagents => Info.Reagents;

        public Item Scroll { get; private set; }

        public virtual SkillName CastSkill { get; } = SkillName.Magery;

        public virtual SkillName DamageSkill { get; } = SkillName.EvalInt;

        public virtual bool RevealOnCast { get; } = true;

        public virtual bool ClearHandsOnCast { get; } = true;

        public virtual bool ShowHandMovement { get; } = true;

        public virtual bool DelayedDamage { get; } = false;

        public virtual bool DelayedDamageStacking { get; } = true;

        public virtual bool BlocksMovement { get; } = true;

        public virtual bool CheckNextSpellTime => !IsWand() || !m_SpellStrike;

        public abstract TimeSpan CastDelayBase { get; }

        public virtual double CastDelayFastScalar { get; } = 1;

        public virtual double CastDelaySecondsPerTick { get; } = 0.25;

        public virtual TimeSpan CastDelayMinimum { get; } = TimeSpan.FromSeconds(0.25);

        public virtual bool IsCasting => State == SpellState.Casting;

        public virtual void OnCasterHurt()
        {
            //Confirm: Monsters and pets cannot be disturbed.
            if (!Caster.Player)
                return;

            if (IsCasting)
            {
                var o = ProtectionSpell.Registry[Caster];
                var disturb = true;

                if (o != null && o is double d)
                    if (d > Utility.RandomDouble() * 100.0)
                        disturb = false;

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
                Caster.SendLocalizedMessage(500111); // You are frozen and can not move.
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

        public virtual bool OnCastInTown(Region r)
        {
            return Info.AllowTown;
        }

        public virtual void FinishSequence()
        {
            State = SpellState.None;

            if (Caster.Spell == this)
                Caster.Spell = null;
        }

        public static T Create<T>(Mobile caster, Item scroll = null, bool spellStrike = false) where T : Spell
        {
            var spell = SpellRegistry.Create<T>(caster, scroll);

            spell.Caster = caster;
            spell.Scroll = scroll;
            spell.m_SpellStrike = spellStrike;

            return spell;
        }
        
        public static Spell Create(SpellEntry entry, Mobile caster, Item scroll = null, bool spellStrike = false)
        {
            var spell = SpellRegistry.Create(entry, caster, scroll);

            spell.Caster = caster;
            spell.Scroll = scroll;
            spell.m_SpellStrike = spellStrike;

            return spell;
        }


        public void StartDelayedDamageContext(Mobile m, Timer t)
        {
            if (DelayedDamageStacking)
                return; //Sanity

            if (!ContextTable.TryGetValue(GetType(), out var contexts))
            {
                contexts = new DelayedDamageContextWrapper();
                ContextTable.Add(GetType(), contexts);
            }

            contexts.Add(m, t);
        }

        public void RemoveDelayedDamageContext(Mobile m)
        {
            if (!ContextTable.TryGetValue(GetType(), out var contexts))
                return;

            contexts.Remove(m);
        }

        public void HarmfulSpell(Mobile m)
        {
            if (m is BaseCreature creature)
                creature.OnHarmfulSpell(Caster);
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

        public virtual bool ConsumeReagents()
        {
            if (Scroll != null || !Caster.Player)
                return true;

            var pack = Caster.Backpack;

            if (pack == null)
                return false;

            if (pack.ConsumeTotal(Info.Reagents, Info.Amounts) == -1)
                return true;

            return false;
        }

        public virtual double GetResistSkill(Mobile m)
        {
            return m.Skills[SkillName.MagicResist].Value;
        }


        public virtual double GetResistPercentForCircle(Mobile target, SpellCircle circle)
        {
            var firstPercent = target.Skills[SkillName.MagicResist].Value / 5.0;
            var secondPercent = target.Skills[SkillName.MagicResist].Value -
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
            var n = GetResistPercent(target);

            n /= 100.0;

            if (n <= 0.0)
                return false;

            if (n >= 1.0)
                return true;

            var maxSkill = (1 + (int) Circle) * 10;
            maxSkill += (1 + (int) Circle / 6) * 25;

            if (target.Skills[SkillName.MagicResist].Value < maxSkill)
                target.CheckSkill(SkillName.MagicResist, 0.0, target.Skills[SkillName.MagicResist].Cap);

            return n >= Utility.RandomDouble();
        }

        public virtual double GetDamageScalar(Mobile target)
        {
            var scalar = 1.0;

            var casterEI = Caster.Skills[DamageSkill].Value;
            var targetRS = target.Skills[SkillName.MagicResist].Value;

            //m_Caster.CheckSkill( DamageSkill, 0.0, 120.0 );

            if (casterEI > targetRS)
                scalar = 1.0 + (casterEI - targetRS) / 500.0;
            else
                scalar = 1.0 + (casterEI - targetRS) / 200.0;

            // magery damage bonus, -25% at 0 skill, +0% at 100 skill, +5% at 120 skill
            scalar += (Caster.Skills[CastSkill].Value - 100.0) / 400.0;

            if (!target.Player && !target.Body.IsHuman /*&& !Core.AOS*/)
                scalar *= 2.0; // Double magery damage to monsters/animals if not AOS

            target.Region.SpellDamageScalar(Caster, target, ref scalar);

            return scalar;
        }

        public virtual double GetSlayerDamageScalar(Mobile defender)
        {
            var atkBook = Spellbook.FindEquippedSpellbook(Caster);

            var scalar = 1.0;
            if (atkBook != null)
            {
                var atkSlayer = SlayerGroup.GetEntryByName(atkBook.OldSlayer);
                var atkSlayer2 = SlayerGroup.GetEntryByName(atkBook.OldSlayer2);

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
                var defSlayer = SlayerGroup.GetEntryByName(defISlayer.OldSlayer);
                var defSlayer2 = SlayerGroup.GetEntryByName(defISlayer.OldSlayer2);

                if (defSlayer != null && defSlayer.Group.OppositionSuperSlays(Caster) ||
                    defSlayer2 != null && defSlayer2.Group.OppositionSuperSlays(Caster))
                    scalar = 2.0;
            }

            return scalar;
        }

        public virtual void DoFizzle()
        {
            Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, 502632); // The spell fizzles.

            if (Caster.Player)
            {
                Caster.FixedEffect(0x3735, 6, 30);

                Caster.PlaySound(0x5C);
            }
        }

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
                case SpellState.Casting when !firstCircle && this is MagerySpell &&
                                             ((MagerySpell) this).Circle == SpellCircle.First:
                    return;
                case SpellState.Casting:
                {
                    State = SpellState.None;
                    Caster.Spell = null;

                    OnDisturb(type, true);

                    m_CastTimer?.Stop();

                    m_AnimTimer?.Stop();

                    Caster.NextSpellTime = Core.TickCount + (int) GetDisturbRecovery().TotalMilliseconds;
                    break;
                }
                case SpellState.Sequencing when !firstCircle && this is MagerySpell &&
                                                ((MagerySpell) this).Circle == SpellCircle.First:
                    return;
                case SpellState.Sequencing:
                    State = SpellState.None;
                    Caster.Spell = null;

                    OnDisturb(type, false);

                    Target.Cancel(Caster);
                    break;
            }
        }

        public virtual void DoHurtFizzle()
        {
            Caster.FixedEffect(0x3735, 6, 30);
            Caster.PlaySound(0x5C);
        }

        public virtual void OnDisturb(DisturbType type, bool message)
        {
            if (message)
                Caster.SendLocalizedMessage(500641); // Your concentration is disturbed, thus ruining thy spell.
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

            if (!string.IsNullOrEmpty(Info.Mantra) &&
                (Caster is BaseCreature creature && creature.SaySpellMantra || Caster.Player))
                Caster.PublicOverheadMessage(MessageType.Spell, Caster.SpeechHue, true, Info.Mantra, false);
        }

        public bool Cast()
        {
            m_StartCastTime = Core.TickCount;

            if (!Caster.CheckAlive()) return false;

            if (IsWand() && Caster.Spell != null && Caster.Spell.IsCasting)
            {
                Caster.SendLocalizedMessage(502643); // You can not cast a spell while frozen.
            }
            else if (Caster.Spell != null && Caster.Spell.IsCasting)
            {
                Caster.SendLocalizedMessage(502642); // You are already casting a spell.
            }
            else if (!IsWand() && (Caster.Paralyzed || Caster.Frozen))
            {
                Caster.SendLocalizedMessage(502643); // You can not cast a spell while frozen.
            }
            else if (CheckNextSpellTime && Core.TickCount < Caster.NextSpellTime)
            {
                Caster.SendLocalizedMessage(502644); // You have not yet recovered from casting a spell.
            }
            else if (Caster is PlayerMobile && ((PlayerMobile) Caster).PeacedUntil > DateTime.Now)
            {
                Caster.SendLocalizedMessage(1072060); // You cannot cast a spell while calmed.
            }
            else if (Caster.Mana >= GetMana() || m_SpellStrike)
            {
                if (Caster.Spell == null && Caster.CheckSpellCast(this) && CheckCast() &&
                    Caster.Region.OnBeginSpellCast(Caster, this))
                {
                    State = SpellState.Casting;
                    Caster.Spell = this;

                    if (!IsWand() && RevealOnCast)
                        Caster.RevealingAction();

                    SayMantra();

                    var castDelay = m_SpellStrike ? TimeSpan.Zero : GetCastDelay();

                    if (ShowHandMovement && (Caster.Body.IsHuman || Caster.Player && Caster.Body.IsMonster))
                    {
                        var count = (int) Math.Ceiling(castDelay.TotalSeconds / AnimateDelay.TotalSeconds);

                        if (count != 0)
                        {
                            m_AnimTimer = new AnimTimer(this, count);
                            m_AnimTimer.Start();
                        }

                        if (Info.LeftHandEffect > 0)
                            Caster.FixedParticles(0, 10, 5, Info.LeftHandEffect, EffectLayer.LeftHand);

                        if (Info.RightHandEffect > 0)
                            Caster.FixedParticles(0, 10, 5, Info.RightHandEffect, EffectLayer.RightHand);
                    }

                    if (ClearHandsOnCast)
                        Caster.ClearHands();

                    m_CastTimer = new CastTimer(this, castDelay);
                    //m_CastTimer.Start();

                    OnBeginCast();

                    if (castDelay > TimeSpan.Zero)
                        m_CastTimer.Start();
                    else
                        m_CastTimer.Tick();

                    return true;
                }

                return false;
            }
            else
            {
                Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 502625, $"{GetMana()}"); // Insufficient mana
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
            var delay = 1.0 - Math.Sqrt((Core.TickCount - m_StartCastTime) / 1000.0 / GetCastDelay().TotalSeconds);

            if (delay < 0.2)
                delay = 0.2;

            return TimeSpan.FromSeconds(delay);
        }

        public virtual TimeSpan GetCastRecovery()
        {
            return NextSpellDelay;
        }

        public virtual TimeSpan GetCastDelay()
        {
            if (IsWand())
                return TimeSpan.Zero;

            var baseDelay = CastDelayBase.TotalMilliseconds;

            var fcDelay = -(CastDelayFastScalar * CastDelaySecondsPerTick);

            //int delay = CastDelayBase + circleDelay + fcDelay;
            var delay = baseDelay + fcDelay;

            Caster.FireHook(h => h.OnGetCastDelay(Caster, this, ref delay));

            return delay < CastDelayMinimum.TotalMilliseconds
                ? CastDelayMinimum
                : TimeSpan.FromSeconds(delay);

            //return TimeSpan.FromSeconds( (double)delay / CastDelayPerSecond );
        }

        public virtual int ComputeKarmaAward()
        {
            return 0;
        }

        public virtual bool CheckSequence()
        {
            if (m_SpellStrike)
                return true;

            var mana = GetMana();

            if (Caster.Deleted || !Caster.Alive || Caster.Spell != this || State != SpellState.Sequencing)
            {
                DoFizzle();
            }
            else if (Scroll != null && !(Scroll is Runebook) &&
                     (Scroll.Amount <= 0 || Scroll.Deleted || Scroll.RootParent != Caster ||
                      IsWand() && (((BaseWand) Scroll).Charges <= 0 || Scroll.Parent != Caster)))
            {
                DoFizzle();
            }
            else if (!ConsumeReagents())
            {
                Caster.LocalOverheadMessage(MessageType.Regular, 0x22,
                    502630); // More reagents are needed for this spell.
            }
            else if (Caster.Mana < mana)
            {
                Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 502625); // Insufficient mana for this spell.
            }
            else if (Caster is PlayerMobile && ((PlayerMobile) Caster).PeacedUntil > DateTime.Now)
            {
                Caster.SendLocalizedMessage(1072060); // You cannot cast a spell while calmed.
                DoFizzle();
            }
            else if (CheckFizzle())
            {
                Caster.Mana -= mana;

                if (Scroll is SpellScroll)
                {
                    Scroll.Consume();
                }
                else if (IsWand())
                {
                    ((BaseWand) Scroll).ConsumeCharge(Caster);
                    Caster.RevealingAction();
                }

                if (IsWand())
                {
                    var m = Scroll.Movable;

                    Scroll.Movable = false;

                    if (ClearHandsOnCast)
                        Caster.ClearHands();

                    Scroll.Movable = m;
                }
                else
                {
                    if (ClearHandsOnCast)
                        Caster.ClearHands();
                }

                var karma = ComputeKarmaAward();

                if (karma != 0)
                    Titles.AwardKarma(Caster, karma, true);

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
                Caster.SendLocalizedMessage(501857); // This spell won't work on that!
                return false;
            }

            if (Caster.CanBeBeneficial(target, true, allowDead) && CheckSequence())
            {
                Caster.DoBeneficial(target);
                return true;
            }

            return false;
        }

        public bool CheckHSequence(Mobile target)
        {
            if (!target.Alive)
            {
                Caster.SendLocalizedMessage(501857); // This spell won't work on that!
                return false;
            }

            if (Caster.CanBeHarmful(target) && CheckSequence())
            {
                Caster.DoHarmful(target);
                return true;
            }

            return false;
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

        private class AnimTimer : Timer
        {
            private readonly Spell m_Spell;

            public AnimTimer(Spell spell, int count) : base(TimeSpan.Zero, AnimateDelay, count)
            {
                m_Spell = spell;

                Priority = TimerPriority.FiftyMS;
            }

            protected override void OnTick()
            {
                if (m_Spell.State != SpellState.Casting || m_Spell.Caster.Spell != m_Spell)
                {
                    Stop();
                    return;
                }

                if (!m_Spell.Caster.Mounted && m_Spell.Info.Action >= 0)
                {
                    if (m_Spell.Caster.Body.IsHuman)
                        m_Spell.Caster.Animate(m_Spell.Info.Action, 7, 1, true, false, 0);
                    else if (m_Spell.Caster.Player && m_Spell.Caster.Body.IsMonster)
                        m_Spell.Caster.Animate(12, 7, 1, true, false, 0);
                }

                if (!Running)
                    m_Spell.m_AnimTimer = null;
            }
        }

        private class CastTimer : Timer
        {
            private readonly Spell m_Spell;

            public CastTimer(Spell spell, TimeSpan castDelay) : base(castDelay)
            {
                m_Spell = spell;

                Priority = TimerPriority.TwentyFiveMS;
            }

            protected override void OnTick()
            {
                if (m_Spell?.Caster == null
                    || m_Spell.State != SpellState.Casting
                    || m_Spell.Caster.Spell != m_Spell
                )
                    return;

                m_Spell.State = SpellState.Sequencing;
                m_Spell.m_CastTimer = null;
                m_Spell.Caster.OnSpellCast(m_Spell);

                m_Spell.Caster.Region?.OnSpellCast(m_Spell.Caster, m_Spell);
                m_Spell.Caster.NextSpellTime =
                    Core.TickCount + (int) m_Spell.GetCastRecovery().TotalMilliseconds; // Spell.NextSpellDelay;

                var originalTarget = m_Spell.Caster.Target;

                m_Spell.OnCast();

                if (m_Spell.Caster.Player && m_Spell.Caster.Target != originalTarget)
                    m_Spell.Caster.Target?.BeginTimeout(m_Spell.Caster, TimeSpan.FromSeconds(30.0));

                m_Spell.m_CastTimer = null;
            }

            public void Tick()
            {
                OnTick();
            }
        }
    }
}