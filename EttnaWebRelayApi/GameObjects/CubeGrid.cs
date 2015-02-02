using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EttnaWebRelayApi.GameObjects
{
	public class CubeGrid
	{
		private List<string> m_names = new List<string>();

		public List<string> Names { get { return m_names; } set { m_names = value; } }

		public long EntityID { get; set; }

		public string Type { get; set; }

		public int BlockCount { get; set; }

		public CubeGrid() { }

		public CubeGrid(List<string> names, long id, string type, int blockcount)
		{
			Names = names;
			EntityID = id;
			Type = type;
			BlockCount = blockcount;
		}
	}
}
