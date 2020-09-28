using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Engines.Magic;
using Server;
using Server.Commands;
using Server.Items;

namespace ZuluContent.Zulu.Engines.Magic
{
    public partial interface IMagicItem
    {
        public IEntity Parent { get; set; }

        public MagicalProperties MagicProps { get; }

        public void OnSingleClick(Mobile m);

        public static void OnIdentified(IMagicItem magicItem)
        {
            if (magicItem is Item item)
            {
                var values = magicItem.MagicProps.GetAllValues().Where(v => v.Info != null && v.Info.Hue > 0);
                
                if (values.FirstOrDefault(v => v.Prop == MagicProp.ArmorBonus) is IMagicMod<ArmorBonus> armorBonus)
                {
                    if(armorBonus.Target != ArmorBonus.None)
                        item.Hue = armorBonus.Info.Hue;
                    return;
                }
            
                foreach (var value in values)
                {
                    item.Hue = value.Info.Hue;
                }
            }
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Identified
        {
            get;
            set;
        }
    }
}