using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using EttnaWebRelayApi.GameObjects;
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
		public List<Player> GetPlayers()
		{
			var players = new List<Player>();
			var connectedPlayers = PlayerManager.Instance.ConnectedPlayers;

			Log.ConsoleAndFile(string.Format("Requesting connected players ({0})", connectedPlayers.Count));

			foreach (ulong remoteUserId in connectedPlayers)
				players.Add(new Player(remoteUserId, PlayerMap.Instance.GetPlayerNameFromSteamId(remoteUserId)));

			return players;
		}

		[HttpGet]
		public List<CubeGrid> GetOwnedGrids(string steamId)
		{
			Log.ConsoleAndFile(string.Format("Requesting cubegrids owned by {0}.", steamId));

			var ownedcubeGrids = new List<CubeGrid>();
			var cubeGridEntities = SectorObjectManager.Instance.GetTypedInternalData<CubeGridEntity>();
			var playerIds = PlayerMap.Instance.GetPlayerIdsFromSteamId(ulong.Parse(steamId));
			if (playerIds.Count > 0)
			{
				Log.ConsoleAndFile(string.Format("Found player {0} for SteamId {1}. Querying ships...", PlayerMap.Instance.GetPlayerNameFromSteamId(ulong.Parse(steamId)), steamId));
				for (int cubeGridId = cubeGridEntities.Count - 1; cubeGridId >= 0; cubeGridId--)
				{
					var cubeGridEntity = cubeGridEntities[cubeGridId];

					for (int cubeBlockId = cubeGridEntity.CubeBlocks.Count - 1; cubeBlockId >= 0; cubeBlockId--)
					{
						var cubeBlock = cubeGridEntity.CubeBlocks[cubeBlockId];

						if (cubeBlock is CockpitEntity || cubeBlock is AntennaEntity || cubeBlock is BeaconEntity)
						{
							if (playerIds.Contains(cubeBlock.Owner))
							{
								CubeGrid grid = new CubeGrid();

								if (cubeGridEntity.Name.Contains("|"))
									grid.Names.AddRange(cubeGridEntity.Name.Split('|'));
								else
									grid.Names.Add(cubeGridEntity.Name);

								grid.EntityID = cubeGridEntity.EntityId;
								grid.BlockCount = cubeGridEntity.CubeBlocks.Count;
								grid.Type = cubeGridEntity.IsStatic ? "Station" : cubeGridEntity.GridSizeEnum.ToString();
								ownedcubeGrids.Add(grid);
								break;
							}
						}
					}
				}
				Log.Console(string.Format("{0} ships found", ownedcubeGrids.Count));
			}
			else
				Log.Console("Found no player with steamID " + steamId);

			return ownedcubeGrids;
		}
	}
}
