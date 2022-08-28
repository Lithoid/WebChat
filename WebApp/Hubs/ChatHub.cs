using BL;
using Microsoft.AspNetCore.SignalR;
using WebApp.Models;



namespace WebApp.Hubs
{
    public class ChatHub:Hub
    {

        public string GetConnectionId() => Context.ConnectionId; 

    }

   
}
