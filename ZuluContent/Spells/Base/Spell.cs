using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells
{
    public abstract class Spell : ISpell
    {
        private static readonly TimeSpan NextSpellDelay = TimeSpan.FromSeconds(0.75);

        private static readonly TimeSpan AnimateDelay = TimeSpan.FromSeconds(1.5);

        private AnimTimer m_AnimTimer;

        private bool m_SpellStrike;
        private long m_StartCastTime;

        public Spell(Mobile caster, Item spellItem = null, bool spellStrike = false)
        {
            Caster = caster;
            SpellItem = spellItem;
            m_SpellStrike = spellStrike;
        }

        public SpellCircle Circle => Info.Circle;

        public SpellState State { get; set; }

        public Mobile Caster { get; private set; }

        public SpellInfo Info => SpellRegistry.SpellInfos[GetType()];

        public string Name => Info.Name;

        public string Mantra => Info.Mantra;

        public Type[] Reagents => Info.Reagents;

        public Item SpellItem { get; private set; }

        public virtual SkillName CastSkill { get; } = SkillName.Magery;

        public virtual SkillName DamageSkill { get; } = SkillName.EvalInt;

        public virtual bool RevealOnCast => Info.RevealOnCast;

        public virtual bool ClearHandsOnCast => Info.ClearHandsOnCast;

        public virtual bool ShowHandMovement => Info.ShowHandMovement;

        public virtual bool DelayedDamage => Info.DelayedDamage;

        public virtual bool DelayedDamageStacking => Info.DelayedDamageStacking;

        public virtual bool BlocksMovement => Info.BlocksMovement;

        public virtual bool CheckNextSpellTime => !IsWand() || !m_SpellStrike;

        public abstract TimeSpan CastDelayBase { get; }

        public virtual double CastDelayFastScalar { get; } = 1;

        public virtual double CastDelaySecondsPerTick { get; } = 0.25;

        public virtual TimeSpan CastDelayMinimum { get; } = TimeSpan.FromSeconds(0.25);

        public virtual bool IsCasting => State == SpellState.Casting;
        
        public virtual bool IsTargeting => State == SpellState.Sequencing && Caster.Target is AsyncSpellTarget;


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
            if (IsCasting)
            {
                if (BlocksMovement)
                {
                    Caster.SendLocalizedMessage(500111); // You are frozen and can not move.
                    return false;
                }
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

        public static T Create<T>(Mobile caster, Item scroll = null, bool spellStrike = false) where T : Spell
        {
            var spell = SpellRegistry.Create<T>(caster, scroll);

            spell.Caster = caster;
            spell.SpellItem = scroll;
            spell.m_SpellStrike = spellStrike;

            return spell;
        }
        
        public static Spell Create(SpellEntry entry, Mobile caster, Item scroll = null, bool spellStrike = false)
        {
            var spell = SpellRegistry.Create(entry, caster, scroll);

            spell.Caster = caster;
            spell.SpellItem = scroll;
            spell.m_SpellStrike = spellStrike;

            return spell;
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

            return true;
        }

        public void Disturb(DisturbType type, bool firstCircle = true, bool resistable = false)
        {
            if (!CheckDisturb(type, firstCircle, resistable))
                return;

            switch (State)
            {
                case SpellState.Casting when !firstCircle && this is MagerySpell {Circle: SpellCircle.First}:
                    return;
                case SpellState.Casting:
                {
                    State = SpellState.None;
                    Caster.Spell = null;

                    OnDisturb(type, true);

                    m_AnimTimer?.Stop();

                    Caster.NextSpellTime = Core.TickCount + (int) GetDisturbRecovery().TotalMilliseconds;
                    break;
                }
                case SpellState.Sequencing when !firstCircle && this is MagerySpell {Circle: SpellCircle.First}:
                    return;
                case SpellState.Sequencing:
                    State = SpellState.None;
                    Caster.Spell = null;

                    OnDisturb(type, type == DisturbType.Movement);

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
            
            if (GetMana() > Caster.Mana && !m_SpellStrike)
            {
                Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 502625, $"{GetMana()}"); // Insufficient mana
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
            if (IsWand() || m_SpellStrike)
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

            if (!IsWand() && RevealOnCast)
                Caster.RevealingAction();

            Caster.DisruptiveAction();
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


            if(castDelay > TimeSpan.Zero)
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
            if (IsWand())
                return true;

            const double chanceOffset = 20.0;
            const double chanceLength = 100.0 / 7.0;
            
            var circle = (int) Circle;

            if (SpellItem != null)
                circle -= 2;

            var avg = chanceLength * circle;

            var minSkill = avg - chanceOffset;
            var maxSkill = avg + chanceOffset;
            
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
        
        public virtual bool CheckSequence()
        {
            if (m_SpellStrike)
                return true;

            var mana = GetMana();

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

                    if (ClearHandsOnCast)
                        Caster.ClearHands();

                    wand.Movable = m;
                    break;
                }
                default:
                {
                    if (ClearHandsOnCast)
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
    }
}