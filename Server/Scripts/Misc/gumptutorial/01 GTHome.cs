using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;

namespace Server.GumpTutorial
{
	public class GTHome : Gump
	{
		public static void Initialize()
		{
			CommandSystem.Register( "GTHome", AccessLevel.Player, new CommandEventHandler( GTHome_OnCommand ) );
		}

		[Usage( "GTHome" )]
		public static void GTHome_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new GTHome() );
		}

		public GTHome()
			: base( 0, 0 )
		{
			Closable=true;
			Disposable=true;
			Dragable=false;
			Resizable=false;
			AddPage(0);

			AddBackground(0, 0, 400, 300, 9270);
			AddHtml( 125, 10, 265, 280, "Welcome to A_Li_N's Gump Tutorial!  This tutorial will take you through all the steps that are needed to create your gump, from the very basic to as advanced as I can.  I have not learned everything about Gumps yet, so this tutorial does not cover everything.  This tutorial will have you look both at script files and in game so you can see what script causes what to happen in game.  I would also HIGHLY suggest you get and use GumpStudio.  This is the program I use to create all layouts for my gumps, and generate the basic code.  I will reference it a lot as we go through.  I am going to assume that you have a basic knowledge of scripting for RunUO.  Meaning that I am not going to go over includes, namespaces, classes, base, etc.  If you do not feel comfertable with these things, you might not want to take on the task of making a gump.  If you feel comfertable enough to deal with me not worrying about those aspects, please continue!    So let us get started!  Press any button to the left for the section you wish to review.", (bool)true, (bool)true);
			AddButton(12, 10, 2445, 2443, (int)Buttons.Basics, GumpButtonType.Reply, 0);
			AddButton(12, 35, 2445, 2443, (int)Buttons.Backgrounds, GumpButtonType.Reply, 0);
			AddButton(12, 60, 2445, 2443, (int)Buttons.Labels, GumpButtonType.Reply, 0);
			AddButton(12, 85, 2445, 2443, (int)Buttons.Images, GumpButtonType.Reply, 0);
			AddButton(12, 110, 2445, 2443, (int)Buttons.Alphas, GumpButtonType.Reply, 0);
			AddButton(12, 135, 2445, 2443, (int)Buttons.Buttons, GumpButtonType.Reply, 0);
			AddButton(12, 160, 2445, 2443, (int)Buttons.Switches, GumpButtonType.Reply, 0);
//			AddButton(12, 160, 2445, 2443, (int)Buttons.HTML, GumpButtonType.Reply, 0);
//			AddButton(12, 185, 2445, 2443, (int)Buttons.Text, GumpButtonType.Reply, 0);

			AddLabel(44, 10, 0, "Basics");
			AddLabel(25, 35, 0, "Backgrounds");
			AddLabel(44, 60, 0, "Labels");
			AddLabel(44, 85, 0, "Images");
			AddLabel(48, 110, 0, "Alphas");
			AddLabel(39, 135, 0, "Buttons");
			AddLabel(35, 160, 0, "Switches");
//			AddLabel(46, 160, 0, "HTML");
//			AddLabel(50, 185, 0, "Text");
		}

		public enum Buttons
		{
			Exit,
			Basics,
			Backgrounds,
			Labels,
			Images,
			Alphas,
			Buttons,
			Switches,
			HTML,
			Text,
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case (int)Buttons.Exit:
				{
					from.SendMessage( "Thank you for using A_Li_N's Gump Tutorial!" );
					break;
				}
				case (int)Buttons.Basics:
				{
					from.SendGump( new GTBasics() );
					break;
				}
				case (int)Buttons.Backgrounds:
				{
					from.SendGump( new GTBackgrounds() );
					break;
				}
				case (int)Buttons.Labels:
				{
					from.SendGump( new GTLabels() );
					break;
				}
				case (int)Buttons.Images:
				{
					from.SendGump( new GTImages() );
					break;
				}
				case (int)Buttons.Alphas:
				{
					from.SendGump( new GTAlphas() );
					break;
				}
				case (int)Buttons.Buttons:
				{
					from.SendGump( new GTButtons() );
					break;
				}
				case (int)Buttons.Switches:
				{
					from.SendGump( new GTSwitches() );
					break;
				}
			}
		}
	}
}
