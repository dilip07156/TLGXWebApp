<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccommodationMappingSupplierVSupplier.aspx.cs" Inherits="TLGX_Consumer.staticdata.hotels.AccommodationMappingSupplierVSupplier" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Scripts/JqueryUI/jquery-ui.css" rel="stylesheet" />
    <script src="../../Scripts/JqueryUI/jquery-ui.js"></script>
    <script src="../../Scripts/MultiSelectJS/jquery.sumoselect.min.js"></script>
    <link href="../../Scripts/MultiSelectJS/sumoselect.min.css" rel="stylesheet" />
    <script type="text/javascript">

        $(document).ready(function () {
            SumoSelectdropdown();
        });

        
        function SumoSelectdropdown() {
            $('.testselect3').SumoSelect({
                search: true, searchText: 'Enter Supplier(s).',
                okCancelInMulti: true, triggerChangeCombined: true,
                forceCustomRendering: true, selectAll: true
            });
                         
        }
    </script>



    <h1>Accommodation Mapping Supplier Vs Supplier</h1>
    <hr />

    <br />
    <div class="col-sm-12">
        <div class="form-group row">
            <div id="dvmsgUploadCompleted" runat="server" enableviewstate="false" style="display: none;">
            </div>
        </div>
    </div>
    <div class="col-md-12">
        <div class="panel panel-default">
            <div class="panel-body">

                <div class="form-group col-md-4">
                    <label class="control-label col-sm-4" for="ddlSupplierName">
                        Source Supplier
                    </label>
                    <div class="col-sm-8">
                        <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlSupplierName" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged">
                            <asp:ListItem Value="0">--All Suppliers--</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group col-md-6">
                    <label class="control-label col-sm-5" for="ddlSupplierName">
                        Compare Against Supplier(s)
                    </label>
                    <div class="col-sm-7">
                        <asp:ListBox runat="server" ID="ddlCompareSupplier1" CssClass="testselect3" ClientIDMode="Static" SelectionMode="multiple"></asp:ListBox>

                    </div>
                </div>
                <div class="form-group col-md-2">
                    <asp:Button runat="server" Text="View Report" CssClass="btn btn-sm btn-primary" ID="btnViewReport" OnClick="btnViewReport_Click"></asp:Button>
                </div>
            </div>
        </div>


    </div>
    <div class="col-md-12">
        <div id="report" runat="server" style="width: 100%; height: 100%; overflow-x: scroll">
            <rsweb:ReportViewer ID="ReportViewerAccommodationMappingSupplierVSupplier" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="true" ZoomMode="FullPage" ShowFindControls="False">
                <LocalReport ReportPath="hotels\AccommodationMappingSupplierVSupplier.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource Name="DsAccommodationMappingSupplierVSupplier" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </div>
</asp:Content>
