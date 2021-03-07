using System;
using Server;
using Server.Engines.Craft;
using Server.Engines.Magic;
using Server.Items;
using Server.Mobiles;
using Server.Spells;

namespace ZuluContent.Zulu.Engines.Magic.Hooks
{
    public interface IEnchantmentHook
    {
        public void OnIdentified(IEntity entity);

        public void OnAdded(IEntity entity);

        public void OnRemoved(IEntity entity, IEntity parent);

        public void OnBeforeRemoved(IEntity entity, Mobile from, ref bool canRemove);

        public void OnSpellAreaCalculation(Mobile caster, Spell spell, ElementalType damageType, ref double area);

        public void OnSpellDamage(Mobile attacker, Mobile defender, Spell spell, ElementalType damageType,
            ref int damage);

        public void OnGetCastDelay(Mobile mobile, Spell spell, ref double delay);

        public void OnParalysis(Mobile mobile, ref TimeSpan duration, ref bool paralyze);

        public void OnPoison(Mobile attacker, Mobile defender, Poison poison, ref bool immune);

        public void OnHeal(Mobile healer, Mobile patient, object source, ref double healAmount);

        public void OnAnimalTaming(Mobile tamer, BaseCreature creature, ref int chance);

        public void OnTracking(Mobile tracker, ref int range);

        public void OnExceptionalChance(Mobile crafter, ref double exceptionalChance, ref int exceptionalDifficulty);

        public void OnQualityBonus(Mobile crafter, ref int multiplier);

        public void OnArmsLoreBonus(Mobile crafter, ref double armsLoreValue);

        public void OnMeditation(Mobile mobile, ref int regen, ref double tickIntervalSeconds);

        public void OnModifyWithMagicEfficiency(Mobile mobile, ref double value);

        public void OnToolHarvestColoredQualityChance(Mobile mobile, ref int bonus, ref int toMod);

        public void OnHarvestColoredQualityChance(Mobile mobile, ref int bonus, ref int toMod);

        public void OnHarvestColoredChance(Mobile mobile, ref int chance);

        public void OnHarvestAmount(Mobile harvester, ref int amount);

        public void OnToolHarvestBonus(Mobile harvester, ref int amount);

        public void OnHarvestBonus(Mobile harvester, ref int amount);

        public void OnBeforeSwing(Mobile attacker, Mobile defender);

        public void OnSwing(Mobile attacker, Mobile defender, ref double damageBonus, ref TimeSpan result);

        public void OnGetSwingDelay(ref double delayInSeconds, Mobile m);

        public void OnCheckHit(Mobile attacker, Mobile defender, ref bool result);

        public void OnMeleeHit(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage);

        public void OnAbsorbMeleeDamage(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage);

        public void OnShieldHit(Mobile attacker, Mobile defender, BaseWeapon weapon, BaseShield shield, ref int damage);

        public void OnArmorHit(Mobile attacker, Mobile defender, BaseWeapon weapon, BaseArmor armor, ref int damage);

        public void OnCraftItemCreated(Mobile from, CraftSystem craftSystem, CraftItem craftItem, BaseTool tool,
            Item item);

        public void OnCraftItemAddToBackpack(Mobile from, CraftSystem craftSystem, CraftItem craftItem, BaseTool tool,
            Item item);

        public void OnCraftSkillRequiredForFame(Mobile from, ref int craftSkillRequiredForFame);

        public void OnSummonFamiliar(Mobile caster, BaseCreature familiar);
        
        public void OnCure(Mobile caster, Mobile target, Poison poison, object source, ref double difficulty);

        public void OnTrap(Mobile caster, Container target, ref double strength);
    }
}