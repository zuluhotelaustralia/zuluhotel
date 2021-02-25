using System;
using System.Linq;
using Server.Mobiles;
using Server.Spells;
using Server.Utilities;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Items
{
    public abstract class BaseTotem : BasePotion
    {
        public override uint PotionStrength { get; set; } = 1;

        public BaseTotem(int itemId) : base(itemId,  PotionEffect.Totem)
        {
        }

        public BaseTotem(Serial serial) : base(serial)
        {
        }
        
        private static bool Summon<T>(Mobile caster) where T : BaseCreature
        {
            var map = caster.Map;

            if (map == null)
                return false;

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

                caster.FireHook(h => h.OnSummonFamiliar(caster, creature));
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

    
    public class Totem : BaseTotem
    {
        public override string DefaultName { get; } = "a Totem";

        public override int Hue { get; set; } = 746;
        
        [Constructible]
        public Totem() : base(0x20D9)
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
        public TotemElixir() : base(0xE28)
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