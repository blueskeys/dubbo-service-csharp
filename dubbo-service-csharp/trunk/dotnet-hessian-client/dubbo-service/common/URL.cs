using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Web;


namespace com.alibaba.dubbo.common
{
    using System.Linq;
//    using CollectionUtils = com.alibaba.dubbo.common.utils.CollectionUtils;
//    using NetUtils = com.alibaba.dubbo.common.utils.NetUtils;
    using StringUtils = com.alibaba.dubbo.common.utils.StringUtils;

    [Serializable]
	public sealed class URL
	{

		private const long serialVersionUID = -1985165475234910535L;

		private readonly string protocol;

		private readonly string username;

		private readonly string password;

		private readonly string host;

		private readonly int port;

		private readonly string path;

		private readonly IDictionary<string, string> parameters;

		// ==== cache ====

		[NonSerialized]
		private volatile IDictionary<string, object> numbers;

		[NonSerialized]
		private volatile IDictionary<string, URL> urls;

		[NonSerialized]
		private volatile string ip;

		[NonSerialized]
		private volatile string full;

		[NonSerialized]
		private volatile string identity;

		[NonSerialized]
		private volatile string parameter;

		[NonSerialized]
		private volatile string @string;

		internal URL()
		{
			this.protocol = null;
			this.username = null;
			this.password = null;
			this.host = null;
			this.port = 0;
			this.path = null;
			this.parameters = null;
		}

		public URL(string protocol, string host, int port) : this(protocol, null, null, host, port, null, (IDictionary<string, string>) null)
		{
		}

//		public URL(string protocol, string host, int port, string[] pairs) : this(protocol, null, null, host, port, null, CollectionUtils.toStringMap(pairs))
//		{
//		}

		public URL(string protocol, string host, int port, IDictionary<string, string> parameters) : this(protocol, null, null, host, port, null, parameters)
		{
		}

		public URL(string protocol, string host, int port, string path) : this(protocol, null, null, host, port, path, (IDictionary<string, string>) null)
		{
		}

//		public URL(string protocol, string host, int port, string path, params string[] pairs) : this(protocol, null, null, host, port, path, CollectionUtils.toStringMap(pairs))
//		{
//		}

		public URL(string protocol, string host, int port, string path, IDictionary<string, string> parameters) : this(protocol, null, null, host, port, path, parameters)
		{
		}

		public URL(string protocol, string username, string password, string host, int port, string path) : this(protocol, username, password, host, port, path, (IDictionary<string, string>) null)
		{
		}

//		public URL(string protocol, string username, string password, string host, int port, string path, params string[] pairs) : this(protocol, username, password, host, port, path, CollectionUtils.toStringMap(pairs))
//		{
//		}

		public URL(string protocol, string username, string password, string host, int port, string path, IDictionary<string, string> parameters)
		{
			if ((username == null || username.Length == 0) && password != null && password.Length > 0)
			{
				throw new System.ArgumentException("Invalid url, password without username!");
			}
			this.protocol = protocol;
			this.username = username;
			this.password = password;
			this.host = host;
			this.port = (port < 0 ? 0 : port);
			this.path = path;
			// trim the beginning "/"
			while (path != null && path.StartsWith("/"))
			{
				path = path.Substring(1);
			}
			if (parameters == null)
			{
				parameters = new Dictionary<string, string>();
			}
			else
			{
				parameters = new Dictionary<string, string>(parameters);
			}
			this.parameters = new Dictionary<string, string>(parameters);
		}

		/// <summary>
		/// Parse url string
		/// </summary>
		/// <param name="url"> URL string </param>
		/// <returns> URL instance </returns>
		/// <seealso cref= URL </seealso>
		public static URL valueOf(string url)
		{
			if (url == null || (url = url.Trim()).Length == 0)
			{
				throw new System.ArgumentException("url == null");
			}
			string protocol = null;
			string username = null;
			string password = null;
			string host = null;
			int port = 0;
			string path = null;
			IDictionary<string, string> parameters = null;
			int i = url.IndexOf("?"); // seperator between body and parameters
			if (i >= 0)
			{
                string[] parts = url.Split(new string[] { "&" }, StringSplitOptions.None);

                parameters = new Dictionary<string, string>();
				foreach (string part1 in parts)
				{
					string part = part1.Trim();
					if (part.Length > 0)
					{
						int j = part.IndexOf('=');
						if (j >= 0)
						{
							parameters[part.Substring(0, j)] = part.Substring(j + 1);
						}
						else
						{
							parameters[part] = part;
						}
					}
				}
				url = url.Substring(0, i);
			}
			i = url.IndexOf("://");
			if (i >= 0)
			{
				if (i == 0)
				{
					throw new Exception("url missing protocol: \"" + url + "\"");
				}
				protocol = url.Substring(0, i);
				url = url.Substring(i + 3);
			}
			else
			{
				// case: file:/path/to/file.txt
				i = url.IndexOf(":/");
				if (i >= 0)
				{
					if (i == 0)
					{
						throw new Exception("url missing protocol: \"" + url + "\"");
					}
					protocol = url.Substring(0, i);
					url = url.Substring(i + 1);
				}
			}

			i = url.IndexOf("/");
			if (i >= 0)
			{
				path = url.Substring(i + 1);
				url = url.Substring(0, i);
			}
			i = url.IndexOf("@");
			if (i >= 0)
			{
				username = url.Substring(0, i);
				int j = username.IndexOf(":");
				if (j >= 0)
				{
					password = username.Substring(j + 1);
					username = username.Substring(0, j);
				}
				url = url.Substring(i + 1);
			}
			i = url.IndexOf(":");
			if (i >= 0 && i < url.Length - 1)
			{
				port = Convert.ToInt32(url.Substring(i + 1));
				url = url.Substring(0, i);
			}
			if (url.Length > 0)
			{
				host = url;
			}
			return new URL(protocol, username, password, host, port, path, parameters);
		}

