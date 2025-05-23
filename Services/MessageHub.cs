using Microsoft.AspNetCore.SignalR;

namespace SignalRDev.Services
{
    public class MessageHub : Hub
    {
        private readonly MessageServices _messageServices;

        public MessageHub(MessageServices messageServices)
        {
            _messageServices = messageServices;
        }

        public override Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext()?.Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                _messageServices.AddConnection(userId, Context.ConnectionId);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _messageServices.RemoveConnection(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendPrivateMessage(string toUserId, string message)
        {
            var connId = _messageServices.GetConnectionId(toUserId);
            if (connId != null)
            {
                await Clients.Client(connId).SendAsync("ReceiveMessage", "Server", message);
            }
        }
        public async Task GetOnlineUsers()
        {
            var onlineUsers = _messageServices.GetOnlineUsers();
            await Clients.Caller.SendAsync("ReceiveOnlineUsers", onlineUsers);
        }

        public async Task SendMessageToUser(string toUserId, string fromUserId, string message)
        {
            var connId = _messageServices.GetConnectionId(toUserId);
            if (!string.IsNullOrWhiteSpace(connId))
            {
                await Clients.Client(connId).SendAsync("ReceivePrivateMessage", fromUserId, message);
            }
        }
    }
}