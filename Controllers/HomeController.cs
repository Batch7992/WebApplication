using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AjaxChat.Models;

namespace AjaxChat.Controllers
{
    public class HomeController : Controller
    {
        static ChatModel chatModel;

        public ActionResult Index(string user, bool? logOn, bool? logOff, string chatMessage)
        {
            try
            {
                if (chatModel == null) chatModel = new ChatModel();

                
                if (chatModel.Messages.Count > 20)
                    chatModel.Messages.RemoveRange(0, 10);

                
                if (!Request.IsAjaxRequest())
                {
                    return View(chatModel);
                }
                else if (logOn != null && (bool)logOn)
                {
                    if(user == null)
                    {
                        user = generateID();
                        
                        if (chatModel.Users.FirstOrDefault(u => u.id == user) != null)
                        {
                            throw new Exception("Пользователь с таким id уже существует");
                        }
                        else
                        {
                            
                            chatModel.Users.Add(new ChatUser()
                            {
                                id = user,
                            });
                        }
                    }
                    return PartialView("ChatRoom", chatModel);
                }
                else if (logOff != null && (bool)logOff)
                {
                    LogOff(chatModel.Users.FirstOrDefault(u => u.id == user));
                    return PartialView("ChatRoom", chatModel);
                }
                else
                {
                    ChatUser currentUser = chatModel.Users.FirstOrDefault(u => u.id == user);

                    if (!string.IsNullOrEmpty(chatMessage))
                    {
                        chatModel.Messages.Add(new ChatMessage()
                        {
                            User = currentUser,
                            Text = chatMessage,
                        });
                    }

                    return PartialView("History", chatModel);
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Content(ex.Message);
            }
        }

        public void LogOff(ChatUser user)
        {
            chatModel.Users.Remove(user);
        }

        public string generateID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}