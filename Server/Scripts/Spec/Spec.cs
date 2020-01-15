using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Server;
using Server.Accounting;
using Server.Targeting;
using Server.Commands;
using Server.Spells;

namespace Server.Mobiles
{
    public enum SpecName
    {
	None,
	Bard,
	Crafter,
	Mage,
	Ranger,
	Thief,
	Warrior,
	Powerplayer
    }

    [PropertyObject]
    public class Spec
    {
	public static void Initialize(){
	    CommandSystem.Register( "spec", AccessLevel.Player, new CommandEventHandler( Spec_OnCommand ) );
	    CommandSystem.Register( "showclasse", AccessLevel.Player, new CommandEventHandler( Spec_OnCommand ) );

	    SetMaximums();
	}

	private const double _perLevel = 0.1; //10% per level

	private class InternalTarget : Target {
	    public InternalTarget() : base( 12, false, TargetFlags.None ){}

	    protected override void OnTarget( Mobile from, object targ ){
		if( targ is PlayerMobile ){
		    PlayerMobile pm = targ as PlayerMobile;
		    pm.Spec.ComputeSpec();
		    from.SendMessage("{0}: {1}, level {2}", pm.Name, pm.Spec.SpecName, pm.Spec.SpecLevel);
		}
		else {
		    from.SendMessage("Can only target players.");

		}
	    }
	}

	public static void Spec_OnCommand( CommandEventArgs e ){
	    PlayerMobile pm = e.Mobile as PlayerMobile;

	    if( pm.AccessLevel >= AccessLevel.Counselor ){
		pm.Target = new InternalTarget();
		return;
	    }
	    
	    pm.Spec.ComputeSpec();

	    if( pm.Spec.SpecName == SpecName.None ){
		pm.SendMessage("You aren't a member of any particular Specialization."); //cliloc this
	    }
	    else{
		//there's a more elegant way to do this with extension methods but I'm in a hurry --sith TODO
		string name = "";
		switch( pm.Spec.SpecName ){
		    case SpecName.Bard:
			name = "Bard";
			break;
		    case SpecName.Crafter:
			name = "Crafter";
			break;
		    case SpecName.Mage:
			name = "Mage";
			break;
		    case SpecName.Powerplayer:
			name = "Power Player";
			break;
		    case SpecName.Ranger:
			name = "Ranger";
			break;
		    case SpecName.Warrior:
			name = "Warrior";
			break;
		    default:
			pm.SendMessage("There appears to be an error in Spec.cs.  Please inform the server staff.");
			Console.WriteLine("[Spec] This should never happen!!");
			return;
		}

		pm.SendMessage("You are a qualified Spec {0} {1}.", pm.Spec.SpecLevel, name);
	    }
	}
	
	private PlayerMobile m_Parent; //store the parent obj
	private Skills m_MySkills;

	private int m_SpecLevel;
	[CommandProperty(AccessLevel.Counselor)]
	public int SpecLevel
	{
	    get
	    {
		return m_SpecLevel;
	    }
	}
    
	private SpecName m_SpecName;
	[CommandProperty(AccessLevel.Counselor)]
	public SpecName SpecName
	{
	    get
	    {
		return m_SpecName;
	    }
	}
    
	//ZHS had this set to 20% per level, let's go with 10 or 5% for better balance
	[CommandProperty(AccessLevel.Counselor)]
	public double Bonus
	{
	    get
	    {
		if (m_SpecName == SpecName.Powerplayer || m_SpecName == SpecName.None)
		{
		    return 1.0;
		}
		else
		{
		    return ( 1.0 + ((double)m_SpecLevel * _perLevel) ); // +5% per level
		}
	    }
	}
    
	//reference original ZH Canada (ZH3) release
	private const double m_ClassPointsPerLevel = 120;
	private const double m_SkillBase = 480;
	private const double m_PercentPerLevel = 0.08;
	private const double m_PercentBase = 0.52;

	private static double[] m_MinSkills;
	private static double[] m_MaxSkills;
	    
