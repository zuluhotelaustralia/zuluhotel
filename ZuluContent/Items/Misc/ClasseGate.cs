using System.Collections;
using Scripts.Zulu.Engines.Classes;
using Server.Mobiles;

namespace Server.Items
{
    public class ClasseGate : Item
    {
        public override bool HandlesOnMovement => true;

        [CommandProperty(AccessLevel.GameMaster)]
        public ZuluClassType ClasseType { get; set; }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public int ClasseLevel { get; set; }

        private bool PickClasse(PlayerMobile playerMobile)
        {
            playerMobile.FixedParticles(0x373A, 10, 10, 5007, EffectLayer.Waist);
                
            ZuluClass.SetClass(playerMobile, ClasseType, ClasseLevel);

            var stat = 60 + ClasseLevel * 15;
            playerMobile.Str = stat;
            playerMobile.Dex = stat;
            playerMobile.Int = stat;

            return true;
        }

        public override bool OnMoveOver(Mobile mobile)
        {
            if (mobile is PlayerMobile playerMobile)
            {
                return PickClasse(playerMobile);
            }

            return true;
        }
        
        [Constructible]
        public ClasseGate(ZuluClassType classe, int level) : base(0x0F6C)
        {
            if (level is > ZuluClass.MaxLevel or < 0)
                level = 0;
            
            Movable = false;
            ClasseType = classe;
            ClasseLevel = level;
            Name = $"Level {level} {classe.FriendlyName()}";
        }
        
        [Constructible]
        public ClasseGate(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version

            writer.Write((int) ClasseType);
            writer.Write(ClasseLevel);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            ClasseType = (ZuluClassType) reader.ReadInt();
            ClasseLevel = reader.ReadInt();
        }
    }
}