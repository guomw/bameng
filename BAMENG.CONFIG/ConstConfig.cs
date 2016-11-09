﻿using System;
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

        /// <summary>
        /// 商户编号(对应商城的编号)
        /// </summary>
        public const int storeId = 296;

        //public const string Bearer = "Bearer ";

    }
}
