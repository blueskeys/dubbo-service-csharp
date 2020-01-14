namespace com.alibaba.dubbo.remoting.zookeeper
{

	public interface StateListener
	{
		void stateChanged(int connected);

	}

	public static class StateListener_Fields
	{
		public const int DISCONNECTED = 0;
		public const int CONNECTED = 1;
		public const int RECONNECTED = 2;
	}

}