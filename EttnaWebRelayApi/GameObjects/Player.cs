using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EttnaWebRelayApi.GameObjects
{
	public class Player
	{
		/// <summary>
		/// Steam ID
		/// </summary>
		public ulong ID { get; set; }
		/// <summary>
		/// Username
		/// </summary>
		public string Name { get; set; }

		public Player(ulong id, string name)
		{
			ID = id;
			Name = name;
		}
	}
}
