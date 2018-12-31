using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;
using Server.Engines.Craft;
using Server.Engines.Harvest;
using Server.Engines.Gather;
using Server.ContextMenus;

namespace Server.Items
{
    public interface IUsesRemaining
    {
        int UsesRemaining{ get; set; }
        bool ShowUsesRemaining{ get; set; }
    }

    public abstract class BaseHarvestTool : Item, IUsesRemaining, ICraftable
    {
        private Mobile m_Crafter;
        private ToolQuality m_Quality;
        private int m_UsesRemaining;
        private CraftResource m_Resource;

        [CommandProperty( AccessLevel.GameMaster )]
        public CraftResource Resource {
            get { return m_Resource; }
            set {
                if( m_Resource != value ){
                    m_Resource = value;
                    Hue = CraftResources.GetHue(m_Resource);
                }
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public Mobile Crafter
        {
            get{ return m_Crafter; }
            set{ m_Crafter = value; InvalidateProperties(); }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public ToolQuality Quality
        {
            get{ return m_Quality; }
            set{ UnscaleUses(); m_Quality = value; InvalidateProperties(); ScaleUses(); }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int UsesRemaining
        {
            get { return m_UsesRemaining; }
            set { m_UsesRemaining = value; InvalidateProperties(); }
        }

        private bool m_UseGatherSystem = true;
        [CommandProperty( AccessLevel.GameMaster )]
        public bool UseGatherSystem
        {
            get { return m_UseGatherSystem; }
            set {
                m_UseGatherSystem = value;
            }
        }

        private string GetNameString()
        {
            string name = this.Name;

            if( name == null )
                name = String.Format("#{0}", LabelNumber );

            return name;
        }

        public override void AddNameProperty( ObjectPropertyList list ){
            int oreType; //tinkering makes pickaxes and stuff from ingots

            switch ( m_Resource ){
                case CraftResource.Gold: oreType = 1160000; break;
                case CraftResource.Spike: oreType = 1160001; break;
                case CraftResource.Fruity: oreType = 1160002; break;
                case CraftResource.Bronze: oreType = 1160003; break;
                case CraftResource.IceRock: oreType = 1160004; break;
                case CraftResource.BlackDwarf: oreType = 1160005; break;
                case CraftResource.DullCopper: oreType = 1160006; break;
                case CraftResource.Platinum: oreType = 1160007; break;
                case CraftResource.SilverRock: oreType = 1160008; break;
                case CraftResource.DarkPagan: oreType = 1160009; break;
                case CraftResource.Copper: oreType = 1160010; break;
                case CraftResource.Mystic: oreType = 1160011; break;
                case CraftResource.Spectral: oreType = 1160012; break;
                case CraftResource.OldBritain: oreType = 1160013; break;
                case CraftResource.Onyx: oreType = 1160014; break;
                case CraftResource.RedElven: oreType = 1160015; break;
                case CraftResource.Undead: oreType = 1160016; break;
                case CraftResource.Pyrite: oreType = 1160017; break;
                case CraftResource.Virginity: oreType = 1160018; break;
                case CraftResource.Malachite: oreType = 1160019; break;
                case CraftResource.Lavarock: oreType = 1160020; break;
                case CraftResource.Azurite: oreType = 1160021; break;
                case CraftResource.Dripstone: oreType = 1160022; break;
                case CraftResource.Executor: oreType = 1160023; break;
                case CraftResource.Peachblue: oreType = 1160024; break;
                case CraftResource.Destruction: oreType = 1160025; break;
                case CraftResource.Anra: oreType = 1160026; break;
                case CraftResource.Crystal: oreType = 1160027; break;
                case CraftResource.Doom: oreType = 1160028; break;
                case CraftResource.Goddess: oreType = 1160029; break;
                case CraftResource.NewZulu: oreType = 1160030; break;
                case CraftResource.DarkSableRuby: oreType = 1160031; break;
                case CraftResource.EbonTwilightSapphire: oreType = 1160032; break;
                case CraftResource.RadiantNimbusDiamond: oreType = 1160033; break;
                default: oreType = 0; break;
            }

            if ( m_Quality == ToolQuality.Exceptional )
            {
                if ( oreType != 0 )
                    list.Add( 1053100, "#{0}\t{1}", oreType, GetNameString() ); // exceptional ~1_oretype~ ~2_armortype~
                else
                    list.Add( 1050040, GetNameString() ); // exceptional ~1_ITEMNAME~
            }
            else
            {
                if ( oreType != 0 )
                    list.Add( 1053099, "#{0}\t{1}", oreType, GetNameString() ); // ~1_oretype~ ~2_armortype~
                else if ( Name == null )
                    list.Add( LabelNumber );
                else
                    list.Add( Name );
            }
        }

        public void ScaleUses()
        {
            m_UsesRemaining = (m_UsesRemaining * GetUsesScalar()) / 100;
            InvalidateProperties();
        }

        public void UnscaleUses()
        {
            m_UsesRemaining = (m_UsesRemaining * 100) / GetUsesScalar();
        }

        public int GetUsesScalar()
        {
            if ( m_Quality == ToolQuality.Exceptional )
                return 200;

            return 100;
        }

        public bool ShowUsesRemaining{ get{ return true; } set{} }

        public abstract HarvestSystem HarvestSystem{ get; }
        public abstract GatherSystem GatherSystem{ get; }

        public BaseHarvestTool( int itemID ) : this( 50, itemID )
        {
        }

        public BaseHarvestTool( int usesRemaining, int itemID ) : base( itemID )
        {
            m_UsesRemaining = usesRemaining;
            m_Quality = ToolQuality.Regular;
        }

        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties( list );

            // Makers mark not displayed on OSI
            //if ( m_Crafter != null )
            //	list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~

            if ( m_Quality == ToolQuality.Exceptional )
                list.Add( 1060636 ); // exceptional

            list.Add( 1060584, m_UsesRemaining.ToString() ); // uses remaining: ~1_val~
        }

        public virtual void DisplayDurabilityTo( Mobile m )
        {
            LabelToAffix( m, 1017323, AffixType.Append, ": " + m_UsesRemaining.ToString() ); // Durability
        }

        public override void OnSingleClick( Mobile from )
        {
            DisplayDurabilityTo( from );

            base.OnSingleClick( from );
        }

        public override void OnDoubleClick( Mobile from )
        {
            if ( IsChildOf( from.Backpack ) || Parent == from )
                this.GatherSystem.BeginGathering( from, this );
            else
                from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
        }

        public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
        {
            base.GetContextMenuEntries( from, list );

            AddContextMenuEntries( from, this, list, HarvestSystem );
        }

        public static void AddContextMenuEntries( Mobile from, Item item, List<ContextMenuEntry> list, HarvestSystem system )
        {
            if ( system != Server.Engines.Harvest.Mining.System )
                return;

            if ( !item.IsChildOf( from.Backpack ) && item.Parent != from )
                return;

            PlayerMobile pm = from as PlayerMobile;

            if ( pm == null )
                return;

            ContextMenuEntry miningEntry = new ContextMenuEntry( pm.ToggleMiningStone ? 6179 : 6178 );
            miningEntry.Color = 0x421F;
            list.Add( miningEntry );

            list.Add( new ToggleMiningStoneEntry( pm, false, 6176 ) );
            list.Add( new ToggleMiningStoneEntry( pm, true, 6177 ) );
        }

        private class ToggleMiningStoneEntry : ContextMenuEntry
        {
            private PlayerMobile m_Mobile;
            private bool m_Value;

            public ToggleMiningStoneEntry( PlayerMobile mobile, bool value, int number ) : base( number )
            {
                m_Mobile = mobile;
                m_Value = value;

                bool stoneMining = ( mobile.StoneMining && mobile.Skills[SkillName.Mining].Base >= 100.0 );

                if ( mobile.ToggleMiningStone == value || ( value && !stoneMining ) )
                    this.Flags |= CMEFlags.Disabled;
            }

            public override void OnClick()
            {
                bool oldValue = m_Mobile.ToggleMiningStone;

                if ( m_Value )
                {
                    if ( oldValue )
                    {
                        m_Mobile.SendLocalizedMessage( 1054023 ); // You are already set to mine both ore and stone!
                    }
                    else if ( !m_Mobile.StoneMining || m_Mobile.Skills[SkillName.Mining].Base < 100.0 )
                    {
                        m_Mobile.SendLocalizedMessage( 1054024 ); // You have not learned how to mine stone or you do not have enough skill!
                    }
                    else
                    {
                        m_Mobile.ToggleMiningStone = true;
                        m_Mobile.SendLocalizedMessage( 1054022 ); // You are now set to mine both ore and stone.
                    }
                }
                else
                {
                    if ( oldValue )
                    {
                        m_Mobile.ToggleMiningStone = false;
                        m_Mobile.SendLocalizedMessage( 1054020 ); // You are now set to mine only ore.
                    }
                    else
                    {
                        m_Mobile.SendLocalizedMessage( 1054021 ); // You are already set to mine only ore!
                    }
                }
            }
        }

        public BaseHarvestTool( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 1 ); // version

            writer.Write( (Mobile) m_Crafter );
            writer.Write( (int) m_Quality );

            writer.Write( (int) m_UsesRemaining );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();

            switch ( version )
            {
                case 1:
                    {
                        m_Crafter = reader.ReadMobile();
                        m_Quality = (ToolQuality) reader.ReadInt();
                        goto case 0;
                    }
                case 0:
                    {
                        m_UsesRemaining = reader.ReadInt();
                        break;
                    }
            }
        }

        #region ICraftable Members

        public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
        {
            Quality = (ToolQuality)quality;

            if ( makersMark )
                Crafter = from;

            Type resourceType = typeRes;

            if( resourceType == null ){
                resourceType = craftItem.Resources.GetAt(0).ItemType;
            }

            Resource = CraftResources.GetFromType( resourceType );

            return quality;
        }

        #endregion
    }
}
