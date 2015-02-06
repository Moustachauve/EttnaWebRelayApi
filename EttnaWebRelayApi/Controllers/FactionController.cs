using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using EttnaWebRelayApi.GameObjects;
using EttnaWebRelayApi.ResultObjects;
using SEModAPIExtensions.API;
using SEModAPIInternal.API.Common;

namespace EttnaWebRelayApi.Controllers
{
	class FactionController : ApiController
	{
		[HttpGet]
		public GetAllFactionResult GetAllFaction()
		{
			var factionList = new List<GameObjects.Faction>();

			foreach (SEModAPIInternal.API.Common.Faction currFaction in FactionsManager.Instance.Factions)
			{
				var factionObj = new GameObjects.Faction(currFaction.Id, currFaction.Tag, currFaction.Name, 
														currFaction.Description, currFaction.PrivateInfo);

				foreach (var member in currFaction.Members)
				{
					var memberObj = new GameObjects.FactionMember(member.SteamId, member.PlayerId,
													member.Name, member.IsFounder, member.IsLeader);

					factionObj.Members.Add(memberObj);
				}

				foreach (var member in currFaction.JoinRequests)
				{
					var memberObj = new GameObjects.FactionMember(member.SteamId, member.PlayerId,
													member.Name, member.IsFounder, member.IsLeader);

					factionObj.JoinRequests.Add(memberObj);
				}
			}

			return new GetAllFactionResult(string.Format("{0} faction(s) returned", FactionsManager.Instance.Factions.Count), 
											false, factionList);
		}
	}
}
