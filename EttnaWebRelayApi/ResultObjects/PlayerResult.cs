using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EttnaWebRelayApi.GameObjects;

namespace EttnaWebRelayApi.ResultObjects
{
	public class GetPlayersResult : BaseResult
	{
		public List<Player> Players { get; set; }


		public GetPlayersResult(string status, bool error)
			: base(status, error)
		{
			Players = new List<Player>();
		}
		public GetPlayersResult(string status, bool error, List<Player> playerList)
			: base(status, error)
		{
			Players = playerList;
		}

	}
}
