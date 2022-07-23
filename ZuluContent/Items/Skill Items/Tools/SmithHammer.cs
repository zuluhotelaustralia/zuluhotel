using System.Linq;
using Server.Engines.Craft;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items.SingleClick;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;

namespace Server.Items;

[FlipableAttribute(0x13E3, 0x13E4)]
public class SmithHammer : BaseTool, IMagicItem
{
    public override CraftSystem CraftSystem => DefBlacksmithy.CraftSystem;
    
    private EnchantmentDictionary m_Enchantments;

    public EnchantmentDictionary Enchantments
    {
        get => m_Enchantments ??= new EnchantmentDictionary();
        protected set => m_Enchantments = value;
    }

    [CommandProperty(AccessLevel.GameMaster)]
    public bool Identified
    {
        get => Enchantments.Identified;
        set
        {
            if (!Enchantments.Identified && value)
                Enchantments.OnIdentified(this);

            Enchantments.Identified = value;
        }
    }


    [Constructible]
    public SmithHammer() : base(0x13E3)
    {
        Weight = 8.0;
        Layer = Layer.OneHanded;
    }


    [Constructible]
    public SmithHammer(int uses) : base(uses, 0x13E3)
    {
        Weight = 8.0;
        Layer = Layer.OneHanded;
    }

    [Constructible]
    public SmithHammer(Serial serial) : base(serial)
    {
    }

    public override void Serialize(IGenericWriter writer)
    {
        base.Serialize(writer);

        writer.Write(1); // version
        Enchantments.Serialize(writer);
    }

    public override void Deserialize(IGenericReader reader)
    {
        base.Deserialize(reader);

        var version = reader.ReadInt();

        if (version == 1)
        {
            m_Enchantments = EnchantmentDictionary.Deserialize(reader);
            if (Parent is Mobile mobile)
                m_Enchantments.FireHook(e => e.OnAdded(this, mobile));
        }
    }
    
    public override void OnSingleClick(Mobile from)
    {
        HandleSingleClick(this, from);
    }
    
    [CommandProperty(AccessLevel.GameMaster)]
    public SkillName FirstSkillBonusName
    {
        get => Enchantments.Get((FirstSkillBonus e) => e.Skill);
        set => Enchantments.Set((FirstSkillBonus e) => e.Skill = value);
    }

    [CommandProperty(AccessLevel.GameMaster)]
    public double FirstSkillBonusValue
    {
        get => Enchantments.Get((FirstSkillBonus e) => e.Value);
        set => Enchantments.Set((FirstSkillBonus e) => e.Value = value);
    }
    
    [CommandProperty(AccessLevel.GameMaster)]
    public CurseType Cursed
    {
        get => Enchantments.Values.Count > 0
            ? Enchantments.Values.First().Value.Cursed
            : CurseType.None;
        set
        {
            Movable = value switch
            {
                < CurseType.RevealedCantUnEquip => true,
                CurseType.RevealedCantUnEquip => false,
            };

            foreach (var (_, val) in Enchantments.Values)
                val.Cursed = value;
        }
    }
    
    public override void OnAdded(IEntity parent)
    {
        base.OnAdded(parent);
            
        if (parent is Mobile mobile && Cursed == CurseType.Unrevealed)
        {
            Cursed = CurseType.RevealedCantUnEquip;
            mobile.FixedParticles(0x374A, 10, 15, 5028, EffectLayer.Waist);
            mobile.PlaySound(0x1E1);
            mobile.SendAsciiMessage(33,
                $"That item is cursed, and reveals itself to be a {SingleClickHandler.GetMagicItemName(this)}");    
        }
            
        Enchantments.FireHook(e => e.OnAdded(this, parent));
    }

    public override void OnRemoved(IEntity parent)
    {
        Enchantments.FireHook(e => e.OnRemoved(this, parent));
        base.OnRemoved(parent);
    }

    public override bool OnDragLift(Mobile from)
    {
        bool canLift = true;
        Enchantments.FireHook(e => e.OnBeforeRemoved(this, from, ref canLift));

        if (Cursed == CurseType.RevealedCantUnEquip && Parent is Mobile parent && parent == from)
            canLift = Movable = false;

        return canLift;
    }
}