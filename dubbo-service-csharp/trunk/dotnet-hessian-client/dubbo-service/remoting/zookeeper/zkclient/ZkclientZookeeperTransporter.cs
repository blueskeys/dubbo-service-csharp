using System;

namespace com.alibaba.dubbo.remoting.zookeeper.zkclient
{

	public class ZkclientZookeeperTransporter : ZookeeperTransporter
	{

		public virtual ZookeeperClient connect(ZooURL url)
		{
			return new ZkclientZookeeperClient(url);
		}

	}

}