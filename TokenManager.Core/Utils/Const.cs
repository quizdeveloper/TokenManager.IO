using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenManager.Core.AppSetting;

namespace TokenManager.Core.Utils
{
    public class Const
    {
        public const int TokenPageItem = 10;
        public const int TokenDefaultPageIndex = 1;

        public static string ApiTokenUpdatePrice = AppSettingUtils.GetString("ApiTokenUpdatePrice");
        public static int UpdateTimePrice = AppSettingUtils.GetInt32("UpdateTimePrice");
    }
}
