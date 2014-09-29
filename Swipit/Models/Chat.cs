using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;

namespace SwipIt.Models
{
    public class Chat : Hub
    {
        public void Distribute(string message)
        {
            Clients.receive(Caller.name, message);
        }
    }
}