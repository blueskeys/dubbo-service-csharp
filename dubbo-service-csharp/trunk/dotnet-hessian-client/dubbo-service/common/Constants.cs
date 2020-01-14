using System;
using System.Text.RegularExpressions;


namespace com.alibaba.dubbo.common
{


	/// <summary>
	/// Constants
	/// 
	/// @author william.liangf
	/// </summary>
	public class Constants
	{

		public const string PROVIDER = "provider";

		public const string CONSUMER = "consumer";

		public const string REGISTER = "register";

		public const string UNREGISTER = "unregister";

		public const string SUBSCRIBE = "subscribe";

		public const string UNSUBSCRIBE = "unsubscribe";

		public const string CATEGORY_KEY = "category";

		public const string PROVIDERS_CATEGORY = "providers";

		public const string CONSUMERS_CATEGORY = "consumers";

		public const string ROUTERS_CATEGORY = "routers";

		public const string CONFIGURATORS_CATEGORY = "configurators";

		public const string DEFAULT_CATEGORY = PROVIDERS_CATEGORY;

		public const string ENABLED_KEY = "enabled";

		public const string DISABLED_KEY = "disabled";

		public const string VALIDATION_KEY = "validation";

		public const string CACHE_KEY = "cache";

		public const string DYNAMIC_KEY = "dynamic";

		public const string DUBBO_PROPERTIES_KEY = "dubbo.properties.file";

		public const string DEFAULT_DUBBO_PROPERTIES = "dubbo.properties";

		public const string SENT_KEY = "sent";

		public const bool DEFAULT_SENT = false;

		public const string REGISTRY_PROTOCOL = "registry";

		public const string @INVOKE = "$invoke";

		public const string @ECHO = "$echo";

//		public static readonly int DEFAULT_IO_THREADS = Runtime.Runtime.availableProcessors() + 1;

		public const string DEFAULT_PROXY = "javassist";

		public const int DEFAULT_PAYLOAD = 8 * 1024 * 1024; // 8M

		public const string DEFAULT_CLUSTER = "failover";

		public const string DEFAULT_DIRECTORY = "dubbo";

		public const string DEFAULT_LOADBALANCE = "random";

		public const string DEFAULT_PROTOCOL = "dubbo";

		public const string DEFAULT_EXCHANGER = "header";

		public const string DEFAULT_TRANSPORTER = "netty";

		public const string DEFAULT_REMOTING_SERVER = "netty";

		public const string DEFAULT_REMOTING_CLIENT = "netty";

		public const string DEFAULT_REMOTING_CODEC = "dubbo";

		public const string DEFAULT_REMOTING_SERIALIZATION = "hessian2";

		public const string DEFAULT_HTTP_SERVER = "servlet";

		public const string DEFAULT_HTTP_CLIENT = "jdk";

		public const string DEFAULT_HTTP_SERIALIZATION = "json";

		public const string DEFAULT_CHARSET = "UTF-8";

		public const int DEFAULT_WEIGHT = 100;

		public const int DEFAULT_FORKS = 2;

		public const string DEFAULT_THREAD_NAME = "Dubbo";

		public const int DEFAULT_CORE_THREADS = 0;

		public const int DEFAULT_THREADS = 200;

		public const bool DEFAULT_KEEP_ALIVE = true;

		public const int DEFAULT_QUEUES = 0;

		public const int DEFAULT_ALIVE = 60 * 1000;

		public const int DEFAULT_CONNECTIONS = 0;

		public const int DEFAULT_ACCEPTS = 0;

		public const int DEFAULT_IDLE_TIMEOUT = 600 * 1000;

		public const int DEFAULT_HEARTBEAT = 60 * 1000;

		public const int DEFAULT_TIMEOUT = 1000;

		public const int DEFAULT_CONNECT_TIMEOUT = 3000;

		public const int DEFAULT_REGISTRY_CONNECT_TIMEOUT = 5000;

		public const int DEFAULT_RETRIES = 2;

		// default buffer size is 8k.
		public const int DEFAULT_BUFFER_SIZE = 8 * 1024;

		public const int MAX_BUFFER_SIZE = 16 * 1024;

		public const int MIN_BUFFER_SIZE = 1 * 1024;

		public const string REMOVE_VALUE_PREFIX = "-";

		public const string HIDE_KEY_PREFIX = ".";

		public const string DEFAULT_KEY_PREFIX = "default.";

		public const string DEFAULT_KEY = "default";

		public const string LOADBALANCE_KEY = "loadbalance";

		// key for router type, for e.g., "script"/"file",  corresponding to ScriptRouterFactory.NAME, FileRouterFactory.NAME 
		public const string ROUTER_KEY = "router";

