using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EttnaWebRelayApi.ResultObjects
{
	public class BaseResult
	{
		public string Status { get; set; }
		public bool Error { get; set; }

		public BaseResult(string status, bool error)
		{
			Status = status;
			Error = error;
		}

	}
}
