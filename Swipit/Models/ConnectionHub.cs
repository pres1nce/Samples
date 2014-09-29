using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SignalR.Hosting;

using System.Diagnostics;
using SignalR.Client;


using System.Collections.ObjectModel;
using System.Threading;



namespace ConnectionHubDemo
{


    public class ConnectionHub
    {
        public string test(string tests)
        {
            //Will this talk to my PersistentConnection?

            var connection = new Connection("http://www.vinnix.com/swipit/echo");
           // var connection = new Connection("http://localhost/swipit/echo");

            connection.Received += data =>
            {
                Console.WriteLine(data);
            };

            connection.Start().Wait();
            connection.Send(tests);



            connection.Stop();


            return tests;
        }
    }
}