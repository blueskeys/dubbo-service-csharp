using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using com.alibaba.dubbo.common;
using com.alibaba.dubbo.common.attributes;
using com.alibaba.dubbo.common.utils;
using com.alibaba.dubbo.config;
using com.alibaba.dubbo.remoting.zookeeper;
using com.alibaba.dubbo.remoting.zookeeper.zkclient;
using dubbo_service.common.utils;
using dubbo_service.rpc.hessian;

namespace com.wyying.service.rpc.hessian
{
    /// <summary>
    /// hessian服务工厂，在应用启动的时候进行调用，
    /// 通过指定的集合进行扫描注册service
    /// </summary>
    public class HessianFactory
    {
        /// <summary>
        /// key->url.path; value->servicehandler
        /// </summary>
        private readonly Dictionary<string, IHttpHandler> hessianServices = new Dictionary<string, IHttpHandler>();
        private readonly IList<string> servicezkList = new List<string>();

        private bool initFlag = false; 

        private static HessianFactory instance;
        // 定义一个标识确保线程同步
        private static readonly object locker = new object();
        private HessianFactory()
        {

        }

        public IHttpHandler GetHttpHandler(string url)
        {
            foreach (var hessianServicesKey in hessianServices.Keys)
            {
                if (url.EndsWith(hessianServicesKey))
                {
                    return hessianServices[hessianServicesKey];
                }
            }
            return null;
        }

        public static HessianFactory Instance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    instance = new HessianFactory();
                }
            }
            return instance;
        }

        /// <summary>
        /// 保证系统仅仅初始化一次
        /// </summary>
        public void Init()
        {
            lock (locker)
            {
                if (!initFlag)
                {
                    DoInit();
                }
            }
        }

        private void DoInit()
        {
            var zkclientZookeeperTransporter = new ZkclientZookeeperTransporter();
            var zooUrl = new ZooURL(DubboConfig.dubbo_registry_address);
            var zookeeperClient = zkclientZookeeperTransporter.connect(zooUrl);

            var parameters = new Dictionary<string, string>();
            parameters.Add(Constants.APPLICATION_KEY,DubboConfig.dubbo_application_name);

            var serviceProviders = ScanAssemblyOfService();
            foreach (var provider in serviceProviders)
            {
                parameters.Add(Constants.INTERFACE_KEY, provider.ServiceName);

                MethodInfo[] methodInfos  = provider.ServiceType.GetMethods();
                List<string> methodnames = new List<string>();
                methodInfos.ToList().ForEach(m => methodnames.Add(m.Name));              
                parameters.Add(Constants.METHODS_KEY, string.Join(",", methodnames));

                //parameters.Add(Constants.TIMESTAMP_KEY, DateTime.Now.ToLongTimeString());

                var path = provider.Path;
                var url = new URL("hessian", IpUtils.GetSingleLocalIpv4(),
                    DubboConfig.dobbo_service_port, path, parameters);

                var servicenode = Constants.PATH_SEPARATOR +
                                  Constants.DEFAULT_DIRECTORY +
                                  Constants.PATH_SEPARATOR +
                                  url.ServiceInterface;

                var serviceurl = servicenode +
                                 Constants.PATH_SEPARATOR +
                                 Constants.PROVIDERS_CATEGORY +
                                 Constants.PATH_SEPARATOR +
                                 URL.encode(url.toFullString());
                //zookeeper服务注册
                zookeeperClient.create(serviceurl, true);
                servicezkList.Add(serviceurl);
                IHttpHandler handler = (IHttpHandler)Activator.CreateInstance(provider.ServiceType);
                hessianServices.Add(provider.Path, handler);
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            var zkclientZookeeperTransporter = new ZkclientZookeeperTransporter();
            var zooUrl = new ZooURL(DubboConfig.dubbo_registry_address);
            var zookeeperClient = zkclientZookeeperTransporter.connect(zooUrl);
            foreach (var s in servicezkList)
            {
                zookeeperClient.delete(s);
            }
        }

        private IList<ServiceProvider> ScanAssemblyOfService()
        {
            IList<ServiceProvider> serviceProviders = new List<ServiceProvider>();
            var types = Assembly.Load(DubboConfig.dubbo_service_assembly).GetTypes();
            foreach (var type in types)
            {
                ServiceProvider serviceProvider = new ServiceProvider();
                var seraServiceAttribute = AttributeUtils.GetServiceAttribute(type);
                if (seraServiceAttribute != null)
                {
                    serviceProvider.ServiceAttribute = seraServiceAttribute;
                    serviceProvider.ServiceType = type;
                    serviceProvider.Path = seraServiceAttribute.path ?? seraServiceAttribute.interfaceClass.FullName + ".hessian";
                    serviceProvider.ServiceName = seraServiceAttribute.interfaceClass.FullName;
                    serviceProviders.Add(serviceProvider);
                }
            }
            return serviceProviders;
        }
    }
}