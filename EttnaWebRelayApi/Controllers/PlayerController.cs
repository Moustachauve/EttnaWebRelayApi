using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using EttnaWebRelayApi.GameObjects;
using EttnaWebRelayApi.ResultObjects;
using SEModAPIInternal.API.Common;
using SEModAPIInternal.API.Entity;
using SEModAPIInternal.API.Entity.Sector;
using SEModAPIInternal.API.Entity.Sector.SectorObject;
using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid;
using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid.CubeBlock;

namespace EttnaWebRelayApi.Controllers
{
	public class PlayerController : ApiController
	{
		[HttpGet]
		public GetPlayersResult GetConnectedPlayers()
		{
			var players = new List<Player>();
			List<ulong> connectedPlayers = PlayerManager.Instance.ConnectedPlayers;

			Log.ConsoleAndFile(string.Format("Requesting connected players ({0})", connectedPlayers.Count));

			foreach (ulong remoteUserId in connectedPlayers)
				players.Add(new Player(remoteUserId, PlayerMap.Instance.GetPlayerNameFromSteamId(remoteUserId)));

			return new GetPlayersResult(string.Format("{0} player(s) online", players.Count), false, players);
		}

	}
}
