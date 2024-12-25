using ChatAppAPI.Models;
using System.Collections.Concurrent;

namespace ChatAppAPI.DataService
{
    public class ShareDb
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connection = new ();

        public ConcurrentDictionary<string, UserConnection> connection => _connection;
    }
}
