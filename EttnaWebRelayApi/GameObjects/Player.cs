using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EttnaWebRelayApi.GameObjects
{
	/// <summary>
	/// Basic information about a player
	/// </summary>
	public class BasicPlayer
	{
		/// <summary>
		/// Steam ID
		/// </summary>
		public ulong SteamID { get; set; }

		/// <summary>
		/// Character entity ID
		/// </summary>
		public long EntityID { get; set; }

		/// <summary>
		/// Username
		/// </summary>
		public string Name { get; set; }

		public BasicPlayer(ulong steamID, long entityID, string name)
		{
			SteamID = steamID;
			EntityID = entityID;
			Name = name;
		}
	}

	//TODO: Finish this class
	/// <summary>
	/// Detailed information about a player
	/// </summary>
	public class PlayerInfo : BasicPlayer
	{
		public PlayerInfo(ulong steamID, long entityID, string name)
			: base(steamID, entityID, name)
		{

		}
	}
}
