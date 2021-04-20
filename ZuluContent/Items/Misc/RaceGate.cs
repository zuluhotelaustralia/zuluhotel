using System;
using Scripts.Zulu.Engines.Races;
using Scripts.Zulu.Utilities;
using Server.Mobiles;

namespace Server.Items
{
    public class RaceGate : Item
    {
        public override bool HandlesOnMovement => true;
        
        [CommandProperty(AccessLevel.GameMaster)]
        public ZuluRaceType RaceType { get; set; }

        private bool PickRace(PlayerMobile playerMobile)
        {
            if (playerMobile.ZuluRaceType != ZuluRaceType.None)
            {
                playerMobile.SendFailureMessage($"You are already a {playerMobile.ZuluRaceType.ToString()}.");
                return true;
            }
            
            playerMobile.FixedParticles(0x373A, 10, 10, 5007, EffectLayer.Waist);
                
            playerMobile.ZuluRaceType = RaceType;
            playerMobile.Hue = Hue;
                
            playerMobile.SendSuccessMessage($"You are now the race of {playerMobile.ZuluRaceType.ToString()}");

            playerMobile.MoveToWorld(new Point3D(1475, 1645, 20), Map.Felucca);
            
            return false;
        }

        public override bool OnMoveOver(Mobile mobile)
        {
            if (mobile is PlayerMobile playerMobile)
            {
                return PickRace(playerMobile);
            }

            return true;
        }
        
        [Constructible]
        public RaceGate(ZuluRaceType race) : base(0x0F6C)
        {
            Movable = false;
            RaceType = race;
            Hue = ZuluRace.Races[RaceType].Hue;
            Name = $"{RaceType.FriendlyName()} Race";
        }
        
        [Constructible]
        public RaceGate(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version

            writer.Write((int) RaceType);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            RaceType = (ZuluRaceType) reader.ReadInt();
        }
    }
}