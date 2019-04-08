using System.Collections.Generic;
using System.Linq;

namespace ContaCorrente.CrossCutting.Notifications
{
    public class Notifications
    {
        private int _statusCode = 400;
        private IList<string> _notifications = new List<string>();
        
        public void AddNotification(string message, int statusCode)
        {
            _statusCode = statusCode;
            _notifications.Add(message);
        }

        public void AddNotification(string message)
        {
            _notifications.Add(message);
        }

        public int GetStatusCode()
        {
            return _statusCode;
        }

        public IEnumerable<string> GetMessages()
        {
            return _notifications.ToArray();
        }

        public void Clear()
        {
            _notifications.Clear();
        }

        public virtual bool HasNotifications()
        {
            return _notifications.Any();
        }

        private static Notifications _instance;

        public static Notifications Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Notifications();
                }
                return _instance;
            }
        }

        private Notifications()
        {
        }
    }
}