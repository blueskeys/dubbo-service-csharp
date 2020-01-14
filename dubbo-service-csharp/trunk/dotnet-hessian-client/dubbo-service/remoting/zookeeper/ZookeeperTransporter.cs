namespace com.alibaba.dubbo.remoting.zookeeper
{

	using Constants = com.alibaba.dubbo.common.Constants;
	using URL = com.alibaba.dubbo.common.URL;

	public interface ZookeeperTransporter
	{
		ZookeeperClient connect(ZooURL url);

	}

}