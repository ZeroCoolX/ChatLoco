﻿using ChatLoco.Entities.MessageDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatLoco.Services
{
    public static class MessageService
    {

        private static Dictionary<int, MessageDTO> MessageCache = new Dictionary<int, MessageDTO>();

        private static int uniqueId = 0;

        public static MessageDTO GetMessage(int messageId)
        {
            try
            {
                return MessageCache[messageId];
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public static MessageDTO CreateMessage(int userId, int chatroomId, string rawMessage)
        {
            try
            {
                uniqueId += 1;
                MessageDTO m = new MessageDTO()
                {
                    Id = uniqueId,
                    UserId = userId,
                    ChatroomId = chatroomId,
                    RawMessage = rawMessage
                };

                string currentTime = DateTime.Now.ToString("MM/dd [h:mm:ss tt]");
                string username = UserService.GetUser(userId).Username;
                string formattedMessage = string.Format("{0} [{1}] : {2}", currentTime, username, rawMessage);

                m.FormattedMessage = formattedMessage;

                MessageCache.Add(m.Id, m);

                return m;
            }
            catch(Exception e)
            {
                return null;
            }
        }

    }
}