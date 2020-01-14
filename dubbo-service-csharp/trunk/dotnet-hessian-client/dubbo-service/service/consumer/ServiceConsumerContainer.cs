using System;
using System.Collections.Generic;
using System.Reflection;
using com.alibaba.dubbo.common;
using com.alibaba.dubbo.common.exception;
using com.alibaba.dubbo.config;
using com.alibaba.dubbo.remoting.zookeeper;
using com.alibaba.dubbo.remoting.zookeeper.zkclient;
using com.wyying.service.rpc.hessian;

namespace com.alibaba.dubbo.service
{  
    /// <summary>
    ///  hessian服务容器
    /// </summary>
    public class ServiceConsumerContainer
    {

        public static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ServiceConsumerContainer));


        private static ServiceConsumerContainer instance;
        private ZookeeperClient zookeeperClient;
        private readonly Dictionary<string, HessianDubboService> hessianServices = new Dictionary<string, HessianDubboService>();
        private readonly HashSet<URL> hessianServiceUrls = new HashSet<URL>();
        private readonly HashSet<URL> serviceurls = new HashSet<URL>();
        // 定义一个标识确保线程同步
        private static readonly object locker = new object();
        private ServiceConsumerContainer(ZookeeperClient zookeeperClient)
        {
            this.zookeeperClient = zookeeperClient;
            Init();
        }

        public static ServiceConsumerContainer Instance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    DubboConfig.Instance();
                    var zkclientZookeeperTransporter = new ZkclientZookeeperTransporter();
                    var zooUrl = new ZooURL(DubboConfig.dubbo_registry_address);
                    var zookeeperClient = zkclientZookeeperTransporter.connect(zooUrl);
                    instance = new ServiceConsumerContainer(zookeeperClient);                               
                }
            }
            return instance;
        }

        /// <summary>
        /// 通过接口的全路径名称来获取代理服务
        /// </summary>
        /// <param name="serviceFullName"></param>
        /// <returns></returns>
        public object GetHessianServices(string serviceFullName)
        {
            HessianDubboService dubboService ;
            bool rel = hessianServices.TryGetValue(serviceFullName, out dubboService);
            if (rel && dubboService != null)
            {
                return dubboService.GetService();
            }
            
            logger.Error("dubboService is not found, serviceFullName=" + serviceFullName);
            throw new ServiceException("dubboService is not found, serviceFullName=" + serviceFullName);
        }

        /// <summary>
        /// 初始化服务提供
        /// </summary>
        private void Init()
        {
            try
            {               
                if (zookeeperClient.ExistsDubboNode())
                {
                    var servicenames =
                        (List<string>)
                        zookeeperClient.getChildren(Constants.PATH_SEPARATOR + Constants.DEFAULT_DIRECTORY, true);
                    foreach (var sname in servicenames)
                    {
                        GetProviders(sname);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("servicecontrainer is init error",e);
                throw;
            }
            logger.Debug("servicecontrainer is inited, service list = "+hessianServices);
        }

        public void UpdateProviders(string sname)
        {
            if (!string.IsNullOrEmpty(sname))
            {
                GetProviders(sname);
            }
        }

        private void GetProviders(string sname)
        {
            //清空，zkwatch的变化
            hessianServices.Remove(sname);

            var providers =
                (List<string>)
                zookeeperClient.getChildren(
                    Constants.PATH_SEPARATOR + Constants.DEFAULT_DIRECTORY + Constants.PATH_SEPARATOR + sname +
                    Constants.PATH_SEPARATOR + Constants.PROVIDERS_CATEGORY, true);
            if (providers == null || providers.Count == 0) return;

            foreach (var provider in providers)
            {
                var url = URL.valueOf(URL.decode(provider));
                if (url.Protocol.Equals("hessian"))
                {
                    hessianServiceUrls.Add(url);

                    var ass = Assembly.Load(DubboConfig.dubbo_service_interface_assembly);

                    var type = ass.GetType(url.ServiceInterface);
                    if (type == null)
                    {
                        logger.Warn("found hessian dubboService, but not found in this assembly; servicename=" +
                                    url.ServiceInterface + ";assembly=" + DubboConfig.dubbo_service_interface_assembly);
                        continue;
                    }

                    var serHessianInvoke = new HessianInvoke(url);

                    HessianDubboService hessianDubboService;
                    if (hessianServices.TryGetValue(url.ServiceInterface, out hessianDubboService))
                        hessianDubboService.AddService(url.Host, serHessianInvoke.GetService(type));

                    else
                        hessianServices.Add(url.ServiceInterface,
                            new HessianDubboService(url.ServiceInterface, url.Host, serHessianInvoke.GetService(type)));
                }
                serviceurls.Add(url);
            }
        }
    }
}