	public void ComputeSpec()
	{
	    m_SpecName = SpecName.None;
	    m_SpecLevel = 0;
		
	    //minimum on-class skill points per level are 600 for level 1, then += 120 pts per level
	    //maximum total skill points per level are 1000 for level 1, then ( 0.52 + 0.08(level) ) * minskill
	    // if you fail the maxskill check then you are still eligible for the next spec down.
	    // average on-class skill must be satisfied for spec.

	    double total = (double)m_MySkills.Total;
	    total *= 0.1;

	    if( total < 600.0 ){
		return;
	    }

	    if( total >= 3920.0 ){
		m_SpecName = SpecName.Powerplayer;
		m_SpecLevel = 1;
		
		if( total >= 5145 ){
		    m_SpecLevel = 2;
		    
		    if( total >= 6370 ){
			m_SpecLevel = 3;
		    }
		}
		//we're a pp so:
		return;
	    }

	    int maxlevel = GetMaxLevel( total );
	    if ( maxlevel > 6 ){
		maxlevel = 6;
	    }

	    double thiefSkills = m_MySkills.Hiding.Value +
		m_MySkills.Snooping.Value +
		m_MySkills.Stealing.Value +
		m_MySkills.Stealth.Value +
		m_MySkills.DetectHidden.Value +
		m_MySkills.RemoveTrap.Value +
		m_MySkills.Lockpicking.Value +
		m_MySkills.Poisoning.Value;
	    int thieflevel = GetSpecLevel(thiefSkills, SpecName.Thief);
	    if( thieflevel > 0 ){
		m_SpecName = SpecName.Thief;
		m_SpecLevel = thieflevel;
	    }
            
	    double bardSkills = m_MySkills.Begging.Value + 
		m_MySkills.Cartography.Value +
		m_MySkills.Discordance.Value +
		m_MySkills.Herding.Value + 
		m_MySkills.Musicianship.Value + 
		m_MySkills.Peacemaking.Value + 
		m_MySkills.Provocation.Value +
		m_MySkills.TasteID.Value;
	    int bardlevel = GetSpecLevel(bardSkills, SpecName.Bard);
	    if( bardlevel > 0 ){
		m_SpecName = SpecName.Bard;
		m_SpecLevel = bardlevel;
	    }
	    
	    double crafterSkills = m_MySkills.ArmsLore.Value + 
		m_MySkills.Blacksmith.Value +
		m_MySkills.Fletching.Value +
		m_MySkills.Carpentry.Value +
		m_MySkills.Lumberjacking.Value +
		m_MySkills.Mining.Value +
		m_MySkills.Tailoring.Value +
		m_MySkills.Tinkering.Value;
	    int crafterlevel = GetSpecLevel(crafterSkills, SpecName.Crafter);
	    if( crafterlevel > 0 ){
		m_SpecName = SpecName.Crafter;
		m_SpecLevel = crafterlevel;
	    }
	    
	    double mageSkills = m_MySkills.Alchemy.Value +
		m_MySkills.EvalInt.Value +
		m_MySkills.Inscribe.Value +
		m_MySkills.ItemID.Value +
		m_MySkills.Magery.Value +
		m_MySkills.Meditation.Value +
		m_MySkills.MagicResist.Value +
		m_MySkills.SpiritSpeak.Value;
	    int magelevel = GetSpecLevel(mageSkills, SpecName.Mage);
	    if( magelevel > 0 ){
		m_SpecName = SpecName.Mage;
		m_SpecLevel = magelevel;
	    }
	    
	    double rangerSkills = m_MySkills.AnimalLore.Value +
		m_MySkills.AnimalTaming.Value +
		m_MySkills.Camping.Value +
		m_MySkills.Cooking.Value +
		m_MySkills.Fishing.Value +
		m_MySkills.Tracking.Value +
		m_MySkills.Archery.Value +
		m_MySkills.Veterinary.Value +
		m_MySkills.Tactics.Value;
	    int rangerlevel = GetSpecLevel(rangerSkills, SpecName.Ranger);
	    if( rangerlevel > 0 ){
		m_SpecName = SpecName.Ranger;
		m_SpecLevel = rangerlevel;
	    }
	
	    double warriorSkills = m_MySkills.Anatomy.Value +
		m_MySkills.Fencing.Value +
		m_MySkills.Healing.Value +
		m_MySkills.Macing.Value +
		m_MySkills.Parry.Value +
		m_MySkills.Swords.Value +
		m_MySkills.Tactics.Value +
		m_MySkills.Wrestling.Value;   
	    int warriorlevel = GetSpecLevel(warriorSkills, SpecName.Warrior);
	    if( warriorlevel > 0 ){
		m_SpecName = SpecName.Warrior;
		m_SpecLevel = warriorlevel;
	    }
	    
	    
	    for( int i=maxlevel; i>=0; i-- ){
		if( total > m_MaxSkills[i] ){
		    maxlevel--;
		}
		else {
		    break;
		}
	    }

	    if( m_SpecLevel > maxlevel ) {
		m_SpecLevel = maxlevel;
	    }
	    if( m_SpecLevel <= 0 ){
		m_SpecLevel = 0;
		m_SpecName = SpecName.None;
	    }
	    
	}

	private double AvgSkill( double onspec, SpecName sn ){
	    if( sn == SpecName.Ranger ){
		return onspec * 8 / 9;
	    }
	    else {
		return onspec;
	    }
	}
	    
	private int GetSpecLevel( double onspec, SpecName sn ){
	    double averaged = AvgSkill( onspec, sn );

	    if( averaged >= m_MinSkills[6] ){
		return 6;
	    }

	    if( averaged >= m_MinSkills[5] ){
		return 5;
	    }
	    if( averaged >= m_MinSkills[4] ){
		return 4;
	    }
	    if( averaged >= m_MinSkills[3] ){
		return 3;
	    }
	    if( averaged >= m_MinSkills[2] ){
		return 2;
	    }
	    if( averaged >= m_MinSkills[1] ){
		return 1;
	    }

	    return 0;
	}
	
