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
		public DirectoryInfo ExportShipPath { get; set; }
	}

	public static class Config
	{
		public const string PLUGIN_PREFIX = "WebAPI";
		public const string PLUGIN_NAME = "Ettna Web Relay API";

		private static ConfigFile m_config;

		public static string FolderPath { get; set; }
		public static string ConfigXML { get { return FolderPath + @"\WebRelayConfig.xml"; } }

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

				if (File.Exists(ConfigXML))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(ConfigFile));
					using(TextReader textReader = new StreamReader(ConfigXML))
					{ 
						m_config = (ConfigFile)serializer.Deserialize(textReader);
					}
					return m_config;
				}
				else
				{
					m_config.MainEndpointName = "EttnaWebRelay";
					m_config.BindIP = "localhost";
					m_config.Port = 1337;
					m_config.ExportShipPath = new DirectoryInfo(FolderPath + @"\Ships");
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
				XmlSerializer serializer = new XmlSerializer(typeof(ConfigFile));
				using (TextWriter textWriter = new StreamWriter(ConfigXML))
				{
					serializer.Serialize(textWriter, m_config);
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex);
			}
		}

	}
}
