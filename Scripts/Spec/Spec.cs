using System;
using Server;
using System.Text;
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
	private PlayerMobile m_Parent; //store the parent obj
	private Skills m_ClassSkills;

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
		    return ( 1.0 + ((double)m_SpecLevel * 0.10) ); // +10% per level
		}
	    }
	}
    
	//pulled this BS from zh scandinavia
	private const int m_SKILLPOINTSPERLEVEL = 120;
	private const int m_SKILLBASE = 480;
	private const double m_PERCENTPERLEVEL = 0.08;
	private const double m_PERCENTBASE = 0.52;

	//constructor needs a reference to the parent playermobile obj.
	public Spec(PlayerMobile parent)
	{
	    m_Parent = parent;
	    m_ClassSkills = m_Parent.Skills;
	    m_SpecName = SpecName.None;
	    m_SpecLevel = 0;
	}

	public void ComputeSpec()
	{
	    //keep in mind this has to run every time anyone swings a weapon or uses a skill so it needs to avoid looping if possible
	    //also note that total is in "x10" notation, whereas skillname.value is in regular decimal form

	    double total = m_ClassSkills.Total / 10;

	    //thief TODO incomplete.
	    double thiefSkills = m_ClassSkills.Hiding.Value;

	    //bard
	    double bardSkills = m_ClassSkills.Begging.Value + 
		m_ClassSkills.Cartography.Value +
		m_ClassSkills.Discordance.Value +
		m_ClassSkills.Herding.Value + 
		m_ClassSkills.Musicianship.Value + 
		m_ClassSkills.Peacemaking.Value + 
		m_ClassSkills.Provocation.Value +
		m_ClassSkills.TasteID.Value;

	    //crafter
	    double crafterSkills = m_ClassSkills.ArmsLore.Value + 
		m_ClassSkills.Blacksmith.Value +
		m_ClassSkills.Fletching.Value +
		m_ClassSkills.Carpentry.Value +
		m_ClassSkills.Lumberjacking.Value +
		m_ClassSkills.Mining.Value +
		m_ClassSkills.Tailoring.Value +
		m_ClassSkills.Tinkering.Value;

	    //mage
	    double mageSkills = m_ClassSkills.Alchemy.Value +
		m_ClassSkills.EvalInt.Value +
		m_ClassSkills.Inscribe.Value +
		m_ClassSkills.ItemID.Value +
		m_ClassSkills.Magery.Value +
		m_ClassSkills.Meditation.Value +
		m_ClassSkills.MagicResist.Value +
		m_ClassSkills.SpiritSpeak.Value;

	    //ranger
	    double rangerSkills = m_ClassSkills.AnimalLore.Value +
		m_ClassSkills.AnimalTaming.Value +
		m_ClassSkills.Camping.Value +
		m_ClassSkills.Cooking.Value +
		m_ClassSkills.Fishing.Value +
		m_ClassSkills.Tracking.Value +
		m_ClassSkills.Veterinary.Value;

	    //warrior
	    double warriorSkills = m_ClassSkills.Anatomy.Value +
		m_ClassSkills.Fencing.Value +
		m_ClassSkills.Healing.Value +
		m_ClassSkills.Macing.Value +
		m_ClassSkills.Parry.Value +
		m_ClassSkills.Swords.Value +
		m_ClassSkills.Tactics.Value +
		m_ClassSkills.Wrestling.Value;

	    // tl;dr dean parker is a shitty fucking coder
	    // extremely messy include/skills.inc contains horrifiying isfromthatclasse() function that can be significantly
	    //reduced if you take the time to actually figure out wtf is going on

	    // floor( (specskills - 480) / 120 ) gives level
	    // enforce specskills/total < 0.6 + 0.08*level to prevent excess offclass skills
	    // check if they are pp
	    // enforce level >= 0
	    // if less than 600 skill points they cannot be spec

	    if(total < 600.0)
	    {
		m_SpecName = SpecName.None;
		m_SpecLevel = 0;
		return;
	    }
      
	    if(total>=6370.0)
	    {
		m_SpecLevel = 3;
		m_SpecName = SpecName.Powerplayer;
		return;
	    }
	    else
	    {
		if(total>=5145.0)
		{
		    m_SpecLevel = 2;
		    m_SpecName = SpecName.Powerplayer;
		    return;
		}
		else
		{
		    if(total>=3920.0)
		    {
			m_SpecLevel = 1;
			m_SpecName = SpecName.Powerplayer;
			return;
		    }
		}
	    }

	    //bard calcs
	    int bardLevel = ( (int)bardSkills - m_SKILLBASE) / m_SKILLPOINTSPERLEVEL;
	    if(bardLevel < 0)
	    {
		bardLevel = 0;
	    }
	    if( (bardSkills/total) < (m_PERCENTBASE + m_PERCENTPERLEVEL * (double)bardLevel ))
	    {
		//on-spec skills must comprise 60% of total at level1, and an additional 8% for each level after
		// if this evals true, then they have too many off-spec skills (on-spec proportion is too small)
		bardLevel = 0; //should this be bardlevel--; ?
	    }
	    if(bardLevel > 0)
	    {
		m_SpecLevel = bardLevel;
		m_SpecName = SpecName.Bard;
	    }
      
	    //crafter cals
	    int crafterLevel = ( (int)crafterSkills - m_SKILLBASE) / m_SKILLPOINTSPERLEVEL;
	    if(crafterLevel < 0)
	    {
		crafterLevel = 0;
	    }
	    if( (crafterSkills/total) < (m_PERCENTBASE + m_PERCENTPERLEVEL * (double)crafterLevel ))
	    {
		//on-spec skills must comprise 60% of total at level1, and an additional 8% for each level after
		// if this evals true, then they have too many off-spec skills (on-spec proportion is too small)
		crafterLevel = 0; //should this be crafterlevel--; ?
	    }
	    if(crafterLevel > 0)
	    {
		m_SpecLevel = crafterLevel;
		m_SpecName = SpecName.Crafter;
	    }
      
	    //mage calcs
	    int mageLevel = ( (int)mageSkills - m_SKILLBASE) / m_SKILLPOINTSPERLEVEL;
	    if(mageLevel < 0)
	    {
		mageLevel = 0;
	    }
	    if( (mageSkills/total) < (m_PERCENTBASE + m_PERCENTPERLEVEL * (double)mageLevel ))
	    {
		//on-spec skills must comprise 60% of total at level1, and an additional 8% for each level after
		// if this evals true, then they have too many off-spec skills (on-spec proportion is too small)
		mageLevel = 0; //should this be magelevel--; ?
	    }
	    if(mageLevel > 0)
	    {
		m_SpecLevel = mageLevel;
		m_SpecName = SpecName.Mage;
	    }
      
	    //ranger calcs
	    int rangerLevel = ( (int)rangerSkills - m_SKILLBASE) / m_SKILLPOINTSPERLEVEL;
	    if(rangerLevel < 0)
	    {
		rangerLevel = 0;
	    }
	    if( (rangerSkills/total) < (m_PERCENTBASE + m_PERCENTPERLEVEL * (double)rangerLevel ))
	    {
		//on-spec skills must comprise 60% of total at level1, and an additional 8% for each level after
		// if this evals true, then they have too many off-spec skills (on-spec proportion is too small)
		rangerLevel = 0; //should this be rangerlevel--; ?
	    }
	    if(rangerLevel > 0)
	    {
		m_SpecLevel = rangerLevel;
		m_SpecName = SpecName.Ranger;
	    }
      
	    //thief calcs
	    int thiefLevel = ( (int)thiefSkills - m_SKILLBASE) / m_SKILLPOINTSPERLEVEL;
	    if(thiefLevel < 0)
	    {
		thiefLevel = 0;
	    }
	    if( (thiefSkills/total) < (m_PERCENTBASE + m_PERCENTPERLEVEL * (double)thiefLevel ))
	    {
		//on-spec skills must comprise 60% of total at level1, and an additional 8% for each level after
		// if this evals true, then they have too many off-spec skills (on-spec proportion is too small)
		thiefLevel = 0; //should this be thieflevel--; ?
	    }
	    if(thiefLevel > 0)
	    {
		m_SpecLevel = thiefLevel;
		m_SpecName = SpecName.Thief;
	    }
      
	    //warrior calcs
	    int warriorLevel = ( (int)warriorSkills - m_SKILLBASE) / m_SKILLPOINTSPERLEVEL;
	    if(warriorLevel < 0)
	    {
		warriorLevel = 0;
	    }
	    if( (warriorSkills/total) < (m_PERCENTBASE + m_PERCENTPERLEVEL * (double)warriorLevel ))
	    {
		//on-spec skills must comprise 60% of total at level1, and an additional 8% for each level after
		// if this evals true, then they have too many off-spec skills (on-spec proportion is too small)
		warriorLevel = 0; //should this be warriorlevel--; ?
	    }
	    if(warriorLevel > 0)
	    {
		m_SpecLevel = warriorLevel;
		m_SpecName = SpecName.Warrior;
	    }
	} //computespec

    } //spec
}
