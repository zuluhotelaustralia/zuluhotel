using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Items
{
    [Flipable(0x02C5, 0x02C6)]
    public class LightCannonNorth : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063650;
            }
        }

        [Constructable]
        public LightCannonNorth()
            : base(0x02C5)
        {
            Weight = 1.0;
        }
        public LightCannonNorth(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x02DC, 0x02DD)]
    public class LightCannonSouth : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063650;
            }
        }

        [Constructable]
        public LightCannonSouth()
            : base(0x02DC)
        {
            Weight = 1.0;
        }
        public LightCannonSouth(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x041F, 0x0428)]
    public class GruesomeStandard : Item
    {

        [Constructable]
        public GruesomeStandard()
            : base(0x041F)
        {
            Weight = 1.0;
        }
        public GruesomeStandard(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0420, 0x0429)]
    public class GruesomeStandardShield : Item
    {

        [Constructable]
        public GruesomeStandardShield()
            : base(0x0420)
        {
            Weight = 1.0;
        }
        public GruesomeStandardShield(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }


    public class ThreeBottlesOfAle : Item
    {

        [Constructable]
        public ThreeBottlesOfAle()
            : base(0x09A1)
        {
            Weight = 1.0;
        }
        public ThreeBottlesOfAle(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class FourBottlesOfAle : Item
    {

        [Constructable]
        public FourBottlesOfAle()
            : base(0x09A2)
        {
            Weight = 1.0;
        }
        public FourBottlesOfAle(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class ThreeBottlesofLiquor : Item
    {

        [Constructable]
        public ThreeBottlesofLiquor()
            : base(0x099D)
        {
            Weight = 1.0;
        }
        public ThreeBottlesofLiquor(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class FourBottlesofLiquor : Item
    {

        [Constructable]
        public FourBottlesofLiquor()
            : base(0x099E)
        {
            Weight = 1.0;
        }
        public FourBottlesofLiquor(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class CampionFlowers : Item
    {

        [Constructable]
        public CampionFlowers()
            : base(0x0C83)
        {
            Weight = 1.0;
        }
        public CampionFlowers(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class Snowdrops : Item
    {

        [Constructable]
        public Snowdrops()
            : base(0x0C88)
        {
            Weight = 1.0;
        }
        public Snowdrops(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class RedPoppies : Item
    {

        [Constructable]
        public RedPoppies()
            : base(0x0C86)
        {
            Weight = 1.0;
        }
        public RedPoppies(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class OrfluerFlowers : Item
    {

        [Constructable]
        public OrfluerFlowers()
            : base(0x0CC1)
        {
            Weight = 1.0;
        }
        public OrfluerFlowers(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class SmallCactus : Item
    {

        [Constructable]
        public SmallCactus()
                : base(0x0D2C)
        {
            Weight = 1.0;
        }
        public SmallCactus(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class SmallBroadCactus : Item
    {

        [Constructable]
        public SmallBroadCactus()
            : base(0x0D2E)
        {
            Weight = 1.0;
        }
        public SmallBroadCactus(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class SingleCactus : Item
    {

        [Constructable]
        public SingleCactus()
            : base(0x0D26)
        {
            Weight = 1.0;
        }
        public SingleCactus(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class ClusterCactus : Item
    {

        [Constructable]
        public ClusterCactus()
            : base(0x0D27)
        {
            Weight = 1.0;
        }
        public ClusterCactus(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0C8B, 0x0C8C)]
    public class WhiteFlowers : Item
    {

        [Constructable]
        public WhiteFlowers()
            : base(0x0C8B)
        {
            Weight = 1.0;
        }
        public WhiteFlowers(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x10D2, 0x10D3)]
    public class SpiderWeb : Item
    {

        [Constructable]
        public SpiderWeb()
            : base(0x10D2)
        {
            Weight = 1.0;
        }
        public SpiderWeb(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x10D4, 0x10D5)]
    public class SmallSpiderWeb : Item
    {

        [Constructable]
        public SmallSpiderWeb()
            : base(0x10D4)
        {
            Weight = 1.0;
        }
        public SmallSpiderWeb(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DamagedBooks : Item
    {

        [Constructable]
        public DamagedBooks()
            : base(0x0C16)
        {
            Weight = 1.0;
        }
        public DamagedBooks(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class BlueBottle : Item
    {

        [Constructable]
        public BlueBottle()
            : base(0x0EFB)
        {
            Weight = 1.0;
        }
        public BlueBottle(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class YellowBottle : Item
    {

        [Constructable]
        public YellowBottle()
            : base(0x0EFE)
        {
            Weight = 1.0;
        }
        public YellowBottle(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class PinkBottle : Item
    {

        [Constructable]
        public PinkBottle()
            : base(0x0F00)
        {
            Weight = 1.0;
        }
        public PinkBottle(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class PurpleBottle : Item
    {

        [Constructable]
        public PurpleBottle()
            : base(0x0F02)
        {
            Weight = 1.0;
        }
        public PurpleBottle(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x10D4, 0x107A)]
    public class SmallStretchedHide : Item
    {

        [Constructable]
        public SmallStretchedHide()
            : base(0x10D4)
        {
            Weight = 1.0;
        }
        public SmallStretchedHide(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x10D5, 0x107B)]
    public class StretchedHide : Item
    {

        [Constructable]
        public StretchedHide()
            : base(0x10D5)
        {
            Weight = 1.0;
        }
        public StretchedHide(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x106B, 0x107C)]
    public class LargeStretchedHide : Item
    {

        [Constructable]
        public LargeStretchedHide()
            : base(0x106B)
        {
            Weight = 1.0;
        }
        public LargeStretchedHide(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x13C8, 0x13CF)]
    public class LeatherSleevesDisplay : Item
    {

        [Constructable]
        public LeatherSleevesDisplay()
            : base(0x13C8)
        {
            Weight = 1.0;
        }
        public LeatherSleevesDisplay(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x13C9, 0x13D0)]
    public class LeatherLegsDisplay : Item
    {

        [Constructable]
        public LeatherLegsDisplay()
            : base(0x13C9)
        {
            Weight = 1.0;
        }
        public LeatherLegsDisplay(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x13CA, 0x13D1)]
    public class LeatherChestDisplay : Item
    {

        [Constructable]
        public LeatherChestDisplay()
            : base(0x13CA)
        {
            Weight = 1.0;
        }
        public LeatherChestDisplay(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x13D7, 0x13DE)]
    public class StuddedSleevesDisplay : Item
    {

        [Constructable]
        public StuddedSleevesDisplay()
            : base(0x13D7)
        {
            Weight = 1.0;
        }
        public StuddedSleevesDisplay(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x13D8, 0x13DF)]
    public class StuddedLegsDisplay : Item
    {

        [Constructable]
        public StuddedLegsDisplay()
            : base(0x13D8)
        {
            Weight = 1.0;
        }
        public StuddedLegsDisplay(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x13D9, 0x13E0)]
    public class StuddedChestDisplay : Item
    {

        [Constructable]
        public StuddedChestDisplay()
            : base(0x13D9)
        {
            Weight = 1.0;
        }
        public StuddedChestDisplay(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x13BD, 0x13C2)]
    public class ChainChestDisplay : Item
    {

        [Constructable]
        public ChainChestDisplay()
            : base(0x13BD)
        {
            Weight = 1.0;
        }
        public ChainChestDisplay(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x13BC, 0x13C1)]
    public class ChainLegsDisplay : Item
    {

        [Constructable]
        public ChainLegsDisplay()
            : base(0x13BC)
        {
            Weight = 1.0;
        }
        public ChainLegsDisplay(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x13E5, 0x13E6)]
    public class RingLegsDisplay : Item
    {

        [Constructable]
        public RingLegsDisplay()
            : base(0x13E5)
        {
            Weight = 1.0;
        }
        public RingLegsDisplay(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x13E7, 0x13E8)]
    public class RingChestDisplay : Item
    {

        [Constructable]
        public RingChestDisplay()
            : base(0x13E7)
        {
            Weight = 1.0;
        }
        public RingChestDisplay(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x13E9, 0x13EA)]
    public class RingSleevesDisplay : Item
    {

        [Constructable]
        public RingSleevesDisplay()
            : base(0x13E9)
        {
            Weight = 1.0;
        }
        public RingSleevesDisplay(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x14F7, 0x14F9)]
    public class Anchor : Item
    {

        [Constructable]
        public Anchor()
            : base(0x14F7)
        {
            Weight = 1.0;
        }
        public Anchor(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class Beehive : Item
    {

        [Constructable]
        public Beehive()
            : base(0x091A)
        {
            Weight = 1.0;
        }
        public Beehive(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0991, 0x0992)]
    public class Tray : Item
    {

        [Constructable]
        public Tray()
            : base(0x0991)
        {
            Weight = 1.0;
        }
        public Tray(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class Kettle : Item
    {

        [Constructable]
        public Kettle()
            : base(0x09ED)
        {
            Weight = 1.0;
        }
        public Kettle(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class BloodyWater : Item
    {

        [Constructable]
        public BloodyWater()
            : base(0x0E23)
        {
            Weight = 1.0;
        }
        public BloodyWater(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class WaterTub : Item
    {

        [Constructable]
        public WaterTub()
            : base(0x0E7B)
        {
            Weight = 1.0;
        }
        public WaterTub(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class LightSaddle : Item
    {

        [Constructable]
        public LightSaddle()
            : base(0x0F37)
        {
            Weight = 1.0;
        }
        public LightSaddle(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class HeavySaddle : Item
    {

        [Constructable]
        public HeavySaddle()
            : base(0x0F38)
        {
            Weight = 1.0;
        }
        public HeavySaddle(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class ArrowBundle : Item
    {

        [Constructable]
        public ArrowBundle()
            : base(0x0F41)
        {
            Weight = 1.0;
        }
        public ArrowBundle(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class FirePit : Item
    {

        [Constructable]
        public FirePit()
            : base(0x0FAC)
        {
            Weight = 1.0;
        }
        public FirePit(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class CampFire : Item
    {

        [Constructable]
        public CampFire()
            : base(0x0DE3)
        {
            Weight = 1.0;
        }
        public CampFire(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class PlayingCardsWithDeck : Item
    {

        [Constructable]
        public PlayingCardsWithDeck()
            : base(0x0FA2)
        {
            Weight = 1.0;
        }
        public PlayingCardsWithDeck(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0FBD, 0x0FBE)]
    public class OpenBook : Item
    {

        [Constructable]
        public OpenBook()
            : base(0x0FBD)
        {
            Weight = 1.0;
        }
        public OpenBook(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class PurpleSashTable : Item
    {

        [Constructable]
        public PurpleSashTable()
            : base(0x118B)
        {
            Weight = 1.0;
        }
        public PurpleSashTable(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DarkPurpleSashTable : Item
    {

        [Constructable]
        public DarkPurpleSashTable()
            : base(0x118C)
        {
            Weight = 1.0;
        }
        public DarkPurpleSashTable(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class RedSashTable : Item
    {

        [Constructable]
        public RedSashTable()
            : base(0x118D)
        {
            Weight = 1.0;
        }
        public RedSashTable(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class OrangeSashTable : Item
    {

        [Constructable]
        public OrangeSashTable()
            : base(0x118E)
        {
            Weight = 1.0;
        }
        public OrangeSashTable(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x1871, 0x1872)]
    public class BeefCarcass : Item
    {

        [Constructable]
        public BeefCarcass()
            : base(0x1871)
        {
            Weight = 1.0;
        }
        public BeefCarcass(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x1873, 0x1874)]
    public class SheepCarcass : Item
    {

        [Constructable]
        public SheepCarcass()
            : base(0x1873)
        {
            Weight = 1.0;
        }
        public SheepCarcass(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class ShortBrazier : Item
    {

        [Constructable]
        public ShortBrazier()
            : base(0x19BB)
        {
            Weight = 1.0;
        }
        public ShortBrazier(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class MediumBrazier : Item
    {

        [Constructable]
        public MediumBrazier()
            : base(0x0E31)
        {
            Weight = 1.0;
        }
        public MediumBrazier(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class TallBrazier : Item
    {

        [Constructable]
        public TallBrazier()
            : base(0x19AA)
        {
            Weight = 1.0;
        }
        public TallBrazier(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x1BD8, 0x1BDB)]
    public class SmallBoardPile : Item
    {

        [Constructable]
        public SmallBoardPile()
            : base(0x1BD8)
        {
            Weight = 1.0;
        }
        public SmallBoardPile(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x1BD9, 0x1BDC)]
    public class LargeBoardPile : Item
    {

        [Constructable]
        public LargeBoardPile()
            : base(0x1BD9)
        {
            Weight = 1.0;
        }
        public LargeBoardPile(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x1BDE, 0x1BE1)]
    public class SmallLogPile : Item
    {

        [Constructable]
        public SmallLogPile()
            : base(0x1BDE)
        {
            Weight = 1.0;
        }
        public SmallLogPile(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x1BDF, 0x1BE2)]
    public class LargeLogPile : Item
    {

        [Constructable]
        public LargeLogPile()
            : base(0x1BDF)
        {
            Weight = 1.0;
        }
        public LargeLogPile(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x1A01, 0x1A02)]
    public class HangingSkeletonWithBoots : Item
    {

        [Constructable]
        public HangingSkeletonWithBoots()
            : base(0x1A01)
        {
            Weight = 1.0;
        }
        public HangingSkeletonWithBoots(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x1E60, 0x1E67)]
    public class BearTrophy : Item
    {

        [Constructable]
        public BearTrophy()
            : base(0x1E60)
        {
            Weight = 1.0;
        }
        public BearTrophy(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x1E61, 0x1E68)]
    public class MooseTrophy : Item
    {

        [Constructable]
        public MooseTrophy()
            : base(0x1E61)
        {
            Weight = 1.0;
        }
        public MooseTrophy(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x1E62, 0x1E69)]
    public class FishTrophy : Item
    {

        [Constructable]
        public FishTrophy()
            : base(0x1E62)
        {
            Weight = 1.0;
        }
        public FishTrophy(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x1E63, 0x1E6A)]
    public class OgreTrophy : Item
    {

        [Constructable]
        public OgreTrophy()
            : base(0x1E63)
        {
            Weight = 1.0;
        }
        public OgreTrophy(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x1E64, 0x1E6B)]
    public class OrcTrophy : Item
    {

        [Constructable]
        public OrcTrophy()
            : base(0x1E64)
        {
            Weight = 1.0;
        }
        public OrcTrophy(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x1E65, 0x1E6C)]
    public class PolarBearTrophy : Item
    {

        [Constructable]
        public PolarBearTrophy()
            : base(0x1E65)
        {
            Weight = 1.0;
        }
        public PolarBearTrophy(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x1E66, 0x1E6D)]
    public class TrollTrophy : Item
    {

        [Constructable]
        public TrollTrophy()
            : base(0x1E66)
        {
            Weight = 1.0;
        }
        public TrollTrophy(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayBarrelOne : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063651;
            }
        }



        // Custom Rares


        [Constructable]
        public DisplayBarrelOne()
            : base(0x473E)
        {
            Weight = 1.0;
        }
        public DisplayBarrelOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayBarrelTwo : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063651;
            }
        }

        [Constructable]
        public DisplayBarrelTwo()
            : base(0x473F)
        {
            Weight = 1.0;
        }
        public DisplayBarrelTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayBarrelThree : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063651;
            }
        }

        [Constructable]
        public DisplayBarrelThree()
            : base(0x4740)
        {
            Weight = 1.0;
        }
        public DisplayBarrelThree(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayBarrelFour : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063651;
            }
        }

        [Constructable]
        public DisplayBarrelFour()
            : base(0x4741)
        {
            Weight = 1.0;
        }
        public DisplayBarrelFour(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayBarrelFive : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063651;
            }
        }

        [Constructable]
        public DisplayBarrelFive()
            : base(0x4742)
        {
            Weight = 1.0;
        }
        public DisplayBarrelFive(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayBarrelSix : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063651;
            }
        }

        [Constructable]
        public DisplayBarrelSix()
            : base(0x4743)
        {
            Weight = 1.0;
        }
        public DisplayBarrelSix(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayBarrelSeven : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063651;
            }
        }

        [Constructable]
        public DisplayBarrelSeven()
            : base(0x4744)
        {
            Weight = 1.0;
        }
        public DisplayBarrelSeven(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayBarrelEight : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063651;
            }
        }

        [Constructable]
        public DisplayBarrelEight()
            : base(0x4745)
        {
            Weight = 1.0;
        }
        public DisplayBarrelEight(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayBarrelNine : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063651;
            }
        }

        [Constructable]
        public DisplayBarrelNine()
            : base(0x4746)
        {
            Weight = 1.0;
        }
        public DisplayBarrelNine(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayBarrelTen : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063651;
            }
        }

        [Constructable]
        public DisplayBarrelTen()
            : base(0x4747)
        {
            Weight = 1.0;
        }
        public DisplayBarrelTen(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayBarrelEleven : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063651;
            }
        }

        [Constructable]
        public DisplayBarrelEleven()
            : base(0x4748)
        {
            Weight = 1.0;
        }
        public DisplayBarrelEleven(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayBarrelTwelve : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063651;
            }
        }

        [Constructable]
        public DisplayBarrelTwelve()
            : base(0x4749)
        {
            Weight = 1.0;
        }
        public DisplayBarrelTwelve(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayBarrelThirteen : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063651;
            }
        }

        [Constructable]
        public DisplayBarrelThirteen()
            : base(0x474A)
        {
            Weight = 1.0;
        }
        public DisplayBarrelThirteen(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayBarrelFourteen : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063651;
            }
        }

        [Constructable]
        public DisplayBarrelFourteen()
            : base(0x474B)
        {
            Weight = 1.0;
        }
        public DisplayBarrelFourteen(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayBarrelFifteen : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063651;
            }
        }

        [Constructable]
        public DisplayBarrelFifteen()
            : base(0x474C)
        {
            Weight = 1.0;
        }
        public DisplayBarrelFifteen(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x476C, 0x476D)]
    public class AlchemyDisplayCase : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063652;
            }
        }
        [Constructable]
        public AlchemyDisplayCase()
            : base(0x476C)
        {
            Weight = 1.0;
        }
        public AlchemyDisplayCase(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class FlowerPotOne : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063654;
            }
        }
        [Constructable]
        public FlowerPotOne()
            : base(0x4778)
        {
            Weight = 1.0;
        }
        public FlowerPotOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class FlowerPotTwo : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063654;
            }
        }
        [Constructable]
        public FlowerPotTwo()
            : base(0x4779)
        {
            Weight = 1.0;
        }
        public FlowerPotTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }


    public class FlowerPotThree : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063654;
            }
        }
        [Constructable]
        public FlowerPotThree()
            : base(0x477A)
        {
            Weight = 1.0;
        }
        public FlowerPotThree(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class FlowerPotFour : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063654;
            }
        }
        [Constructable]
        public FlowerPotFour()
            : base(0x477B)
        {
            Weight = 1.0;
        }
        public FlowerPotFour(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class FlowerPotFive : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063654;
            }
        }
        [Constructable]
        public FlowerPotFive()
            : base(0x477C)
        {
            Weight = 1.0;
        }
        public FlowerPotFive(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class FlowerPotSix : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063654;
            }
        }
        [Constructable]
        public FlowerPotSix()
            : base(0x4790)
        {
            Weight = 1.0;
        }
        public FlowerPotSix(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class FlowerPotSeven : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063654;
            }
        }
        [Constructable]
        public FlowerPotSeven()
            : base(0x4791)
        {
            Weight = 1.0;
        }
        public FlowerPotSeven(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x4770, 0x4771)]
    public class FlowerPlanterOne : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063653;
            }
        }
        [Constructable]
        public FlowerPlanterOne()
            : base(0x4770)
        {
            Weight = 1.0;
        }
        public FlowerPlanterOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x4773, 0x4774)]
    public class FlowerPlanterTwo : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063653;
            }
        }
        [Constructable]
        public FlowerPlanterTwo()
            : base(0x4773)
        {
            Weight = 1.0;
        }
        public FlowerPlanterTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x4775, 0x4776)]
    public class FlowerPlanterThree : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063653;
            }
        }
        [Constructable]
        public FlowerPlanterThree()
            : base(0x4775)
        {
            Weight = 1.0;
        }
        public FlowerPlanterThree(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x4777, 0x4778)]
    public class FlowerPlanterFour : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063653;
            }
        }
        [Constructable]
        public FlowerPlanterFour()
            : base(0x4777)
        {
            Weight = 1.0;
        }
        public FlowerPlanterFour(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x48DC, 0x47DF)]
    public class GlassDisplayOne : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063655;
            }
        }
        [Constructable]
        public GlassDisplayOne()
            : base(0x48DC)
        {
            Weight = 1.0;
        }
        public GlassDisplayOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x47E0, 0x48E3)]
    public class GlassDisplayTwo : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063655;
            }
        }
        [Constructable]
        public GlassDisplayTwo()
            : base(0x47E0)
        {
            Weight = 1.0;
        }
        public GlassDisplayTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x47E5, 0x47E6)]
    public class GlassDisplayThree : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063655;
            }
        }
        [Constructable]
        public GlassDisplayThree()
            : base(0x47E5)
        {
            Weight = 1.0;
        }
        public GlassDisplayThree(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x48DD, 0x48DE)]
    public class GlassDisplayFour : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063655;
            }
        }
        [Constructable]
        public GlassDisplayFour()
            : base(0x48DD)
        {
            Weight = 1.0;
        }
        public GlassDisplayFour(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x48DF, 0x48E0)]
    public class GlassDisplayFive : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063655;
            }
        }
        [Constructable]
        public GlassDisplayFive()
            : base(0x48DF)
        {
            Weight = 1.0;
        }
        public GlassDisplayFive(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DisplayMirror : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063656;
            }
        }
        [Constructable]
        public DisplayMirror()
            : base(0x4911)
        {
            Weight = 1.0;
        }
        public DisplayMirror(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class BrokenBarrel : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063657;
            }
        }
        [Constructable]
        public BrokenBarrel()
            : base(0x4985)
        {
            Weight = 1.0;
        }
        public BrokenBarrel(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class BrokenCrate : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063658;
            }
        }
        [Constructable]
        public BrokenCrate()
            : base(0x4988)
        {
            Weight = 1.0;
        }
        public BrokenCrate(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x4A9F, 0x4AA2)]
    public class BrokenFaceStatue : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063659;
            }
        }
        [Constructable]
        public BrokenFaceStatue()
            : base(0x4A9F)
        {
            Weight = 1.0;
        }
        public BrokenFaceStatue(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x4AA0, 0x4AA1)]
    public class BrokenHeadStatue : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063659;
            }
        }
        [Constructable]
        public BrokenHeadStatue()
            : base(0x4AA0)
        {
            Weight = 1.0;
        }
        public BrokenHeadStatue(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x47D7, 0x47D8)]
    public class DisplayCart : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063660;
            }
        }
        [Constructable]
        public DisplayCart()
            : base(0x47D7)
        {
            Weight = 1.0;
        }
        public DisplayCart(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x47D9, 0x47DA)]
    public class DisplayFlowerCart : Item
    {
        public override int LabelNumber
        {
            get
            {
                return 1063660;
            }
        }
        [Constructable]
        public DisplayFlowerCart()
            : base(0x47D9)
        {
            Weight = 1.0;
        }
        public DisplayFlowerCart(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    // End custom rares
    // Begin carpet rares

    public class RedCarpetCenterOne : Item
    {

        [Constructable]
        public RedCarpetCenterOne()
            : base(0x0AC6)
        {
            Weight = 1.0;
        }
        public RedCarpetCenterOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class RedCarpetCenterTwo : Item
    {

        [Constructable]
        public RedCarpetCenterTwo()
            : base(0x0AC7)
        {
            Weight = 1.0;
        }
        public RedCarpetCenterTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class RedCarpetCenterThree : Item
    {

        [Constructable]
        public RedCarpetCenterThree()
            : base(0x0AC8)
        {
            Weight = 1.0;
        }
        public RedCarpetCenterThree(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0AC9, 0x0ACB)]
    public class RedCarpetCornerOne : Item
    {

        [Constructable]
        public RedCarpetCornerOne()
            : base(0x0AC9)
        {
            Weight = 1.0;
        }
        public RedCarpetCornerOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0ACA, 0x0ACC)]
    public class RedCarpetCornerTwo : Item
    {


        [Constructable]
        public RedCarpetCornerTwo()
            : base(0x0ACA)
        {
            Weight = 1.0;
        }
        public RedCarpetCornerTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0ACD, 0x0ACE)]
    public class RedCarpetSideOne : Item
    {
        [Constructable]
        public RedCarpetSideOne()
            : base(0x0ACD)
        {
            Weight = 1.0;
        }
        public RedCarpetSideOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0ACF, 0x0AD0)]
    public class RedCarpetSideTwo : Item
    {
        [Constructable]
        public RedCarpetSideTwo()
            : base(0x0ACF)
        {
            Weight = 1.0;
        }
        public RedCarpetSideTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class BlueCarpetCenterOne : Item
    {

        [Constructable]
        public BlueCarpetCenterOne()
            : base(0x0ABE)
        {
            Weight = 1.0;
        }
        public BlueCarpetCenterOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class BlueCarpetCenterTwo : Item
    {

        [Constructable]
        public BlueCarpetCenterTwo()
            : base(0x0ABF)
        {
            Weight = 1.0;
        }
        public BlueCarpetCenterTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class BlueCarpetCenterThree : Item
    {

        [Constructable]
        public BlueCarpetCenterThree()
            : base(0x0AC0)
        {
            Weight = 1.0;
        }
        public BlueCarpetCenterThree(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0AC2, 0x0AC4)]
    public class BlueCarpetCornerOne : Item
    {

        [Constructable]
        public BlueCarpetCornerOne()
            : base(0x0AC2)
        {
            Weight = 1.0;
        }
        public BlueCarpetCornerOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0AC3, 0x0AC5)]
    public class BlueCarpetCornerTwo : Item
    {


        [Constructable]
        public BlueCarpetCornerTwo()
            : base(0x0AC3)
        {
            Weight = 1.0;
        }
        public BlueCarpetCornerTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0AF6, 0x0AF7)]
    public class BlueCarpetSideOne : Item
    {
        [Constructable]
        public BlueCarpetSideOne()
            : base(0x0AF6)
        {
            Weight = 1.0;
        }
        public BlueCarpetSideOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0AF8, 0x0AF9)]
    public class BlueCarpetSideTwo : Item
    {
        [Constructable]
        public BlueCarpetSideTwo()
            : base(0x0AF8)
        {
            Weight = 1.0;
        }
        public BlueCarpetSideTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class BlueAndGoldCarpet : Item
    {

        [Constructable]
        public BlueAndGoldCarpet()
            : base(0x0AD1)
        {
            Weight = 1.0;
        }
        public BlueAndGoldCarpet(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0AD2, 0x0AD4)]
    public class BlueAndGoldCarpetCornerOne : Item
    {

        [Constructable]
        public BlueAndGoldCarpetCornerOne()
            : base(0x0AD2)
        {
            Weight = 1.0;
        }
        public BlueAndGoldCarpetCornerOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0AD3, 0x0AD5)]
    public class BlueAndGoldCarpetCornerTwo : Item
    {


        [Constructable]
        public BlueAndGoldCarpetCornerTwo()
            : base(0x0AD3)
        {
            Weight = 1.0;
        }
        public BlueAndGoldCarpetCornerTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0AD6, 0x0AD7)]
    public class BlueAndGoldCarpetSideOne : Item
    {
        [Constructable]
        public BlueAndGoldCarpetSideOne()
            : base(0x0AD6)
        {
            Weight = 1.0;
        }
        public BlueAndGoldCarpetSideOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0AD8, 0x0AD9)]
    public class BlueAndGoldCarpetSideTwo : Item
    {
        [Constructable]
        public BlueAndGoldCarpetSideTwo()
            : base(0x0AD8)
        {
            Weight = 1.0;
        }
        public BlueAndGoldCarpetSideTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class YellowAndGoldCarpet : Item
    {

        [Constructable]
        public YellowAndGoldCarpet()
            : base(0x0ADA)
        {
            Weight = 1.0;
        }
        public YellowAndGoldCarpet(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0ADB, 0x0ADD)]
    public class YellowAndGoldCarpetCornerOne : Item
    {

        [Constructable]
        public YellowAndGoldCarpetCornerOne()
            : base(0x0ADB)
        {
            Weight = 1.0;
        }
        public YellowAndGoldCarpetCornerOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0ADC, 0x0ADE)]
    public class YellowAndGoldCarpetCornerTwo : Item
    {


        [Constructable]
        public YellowAndGoldCarpetCornerTwo()
            : base(0x0ADC)
        {
            Weight = 1.0;
        }
        public YellowAndGoldCarpetCornerTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0ADF, 0x0AE0)]
    public class YellowAndGoldCarpetSideOne : Item
    {
        [Constructable]
        public YellowAndGoldCarpetSideOne()
            : base(0x0ADF)
        {
            Weight = 1.0;
        }
        public YellowAndGoldCarpetSideOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0AE1, 0x0AE2)]
    public class YellowAndGoldCarpetSideTwo : Item
    {
        [Constructable]
        public YellowAndGoldCarpetSideTwo()
            : base(0x0AE1)
        {
            Weight = 1.0;
        }
        public YellowAndGoldCarpetSideTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class BlueAndRedFancyCarpet : Item
    {

        [Constructable]
        public BlueAndRedFancyCarpet()
            : base(0x0AED)
        {
            Weight = 1.0;
        }
        public BlueAndRedFancyCarpet(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0AEE, 0x0AF0)]
    public class BlueAndRedFancyCarpetCornerOne : Item
    {

        [Constructable]
        public BlueAndRedFancyCarpetCornerOne()
            : base(0x0AEE)
        {
            Weight = 1.0;
        }
        public BlueAndRedFancyCarpetCornerOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0AEF, 0x0AF1)]
    public class BlueAndRedFancyCarpetCornerTwo : Item
    {


        [Constructable]
        public BlueAndRedFancyCarpetCornerTwo()
            : base(0x0AEF)
        {
            Weight = 1.0;
        }
        public BlueAndRedFancyCarpetCornerTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0AF2, 0x0AF3)]
    public class BlueAndRedFancyCarpetSideOne : Item
    {
        [Constructable]
        public BlueAndRedFancyCarpetSideOne()
            : base(0x0AF2)
        {
            Weight = 1.0;
        }
        public BlueAndRedFancyCarpetSideOne(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Flipable(0x0AF4, 0x0AF5)]
    public class BlueAndRedFancyCarpetSideTwo : Item
    {
        [Constructable]
        public BlueAndRedFancyCarpetSideTwo()
            : base(0x0AF4)
        {
            Weight = 1.0;
        }
        public BlueAndRedFancyCarpetSideTwo(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
