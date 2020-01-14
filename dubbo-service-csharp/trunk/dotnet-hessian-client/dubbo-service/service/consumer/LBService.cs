using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dubbo_service.common.utils
{
    /// <summary>
    /// 负载均衡算法
    /// </summary>
    public abstract class LBService
    {

        // 待路由的Ip列表，Key代表Ip，Value代表该Ip的权重
        //public static Dictionary<string, int?> serverWeightMap = new Dictionary<string, int?>();

        //private static List<string> serverList = new List<string>();

        public abstract List<string> GetServerList();

        /// <summary>
        ///**********************
        ///       轮询          
        /// **********************
        /// </summary>
        private static int pos = 0;

        public string RoundRobin()
        {
            string server = null;
            lock (this)
            {
                if (pos > GetServerList().Count)
                {
                    pos = 0;
                }
                server = GetServerList()[pos];
                pos++;
            }
            return server;
        }


        /// <summary>
        ///**********************
        ///       随机            *
        /// **********************
        /// </summary>
        public string Random()
        {

            Random random = new Random();
            int randomPos = random.Next(GetServerList().Count);

            return GetServerList()[randomPos];
        }

        /// <summary>
        ///**********************
        ///       源地址hash      *
        /// **********************
        /// </summary>
        public string Hash(string remoteIp)
        {
            int hashCode = remoteIp.GetHashCode();
            int serverListSize = GetServerList().Count;
            int serverPos = hashCode % serverListSize;

            return GetServerList()[serverPos];
        }


        /// <summary>
        ///**********************
        ///       加权轮询         *
        /// **********************
        /// </summary>
        public  string WeightRoundRobin()
        {

            string server = null;
            lock (this)
            {
                if (pos > GetServerList().Count)
                {
                    pos = 0;
                }
                server = GetServerList()[pos];
                pos++;
            }

            return server;
        }


        /// <summary>
        ///**********************
        ///       加权随机         *
        /// **********************
        /// </summary>
        public string WeightRandom()
        {

            Random random = new Random();
            int randomPos = random.Next(GetServerList().Count);

            return GetServerList()[randomPos];
        }
    }

}
