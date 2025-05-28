using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRDev.Services;

namespace SignalRDev.Controllers
{
    [Authorize]
    public class OnlineUsersController : Controller
    {
        private readonly MessageServices _messageServices;

        public OnlineUsersController(MessageServices messageServices)
        {
            _messageServices = messageServices;
        }

        public async Task<IActionResult> Index()
        {
            var onlineUsers = await _messageServices.GetOnlineUsersWithNames();
            return View(onlineUsers);
        }

        [HttpGet]
        public IActionResult GetOnlineUsersCount()
        {
            var count = _messageServices.GetOnlineUsers().Count;
            return Json(count);
        }
    }
} 