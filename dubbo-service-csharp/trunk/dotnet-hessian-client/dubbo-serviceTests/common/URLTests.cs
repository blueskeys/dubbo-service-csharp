using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.alibaba.dubbo.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.alibaba.dubbo.common.Tests
{
    [TestClass()]
    public class URLTests
    {
        [TestMethod()]
        public void valueOfTest()
        {
            string uul = @"hessian%3a%2f%2f192.168.100.10%3a36955%2fhessiantest.hessian%3f?application=account-service&interface%3ddubbo_service_demo_webserver.Service.IDubboService&timestamp=1480921923658";
            URL url = URL.valueOf(URL.decode(uul));
            Console.WriteLine(url);

            string uul2 = "dubbo://192.168.100.242:12109/com.vyying.account.service.api.module.account.AccountApiService?application=account-service&default.retries=0&default.timeout=10000&default.validation=true&dubbo=2.8.4.4-SNAPSHOT&dubbo://192.168.100.242:12109/com.vyying.account.service.api.module.account.AccountApiService?anyhost=true&generic=false&interface=com.vyying.account.service.api.module.account.AccountApiService&methods=queryAccountInfo,queryAccount,login,queryAccountByName&organization=wyying&owner=account&pid=17716&revision=0.1.0-SNAPSHOT&serialization=hessian2&server=netty&side=provider&threadpool=cached&threads=500&timestamp=1480921923658";
            URL url2 = URL.valueOf(URL.decode(uul2));
            Console.WriteLine(url2);

            string uul3 = "/dubbo/com.eqying.pf.service.provider.api.IService/providers";
            string[] urls = uul3.Split('/');
            foreach (var s in urls)
            {
                Console.WriteLine(s);
            }

        }

        public void testLambda()
        {
            User u = new User();
            u.Name = "1";
            u.Address = "11";
            User u2 = new User();
            u2.Name = "2";
            u2.Address = "22";
            List<User> users = new List<User>();
            users.Add(u);
            users.Add(u2);
            List<string> names = new List<string>();
            users.ForEach(uu => names.Add(u.Name));

        }

        class User
        {
            private string name;
            private string address;

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public string Address
            {
                get { return address; }
                set { address = value; }
            }
        }
    }
}