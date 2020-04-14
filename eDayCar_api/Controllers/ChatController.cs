using eDayCar_api.Entities.Identity;
using eDayCar_api.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDayCar_api.Controllers
{
    [Route("api/chat/[action]")]
    [ApiController]
    [Authorize]
    public class ChatController: ControllerBase
    {
        private readonly IChatRepository _chatRepository;



        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

     

        [HttpPost]
        public IActionResult StartChat([FromBody] Chat newChat)
        {
            if( newChat.Participants[0] == newChat.Participants[1])
            {
                return Forbid();
            }
            return new JsonResult(_chatRepository.StartChat(newChat));
        }

        [HttpPost]
        public void SendMessage([FromBody] Message newMessage)
        {
            _chatRepository.SendMessage(newMessage);
        }

        [HttpGet]
        public List<Message> LoadAllMessages([FromBody] string Id)
        {
            return _chatRepository.GetAllMesagesByChatId(Id);
        }

        [HttpPost]
        public List<Chat> LoadAllChats([FromBody] dynamic arg)
        {
            return _chatRepository.GetAllChatsByLogin(arg.login.ToString());
        }


    }
}
