using Microsoft.AspNetCore.SignalR;
using SignalRDev.Services;
using System.Security.Claims;

namespace SignalRDev.Extensions
{
    public static class HubExtensions
    {

        public static List<string> GetConnectionId(this HubCallerContext context, string userId)
        {
            return context.Items.ContainsKey(userId) ? context.Items[userId] as List<string> : null;
        }
        public static List<string> GetOnlineUsers(Hub hub)
        {
            return hub.Context.Items.Keys
                      .Select(k => k.ToString())
                      .ToList();
        }

        public static async Task SendPrivateMessageAsync(this IHubContext<MessageHub> hubContext, List<string> connIds, string message, string sender = "Server")
        {
            if (connIds == null || connIds.Count == 0)
                return;

            foreach (var id in connIds)
            {
                await hubContext.Clients.Client(id).SendAsync("ReceiveMessage", sender, message);
            }
        }


    }



}
