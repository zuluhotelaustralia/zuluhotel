using System;
using Scripts.Zulu.Utilities;
using Server.Mobiles;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Items
{
    public class LifeCrystal : Item
    {
        public override string DefaultName => "Magical Crystal";

        [Constructible]
        public LifeCrystal() : base(0x1F1C)
        {
            Weight = 1;
        }
        
        [Constructible]
        public LifeCrystal(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;
            
            if (!from.InRange(GetWorldLocation(), 2))
            {
                from.SendLocalizedMessage(500446); // That is too far away.
                return;
            }
            
            from.SendSuccessMessage("You smash the magic crystal!");
            
            var mobiles = from.GetMobilesInRange(15);

            foreach (var mobile in mobiles)
            {
                if (!(mobile is PlayerMobile playerMobile))
                {
                    continue;
                }
                
                if (!mobile.CanBuff(mobile, true, BuffIcon.GiftOfLife))
                    continue;

                mobile.TryAddBuff(new DeathPardon()
                {
                    Value = true
                });
                
                playerMobile.FixedParticles(0x373A, 10, 10, 5007, EffectLayer.Waist);
                playerMobile.PlaySound(0x202);
                playerMobile.SendSuccessMessage("Death pardons your next transgression into the nether realm...");
            }

            mobiles.Free();
            
            Delete();
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}