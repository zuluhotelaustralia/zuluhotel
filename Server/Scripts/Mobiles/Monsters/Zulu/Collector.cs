using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("the collector's corpse")]
    public class Collector : BaseCreature
    {
        //pick a player attacking us to fling a bone at
        public static Mobile FindRandomPlayer(BaseCreature creature)
        {
            List<DamageStore> rights = BaseCreature.GetLootingRights(creature.DamageEntries, creature.HitsMax);

            for (int i = rights.Count - 1; i >= 0; --i)
            {
                DamageStore ds = rights[i];

                if (!ds.m_HasRight)
                    rights.RemoveAt(i);
            }

            if (rights.Count > 0)
                return rights[Utility.Random(rights.Count)].m_Mobile;

            return null;
        }
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
        }

        [Constructable]
        public Collector() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("demon knight") + " the Bone Collector";
            Body = 318;
            BaseSoundID = 0x165;

            SetStr(500);
            SetDex(200);
            SetInt(1000);

            SetHits(30000);
            SetMana(5000);

            SetDamage(17, 21);

            SetSkill(SkillName.SpiritSpeak, 120.0, 160.0);
            SetSkill(SkillName.DetectHidden, 80.0);
            SetSkill(SkillName.EvalInt, 130.0);
            SetSkill(SkillName.Magery, 130.0);
            SetSkill(SkillName.Meditation, 130.0);
            SetSkill(SkillName.MagicResist, 250.0);
            SetSkill(SkillName.Tactics, 130.0);
            SetSkill(SkillName.Wrestling, 130.0);

            Fame = 28000;
            Karma = -28000;

            VirtualArmor = 64;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.SuperBoss, 2);
            AddLoot(LootPack.UltraRich);
            AddLoot(LootPack.HighScrolls, Utility.RandomMinMax(2, 20));
            AddLoot(LootPack.GreaterNecroScrolls, 8);
            if (Utility.RandomDouble() > 0.95)
            {
                PackItem(new NecromancerSpellbook());
            }
        }

        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public override int TreasureMapLevel { get { return 5; } }

        // like a mutex but shitty --sith
        private static bool m_InHere;

        public override void OnDamage(int amount, DamageType type, Mobile from, bool willKill)
        {
            if (from != null && from != this && !m_InHere)
            {
                m_InHere = true;
                from.Damage(Utility.RandomMinMax(5, 30), this, DamageType.Air, AttackType.Magical);

                MovingEffect(from, 0xECA, 10, 0, false, false, 0, 0);
                PlaySound(0x491);

                if (0.10 > Utility.RandomDouble())
                {
                    Timer.DelayCall(TimeSpan.FromSeconds(1.0), new TimerStateCallback(CreateBones_Callback), from);
                }

                m_InHere = false;
            }
        }

        public virtual void CreateBones_Callback(object state)
        {
            Mobile from = (Mobile)state;
            Map map = from.Map;

            if (map == null)
                return;

            int count = Utility.RandomMinMax(1, 3);

            for (int i = 0; i < count; ++i)
            {
                int x = from.X + Utility.RandomMinMax(-1, 1);
                int y = from.Y + Utility.RandomMinMax(-1, 1);
                int z = from.Z;

                if (!map.CanFit(x, y, z, 16, false, true))
                {
                    z = map.GetAverageZ(x, y);

                    if (z == from.Z || !map.CanFit(x, y, z, 16, false, true))
                        continue;
                }

                UnholyBone bone = new UnholyBone();

                bone.Hue = 0;
                bone.Name = "unholy bones";
                bone.ItemID = Utility.Random(0xECA, 9);

                bone.MoveToWorld(new Point3D(x, y, z), map);
            }
        }

        public Collector(Serial serial) : base(serial)
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