		public const string CLUSTER_KEY = "cluster";

		public const string REGISTRY_KEY = "registry";

		public const string MONITOR_KEY = "monitor";

		public const string SIDE_KEY = "side";

		public const string PROVIDER_SIDE = "provider";

		public const string CONSUMER_SIDE = "consumer";

		public const string DEFAULT_REGISTRY = "dubbo";

		public const string BACKUP_KEY = "backup";

		public const string DIRECTORY_KEY = "directory";

		public const string DEPRECATED_KEY = "deprecated";

		public const string ANYHOST_KEY = "anyhost";

		public const string ANYHOST_VALUE = "0.0.0.0";

		public const string LOCALHOST_KEY = "localhost";

		public const string LOCALHOST_VALUE = "127.0.0.1";

		public const string APPLICATION_KEY = "application";

		public const string LOCAL_KEY = "local";

		public const string STUB_KEY = "stub";

		public const string MOCK_KEY = "mock";

		public const string PROTOCOL_KEY = "protocol";

		public const string PROXY_KEY = "proxy";

		public const string WEIGHT_KEY = "weight";

		public const string FORKS_KEY = "forks";

		public const string DEFAULT_THREADPOOL = "limited";

		public const string DEFAULT_CLIENT_THREADPOOL = "cached";

		public const string THREADPOOL_KEY = "threadpool";

		public const string THREAD_NAME_KEY = "threadname";

		public const string IO_THREADS_KEY = "iothreads";

		public const string CORE_THREADS_KEY = "corethreads";

		public const string THREADS_KEY = "threads";

		public const string QUEUES_KEY = "queues";

		public const string ALIVE_KEY = "alive";

		public const string EXECUTES_KEY = "executes";

		public const string BUFFER_KEY = "buffer";

		public const string PAYLOAD_KEY = "payload";

		public const string REFERENCE_FILTER_KEY = "reference.filter";

		public const string INVOKER_LISTENER_KEY = "invoker.listener";

		public const string SERVICE_FILTER_KEY = "service.filter";

		public const string EXPORTER_LISTENER_KEY = "exporter.listener";

		public const string ACCESS_LOG_KEY = "accesslog";

		public const string ACTIVES_KEY = "actives";

		public const string CONNECTIONS_KEY = "connections";

		public const string ACCEPTS_KEY = "accepts";

		public const string IDLE_TIMEOUT_KEY = "idle.timeout";

		public const string HEARTBEAT_KEY = "heartbeat";

		public const string HEARTBEAT_TIMEOUT_KEY = "heartbeat.timeout";

		public const string CONNECT_TIMEOUT_KEY = "connect.timeout";

		public const string TIMEOUT_KEY = "timeout";

		public const string RETRIES_KEY = "retries";

		public const string PROMPT_KEY = "prompt";

		public const string DEFAULT_PROMPT = "dubbo>";

		public const string CODEC_KEY = "codec";

		public const string SERIALIZATION_KEY = "serialization";

		// modified by lishen
		public const string EXTENSION_KEY = "extension";

		// modified by lishen
		public const string KEEP_ALIVE_KEY = "keepalive";

		// modified by lishen
		// TODO change to a better name
		public const string OPTIMIZER_KEY = "optimizer";

		public const string EXCHANGER_KEY = "exchanger";

		public const string TRANSPORTER_KEY = "transporter";

		public const string SERVER_KEY = "server";

		public const string CLIENT_KEY = "client";

		public const string ID_KEY = "id";

		public const string ASYNC_KEY = "async";

		public const string RETURN_KEY = "return";

		public const string TOKEN_KEY = "token";

		public const string METHOD_KEY = "method";

		public const string METHODS_KEY = "methods";

		public const string CHARSET_KEY = "charset";

		public const string RECONNECT_KEY = "reconnect";

		public const string SEND_RECONNECT_KEY = "send.reconnect";

		public const int DEFAULT_RECONNECT_PERIOD = 2000;

		public const string SHUTDOWN_TIMEOUT_KEY = "shutdown.timeout";

		public const int DEFAULT_SHUTDOWN_TIMEOUT = 1000 * 60 * 15;

		public const string PID_KEY = "pid";

		public const string TIMESTAMP_KEY = "timestamp";

		public const string WARMUP_KEY = "warmup";

		public const int DEFAULT_WARMUP = 10 * 60 * 1000;

		public const string CHECK_KEY = "check";

		public const string REGISTER_KEY = "register";

		public const string SUBSCRIBE_KEY = "subscribe";

		public const string GROUP_KEY = "group";

