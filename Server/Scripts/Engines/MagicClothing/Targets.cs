using System;

using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server{

    public class SetSkillmodTarget : Target
    {
	private SkillName _sn;
	private double _amt;
	
	public SetSkillmodTarget( SkillName sn, double amount ) : base( -1, false, TargetFlags.None )
	{
	    _sn = sn;
	    _amt = amount;
	}

	protected override void OnTarget( Mobile from, object targeted ) {
	    if ( targeted is BaseClothing ){
		BaseClothing clothes = targeted as BaseClothing;
		clothes.ZuluSkillMods.SetMod( _sn, _amt);
	    }
	    else if( targeted is BaseJewel ){
		BaseJewel jewel = targeted as BaseJewel;
		jewel.ZuluSkillMods.SetMod( _sn, _amt );
	    }
	    else{
		from.SendMessage("Must target BaseClothing or BaseJewel");
	    }
	}
    }

    public class GetSkillmodTarget : Target {
	public GetSkillmodTarget( ) : base( -1, false, TargetFlags.None ){
	}

	protected override void OnTarget( Mobile from, object targeted ){
	    SkillName sn;
	    double amt;
	    	    
	    if ( targeted is BaseClothing ){
		BaseClothing clothes = targeted as BaseClothing;
		sn = clothes.ZuluSkillMods.Mod.Skill;
		amt = clothes.ZuluSkillMods.Mod.Value;
	    }
	    else if( targeted is BaseJewel ){
		BaseJewel jewel = targeted as BaseJewel;
		sn = jewel.ZuluSkillMods.Mod.Skill;
		amt = jewel.ZuluSkillMods.Mod.Value;
	    }
	    else{
		from.SendMessage("Must target BaseClothing or BaseJewel");
		return;
	    }

	    from.SendMessage("{0}: {1}, {2}", targeted, sn, amt );
	}
    }
}
