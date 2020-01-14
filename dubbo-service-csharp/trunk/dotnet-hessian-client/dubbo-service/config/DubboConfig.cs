using SharpConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using com.alibaba.dubbo.common.utils;
using com.alibaba.dubbo.service;

namespace com.alibaba.dubbo.config
{
    public class DubboConfig
    {
        public static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(DubboConfig));


        public static string dubbo_registry_protocol = "zookeeper";
        public static string dubbo_registry_address = "";
        public static string dubbo_registry_client = "curator";
        public static string dubbo_registry_register= "true";
        public static string dubbo_registry_subscribe = "true";

        public static string dubbo_application_name;
        public static string dubbo_application_owner;
        public static string dubbo_application_organization;

        public static string dubbo_service_interface_assembly = "";
        public static int dubbo_service_loadbalance = 0;
        public static int dubbo_service_reties = 3;
        public static int dubbo_service_timeout = 10000;
        public static int dubbo_service_threadpool_size = 10;

        public static string dubbo_service_assembly= "";
        public static int dobbo_service_port;
        public static string dubbo_service_protocol = "hessian";
        private static DubboConfig instance;

        private static readonly object locker = new object();
        private DubboConfig()
        {
            Init();
        }

        public static DubboConfig Instance()
        {
            if (instance == null)
            {
                lock (locker)
                {                 
                    instance = new DubboConfig();
                }
            }
            return instance;
        }

        static DubboConfig()
        {
            Init();
        }

        // 定义私有构造函数，使外界不能创建该类实例
        public static void Init()
        {
            try
            {
                Configuration config;
                if (AppUtils.IsWebApp())
                {
                    logger.Info("加载配置文件dubbo.cfg,"+ HttpContext.Current.Server.MapPath("~/dubbo.cfg"));
                    config = Configuration.LoadFromFile(HttpContext.Current.Server.MapPath("~/dubbo.cfg"));
                    if (config == null)
                    {
                        config = Configuration.LoadFromFile("c:/dubbo.cfg");
                    }
                }
                else
                {
                    config = Configuration.LoadFromFile(System.IO.Directory.GetCurrentDirectory() + "\\dubbo.cfg");
                }

                var sectionRegistry = config["registry"];
                dubbo_registry_address = sectionRegistry["dubbo.registry.address"].StringValue;

                var applicationClient = config["application"];
                if (applicationClient != null)
                {
                    dubbo_application_name = applicationClient["dubbo.application.owner"].StringValue;
                    dubbo_application_owner = applicationClient["dubbo.application.owner"].StringValue;
                    dubbo_application_organization = applicationClient["dubbo.application.organization"].StringValue;
                }

                var sectionClient = config["client"];
                if (sectionClient != null)
                {
                    dubbo_service_interface_assembly = sectionClient["dubbo.service.interface.assembly"].StringValue;
                    dubbo_service_loadbalance = sectionClient["dubbo.service.loadbalance"].IntValue;
                    dubbo_service_reties = sectionClient["dubbo.service.reties"].IntValue;
                    dubbo_service_timeout = sectionClient["dubbo.service.timeout"].IntValue;
                    dubbo_service_threadpool_size = sectionClient["dubbo.service.threadpool.size"].IntValue;
                }

                var sectionService = config["service"];
                if (sectionService != null)
                {
                    dobbo_service_port = sectionService["dubbo.service.port"].IntValue;
                    dubbo_service_assembly = sectionService["dubbo.service.assembly"].StringValue;
                    dubbo_service_protocol = sectionService["dubbo.service.protocol"].StringValue;

                }
                logger.Info("dubboconfig init success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception="+ex);
                logger.Error("dubboconfig is init error",ex);
            }                
        }

    }
}
