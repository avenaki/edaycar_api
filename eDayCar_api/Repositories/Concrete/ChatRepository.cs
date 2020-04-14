using eDayCar_api.Entities.Identity;
using eDayCar_api.Repositories.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;


namespace eDayCar_api.Repositories.Concrete
{
    public class ChatRepository: IChatRepository
    {
        private IMongoCollection<Chat> Collection { get; }

        public ChatRepository(MongoContext context)
        {
            Collection = context.GetCollection<Chat>("Chats");
        }

        public Chat StartChat(Chat newChat)
        { 
            var filter = Builders<Chat>.Filter.All("Participants", newChat.Participants );
            var chat = Collection.Find(filter);
            if (chat.CountDocuments() == 0)
            {
                Collection.InsertOne(newChat);
                var currentChat = Collection.Find(filter).ToList()[0];
                return currentChat;
            }
            return chat.ToList()[0];
        }
        
        public  void SendMessage(Message message)
        {

            var filter = Builders<Chat>.Filter.All("Participants", new List<string>() { message.Receiver, message.Sender });
            var update = Builders<Chat>.Update.Push("Messages", message);
            Collection.FindOneAndUpdateAsync(filter, update);
           
    
        }

        List<Message> IChatRepository.GetAllMessagesByLogin(string login, string currentUserLogin)
        {
            var filter = Builders<Chat>.Filter.All("Participants", new List<string>() { login, currentUserLogin });
            var chat = Collection.Find(filter).ToList()[0];
            return chat.Messages;
        }
        List<Message> IChatRepository.GetAllMesagesByChatId(string id)
        {
            return Collection.Find(i => i.Id == id).FirstOrDefault().Messages;
        }

        List<Chat> IChatRepository.GetAllChatsByLogin(string login)
        {
            var filter = Builders<Chat>.Filter.All("Participants", new List<string>() { login } );
            return Collection.Find(filter).ToList();
        }
  
 
    }
}
