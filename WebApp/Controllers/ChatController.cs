using BL;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Repositories;
using System;
using System.Security.Claims;
using WebApp.Hubs;
using WebApp.Models;

namespace WebApp.Controllers
{

    [Authorize]
    public class ChatController : Controller
    {
        private IChatRepository _chatRepository;
        private IMessageRepository _messageRepository;
        private readonly UserManager<AppUser> _userManager;


        public ChatController(IChatRepository chatRepository, IMessageRepository messageRepository, UserManager<AppUser> userManager)
        {
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(ChatViewModel.GetChatList(_chatRepository));
        }

        public async Task<IActionResult> GetUsers()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            return View(_userManager.Users.Where(u => u.Id != userId));
        }


        public async Task<IActionResult> ChatView(Guid chatId, int page = 1)
        {

          
            ViewBag.ChatName = ChatViewModel.GetChatById(_chatRepository, chatId).ChatName;
            ViewBag.ChatId = chatId;


            int pageSize = 4;   

            var messages = MessageViewModel.GetMessageList(_messageRepository, chatId);
            var count = await messages.CountAsync();
            var items = await messages.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Messages = items
            };


            return View(viewModel);

        }


        public async Task<IActionResult> SendMessage(string chatId, string message)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            Guid id = Guid.Parse(chatId);

            MessageViewModel mess = new MessageViewModel();

            mess.Id = Guid.NewGuid();
            mess.Text = message.Substring(0, 300);
            mess.DateCreated = DateTime.Now;
            mess.UserId = userId;
            mess.ChatId = id;

            await _messageRepository.AddItemAsync(mess);

            return RedirectToAction("ChatView", new { chatId = id });
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

            chat.AddUserToChat(_chatRepository, userId);
            await _chatRepository.AddItemAsync(chat);

            return RedirectToAction("ChatView", new { chatId = chat.Id });

        }

        public async Task<IActionResult> JoinChat(Guid chatId)
        {

            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var chat = ChatViewModel.GetChatById(_chatRepository, chatId);
            if (chat.UserIds.Contains(userId))
            {
                return RedirectToAction("ChatView", new { chatId = chatId });

            }
            else
            {
                await chat.AddUserToChat(_chatRepository, userId);
                return RedirectToAction("ChatView", new { chatId = chatId });
            }
            



           

        }
        public async Task<IActionResult> DeleteMessage(Guid messageId, Guid chatId)
        {

            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            await _messageRepository.DeleteItemAsync(messageId);
            return RedirectToAction("ChatView", new { chatId = chatId });

        }

        public async Task<IActionResult> Find()
        {

            var users = _userManager.Users
                .Where(u => u.Id.ToString() != User.FindFirst(ClaimTypes.NameIdentifier).Value).ToList();

            return View(users);
        }

        public async Task<IActionResult> CreatePrivateChat(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var currentUser = await _userManager.GetUserAsync(User);

            var privateChat =  ChatViewModel.GetPrivateChatWith(_chatRepository, currentUser.Id, userId);
            if (privateChat != null)
            {
                return RedirectToAction("ChatView", new { chatId = privateChat.Id });
            }
            else
            {
                var chat = new ChatViewModel()
                {
                    Id = Guid.NewGuid(),
                    ChatName = user.UserName+" with "+ currentUser.UserName,
                    DateCreated = DateTime.Now,
                    IsPrivate = true,

                };

                await _chatRepository.AddItemAsync(chat);

                await chat.AddUserToChat(_chatRepository, userId);
                await chat.AddUserToChat(_chatRepository, currentUser.Id);



                return RedirectToAction("ChatView", new { chatId = chat.Id });
            }
        }
        public async Task<IActionResult> LeaveChat(Guid chatId)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var chat = ChatViewModel.GetChatById(_chatRepository, chatId);

            await chat.RemoveUserFromChat(_chatRepository, userId);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> EditMessage(Guid messageId)
        {

            return View(MessageViewModel.GetMessageById(_messageRepository,messageId));

        }

        public async Task<IActionResult> EditMessageSubmit(MessageViewModel model)
        {

           
                var save = model;
                await _messageRepository.DeleteItemAsync(save.Id);
                await _messageRepository.AddItemAsync(save);
            
           
            return RedirectToAction("ChatView", new { chatId = model.ChatId });

        }
    }
}
