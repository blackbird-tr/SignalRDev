
using System.Collections.Concurrent;
namespace SignalRDev.Services
{
 
        public class MessageServices
        {
            // Thread-safe sözlük ile userId <-> connectionId eşlemesi tutuyoruz
            private static ConcurrentDictionary<string, string> userConnections = new(); 
            public void AddConnection(string userId, string connectionId)
            {
                userConnections[userId] = connectionId;
            }

            public void RemoveConnection(string connectionId)
            {
                var user = userConnections.FirstOrDefault(x => x.Value == connectionId).Key;
                if (!string.IsNullOrEmpty(user))
                {
                    userConnections.TryRemove(user, out _);
                }
            }

            public string? GetConnectionId(string userId)
            {
                userConnections.TryGetValue(userId, out var connectionId);
                return connectionId;
            }
        public List<string> GetOnlineUsers()
        {
            return userConnections.Keys.ToList();
        }
        public IReadOnlyDictionary<string, string> GetAllConnections()
            {
                return userConnections;
            }
        }
    }
