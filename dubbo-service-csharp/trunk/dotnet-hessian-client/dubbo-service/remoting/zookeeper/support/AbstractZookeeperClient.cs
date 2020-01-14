using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace com.alibaba.dubbo.remoting.zookeeper.support
{
   

    using URL = com.alibaba.dubbo.common.URL;
	

	public abstract class AbstractZookeeperClient<TargetChildListener> : ZookeeperClient
	{
		public abstract bool Connected {get;}
		public abstract IList<string> getChildren(string path,bool watch);
		public abstract void delete(string path);
	    public abstract bool ExistsDubboNode();


        public static readonly log4net.ILog logger = log4net.LogManager.GetLogger("AbstractZookeeperClient");

        private readonly ZooURL url;

		private readonly ConcurrentBag<StateListener> stateListeners = new ConcurrentBag<StateListener>();
         
		private readonly ConcurrentDictionary<string, ConcurrentDictionary<ChildListener, TargetChildListener>> childListeners = new ConcurrentDictionary<string, ConcurrentDictionary<ChildListener, TargetChildListener>>();

		private volatile bool closed = false;

		public AbstractZookeeperClient(ZooURL url)
		{
			this.url = url;
		}

		public virtual ZooURL Url
		{
			get
			{
				return url;
			}
		}

	  

	    public virtual void create(string path, bool ephemeral)
		{
			int i = path.LastIndexOf('/');
			if (i > 0)
			{
				create(path.Substring(0, i), false);
			}
			if (ephemeral)
			{
				createEphemeral(path);
			}
			else
			{
				createPersistent(path);
			}
		}

		public virtual void addStateListener(StateListener listener)
		{
			stateListeners.Add(listener);
		}

		public virtual void removeStateListener(StateListener listener)
		{
            stateListeners.TryTake(out listener);
		}

		public virtual ConcurrentBag<StateListener> SessionListeners
		{
			get
			{
				return stateListeners;
			}
		}

		public virtual IList<string> addChildListener(string path, ChildListener listener)
		{
            ConcurrentDictionary<ChildListener, TargetChildListener> listeners = new ConcurrentDictionary<ChildListener, TargetChildListener>();
		    childListeners.TryGetValue(path, out listeners);
            if (listeners == null)
			{
				childListeners.TryAdd(path, new ConcurrentDictionary<ChildListener, TargetChildListener>());
				//listeners = childListeners.get(path);
			}
			TargetChildListener targetListener = createTargetChildListener(path, listener);
		    listeners.TryGetValue(listener, out targetListener);
			if (targetListener == null)
			{
				listeners.TryAdd(listener, createTargetChildListener(path, listener));
				//targetListener = listeners.get(listener);
			}
			return addTargetChildListener(path, targetListener);
		}

		public virtual void removeChildListener(string path, ChildListener listener)
		{
            ConcurrentDictionary<ChildListener, TargetChildListener> listeners = new ConcurrentDictionary<ChildListener, TargetChildListener>();
            childListeners.TryGetValue(path,out listeners);
			if (listeners != null)
			{
                TargetChildListener targetListener = createTargetChildListener(path, listener);        
                bool isTry = listeners.TryRemove(listener,out targetListener);
				if (isTry)
				{
					removeTargetChildListener(path, targetListener);
				}
			}
		}

		protected internal virtual void stateChanged(int state)
		{
			foreach (StateListener sessionListener in SessionListeners)
			{
				sessionListener.stateChanged(state);
			}
		}

		public virtual void close()
		{
			if (closed)
			{
				return;
			}
			closed = true;
			try
			{
				doClose();
			}
			catch (Exception t)
			{
				logger.Warn(t.Message, t);
			}
		}

        public  abstract void doClose();

		public abstract void createPersistent(string path);

        public abstract void createEphemeral(string path);

        public abstract TargetChildListener createTargetChildListener(string path, ChildListener listener);

        public abstract IList<string> addTargetChildListener(string path, TargetChildListener listener);

        public abstract void removeTargetChildListener(string path, TargetChildListener listener);

	}

}