		public string Protocol
		{
			get
			{
				return protocol;
			}
		}

		public string Username
		{
			get
			{
				return username;
			}
		}

		public string Password
		{
			get
			{
				return password;
			}
		}

		public string Authority
		{
			get
			{
				if ((username == null || username.Length == 0) && (password == null || password.Length == 0))
				{
					return null;
				}
				return (username == null ? "" : username) + ":" + (password == null ? "" : password);
			}
		}

		public string Host
		{
			get
			{
				return host;
			}
		}

		/// <summary>
		/// </summary>
		/// <returns> ip </returns>
//		public string Ip
//		{
//			get
//			{
//				if (ip == null)
//				{
//					ip = NetUtils.getIpByHost(host);
//				}
//				return ip;
//			}
//		}

		public int Port
		{
			get
			{
				return port;
			}
		}

		public int getPort(int defaultPort)
		{
			return port <= 0 ? defaultPort : port;
		}

		public string Address
		{
			get
			{
				return port <= 0 ? host : host + ":" + port;
			}
		}

		public string BackupAddress
		{
			get
			{
				return getBackupAddress(0);
			}
		}

		public string getBackupAddress(int defaultPort)
		{
			StringBuilder address = new StringBuilder(appendDefaultPort(Address, defaultPort));
			string[] backups = getParameter(Constants.BACKUP_KEY, new string[0]);
			if (backups != null && backups.Length > 0)
			{
				foreach (string backup in backups)
				{
					address.Append(",");
					address.Append(appendDefaultPort(backup, defaultPort));
				}
			}
			return address.ToString();
		}

		public IList<URL> BackupUrls
		{
			get
			{
				IList<URL> urls = new List<URL>();
				urls.Add(this);
				string[] backups = getParameter(Constants.BACKUP_KEY, new string[0]);
				if (backups != null && backups.Length > 0)
				{
					foreach (string backup in backups)
					{
						urls.Add(this.setAddress(backup));
					}
				}
				return urls;
			}
		}

		private string appendDefaultPort(string address, int defaultPort)
		{
			if (address != null && address.Length > 0 && defaultPort > 0)
			{
				int i = address.IndexOf(':');
				if (i < 0)
				{
					return address + ":" + defaultPort;
				}
				else if (Convert.ToInt32(address.Substring(i + 1)) == 0)
				{
					return address.Substring(0, i + 1) + defaultPort;
				}
			}
			return address;
		}

		public string Path
		{
			get
			{
				return path;
			}
		}

		public string AbsolutePath
		{
			get
			{
				if (path != null && !path.StartsWith("/"))
				{
					return "/" + path;
				}
				return path;
			}
		}

		public URL setProtocol(string protocol)
		{
			return new URL(protocol, username, password, host, port, path, Parameters);
		}

		public URL setUsername(string username)
		{
			return new URL(protocol, username, password, host, port, path, Parameters);
		}

		public URL setPassword(string password)
		{
			return new URL(protocol, username, password, host, port, path, Parameters);
		}

		public URL setAddress(string address)
		{
			int i = address.LastIndexOf(':');
			string host;
			int port = this.port;
			if (i >= 0)
			{
				host = address.Substring(0, i);
				port = Convert.ToInt32(address.Substring(i + 1));
			}
			else
			{
				host = address;
			}
			return new URL(protocol, username, password, host, port, path, Parameters);
		}

		public URL setHost(string host)
		{
			return new URL(protocol, username, password, host, port, path, Parameters);
		}

		public URL setPort(int port)
		{
			return new URL(protocol, username, password, host, port, path, Parameters);
		}

