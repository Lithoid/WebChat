using BL;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Repositories;
using System.Text.RegularExpressions;
using WebApp.Hubs;

namespace WebApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class MessageController : Controller
    {
        private IHubContext<ChatHub> _chat;
        private IMessageRepository _messageRepository;
        private UserManager<AppUser> _userManager;

        public MessageController(
            IHubContext<ChatHub> chat, IMessageRepository messageRepository, UserManager<AppUser> userManager)
        {
            _chat = chat;
            _messageRepository = messageRepository;
            _userManager = userManager;

        }

        [HttpPost("[action]/{connectionId}/{groupId}")]
        public async Task<IActionResult> JoinGroup(string connectionId, string groupId)
        {

            await _chat.Groups.AddToGroupAsync(connectionId, groupId.ToString());
            return Ok();
        }

        [HttpPost("[action]/{connectionId}/{groupId}")]
        public async Task<IActionResult> LeaveGroup(string connectionId, string groupId)
        {

            await _chat.Groups.RemoveFromGroupAsync(connectionId, groupId.ToString());
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(string message, string groupId)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            MessageViewModel mess = new MessageViewModel();

            mess.Id = Guid.NewGuid();
            mess.Text = message;
            mess.DateCreated = DateTime.Now;
            mess.UserId = userId;
            mess.ChatId = Guid.Parse(groupId);

            await _messageRepository.AddItemAsync(mess);

            await _chat.Clients.Group(groupId.ToString())
                .SendAsync("RecieveMessage",message);
            return Ok();
        }
    }
}
