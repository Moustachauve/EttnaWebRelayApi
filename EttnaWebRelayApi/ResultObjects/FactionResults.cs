using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EttnaWebRelayApi.ResultObjects
{
	public class GetAllFactionResult : BaseResult
	{
		List<GameObjects.Faction> Factions;

		public GetAllFactionResult(string status, bool error)
			: base(status, error)
		{
			Factions = new List<GameObjects.Faction>();
		}
		public GetAllFactionResult(string status, bool error, List<GameObjects.Faction> factionList)
			: base(status, error)
		{
			Factions = factionList;
		}

	}
}
