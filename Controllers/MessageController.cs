using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRDev.Data;
using SignalRDev.Models;
using SignalRDev.Services;
using SignalRDev.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SignalRDev.Controllers
{
    [Authorize] // Tüm action'lar için yetkilendirme gerekli
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager; private readonly IHubContext<MessageHub> _hubContext;
        private readonly MessageServices _messageServices;
        public MessageController(
      ApplicationDbContext context,
      UserManager<IdentityUser> userManager,
      IHubContext<MessageHub> hubContext,
      MessageServices messageServices)
        {
            _context = context;
            _userManager = userManager;
            _hubContext = hubContext;
            _messageServices = messageServices;
        }

        // GET: /Message/GetMessages
        public IActionResult GetMessages()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var messages = _context.Messages.Where(m => m.ReceiverId == userId).OrderByDescending(m => m.CreatedAt).ToList();
            return View(messages);
        }

        // GET: /Message/SendMessage
        public async Task<IActionResult> SendMessage()
        {
            var users = await _userManager.Users.ToListAsync();
            ViewBag.Users = new SelectList(users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(MessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var message = new Message
                {
                    Title = model.Title,
                    Body = model.Body,
                    SenderId = model.SenderId,
                    ReceiverId = model.ReceiverId,
                    CreatedAt = DateTime.Now
                };

                await _messageServices.SendMessageAsync(message);
                return RedirectToAction("GetMessages");
            }

            var users = await _userManager.Users.ToListAsync();
            ViewBag.Users = new SelectList(users, "Id", "UserName");
            return View(model);
        }
        // GET: /Message/Index
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var messages = await _context.Messages
                .Where(m => m.SenderId == currentUser.Id || m.ReceiverId == currentUser.Id)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();

            return View(messages);
        }

        [HttpGet]
        public IActionResult SendToAll()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendToAll(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                ModelState.AddModelError("", "Mesaj boş olamaz.");
                return View();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            await _messageServices.SendMessageToAllAsync(currentUser.Id, message);
            return RedirectToAction("Index");
        }
    }
} 