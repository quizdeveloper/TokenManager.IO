using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenManager.Core.Database;
using TokenManager.Entities.Condition;
using TokenManager.Entities.Entities;

namespace TokenManager.Dal.Token
{
    public class TokenDal : SqlBase, ITokenDal
    {
        public TokenDal(IDbConnection dbConnection) : base(dbConnection)
        {

        }

        /// <summary>
        /// Update info of a token
        /// </summary>
        /// <param name="token">Token object info</param>
        /// <returns>Number row effected</returns>
        public int Update(TokenEntity token)
        {
            string spName = "SP_Token_Update";
            var param = new
            {
                Id = token.Id,
                Name = token.Name,
                Symbol = token.Symbol,
                TotalSupply = token.TotalSupply,
                ContractAddress = token.ContractAddress,
                TotalHolders = token.TotalHolders
            };

            return ExecuteScalarStoreProcedure(spName, param);

        }

        /// <summary>
        /// Create a token
        /// </summary>
        /// <param name="token">Token object info</param>
        /// <returns>Number row effected</returns>
        public int Create(TokenEntity token)
        {
            if (token == null) return 0;

            string spName = "SP_Token_Create";
            var param = new
            {
                Name = token.Name,
                Symbol = token.Symbol,
                TotalSupply = token.TotalSupply,
                ContractAddress = token.ContractAddress,
                TotalHolders = token.TotalHolders
            };

            return ExecuteScalarStoreProcedure(spName, param);
        }

        /// <summary>
        /// Get token info
        /// </summary>
        /// <param name="tokenId">Token id</param>
        /// <returns>Token object</returns>
        public TokenEntity GetById(int tokenId)
        {
            string spName = "SP_Token_GetById";
            var param = new
            {
                Id = tokenId
            };

            return QuerySingleOrDefault<TokenEntity>(spName, param, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Get list of token
        /// </summary>
        /// <param name="condition">Filter condition</param>
        /// <returns>List of token</returns>
        public IEnumerable<TokenEntity> List(TokenSearchCondition condition)
        {
            string spName = "SP_Token_Find";
            var param = new
            {
                Keyword = condition.Keyword,
                PageIndex = condition.PageIndex,
                PageItem = condition.PageItem
            };

            return QueryStoreProcedure<TokenEntity>(spName, param);
        }

        /// <summary>
        /// Total token found base con List() method
        /// </summary>
        /// <param name="condition">Filter condition</param>
        /// <returns>Count number token</returns>
        public int Count(TokenSearchCondition condition)
        {
            string spName = "SP_Token_Count";
            var param = new
            {
                Keyword = condition.Keyword
            };

            return QuerySingleOrDefault<TokenCountEntity>(spName, param, CommandType.StoredProcedure).TotalRecord;
        }

        /// <summary>
        /// Get token info by symbol
        /// </summary>
        /// <param name="symbol">Symbol of token</param>
        /// <returns>Token object</returns>
        public TokenEntity GetBySymbol(string symbol)
        {
            string spName = "SP_Token_GetBySymbol";
            var param = new
            {
                Symbol = symbol
            };

            return QuerySingleOrDefault<TokenEntity>(spName, param, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Get total supply of all tokens
        /// </summary>
        /// <returns>Total supply</returns>
        public long TotalSupply()
        {
            string spName = "SP_Token_SumSupply";
            return QuerySingleOrDefault<TokenCountEntity>(spName, null, CommandType.StoredProcedure).SumSupply;
        }

        /// <summary>
        /// Update only price info
        /// </summary>
        /// <param name="token">Token info</param>
        /// <returns>Token object</returns>
        public int UpdatePrice(TokenEntity token)
        {
            string spName = "SP_Token_UpdatePrice";
            var param = new
            {
                Id = token.Id,
                Price = token.Price
            };

            return ExecuteScalarStoreProcedure(spName, param);
        }

        /// <summary>
        /// Get list of token no paging
        /// </summary>
        /// <param name="condition">Filter condition</param>
        /// <returns>List of token</returns>
        public IEnumerable<TokenEntity> Export(TokenSearchCondition condition)
        {
            string spName = "SP_Token_Export";
            var param = new
            {
                Keyword = condition.Keyword
            };

            return QueryStoreProcedure<TokenEntity>(spName, param);
        }
    }
}
