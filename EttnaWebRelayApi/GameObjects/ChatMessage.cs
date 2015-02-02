using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EttnaWebRelayApi.GameObjects
{
	public class ChatMessage
	{
		public int Index { get; set; }
		public ulong SteamID { get; set; }
		public string Name { get; set; }
		public string Message { get; set; }

		public ChatMessage(int index, ulong steamID, string name, string message)
		{
			Index = index;
			SteamID = steamID;
			Name = name;
			Message = message;
		}
	}
}
