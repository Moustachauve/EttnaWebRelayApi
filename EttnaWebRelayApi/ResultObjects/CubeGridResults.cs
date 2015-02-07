using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EttnaWebRelayApi.GameObjects;

namespace EttnaWebRelayApi.ResultObjects
{
	public class GetCubeGridsResult : BaseResult
	{
		public List<CubeGrid> OwnedGrids {get; set;}

		public GetCubeGridsResult(string status, bool error)
			:base(status, error)
		{
			OwnedGrids = new List<CubeGrid>();
		}

		public GetCubeGridsResult(string status, bool error, List<CubeGrid> ownedGridList)
			: base(status, error)
		{
			OwnedGrids = ownedGridList;
		}

	}
}
