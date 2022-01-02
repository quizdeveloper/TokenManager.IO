using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using TokenManager.Bsl.Token;
using TokenManager.Core.Logger;
using TokenManager.Core.Utils;
using TokenManager.Entities.Condition;
using TokenManager.Entities.Entities;

namespace TokenManager
{
    public partial class Token : Page
    {
        #region Variables
        [Dependency]
        public ITokenBsl _tokenBsl { get; set; }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Page.Title = "Token management page";
                    TokenSearchCondition condition = new TokenSearchCondition();
                    condition.Keyword = txtKeyword.Text.ToLower().Trim();
                    var totalToken = _tokenBsl.Count(condition);
                    grvToken.VirtualItemCount = totalToken;

                    GridviewBinding(Const.TokenDefaultPageIndex, Const.TokenPageItem);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogType.Error, ex.ToString());
            }
        }

        protected void grvToken_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                int currentPage = e.NewPageIndex + 1;
                GridviewBinding(currentPage, Const.TokenPageItem);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogType.Error, ex.ToString());
            }
        }

        protected void grvToken_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Update")
                {
                    // Get index of row passed as command argument
                    int rowIndex = e.CommandArgument.ToInt();
                    int tokenId = ((HiddenField)(grvToken.Rows[rowIndex].Cells[0].FindControl("hdTokenId"))).Value.ToInt();
                    if (tokenId > 0)
                    {
                        var tokenObj = _tokenBsl.GetById(tokenId);
                        if (tokenObj != null)
                        {
                            hdTokenId.Value = tokenObj.Id.ToString();
                            txtName.Text = tokenObj.Name;
                            txtSymbol.Text = tokenObj.Symbol;
                            txtContractAddress.Text = tokenObj.ContractAddress;
                            txtTotalSupply.Text = tokenObj.TotalSupply.ToString();
                            txtTotalHolders.Text = tokenObj.TotalHolders.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogType.Error, ex.ToString());
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    // Get value from form
                    TokenEntity entity = new TokenEntity();
                    entity.Id = hdTokenId.Value.ToInt(0);
                    entity.Name = txtName.Text;
                    entity.Symbol = txtSymbol.Text.ToLower();
                    entity.ContractAddress = txtContractAddress.Text;
                    entity.TotalSupply = txtTotalSupply.Text.ToLong(0);
                    entity.TotalHolders = txtTotalHolders.Text.ToInt(0);

                    // Validate Symbol exist
                    var tokenBySymbol = _tokenBsl.GetBySymbol(entity.Symbol);
                    if (entity.Id > 0)
                    {
                        var tokenById = _tokenBsl.GetById(entity.Id);
                        if (tokenById != null)
                        {
                            if (tokenBySymbol != null && tokenBySymbol.Id != tokenById.Id)
                            {
                                lblValidateExistSymbol.Visible = true;
                            }
                            else
                            {
                                _tokenBsl.Update(entity);
                                ResetForm();
                                int pageIndex = Session["CurrentPageIndex"] != null ? Session["CurrentPageIndex"].ToInt(1) : Const.TokenDefaultPageIndex;
                                GridviewBinding(pageIndex, Const.TokenPageItem);
                            }
                        }
                    }
                    else
                    {
                        if (tokenBySymbol != null)
                        {
                            lblValidateExistSymbol.Visible = true;
                        }
                        else
                        {
                            _tokenBsl.Create(entity);
                            ResetForm();
                            int pageIndex = Session["CurrentPageIndex"] != null ? Session["CurrentPageIndex"].ToInt(1) : Const.TokenDefaultPageIndex;
                            GridviewBinding(pageIndex, Const.TokenPageItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogType.Error, ex.ToString());
            }
        }

        protected void grvToken_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GridviewBinding(Const.TokenDefaultPageIndex, Const.TokenPageItem);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogType.Error, ex.ToString());
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                TokenSearchCondition condition = new TokenSearchCondition();
                condition.Keyword = txtKeyword.Text.ToLower().Trim();
                var exportToken = _tokenBsl.Export(condition).ToList();

                if (exportToken != null && exportToken.Any())
                {
                    StringWriter sw = new StringWriter();
                    sw.WriteLine("\"Rank\",\"Symbol\",\"Name\",\"Contract Address\",\"Total Holders\",\"Total Supply\",\"Total Supply %\"");
                    int tokenRank = 0;
                    foreach (var token in exportToken)
                    {
                        tokenRank++;
                        sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"",
                                                   tokenRank,
                                                   token.Symbol.ToUpper(),
                                                   token.Name,
                                                   token.ContractAddress,
                                                   token.TotalHoldersFormat,
                                                   token.TotalSupplyFormat,
                                                   token.TotalSupplyPercent
                                                   ));
                    }

                    string fileName = string.Format("{0}_{1}", "TokenExport", DateTime.UtcNow.ToString("dd-MM-yyyy-HHmmss"));

                    // Write response. 
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
                    Response.ContentType = "text/csv";
                    Response.Write(sw.ToString());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogType.Error, ex.ToString());
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Binding data for gridview
        /// </summary>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageItem">Total items in a page</param>
        private void GridviewBinding(int pageIndex, int pageItem)
        {
            //1. Get list token
            if (pageIndex <= 0) pageIndex = 1;
            TokenSearchCondition condition = new TokenSearchCondition();
            condition.Keyword = txtKeyword.Text.ToLower().Trim();
            condition.PageIndex = pageIndex;
            condition.PageItem = pageItem;
            var listToken = _tokenBsl.List(condition).ToList();

            //2. Binding data to gridview
            UpdatePagger();
            grvToken.AllowPaging = true;
            grvToken.AllowCustomPaging = true;
            grvToken.PageIndex = pageIndex - 1;
            grvToken.PageSize = pageItem;
            grvToken.DataSource = listToken;
            grvToken.DataBind();
            Session["CurrentPageIndex"] = pageIndex;

            // 3. Show export button if have data
            btnExport.Enabled = false;
            if (listToken != null && listToken.Any()) btnExport.Enabled = true;
            //4. Binding Chart
            DrawChart();
        }

        /// <summary>
        /// Reset form
        /// </summary>
        private void ResetForm()
        {
            hdTokenId.Value = "0";
            txtName.Text = "";
            txtSymbol.Text = "";
            txtContractAddress.Text = "";
            txtTotalSupply.Text = "";
            txtTotalHolders.Text = "";
            lblValidateExistSymbol.Visible = false;
        }

        /// <summary>
        /// Update pagging for girdview
        /// </summary>
        private void UpdatePagger()
        {
            TokenSearchCondition condition = new TokenSearchCondition();
            condition.Keyword = txtKeyword.Text.ToLower().Trim();
            var totalToken = _tokenBsl.Count(condition);
            grvToken.VirtualItemCount = totalToken;
        }

        /// <summary>
        /// Draw chart
        /// </summary>
        private void DrawChart()
        {
            TokenSearchCondition condition = new TokenSearchCondition();
            condition.Keyword = txtKeyword.Text.ToLower().Trim();
            var exportToken = _tokenBsl.Export(condition).ToList();

            string labels = string.Join(",", exportToken.Select(x => x.Name).ToList());
            string datas = string.Join(",", exportToken.Select(x => x.TotalSupply).ToList());
            labels = "'" + labels.Replace(",", "','") + "'";
            string chartDoughnut = "";
            chartDoughnut += "<script>";
            chartDoughnut += "	const data = {";
            chartDoughnut += "		labels: [" + labels + "],";
            chartDoughnut += "		datasets: [";
            chartDoughnut += "			{";
            chartDoughnut += "				label: '',";
            chartDoughnut += "				data: [" + datas + "],";
            chartDoughnut += "				backgroundColor: Object.values(CHART_COLORS),";
            chartDoughnut += "			}";
            chartDoughnut += "		]";
            chartDoughnut += "	};";

            chartDoughnut += "	const config = {";
            chartDoughnut += "		type: 'doughnut',";
            chartDoughnut += "		data: data,";
            chartDoughnut += "		options: {";
            chartDoughnut += "			animation: {duration : 0},";
            chartDoughnut += "			responsive: true,";
            chartDoughnut += "			plugins: {";
            chartDoughnut += "				legend: {";
            chartDoughnut += "					display: true,";
            chartDoughnut += "					position: 'bottom',";
            chartDoughnut += "				},";
            chartDoughnut += "				title: {";
            chartDoughnut += "					display: true,";
            chartDoughnut += "					text: 'Token Statistics by Total Supply'";
            chartDoughnut += "				}";
            chartDoughnut += "			}";
            chartDoughnut += "		},";
            chartDoughnut += "	};";

            chartDoughnut += "	var myChart = new Chart(";
            chartDoughnut += "		document.getElementById('tokenChart'),";
            chartDoughnut += "		config";
            chartDoughnut += "	);";
            chartDoughnut += "</script>";
            lrChart.Text = chartDoughnut;
        }

        #endregion


    }
}