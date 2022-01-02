using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TokenManager.Bsl.Token;
using TokenManager.Core.Utils;
using TokenManager.Entities.Condition;

namespace TokenManager
{
    public partial class TokenDetail : Page
    {
        [Dependency]
        public ITokenBsl _tokenBsl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Detail token page";
                string symbol = Page.RouteData.Values["symbol"].ToString();
                if(string.IsNullOrEmpty(symbol)) Response.Redirect("/");
                var tokenObj = _tokenBsl.GetBySymbol(symbol);
                if(tokenObj == null) Response.Redirect("/");
                lblContractName.Text = tokenObj.ContractAddress;
                lblPrice.Text = tokenObj.PriceFormat;
                lblTotalSupply.Text = tokenObj.TotalSupplyFormat;
                lblTotalHolders.Text = tokenObj.TotalHoldersFormat;
                lblTokenName.Text = tokenObj.Name;
            }
        }
    }
}