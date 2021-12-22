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
        static public long counter = 0;
        static public long counterAll = 1;
        static public long counterMonth = 0;
        static public long month = 0;

        public override Task OnConnected()
        {
            var monthNow = DateTime.Now;
            if (month != monthNow.Month)
            {
                month = monthNow.Month;
                counterMonth = new CounterInit().GetCountMonth(monthNow.Month, monthNow.Year);
            }
            if (counterAll == 1)
            {
                counterAll = new CounterInit().GetCountTotal();
            }
            counterMonth++;
            counterAll++;
            counter = counter + 1;
            Clients.All.UpdateCount(counter, counterAll, counterMonth);
            return base.OnConnected();
        }
        public override Task OnReconnected()
        {
            counter = counter + 1;
            Clients.All.UpdateCount(counter, counterAll, counterMonth);
            return base.OnReconnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            counter = counter - 1;
            Clients.All.UpdateCount(counter, counterAll, counterMonth);
            return base.OnDisconnected(stopCalled);
        }
    }
}