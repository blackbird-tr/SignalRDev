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

        
    }
}