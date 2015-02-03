using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EttnaWebRelayApi.GameObjects;

namespace EttnaWebRelayApi.ResultObjects
{
	public class GetConnectedPlayersResult : BaseResult
	{
		public List<BasicPlayer> Players;


		public GetConnectedPlayersResult(string status, bool error)
			: base(status, error)
		{
			Players = new List<BasicPlayer>();
		}
		public GetConnectedPlayersResult(string status, bool error, List<BasicPlayer> playerList)
			: base(status, error)
		{
			Players = playerList;
		}

	}

	public class GetPlayerResult : BaseResult
	{
		BasicPlayer PlayerInfo { get; set; }

		public GetPlayerResult(string status, bool error, BasicPlayer player)
			: base(status, error)
		{
			PlayerInfo = player;
		}
	}
}
