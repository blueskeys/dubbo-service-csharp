using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooKeeperNet;

namespace dotnet_hessian_client.zookeeper
{
    class Watcher : IWatcher
    {
        public void Process(WatchedEvent @event)
        {
            //if (@event.Type == EventType.NodeDataChanged)
            //{
            Console.WriteLine("已经触发了" + @event.Type + "事件！");
            //}
        }
    }
}
