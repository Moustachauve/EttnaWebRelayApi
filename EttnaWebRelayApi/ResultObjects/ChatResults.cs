using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EttnaWebRelayApi.ResultObjects
{
	public class SendPublicMessageResult
	{
		public string Message { get; set; }
		public bool Error { get; set; }

		public SendPublicMessageResult(string message, bool error)
		{
			Message = message;
			Error = error;
		}
	}

}
