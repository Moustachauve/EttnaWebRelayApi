using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using EttnaWebRelayApi.GameObjects;
using EttnaWebRelayApi.ResultObjects;
using SEModAPIExtensions.API;
using SEModAPIInternal.API.Common;

namespace EttnaWebRelayApi.Controllers
{
	public class ChatController : ApiController
	{
		[HttpGet]
		public BaseResult SendPublicMessage(string message)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(message))
					return new BaseResult("Message can't be empty", true);
				ChatManager.Instance.SendPublicChatMessage(message);
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				return new BaseResult("Internal server error", true);
			}

			return new BaseResult(string.Format("Message sent (Lenght={0})", message.Length), false);
		}

		[HttpPost]
		public List<BaseResult> SendPublicMessages(string[] messages)
		{
			var results = new List<BaseResult>();

			try
			{
				for (int i = 0; i < messages.Length; i++)
				{
					if (string.IsNullOrWhiteSpace(messages[i]))
						results.Add(new BaseResult("Message can't be empty", true));
					else
					{
						ChatManager.Instance.SendPublicChatMessage(messages[i]);
						results.Add(new BaseResult(string.Format("Message sent (Lenght={0})", messages[i].Length), false));
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex);

				results.Add(new BaseResult("Internal server error", true));
			}

			return results;
		}

		[HttpGet]
		public GetChatMessagesFromResult GetChatMessagesFrom(int id)
		{
			if (id < 0)
				return new GetChatMessagesFromResult("Id can't be smaller than 0", true);
			if (ChatManager.Instance.ChatHistory.Count < id)
				return new GetChatMessagesFromResult("Id is greater than last message Id", true);
			if (ChatManager.Instance.ChatHistory.Count == id)
				return new GetChatMessagesFromResult("No new messages", false);
			var messageList = new List<ChatMessage>();

			for (int i = id; i < ChatManager.Instance.ChatHistory.Count; i++)
			{
				ChatManager.ChatEvent curChatMessage = ChatManager.Instance.ChatHistory[i];
				ulong steamID = curChatMessage.SourceUserId;

				string name;
				if (curChatMessage.SourceUserId == 0)
					name = "Server";
				else
					name = PlayerMap.Instance.GetPlayerNameFromSteamId(steamID);

				string message = curChatMessage.Message;

				messageList.Add(new ChatMessage(i, steamID, name, message));
			}
			return new GetChatMessagesFromResult("", false, messageList);
		}
	}
}
