using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using EttnaWebRelayApi.ResultObjects;
using SEModAPIExtensions.API;

namespace EttnaWebRelayApi.Controllers
{
	public class ChatController : ApiController
	{
		[HttpGet]
		public SendPublicMessageResult SendPublicMessage(string message)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(message))
					return new SendPublicMessageResult("Message can't be empty", true);
				ChatManager.Instance.SendPublicChatMessage(message);
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				return new SendPublicMessageResult("Internal server error", true);
			}

			return new SendPublicMessageResult(string.Format("Message sent (Lenght={0})", message.Length), false);
		}
		
		[HttpPost]
		public List<SendPublicMessageResult> SendPublicMessages(string[] messages)
		{
			var results = new List<SendPublicMessageResult>();

			try
			{
				for (int i = 0; i < messages.Length; i++)
				{
					if (string.IsNullOrWhiteSpace(messages[i]))
						results.Add(new SendPublicMessageResult("Message can't be empty", true));
					else
					{
						ChatManager.Instance.SendPublicChatMessage(messages[i]);
						results.Add(new SendPublicMessageResult(string.Format("Message sent (Lenght={0})", messages[i].Length), false));
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex);

				results.Add(new SendPublicMessageResult("Internal server error", true));
			}

			return results;
		}

		[HttpGet]
		public string GetChatMessagesFrom(int id)
		{v

		}



	}
}
