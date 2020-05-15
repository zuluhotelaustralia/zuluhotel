using System;
using Server.Items;
using Server.Spells;

namespace Server.Engines.Craft
{
    public class DefInscription : CraftSystem
    {
        public override SkillName MainSkill
        {
            get { return SkillName.Inscribe; }
        }

        public override int GumpTitleNumber
        {
            get { return 1044009; } // <CENTER>INSCRIPTION MENU</CENTER>
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefInscription();

                return m_CraftSystem;
            }
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.0; // 0%
        }

        private DefInscription()
            : base(1, 1, 1.25)// base( 1, 1, 3.0 )
        {
        }

        public override int CanCraft(Mobile from, BaseTool tool, Type typeItem)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
            else if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            if (typeItem != null)
            {
                object o = Activator.CreateInstance(typeItem);

                if (o is SpellScroll)
                {
                    SpellScroll scroll = (SpellScroll)o;
                    Spellbook book = Spellbook.Find(from, scroll.SpellID);

                    bool hasSpell = (book != null && book.HasSpell(scroll.SpellID));

                    scroll.Delete();

                    return (hasSpell ? 0 : 1042404); // null : You don't have that spell!
                }
                else if (o is Item)
                {
                    ((Item)o).Delete();
                }
            }

            return 0;
        }

        public override void PlayCraftEffect(Mobile from)
        {
            from.PlaySound(0x249);
        }

        private static Type typeofSpellScroll = typeof(SpellScroll);

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

            if (!typeofSpellScroll.IsAssignableFrom(item.ItemType)) //  not a scroll
            {
                if (failed)
                {
                    if (lostMaterial)
                        return 1044043; // You failed to create the item, and some of your materials are lost.
                    else
                        return 1044157; // You failed to create the item, but no materials were lost.
                }
                else
                {
                    if (quality == 0)
                        return 502785; // You were barely able to make this item.  It's quality is below average.
                    else if (makersMark && quality == 2)
                        return 1044156; // You create an exceptional quality item and affix your maker's mark.
                    else if (quality == 2)
                        return 1044155; // You create an exceptional quality item.
                    else
                        return 1044154; // You create the item.
                }
            }
            else
            {
                if (failed)
                    return 501630; // You fail to inscribe the scroll, and the scroll is ruined.
                else
                    return 501629; // You inscribe the spell and put the scroll in your backpack.
            }
        }

        private int m_Circle, m_Mana;

        private enum Reg { BlackPearl, Bloodmoss, Garlic, Ginseng, MandrakeRoot, Nightshade, SulfurousAsh, SpidersSilk }

        private Type[] m_RegTypes = new Type[]
            {
        typeof( BlackPearl ),
        typeof( Bloodmoss ),
        typeof( Garlic ),
        typeof( Ginseng ),
        typeof( MandrakeRoot ),
        typeof( Nightshade ),
        typeof( SulfurousAsh ),
        typeof( SpidersSilk )
            };

        private int m_Index;

        private void AddSpell(Type type, params Reg[] regs)
        {
            double minSkill, maxSkill;

            switch (m_Circle)
            {
                default:
                case 0: minSkill = -25.0; maxSkill = 25.0; break;
                case 1: minSkill = -10.8; maxSkill = 39.2; break;
                case 2: minSkill = 03.5; maxSkill = 53.5; break;
                case 3: minSkill = 17.8; maxSkill = 67.8; break;
                case 4: minSkill = 32.1; maxSkill = 82.1; break;
                case 5: minSkill = 46.4; maxSkill = 96.4; break;
                case 6: minSkill = 60.7; maxSkill = 110.7; break;
                case 7: minSkill = 75.0; maxSkill = 130.0; break;
            }

            int index = AddCraft(type, 1044369 + m_Circle, 1044381 + m_Index++, minSkill, maxSkill, m_RegTypes[(int)regs[0]], 1044353 + (int)regs[0], 1, 1044361 + (int)regs[0]);

            for (int i = 1; i < regs.Length; ++i)
                AddRes(index, m_RegTypes[(int)regs[i]], 1044353 + (int)regs[i], 1, 1044361 + (int)regs[i]);

            AddRes(index, typeof(BlankScroll), 1044377, 1, 1044378);

            SetManaReq(index, m_Mana);
        }

        // holy fuck I hate whoever wrote the Craft engine... omg
        private void AddZuluNecroSpell(int spell, int mana, double minSkill, Type type, params Type[] regs)
        {
            int id = CraftItem.ItemIDOf(regs[0]);

            int index = AddCraft(type, "Necromantic Chants", 1060509 + spell, minSkill, 130.0, regs[0], id < 0x4000 ? 1020000 + id : 1078872 + id, 1, 501627);

            for (int i = 1; i < regs.Length; ++i)
            {
                id = CraftItem.ItemIDOf(regs[i]);
                AddRes(index, regs[i], id < 0x4000 ? 1020000 + id : 1078872 + id, 1, 501627);
            }

            AddRes(index, typeof(BlankScroll), 1044377, 1, 1044378);
            SetManaReq(index, mana);
        }

        private void AddZuluEarthSpell(int spell, int mana, double minSkill, Type type, params Type[] regs)
        {
            int id = CraftItem.ItemIDOf(regs[0]);

            int index = AddCraft(type, "Druidic Magic", 1071026 + spell, minSkill, 130.0, regs[0], id < 0x4000 ? 1020000 + id : 1078872 + id, 1, 501627);

            for (int i = 1; i < regs.Length; ++i)
            {
                id = CraftItem.ItemIDOf(regs[i]);
                AddRes(index, regs[i], id < 0x4000 ? 1020000 + id : 1078872 + id, 1, 501627);
            }

            AddRes(index, typeof(BlankScroll), 1044377, 1, 1044378);
            SetManaReq(index, mana);
        }

        private void AddNecroSpell(int spell, int mana, double minSkill, Type type, params Type[] regs)
        {
            int id = CraftItem.ItemIDOf(regs[0]);

            int index = AddCraft(type, 1061677, 1060509 + spell, minSkill, minSkill + 1.0, regs[0], id < 0x4000 ? 1020000 + id : 1078872 + id, 1, 501627);  //Yes, on OSI it's only 1.0 skill diff'.  Don't blame me, blame OSI.

            for (int i = 1; i < regs.Length; ++i)
            {
                id = CraftItem.ItemIDOf(regs[i]);
                AddRes(index, regs[i], id < 0x4000 ? 1020000 + id : 1078872 + id, 1, 501627);
            }

            AddRes(index, typeof(BlankScroll), 1044377, 1, 1044378);

            SetManaReq(index, mana);
        }

        public override void InitCraftList()
        {
            m_Circle = 0;
            m_Mana = 4;

            AddSpell(typeof(ReactiveArmorScroll), Reg.Garlic, Reg.SpidersSilk, Reg.SulfurousAsh);
            AddSpell(typeof(ClumsyScroll), Reg.Bloodmoss, Reg.Nightshade);
            AddSpell(typeof(CreateFoodScroll), Reg.Garlic, Reg.Ginseng, Reg.MandrakeRoot);
            AddSpell(typeof(FeeblemindScroll), Reg.Nightshade, Reg.Ginseng);
            AddSpell(typeof(HealScroll), Reg.Garlic, Reg.Ginseng, Reg.SpidersSilk);
            AddSpell(typeof(MagicArrowScroll), Reg.SulfurousAsh);
            AddSpell(typeof(NightSightScroll), Reg.SpidersSilk, Reg.SulfurousAsh);
            AddSpell(typeof(WeakenScroll), Reg.Garlic, Reg.Nightshade);

            m_Circle = 1;
            m_Mana = 6;

            AddSpell(typeof(AgilityScroll), Reg.Bloodmoss, Reg.MandrakeRoot);
            AddSpell(typeof(CunningScroll), Reg.Nightshade, Reg.MandrakeRoot);
            AddSpell(typeof(CureScroll), Reg.Garlic, Reg.Ginseng);
            AddSpell(typeof(HarmScroll), Reg.Nightshade, Reg.SpidersSilk);
            AddSpell(typeof(MagicTrapScroll), Reg.Garlic, Reg.SpidersSilk, Reg.SulfurousAsh);
            AddSpell(typeof(MagicUnTrapScroll), Reg.Bloodmoss, Reg.SulfurousAsh);
            AddSpell(typeof(ProtectionScroll), Reg.Garlic, Reg.Ginseng, Reg.SulfurousAsh);
            AddSpell(typeof(StrengthScroll), Reg.Nightshade, Reg.MandrakeRoot);

            m_Circle = 2;
            m_Mana = 9;

            AddSpell(typeof(BlessScroll), Reg.Garlic, Reg.MandrakeRoot);
            AddSpell(typeof(FireballScroll), Reg.BlackPearl);
            AddSpell(typeof(MagicLockScroll), Reg.Bloodmoss, Reg.Garlic, Reg.SulfurousAsh);
            AddSpell(typeof(PoisonScroll), Reg.Nightshade);
            AddSpell(typeof(TelekinisisScroll), Reg.Bloodmoss, Reg.MandrakeRoot);
            AddSpell(typeof(TeleportScroll), Reg.Bloodmoss, Reg.MandrakeRoot);
            AddSpell(typeof(UnlockScroll), Reg.Bloodmoss, Reg.SulfurousAsh);
            AddSpell(typeof(WallOfStoneScroll), Reg.Bloodmoss, Reg.Garlic);

            m_Circle = 3;
            m_Mana = 11;

            AddSpell(typeof(ArchCureScroll), Reg.Garlic, Reg.Ginseng, Reg.MandrakeRoot);
            AddSpell(typeof(ArchProtectionScroll), Reg.Garlic, Reg.Ginseng, Reg.MandrakeRoot, Reg.SulfurousAsh);
            AddSpell(typeof(CurseScroll), Reg.Garlic, Reg.Nightshade, Reg.SulfurousAsh);
            AddSpell(typeof(FireFieldScroll), Reg.BlackPearl, Reg.SpidersSilk, Reg.SulfurousAsh);
            AddSpell(typeof(GreaterHealScroll), Reg.Garlic, Reg.SpidersSilk, Reg.MandrakeRoot, Reg.Ginseng);
            AddSpell(typeof(LightningScroll), Reg.MandrakeRoot, Reg.SulfurousAsh);
            AddSpell(typeof(ManaDrainScroll), Reg.BlackPearl, Reg.SpidersSilk, Reg.MandrakeRoot);
            AddSpell(typeof(RecallScroll), Reg.BlackPearl, Reg.Bloodmoss, Reg.MandrakeRoot);

            m_Circle = 4;
            m_Mana = 14;

            AddSpell(typeof(BladeSpiritsScroll), Reg.BlackPearl, Reg.Nightshade, Reg.MandrakeRoot);
            AddSpell(typeof(DispelFieldScroll), Reg.BlackPearl, Reg.Garlic, Reg.SpidersSilk, Reg.SulfurousAsh);
            AddSpell(typeof(IncognitoScroll), Reg.Bloodmoss, Reg.Garlic, Reg.Nightshade);
            AddSpell(typeof(MagicReflectScroll), Reg.Garlic, Reg.MandrakeRoot, Reg.SpidersSilk);
            AddSpell(typeof(MindBlastScroll), Reg.BlackPearl, Reg.MandrakeRoot, Reg.Nightshade, Reg.SulfurousAsh);
            AddSpell(typeof(ParalyzeScroll), Reg.Garlic, Reg.MandrakeRoot, Reg.SpidersSilk);
            AddSpell(typeof(PoisonFieldScroll), Reg.BlackPearl, Reg.Nightshade, Reg.SpidersSilk);
            AddSpell(typeof(SummonCreatureScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk);

            m_Circle = 5;
            m_Mana = 20;

            AddSpell(typeof(DispelScroll), Reg.Garlic, Reg.MandrakeRoot, Reg.SulfurousAsh);
            AddSpell(typeof(EnergyBoltScroll), Reg.BlackPearl, Reg.Nightshade);
            AddSpell(typeof(ExplosionScroll), Reg.Bloodmoss, Reg.MandrakeRoot);
            AddSpell(typeof(InvisibilityScroll), Reg.Bloodmoss, Reg.Nightshade);
            AddSpell(typeof(MarkScroll), Reg.Bloodmoss, Reg.BlackPearl, Reg.MandrakeRoot);
            AddSpell(typeof(MassCurseScroll), Reg.Garlic, Reg.MandrakeRoot, Reg.Nightshade, Reg.SulfurousAsh);
            AddSpell(typeof(ParalyzeFieldScroll), Reg.BlackPearl, Reg.Ginseng, Reg.SpidersSilk);
            AddSpell(typeof(RevealScroll), Reg.Bloodmoss, Reg.SulfurousAsh);

            m_Circle = 6;
            m_Mana = 40;

            AddSpell(typeof(ChainLightningScroll), Reg.BlackPearl, Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SulfurousAsh);
            AddSpell(typeof(EnergyFieldScroll), Reg.BlackPearl, Reg.MandrakeRoot, Reg.SpidersSilk, Reg.SulfurousAsh);
            AddSpell(typeof(FlamestrikeScroll), Reg.SpidersSilk, Reg.SulfurousAsh);
            AddSpell(typeof(GateTravelScroll), Reg.BlackPearl, Reg.MandrakeRoot, Reg.SulfurousAsh);
            AddSpell(typeof(ManaVampireScroll), Reg.BlackPearl, Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk);
            AddSpell(typeof(MassDispelScroll), Reg.BlackPearl, Reg.Garlic, Reg.MandrakeRoot, Reg.SulfurousAsh);
            AddSpell(typeof(MeteorSwarmScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SulfurousAsh, Reg.SpidersSilk);
            AddSpell(typeof(PolymorphScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk);

            m_Circle = 7;
            m_Mana = 50;

            AddSpell(typeof(EarthquakeScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.Ginseng, Reg.SulfurousAsh);
            AddSpell(typeof(EnergyVortexScroll), Reg.BlackPearl, Reg.Bloodmoss, Reg.MandrakeRoot, Reg.Nightshade);
            AddSpell(typeof(ResurrectionScroll), Reg.Bloodmoss, Reg.Garlic, Reg.Ginseng);
            AddSpell(typeof(SummonAirElementalScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk);
            AddSpell(typeof(SummonDaemonScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk, Reg.SulfurousAsh);
            AddSpell(typeof(SummonEarthElementalScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk);
            AddSpell(typeof(SummonFireElementalScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk, Reg.SulfurousAsh);
            AddSpell(typeof(SummonWaterElementalScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk);

            //necro
            AddZuluNecroSpell(0, 40, 100.0, typeof(ControlUndeadScroll), Reagent.Bloodspawn, Reagent.Bone, Reagent.Blackmoor);
            AddZuluNecroSpell(1, 40, 100.0, typeof(DarknessScroll), Reagent.Pumice, Reagent.PigIron);
            AddZuluNecroSpell(2, 40, 100.0, typeof(DecayingRayScroll), Reagent.VialOfBlood, Reagent.VolcanicAsh, Reagent.DaemonBone);
            AddZuluNecroSpell(3, 40, 100.0, typeof(SpectresTouchScroll), Reagent.ExecutionersCap, Reagent.Brimstone, Reagent.DaemonBone);
            AddZuluNecroSpell(4, 60, 100.0, typeof(AbyssalFlameScroll), Reagent.Brimstone, Reagent.Obsidian, Reagent.VolcanicAsh, Reagent.DaemonBone);
            AddZuluNecroSpell(5, 60, 100.0, typeof(AnimateDeadScroll), Reagent.Bone, Reagent.FertileDirt, Reagent.VialOfBlood, Reagent.Obsidian);
            AddZuluNecroSpell(6, 60, 100.0, typeof(SacrificeScroll), Reagent.ExecutionersCap, Reagent.Bloodspawn, Reagent.WyrmsHeart, Reagent.Bone);
            AddZuluNecroSpell(7, 60, 100.0, typeof(WraithBreathScroll), Reagent.Obsidian, Reagent.Pumice, Reagent.Bone, Reagent.Blackmoor);
            AddZuluNecroSpell(8, 100, 100.0, typeof(SorcerorsBaneScroll), Reagent.VolcanicAsh, Reagent.Pumice, Reagent.DragonsBlood, Reagent.DeadWood);
            AddZuluNecroSpell(9, 100, 100.0, typeof(SummonSpiritScroll), Reagent.DaemonBone, Reagent.Brimstone, Reagent.DragonsBlood, Reagent.Bloodspawn);
            AddZuluNecroSpell(10, 100, 100.0, typeof(WraithFormScroll), Reagent.DaemonBone, Reagent.Brimstone, Reagent.Bloodspawn);
            AddZuluNecroSpell(11, 100, 100.0, typeof(WyvernStrikeScroll), Reagent.DragonsBlood, Reagent.NoxCrystal, Reagent.Blackmoor, Reagent.Bloodspawn);
            AddZuluNecroSpell(12, 130, 100.0, typeof(KillScroll), Reagent.DaemonBone, Reagent.ExecutionersCap, Reagent.VialOfBlood, Reagent.WyrmsHeart);
            AddZuluNecroSpell(13, 130, 100.0, typeof(LicheFormScroll), Reagent.DaemonBone, Reagent.Brimstone, Reagent.DragonsBlood, Reagent.VolcanicAsh);
            AddZuluNecroSpell(14, 130, 100.0, typeof(PlagueScroll), Reagent.VolcanicAsh, Reagent.BatWing, Reagent.Pumice, Reagent.NoxCrystal);
            AddZuluNecroSpell(15, 130, 100.0, typeof(SpellbindScroll), Reagent.EyeOfNewt, Reagent.VialOfBlood, Reagent.FertileDirt, Reagent.PigIron);

            //earth
            AddZuluEarthSpell(0, 5, 100.0, typeof(AntidoteScroll), Reagent.DeadWood, Reagent.FertileDirt, Reagent.ExecutionersCap);
            AddZuluEarthSpell(1, 5, 100.0, typeof(OwlSightScroll), Reagent.EyeOfNewt);
            AddZuluEarthSpell(2, 5, 100.0, typeof(ShiftingEarthScroll), Reagent.FertileDirt, Reagent.DeadWood, Reagent.Obsidian);
            AddZuluEarthSpell(3, 5, 100.0, typeof(SummonMammalsScroll), Reagent.NoxCrystal, Reagent.PigIron, Reagent.EyeOfNewt);
            AddZuluEarthSpell(4, 10, 100.0, typeof(CallLightningScroll), Reagent.WyrmsHeart, Reagent.PigIron, Reagent.Bone);
            AddZuluEarthSpell(5, 10, 100.0, typeof(EarthsBlessingScroll), Reagent.PigIron, Reagent.Obsidian, Reagent.VolcanicAsh);
            AddZuluEarthSpell(6, 10, 100.0, typeof(EarthPortalScroll), Reagent.Brimstone, Reagent.ExecutionersCap, Reagent.EyeOfNewt);
            AddZuluEarthSpell(7, 10, 100.0, typeof(NaturesTouchScroll), Reagent.Pumice, Reagent.VialOfBlood, Reagent.Obsidian);
            AddZuluEarthSpell(8, 15, 100.0, typeof(GustOfAirScroll), Reagent.FertileDirt, Reagent.Brimstone, Reagent.EyeOfNewt);
            AddZuluEarthSpell(9, 15, 100.0, typeof(RisingFireScroll), Reagent.BatWing, Reagent.Brimstone, Reagent.VialOfBlood);
            AddZuluEarthSpell(10, 15, 100.0, typeof(ShapeshiftScroll), Reagent.WyrmsHeart, Reagent.Blackmoor, Reagent.BatWing);
            AddZuluEarthSpell(11, 20, 100.0, typeof(IceStrikeScroll), Reagent.Bone, Reagent.BatWing, Reagent.Brimstone);
            AddZuluEarthSpell(12, 20, 100.0, typeof(EarthSpiritScroll), Reagent.DragonsBlood, Reagent.FertileDirt, Reagent.VolcanicAsh);
            AddZuluEarthSpell(13, 20, 100.0, typeof(FireSpiritScroll), Reagent.EyeOfNewt, Reagent.Blackmoor, Reagent.Obsidian);
            AddZuluEarthSpell(14, 20, 100.0, typeof(StormSpiritScroll), Reagent.FertileDirt, Reagent.VolcanicAsh, Reagent.BatWing);
            AddZuluEarthSpell(15, 20, 100.0, typeof(WaterSpiritScroll), Reagent.WyrmsHeart, Reagent.NoxCrystal, Reagent.EyeOfNewt);

            // Runebook
            int index = AddCraft(typeof(Runebook), 1044294, 1041267, 45.0, 95.0, typeof(BlankScroll), 1044377, 8, 1044378);
            AddRes(index, typeof(RecallScroll), 1044445, 1, 1044253);
            AddRes(index, typeof(GateTravelScroll), 1044446, 1, 1044253);

            if (Core.AOS)
            {
                //				AddCraft(typeof(Engines.BulkOrders.BulkOrderBook), 1044294, 1028793, 65.0, 115.0, typeof(BlankScroll), 1044377, 10, 1044378);
            }

            if (Core.SE)
            {
                AddCraft(typeof(Spellbook), 1044294, 1023834, 50.0, 126, typeof(BlankScroll), 1044377, 10, 1044378);
            }

            /* TODO
               if ( Core.ML )
               {
               index = AddCraft( typeof( ScrappersCompendium ), 1044294, 1072940, 75.0, 125.0, typeof( BlankScroll ), 1044377, 100, 1044378 );
               AddRes( index, typeof( DreadHornMane ), 1032682, 1, 1044253 );
               AddRes( index, typeof( Taint ), 1032679, 10, 1044253 );
               AddRes( index, typeof( Corruption ), 1032676, 10, 1044253 );
               AddRareRecipe( index, 400 );
               ForceNonExceptional( index );
               SetNeededExpansion( index, Expansion.ML );
               }
            */

            MarkOption = true;
        }
    }
}
