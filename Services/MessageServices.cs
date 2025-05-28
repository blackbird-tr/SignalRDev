
using System.Collections.Concurrent;
namespace SignalRDev.Services
{
 
        public class MessageServices
        {
            // Thread-safe sözlük ile userId <-> connectionId eşlemesi tutuyoruz
            public static ConcurrentDictionary<string, List<string>> userConnections = new(); 

            public void AddConnection(string userId, string connectionId)
            { 

            if (userConnections.ContainsKey(userId)) { 
                 userConnections[userId].Add(connectionId);
            }
            else
            {
                userConnections.TryAdd(userId, new List<string> { connectionId });

            }
            }

        public void RemoveConnection(string connectionId)
        {
            foreach (var pair in userConnections)
            {
                if (pair.Value.Contains(connectionId))
                {
                    pair.Value.Remove(connectionId);

                    if (pair.Value.Count == 0)
                    {
                        userConnections.TryRemove(pair.Key, out _);
                    }
                    break;
                }
            }
        }

        
        public List<string> GetOnlineUsers()
        {
            return userConnections.Keys.ToList();
        } 
        }
    }
