<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Views/Layout/Site.Master" AutoEventWireup="true" CodeBehind="TokenDetail.aspx.cs" Inherits="TokenManager.TokenDetail" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron bg-white bd-gray home-box">
        <div class="row">
            <div class="col-sm-12">

                <table class="table table-23">
                    <tbody>
                        <tr>
                            <td colspan="2">
                                <strong>
                                    <asp:Label ID="lblContractName" runat="server"></asp:Label>
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td class="percent-40">Price</td>
                            <td>
                                <strong>
                                    <asp:Label ID="lblPrice" runat="server"></asp:Label>
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td class="percent-40">Total Supply</td>
                            <td>
                                <asp:Label ID="lblTotalSupply" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="percent-40">Total Holders</td>
                            <td>
                                <asp:Label ID="lblTotalHolders" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="percent-40">Name</td>
                            <td>
                                <asp:Label ID="lblTokenName" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>

            </div>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <a href="/" class="btn btn-info">Back</a>
            </div>
        </div>
    </div>
</asp:Content>
