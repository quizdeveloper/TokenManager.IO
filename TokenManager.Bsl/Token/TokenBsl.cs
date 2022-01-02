using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenManager.Core.Utils;
using TokenManager.Dal.Token;
using TokenManager.Entities.Condition;
using TokenManager.Entities.Entities;
using TokenManager.Entities.Models;

namespace TokenManager.Bsl.Token
{
    public class TokenBsl : ITokenBsl
    {
        private ITokenDal _tokenDal;

        public TokenBsl(ITokenDal tokenDal)
        {
            _tokenDal = tokenDal;
        }

        public int Count(TokenSearchCondition condition)
        {
            return _tokenDal.Count(condition);
        }

        public int Create(TokenEntity token)
        {
            return _tokenDal.Create(token);
        }

        public IEnumerable<TokenModel> Export(TokenSearchCondition condition)
        {
            var totkens = _tokenDal.Export(condition);
            if (totkens != null)
            {
                var sumSupply = totkens.Sum(x => x.TotalSupply);
                var models = totkens.Select(x => new TokenModel(x, sumSupply)).ToList();
                if (models != null && models.Any())
                {
                    int i = 0;
                    foreach (var token in models)
                    {
                        i++;
                        token.Index = i++;
                    }
                }
                return models;
            }
            return null;
        }

        public TokenModel GetById(int tokenId)
        {
            var totkenObj = _tokenDal.GetById(tokenId);
            if (totkenObj == null) return null;
            return new TokenModel(totkenObj);
        }

        public TokenModel GetBySymbol(string symbol)
        {
            var totkenObj = _tokenDal.GetBySymbol(symbol);
            if (totkenObj == null) return null;
            return new TokenModel(totkenObj);
        }

        public IEnumerable<TokenModel> List(TokenSearchCondition condition)
        {
            var totkens = _tokenDal.List(condition);
            var sumSupply = _tokenDal.TotalSupply();
            if (totkens == null) return null;
            var models = totkens.Select(x => new TokenModel(x, sumSupply)).ToList();
            if(models != null && models.Any())
            {
                int i = 0;
                foreach(var token in models)
                {
                    i++;
                    token.Index = (condition.PageIndex - 1) * Const.TokenPageItem + i;
                }
            }
            return models;
        }

        public int Update(TokenEntity token)
        {
            return _tokenDal.Update(token);
        }
    }
}
