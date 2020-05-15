using System;
using Server.Items;

namespace Server.Spells
{
    public class Reagent
    {
        private static Type[] m_Types = new Type[27]
            {
        typeof( BlackPearl ),
        typeof( Bloodmoss ),
        typeof( Garlic ),
        typeof( Ginseng ),
        typeof( MandrakeRoot ),
        typeof( Nightshade ),
        typeof( SulfurousAsh ),
        typeof( SpidersSilk ),
        typeof( BatWing ),
        typeof( DaemonBlood ),
        typeof( NoxCrystal ),
        typeof( PigIron ),
        typeof( Blackmoor ),
        typeof( Bloodspawn ),
        typeof( Brimstone ),
        typeof( DaemonBone ),
        typeof( DragonsBlood ),
        typeof( EyeOfNewt ),
        typeof( Obsidian ),
        typeof( Pumice ),
        typeof( VialOfBlood ),
        typeof( VolcanicAsh ),
        typeof( WyrmsHeart ),
        typeof( ExecutionersCap ),
        typeof( Bone ),
        typeof( DeadWood ),
        typeof( FertileDirt )
            };

        public Type[] Types
        {
            get { return m_Types; }
        }

        public static Type BlackPearl
        {
            get { return m_Types[0]; }
            set { m_Types[0] = value; }
        }

        public static Type Bloodmoss
        {
            get { return m_Types[1]; }
            set { m_Types[1] = value; }
        }

        public static Type Garlic
        {
            get { return m_Types[2]; }
            set { m_Types[2] = value; }
        }

        public static Type Ginseng
        {
            get { return m_Types[3]; }
            set { m_Types[3] = value; }
        }

        public static Type MandrakeRoot
        {
            get { return m_Types[4]; }
            set { m_Types[4] = value; }
        }

        public static Type Nightshade
        {
            get { return m_Types[5]; }
            set { m_Types[5] = value; }
        }

        public static Type SulfurousAsh
        {
            get { return m_Types[6]; }
            set { m_Types[6] = value; }
        }

        public static Type SpidersSilk
        {
            get { return m_Types[7]; }
            set { m_Types[7] = value; }
        }

        public static Type BatWing
        {
            get { return m_Types[8]; }
            set { m_Types[8] = value; }
        }

        public static Type DaemonBlood
        {
            get { return m_Types[9]; }
            set { m_Types[9] = value; }
        }

        public static Type NoxCrystal
        {
            get { return m_Types[10]; }
            set { m_Types[10] = value; }
        }

        public static Type PigIron
        {
            get { return m_Types[11]; }
            set { m_Types[11] = value; }
        }

        public static Type Blackmoor
        {
            get { return m_Types[12]; }
            set { m_Types[12] = value; }
        }

        public static Type Bloodspawn
        {
            get { return m_Types[13]; }
            set { m_Types[13] = value; }
        }

        public static Type Brimstone
        {
            get { return m_Types[14]; }
            set { m_Types[14] = value; }
        }

        public static Type DaemonBone
        {
            get { return m_Types[15]; }
            set { m_Types[15] = value; }
        }

        public static Type DragonsBlood
        {
            get { return m_Types[16]; }
            set { m_Types[16] = value; }
        }

        public static Type EyeOfNewt
        {
            get { return m_Types[17]; }
            set { m_Types[17] = value; }
        }

        public static Type Obsidian
        {
            get { return m_Types[18]; }
            set { m_Types[18] = value; }
        }

        public static Type Pumice
        {
            get { return m_Types[19]; }
            set { m_Types[19] = value; }
        }

        public static Type VialOfBlood
        {
            get { return m_Types[20]; }
            set { m_Types[21] = value; }
        }

        public static Type VolcanicAsh
        {
            get { return m_Types[21]; }
            set { m_Types[22] = value; }
        }

        public static Type WyrmsHeart
        {
            get { return m_Types[22]; }
            set { m_Types[23] = value; }
        }

        public static Type ExecutionersCap
        {
            get { return m_Types[23]; }
            set { m_Types[24] = value; }
        }

        public static Type Bone
        {
            get { return m_Types[24]; }
            set { m_Types[25] = value; }
        }

        public static Type DeadWood
        {
            get { return m_Types[25]; }
            set { m_Types[26] = value; }
        }

        public static Type FertileDirt
        {
            get { return m_Types[26]; }
            set { m_Types[27] = value; }
        }
    }
}

