using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenManager.Entities.Entities;

namespace TokenManager.Entities.Models
{
    public class TokenModel
    {
        public TokenModel()
        {

        }

        public TokenModel(TokenEntity entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Symbol = entity.Symbol;
            this.TotalSupply = entity.TotalSupply;
            this.ContractAddress = entity.ContractAddress;
            this.Price = entity.Price;
            this.TotalHolders = entity.TotalHolders;
        }

        public TokenModel(TokenEntity entity, long overralSupply)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Symbol = entity.Symbol;
            this.TotalSupply = entity.TotalSupply;
            this.ContractAddress = entity.ContractAddress;
            this.Price = entity.Price;
            this.TotalHolders = entity.TotalHolders;
            this.TotalSupplyPercent = overralSupply <= 0 || this.TotalSupply <= 0 ? "" : ((decimal)this.TotalSupply / (decimal)overralSupply * 100).ToString("0.0000") + "%";
            this.TotalSupplyPercentNumber = overralSupply <= 0 || this.TotalSupply <= 0 ? 0 : (decimal)this.TotalSupply / (decimal)overralSupply * 100;
        }

        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public long TotalSupply { get; set; }
        public int TotalHolders { get; set; }
        public string ContractAddress { get; set; }
        public string TotalSupplyPercent { get; set; }
        public decimal TotalSupplyPercentNumber { get; set; }
        public string LinkDetail
        {
            get
            {
                if (string.IsNullOrEmpty(Symbol)) return "";
                return string.Format("/detail/{0}", Symbol.ToLower());
            }
        }
        public Decimal Price { get; set; }
        public string PriceFormat
        {
            get
            {
                if (Price <= 0) return "$ 0.00";
                return string.Format("$ {0}", this.Price.ToString("#.##"));
            }
        }

        public string TotalSupplyFormat
        {
            get
            {
                if (TotalSupply <= 0) return "0";
                return this.TotalSupply.ToString("#,##");
            }
        }

        public string TotalHoldersFormat
        {
            get
            {
                if (TotalHolders <= 0) return "0";
                return this.TotalHolders.ToString("#,##");
            }
        }
    }
}
