using System;
using System.Linq;
using Server.Mobiles;
using Server.Spells;
using Server.Utilities;

namespace Server.Items
{
    public abstract class BaseTotem : BasePotion
    {
        
        protected BaseTotem(int itemId) : base(itemId,  PotionEffect.Totem)
        {
        }

        protected BaseTotem(Serial serial) : base(serial)
        {
        }
        
        private static bool Summon<T>(Mobile caster) where T : BaseCreature
        {
            var map = caster.Map;

            if (map == null)
                return false;

            // var scale = 1.0 + (caster.Skills[SkillName.Magery].Value - 100.0) / 200.0;

            // if (scaleStats)
            // {
            //     creature.RawStr = (int) (creature.RawStr * scale);
            //     creature.Hits = creature.HitsMax;
            //
            //     creature.RawDex = (int) (creature.RawDex * scale);
            //     creature.Stam = creature.StamMax;
            //
            //     creature.RawInt = (int) (creature.RawInt * scale);
            //     creature.Mana = creature.ManaMax;
            // }
            
            var p = new Point3D(caster);

            var creature = typeof(T).CreateInstance<Humuc>();
            
            if (SpellHelper.FindValidSpawnLocation(map, ref p, true))
            {
                creature.Name = caster.Name;
                creature.Owners.Add(caster);
                creature.SetControlMaster(caster);
                creature.MoveToWorld(p, caster.Map);
                creature.ControlOrder = OrderType.Follow;
                
                foreach (var s in caster.Skills) 
                    creature.Skills[s.SkillName].Base = s.Base;

                return true;
            }
            

            creature.Delete();
            caster.SendLocalizedMessage(501942); // That location is blocked.
            return false;
        }


        public override void Drink(Mobile from)
        {
            if (!(from is PlayerMobile player) || player.AllFollowers.Any(f => f is Humuc))
            {
                from.SendLocalizedMessage(1061605); // You already have a familiar.
                return;
            }

            var type = GetType() == typeof(Totem) ? typeof(TotemElixir) : typeof(Totem);

            if (player.Backpack.GetAmount(type) == 0)
            {
                from.SendLocalizedMessage(1055142); // You do not have the necessary ingredients. The contraptions rumbles angrily but does nothing.
                return;
            }
            
            if (Summon<Humuc>(from) && player.Backpack.ConsumeUpTo(type, 1) == 1)
            {
                from.FixedEffect(0x3727, 10, 15);
                from.PlaySound(0x1FD);
                PlayDrinkEffect(from);
                Consume();
            }
            
        }
    }

    
    public class Totem : BaseTotem
    {
        public override string DefaultName { get; } = "a Totem";

        public override int Hue { get; set; } = 746;
        
        [Constructible]
        public Totem() : base(0x20D8)
        {
        }

        public Totem(Serial serial) : base(serial)
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
    
    public class TotemElixir : BaseTotem
    {
        public override string DefaultName { get; } = "an Elixir";

        public override int Hue { get; set; } = 0;

        [Constructible]
        public TotemElixir() : base(0xE27)
        {
        }

        public TotemElixir(Serial serial) : base(serial)
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
}