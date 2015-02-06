using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using EttnaWebRelayApi.ResultObjects;

namespace EttnaWebRelayApi
{
	public static class SavedCubeGrid
	{
		public static void ScanDirectory()
		{
			if(!Directory.Exists(Config.Settings.ExportShipPath))
			{
				Directory.CreateDirectory(Config.Settings.ExportShipPath);
				return;
			}

			string[] test = Directory.GetFiles(Config.Settings.ExportShipPath, "*.sbc");
			foreach (var item in test)
			{
				Console.WriteLine(item);
			}
		}
	}
}
