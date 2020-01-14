using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.alibaba.dubbo.common.utils
{


	public class CollectionUtils
	{

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings({ "unchecked", "rawtypes" }) public static <T> java.util.List<T> sort(java.util.List<T> list)
		public static List<T> sort<T>(List<T> list)
		{
			if (list != null && list.Count > 0)
			{
                list.Sort();
			}
			return list;
		}

		private static readonly IComparer<string> SIMPLE_NAME_COMPARATOR = new ComparatorAnonymousInnerClassHelper();

		private class ComparatorAnonymousInnerClassHelper : IComparer<string>
		{
			public ComparatorAnonymousInnerClassHelper()
			{
			}

			public virtual int Compare(string s1, string s2)
			{
				if (s1 == null && s2 == null)
				{
					return 0;
				}
				if (s1 == null)
				{
					return -1;
				}
				if (s2 == null)
				{
					return 1;
				}
				int i1 = s1.LastIndexOf('.');
				if (i1 >= 0)
				{
					s1 = s1.Substring(i1 + 1);
				}
				int i2 = s2.LastIndexOf('.');
				if (i2 >= 0)
				{
					s2 = s2.Substring(i2 + 1);
				}               
				return string.Equals(s1,s2, StringComparison.OrdinalIgnoreCase)?0:-1;
			}
		}

		public static List<string> sortSimpleName(List<string> list)
		{
			if (list != null && list.Count > 0)
			{
				list.Sort();
			}
			return list;
		}

		public static IDictionary<string, IDictionary<string, string>> splitAll(IDictionary<string, IList<string>> list, string separator)
		{
			if (list == null)
			{
				return null;
			}
			IDictionary<string, IDictionary<string, string>> result = new Dictionary<string, IDictionary<string, string>>();
			foreach (KeyValuePair<string, IList<string>> entry in list)
			{
				result[entry.Key] = Split(entry.Value, separator);
			}
			return result;
		}

		public static IDictionary<string, IList<string>> joinAll(IDictionary<string, IDictionary<string, string>> map, string separator)
		{
			if (map == null)
			{
				return null;
			}
			IDictionary<string, IList<string>> result = new Dictionary<string, IList<string>>();
			foreach (KeyValuePair<string, IDictionary<string, string>> entry in map)
			{
				result[entry.Key] = join(entry.Value, separator);
			}
			return result;
		}

		public static IDictionary<string, string> Split(IList<string> list, string separator)
		{
			if (list == null)
			{
				return null;
			}
			IDictionary<string, string> map = new Dictionary<string, string>();
			if (list == null || list.Count == 0)
			{
				return map;
			}
			foreach (string item in list)
			{
				int index = item.IndexOf(separator);
				if (index == -1)
				{
					map[item] = "";
				}
				else
				{
					map[item.Substring(0, index)] = item.Substring(index + 1);
				}
			}
			return map;
		}

		public static IList<string> join(IDictionary<string, string> map, string separator)
		{
			if (map == null)
			{
				return null;
			}
			IList<string> list = new List<string>();
			if (map == null || map.Count == 0)
			{
				return list;
			}
			foreach (KeyValuePair<string, string> entry in map)
			{
				string key = entry.Key;
				string value = entry.Value;
				if (value == null || value.Length == 0)
				{
					list.Add(key);
				}
				else
				{
					list.Add(key + separator + value);
				}
			}
			return list;
		}

		public static string join(IList<string> list, string separator)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string ele in list)
			{
				if (sb.Length > 0)
				{
					sb.Append(separator);
				}
				sb.Append(ele);
			}
			return sb.ToString();
		}

		public static bool mapEquals(IDictionary<object, object> map1, IDictionary<object, object> map2)
		{
			if (map1 == null && map2 == null)
			{
				return true;
			}
			if (map1 == null || map2 == null)
			{
				return false;
			}
			if (map1.Count != map2.Count)
			{
				return false;
			}
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: for (java.util.Map.Entry<?, ?> entry : map1.entrySet())
			foreach (var entry in map1)
			{
				object key = entry.Key;
				object value1 = entry.Value;
				object value2 = map2[key];
				if (!objectEquals(value1, value2))
				{
					return false;
				}
			}
			return true;
		}

		private static bool objectEquals(object obj1, object obj2)
		{
			if (obj1 == null && obj2 == null)
			{
				return true;
			}
			if (obj1 == null || obj2 == null)
			{
				return false;
			}
			return obj1.Equals(obj2);
		}

		public static IDictionary<string, string> toStringMap(params string[] pairs)
		{
			IDictionary<string, string> parameters = new Dictionary<string, string>();
			if (pairs.Length > 0)
			{
				if (pairs.Length % 2 != 0)
				{
					throw new System.ArgumentException("pairs must be even.");
				}
				for (int i = 0; i < pairs.Length; i = i + 2)
				{
					parameters[pairs[i]] = pairs[i + 1];
				}
			}
			return parameters;
		}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") public static <K, V> java.util.Map<K, V> toMap(Object... pairs)
		public static IDictionary<K, V> toMap<K, V>(params object[] pairs)
		{
			IDictionary<K, V> ret = new Dictionary<K, V>();
			if (pairs == null || pairs.Length == 0)
			{
				return ret;
			}

			if (pairs.Length % 2 != 0)
			{
				throw new System.ArgumentException("Map pairs can not be odd number.");
			}
			int len = pairs.Length / 2;
			for (int i = 0; i < len; i++)
			{
				ret[(K) pairs[2 * i]] = (V) pairs[2 * i + 1];
			}
			return ret;
		}

		public static bool isEmpty<T1>(ICollection<T1> collection)
		{
			return collection == null || collection.Count == 0;
		}

		public static bool isNotEmpty<T1>(ICollection<T1> collection)
		{
			return collection != null && collection.Count > 0;
		}

		private CollectionUtils()
		{
		}

	}
}