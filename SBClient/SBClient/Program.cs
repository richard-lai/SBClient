using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
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



            //Console.WriteLine("Checking");
            //if (!_namespaceManager.QueueExists("MyQueue"))
            //{
            //    Console.WriteLine("Creating");
            //    _namespaceManager.CreateQueue("MyQueue");
            //}

            QueueClient client = _messagingFactory.CreateQueueClient("MyQueue");
            client.Send(new BrokeredMessage("aha"));

            Console.WriteLine("Finished");
        }
    }
}
