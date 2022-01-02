using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenManager.Entities.Entities
{
    public class TokenEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public long TotalSupply { get; set; }
        public string ContractAddress { get; set; }
        public int TotalHolders { get; set; }
        public Decimal Price { get; set; }
    }
}
