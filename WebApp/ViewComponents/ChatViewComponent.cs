using BL;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace WebApp.ViewComponents
{

    [Authorize]
    public class ChatViewComponent : ViewComponent
    {
        private IChatRepository _chatRepository;
        private readonly UserManager<AppUser> _userManager;
        public ChatViewComponent(IChatRepository chatRepository, UserManager<AppUser> userManager)
        {
            _chatRepository = chatRepository;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);


            if (user == null)
            {
                return View(ChatViewModel.GetChatList(_chatRepository));

            }
            var userId = user.Id;
            var chats = ChatViewModel.GetUsersChat(_chatRepository, userId);
            return View(chats);
        }
    }
}
