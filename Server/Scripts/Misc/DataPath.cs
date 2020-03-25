using System;
using System.IO;
using Microsoft.Win32;
using Server;

namespace Server.Misc
{
    public class DataPath
    {
	/* CustomPath is a path, relative to the base directory of RunZH, where the server
	 * expects to find the UO data files.
	 * 
	 * We got rid of all the silly "locate your uo install" stuff because
	 * this is for servers.  If you are running this on your home desktop PC
	 * for reasons other than testing/development, you are bad and should feel bad.
	 * --sith
	 */

	private static string CustomPath = "/home/nathan/Code/zuluhotel/muls/";

	/* The following is a list of files which are required for proper execution:
	 *
	 * Multi.idx
	 * Multi.mul
	 * VerData.mul
	 * TileData.mul
	 * Map*.mul or Map*LegacyMUL.uop
	 * StaIdx*.mul
	 * Statics*.mul
	 * MapDif*.mul
	 * MapDifL*.mul
	 * StaDif*.mul
	 * StaDifL*.mul
	 * StaDifI*.mul
	 */

	public static void Configure()
	{
	    Core.DataDirectories.Add( CustomPath );
	}
    }
}
