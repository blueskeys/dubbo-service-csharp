using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Autofac;
using com.alibaba.dubbo.common;
using com.alibaba.dubbo.remoting.zookeeper;
using com.alibaba.dubbo.remoting.zookeeper.zkclient;
using com.eqying.pf.service.provider.api;
using com.eqying.pf.service.provider.model.DAL;
using com.wyying.service.rpc.hessian;
using dotnet_hessian_client.DAL;
using ZooKeeperNet;
using com.alibaba.dubbo.service;

namespace dotnet_hessian_client.zookeeper
{
    class ZookeeperDemo
    {
       
        public void Init()
        {

            UserServiceI userservice =(UserServiceI) ServiceConsumerContainer.Instance().GetHessianServices(typeof(UserServiceI).FullName);

            Console.WriteLine(userservice.getUserInfo("UserServiceI"));

        }

    }
}
