using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenManager.Entities.Condition;
using TokenManager.Entities.Entities;

namespace TokenManager.Dal.Token
{
    public interface ITokenDal
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
        /// Update only price info
        /// </summary>
        /// <param name="token">Token info</param>
        /// <returns>Token object</returns>
        int UpdatePrice(TokenEntity token);

        /// <summary>
        /// Get token info
        /// </summary>
        /// <param name="tokenId">Token id</param>
        /// <returns>Token object</returns>
        TokenEntity GetById(int tokenId);

        /// <summary>
        /// Get token info by symbol
        /// </summary>
        /// <param name="symbol">Symbol of token</param>
        /// <returns>Token object</returns>
        TokenEntity GetBySymbol(string symbol);

        /// <summary>
        /// Get list of token
        /// </summary>
        /// <param name="condition">Filter condition</param>
        /// <returns>List of token</returns>
        IEnumerable<TokenEntity> List(TokenSearchCondition condition);

        /// <summary>
        /// Get list of token no paging
        /// </summary>
        /// <param name="condition">Filter condition</param>
        /// <returns>List of token</returns>
        IEnumerable<TokenEntity> Export(TokenSearchCondition condition);

        /// <summary>
        /// Total token found base con List() method
        /// </summary>
        /// <param name="condition">Filter condition</param>
        /// <returns>Count number token</returns>
        int Count(TokenSearchCondition condition);

        /// <summary>
        /// Get total supply of all tokens
        /// </summary>
        /// <returns>Total supply</returns>
        long TotalSupply();
    }
}
