using Shadowsocks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Shadowsocks.Util
{
    public class ProxySpeedTest
    {
        public ProxySpeedTest()
        {
        }

        public static float DoTest(Server server)
        {
            IList<float> singles = new List<float>();
            for (int i = 0; i < 2; i++)
            {
                float single = ProxySpeedTest.TestProxy(server.server, server.server_port);
                if (single > 0f)
                {
                    singles.Add(single);
                }
            }
            if (singles.Count == 0)
            {
                return 2.14748365E+09f;
            }
            return singles.Average();
        }

        private static float TestProxy(string addr, int port)
        {
            float totalSeconds;
            try
            {
                DateTime now = DateTime.Now;
                TcpClient tcpClient = new TcpClient();
                IAsyncResult asyncResult = tcpClient.BeginConnect(addr, port, null, null);
                if (!asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5)))
                {
                    throw new Exception("Failed to connect.");
                }
                tcpClient.EndConnect(asyncResult);
                totalSeconds = (float)(DateTime.Now - now).TotalSeconds;
            }
            catch (Exception exception)
            {
                totalSeconds = -1f;
            }
            return totalSeconds;
        }
    }
}
