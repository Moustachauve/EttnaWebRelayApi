using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Web.Http;
using EttnaWebRelayApi.GameObjects;
using EttnaWebRelayApi.ResultObjects;
using EttnaWebRelayApi.Utility;
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
		public GetCubeGridsResult GetPlayerCubeGrids(string steamId)
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

		[HttpGet]
		public BaseResult ExportCubeGrid(long entityID)
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
				Log.Error(e);
				return new BaseResult(string.Format("Internal error: {0}. See log for details", e.Message), true);
			}
		}

		[HttpGet]
		public BaseResult ImportCubeGrid(long entityID)
		{
			try
			{
				if (!SavedCubeGridManager.Contains(entityID))
					return new BaseResult(string.Format("{0} is not saved", entityID), true);
				if (SectorObjectManager.Instance.GetEntry(entityID) != null)
					return new BaseResult(string.Format("{0} is already in the world", entityID), true);

				var savedCubeGrid = SavedCubeGridManager.GetSavedCubeGrid(entityID);
				CubeGridEntity cubeGrid = new CubeGridEntity(savedCubeGrid.m_versionList.Values[0]);

				SectorObjectManager.Instance.AddEntity(cubeGrid);

				return new BaseResult(string.Format("{0} lastest save imported successfully", entityID), false);
			}
			catch (Exception e)
			{
				Log.Error(e);
				return new BaseResult(string.Format("Internal error: {0}. See log for details", e.Message), true);
			}
		}

		[HttpGet]
		public BaseResult Teleport(long entityID, double x, double y, double z)
		{
			BaseObject baseObj = SectorObjectManager.Instance.GetEntry(entityID);

			if (!(baseObj is CubeGridEntity))
				return new BaseResult(string.Format("Entity {0} is not a cubeGrid", entityID), true);

			CubeGridEntity cubeGrid = (CubeGridEntity)baseObj;
			cubeGrid.Position = new SEModAPI.API.Vector3DWrapper(x, y, z);


			return new BaseResult(string.Format("Ship teleported to X:{0}, Y:{1}, Z:{2}", x, y, z), false);
		}

		[HttpGet]
		public BaseResult Delete(long entityID)
		{
			try
			{
				BaseObject baseObj = SectorObjectManager.Instance.GetEntry(entityID);
				string dispName = baseObj.Name;
				baseObj.Dispose();

				return new BaseResult(string.Format("Ship \"{0}\" ({1}) successfully deleted", dispName, entityID), false);
			}
			catch (Exception e)
			{
				Log.Error(e);
				return new BaseResult(string.Format("Internal error: {0}. See log for details", e.Message), true);
			}
		}
	}
}
