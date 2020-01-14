using com.alibaba.dubbo.common;
using com.alibaba.dubbo.remoting.zookeeper;
using com.alibaba.dubbo.remoting.zookeeper.zkclient;
using com.wyying.service.rpc.hessian;
using dubbo_service.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.alibaba.dubbo.service
{
    public class ServiceConsumerContainer
    {
        HashSet<URL> serviceurls = new HashSet<URL>();
        HashSet<URL> hessianServiceUrls = new HashSet<URL>();
        Dictionary<String, IList<object>> hessianServices = new Dictionary<string, IList<object>>();

        private static ServiceConsumerContainer instance;

        public static ServiceConsumerContainer Instance()
        {
            if (instance == null)
            {
                instance = new ServiceConsumerContainer();
            }
            return instance;
        }

        public ServiceConsumerContainer()
        {
            Init();
        }

        public object GetHessianServices(String serviceFullName)
        {
            IList<object> services = new List<object>();
            this.hessianServices.TryGetValue(serviceFullName, out services);
            //TODO 此处添加负载均衡的代码
            return services[0];
        }

        private void Init()
        {

            ZkclientZookeeperTransporter zkclientZookeeperTransporter = new ZkclientZookeeperTransporter();
            //String zooConstring = "192.168.100.242:2182,192.168.100.242:2183,192.168.100.242:2184";

            DubboConfig dc = new DubboConfig();

            ZooURL zooUrl = new ZooURL(DubboConfig.dubbo_registry_address);
            ZookeeperClient zookeeperClient = zkclientZookeeperTransporter.connect(zooUrl);

            if (zookeeperClient.ExistsDubboNode())
            {
                List<string> servicenames = (List<string>)zookeeperClient.getChildren(Constants.PATH_SEPARATOR + Constants.DEFAULT_DIRECTORY, true);
                foreach (string sname in servicenames)
                {
                    List<string> providers = (List<string>)zookeeperClient.getChildren(Constants.PATH_SEPARATOR + Constants.DEFAULT_DIRECTORY + Constants.PATH_SEPARATOR + sname + Constants.PATH_SEPARATOR + Constants.PROVIDERS_CATEGORY, true);
                    foreach (string provider in providers)
                    {
                        URL url = URL.valueOf(URL.decode(provider));
                        if (url.Protocol.Equals("hessian"))
                        {
                            hessianServiceUrls.Add(url);
                            //Assembly ass = Assembly.LoadFile(@"D:\workspace2016\lab\dubbo-demo\trunk\dotnet-hessian-client\dotnet-hessian-client\bin\Debug\dotnet-hessian-client.exe");
                            Assembly ass = Assembly.Load(DubboConfig.dubbo_service_assembly);

                            Type type = ass.GetType(url.ServiceInterface);
                            if (type == null) continue;

                            HessianInvoke serHessianInvoke = new HessianInvoke(url);

                            IList<object> services;
                            if (this.hessianServices.TryGetValue(url.ServiceInterface, out services))
                            {
                                services.Add(serHessianInvoke.GetService(type));
                            }
                            else
                            {
                                hessianServices.Add(url.ServiceInterface, new List<object> { serHessianInvoke.GetService(type) });
                            }

                        }
                        serviceurls.Add(url);
                    }
                }

            }
        }
    }
}
