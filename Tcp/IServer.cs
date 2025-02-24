using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcp
{
    public interface IServer
    {
        public Uri Uri { get; }
        public int Port { get; }

        /// <summary>
        ///  In HTTP/1.1, persistent connections (or "keep-alive") are the default behavior if no Connection header is specified.
        /// </summary>
        public bool KeepAlive { get; }
    }
}
