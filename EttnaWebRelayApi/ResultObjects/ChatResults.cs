using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EttnaWebRelayApi.GameObjects;
using SEModAPIExtensions.API;

namespace EttnaWebRelayApi.ResultObjects
{

	public class GetChatMessagesFromResult : BaseResult
	{
		public List<ChatMessage> ChatMessages { get; set; }
		public int LastMessageIndex { get { return ChatMessages.Count - 1; } }

		public GetChatMessagesFromResult(string status, bool error)
			: base(status, error)
		{
			ChatMessages = new List<ChatMessage>();
		}

		public GetChatMessagesFromResult(string status, bool error, List<ChatMessage> messageList)
			: base(status, error)
		{
			ChatMessages = messageList;
		}
	}

	public class GetLastMessageIdResult : BaseResult
	{
		public int LastMessageID { get; set; }

		public GetLastMessageIdResult(string status, bool error, int lastId)
			: base(status, error)
		{
			LastMessageID = lastId;
		}
	}
}
