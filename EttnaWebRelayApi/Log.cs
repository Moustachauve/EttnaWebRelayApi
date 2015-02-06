using System.Collections.Generic;
using System.Linq;
using System.Text;

using SEModAPIInternal.Support;

namespace EttnaWebRelayApi
{
	public static class Log
	{
		const string PLUGIN_PREFIX = "WebAPI";

		private static ApplicationLog m_log;
		private static ApplicationLog m_errorLog;

		static Log()
		{
			m_log = new ApplicationLog();
			m_errorLog = new ApplicationLog();

			StringBuilder pluginVersion = new StringBuilder( typeof( Log ).Assembly.GetName( ).Version.ToString( ) );

			m_log.Init(PLUGIN_PREFIX + "_Main.log", pluginVersion);
			m_errorLog.Init(PLUGIN_PREFIX + "_Error.log", pluginVersion);
		}

		public static void File(string text)
		{
			m_log.WriteLine(text);
		}
		public static void Console(string text)
		{
			System.Console.WriteLine("[{0}] {1}", PLUGIN_PREFIX, text);
		}

		public static void ConsoleAndFile(string text)
		{
			File(text);
			Console(text);
		}

		public static void Error(System.Exception ex)
		{
			m_errorLog.WriteLine("Error in " + ex.TargetSite);
			m_errorLog.WriteLine(ex.Message);
			m_errorLog.WriteLine(ex.StackTrace);
			//TEST
			m_errorLog.WriteLine("[TEST]" + ex.ToString());
			Console("[ERROR] " + ex.Message);
			Console("[ERROR] See the error log for more information");
		}
	}
}
