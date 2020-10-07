using System;
using System.Collections.Generic;
using System.Linq;
using Server;
using Server.Commands;
using Server.Items;

namespace ZuluContent.Zulu.Engines.Magic.Enums
{
    public partial interface IMagicItem
    {
        public IEntity Parent { get; set; }
        
        public EnchantmentDictionary Enchantments { get; }
        
        public void OnSingleClick(Mobile m);

        public static void OnIdentified(IMagicItem magicItem)
        {
            // if (magicItem is Item item)
            // {
            //     var values = magicItem.MagicProps.GetAllValues().Where(v => v.Enchant != null && v.Enchant.Hue > 0);
            //     
            //     if (values.FirstOrDefault(v => v.Prop == MagicProp.ArmorBonus) is IMagicMod<ArmorBonusType> armorBonus)
            //     {
            //         if(armorBonus.Target != ArmorBonusType.None)
            //             item.Hue = armorBonus.Enchant.Hue;
            //         return;
            //     }
            //
            //     foreach (var value in values)
            //     {
            //         item.Hue = value.Enchant.Hue;
            //     }
            // }
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Identified
        {
            get;
            set;
        }
    }
}