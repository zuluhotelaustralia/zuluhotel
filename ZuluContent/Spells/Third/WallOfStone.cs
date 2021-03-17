using System;
using System.Threading.Tasks;
using Server.Items;
using Server.Misc;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Third
{
    public class WallOfStoneSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public WallOfStoneSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var point = SpellHelper.GetSurfaceTop(response.Target);
            
            SpellHelper.Turn(Caster, point);

            var dx = Caster.Location.X - point.X;
            var dy = Caster.Location.Y - point.Y;
            var rx = (dx - dy) * 44;
            var ry = (dx + dy) * 44;

            var eastToWest = rx switch
            {
                >= 0 when ry >= 0 => false,
                >= 0 => true,
                _ => ry >= 0
            };

            Effects.PlaySound(new Point3D(point), Caster.Map, 0x1F6);
            
            var durationSeconds = Caster.Skills[SkillName.Magery].Value * 3.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref durationSeconds));
            var duration = TimeSpan.FromSeconds(durationSeconds);

            for (var i = -2; i <= 2; ++i)
            {
                var loc = new Point3D(eastToWest ? point.X + i : point.X, eastToWest ? point.Y : point.Y + i, point.Z);
                var canFit = SpellHelper.AdjustField(ref loc, Caster.Map, 22, true);

                Effects.SendLocationParticles( EffectItem.Create( loc, Caster.Map, EffectItem.DefaultDuration ), 0x376A, 9, 10, 5025 );

                if (!canFit)
                    continue;

                Item item = new MagicStoneWall(loc, Caster, duration, eastToWest);
                Effects.SendLocationParticles(item, 0x376A, 9, 10, 5025);
            }
        }

        [DispellableField]
        private sealed class MagicStoneWall : Item, IDispellable
        {
            private readonly Mobile m_Caster;
            private DateTime m_End;
            private Timer m_Timer;

            public bool Dispellable { get; set; } = true;

            public MagicStoneWall(
                Point3D loc, 
                Mobile caster, 
                TimeSpan duration,
                bool eastToWest
            ) 
                : base(eastToWest ? 0x58 : 0x57)
            {
                Visible = false;
                Movable = false;
                m_Caster = caster;

                MoveToWorld(loc, m_Caster.Map);
                
                if (caster.InLOS(this))
                    Visible = true;
                else
                    Delete();

                if (Deleted)
                    return;

                m_Timer = new InternalTimer(this, duration);
                m_Timer.Start();

                m_End = DateTime.Now + duration;
            }

            public MagicStoneWall(Serial serial) : base(serial) { }

            public override bool BlocksFit => true;

            public override void Serialize(IGenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write(1); // version

                writer.WriteDeltaTime(m_End);
            }

            public override void Deserialize(IGenericReader reader)
            {
                base.Deserialize(reader);

                var version = reader.ReadInt();

                switch (version)
                {
                    case 1:
                    {
                        m_End = reader.ReadDeltaTime();

                        m_Timer = new InternalTimer(this, m_End - DateTime.Now);
                        m_Timer.Start();

                        break;
                    }
                    case 0:
                    {
                        var duration = TimeSpan.FromSeconds(10.0);

                        m_Timer = new InternalTimer(this, duration);
                        m_Timer.Start();

                        m_End = DateTime.Now + duration;

                        break;
                    }
                }
            }

            public override void OnAfterDelete()
            {
                base.OnAfterDelete();
                m_Timer?.Stop();
            }

            private class InternalTimer : Timer
            {
                private readonly MagicStoneWall m_Item;

                public InternalTimer(MagicStoneWall item, TimeSpan duration) : base(duration)
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
    }
}