	private int GetMaxLevel( double total ){
	    int num = m_MaxSkills.Length;
	    num--;
	    
	    for( int i=num; i>=0; i-- ){
		if( m_MaxSkills[i] >= total ){
		    return i;
		}
	    }

	    return 0;
	}
	
	private static void SetMaximums() {
	    // max skill = minskill * (0.52 + 0.08*level)

	    m_MinSkills = new double[7];

	    for( int i=0; i<7; i++ ){
		m_MinSkills[i] = m_SkillBase + ( m_ClassPointsPerLevel * (double)i );
	    }
	    
	    m_MaxSkills = new double[7];

	    for( int i=0; i<7; i++ ){
	        m_MaxSkills[i] = Math.Floor( m_MinSkills[i] / ( m_PercentBase + ( m_PercentPerLevel * (double)i )) );
	    }

	    Console.WriteLine("Min: [ {0}, {1}, {2}, {3}, {4}, {5}, {6} ]", m_MinSkills[0], m_MinSkills[1], m_MinSkills[2], m_MinSkills[3], m_MinSkills[4], m_MinSkills[5], m_MinSkills[6]);
	    Console.WriteLine("Max: [ {0}, {1}, {2}, {3}, {4}, {5}, {6} ]", m_MaxSkills[0], m_MaxSkills[1], m_MaxSkills[2], m_MaxSkills[3], m_MaxSkills[4], m_MaxSkills[5], m_MinSkills[6]);	    
	}	    
	    
	//constructor needs a reference to the parent playermobile obj.
	public Spec(PlayerMobile parent)
	{
	    m_Parent = parent;
	    m_MySkills = m_Parent.Skills;
	    m_SpecName = SpecName.None;
	    m_SpecLevel = 0;
	}

	public bool IsSkillOnSpec( SkillName sn ) {
	    switch( m_SpecName ) {
		case SpecName.None:
		    {
			return false;
		    }
		case SpecName.Powerplayer:
		    {
			return false;
		    }
		case SpecName.Warrior:
		    {
			if ( sn == SkillName.Wrestling ||
			     sn == SkillName.Tactics ||
			     sn == SkillName.Healing ||
			     sn == SkillName.Anatomy ||
			     sn == SkillName.Swords ||
			     sn == SkillName.Macing ||
			     sn == SkillName.Fencing ||
			     sn == SkillName.Parry ) {
			    return true;
			}
			else {
			    return false;
			}
		    }
		case SpecName.Ranger:
		    {
			if ( sn == SkillName.Tracking ||
			     sn == SkillName.Archery ||
			     sn == SkillName.AnimalLore ||
			     sn == SkillName.Veterinary ||
			     sn == SkillName.AnimalTaming ||
			     sn == SkillName.Fishing ||
			     sn == SkillName.Camping ||
			     sn == SkillName.Cooking ||
			     sn == SkillName.Tactics ) {
			    return true;
			}
			else {
			    return false;
			}
		    }
		case SpecName.Mage:
		    {
			if ( sn == SkillName.Alchemy ||
			     sn == SkillName.ItemID ||
			     sn == SkillName.EvalInt ||
			     sn == SkillName.Inscribe ||
			     sn == SkillName.MagicResist ||
			     sn == SkillName.Meditation ||
			     sn == SkillName.Magery ||
			     sn == SkillName.SpiritSpeak ) {
			    return true;
			}
			else {
			    return false;
			}
		    }
		case SpecName.Crafter:
		    {
			if ( sn == SkillName.Tinkering ||
			     sn == SkillName.ArmsLore ||
			     sn == SkillName.Fletching ||
			     sn == SkillName.Tailoring ||
			     sn == SkillName.Mining ||
			     sn == SkillName.Lumberjacking ||
			     sn == SkillName.Carpentry ||
			     sn == SkillName.Blacksmith ) {
			    return true;
			}
			else {
			    return false;
			}
		    }
		case SpecName.Thief:
		    {
			if ( sn == SkillName.Hiding ||
			     sn == SkillName.Stealth ||
			     sn == SkillName.Stealing ||
			     sn == SkillName.DetectHidden ||
			     sn == SkillName.RemoveTrap ||
			     sn == SkillName.Poisoning ||
			     sn == SkillName.Lockpicking ||
			     sn == SkillName.Snooping ) {
			    return true;
			}
			else {
			    return false;
			}
		    }
		case SpecName.Bard:
		    {
			if ( sn == SkillName.Provocation ||
			     sn == SkillName.Musicianship ||
			     sn == SkillName.Herding ||
			     sn == SkillName.Discordance ||
			     sn == SkillName.TasteID ||
			     sn == SkillName.Peacemaking ||
			     sn == SkillName.Cartography ||
			     sn == SkillName.Begging ) {
			    return true;
			}
			else {
			    return false;
			}
		    }
		default:
		    {
			return false;
		    }
	    }
	}
    } //spec
}
