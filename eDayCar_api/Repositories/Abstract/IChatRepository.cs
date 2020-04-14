using eDayCar_api.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDayCar_api.Repositories.Abstract
{
    public interface IChatRepository
    {
        Chat StartChat(Chat chat);
        void SendMessage(Message message);
        List<Message> GetAllMessagesByLogin(string login, string currentUserLogin);
        List<Chat> GetAllChatsByLogin(string login);
        List<Message> GetAllMesagesByChatId(string id);

    }
}