		public const string PATH_KEY = "path";

		public const string INTERFACE_KEY = "interface";

		public const string GENERIC_KEY = "generic";

		public const string FILE_KEY = "file";

		public const string WAIT_KEY = "wait";

		public const string CLASSIFIER_KEY = "classifier";

		public const string VERSION_KEY = "version";

		public const string REVISION_KEY = "revision";

		public const string DUBBO_VERSION_KEY = "dubbo";

		public const string HESSIAN_VERSION_KEY = "hessian.version";

		public const string DISPATCHER_KEY = "dispatcher";

		public const string CHANNEL_HANDLER_KEY = "channel.handler";

		public const string DEFAULT_CHANNEL_HANDLER = "default";

		public const string ANY_VALUE = "*";

		public const string COMMA_SEPARATOR = ",";

		public static readonly Regex COMMA_SPLIT_PATTERN = new Regex("\\s*[,]+\\s*");

		public const string PATH_SEPARATOR = "/";

		public const string REGISTRY_SEPARATOR = "|";

		public static readonly Regex REGISTRY_SPLIT_PATTERN = new Regex("\\s*[|;]+\\s*");

		public const string SEMICOLON_SEPARATOR = ";";

		public static readonly Regex SEMICOLON_SPLIT_PATTERN = new Regex("\\s*[;]+\\s*");

		public const string CONNECT_QUEUE_CAPACITY = "connect.queue.capacity";

		public const string CONNECT_QUEUE_WARNING_SIZE = "connect.queue.warning.size";

		public const int DEFAULT_CONNECT_QUEUE_WARNING_SIZE = 1000;

		public const string CHANNEL_ATTRIBUTE_READONLY_KEY = "channel.readonly";

		public const string CHANNEL_READONLYEVENT_SENT_KEY = "channel.readonly.sent";

		public const string CHANNEL_SEND_READONLYEVENT_KEY = "channel.readonly.send";

		public const string COUNT_PROTOCOL = "count";

		public const string TRACE_PROTOCOL = "trace";

		public const string EMPTY_PROTOCOL = "empty";

		public const string ADMIN_PROTOCOL = "admin";

		public const string PROVIDER_PROTOCOL = "provider";

		public const string CONSUMER_PROTOCOL = "consumer";

		public const string ROUTE_PROTOCOL = "route";

		public const string SCRIPT_PROTOCOL = "script";

		public const string CONDITION_PROTOCOL = "condition";

		public const string MOCK_PROTOCOL = "mock";

		public const string RETURN_PREFIX = "return ";

		public const string THROW_PREFIX = "throw";

		public const string FAIL_PREFIX = "fail:";

		public const string FORCE_PREFIX = "force:";

		public const string FORCE_KEY = "force";

		public const string MERGER_KEY = "merger";

		/// <summary>
		/// 集群时是否排除非available的invoker
		/// </summary>
		public const string CLUSTER_AVAILABLE_CHECK_KEY = "cluster.availablecheck";

		public const bool DEFAULT_CLUSTER_AVAILABLE_CHECK = true;

		/// <summary>
		/// 集群时是否启用sticky策略
		/// </summary>
		public const string CLUSTER_STICKY_KEY = "sticky";

		/// <summary>
		/// sticky默认�?
		/// </summary>
		public const bool DEFAULT_CLUSTER_STICKY = false;

		/// <summary>
		/// 创建client时，是否先要建立连接�?
		/// </summary>
		public const string LAZY_CONNECT_KEY = "lazy";

		/// <summary>
		/// lazy连接的初始状态是连接状态还是非连接状态？
		/// </summary>
		public const string LAZY_CONNECT_INITIAL_STATE_KEY = "connect.lazy.initial.state";

		/// <summary>
		/// lazy连接的初始状态默认是连接状�?
		/// </summary>
		public const bool DEFAULT_LAZY_CONNECT_INITIAL_STATE = true;

		/// <summary>
		/// 注册中心是否同步存储文件，默认异�?
		/// </summary>
		public const string REGISTRY_FILESAVE_SYNC_KEY = "save.file";

		/// <summary>
		/// 注册中心失败事件重试事件
		/// </summary>
		public const string REGISTRY_RETRY_PERIOD_KEY = "retry.period";

		/// <summary>
		/// 重试周期
		/// </summary>
		public const int DEFAULT_REGISTRY_RETRY_PERIOD = 5 * 1000;

		/// <summary>
		/// 注册中心自动重连时间
		/// </summary>
		public const string REGISTRY_RECONNECT_PERIOD_KEY = "reconnect.period";

		public const int DEFAULT_REGISTRY_RECONNECT_PERIOD = 3 * 1000;

