using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.eqying.pf.service.provider.api;
using hessiancsharp.client;

namespace com.eqying.pf.service.provider.model.DAL
{
    class HttpInvoke
    {
        //string Url = @"http://192.168.1.101:8080/wchat/remote/apiService";
        string Url = @"http://192.168.100.189:8080/http/userService";
        private static CHessianProxyFactory factory;

        private string token = "";
        private string username = "";

        private static HttpInvoke instance;


        private static String serverIP;
        private static String serverPort;


        public HttpInvoke()
        {
            factory = new CHessianProxyFactory();
        }

        public static HttpInvoke Instance()
        {
            if (instance == null)
            {
                instance = new HttpInvoke();
            }
            return instance;
        }


        /// <summary>
        /// 登陆服务器
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Boolean GetUerInfo(string userId)
        {
            Boolean loginState = false;
            try
            {
                UserServiceI service = (UserServiceI)factory.Create(typeof(UserServiceI), Url);
                User user = service.getUserInfo("1001");
                //Hashtable ht = service.login(username, password);

                Console.WriteLine(user);

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.Message, "出错了!");
            }
            return loginState;
        }

        public static string ServerIP
        {
            get { return serverIP; }
            set { serverIP = value; }
        }

//        public string Url
//        {
//            get { return string.Format(@"http://{0}:{1}/wchat/remote/apiService", ServerIP, ServerPort); }
//        }

        public static string ServerPort
        {
            get { return serverPort; }
            set { serverPort = value; }
        }
    }
}
