using System.Net;

namespace DistSession.Lib {
    public class Utils {
        /// <summary>
        /// 取得 Server IP
        /// </summary>
        /// <returns></returns>
        public string GetServerIP() {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList
                .Where(q => q.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault().ToString();
        }

        /// <summary>
        /// 取得 Server Hostname
        /// </summary>
        /// <returns></returns>
        public string GetHostName() {
            return Dns.GetHostName();
        }
    }
}
