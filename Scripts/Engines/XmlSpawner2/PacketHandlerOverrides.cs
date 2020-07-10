#define CLIENT6017

using System;
using System.IO;
using System.Text;
using Server;
using Server.Network;
using Server.Items;
using Server.Mobiles;


namespace Server.Engines.XmlSpawner2
{

    public class PacketHandlerOverrides
    {
        public static void Initialize()
        {
            //
            // this will replace the default packet handlers with XmlSpawner2 versions.
            // The delay call is to make sure they are assigned after the core default assignments.
            //
            // If you dont want these packet handler overrides to be applied, just comment them out here.
            //

            // This will replace the default packet handler for basebooks content change.  This allows the
            // use of the text entry book interface for editing spawner entries.
            // Regular BaseBooks will still call their default handlers for ContentChange and HeaderChange
            Timer.DelayCall(TimeSpan.Zero, ContentChangeOverride);

            // this replaces the default packet handler for Use requests.  Items and Mobiles will still 
            // behave exactly the same way, it simply adds a hook in to call the OnUse method for attachments
            // they might have.
            Timer.DelayCall( TimeSpan.Zero, UseReqOverride );
        }

        public static void ContentChangeOverride()
        {
            PacketHandlers.Register(0x66, 0, true, BaseEntryBook.ContentChange);
#if(CLIENT6017)
            PacketHandlers.Register6017(0x66, 0, true, BaseEntryBook.ContentChange);
#endif
        }

        public static void UseReqOverride()
        {
            PacketHandlers.Register(0x06, 5, true, XmlAttach.UseReq);
#if(CLIENT6017)
            PacketHandlers.Register6017(0x06, 5, true, XmlAttach.UseReq);
#endif
        }
    }
}