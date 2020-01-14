using Microsoft.VisualStudio.TestTools.UnitTesting;
using dubbo_service.common.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dubbo_service.common.utils.Tests
{
    [TestClass()]
    public class IpUtilsTests
    {
        [TestMethod()]
        public void GetLocalIpv4Test()
        {
            string[] ips = IpUtils.GetLocalIpv4();
            foreach (var ip in ips)
            {
                Console.WriteLine("ip============="+ ip);         
            }
        }
    }
}