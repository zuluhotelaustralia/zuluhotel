namespace Server.Chat
{
    public class ChatSystem
	{
		public static void Initialize()
		{
			EventSink.ChatRequest += EventSink_ChatRequest;
		}

		private static void EventSink_ChatRequest( Mobile mobile )
		{
      mobile.SendMessage( "Chat is not currently supported." );
		}
	}
}
