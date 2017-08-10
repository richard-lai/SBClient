using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SBClient
{
    class Program
    {
        static void Main(string[] args)
        {Console.WriteLine("Connecting");
            var _messagingFactory = MessagingFactory.Create();
            var _namespaceManager = NamespaceManager.Create();

            _namespaceManager.Settings.TokenProvider = TokenProvider.CreateOAuthTokenProvider(new Uri[] { new Uri("https://richlaisbhost:9355/ServiceBusDefaultNamespace") }, new NetworkCredential("richard.lai", "I.am.a.pirate101"));
            _messagingFactory.GetSettings().TokenProvider = TokenProvider.CreateOAuthTokenProvider(new Uri[] { new Uri("https://richlaisbhost:9355/ServiceBusDefaultNamespace") }, new NetworkCredential("richard.lai", "I.am.a.pirate101"));

            Console.WriteLine("Checking");
            if (!_namespaceManager.QueueExists("MyQueue"))
            {
                Console.WriteLine("Creating");
                _namespaceManager.CreateQueue("MyQueue");
            }

            QueueClient client = _messagingFactory.CreateQueueClient("MyQueue");
            client.Send(new BrokeredMessage("aha"));

            Console.WriteLine("Finished");
        }
    }
}
