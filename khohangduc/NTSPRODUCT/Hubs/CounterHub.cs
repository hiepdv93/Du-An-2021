using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace NTSPRODUCT.Hubs
{
    public class CounterHub : Hub
    {
        public long counter = 0;
        public long counterAll = 1;
        public override Task OnConnected()
        {
            if (counterAll == 1)
            {
                counterAll = new CounterInit().GetCountTotal();
            }
            counterAll++;
            counter = counter + 1;
            Clients.All.UpdateCount(counter, counterAll);
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            counter = counter - 1;
            Clients.All.UpdateCount(counter, counterAll);
            return base.OnDisconnected(stopCalled);
        }
    }
}