using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EttnaWebRelayApi.GameObjects
{
	public class CubeGrid
	{
		public string Name { get; set; }

		public long EntityID { get; set; }

		public string Type { get; set; }

		public int BlockCount { get; set; }

		public Vector3 Position { get; set; }

		public Vector3 LinearVelocity { get; set; }

		public Vector3 AngularVelocity { get; set; }

		public float MaxLinearVelocity { get; set; }

		public CubeGrid() { }

		public CubeGrid(string name, long id, string type, int blockcount)
		{
			Name = name;
			EntityID = id;
			Type = type;
			BlockCount = blockcount;
		}
	}
}
