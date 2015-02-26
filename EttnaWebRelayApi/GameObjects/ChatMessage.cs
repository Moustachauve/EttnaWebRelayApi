using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EttnaWebRelayApi.GameObjects
{
	public class ChatMessage
	{
		public int MessageID { get; set; }
		public ulong SteamID { get; set; }
		public string Name { get; set; }
		public string Message { get; set; }

		public ChatMessage(int messageID, ulong steamID, string name, string message)
		{
			MessageID = messageID;
			SteamID = steamID;
			Name = name;
			Message = message;
		}
	}
}
