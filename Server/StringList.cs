using System;
using System.IO;
using System.Text;
using System.Collections;

//ALL RIGHT THUMBS UP LET'S DO THIS
namespace Server
{
    public class StringEntry
    {
	private int m_Number;
	private string m_Text;

	public int Number{ get{ return m_Number; } }
	public string Text{ get{ return m_Text; } }

	public StringEntry( int number, string text )
	{
	    m_Number = number;
	    m_Text = text;
	}
    }
    
    public class StringList
    {
	private Hashtable m_Table;
	private StringEntry[] m_Entries;
	private string m_Language;

	public StringEntry[] Entries{ get{ return m_Entries; } }
	public Hashtable Table{ get{ return m_Table; } }
	public string Language{ get{ return m_Language; } }

	private static byte[] m_Buffer = new byte[1024];

	public StringList( string language )
	{
	    m_Language = language;
	    m_Table = new Hashtable();

	    string path = "/Users/nathan/Games/uoclient/ZuluHotel/Cliloc.enu";

	    if ( path == null )
	    {
		m_Entries = new StringEntry[0];
		return;
	    }

	    ArrayList list = new ArrayList();

	    using ( BinaryReader bin = new BinaryReader( new FileStream( path, FileMode.Open, FileAccess.Read, FileShare.Read ) ) )
	    {
		bin.ReadInt32();
		bin.ReadInt16();

		while ( bin.BaseStream.Length != bin.BaseStream.Position )
		{
		    int number = bin.ReadInt32();
		    bin.ReadByte();
		    int length = bin.ReadInt16();

		    if ( length > m_Buffer.Length )
			m_Buffer = new byte[(length + 1023) & ~1023];

		    bin.Read( m_Buffer, 0, length );
		    string text = Encoding.UTF8.GetString( m_Buffer, 0, length );

		    list.Add( new StringEntry( number, text ) );
		    m_Table[number] = text;
		}
	    }

	    m_Entries = (StringEntry[])list.ToArray( typeof( StringEntry ) );
	}
    }
}
