using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

using SEModAPIExtensions.API;
using SEModAPIExtensions.API.Plugin;
using SEModAPIExtensions.API.Plugin.Events;

using SEModAPIInternal.API.Entity.Sector.SectorObject;
using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid;
using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid.CubeBlock;
using SEModAPIInternal.API.Entity;
using SEModAPIInternal.API.Common;
using System.IO;

namespace EttnaWebRelayApi
{
	public class Main : PluginBase
	{
		private HttpSelfHostServer m_apiServer;

		public Main()
		{
			Log.Console(String.Format("{0} v{1} constructed",Config.PLUGIN_NAME, Version));
		}

		private void StartWebAPI()
		{
			Log.ConsoleAndFile("Starting server...");
			var config = new HttpSelfHostConfiguration(string.Format("http://{0}:{1}/",Config.Settings.BindIP, Config.Settings.Port));

			config.Routes.MapHttpRoute(
				"API Default", Config.Settings.MainEndpointName + "/{controller}/{action}/{id}",
				new { id = RouteParameter.Optional });

			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

			m_apiServer = new HttpSelfHostServer(config);
			m_apiServer.OpenAsync().Wait();
			Log.ConsoleAndFile("Server is running.");
		}

		private void StopWebAPI()
		{
			Log.ConsoleAndFile("Stopping web API...");
			m_apiServer.CloseAsync().Wait();
			m_apiServer.Dispose();
			m_apiServer = null;
			Log.ConsoleAndFile("Web API stopped successfully");
		}


		public override void Init() { }

		public void InitWithPath(string path)
		{
			try
			{
				Log.ConsoleAndFile("Loading configuration...");
				Config.FolderPath = new FileInfo(path).DirectoryName;
				Log.ConsoleAndFile("Scanning for previously exported ships...");
				SavedCubeGrid.ScanDirectory();

				StartWebAPI();
			}
			catch(Exception ex)
			{
				Log.Error(ex);
			}
		}

		public override void Shutdown() { StopWebAPI(); }

		public override void Update() { }
	}
}
