using System;
using System.Collections.Generic;
using System.IO;
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
	public class CubeGridController : ApiController
	{
		[HttpGet]
		public GetCubeGridsResult GetAllCubeGrids()
		{
			var cubeGrids = new List<CubeGrid>();
			var cubeGridEntities = SectorObjectManager.Instance.GetTypedInternalData<CubeGridEntity>();

			for (int cubeGridId = cubeGridEntities.Count - 1; cubeGridId >= 0; cubeGridId--)
			{
				var currCubeGridEntity = cubeGridEntities[cubeGridId];

				CubeGrid grid = new CubeGrid();

				grid.Name = currCubeGridEntity.DisplayName;

				grid.EntityID = currCubeGridEntity.EntityId;
				grid.BlockCount = currCubeGridEntity.CubeBlocks.Count;
				grid.Type = currCubeGridEntity.IsStatic ? "Station" : currCubeGridEntity.GridSizeEnum.ToString();
				cubeGrids.Add(grid);
			}

			return new GetCubeGridsResult(string.Format("{0} cubegrid(s) found", cubeGrids.Count), false, cubeGrids);
		}
		[HttpGet]
		public GetCubeGridsResult GetOwnedGrids(string steamId)
		{
			var ownedcubeGrids = new List<CubeGrid>();
			var cubeGridEntities = SectorObjectManager.Instance.GetTypedInternalData<CubeGridEntity>();
			var playerIds = PlayerMap.Instance.GetPlayerIdsFromSteamId(ulong.Parse(steamId));
			if (playerIds.Count == 0)
				return new GetCubeGridsResult(string.Format("Found no player with steamID \"{0}\"", steamId), true);

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

							grid.Name = cubeGridEntity.DisplayName;
							grid.EntityID = cubeGridEntity.EntityId;
							grid.BlockCount = cubeGridEntity.CubeBlocks.Count;
							grid.Type = cubeGridEntity.IsStatic ? "Station" : cubeGridEntity.GridSizeEnum.ToString();
							ownedcubeGrids.Add(grid);
							break;
						}
					}
				}
			}

			return new GetCubeGridsResult(string.Format("{0} ships found", ownedcubeGrids.Count), false, ownedcubeGrids);
		}
		
		//TODO: Test this endpoint
		[HttpGet]
		public BaseResult ExportGrid(long entityID)
		{
			try
			{
				BaseObject baseObj = SectorObjectManager.Instance.GetEntry(entityID);

				if (!(baseObj is CubeGridEntity))
					return new BaseResult(string.Format("Entity {0} is not a cubeGrid", entityID), true);

				CubeGridEntity cubeGrid = (CubeGridEntity)baseObj;

				//new FileInfo(Config.Settings.ExportShipPath + @"\" + cubeGrid.EntityId + ".sbc");
				FileInfo file = new FileInfo(string.Format(@"{0}\{1}_{2}.sbc", Config.Settings.ExportShipPath, cubeGrid.EntityId, DateTime.Now.ToString("yyyyMMddHHmmss")));

				cubeGrid.Export(file);

				return new BaseResult(string.Format("{0} exported successfully", entityID), false);
			}
			catch (Exception e)
			{
				return new BaseResult(string.Format("Internal error: {0}. See log for details", e.Message), true);
			}
		}

		public void ImportGrid(long entityID)
		{
			try
			{


			}
			catch (Exception e)
			{

				throw;
			}
		}
	}
}
