using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using com.alibaba.dubbo.common.attributes;

namespace com.alibaba.dubbo.common.utils
{
    /// <summary>
    /// 静态辅助方法
    /// </summary>
    public static class AttributeUtils
    {


        /// <summary>
        /// 获取服务定义信息
        /// </summary>
        /// <param name="contractType">类型</param>
        /// <returns>服务名</returns>
        public static ServiceAttribute GetServiceAttribute(Type contractType)
        {
            var serviceName = string.Empty;
            var attrs = Attribute.GetCustomAttributes(contractType);
            foreach (var attr in attrs)
            {
                var attribute = attr as ServiceAttribute;
                if (attribute != null)
                {
                    return attribute;
                }
            }
            return null;
        }

//        /// <summary>
//        /// 获取方法名称
//        /// </summary>
//        /// <param name="method">Method</param>
//        /// <returns>方法名</returns>
//        public static string GetMethodName(MethodInfo method)
//        {
//            var methodName = method.Name;
//            var attr = Attribute.GetCustomAttribute(method, typeof(ThriftSOperationAttribute), false);
//            if (attr != null)
//            {
//                var attribute = attr as ThriftSOperationAttribute;
//                if (string.IsNullOrEmpty(attribute.Name) == false)
//                {
//                    methodName = attribute.Name;
//                }
//            }
//
//            return methodName;
//        }

        
    }
}
