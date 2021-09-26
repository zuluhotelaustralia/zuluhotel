using System;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Mobiles;
using Server.Multis;
using Server.Spells;
using Server.Targeting;
using ZuluContent.Multis;

namespace Server.Items
{
    public class SpecialFishingNet : Item
    {
        public override int LabelNumber => 1041079;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool InUse { get; set; }

        private static readonly int[] Hues = new[]
        {
            0x09B,
            0x0CD,
            0x0D3,
            0x14D,
            0x1DD,
            0x1E9,
            0x1F4,
            0x373,
            0x451,
            0x47F,
            0x489,
            0x492,
            0x4B5,
            0x8AA
        };

        [Constructible]
        public SpecialFishingNet() : base(0x0DCA)
        {
            Weight = 1.0;

            if (0.01 > Utility.RandomDouble())
                Hue = Utility.RandomList(Hues);
            else
                Hue = 0x8A0;
        }

        [Constructible]
        public SpecialFishingNet(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1); // version

            writer.Write(InUse);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                {
                    InUse = reader.ReadBool();

                    if (InUse)
                        Delete();

                    break;
                }
            }

            Stackable = false;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (InUse)
            {
                from.SendLocalizedMessage(1010483); // Someone is already using that net!
            }
            else if (IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1010484); // Where do you wish to use the net?
                from.BeginTarget(-1, true, TargetFlags.None, OnTarget);
            }
            else
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
        }

        public void OnTarget(Mobile from, object obj)
        {
            if (Deleted || InUse)
                return;

            IPoint3D p3D = obj as IPoint3D;

            if (p3D == null)
                return;

            Map map = from.Map;

            if (map == null || map == Map.Internal)
                return;

            int x = p3D.X, y = p3D.Y, z = map.GetAverageZ(x, y); // OSI just takes the targeted Z

            if (!from.InRange(p3D, 6))
            {
                from.SendLocalizedMessage(500976); // You need to be closer to the water to fish!
            }
            else if (!from.InLOS(obj))
            {
                from.SendLocalizedMessage(500979); // You cannot see that location.
            }
            else if (FullValidation(map, x, y))
            {
                var p = new Point3D(x, y, z);

                if (GetType() == typeof(SpecialFishingNet))
                {
                    for (int i = 1; i < Amount; ++i) // these were stackable before, doh
                        from.AddToBackpack(new SpecialFishingNet());
                }

                InUse = true;
                Movable = false;
                MoveToWorld(p, map);

                SpellHelper.Turn(from, p);
                from.Animate(12, 5, 1, true, false, 0);

                Effects.SendLocationEffect(p, map, 0x352D, 16, 4);
                Effects.PlaySound(p, map, 0x364);
                
                var index = 0;

                Timer.StartTimer(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.25), 14, () => DoEffect(from, p, index++));

                from.SendLocalizedMessage(1010487); // You plunge the net into the sea...
            }
            else
            {
                from.SendLocalizedMessage(1010485); // You can only use this net in deep water!
            }
        }

        private void DoEffect(Mobile from, Point3D p, int index)
        {
            if (Deleted)
                return;

            if (index == 1)
            {
                Effects.SendLocationEffect(p, Map, 0x352D, 16, 4);
                Effects.PlaySound(p, Map, 0x364);
            }
            else if (index <= 7 || index == 14)
            {
                for (int i = 0; i < 3; ++i)
                {
                    int x, y;

                    switch (Utility.Random(8))
                    {
                        default:
                        case 0:
                            x = -1;
                            y = -1;
                            break;
                        case 1:
                            x = -1;
                            y = 0;
                            break;
                        case 2:
                            x = -1;
                            y = +1;
                            break;
                        case 3:
                            x = 0;
                            y = -1;
                            break;
                        case 4:
                            x = 0;
                            y = +1;
                            break;
                        case 5:
                            x = +1;
                            y = -1;
                            break;
                        case 6:
                            x = +1;
                            y = 0;
                            break;
                        case 7:
                            x = +1;
                            y = +1;
                            break;
                    }

                    Effects.SendLocationEffect(new Point3D(p.X + x, p.Y + y, p.Z), Map, 0x352D, 16, 4);
                }

                if (Utility.RandomBool())
                    Effects.PlaySound(p, Map, 0x364);

                if (index == 14)
                    FinishEffect(p, Map, from);
                else
                    Z -= 1;
            }
        }

        protected void Spawn(Point3D p, Map map, BaseCreature spawn)
        {
            if (map == null)
            {
                spawn.Delete();
                return;
            }

            int x = p.X, y = p.Y;

            for (int j = 0; j < 20; ++j)
            {
                int tx = p.X - 2 + Utility.Random(5);
                int ty = p.Y - 2 + Utility.Random(5);

                LandTile t = map.Tiles.GetLandTile(tx, ty);

                if (t.Z == p.Z && (t.ID >= 0xA8 && t.ID <= 0xAB || t.ID >= 0x136 && t.ID <= 0x137) &&
                    new Point3D(tx, ty, p.Z).GetMulti(map) == null
                )
                {
                    x = tx;
                    y = ty;
                    break;
                }
            }

            spawn.MoveToWorld(new Point3D(x, y, p.Z), map);
        }

        protected virtual void FinishEffect(Point3D p, Map map, Mobile from)
        {
            from.RevealingAction();

            from.ShilCheckSkill(SkillName.Fishing, points: 100);

            switch (Utility.Random(10))
            {
                case 0:
                case 1:
                case 2:
                {
                    BaseCreature spawn = null;

                    if (Utility.Random(11) > 9)
                        spawn = "Kraken";
                    else if (Utility.Random(11) > 9)
                        spawn = "Kraken";
                    else if (Utility.Random(11) > 9)
                        spawn = "SeaSerpent";

                    if (spawn != null)
                    {
                        from.SendFailureMessage("You caught something dangerous in your net!");
                        from.ShilCheckSkill(SkillName.Fishing, points: 100);
                        spawn.MoveToWorld(from.Location, from.Map);
                    }
                    else
                    {
                        if (Utility.Random(6) == 2)
                        {
                            var sosBottle = new MessageInABottle();
                            from.AddToBackpack(sosBottle);
                        }
                        else
                        {
                            var pouch = new Pouch();
                            pouch.AddItem(new Gold(Utility.Random(100) + 250));
                            from.AddToBackpack(pouch);
                            from.SendSuccessMessage("You find an old backpack!");
                        }
                    }
                    break;
                }
                case 3:
                {
                    var shell = GetRandomShell(from);
                    from.AddToBackpack(shell);
                    from.SendSuccessMessage("You find something valuable!");
                    break;
                }
                case 4:
                case 5:
                {
                    var magicFish = GetFish(from);
                    if (magicFish == null)
                    {
                        from.AddToBackpack(new Fish(Utility.RandomMinMax(6, 12)));
                        from.ShilCheckSkill(SkillName.Fishing, points: 100);
                    }
                    else
                    {
                        magicFish.Amount = Utility.RandomMinMax(1, 2);
                        from.AddToBackpack(magicFish);
                        from.ShilCheckSkill(SkillName.Fishing, points: 150);
                    }
                    from.SendSuccessMessage("You caught some fish.");
                    break;
                }
                case 6:
                {
                    if (Utility.Random(10) > 8)
                    {
                        from.AddToBackpack(new FishingTrophy1(from));
                        from.SendSuccessMessage("You catch a trophy!");
                    }
                    else if (Utility.Random(10) > 8)
                    {
                        from.AddToBackpack(new FishingTrophy2(from));
                        from.SendSuccessMessage("You catch a trophy!");
                    }
                    else
                    {
                        var bag = new Bag();
                        bag.AddItem(new Gold(Utility.Random(200) + 150));
                        from.AddToBackpack(bag);
                        from.SendSuccessMessage("You find an old bag!");
                    }

                    break;
                }
                case 7:
                case 8:
                {
                    var remains = GetRemains(from);
                    from.AddToBackpack(remains);
                    from.SendSuccessMessage("You find human skeleton remains!");
                    break;
                }
                case 9:
                {
                    var seaSerpent = "SeaSerpent";
                    Spawn(from.Location, from.Map, seaSerpent);
                    from.SendFailureMessage("You did not catch anything, except...");
                    break;
                }
            }

            Delete();
        }

        private static Item GetRemains(Mobile from)
        {
            var skill = from.Skills[SkillName.Fishing].Value;

            switch (Utility.Random(7))
            {
                case 0:
                {
                    if (skill > 100)
                    {
                        if (Utility.Random(3) == 2)
                            return new TerrorArms();

                        return new BoneArms();
                    }

                    return new BoneArms();
                }
                case 1:
                {
                    if (skill > 100)
                    {
                        if (Utility.Random(3) == 2)
                            return new TerrorGloves();

                        return new BoneGloves();
                    }

                    return new BoneGloves();
                }
                case 2:
                {
                    if (skill > 110)
                    {
                        if (Utility.Random(4) == 2)
                            return new TerrorLegs();

                        return new BoneLegs();
                    }

                    return new BoneLegs();
                }
                case 3:
                case 4:
                {
                    if (skill > 120)
                    {
                        if (Utility.Random(5) == 2)
                            return new TerrorChest();

                        return new BoneChest();
                    }

                    return new BoneChest();
                }
                case 5:
                {
                    if (skill > 110)
                    {
                        if (Utility.Random(5) == 2)
                            return new TerrorHelm();

                        return new BoneHelm();
                    }

                    return new BoneHelm();
                }
                case 6:
                {
                    if (skill > 120)
                    {
                        if (Utility.Random(5) == 2)
                            return new DartThrowerOfAmroth();

                        return new HeavyCrossbow();
                    }

                    return new HeavyCrossbow();
                }
                default:
                    return new BoneGloves();
            }
        }

        private static BaseMagicFish? GetFish(Mobile from)
        {
            var skill = from.Skills[SkillName.Fishing].Value;
            var chance = Utility.RandomMinMax(1, (int) skill);

            if (chance >= 100)
            {
                return Utility.Random(4) switch
                {
                    0 => new PrizedFish(),
                    1 => new TrulyRareFish(),
                    2 => new WondrousFish(),
                    3 => new PeculiarFish(),
                    _ => new PrizedFish()
                };
            }

            return null;
        }
        
        private static Shell GetRandomShell(Mobile from)
        {
            var chance = Utility.Random(5);

            switch (chance)
            {
                case 0:
                    return new Shell(4);
                case 1:
                    return new Shell(5);
                case 2:
                    return new Shell(6);
                case 3:
                case 4:
                {
                    if (from.Skills[SkillName.Fishing].Value > 100.0)
                    {
                        if (Utility.Random(3) == 2)
                            return new Shell(7);
                        
                        return new Shell(8);
                    }
                    
                    return new Shell(6);
                }
                default:
                    return new Shell(4);
            }
        }

        private static bool FullValidation(Map map, int x, int y)
        {
            var valid = ValidateDeepWater(map, x, y);
            for (int j = 1, offset = 5; valid && j <= 5; ++j, offset += 5)
            {
                if (!ValidateDeepWater(map, x + offset, y + offset))
                    valid = false;
                else if (!ValidateDeepWater(map, x + offset, y - offset))
                    valid = false;
                else if (!ValidateDeepWater(map, x - offset, y + offset))
                    valid = false;
                else if (!ValidateDeepWater(map, x - offset, y - offset))
                    valid = false;
            }

            return valid;
        }

        private static int[] m_WaterTiles = new[]
        {
            0x00A8, 0x00AB,
            0x0136, 0x0137
        };

        private static bool ValidateDeepWater(Map map, int x, int y)
        {
            var tileID = map.Tiles.GetLandTile(x, y).ID;
            var water = false;

            for (int i = 0; !water && i < m_WaterTiles.Length; i += 2)
                water = tileID >= m_WaterTiles[i] && tileID <= m_WaterTiles[i + 1];

            return water;
        }
    }
}