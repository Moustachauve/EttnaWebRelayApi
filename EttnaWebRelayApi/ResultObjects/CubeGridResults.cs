using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EttnaWebRelayApi.GameObjects;

namespace EttnaWebRelayApi.ResultObjects
{
	public class GetOwnedGridsResult : BaseResult
	{
		List<CubeGrid> OwnedGrids {get; set;}

		public GetOwnedGridsResult(string status, bool error)
			:base(status, error)
		{
			OwnedGrids = new List<CubeGrid>();
		}

		public GetOwnedGridsResult(string status, bool error, List<CubeGrid> ownedGridList)
			: base(status, error)
		{
			OwnedGrids = ownedGridList;
		}

	}
}
