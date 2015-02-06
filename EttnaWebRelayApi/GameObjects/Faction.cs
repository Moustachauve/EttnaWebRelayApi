using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EttnaWebRelayApi.GameObjects
{
	public class Faction
	{
		public List<FactionMember> Members { get; set; }
		public List<FactionMember> JoinRequests { get; set; }

		public long ID { get; set; }
		public string Tag { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string PrivateInfo { get; set; }

		public Faction(long id, string tag, string name, string description, string privateInfo)
		{
			Members = new List<FactionMember>();
			JoinRequests = new List<FactionMember>();

			ID = id;
			Tag = tag;
			Name = name;
			Description = description;
			PrivateInfo = privateInfo;
		}
	}

	public class FactionMember : BasicPlayer
	{
		public bool IsFounder { get; set; }
		public bool IsLeader { get; set; }

		public FactionMember(ulong steamID, long entityID, string name, bool isFounder, bool isLeader)
			: base(steamID, entityID, name)
		{
			IsFounder = isFounder;
			IsLeader = isLeader;
		}
	}

}
