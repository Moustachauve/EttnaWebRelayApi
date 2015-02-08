using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using EttnaWebRelayApi.ResultObjects;

namespace EttnaWebRelayApi.Utility
{
	public static class SavedCubeGridManager
	{
		private static Dictionary<long, SavedCubeGrid> m_savedCubeGridList;

		static SavedCubeGridManager()
		{
			m_savedCubeGridList = new Dictionary<long, SavedCubeGrid>();
		}

		public static bool Contains(long entityID)
		{
			return m_savedCubeGridList.ContainsKey(entityID);
		}

		public static SavedCubeGrid GetSavedCubeGrid(long entityID)
		{
			return m_savedCubeGridList[entityID];
		}

		public static void AddSavedCubeGrid(long entityID, ulong versionDate, FileInfo path)
		{
			if (!m_savedCubeGridList.ContainsKey(entityID))
			{
				var newSavedShip = new SavedCubeGrid();
				m_savedCubeGridList.Add(entityID, newSavedShip);
			}

			m_savedCubeGridList[entityID].m_versionList.Add(versionDate, path);

		}

		public static void ScanDirectory()
		{
			if (!Directory.Exists(Config.Settings.ExportShipPath))
			{
				Directory.CreateDirectory(Config.Settings.ExportShipPath);
				return;
			}
			string[] allCubeGrid = Directory.GetFiles(Config.Settings.ExportShipPath, "*.sbc");
			foreach (var cubeGridPath in allCubeGrid)
			{
				FileInfo curFile = new FileInfo(cubeGridPath);

				//Split the file name. Format : ShipEntityID_date(yyyyMMddHHmmss)
				string[] parsedName = Path.GetFileNameWithoutExtension(curFile.Name).Split('_');
				long shipEntity = long.Parse(parsedName[0]);
				ulong versionDate = ulong.Parse(parsedName[1]);

				AddSavedCubeGrid(shipEntity, versionDate, curFile);
			}
		}
	}
}
