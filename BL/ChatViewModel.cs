using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{

    public class ChatViewModel
    {
        public Guid Id { get; set; }
        public string ChatName { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsPrivate { get; set; }


        public List<Guid> UserIds { get; set; } = new List<Guid>();
        public List<Guid> MessageIds { get; set; } = new List<Guid>();



        public ChatViewModel()
        {
        }

        public bool IsEmpty
        {
            get => ChatName == null;
        }

        public ChatViewModel(Chat chat)
        {
            Id = chat.Id;
            ChatName = chat.ChatName;
            DateCreated = chat.DateCreated;
            IsPrivate = chat.IsPrivate;

            UserIds = chat.Users.Select(c => c.Id).ToList();
            MessageIds = chat.Messages.Select(c => c.Id).ToList();
        }
        public static implicit operator Chat(ChatViewModel model)
        {
            return new Chat
            {
                Id = model.Id,
                ChatName = model.ChatName,
                DateCreated = model.DateCreated,
                IsPrivate = model.IsPrivate,
                UserChatKeys = model.UserIds.Select(i => new UserChat { ChatId = model.Id, UserId = i }).ToList(),

            };
        }

        public static ChatViewModel GetChatById(IChatRepository repository,Guid id)
        {
            return (repository.AllItems as DbSet<Chat>)
                .Where(o => o.Id == id)
                .Include(o => o.Users)
                .Include(o => o.Messages)
                .Select(o => new ChatViewModel(o))
                .FirstOrDefault();
        }

        public static IQueryable<ChatViewModel> GetUsersChat(IChatRepository repository, Guid userId)
        {
            return (repository.AllItems as DbSet<Chat>)
                .Where(c => c.Users.Any(u => u.Id == userId))
                .Include(o => o.Users)
                .Include(o => o.Messages)
                .Select(o => new ChatViewModel(o));
        }

        public static IQueryable<ChatViewModel> GetChatList(IChatRepository repository, bool privacy = false,string name="")
        {
            if (String.IsNullOrEmpty(name))
            {
                return (repository.AllItems as DbSet<Chat>)
                .Where(c => c.IsPrivate == privacy)
                .Include(o => o.Users)
                .Include(o => o.Messages)
                .Select(o => new ChatViewModel(o));
            }else
            {
                return (repository.AllItems as DbSet<Chat>)
                .Where(c => c.IsPrivate == privacy)
                .Include(o => o.Users)
                .Include(o => o.Messages)
                .Select(o => new ChatViewModel(o));
            }
            
        }


        

        public static ChatViewModel GetPrivateChatWith(IChatRepository repository, Guid thisId,Guid thatId)
        {
            return (repository.AllItems as DbSet<Chat>)
                .Where(c => c.Users.Any(u=>u.Id== thisId) && c.Users.Any(u => u.Id == thatId) && c.Users.Count==2)
                .Include(o => o.Users)
                .Include(o => o.Messages)
                .Select(o => new ChatViewModel(o))
                .FirstOrDefault();
        }

        public async Task AddUserToChat(IChatRepository repository, Guid userId)
        {
            this.UserIds.Add(userId);
            var chat = this;
            await repository.DeleteItemAsync(chat.Id);
            await repository.AddItemAsync(chat);


        }
        public async Task RemoveUserFromChat(IChatRepository repository, Guid userId)
        {
            this.UserIds.Remove(userId);
            var chat = this;
            if (chat.IsPrivate)
            {
                await repository.DeleteItemAsync(chat.Id);
            }
            else
            {
               
                await repository.DeleteItemAsync(chat.Id);
                await repository.AddItemAsync(chat);
            }
            


        }


    }
}
