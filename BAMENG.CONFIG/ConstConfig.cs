using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAMENG.CONFIG
{
    public class ConstConfig
    {

#if DEBUG
        /// <summary>
        /// 签名密钥
        /// </summary>
        public const string SECRET_KEY = "BAMEENG20161021_TEST";
#else
        public const string SECRET_KEY = "BAMEENG20161021";
#endif

    }
}
