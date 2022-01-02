<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Views/Layout/Site.Master" AutoEventWireup="true" CodeBehind="Token.aspx.cs" Inherits="TokenManager.Token" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <webopt:BundleReference runat="server" Path="~/Content/home" />

    <div class="jumbotron bg-white bd-gray home-box">
        <div class="row">
            <div class="col-sm-5">
                <h4>Save / Update Token</h4>
                <div class="form-group row">
                    <label for="txtName" class="col-sm-4 col-form-label normal-400">Name <span class="red-clr">*</span></label>
                    <div class="col-sm-8">
                        <asp:HiddenField ID="hdTokenId" runat="server" Value="0"></asp:HiddenField>
                        <asp:TextBox ID="txtName" CssClass="form-control" runat="server" placeholder="Name" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="Name cannot be blank" ControlToValidate="txtName" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group row">
                    <label for="txtSymbol" class="col-sm-4 col-form-label normal-400">Symbol <span class="red-clr">*</span></label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtSymbol" CssClass="form-control upper-text" runat="server" placeholder="Symbol" MaxLength="5"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="Symbol cannot be blank" ControlToValidate="txtSymbol" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblValidateExistSymbol" runat="server" CssClass="red-clr" Text="Symbol existed! Please enter other symbol." Visible="false"></asp:Label>
                    </div>
                </div>

                <div class="form-group row">
                    <label for="txtContractAddress" class="col-sm-4 col-form-label normal-400">Contract Address <span class="red-clr">*</span></label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtContractAddress" CssClass="form-control" runat="server" placeholder="Contract Address" MaxLength="66"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="Contract address cannot be blank" ControlToValidate="txtContractAddress" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group row">
                    <label for="txtTotalSupply" class="col-sm-4 col-form-label normal-400">Total Supply <span class="red-clr">*</span></label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtTotalSupply" CssClass="form-control" runat="server" placeholder="Total Supply" MaxLength="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="Total supply cannot be blank" ControlToValidate="txtTotalSupply" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RangeValidator Type="Double" ID="RangeValidator2" Display="Dynamic" runat="server" ErrorMessage="Total supply must be from 1" ControlToValidate="txtTotalSupply" ForeColor="Red" MinimumValue="1" MaximumValue="999999999999999" ValidationGroup="Save"></asp:RangeValidator>
                    </div>
                </div>

                <div class="form-group row">
                    <label for="txtTotalHolders" class="col-sm-4 col-form-label normal-400">Total Holders <span class="red-clr">*</span></label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtTotalHolders" CssClass="form-control" runat="server" placeholder="Total Holders" MaxLength="9"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="Total holders cannot be blank" ControlToValidate="txtTotalHolders" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RangeValidator Type="Integer" ID="RangeValidator1" Display="Dynamic" runat="server" ErrorMessage="Total holders must be from 1" ControlToValidate="txtTotalHolders" ForeColor="Red" MinimumValue="1" MaximumValue="999999999" ValidationGroup="Save"></asp:RangeValidator>
                    </div>
                </div>

                <div class="form-group row">
                    <label for="btnSave" class="col-sm-4 col-form-label normal-400">&nbsp;</label>
                    <div class="col-sm-8">
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="Save" />
                        &nbsp;
                        <asp:Button ID="btnReset" CssClass="btn btn-info" runat="server" Text="Reset" OnClick="btnReset_Click" />
                    </div>
                </div>

            </div>
            <div class="col-sm-7">
                <div class="token-chart">
                    <canvas id="tokenChart"></canvas>
                </div>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.7.0/chart.min.js"></script>
                <script src="../../Scripts/const.js"></script>
                <asp:Literal ID="lrChart" runat="server"></asp:Literal>
            </div>
        </div>

        <div class="row mg-top-40">
            <div class="col-sm-6 col-xs-9">
                <div class="form-row">
                    <div class="col-sm-7 no-pf-left col-xs-9">
                        <asp:TextBox ID="txtKeyword" CssClass="form-control" runat="server" placeholder="Search by Name or Symbol" onkeypress="return SearchEvent(event)"></asp:TextBox>
                    </div>
                    <div class="col-sm-5 col-xs-3 no-pf-left">
                        <asp:Button ID="btnSearch" CssClass="btn btn-success mb-2" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
            <div class="col-sm-6 text-right col-xs-3">
                <asp:Button ID="btnExport" CssClass="btn btn-info mb-2" runat="server" Text="Export" Enabled="False" OnClick="btnExport_Click" />
            </div>
        </div>
        <div class="row mg-top-20">
            <div class="col-sm-12">
                <asp:GridView ID="grvToken" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" OnPageIndexChanging="grvToken_PageIndexChanging" OnRowCommand="grvToken_RowCommand" OnRowUpdating="grvToken_RowUpdating" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found">
                    <Columns>
                        <asp:TemplateField HeaderText="Rank">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdTokenId" runat="server" Value='<%# Bind("Id") %>' />
                                <asp:Label ID="lblRank" runat="server" Text='<%# Bind("Index") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField DataTextField="Symbol" ItemStyle-CssClass="upper-text" HeaderText="Symbol" DataNavigateUrlFields="LinkDetail" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="ContractAddress" HeaderText="Contract Address" />
                        <asp:BoundField DataField="TotalHoldersFormat" HeaderText="Total Holders" />
                        <asp:BoundField DataField="TotalSupplyFormat" HeaderText="Total Supply" />
                        <asp:BoundField DataField="TotalSupplyPercent" HeaderText="Total Supply %" />
                        <asp:ButtonField CommandName="Update" Text="Edit" />
                    </Columns>
                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="3" NextPageText="Next" PreviousPageText="Prev" />
                    <PagerStyle CssClass="pager-style" />
                </asp:GridView>
               
            </div>
        </div>
    </div>

    <script>
        function SearchEvent(e) {
            if (e.keyCode == 13) {
                __doPostBack('<%=btnSearch.UniqueID%>', "");

                e.preventDefault();
                e.stopPropagation();
                return false;
            }
        }
    </script>
</asp:Content>
