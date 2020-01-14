using System;
using com.alibaba.dubbo.remoting.zookeeper.zkclient;
using com.alibaba.dubbo.service;
using ZooKeeperNet;

namespace ZookeeperViewer.Component
{
    /// <summary>
    /// 
    /// </summary>
    public class ZookeeperWatcher : IWatcher
    {
        private ZkclientZookeeperClient zkclient;

        public ZookeeperWatcher(ZkclientZookeeperClient zkclient)
        {
            this.zkclient = zkclient;
        }

        public void Process(WatchedEvent @event)
        {
            ProcessWatchedEvent(@event);
        }

        public void ProcessWatchedEvent(WatchedEvent @event)
        {
            if (!string.IsNullOrWhiteSpace(@event.Path))
            {
                try
                {
                    var path = @event.Path.Substring(0, @event.Path.LastIndexOf('/'));
                    var name = @event.Path.Substring(@event.Path.LastIndexOf('/') + 1);
                    var pathNames = @event.Path.Split('/');
                    switch (@event.Type)
                    {
                        case EventType.NodeCreated:
                            Console.WriteLine("KeeperState.NodeCreated, path=" + path + ",name" + name);
                            ServiceConsumerContainer.Instance().UpdateProviders(pathNames[2]);
                            break;
                        case EventType.NodeDeleted:
                            Console.WriteLine("KeeperState.NodeDeleted, path=" + path + ",name" + name);
                            ServiceConsumerContainer.Instance().UpdateProviders(pathNames[2]);
                            break;
                        case EventType.NodeChildrenChanged:
                            Console.WriteLine("KeeperState.NodeChildrenChanged, path=" + path + ",name" + name);
                            ServiceConsumerContainer.Instance().UpdateProviders(pathNames[2]);
                            break;
                    }
                }
                catch
                {
                }
            }
            else
            {
                if (@event.State == KeeperState.Disconnected)
                    Console.WriteLine("KeeperState.Disconnected");
                else if (@event.State == KeeperState.SyncConnected)
                    Console.WriteLine("KeeperState.SyncConnected");
            }
        }
    }
}