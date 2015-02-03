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
		public GetConnectedPlayersResult GetConnectedPlayers()
		{
			var players = new List<BasicPlayer>();
			List<ulong> connectedPlayers = PlayerManager.Instance.ConnectedPlayers;

			Log.ConsoleAndFile(string.Format("Requesting connected players ({0})", connectedPlayers.Count));

			foreach (ulong steamID in connectedPlayers)
			{
				string userName = PlayerMap.Instance.GetPlayerNameFromSteamId(steamID);
				long entityID = PlayerMap.Instance.GetPlayerEntityId(steamID);
				players.Add(new BasicPlayer(steamID, entityID, userName));
			}


			return new GetConnectedPlayersResult(string.Format("{0} player(s) online", players.Count), false, players);
		}

		//TODO: Finish
		[HttpGet]
		public CharacterEntity GetPlayer(ulong steamID)
		{
			long gameEntity = PlayerMap.Instance.GetPlayerEntityId(steamID);
			CharacterEntity characEntity = (CharacterEntity) GameEntityManager.GetEntity(gameEntity);

			return characEntity;
		}
	}
}
