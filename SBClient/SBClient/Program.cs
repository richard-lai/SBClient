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
        {
            Console.WriteLine("Connecting");
            //var _messagingFactory = MessagingFactory.Create();
            //var _namespaceManager = NamespaceManager.Create();

            NetworkCredential credential = new NetworkCredential("richard.lai", "I.am.a.pirate101");
            var sbUriList = new List<Uri>() { new UriBuilder { Scheme = "sb", Host = "richlaisbhost", Path = "ServiceBusDefaultNamespace" }.Uri };

            var httpsUriList = new List<Uri>() { new UriBuilder { Scheme = "https", Host = "richlaisbhost", Path = "ServiceBusDefaultNamespace", Port = 9355 }.Uri };
            TokenProvider tokenProvider = TokenProvider.CreateOAuthTokenProvider(httpsUriList, credential);

            MessagingFactory messagingFactory = MessagingFactory.Create(sbUriList, tokenProvider);

            ServiceBusConnectionStringBuilder connBuilder = new ServiceBusConnectionStringBuilder { ManagementPort = 9355, RuntimePort = 9354 };
            connBuilder.Endpoints.Add(sbUriList[0]);
            connBuilder.StsEndpoints.Add(httpsUriList[0]);

            NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(connBuilder.ToString());
            namespaceManager.Settings.TokenProvider = tokenProvider;

            Console.WriteLine("Checking");
            if (!namespaceManager.QueueExists("MyQueue"))
            {
                Console.WriteLine("Creating");
                namespaceManager.CreateQueue("MyQueue");
            }

            Console.WriteLine("Finished");
        }
    }
}
