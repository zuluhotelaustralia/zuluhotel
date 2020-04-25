using System;
using Server;

namespace Server.Items
{
    public class PotionEffect {
        private PotionEffect(int ordinal, int bottleLabel, int kegLabel):this(ordinal, bottleLabel, kegLabel, null, null) {}

        private int m_Ordinal;
        
        private PotionEffect(int ordinal, int bottleLabel, int kegLabel, string bottleName, string kegName) {
            m_BottleLabel = bottleLabel;
            m_KegLabel = kegLabel;
            m_BottleName = bottleName;
            m_KegName = kegName;
            m_Ordinal = ordinal;
        }

        private PotionEffect(int ordinal, string bottleName, string kegName):this(ordinal, -1, -1, bottleName, kegName) {
        }

        public static PotionEffect Deserialize(GenericReader r) {
            // Backwards compatible with previous enum of potion effects.
            return PotionEffect.FromOrdinal(r.ReadInt());
        }

        public static PotionEffect FromOrdinal(int ord) {
            switch(ord) {
                case 0: return Nightsight;
                case 1: return CureLesser;
                case 2: return Cure;
                case 3: return CureGreater;
                case 4: return Agility;
                case 5: return AgilityGreater;
                case 6: return Strength;
                case 7: return StrengthGreater;
                case 8: return PoisonLesser;
                case 9: return Poison;
                case 10: return PoisonGreater;
                case 11: return PoisonDeadly;
                case 12: return Refresh;
                case 13: return RefreshTotal;
                case 14: return HealLesser;
                case 15: return Heal;
                case 16: return HealGreater;
                case 17: return ExplosionLesser;
                case 18: return Explosion;
                case 19: return ExplosionGreater;
                case 20: return Conflagration;
                case 21: return ConflagrationGreater;
                case 22: return MaskOfDeath;
                case 23: return MaskOfDeathGreater;
                case 24: return ConfusionBlast;
                case 25: return ConfusionBlastGreater;
                case 26: return Invisibility;
                case 27: return Parasitic;
                case 28: return Darkglow;
                case 29: return PoisonLethal;
                case 30: return Mana;
                case 31: return ManaTotal;
                default:
                    throw new ArgumentException(String.Format("Unknown potion ordinal {0}", ord));
            }
        }

        public void Serialize(GenericWriter w) {
            w.Write(m_Ordinal);
        }

        public BasePotion CreatePotion() {
            switch(m_Ordinal) {
                case 0: return new NightSightPotion();
                case 1: return new LesserCurePotion();
                case 2: return new CurePotion();
                case 3: return new GreaterCurePotion();
                case 4: return new AgilityPotion();
                case 5: return new GreaterAgilityPotion();
                case 6: return new StrengthPotion();
                case 7: return new GreaterStrengthPotion();
                case 8: return new LesserPoisonPotion();
                case 9: return new PoisonPotion();
                case 10: return new GreaterPoisonPotion();
                case 11: return new DeadlyPoisonPotion();
                case 12: return new RefreshPotion();
                case 13: return new TotalRefreshPotion();
                case 14: return new LesserHealPotion();
                case 15: return new HealPotion();
                case 16: return new GreaterHealPotion();
                case 17: return new LesserExplosionPotion();
                case 18: return new ExplosionPotion();
                case 19: return new GreaterExplosionPotion();
                case 20: return new ConflagrationPotion();
                case 21: return new GreaterConflagrationPotion();
//                case 22: return new MaskOfDeathPotion();
//                case 23: return new GreaterMaskOfDeathPotion();
                case 24: return new ConfusionBlastPotion();
                case 25: return new GreaterConfusionBlastPotion();
                case 26: return new InvisibilityPotion();
                case 27: return new ParasiticPotion();
                case 28: return new DarkglowPotion();
                case 29: return new LethalPoisonPotion();
                case 30: return new ManaPotion();
                case 31: return new TotalManaPotion();
                default: return null;
            }
        }

        private string m_BottleName;
        public virtual string BottleName {
            get { return m_BottleName; }
        }

        private string m_KegName;
        public virtual string KegName {
            get { return m_KegName; }
        }

        private int m_BottleLabel;
        public virtual int BottleLabel {
            get { return m_BottleLabel; }
        }
        
        private int m_KegLabel;
        public virtual int KegLabel {
            get { return m_KegLabel; }
        }

	public readonly static PotionEffect Nightsight = new PotionEffect(0, 1041314, 1041620);
	public readonly static PotionEffect CureLesser = new PotionEffect(1, 1041315, 1041621);
	public readonly static PotionEffect Cure = new PotionEffect(2, 1041316, 1041622);
	public readonly static PotionEffect CureGreater = new PotionEffect(3, 1041317, 1041623);
	public readonly static PotionEffect Agility = new PotionEffect(4, 1041318, 1041624);
	public readonly static PotionEffect AgilityGreater = new PotionEffect(5, 1041319, 1041625);
	public readonly static PotionEffect Strength = new PotionEffect(6, 1041320, 1041626);
	public readonly static PotionEffect StrengthGreater = new PotionEffect(7, 1041321, 1041627);
	public readonly static PotionEffect PoisonLesser = new PotionEffect(8, 1041322, 1041628);
	public readonly static PotionEffect Poison = new PotionEffect(9, 1041323, 1041629);
	public readonly static PotionEffect PoisonGreater = new PotionEffect(10, 1041324, 1041630);
	public readonly static PotionEffect PoisonDeadly = new PotionEffect(11, 1041325, 1041631);
	public readonly static PotionEffect Refresh = new PotionEffect(12, 1041326, 1041632);
	public readonly static PotionEffect RefreshTotal = new PotionEffect(13, 1041327, 1041633);
	public readonly static PotionEffect HealLesser = new PotionEffect(14, 1041328, 1041634);
	public readonly static PotionEffect Heal = new PotionEffect(15, 1041329, 1041635);
	public readonly static PotionEffect HealGreater = new PotionEffect(16, 1041330, 1041636);
	public readonly static PotionEffect ExplosionLesser = new PotionEffect(17, 1041331, 1041637);
	public readonly static PotionEffect Explosion = new PotionEffect(18, 1041332, 1041638);
	public readonly static PotionEffect ExplosionGreater = new PotionEffect(19, 1041333, 1041639);
	public readonly static PotionEffect Conflagration = new PotionEffect(20, 1072095, 1072658);
	public readonly static PotionEffect ConflagrationGreater = new PotionEffect(21, 1072098, 1072659);
	public readonly static PotionEffect MaskOfDeath = new PotionEffect(22, 1041336, 1072660);
	public readonly static PotionEffect MaskOfDeathGreater = new PotionEffect(23, 1041337, 1072661);
	public readonly static PotionEffect ConfusionBlast = new PotionEffect(24, 1072105, 1072662);
	public readonly static PotionEffect ConfusionBlastGreater = new PotionEffect(25, 1072108, 1072663);
	public readonly static PotionEffect Invisibility = new PotionEffect(26, 1072941, 1072664);
	public readonly static PotionEffect Parasitic = new PotionEffect(27, 1072848, 1072665);
	public readonly static PotionEffect Darkglow = new PotionEffect(28, 1072849, 1072666);
	public readonly static PotionEffect PoisonLethal = new PotionEffect(29, "Lethal Poison potion", "A keg of Lethal Poison potions");
        public readonly static PotionEffect Mana = new PotionEffect(30, "Mana potion", "A keg of Mana potions");
	public readonly static PotionEffect ManaTotal = new PotionEffect(31, "Mage's Brew", "A keg of Mage's Brew");
    }
}
