using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenManager.Bsl.Token;
using TokenManager.Core.AppSetting;
using TokenManager.Core.Utils;
using TokenManager.Dal.DBHelper;
using TokenManager.Dal.RestHelper;
using TokenManager.Dal.Token;
using TokenManager.Entities.Condition;
using TokenManager.Entities.Models;

namespace TokenManager.Services
{
    public class Process
    {
        private readonly IRestHelper _autoManagerClient;
        private readonly ITokenDal _tokenDal;
        public Process()
        {
            Dictionary<string, string> connectionsDic = new Dictionary<string, string>();
            connectionsDic.Add("TokenManagerConnectionString", AppSettingUtils.GetConnection("TokenManagerConnectionString"));
            var db = (SqlConnection)new DbConnectionFactory(connectionsDic).CreateMsSqlConnection("TokenManagerConnectionString");

            _tokenDal = new TokenDal(db);
            _autoManagerClient = new RestHelper(Const.ApiTokenUpdatePrice);
        }

        public void Run()
        {
            string actionUrl = "/data/price?fsym={0}&tsyms=usd";
            var condition = new TokenSearchCondition();
            condition.PageIndex = 1;
            condition.PageItem = 1000; // I only got top 1000 records to process. In case have a lot of records, we can process by batch with BULK UPDATE (ex: each batch has 1000 records and loop to end, it same with paging).
            var tokens = _tokenDal.List(condition);
            if(tokens != null && tokens.Any())
            {
                foreach(var token in tokens)
                {
                    if(token.Id > 0)
                    {
                        // Get price from API
                        string actionUrlFull = string.Format(actionUrl, token.Symbol.ToLower());
                        PriceServiceModel autoModel = _autoManagerClient.GetRequest<PriceServiceModel>(actionUrlFull);
                        Decimal tokenPrice = autoModel != null ? autoModel.USD : 0;

                        // Update price to DB
                        var tokenObj = _tokenDal.GetById(token.Id);
                        if(tokenObj != null)
                        {
                            tokenObj.Price = tokenPrice;
                            _tokenDal.UpdatePrice(tokenObj);
                        }

                    }
                }
            }
        }
    }
}
