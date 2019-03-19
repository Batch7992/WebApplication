using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjaxChat.Models
{
    public class ChatModel
    {
        // Все пользователи чата
        public List<ChatUser> Users;

        // все сообщения
        public List<ChatMessage> Messages;

        public ChatModel()
        {
            Users = new List<ChatUser>();
            Messages = new List<ChatMessage>();       
        }
    }
    public class ChatUser
    {
        public string id;
    }

    public class ChatMessage
    {
        // автор сообщения, если null - автор сервер
        public ChatUser User;
        // текст
        public string Text = "";

    }
}