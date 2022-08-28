using Microsoft.AspNetCore.Identity;
using System;



namespace Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public List<Message> Messages { get; set; }

        public List<Chat> Chats { get; set; }

        public List<UserChat> UserChatKeys { get; set; }
    }
}