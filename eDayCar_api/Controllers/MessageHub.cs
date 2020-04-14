using eDayCar_api.Entities.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace eDayCar_api.Controllers
{
   
    public class MessageHub: Hub

    {
        private static Dictionary<string, string> _connectedUsers = new Dictionary<string, string>();

        public async Task NewMessage( Message msg)
        {
            if( _connectedUsers.ContainsKey(msg.Receiver))
            {
                var receiverId = _connectedUsers[msg.Receiver];
                await Clients.Client(receiverId).SendAsync("MessageReceived", msg);
                await Clients.Client(Context.ConnectionId).SendAsync("MessageReceived", msg);

            }
           
            else
            {
                await Clients.Client(Context.ConnectionId).SendAsync("MessageReceived", msg);
              
            }
         
        }

        public override Task OnConnectedAsync()
        {
            string login = Context.User.Identity.Name;
            if( login == null)
            {
                return Task.CompletedTask;
            }
            if( _connectedUsers.ContainsKey(login))
            {
                _connectedUsers[login] = Context.ConnectionId;
           
            }
            else
            {
                _connectedUsers.Add(login, Context.ConnectionId);
            }
           

            return Task.CompletedTask;
        }
     


    }
}
