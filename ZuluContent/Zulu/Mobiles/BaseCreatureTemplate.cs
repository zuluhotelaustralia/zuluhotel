using System;
using System.Linq;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Utilities;
using ZuluContent.Configuration.Types.Creatures;
using ZuluContent.Zulu.Engines.Magic;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace Server.Mobiles
{
    public class BaseCreatureTemplate : BaseCreature
    {
        private static readonly Action<CreatureProperties, BaseCreature> MapAction
            = ZuluUtil.BuildMapAction<CreatureProperties, BaseCreature>();
        public string TemplateName { get; private set; }
        public CreatureProperties Properties => TemplateName != null ? ZhConfig.Creatures.Entries[TemplateName] : null;

        [Constructible]
        public BaseCreatureTemplate(string templateName) : base(ZhConfig.Creatures.Entries[templateName])
        {
            TemplateName = templateName;
            Apply(ZhConfig.Creatures.Entries[templateName]);
        }

        public BaseCreatureTemplate(Serial serial) : base(serial)
        {
        }

        public void Apply(CreatureProperties props)
        {
            MapAction(props, this);

            SetHits(props.HitsMaxSeed);
            SetStam(StamMaxSeed);
            SetMana(ManaMaxSeed);

            // Non-mappable props
            if (props.Skills.Any())
            {
                foreach (var (skill, prop) in props.Skills)
                    SetSkill(skill, prop);
            }

            if (props.Resistances.Any())
            {
                foreach (var (resistance, prop) in props.Resistances)
                    this.TrySetResist(resistance, prop);
            }
            

            Dress(props);
        }

        private void Dress(CreatureProperties props)
        {
            if (!Items.Any() && props.Equipment.Any())
            {
                foreach (var equip in props.Equipment)
                {
                    var item = equip.ItemType?.CreateInstance<Item>();

                    if (item == null)
                        continue;

                    if (equip.Name != null)
                        item.Name = equip.Name;

                    if (equip.Hue != null)
                        item.Hue = equip.Hue;

                    if (item is BaseArmor armor && equip.ArmorRating != null)
                        armor.BaseArmorRating = (int)equip.ArmorRating;

                    AddItem(item);
                    item.Movable = false;
                }
            }


            if (props.Attack != null)
            {
                DamageMin = (int)props.Attack.Damage.Min;
                DamageMax = (int)(props.Attack.Damage.Max ?? props.Attack.Damage.Min);

                if (Weapon == null && (
                        props.Attack.Animation != null || props.Attack.HitSound != null ||
                        props.Attack.MissSound != null || props.Attack.MaxRange != null
                    )
                )
                {
                    AddItem(new Fists());
                }


                if (Weapon is BaseWeapon weapon)
                {
                    if (props.Attack.Animation != null)
                        weapon.Animation = props.Attack.Animation.Value;

                    if (props.Attack.HitSound != null)
                        weapon.HitSound = props.Attack.HitSound.Value;

                    if (props.Attack.MissSound != null)
                        weapon.MissSound = props.Attack.MissSound.Value;

                    if (props.Attack.MaxRange != null)
                        weapon.MaxRange = props.Attack.MaxRange.Value;

                    if (props.Attack.Speed != null)
                        weapon.Speed = props.Attack.Speed;

                    if (props.Attack.HitPoison != null)
                        weapon.Poison = props.Attack.HitPoison;

                    if (props.Attack.ProjectileEffectId != null && weapon is BaseRanged br)
                        br.EffectId = props.Attack.ProjectileEffectId.Value;
                }
            }
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
            writer.Write(TemplateName);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();

            switch (version)
            {
                case 0:
                {
                    TemplateName = reader.ReadString();
                    if (ZhConfig.Creatures.Entries.TryGetValue(TemplateName, out var props))
                    {
                        Apply(props);
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Could not find creature template matching {TemplateName}, deleting Mobile {Serial}");
                        Timer.DelayCall(TimeSpan.Zero, Delete);
                    }
                    break;
                }
            }
        }
    }
}