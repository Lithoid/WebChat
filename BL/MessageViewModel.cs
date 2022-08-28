using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace BL
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public Guid ChatId { get; set; }

        public string ChatName { get; set; }
        public MessageViewModel()
        {
        }

        public bool IsEmpty
        {
            get => Text == null;
        }

        public MessageViewModel(Message message)
        {
            Id = message.Id;
            Text=message.Text;
            DateCreated = message.DateCreated;
            UserId = message.UserId;
            UserName = message.User.UserName;
            ChatId = message.ChatId;
            ChatName = message.Chat.ChatName;
        }
        public static implicit operator Message(MessageViewModel model)
        {
            return new Message
            {
                Id = model.Id,
                Text = model.Text,
                DateCreated = model.DateCreated,
                UserId = model.UserId,
                ChatId = model.ChatId,
                                
            };
        }

        public static IQueryable<MessageViewModel> GetMessageList(IMessageRepository repository, Guid chatId)
        {
            return (repository.AllItems as DbSet<Message>)
                .Where(c => c.ChatId==chatId)
                .Include(o => o.User)
                .Include(o => o.Chat)
                .Select(o => new MessageViewModel(o));
        }

    }
}