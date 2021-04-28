using System;
using System.Linq;
using Server.Mobiles;

namespace Server
{
    public class OppositionGroup
    {
        public CreatureType Type { get; init; }

        public CreatureType[] Friendlies { get; init; }
        public CreatureType[] Enemies { get; init; }

        public OppositionGroup(CreatureType[] friendlies, CreatureType[] enemies)
        {
            Friendlies = friendlies;
            Enemies = enemies;
        }

        public bool IsEnemy(BaseCreature from, BaseCreature target)
        {
            return from.CreatureType == Type && Enemies.Contains(target.CreatureType);
        }
        
        public bool IsFriendly(BaseCreature from, BaseCreature target)
        {
            return from.CreatureType == Type && Friendlies.Contains(target.CreatureType);
        }

        // public static OppositionGroup TerathansAndOphidians { get; } = new(
        //     new[]
        //     {
        //         "TerathanAvenger",
        //         "TerathanDrone",
        //         "TerathanMatriarch",
        //         "TerathanWarrior"
        //     },
        //     new[]
        //     {
        //         "OphidianShaman",
        //         "OphidianAvenger",
        //         "OphidianWarrior"
        //     }
        // );
        //
        // public static OppositionGroup SavagesAndOrcs { get; } = new(
        //     new[]
        //     {
        //         "OrcCaptain",
        //         "OrcishLord"
        //     },
        //     new string[]
        //     {
        //     }
        // );
        //
        // public static OppositionGroup FeyAndUndead { get; } = new(
        //     new[]
        //     {
        //         "Wisp"
        //     },
        //     new[]
        //     {
        //         "LicheLord",
        //         "Shade",
        //         "Spectre",
        //         "Wraith",
        //         "BoneKnight",
        //         "Mummy",
        //         "Skeleton",
        //         "Zombie",
        //         "Liche"
        //     }
        // );
    }
}