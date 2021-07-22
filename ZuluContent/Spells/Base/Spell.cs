using System;
using Scripts.Zulu.Engines.Classes;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells
{
    public abstract class Spell : ISpell
    {
        private static readonly TimeSpan NextSpellDelay = TimeSpan.FromSeconds(0.75);
        private static readonly TimeSpan AnimateDelay = TimeSpan.FromSeconds(1.5);
        private AnimTimer m_AnimTimer;
        private long m_StartCastTime;
        public Mobile Caster { get; }
        public SpellCircle Circle { get; }
        public SpellState State { get; protected set; }
        public SpellInfo Info { get; }
        public Item SpellItem { get; }
        public virtual SkillName CastSkill { get; } = SkillName.Magery;
        public virtual SkillName DamageSkill { get; } = SkillName.EvalInt;
        public virtual bool CheckNextSpellTime => !IsWand();
        public virtual int Mana => Circle.Mana;
        public virtual bool IsCasting => State == SpellState.Casting;
        public virtual bool IsTargeting => State == SpellState.Sequencing && Caster.Target is AsyncSpellTarget;

        public Spell(Mobile caster, Item spellItem = null)
        {
            Caster = caster;
            SpellItem = spellItem;
            Info = SpellRegistry.SpellInfos[GetType()];
            Circle = Info.Circle;
        }

        public virtual void OnCasterHurt()
        {
            //Confirm: Monsters and pets cannot be disturbed.
            if (!Caster.Player)
                return;

            if (IsCasting)
            {
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
            if (IsCasting && Info.BlocksMovement)
            {
                Caster.SendLocalizedMessage(500111); // You are frozen and can not move.
                return false;
            }

            if (Caster.Target is AsyncSpellTarget target && target.Spell == this)
                Disturb(DisturbType.Movement);

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

        public virtual bool OnCasterEquipping(Item item)
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
            if (SpellItem != null || !Caster.Player)
                return true;

            var pack = Caster.Backpack;

            if (pack == null)
                return false;

            if (pack.ConsumeTotal(Info.Reagents, Info.Amounts) == -1)
                return true;

            return false;
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

        public virtual bool CheckDisturb(DisturbType type, bool firstCircle, bool resistable)
        {
            if (resistable && IsWand())
                return false;

            if (type == DisturbType.Movement && !Caster.Player)
                return false;

            return true;
        }

        public void Disturb(DisturbType type, bool firstCircle = true, bool resistable = false)
        {
            if (!CheckDisturb(type, firstCircle, resistable))
                return;

            switch (State)
            {
                case SpellState.Casting when !firstCircle && this is MagerySpell && Circle == SpellCircle.First:
                {
                    return;
                }
                case SpellState.Casting:
                {
                    State = SpellState.None;
                    Caster.Spell = null;

                    OnDisturb(type, true);

                    m_AnimTimer?.Stop();

                    Caster.NextSpellTime = Core.TickCount + (int) GetDisturbRecovery().TotalMilliseconds;
                    break;
                }
                case SpellState.Sequencing when !firstCircle && this is MagerySpell && Circle == SpellCircle.First:
                {
                    return;
                }
                case SpellState.Sequencing:
                {
                    State = SpellState.None;
                    Caster.Spell = null;

                    OnDisturb(type, type == DisturbType.Movement);

                    Target.Cancel(Caster);
                    break;
                }
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

        public virtual bool CanCast()
        {
            if (!Caster.CheckAlive())
                return false;

            if (IsWand() && Caster.Spell != null && Caster.Spell.IsCasting)
            {
                Caster.SendLocalizedMessage(502643); // You can not cast a spell while frozen.
                return false;
            }

            if (Caster.Spell != null && Caster.Spell.IsCasting)
            {
                Caster.SendLocalizedMessage(502642); // You are already casting a spell.
                return false;
            }

            if (!IsWand() && (Caster.Paralyzed || Caster.Frozen))
            {
                Caster.SendLocalizedMessage(502643); // You can not cast a spell while frozen.
                return false;
            }

            if (CheckNextSpellTime && Core.TickCount < Caster.NextSpellTime)
            {
                Caster.SendLocalizedMessage(502644); // You have not yet recovered from casting a spell.
                return false;
            }

            if (!SpellHelper.CheckValidHands(Caster))
            {
                Caster.SendLocalizedMessage(502626); // Your hands must be free to cast spells or meditate.
                return false;
            }

            if (Caster is PlayerMobile player && player.PeacedUntil > DateTime.Now)
            {
                Caster.SendLocalizedMessage(1072060); // You cannot cast a spell while calmed.
                return false;
            }

            if (Mana > Caster.Mana)
            {
                Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 502625, $"{Mana}"); // Insufficient mana
                return false;
            }

            if (Caster.Spell != null || !Caster.CheckSpellCast(this) || !Caster.Region.OnBeginSpellCast(Caster, this))
            {
                return false;
            }

            return true;
        }

        public virtual bool IsWand() => SpellItem is BaseWand;

        public virtual void SayMantra()
        {
            if (IsWand())
                return;

            if (!string.IsNullOrEmpty(Info.Mantra) && (Caster is BaseCreature {SaySpellMantra: true} || Caster.Player))
                Caster.PublicOverheadMessage(MessageType.Spell, Caster.SpeechHue, true, Info.Mantra, false);
        }

        public async void Cast()
        {
            m_StartCastTime = Core.TickCount;

            if (!CanCast())
                return;

            State = SpellState.Casting;
            Caster.Spell = this;

            if (!IsWand() && Info.RevealOnCast)
                Caster.RevealingAction();

            Caster.DisruptiveAction();
            SayMantra();

            var castDelay = GetCastDelay();

            if (Info.ShowHandMovement && (Caster.Body.IsHuman || Caster.Player && Caster.Body.IsMonster))
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


            if (castDelay > TimeSpan.Zero)
                await Timer.Pause(castDelay);

            Caster.OnSpellCast(this);

            if (Caster.Spell != this || State != SpellState.Casting)
                return;

            State = SpellState.Sequencing;

            Caster.Region?.OnSpellCast(Caster, this);
            Caster.NextSpellTime = Core.TickCount + (int) GetCastRecovery().TotalMilliseconds; // Spell.NextSpellDelay;

            if (this is IAsyncSpell asyncSpell && CheckSequence())
                await asyncSpell.CastAsync();

            FinishSequence();
        }

        public virtual bool CheckFizzle()
        {
            return IsWand() || Caster.ShilCheckSkill(CastSkill, Circle.Difficulty, Circle.PointValue);
        }


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
            var delay = (double) Circle.Delay;

            if (Circle > 8)
            {
                var modifier = Caster.GetClassModifier(CastSkill);
                if (modifier > 1.0)
                    delay = Circle.Delay / Caster.GetClassModifier(CastSkill);
            }

            return TimeSpan.FromMilliseconds(delay);
        }

        public virtual bool CheckSequence()
        {
            var mana = Mana;

            if (Caster.Deleted || !Caster.Alive || Caster.Spell != this || State != SpellState.Sequencing)
            {
                DoFizzle();
                return false;
            }

            if (SpellItem is Runebook)
                return true;

            if (SpellItem != null && (SpellItem.Amount <= 0 || SpellItem.Deleted || SpellItem.RootParent != Caster))
            {
                DoFizzle();
                return false;
            }

            if (SpellItem is BaseWand {Charges: <= 0})
            {
                DoFizzle();
                return false;
            }

            if (Caster is PlayerMobile playerMobile && playerMobile.PeacedUntil > DateTime.Now)
            {
                Caster.SendLocalizedMessage(1072060); // You cannot cast a spell while calmed.
                DoFizzle();
                return false;
            }

            if (!ConsumeReagents())
            {
                // More reagents are needed for this spell.
                Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 502630);
                return false;
            }

            if (Caster.Mana < mana)
            {
                // Insufficient mana for this spell.
                Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 502625);
                return false;
            }

            if (!CheckFizzle())
            {
                DoFizzle();
                return false;
            }

            Caster.Mana -= mana;

            switch (SpellItem)
            {
                case SpellScroll:
                    SpellItem.Consume();
                    break;
                case BaseWand wand:
                {
                    wand.ConsumeCharge(Caster);

                    Caster.RevealingAction();
                    var m = wand.Movable;

                    wand.Movable = false;

                    if (Info.ClearHandsOnCast)
                        Caster.ClearHands();

                    wand.Movable = m;
                    break;
                }
                default:
                {
                    if (Info.ClearHandsOnCast)
                        Caster.ClearHands();
                    break;
                }
            }

            return true;
        }

        public bool CheckBeneficialSequence(Mobile target)
        {
            if (!target.Alive && !Info.AllowDead)
            {
                Caster.SendLocalizedMessage(501857); // This spell won't work on that!
                return false;
            }

            if (Caster.CanBeBeneficial(target, true, Info.AllowDead))
            {
                Caster.DoBeneficial(target);
                return true;
            }

            return false;
        }

        public bool CheckHarmfulSequence(Mobile target)
        {
            if (!target.Alive)
            {
                Caster.SendLocalizedMessage(501857); // This spell won't work on that!
                return false;
            }

            if (Caster.CanBeHarmful(target))
            {
                Caster.DoHarmful(target);
                return true;
            }

            return false;
        }

        private class AnimTimer : Timer
        {
            private readonly Spell m_Spell;

            public AnimTimer(Spell spell, int count) : base(TimeSpan.Zero, AnimateDelay, count)
            {
                m_Spell = spell;
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
    }
}