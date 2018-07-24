<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExportSupplierReport.aspx.cs" Inherits="TLGX_Consumer.staticdata.ExportSupplierReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Export Supplier Report</h1>
        </div>
    </div>


<%--    <asp:UpdatePanel runat="server" ID="PnlUpdateDiv">
        <ContentTemplate>--%>
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="col-md-12">

                            <div class="col-md-2">
                                <asp:CheckBox ID="chkIsMDMDataOnly" runat="server" Text="MDM Acco Data Only" />
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList runat="server" ID="ddlSupplierName" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">--All Suppliers--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:Button runat="server" Text="View Report" CssClass="btn btn-sm btn-primary" ID="btnViewReport" OnClick="btnViewReport_Click"></asp:Button>
                                <asp:Button runat="server" Text="Export" CssClass="btn btn-sm btn-primary" ID="btnExportSuppilerCsv" OnClick="btnExportSupplierCsv_Click"></asp:Button>
                            </div>
                        </div>

                        <div class="col-md-12" style="overflow-x:auto">
                            <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped"
                                ShowHeaderWhenEmpty="false" ShowFooter="true" EmptyDataText="No data found for Suppliers." OnDataBound="gvSupplier_DataBound" Style="overflow-x: scroll" OnRowDataBound="gvSupplier_RowDataBound">
                                <Columns>
                                    <asp:BoundField HeaderText="Priority" DataField="Priority" />
                                    <asp:BoundField HeaderText="Supplier Name" DataField="SupplierName" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField HeaderText="LAST FETCHED FROM" DataField="Country_LastFetched" />
                                    <asp:BoundField HeaderText="COUNTRY TOTAL RECORDS RECEIVED FROM SUPPLIER" DataField="Country_TotalRecordReceived" />
                                    <asp:BoundField HeaderText="COUNTRY AUTO MAPPED" DataField="Country_AutoMapped" />
                                    <asp:BoundField HeaderText="COUNTRY MANNUAL MAPPED" DataField="Country_MannualMapped" />
                                    <asp:BoundField HeaderText="COUNTRY REVIEW" DataField="Country_ReviewMapped" />
                                    <asp:BoundField HeaderText="COUNTRY UNMAPPED" DataField="Country_Unmapped" />
                                    <asp:BoundField HeaderText="COUNTRY TOTAL" DataField="CountryTotal" />
                                    <asp:BoundField HeaderText="COUNTRY COMPLETE %" DataField="Country_CompletePercentage" />
                                    <asp:BoundField HeaderText="CITY TOTAL RECORDS RECEIVED FROM SUPPLIER" DataField="City_TotalRecordReceived" />
                                    <asp:BoundField HeaderText="CITY AUTO MAPPED" DataField="City_AutoMapped" />
                                    <asp:BoundField HeaderText="CITY MANNUAL MAPPED" DataField="City_MannualMapped" />
                                    <asp:BoundField HeaderText="CITY REVIEW" DataField="City_ReviewMapped" />
                                    <asp:BoundField HeaderText="CITY UNMAPPED" DataField="City_Unmapped" />
                                    <asp:BoundField HeaderText="CITY TOTAL" DataField="CityTotal" />
                                    <asp:BoundField HeaderText="CITY COMPLETED %" DataField="City_CompletePercentage" />
                                    <asp:BoundField HeaderText="HOTEL TOTAL RECORDS RECIVED FROM SUPPLIER" DataField="Hotel_TotalRecordReceived" />
                                    <asp:BoundField HeaderText="HOTEL AUTO MAPPED" DataField="Hotel_AutoMapped" />
                                    <asp:BoundField HeaderText="HOTEL MANNUAL MAPPED" DataField="Hotel_MannualMapped" />
                                    <asp:BoundField HeaderText="HOTEL REVIEW" DataField="Hotel_ReviewMapped" />
                                    <asp:BoundField HeaderText="HOTEL UNMAPPED" DataField="Hotel_Unmapped" />
                                    <asp:BoundField HeaderText="HOTEL TOTAL" DataField="HotelTotal" />
                                    <asp:BoundField HeaderText="HOTEL COMPLETE %" DataField="Hotel_CompletePercentage" />
                                    <asp:BoundField HeaderText="TOTAL ROOM TYPES AVAILABLE FROM SUPPLIERS" DataField="AvaialbleFromSupplier" />
                                    <asp:BoundField HeaderText="ROOM MAPPED" DataField="HotelsMapped" />
                                    <asp:BoundField HeaderText="Eligible Room" DataField="TotalEligibleRoom" />
                                    <asp:BoundField HeaderText="ROOM AUTO MAPPED" DataField="Room_AutoMapped" />
                                    <asp:BoundField HeaderText="ROOM MANUAL MAPPED" DataField="Room_MannualMapped" />
                                    <asp:BoundField HeaderText="ROOM REVIEW" DataField="Room_ReviewMapped" />
                                    <asp:BoundField HeaderText="Add" DataField="Room_Add" />
                                    <asp:BoundField HeaderText="ROOM UNMAPPED" DataField="Room_Unmapped" />
                                    <asp:BoundField HeaderText="ROOM TOTAL" DataField="RoomTotal" />
                                    <asp:BoundField HeaderText="ROOM COMPLETE %" DataField="Room_CompletePercentage" />

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>
