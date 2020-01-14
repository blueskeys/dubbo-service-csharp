using System;
using System.Collections.Generic;
using com.alibaba.dubbo.common;
using com.alibaba.dubbo.remoting.zookeeper.support;
using ZookeeperViewer.Component;
using ZooKeeperNet;

namespace com.alibaba.dubbo.remoting.zookeeper.zkclient
{
    public class ZkclientZookeeperClient : AbstractZookeeperClient<IWatcher>
    {
        private readonly ZooKeeper zooKeeper;

        private volatile KeeperState state = KeeperState.SyncConnected;

        public ZkclientZookeeperClient(ZooURL url) : base(url)
        {
            zooKeeper = new ZooKeeper(url.Url,
                TimeSpan.FromSeconds(url.SessionTimeout),
                new ZookeeperWatcher(this));
        }

        public override bool Connected
        {
            get { return state == KeeperState.SyncConnected; }
        }

        public override void createPersistent(string path)
        {
            try
            {
                var i = path.LastIndexOf('/');
                var value = path.Substring(i + 1);
                zooKeeper.Create(path, value.GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
            }
            catch (Exception)
            {
            }
        }

        public override void createEphemeral(string path)
        {
            try
            {
                var i = path.LastIndexOf('/');
                var value = path.Substring(i + 1);
                zooKeeper.Create(path, value.GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Ephemeral);
            }
            catch (Exception)
            {
            }
        }


        public override void delete(string path)
        {
            try
            {
                zooKeeper.Delete(path, -1);
            }
            catch (Exception)
            {
            }
        }

        public override bool ExistsDubboNode()
        {
            return zooKeeper.Exists("/" + Constants.DEFAULT_DIRECTORY, false) != null;
        }

        public override IList<string> getChildren(string path, bool watch)
        {
            try
            {
                return (IList<string>) zooKeeper.GetChildren(path, true);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override void doClose()
        {
            zooKeeper.Dispose();
        }

        public override IWatcher createTargetChildListener(string path, ChildListener listener)
        {
            throw new NotImplementedException();
        }

        public override IList<string> addTargetChildListener(string path, IWatcher listener)
        {
//            return client.subscribeChildChanges(path, listener);
            return null;
        }

        public override void removeTargetChildListener(string path, IWatcher listener)
        {
        }

        private class IZkStateListenerAnonymousInnerClass : IWatcher
        {
            private readonly ZkclientZookeeperClient outerInstance;

            public IZkStateListenerAnonymousInnerClass(ZkclientZookeeperClient outerInstance)
            {
                this.outerInstance = outerInstance;
            }

            public void Process(WatchedEvent @event)
            {
                outerInstance.state = @event.State;
                if (@event.State == KeeperState.Disconnected)
                    outerInstance.stateChanged(StateListener_Fields.DISCONNECTED);
                else if (@event.State == KeeperState.SyncConnected)
                    outerInstance.stateChanged(StateListener_Fields.CONNECTED);
            }

            public virtual void handleNewSession()
            {
                outerInstance.stateChanged(StateListener_Fields.RECONNECTED);
            }
        }
    }
}