		public const string SESSION_TIMEOUT_KEY = "session";

		public const int DEFAULT_SESSION_TIMEOUT = 60 * 1000;

		/// <summary>
		/// 注册中心导出URL参数的KEY
		/// </summary>
		public const string EXPORT_KEY = "export";

		/// <summary>
		/// 注册中心引用URL参数的KEY
		/// </summary>
		public const string REFER_KEY = "refer";

		/// <summary>
		/// callback inst id
		/// </summary>
		public const string CALLBACK_SERVICE_KEY = "callback.service.instid";

		/// <summary>
		/// 每个客户端同一个接�?callback服务实例的限�?
		/// </summary>
		public const string CALLBACK_INSTANCES_LIMIT_KEY = "callbacks";

		/// <summary>
		/// 每个客户端同一个接�?callback服务实例的限�?
		/// </summary>
		public const int DEFAULT_CALLBACK_INSTANCES = 1;

		public const string CALLBACK_SERVICE_PROXY_KEY = "callback.service.proxy";

		public const string IS_CALLBACK_SERVICE = "is_callback_service";

		/// <summary>
		/// channel中callback的invokers
		/// </summary>
		public const string CHANNEL_CALLBACK_KEY = "channel.callback.invokers.key";

		[Obsolete]
		public const string SHUTDOWN_WAIT_SECONDS_KEY = "dubbo.service.shutdown.wait.seconds";

		public const string SHUTDOWN_WAIT_KEY = "dubbo.service.shutdown.wait";

		public const string IS_SERVER_KEY = "isserver";

		/// <summary>
		/// 默认值毫秒，避免重新计算.
		/// </summary>
		public const int DEFAULT_SERVER_SHUTDOWN_TIMEOUT = 10000;

		public const string ON_CONNECT_KEY = "onconnect";

		public const string ON_DISCONNECT_KEY = "ondisconnect";

		public const string ON_INVOKE_METHOD_KEY = "oninvoke.method";

		public const string ON_RETURN_METHOD_KEY = "onreturn.method";

		public const string ON_THROW_METHOD_KEY = "onthrow.method";

		public const string ON_INVOKE_INSTANCE_KEY = "oninvoke.instance";

		public const string ON_RETURN_INSTANCE_KEY = "onreturn.instance";

		public const string ON_THROW_INSTANCE_KEY = "onthrow.instance";

		public const string OVERRIDE_PROTOCOL = "override";

		public const string PRIORITY_KEY = "priority";

		public const string RULE_KEY = "rule";

		public const string TYPE_KEY = "type";

		public const string RUNTIME_KEY = "runtime";

		// when ROUTER_KEY's value is set to ROUTER_TYPE_CLEAR, RegistryDirectory will clean all current routers
		public const string ROUTER_TYPE_CLEAR = "clean";

		public const string DEFAULT_SCRIPT_TYPE_KEY = "javascript";

		public const string STUB_EVENT_KEY = "dubbo.stub.event";

		public const bool DEFAULT_STUB_EVENT = false;

		public const string STUB_EVENT_METHODS_KEY = "dubbo.stub.event.methods";

		//invocation attachment属性中如果有此值，则选择mock invoker
		public const string INVOCATION_NEED_MOCK = "invocation.need.mock";

		public const string LOCAL_PROTOCOL = "injvm";

		public const string AUTO_ATTACH_INVOCATIONID_KEY = "invocationid.autoattach";

		public const string SCOPE_KEY = "scope";

		public const string SCOPE_LOCAL = "local";

		public const string SCOPE_REMOTE = "remote";

		public const string SCOPE_NONE = "none";

		public const string RELIABLE_PROTOCOL = "napoli";

		public const string TPS_LIMIT_RATE_KEY = "tps";

		public const string TPS_LIMIT_INTERVAL_KEY = "tps.interval";

		public const long DEFAULT_TPS_LIMIT_INTERVAL = 60 * 1000;

		public const string DECODE_IN_IO_THREAD_KEY = "decode.in.io";

		public const bool DEFAULT_DECODE_IN_IO_THREAD = true;

		public const string INPUT_KEY = "input";

		public const string OUTPUT_KEY = "output";

//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
//		public static readonly string EXECUTOR_SERVICE_COMPONENT_KEY = typeof(ExecutorService).FullName;

		public const string GENERIC_SERIALIZATION_NATIVE_JAVA = "nativejava";

		public const string GENERIC_SERIALIZATION_DEFAULT = "true";

		public const string GENERIC_SERIALIZATION_BEAN = "bean";

		/*
		 * private Constants(){ }
		 */

	}

}