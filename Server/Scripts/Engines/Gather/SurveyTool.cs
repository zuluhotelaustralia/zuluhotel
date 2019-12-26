using System;
using System.Collections;
using System.Collections.Generic;

using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targets;
using Server.Targeting;
using Server.Engines.Gather;

namespace Server.Items
{

    internal class SurveyReport {
	private string _res;
	private double _amount;

	public string Resource { get { return _res; } }
	public double Amount { get { return _amount; } }

	public SurveyReport( double a, string r ){
	    _res = r;
	    _amount = a;
	}
    }
    
    public class SurveyTool : Item
    {
	private List<SurveyReport> m_ReportList;
	private bool _Residuals;
	private bool _Unknowns;
	
	private DateTime _nextUse;
	
	public enum SurveyType {
	    Tree,
	    Terrain
	}
	
	[Constructable]
	public SurveyTool( ) : base( 0xF39 )
	{
	    m_ReportList = new List<SurveyReport>();
	    _nextUse = DateTime.Now;
	    Name = "a surveyor's tool";
	}

	public SurveyTool( Serial serial ) : base( serial )
	{
	    m_ReportList = new List<SurveyReport>();
	    _nextUse = DateTime.Now;
	    Name = "a surveyor's tool";
	}

	public override void OnDoubleClick( Mobile from ){
	    if( DateTime.Compare( DateTime.Now, _nextUse ) >= 0 ){
		from.Target = new InternalTarget( this );
	    }
	}

	public void MunchMunch( Mobile from ){
	    from.PlaySound( Utility.Random( 0x3A, 3 ) );
	    from.Emote("*Munch munch munch*");
	    
	    if ( from.Body.IsHuman && !from.Mounted ){
		from.Animate( 34, 5, 1, true, false, 0 );
	    }
	}

	//generate the actual report here
	public void WriteReport( Mobile from ){
	    // foreach report in m_Reportlist
	    // pretty-print report.Resource and report.Amount
	    // Amount represents the adjusted abundance of the node based on
	    //   distance and skill
	    // they can always choose to mark a rune later, fuck the x/y coords

	    Item pen = from.Backpack.FindItemByType( typeof( PenAndInk ), true );

	    if( pen == null ){
		from.SendMessage("Without a pen-and-ink you cannot write your field report!");
		return;
	    }

	    from.SendMessage("Select an empty book for your field report.");

	    from.Target = new SimpleTarget( 4, false, TargetFlags.None, (Mobile from, object target) => {
		    if( !(target is BaseBook) ){
			from.SendMessage("You must select a book!");
			return;
		    }

		    BaseBook book = (BaseBook)target;

		    if( !book.Writable ){
			from.SendMessage("You cannot write in that book.");
			return;
		    }

		    from.PlaySound(0x249);

		    new SimpleTimeout( new TimeSpan( 0, 0, 5), () => {
			    if( !pen.IsAccessibleTo(from) ){
				from.SendMessage("You require a pen.");
				return;
			    }

			    if( !book.IsAccessibleTo(from) ) {
				from.SendMessage("You require a book.");
				return;
			    }

			    book.Author = from.Name;
			    book.Title = "Field Report";

			    int i = 0;

			    foreach( SurveyReport rpt in m_ReportList ){
				int percentage = (int)(rpt.Amount * 100);
				book.Pages[i].Lines = new string[]{
				    "-" + rpt.Resource + "-",
				    "Surveyed rate: " + percentage + "%"
				};

				i++;

				if( i >= book.PagesCount ){
				    from.SendMessage("You've run out of pages in your book!");
				    break;
				}
			    }

			    if(_Residuals && (i < book.PagesCount) ){
				book.Pages[i].Lines = new string[] {
				    "-Others-",
				    "There are various other",
				    "materials present in",
				    "trace amounts."
				};

				i++;
			    }
			    
			    if(_Unknowns && (i < book.PagesCount) ){
				book.Pages[i].Lines = new string[] {
				    "-Unknown-",
				    "I detect some other",
				    "materials that I cannot",
				    "quite identify."
				};

				i++;
			    }

			    from.SendMessage("You finish compiling your field report.");
			}).Start();
		});
       				
	}
	    
	//actually take the sample of what's there
	public void Sample( Point3D loc, SurveyType t, Mobile from ){
	    // determine what was clciked (tree or terrain)
	    m_ReportList.Clear();
	    _Residuals = false;
	    _Unknowns = false;

	    MunchMunch( from );
	    from.SendMessage( "You take a sample...");
	    
	    GatherSystem sys;
	    if( t == SurveyType.Tree ){
		sys = Server.Engines.Gather.Lumberjacking.System;
	    }
	    else {
		sys = Server.Engines.Gather.Mining.System;
	    }
	    	    
	    foreach( GatherNode node in sys.Nodes ){
		int dx = Math.Abs( loc.X - node.X );
		int dy = Math.Abs( loc.Y - node.Y );
		
		double dxsq = Math.Pow( (double)dx, 2.0 );
		double dysq = Math.Pow( (double)dy, 2.0 );
		
		double dist = Math.Sqrt( dxsq + dysq );
		double a = ( node.Abundance * node.Difficulty ) / dist;

		Skill userskill = from.Skills.TasteID;
		double chance = (130.0 - userskill.Value)/userskill.Value;

		if( chance < 0.01 ) {
		    chance = 0.01;
		}
		if( chance >= 1.0 ){
		    chance = 0.98;
		}
		
		if( Server.Misc.SkillCheck.CheckSkill(from, userskill, null, chance) ){
		    if( node.MinSkill >=  userskill.Value ){
			if( a >= 0.01 ){
			    CraftResource cr = CraftResources.GetFromType( node.Resource );
			    CraftResourceInfo cri = CraftResources.GetInfo( cr );
			    m_ReportList.Add( new SurveyReport( a, cri.Name ) );
			}
			else {
			    //"other trace elements"
			    _Residuals = true;
			}
		    }
		    else{
			//other types you can't identify
			_Unknowns = true;
		    }
		}
	    }

	    WriteReport( from );
	}

	public override void Serialize( GenericWriter writer )
	{
	    base.Serialize( writer );
	    
	    writer.Write( (int) 0 ); // version
	}

	public override void Deserialize( GenericReader reader )
	{
	    base.Deserialize( reader );

	    int version = reader.ReadInt();
	}

	public class InternalTarget : Target {
	    private SurveyTool m_Tool;
	    private Lumberjacking m_LumberjackingSystem;
	    private Mining m_MiningSystem;

	    public InternalTarget( SurveyTool tool ) : base( 4, true, TargetFlags.None ) {
		m_Tool = tool;
		m_LumberjackingSystem = Server.Engines.Gather.Lumberjacking.System;
		m_MiningSystem = Server.Engines.Gather.Mining.System;
	    }

	    protected override void OnTarget( Mobile from, object targeted ){
		int tileID = m_LumberjackingSystem.GetTileID( targeted );

		if( m_LumberjackingSystem.Validate( tileID ) ){
		    m_Tool.Sample( from.Location, SurveyType.Tree, from );
		}
		else if( m_MiningSystem.ValidateRock( tileID ) ){
		    m_Tool.Sample( from.Location, SurveyType.Terrain, from );
		}
		else {
		    from.SendMessage("You must target rock or trees suitable for gathering resources."); //TODO cliloc this
		}
	    }
	}
    }
}
