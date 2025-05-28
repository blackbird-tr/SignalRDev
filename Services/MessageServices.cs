using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using SignalRDev.Data;
using SignalRDev.Models;
using Microsoft.AspNetCore.Identity;

namespace SignalRDev.Services
{
    public class MessageServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<MessageHub> _hubContext;
        private readonly UserManager<IdentityUser> _userManager;
        // Static dictionary for connection management
        private static readonly ConcurrentDictionary<string, List<string>> UserConnections = new();

        public MessageServices(
            ApplicationDbContext context, 
            IHubContext<MessageHub> hubContext,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        public void AddConnection(string userId, string connectionId)
        {
            if (UserConnections.ContainsKey(userId))
            {
                UserConnections[userId].Add(connectionId);
            }
            else
            {
                UserConnections.TryAdd(userId, new List<string> { connectionId });
            }
        }

        public void RemoveConnection(string connectionId)
        {
            foreach (var pair in UserConnections)
            {
                if (pair.Value.Contains(connectionId))
                {
                    pair.Value.Remove(connectionId);

                    if (pair.Value.Count == 0)
                    {
                        UserConnections.TryRemove(pair.Key, out _);
                    }
                    break;
                }
            }
        }

        public List<string> GetOnlineUsers()
        {
            return UserConnections.Keys.ToList();
        }
        public List<string> GetConnectionIds(string userId)
        {
            return UserConnections.TryGetValue(userId, out var connections) ? connections : new List<string>();
        }
        public string GetConnectionId(string userId)
        {
            if (UserConnections.TryGetValue(userId, out var connections) && connections.Any())
            {
                return connections.First();
            }
            return null;
        }
        public async Task SendMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
             
            var receiverConnectionIds = GetConnectionIds(message.ReceiverId)?.ToList();

            if (receiverConnectionIds != null && receiverConnectionIds.Any())
            {
                foreach (var connId in receiverConnectionIds)
                {
                    await _hubContext.Clients.Client(connId)
                        .SendAsync("ReceiveMessage", message.SenderId, message.Body);
                }
            }
        }

        public async Task SendMessageToAllAsync(string senderId, string message)
        {
            var onlineUsers = GetOnlineUsers();
            foreach (var userId in onlineUsers)
            {
                var newMessage = new Message
                {
                    Title = "Toplu Mesaj",
                    Body = message,
                    SenderId = senderId,
                    ReceiverId = userId,
                    CreatedAt = DateTime.Now
                };

                await SendMessageAsync(newMessage);
            }
        }

        public async Task<List<(string UserId, string UserName)>> GetOnlineUsersWithNames()
        {
            var onlineUsers = GetOnlineUsers();
            var result = new List<(string UserId, string UserName)>();

            foreach (var userId in onlineUsers)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    result.Add((userId, user.UserName));
                }
            }

            return result;
        }
    }
}
