using System.Collections.Generic;

namespace com.alibaba.dubbo.remoting.zookeeper
{

	public interface ChildListener
	{

		void childChanged(string path, IList<string> children);

	}

}