		public URL setPath(string path)
		{
			return new URL(protocol, username, password, host, port, path, Parameters);
		}

		public IDictionary<string, string> Parameters
		{
			get
			{
				return parameters;
			}
		}

		public string getParameterAndDecoded(string key)
		{
			return getParameterAndDecoded(key, null);
		}

		public string getParameterAndDecoded(string key, string defaultValue)
		{
			return decode(getParameter(key, defaultValue));
		}

		public string getParameter(string key)
		{
			string value = parameters[key];
			if (value == null || value.Length == 0)
			{
				value = parameters[Constants.DEFAULT_KEY_PREFIX + key];
			}
			return value;
		}

		public string getParameter(string key, string defaultValue)
		{
			string value = getParameter(key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			return value;
		}

		public string[] getParameter(string key, string[] defaultValue)
		{
			string value = getParameter(key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			return Constants.COMMA_SPLIT_PATTERN.Split(value);
		}

		private IDictionary<string, object> Numbers
		{
			get
			{
				if (numbers == null) 
				{
					numbers = new ConcurrentDictionary<string, object>();
				}
				return numbers;
			}
		}

		private IDictionary<string, URL> Urls
		{
			get
			{
				if (urls == null)
				{
					urls = new ConcurrentDictionary<string, URL>();
				}
				return urls;
			}
		}

		public URL getUrlParameter(string key)
		{
			URL u = Urls[key];
			if (u != null)
			{
				return u;
			}
			string value = getParameterAndDecoded(key);
			if (value == null || value.Length == 0)
			{
				return null;
			}
			u = URL.valueOf(value);
			Urls[key] = u;
			return u;
		}

		public double getParameter(string key, double defaultValue)
		{
			object n = Numbers[key];
			if (n != null)
			{
				return (double)n;
			}
			string value = getParameter(key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			double d = Convert.ToDouble(value);
			Numbers[key] = d;
			return d;
		}

		public float getParameter(string key, float defaultValue)
		{
			object n = Numbers[key];
			if (n != null)
			{
				return (float)n;
			}
			string value = getParameter(key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			float f = Convert.ToSingle(value);
			Numbers[key] = f;
			return f;
		}

		public long getParameter(string key, long defaultValue)
		{
			object n = Numbers[key];
			if (n != null)
			{
				return (long)n;
			}
			string value = getParameter(key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			long l = Convert.ToInt64(value);
			Numbers[key] = l;
			return l;
		}

		public int getParameter(string key, int defaultValue)
		{
			object n = Numbers[key];
			if (n != null)
			{
				return (int)n;
			}
			string value = getParameter(key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			int i = Convert.ToInt32(value);
			Numbers[key] = i;
			return i;
		}

		public short getParameter(string key, short defaultValue)
		{
			object n = Numbers[key];
			if (n != null)
			{
				return (short)n;
			}
			string value = getParameter(key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			short s = Convert.ToInt16(value);
			Numbers[key] = s;
			return s;
		}

		public sbyte getParameter(string key, sbyte defaultValue)
		{
			object n = Numbers[key];
			if (n != null)
			{
				return (sbyte)n;
			}
			string value = getParameter(key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			sbyte b = Convert.ToSByte(value);
			Numbers[key] = b;
			return b;
		}

		public float getPositiveParameter(string key, float defaultValue)
		{
			if (defaultValue <= 0)
			{
				throw new System.ArgumentException("defaultValue <= 0");
			}
			float value = getParameter(key, defaultValue);
			if (value <= 0)
			{
				return defaultValue;
			}
			return value;
		}

		public double getPositiveParameter(string key, double defaultValue)
		{
			if (defaultValue <= 0)
			{
				throw new System.ArgumentException("defaultValue <= 0");
			}
			double value = getParameter(key, defaultValue);
			if (value <= 0)
			{
				return defaultValue;
			}
			return value;
		}

		public long getPositiveParameter(string key, long defaultValue)
		{
			if (defaultValue <= 0)
			{
				throw new System.ArgumentException("defaultValue <= 0");
			}
			long value = getParameter(key, defaultValue);
			if (value <= 0)
			{
				return defaultValue;
			}
			return value;
		}

		public int getPositiveParameter(string key, int defaultValue)
		{
			if (defaultValue <= 0)
			{
				throw new System.ArgumentException("defaultValue <= 0");
			}
			int value = getParameter(key, defaultValue);
			if (value <= 0)
			{
				return defaultValue;
			}
			return value;
		}

		public short getPositiveParameter(string key, short defaultValue)
		{
			if (defaultValue <= 0)
			{
				throw new System.ArgumentException("defaultValue <= 0");
			}
			short value = getParameter(key, defaultValue);
			if (value <= 0)
			{
				return defaultValue;
			}
			return value;
		}

		public sbyte getPositiveParameter(string key, sbyte defaultValue)
		{
			if (defaultValue <= 0)
			{
				throw new System.ArgumentException("defaultValue <= 0");
			}
			sbyte value = getParameter(key, defaultValue);
			if (value <= 0)
			{
				return defaultValue;
			}
			return value;
		}

		public char getParameter(string key, char defaultValue)
		{
			string value = getParameter(key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			return value[0];
		}

		public bool getParameter(string key, bool defaultValue)
		{
			string value = getParameter(key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			return Convert.ToBoolean(value);
		}

		public bool hasParameter(string key)
		{
			string value = getParameter(key);
			return value != null && value.Length > 0;
		}

		public string getMethodParameterAndDecoded(string method, string key)
		{
			return URL.decode(getMethodParameter(method, key));
		}

		public string getMethodParameterAndDecoded(string method, string key, string defaultValue)
		{
			return URL.decode(getMethodParameter(method, key, defaultValue));
		}

		public string getMethodParameter(string method, string key)
		{
			string value = parameters[method + "." + key];
			if (value == null || value.Length == 0)
			{
				return getParameter(key);
			}
			return value;
		}

		public string getMethodParameter(string method, string key, string defaultValue)
		{
			string value = getMethodParameter(method, key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			return value;
		}

		public double getMethodParameter(string method, string key, double defaultValue)
		{
			string methodKey = method + "." + key;
			object n = Numbers[methodKey];
			if (n != null)
			{
				return (int)n;
			}
			string value = getMethodParameter(method, key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			double d = Convert.ToDouble(value);
			Numbers[methodKey] = d;
			return d;
		}

		public float getMethodParameter(string method, string key, float defaultValue)
		{
			string methodKey = method + "." + key;
			object n = Numbers[methodKey];
			if (n != null)
			{
				return (int)n;
			}
			string value = getMethodParameter(method, key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			float f = Convert.ToSingle(value);
			Numbers[methodKey] = f;
			return f;
		}

		public long getMethodParameter(string method, string key, long defaultValue)
		{
			string methodKey = method + "." + key;
			object n = Numbers[methodKey];
			if (n != null)
			{
				return (int)n;
			}
			string value = getMethodParameter(method, key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			long l = Convert.ToInt64(value);
			Numbers[methodKey] = l;
			return l;
		}

		public int getMethodParameter(string method, string key, int defaultValue)
		{
			string methodKey = method + "." + key;
			object n = Numbers[methodKey];
			if (n != null)
			{
				return (int)n;
			}
			string value = getMethodParameter(method, key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			int i = Convert.ToInt32(value);
			Numbers[methodKey] = i;
			return i;
		}

		public short getMethodParameter(string method, string key, short defaultValue)
		{
			string methodKey = method + "." + key;
			object n = Numbers[methodKey];
			if (n != null)
			{
				return (short)n;
			}
			string value = getMethodParameter(method, key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			short s = Convert.ToInt16(value);
			Numbers[methodKey] = s;
			return s;
		}

		public sbyte getMethodParameter(string method, string key, sbyte defaultValue)
		{
			string methodKey = method + "." + key;
			object n = Numbers[methodKey];
			if (n != null)
			{
				return (sbyte)n;
			}
			string value = getMethodParameter(method, key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			sbyte b = Convert.ToSByte(value);
			Numbers[methodKey] = b;
			return b;
		}

		public double getMethodPositiveParameter(string method, string key, double defaultValue)
		{
			if (defaultValue <= 0)
			{
				throw new System.ArgumentException("defaultValue <= 0");
			}
			double value = getMethodParameter(method, key, defaultValue);
			if (value <= 0)
			{
				return defaultValue;
			}
			return value;
		}

		public float getMethodPositiveParameter(string method, string key, float defaultValue)
		{
			if (defaultValue <= 0)
			{
				throw new System.ArgumentException("defaultValue <= 0");
			}
			float value = getMethodParameter(method, key, defaultValue);
			if (value <= 0)
			{
				return defaultValue;
			}
			return value;
		}

		public long getMethodPositiveParameter(string method, string key, long defaultValue)
		{
			if (defaultValue <= 0)
			{
				throw new System.ArgumentException("defaultValue <= 0");
			}
			long value = getMethodParameter(method, key, defaultValue);
			if (value <= 0)
			{
				return defaultValue;
			}
			return value;
		}

		public int getMethodPositiveParameter(string method, string key, int defaultValue)
		{
			if (defaultValue <= 0)
			{
				throw new System.ArgumentException("defaultValue <= 0");
			}
			int value = getMethodParameter(method, key, defaultValue);
			if (value <= 0)
			{
				return defaultValue;
			}
			return value;
		}

		public short getMethodPositiveParameter(string method, string key, short defaultValue)
		{
			if (defaultValue <= 0)
			{
				throw new System.ArgumentException("defaultValue <= 0");
			}
			short value = getMethodParameter(method, key, defaultValue);
			if (value <= 0)
			{
				return defaultValue;
			}
			return value;
		}

		public sbyte getMethodPositiveParameter(string method, string key, sbyte defaultValue)
		{
			if (defaultValue <= 0)
			{
				throw new System.ArgumentException("defaultValue <= 0");
			}
			sbyte value = getMethodParameter(method, key, defaultValue);
			if (value <= 0)
			{
				return defaultValue;
			}
			return value;
		}

		public char getMethodParameter(string method, string key, char defaultValue)
		{
			string value = getMethodParameter(method, key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			return value[0];
		}

		public bool getMethodParameter(string method, string key, bool defaultValue)
		{
			string value = getMethodParameter(method, key);
			if (value == null || value.Length == 0)
			{
				return defaultValue;
			}
			return Convert.ToBoolean(value);
		}

		public bool hasMethodParameter(string method, string key)
		{
			if (method == null)
			{
				string suffix = "." + key;
				foreach (string fullKey in parameters.Keys)
				{
					if (fullKey.EndsWith(suffix))
					{
						return true;
					}
				}
				return false;
			}
			if (key == null)
			{
				string prefix = method + ".";
				foreach (string fullKey in parameters.Keys)
				{
					if (fullKey.StartsWith(prefix))
					{
						return true;
					}
				}
				return false;
			}
			string value = getMethodParameter(method, key);
			return value != null && value.Length > 0;
		}

//		public bool LocalHost
//		{
//			get
//			{
//				return NetUtils.isLocalHost(host) || getParameter(Constants.LOCALHOST_KEY, false);
//			}
//		}

		public bool AnyHost
		{
			get
			{
				return Constants.ANYHOST_VALUE.Equals(host) || getParameter(Constants.ANYHOST_KEY, false);
			}
		}

		public URL addParameterAndEncoded(string key, string value)
		{
			if (value == null || value.Length == 0)
			{
				return this;
			}
			return addParameter(key, encode(value));
		}

		public URL addParameter(string key, bool value)
		{
			return addParameter(key, Convert.ToString(value));
		}

		public URL addParameter(string key, char value)
		{
			return addParameter(key, Convert.ToString(value));
		}

		public URL addParameter(string key, sbyte value)
		{
			return addParameter(key, Convert.ToString(value));
		}

		public URL addParameter(string key, short value)
		{
			return addParameter(key, Convert.ToString(value));
		}

//		public URL addParameter(string key, int value)
//		{
//			return addParameter(key, Convert.ToString(value));
//		}

		public URL addParameter(string key, long value)
		{
			return addParameter(key, Convert.ToString(value));
		}

		public URL addParameter(string key, float value)
		{
			return addParameter(key, Convert.ToString(value));
		}

		public URL addParameter(string key, double value)
		{
			return addParameter(key, Convert.ToString(value));
		}

//		public URL addParameter<T1>(string key, Enum<T1> value)
//		{
//			if (value == null)
//			{
//				return this;
//			}
//			return addParameter(key, Convert.ToString(value));
//		}

//		public URL addParameter(string key, int value)
//		{
//			if (value == null)
//			{
//				return this;
//			}
//			return addParameter(key, Convert.ToString(value));
//		}

//		public URL addParameter(string key, string value)
//		{
//			if (value == null || value. == 0)
//			{
//				return this;
//			}
//			return addParameter(key, Convert.ToString(value));
//		}

		public URL addParameter(string key, string value)
		{
			if (key == null || key.Length == 0 || value == null || value.Length == 0)
			{
				return this;
			}
			if (value.Equals(Parameters[key])) // value != null
			{
				return this;
			}

			IDictionary<string, string> map = new Dictionary<string, string>(Parameters);
			map[key] = value;
			return new URL(protocol, username, password, host, port, path, map);
		}

		public URL addParameterIfAbsent(string key, string value)
		{
			if (key == null || key.Length == 0 || value == null || value.Length == 0)
			{
				return this;
			}
			if (hasParameter(key))
			{
				return this;
			}
			IDictionary<string, string> map = new Dictionary<string, string>(Parameters);
			map[key] = value;
			return new URL(protocol, username, password, host, port, path, map);
		}

		/// <summary>
		/// Add parameters to a new url.
		/// </summary>
		/// <param name="parameters"> </param>
		/// <returns> A new URL  </returns>
		public URL addParameters(IDictionary<string, string> parameters)
		{
			if (parameters == null || parameters.Count == 0)
			{
				return this;
			}

			bool hasAndEqual = true;
			foreach (KeyValuePair<string, string> entry in parameters)
			{
				string value = Parameters[entry.Key];
				if (value == null && entry.Value != null || !value.Equals(entry.Value))
				{
					hasAndEqual = false;
					break;
				}
			}
			// 如果没有修改，直接返回�?
			if (hasAndEqual)
			{
				return this;
			}

			IDictionary<string, string> map = new Dictionary<string, string>(Parameters);
//JAVA TO C# CONVERTER TODO TASK: There is no .NET Dictionary equivalent to the Java 'putAll' method:
            foreach (var item in parameters)
            {
                map.Add(item.Key, item.Value);
            }
            return new URL(protocol, username, password, host, port, path, map);
		}

		public URL addParametersIfAbsent(IDictionary<string, string> parameters)
		{
			if (parameters == null || parameters.Count == 0)
			{
				return this;
			}
			IDictionary<string, string> map = new Dictionary<string, string>(parameters);
            //JAVA TO C# CONVERTER TODO TASK: There is no .NET Dictionary equivalent to the Java 'putAll' method:
		    foreach (var item in parameters)
		    {
                map.Add(item.Key, item.Value);
            }
			return new URL(protocol, username, password, host, port, path, map);
		}

		public URL addParameters(params string[] pairs)
		{
			if (pairs == null || pairs.Length == 0)
			{
				return this;
			}
			if (pairs.Length % 2 != 0)
			{
				throw new System.ArgumentException("Map pairs can not be odd int.");
			}
			IDictionary<string, string> map = new Dictionary<string, string>();
			int len = pairs.Length / 2;
			for (int i = 0; i < len; i++)
			{
				map[pairs[2 * i]] = pairs[2 * i + 1];
			}
			return addParameters(map);
		}

		public URL addParameterString(string query)
		{
			if (query == null || query.Length == 0)
			{
				return this;
			}
			return addParameters(StringUtils.parseQueryString(query));
		}

		public URL removeParameter(string key)
		{
			if (key == null || key.Length == 0)
			{
				return this;
			}
			return removeParameters(key);
		}

		public URL removeParameters(IList<string> keys)
		{
			if (keys == null || keys.Count == 0)
			{
				return this;
			}
            
			return removeParameters(keys.ToArray());
		}

		public URL removeParameters(params string[] keys)
		{
			if (keys == null || keys.Length == 0)
			{
				return this;
			}
			IDictionary<string, string> map = new Dictionary<string, string>(Parameters);
			foreach (string key in keys)
			{
				map.Remove(key);
			}
			if (map.Count == Parameters.Count)
			{
				return this;
			}
			return new URL(protocol, username, password, host, port, path, map);
		}

		public URL clearParameters()
		{
			return new URL(protocol, username, password, host, port, path, new Dictionary<string, string>());
		}

		public string getRawParameter(string key)
		{
			if ("protocol".Equals(key))
			{
				return protocol;
			}
			if ("username".Equals(key))
			{
				return username;
			}
			if ("password".Equals(key))
			{
				return password;
			}
			if ("host".Equals(key))
			{
				return host;
			}
			if ("port".Equals(key))
			{
				return Convert.ToString(port);
			}
			if ("path".Equals(key))
			{
				return path;
			}
			return getParameter(key);
		}

		public IDictionary<string, string> toMap()
		{
			IDictionary<string, string> map = new Dictionary<string, string>(parameters);
			if (protocol != null)
			{
				map["protocol"] = protocol;
			}
			if (username != null)
			{
				map["username"] = username;
			}
			if (password != null)
			{
				map["password"] = password;
			}
			if (host != null)
			{
				map["host"] = host;
			}
			if (port > 0)
			{
				map["port"] = Convert.ToString(port);
			}
			if (path != null)
			{
				map["path"] = path;
			}
			return map;
		}

		public override string ToString()
		{
			if (@string != null)
			{
				return @string;
			}
			return @string = buildString(false, true); // no show username and password
		}

		public string ToString(params string[] parameters)
		{
			return buildString(false, true, parameters); // no show username and password
		}

		public string toIdentityString()
		{
			if (identity != null)
			{
				return identity;
			}
			return identity = buildString(true, false); // only return identity message, see the method "equals" and "hashCode"
		}

		public string toIdentityString(params string[] parameters)
		{
			return buildString(true, false, parameters); // only return identity message, see the method "equals" and "hashCode"
		}

		public string toFullString()
		{
			if (full != null)
			{
				return full;
			}
			return full = buildString(true, true);
		}

		public string toFullString(params string[] parameters)
		{
			return buildString(true, true, parameters);
		}

		public string toParameterString()
		{
			if (parameter != null)
			{
				return parameter;
			}
			return parameter = toParameterString(new string[0]);
		}

		public string toParameterString(params string[] parameters)
		{
			StringBuilder buf = new StringBuilder();
			buildParameters(buf, false, parameters);
			return buf.ToString();
		}

		private void buildParameters(StringBuilder buf, bool concat, string[] parameters)
		{
			if (Parameters != null && Parameters.Count > 0)
			{
				IList<string> includes = (parameters == null || parameters.Length == 0 ? null : new List<string>(parameters));
				bool first = true;
				foreach (KeyValuePair<string, string> entry in (new SortedDictionary<string, string>(Parameters)))
				{
					if (entry.Key != null && entry.Key.Length > 0 && (includes == null || includes.Contains(entry.Key)))
					{
						if (first)
						{
							if (concat)
							{
								buf.Append("?");
							}
							first = false;
						}
						else
						{
							buf.Append("&");
						}
						buf.Append(entry.Key);
						buf.Append("=");
						buf.Append(entry.Value == null ? "" : entry.Value.Trim());
					}
				}
			}
		}

		private string buildString(bool appendUser, bool appendParameter, params string[] parameters)
		{
			return buildString(appendUser, appendParameter, false, false, parameters);
		}

		private string buildString(bool appendUser, bool appendParameter, bool useIP, bool useService, params string[] parameters)
		{
			StringBuilder buf = new StringBuilder();
			if (protocol != null && protocol.Length > 0)
			{
				buf.Append(protocol);
				buf.Append("://");
			}
			if (appendUser && username != null && username.Length > 0)
			{
				buf.Append(username);
				if (password != null && password.Length > 0)
				{
					buf.Append(":");
					buf.Append(password);
				}
				buf.Append("@");
			}
			string host;
			if (useIP)
			{
				//host = Ip;
                host = "localhost";
            }
			else
			{
				host = Host;
			}
			if (host != null && host.Length > 0)
			{
				buf.Append(host);
				if (port > 0)
				{
					buf.Append(":");
					buf.Append(port);
				}
			}
			string path;
			if (useService)
			{
				path = ServiceKey;
			}
			else
			{
				path = Path;
			}
			if (path != null && path.Length > 0)
			{
				buf.Append("/");
				buf.Append(path);
			}
			if (appendParameter)
			{
				buildParameters(buf, true, parameters);
			}
			return buf.ToString();
		}

//		public java.net.URL toJavaURL()
//		{
//			try
//			{
//				return new java.net.URL(ToString());
//			}
//			catch (MalformedURLException e)
//			{
//				throw new IllegalStateException(e.Message, e);
//			}
//		}

//		public InetSocketAddress toInetSocketAddress()
//		{
//			return new InetSocketAddress(host, port);
//		}

		public string ServiceKey
		{
			get
			{
				string inf = ServiceInterface;
				if (inf == null)
				{
					return null;
				}
				StringBuilder buf = new StringBuilder();
				string group = getParameter(Constants.GROUP_KEY);
				if (group != null && group.Length > 0)
				{
					buf.Append(group).Append("/");
				}
				buf.Append(inf);
				string version = getParameter(Constants.VERSION_KEY);
				if (version != null && version.Length > 0)
				{
					buf.Append(":").Append(version);
				}
				return buf.ToString();
			}
		}

		public string toServiceString()
		{
			return buildString(true, false, true, true);
		}

		[Obsolete]
		public string ServiceName
		{
			get
			{
				return ServiceInterface;
			}
		}

		public string ServiceInterface
		{
			get
			{
				return getParameter(Constants.INTERFACE_KEY, path);
			}
		}

		public URL setServiceInterface(string service)
		{
			return addParameter(Constants.INTERFACE_KEY, service);
		}

		/// @deprecated Replace to <code>getParameter(String, int)</code> 
		/// <seealso cref= #getParameter(String, int) </seealso>
		[Obsolete("Replace to <code>getParameter(String, int)</code>")]
		public int getIntParameter(string key)
		{
			return getParameter(key, 0);
		}

		/// @deprecated Replace to <code>getParameter(String, int)</code> 
		/// <seealso cref= #getParameter(String, int) </seealso>
		[Obsolete("Replace to <code>getParameter(String, int)</code>")]
		public int getIntParameter(string key, int defaultValue)
		{
			return getParameter(key, defaultValue);
		}

		/// @deprecated Replace to <code>getPositiveParameter(String, int)</code> 
		/// <seealso cref= #getPositiveParameter(String, int) </seealso>
		[Obsolete("Replace to <code>getPositiveParameter(String, int)</code>")]
		public int getPositiveIntParameter(string key, int defaultValue)
		{
			return getPositiveParameter(key, defaultValue);
		}

		/// @deprecated Replace to <code>getParameter(String, boolean)</code> 
		/// <seealso cref= #getParameter(String, boolean) </seealso>
		[Obsolete("Replace to <code>getParameter(String, boolean)</code>")]
		public bool getBooleanParameter(string key)
		{
			return getParameter(key, false);
		}

		/// @deprecated Replace to <code>getParameter(String, boolean)</code> 
		/// <seealso cref= #getParameter(String, boolean) </seealso>
		[Obsolete("Replace to <code>getParameter(String, boolean)</code>")]
		public bool getBooleanParameter(string key, bool defaultValue)
		{
			return getParameter(key, defaultValue);
		}

		/// @deprecated Replace to <code>getMethodParameter(String, String, int)</code> 
		/// <seealso cref= #getMethodParameter(String, String, int) </seealso>
		[Obsolete("Replace to <code>getMethodParameter(String, String, int)</code>")]
		public int getMethodIntParameter(string method, string key)
		{
			return getMethodParameter(method, key, 0);
		}

		/// @deprecated Replace to <code>getMethodParameter(String, String, int)</code> 
		/// <seealso cref= #getMethodParameter(String, String, int) </seealso>
		[Obsolete("Replace to <code>getMethodParameter(String, String, int)</code>")]
		public int getMethodIntParameter(string method, string key, int defaultValue)
		{
			return getMethodParameter(method, key, defaultValue);
		}

		/// @deprecated Replace to <code>getMethodPositiveParameter(String, String, int)</code> 
		/// <seealso cref= #getMethodPositiveParameter(String, String, int) </seealso>
		[Obsolete("Replace to <code>getMethodPositiveParameter(String, String, int)</code>")]
		public int getMethodPositiveIntParameter(string method, string key, int defaultValue)
		{
			return getMethodPositiveParameter(method, key, defaultValue);
		}

		/// @deprecated Replace to <code>getMethodParameter(String, String, boolean)</code> 
		/// <seealso cref= #getMethodParameter(String, String, boolean) </seealso>
		[Obsolete("Replace to <code>getMethodParameter(String, String, boolean)</code>")]
		public bool getMethodBooleanParameter(string method, string key)
		{
			return getMethodParameter(method, key, false);
		}

		/// @deprecated Replace to <code>getMethodParameter(String, String, boolean)</code> 
		/// <seealso cref= #getMethodParameter(String, String, boolean) </seealso>
		[Obsolete("Replace to <code>getMethodParameter(String, String, boolean)</code>")]
		public bool getMethodBooleanParameter(string method, string key, bool defaultValue)
		{
			return getMethodParameter(method, key, defaultValue);
		}

		public static string encode(string value)
		{
			if (value == null || value.Length == 0)
			{
				return "";
			}
			try
			{
				return HttpUtility.UrlEncode(value, Encoding.UTF8);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message, e);
			}
		}

		public static string decode(string value)
		{
			if (value == null || value.Length == 0)
			{
				return "";
			}
			try
			{
				return HttpUtility.UrlDecode(value, Encoding.UTF8);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message, e);
			}
		}

		public override int GetHashCode()
		{
			const int prime = 31;
			int result = 1;
			result = prime * result + ((host == null) ? 0 : host.GetHashCode());
			result = prime * result + ((parameters == null) ? 0 : parameters.GetHashCode());
			result = prime * result + ((password == null) ? 0 : password.GetHashCode());
			result = prime * result + ((path == null) ? 0 : path.GetHashCode());
			result = prime * result + port;
			result = prime * result + ((protocol == null) ? 0 : protocol.GetHashCode());
			result = prime * result + ((username == null) ? 0 : username.GetHashCode());
			return result;
		}

		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (this.GetType() != obj.GetType())
			{
				return false;
			}
			URL other = (URL) obj;
			if (host == null)
			{
				if (other.host != null)
				{
					return false;
				}
			}
			else if (!host.Equals(other.host))
			{
				return false;
			}
			if (parameters == null)
			{
				if (other.parameters != null)
				{
					return false;
				}
			}
			else if (!parameters.Equals(other.parameters))
			{
				return false;
			}
			if (password == null)
			{
				if (other.password != null)
				{
					return false;
				}
			}
			else if (!password.Equals(other.password))
			{
				return false;
			}
			if (path == null)
			{
				if (other.path != null)
				{
					return false;
				}
			}
			else if (!path.Equals(other.path))
			{
				return false;
			}
			if (port != other.port)
			{
				return false;
			}
			if (protocol == null)
			{
				if (other.protocol != null)
				{
					return false;
				}
			}
			else if (!protocol.Equals(other.protocol))
			{
				return false;
			}
			if (username == null)
			{
				if (other.username != null)
				{
					return false;
				}
			}
			else if (!username.Equals(other.username))
			{
				return false;
			}
			return true;
		}


	}
}