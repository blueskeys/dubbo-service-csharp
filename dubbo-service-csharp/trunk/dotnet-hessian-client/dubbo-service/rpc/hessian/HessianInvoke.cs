using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using com.alibaba.dubbo.common;
using hessiancsharp.client;


namespace com.wyying.service.rpc.hessian
{
public class HessianInvoke
    {
        public static readonly log4net.ILog logger = log4net.LogManager.GetLogger("HessianInvoke");


        private static CHessianProxyFactory factory;
        private URL url;

        public object GetService(Type clazz)
        {
            if (!url.Protocol.Equals("hessian"))
            {
                logger.Error("Service Protocol is not hessian");
            }
            string hessianurl = "http://"+url.Address+url.AbsolutePath;
            return factory.Create(clazz, hessianurl);
            //return service;
        }

        public HessianInvoke(URL url)
        {
            this.url = url;
            factory = new CHessianProxyFactory();
        }
    }
}
