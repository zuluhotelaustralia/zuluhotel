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
		    case SpecName.Thief:
			name = "Thief";
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
		    return ( 1.0 + ((double)m_SpecLevel * _perLevel) ); 
		}
	    }
	}

        public static double GetBonusFor(Mobile m, SpecName name) {
            return m is PlayerMobile ?
                (m as PlayerMobile).Spec.GetBonusFor(name) :
                1.0;
        }
        
        public double GetBonusFor(SpecName name) {
            return SpecName == name ? Bonus : 1.0;
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

            Skills skills = m_Parent.Skills;
	    double total = (double)skills.Total;
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

	    double thiefSkills = skills.Hiding.Value +
		skills.Snooping.Value +
		skills.Stealing.Value +
		skills.Stealth.Value +
		skills.DetectHidden.Value +
		skills.RemoveTrap.Value +
		skills.Lockpicking.Value +
		skills.Poisoning.Value;
	    int thieflevel = GetSpecLevel(thiefSkills, SpecName.Thief);
	    if( thieflevel > 0 ){
		m_SpecName = SpecName.Thief;
		m_SpecLevel = thieflevel;
	    }
            
	    double bardSkills = skills.Begging.Value + 
		skills.Cartography.Value +
		skills.Discordance.Value +
		skills.Herding.Value + 
		skills.Musicianship.Value + 
		skills.Peacemaking.Value + 
		skills.Provocation.Value +
		skills.TasteID.Value;
	    int bardlevel = GetSpecLevel(bardSkills, SpecName.Bard);
	    if( bardlevel > 0 ){
		m_SpecName = SpecName.Bard;
		m_SpecLevel = bardlevel;
	    }
	    
	    double crafterSkills = skills.ArmsLore.Value + 
		skills.Blacksmith.Value +
		skills.Fletching.Value +
		skills.Carpentry.Value +
		skills.Lumberjacking.Value +
		skills.Mining.Value +
		skills.Tailoring.Value +
		skills.Tinkering.Value;
	    int crafterlevel = GetSpecLevel(crafterSkills, SpecName.Crafter);
	    if( crafterlevel > 0 ){
		m_SpecName = SpecName.Crafter;
		m_SpecLevel = crafterlevel;
	    }
	    
	    double mageSkills = skills.Alchemy.Value +
		skills.EvalInt.Value +
		skills.Inscribe.Value +
		skills.ItemID.Value +
		skills.Magery.Value +
		skills.Meditation.Value +
		skills.MagicResist.Value +
		skills.SpiritSpeak.Value;
	    int magelevel = GetSpecLevel(mageSkills, SpecName.Mage);
	    if( magelevel > 0 ){
		m_SpecName = SpecName.Mage;
		m_SpecLevel = magelevel;
	    }
	    
	    double rangerSkills = skills.AnimalLore.Value +
		skills.AnimalTaming.Value +
		skills.Camping.Value +
		skills.Cooking.Value +
		skills.Fishing.Value +
		skills.Tracking.Value +
		skills.Archery.Value +
		skills.Veterinary.Value +
		skills.Tactics.Value;
	    int rangerlevel = GetSpecLevel(rangerSkills, SpecName.Ranger);
	    if( rangerlevel > 0 ){
		m_SpecName = SpecName.Ranger;
		m_SpecLevel = rangerlevel;
	    }
	
	    double warriorSkills = skills.Anatomy.Value +
		skills.Fencing.Value +
		skills.Healing.Value +
		skills.Macing.Value +
		skills.Parry.Value +
		skills.Swords.Value +
		skills.Tactics.Value +
		skills.Wrestling.Value;   
	    int warriorlevel = GetSpecLevel(warriorSkills, SpecName.Warrior);
	    if( warriorlevel > 0 ){
		m_SpecName = SpecName.Warrior;
		m_SpecLevel = warriorlevel;
	    }

	    //idx:    0    1     2     3     4     5      6
	    //Min: [ 480, 600,  720,  840,  960,  1080, 1200 ]
	    //Max: [ 923, 1000, 1058, 1105, 1142, 1173, 1200 ]
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

	//idx:    0    1     2     3     4     5      6
	//Min: [ 480, 600,  720,  840,  960,  1080, 1200 ]
	//Max: [ 923, 1000, 1058, 1105, 1142, 1173, 1200 ]
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
