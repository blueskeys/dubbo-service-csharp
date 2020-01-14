using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dubbo_service.common;
using dubbo_service.common.utils;
using com.alibaba.dubbo.config;

namespace com.alibaba.dubbo.service
{
    /// <summary>
    /// hessian的Service封装，里面包括负载均衡，失败重试逻辑
    /// </summary>
    public class HessianDubboService : LBService,  IDubboService
    {
        private string serviceName;
        private List<string> serverList = new List<string>();
        private readonly Dictionary<string, object> hessianServices = new Dictionary<string, object>();

        public HessianDubboService(string serviceName,string ip, object service)
        {
            this.serviceName = serviceName;
            AddService(ip, service);
        }


        public void AddService(string ip,object service)
        {
            if (!hessianServices.ContainsKey(ip))
            {
                hessianServices.Add(ip,service);
            }
        }

        public override List<string> GetServerList()
        {
            foreach (var hessianServicesKey in hessianServices.Keys)
            {
                serverList.Add(hessianServicesKey);
            }
            return serverList;
        }

        public object GetService()
        {
            int policy = DubboConfig.dubbo_service_loadbalance;
            switch (policy)
            {
                case (int)LoadBalancePolicy.Hash:
                    return hessianServices[Hash(IpUtils.GetSingleLocalIpv4())];
                    case (int)LoadBalancePolicy.Random:
                    return hessianServices[Random()];
                default:
                    return hessianServices[RoundRobin()];

            }
        }

    }
}
