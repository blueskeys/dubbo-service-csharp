using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

/*
 * Copyright 1999-2011 Alibaba Group.
 *  
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *  
 *      http://www.apache.org/licenses/LICENSE-2.0
 *  
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
namespace com.alibaba.dubbo.common.utils
{

//
//    using UnsafeStringWriter = com.alibaba.dubbo.common.io.UnsafeStringWriter;
//    using JSON = com.alibaba.dubbo.common.json.JSON;
//    using Logger = com.alibaba.dubbo.common.logger.Logger;
//    using LoggerFactory = com.alibaba.dubbo.common.logger.LoggerFactory;
    using System.Linq;

    /// <summary>
    /// StringUtils
    /// 
    /// @author qian.lei
    /// </summary>

    public sealed class StringUtils
	{

//		private static readonly Logger logger = LoggerFactory.getLogger(typeof(StringUtils));

		public static readonly string[] EMPTY_STRING_ARRAY = new string[0];

		private static readonly Regex KVP_PATTERN = new Regex("([_.a-zA-Z0-9][-_.a-zA-Z0-9]*)[=](.*)"); //key value pair pattern.

		private static readonly Regex INT_PATTERN = new Regex("^\\d+$");

		public static bool isBlank(string str)
		{
			if (str == null || str.Length == 0)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// is empty string.
		/// </summary>
		/// <param name="str"> source string. </param>
		/// <returns> is empty. </returns>
		public static bool isEmpty(string str)
		{
			if (str == null || str.Length == 0)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// is not empty string.
		/// </summary>
		/// <param name="str"> source string. </param>
		/// <returns> is not empty. </returns>
		public static bool isNotEmpty(string str)
		{
			return str != null && str.Length > 0;
		}

		/// 
		/// <param name="s1"> </param>
		/// <param name="s2"> </param>
		/// <returns> equals </returns>
		public static bool isEquals(string s1, string s2)
		{
			if (s1 == null && s2 == null)
			{
				return true;
			}
			if (s1 == null || s2 == null)
			{
				return false;
			}
			return s1.Equals(s2);
		}

		/// <summary>
		/// is integer string.
		/// </summary>
		/// <param name="str"> </param>
		/// <returns> is integer </returns>
		public static bool isInteger(string str)
		{
			if (str == null || str.Length == 0)
			{
				return false;
			}
			return INT_PATTERN.Matches(str).Count >0;
		}

		public static int parseInteger(string str)
		{
			if (!isInteger(str))
			{
				return 0;
			}
			return Convert.ToInt32(str);
		}

		/// <summary>
		/// Returns true if s is a legal Java identifier.<para>
		/// <a href="http://www.exampledepot.com/egs/java.lang/IsJavaId.html">more info.</a>
		/// </para>
		/// </summary>
//		public static bool isJavaIdentifier(string s)
//		{
//			if (s.Length == 0 || !char.isJavaIdentifierStart(s[0]))
//			{
//				return false;
//			}
//			for (int i = 1; i < s.Length; i++)
//			{
//				if (!char.isJavaIdentifierPart(s[i]))
//				{
//					return false;
//				}
//			}
//			return true;
//		}

		public static bool isContains(string values, string value)
		{
			if (values == null || values.Length == 0)
			{
				return false;
			}
			return isContains(Constants.COMMA_SPLIT_PATTERN.Split(values), value);
		}

		/// 
		/// <param name="values"> </param>
		/// <param name="value"> </param>
		/// <returns> contains </returns>
		public static bool isContains(string[] values, string value)
		{
			if (value != null && value.Length > 0 && values != null && values.Length > 0)
			{
				foreach (string v in values)
				{
					if (value.Equals(v))
					{
						return true;
					}
				}
			}
			return false;
		}

		public static bool isNumeric(string str)
		{
			if (str == null)
			{
				return false;
			}
			int sz = str.Length;
			for (int i = 0; i < sz; i++)
			{
				if (char.IsDigit(str[i]) == false)
				{
					return false;
				}
			}
			return true;
		}

		/// 
		/// <param name="e"> </param>
		/// <returns> string </returns>
//		public static string ToString(Exception e)
//		{
//			UnsafeStringWriter w = new UnsafeStringWriter();
//			PrintWriter p = new PrintWriter(w);
////JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
//			p.print(e.GetType().FullName);
//			if (e.Message != null)
//			{
//				p.print(": " + e.Message);
//			}
//			p.println();
//			try
//			{
//				e.printStackTrace(p);
//				return w.ToString();
//			}
//			finally
//			{
//				p.close();
//			}
//		}

		/// 
		/// <param name="msg"> </param>
		/// <param name="e"> </param>
		/// <returns> string </returns>
//		public static string ToString(string msg, Exception e)
//		{
//			UnsafeStringWriter w = new UnsafeStringWriter();
//			w.write(msg + "\n");
//			PrintWriter p = new PrintWriter(w);
//			try
//			{
//				e.printStackTrace(p);
//				return w.ToString();
//			}
//			finally
//			{
//				p.close();
//			}
//		}

		/// <summary>
		/// translat.
		/// </summary>
		/// <param name="src"> source string. </param>
		/// <param name="from"> src char table. </param>
		/// <param name="to"> target char table. </param>
		/// <returns> String. </returns>
		public static string translat(string src, string from, string to)
		{
			if (isEmpty(src))
			{
				return src;
			}
			StringBuilder sb = null;
			int ix;
			char c;
			for (int i = 0,len = src.Length;i < len;i++)
			{
				c = src[i];
				ix = from.IndexOf(c);
				if (ix == -1)
				{
					if (sb != null)
					{
						sb.Append(c);
					}
				}
				else
				{
					if (sb == null)
					{
						sb = new StringBuilder(len);
						sb.Append(src, 0, i);
					}
					if (ix < to.Length)
					{
						sb.Append(to[ix]);
					}
				}
			}
			return sb == null ? src : sb.ToString();
		}

		/// <summary>
		/// split.
		/// </summary>
		/// <param name="ch"> char. </param>
		/// <returns> string array. </returns>
		public static string[] Split(string str, char ch)
		{
			List<string> list = null;
			char c;
			int ix = 0, len = str.Length;
			for (int i = 0;i < len;i++)
			{
				c = str[i];
				if (c == ch)
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(str.Substring(ix, i - ix));
					ix = i + 1;
				}
			}
			if (ix > 0)
			{
				list.Add(str.Substring(ix));
			}
			return list == null ? EMPTY_STRING_ARRAY : list.ToArray();
		}

		/// <summary>
		/// join string.
		/// </summary>
		/// <param name="array"> String array. </param>
		/// <returns> String. </returns>
		public static string join(string[] array)
		{
			if (array.Length == 0)
			{
				return "";
			}
			StringBuilder sb = new StringBuilder();
			foreach (string s in array)
			{
				sb.Append(s);
			}
			return sb.ToString();
		}

		/// <summary>
		/// join string like javascript.
		/// </summary>
		/// <param name="array"> String array. </param>
		/// <param name="split"> split </param>
		/// <returns> String. </returns>
		public static string join(string[] array, char split)
		{
			if (array.Length == 0)
			{
				return "";
			}
			StringBuilder sb = new StringBuilder();
			for (int i = 0;i < array.Length;i++)
			{
				if (i > 0)
				{
					sb.Append(split);
				}
				sb.Append(array[i]);
			}
			return sb.ToString();
		}

		/// <summary>
		/// join string like javascript.
		/// </summary>
		/// <param name="array"> String array. </param>
		/// <param name="split"> split </param>
		/// <returns> String. </returns>
		public static string join(string[] array, string split)
		{
			if (array.Length == 0)
			{
				return "";
			}
			StringBuilder sb = new StringBuilder();
			for (int i = 0;i < array.Length;i++)
			{
				if (i > 0)
				{
					sb.Append(split);
				}
				sb.Append(array[i]);
			}
			return sb.ToString();
		}

		public static string join(ICollection<string> coll, string split)
		{
			if (coll.Count == 0)
			{
				return "";
			}

			StringBuilder sb = new StringBuilder();
			bool isFirst = true;
			foreach (string s in coll)
			{
				if (isFirst)
				{
					isFirst = false;
				}
				else
				{
					sb.Append(split);
				}
				sb.Append(s);
			}
			return sb.ToString();
		}

		/// <summary>
		/// parse key-value pair.
		/// </summary>
		/// <param name="str"> string. </param>
		/// <param name="itemSeparator"> item separator. </param>
		/// <returns> key-value map; </returns>
		private static IDictionary<string, string> parseKeyValuePair(string str, string itemSeparator)
		{
            string[] tmp = str.Split(new string[] { itemSeparator }, StringSplitOptions.None);
            IDictionary<string, string> map = new Dictionary<string, string>(tmp.Length);
			for (int i = 0;i < tmp.Length;i++)
			{
                MatchCollection matches = KVP_PATTERN.Matches(tmp[i]);
                //ÌáÈ¡Æ¥ÅäÏî
                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    map[groups[0].Value] = groups[1].Value;                   
                }
            }
			return map;
		}

		public static string getQueryStringValue(string qs, string key)
		{
			IDictionary<string, string> map = StringUtils.parseQueryString(qs);
			return map[key];
		}

		/// <summary>
		/// parse query string to Parameters.
		/// </summary>
		/// <param name="qs"> query string. </param>
		/// <returns> Parameters instance. </returns>
		public static IDictionary<string, string> parseQueryString(string qs)
		{
			if (qs == null || qs.Length == 0)
			{
				return new Dictionary<string, string>();
			}
			return parseKeyValuePair(qs, "\\&");
		}

		public static string getServiceKey(IDictionary<string, string> ps)
		{
			StringBuilder buf = new StringBuilder();
			string group = ps[Constants.GROUP_KEY];
			if (group != null && group.Length > 0)
			{
				buf.Append(group).Append("/");
			}
			buf.Append(ps[Constants.INTERFACE_KEY]);
			string version = ps[Constants.VERSION_KEY];
			if (version != null && version.Length > 0)
			{
				buf.Append(":").Append(version);
			}
			return buf.ToString();
		}

		public static string toQueryString(IDictionary<string, string> ps)
		{
			StringBuilder buf = new StringBuilder();
			if (ps != null && ps.Count > 0)
			{
				foreach (KeyValuePair<string, string> entry in (new SortedDictionary<string, string>(ps)))
				{
					string key = entry.Key;
					string value = entry.Value;
					if (key != null && key.Length > 0 && value != null && value.Length > 0)
					{
						if (buf.Length > 0)
						{
							buf.Append("&");
						}
						buf.Append(key);
						buf.Append("=");
						buf.Append(value);
					}
				}
			}
			return buf.ToString();
		}

		public static string camelToSplitName(string camelName, string split)
		{
			if (camelName == null || camelName.Length == 0)
			{
				return camelName;
			}
			StringBuilder buf = null;
			for (int i = 0; i < camelName.Length; i++)
			{
				char ch = camelName[i];
				if (ch >= 'A' && ch <= 'Z')
				{
					if (buf == null)
					{
						buf = new StringBuilder();
						if (i > 0)
						{
							buf.Append(camelName.Substring(0, i));
						}
					}
					if (i > 0)
					{
						buf.Append(split);
					}
					buf.Append(char.ToLower(ch));
				}
				else if (buf != null)
				{
					buf.Append(ch);
				}
			}
			return buf == null ? camelName : buf.ToString();
		}

//		public static string toArgumentString(object[] args)
//		{
//			StringBuilder buf = new StringBuilder();
//			foreach (object arg in args)
//			{
//				if (buf.Length > 0)
//				{
//					buf.Append(Constants.COMMA_SEPARATOR);
//				}
//				if (arg == null || ReflectUtils.isPrimitives(arg.GetType()))
//				{
//					buf.Append(arg);
//				}
//				else
//				{
//					try
//					{
//						buf.Append(JSON.json(arg));
//					}
//					catch (IOException e)
//					{
//					    e.
//					}
//				}
//			}
//			return buf.ToString();
//		}

		private StringUtils()
		{
		}
	}
}