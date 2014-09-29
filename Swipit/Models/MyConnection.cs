using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR;
using System.Threading.Tasks;
using SignalR.Transports;
using SignalR.Hosting.AspNet;
using SwipIt.Models;


namespace SwipIt.Async
{
    public class MyConnection : PersistentConnection
    {

        //protected override Task OnConnectedAsync(IRequest request, string connectionId)
        //{
        //    return Connection.Broadcast("Connection " + connectionId + " connected");
        //}


        protected override Task OnReceivedAsync(IRequest request, string connectionId, string data)
        {
            // Broadcast data to all clients
            return Connection.Broadcast(data);
        }
    }





}