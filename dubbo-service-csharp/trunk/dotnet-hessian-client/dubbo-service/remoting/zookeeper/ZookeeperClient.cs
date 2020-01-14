using System.Collections.Generic;

namespace com.alibaba.dubbo.remoting.zookeeper
{

	using URL = com.alibaba.dubbo.common.URL;

	public interface ZookeeperClient
	{

		void create(string path, bool ephemeral);

		void delete(string path);

		IList<string> getChildren(string path,bool watch);

       
		IList<string> addChildListener(string path, ChildListener listener);

		void removeChildListener(string path, ChildListener listener);

		void addStateListener(StateListener listener);

		void removeStateListener(StateListener listener);

		bool Connected {get;}

		void close();

        ZooURL Url {get;}

	    bool ExistsDubboNode();
	}

}