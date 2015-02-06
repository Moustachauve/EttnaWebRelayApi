using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EttnaWebRelayApi
{
	[XmlRoot("WebAPIConfig")]
	public class ConfigFile
	{
		public string MainEndpointName { get; set; }
		public string BindIP { get; set; }
		public int Port { get; set; }
		public string ExportShipPath { get; set; }
	}

	public static class Config
	{
		public const string PLUGIN_PREFIX = "WebAPI";
		public const string PLUGIN_NAME = "Ettna Web Relay API";

		private static ConfigFile m_config;

		public static string FolderPath { get; set; }
		public static string ConfigXMLPath { get { return FolderPath + @"\WebRelayConfig.xml"; } }

		public static ConfigFile Settings
		{
			get
			{
				if (m_config == null)
					m_config = Load();

				return m_config;
			}
			set { m_config = value; }
		}

		public static ConfigFile Load()
		{
			try
			{
				if (m_config == null)
					m_config = new ConfigFile();

				if (File.Exists(ConfigXMLPath))
				{
					Log.ConsoleAndFile("Config file exist");
					XmlSerializer serializer = new XmlSerializer(typeof(ConfigFile));
					using(TextReader textReader = new StreamReader(ConfigXMLPath))
					{ 
						m_config = (ConfigFile)serializer.Deserialize(textReader);
					}
					return m_config;
				}
				else
				{
					Log.ConsoleAndFile("Config file do not exist");
					m_config.MainEndpointName = "EttnaWebRelay";
					m_config.BindIP = "localhost";
					m_config.Port = 1337;
					m_config.ExportShipPath = FolderPath + @"\Ships";
					Save();
				}
				return m_config;
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				return (ConfigFile)null;
			}
		}

		public static void Save()
		{
			try
			{
				Log.ConsoleAndFile("Saving config file...");
				XmlSerializer serializer = new XmlSerializer(typeof(ConfigFile));
				using (TextWriter textWriter = new StreamWriter(ConfigXMLPath))
				{
					serializer.Serialize(textWriter, m_config);
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				Log.ConsoleAndFile(ex.StackTrace);
			}
		}

	}
}
