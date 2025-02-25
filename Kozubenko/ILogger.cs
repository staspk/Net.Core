using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kozubenko
{
    public interface ILogger
    {
        public void Log(string message, Exception? e = null);
    }
}
