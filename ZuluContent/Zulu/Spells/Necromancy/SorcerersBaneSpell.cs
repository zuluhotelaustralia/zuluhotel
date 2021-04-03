using System;
using System.Collections;
using System.Threading.Tasks;
using Server;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class SorcerersBaneSpell : NecromancerSpell, ITargetableAsyncSpell<Mobile>
    {
        private const int WaterfallEw = 13591;
        private const int WaterfallNs = 13561;

        private static readonly int[] Pool = {6054, 6047, 6053, 6051, 6039, 6045, 6056, 6049, 6055};
        
        public SorcerersBaneSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var target = response.Target;
            SpellHelper.Turn(Caster, target);

            target.PlaySound(0x11);

            var point = SpellHelper.GetSurfaceTop(target.Location);
            var map = target.Map;
            
            target.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot);
            target.PlaySound(0x208);
            
            var manaStolen = (double)target.Mana;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref manaStolen));
            manaStolen = SpellHelper.TryResistDamage(Caster, target, Circle, (int) manaStolen);
            
            if (manaStolen > target.Mana)
                manaStolen = target.Mana;

            // Note: Slight deviation from ZH3 here; Making this work more like Mana drain/vampire instead of using an explicit class check
            var amount = SpellHelper.TryResistDamage(Caster, target, SpellCircle.Seventh, (int)manaStolen);
            if (amount > target.Mana)
                amount = target.Mana;

            target.Mana -= amount;
            Caster.Mana += amount;
            
            var damage = SpellHelper.CalcSpellDamage(Caster, target, this);
            SpellHelper.Damage(damage / 2, target, Caster, this);
            
            await Timer.Pause(500);
            Caster.PlaySound(0x217);
            
            // Water column
            for (var z = 10; z >= 0; z--)
            {
                var loc = new Point3D(point.X, point.Y, point.Z + z * 10);

                EffectItem.Create(loc, map, TimeSpan.FromSeconds(5)).ItemID = WaterfallEw;
                EffectItem.Create(loc, map, TimeSpan.FromSeconds(5)).ItemID = WaterfallNs;
                await Timer.Pause(50);
            }

            // Water pool
            var poolNum = 0;
            for (var y = -1; y <= 1; y++)
            {
                for (var x = -1; x <= 1; x++)
                {
                    var loc = new Point3D(point.X + x, point.Y + y, point.Z);
                    EffectItem.Create(loc, map, TimeSpan.FromSeconds(10)).ItemID = Pool[poolNum];
                    poolNum++;
                }
            }
            
            Effects.PlaySound(point, map, 0x10);
            
            if(!target.Deleted && target.Alive)
                SpellHelper.Damage(damage / 2, target, Caster, this);
        }
    }
}