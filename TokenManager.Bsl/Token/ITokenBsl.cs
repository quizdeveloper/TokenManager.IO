using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenManager.Entities.Condition;
using TokenManager.Entities.Entities;
using TokenManager.Entities.Models;

namespace TokenManager.Bsl.Token
{
    public interface ITokenBsl
    {
        /// <summary>
        /// Create a token
        /// </summary>
        /// <param name="token">Token object info</param>
        /// <returns>Number row effected</returns>
        int Create(TokenEntity token);

        /// <summary>
        /// Update info of a token
        /// </summary>
        /// <param name="token">Token object info</param>
        /// <returns>Number row effected</returns>
        int Update(TokenEntity token);

        /// <summary>
        /// Get token info
        /// </summary>
        /// <param name="tokenId">Token id</param>
        /// <returns>Token object</returns>
        TokenModel GetById(int tokenId);

        /// <summary>
        /// Get token by symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        TokenModel GetBySymbol(string symbol);

        /// <summary>
        /// Get list of token
        /// </summary>
        /// <param name="condition">Filter condition</param>
        /// <returns>List of token</returns>
        IEnumerable<TokenModel> List(TokenSearchCondition condition);

        /// <summary>
        /// Get list of token with no paging
        /// </summary>
        /// <param name="condition">Filter condition</param>
        /// <returns>List of token</returns>
        IEnumerable<TokenModel> Export(TokenSearchCondition condition);

        /// <summary>
        /// Total token found base con List() method
        /// </summary>
        /// <param name="condition">Filter condition</param>
        /// <returns>Count number token</returns>
        int Count(TokenSearchCondition condition);
    }
}
