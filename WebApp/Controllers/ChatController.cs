using BL;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using System;
using WebApp.Hubs;

namespace WebApp.Controllers
{
    public class ChatController : Controller
    {
        private IChatRepository _chatRepository;
        private IMessageRepository _messageRepository;
        private readonly UserManager<AppUser> _userManager;


        public ChatController(IChatRepository chatRepository,IMessageRepository messageRepository, UserManager<AppUser> userManager)
        {
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(ChatViewModel.GetChatList(_chatRepository));
        }


        public async Task<IActionResult> ChatView(Guid chatId)
        {

            var messages = MessageViewModel.GetMessageList(_messageRepository, chatId);
            ViewBag.ChatName = ChatViewModel.GetChatById(_chatRepository, chatId).ChatName;
            ViewBag.ChatId = chatId;
            return View(messages);

        }


        public async Task<IActionResult> SendMessage(string chatId,string message)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            Guid id = Guid.Parse(chatId);

            MessageViewModel mess = new MessageViewModel();

            mess.Id = Guid.NewGuid();
            mess.Text = message;
            mess.DateCreated = DateTime.Now;
            mess.UserId = userId;
            mess.ChatId = id;

            await _messageRepository.AddItemAsync(mess);

            return RedirectToAction("ChatView", new { chatId = id});
        }

        public async Task<IActionResult> CreateChat(IFormCollection form)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            string name = form["name"];

            ChatViewModel chat = new ChatViewModel()
            {
                Id = Guid.NewGuid(),
                ChatName = name,
                DateCreated = DateTime.Now,
                IsPrivate = false
               
            };
            
            chat.AddUserToChat(_chatRepository,userId);
            await _chatRepository.AddItemAsync(chat);

            return RedirectToAction("ChatView", new { chatId = chat.Id });

        }

        public async Task<IActionResult> JoinChat(Guid chatId)
        {

            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var chat = ChatViewModel.GetChatById(_chatRepository, chatId);
            chat.AddUserToChat(_chatRepository, userId);



            return RedirectToAction("ChatView", new { chatId = chatId });

        }
    }
}
