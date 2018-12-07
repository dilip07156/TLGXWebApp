<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HotelMappingReport.aspx.cs" Inherits="TLGX_Consumer.staticdata.HotelMappingReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Scripts/JqueryUI/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/JqueryUI/jquery-ui.js"></script>
    <script src="../Scripts/MultiSelectJS/jquery.sumoselect.min.js"></script>
    <link href="../Scripts/MultiSelectJS/sumoselect.min.css" rel="stylesheet" />

    <script type="text/javascript">

        $(document).ready(function () {
            //enableClickableOptGroups: true;
            SumoSelectdropdown();
            callajax();
        });


        function pageLoad(sender, args) {
            // enableClickableOptGroups: true;
            SumoSelectdropdown();
        }

        function SumoSelectdropdown() {

            $('#<%=ddlRegion.ClientID %>').SumoSelect({
                search: true, searchText: 'Enter Region.', okCancelInMulti: true, triggerChangeCombined: true,
                forceCustomRendering: true, selectAll: true
            });

            $('#<%=ddlCountry.ClientID %>').SumoSelect({
                search: true, searchText: 'Enter Country.', okCancelInMulti: true, triggerChangeCombined: true,
                forceCustomRendering: true, selectAll: true
            });

            $('#<%=ddlPriorities.ClientID %>').SumoSelect({
                search: true, searchText: 'Enter AccoPriority.', okCancelInMulti: true, selectAll: true
            });
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            callajax();
        });


        function callajax() {

            var selected = [];
            $('[id*=ddlCountry] option:selected').each(function () {
                selected.push($(this).val());
            });
            var DC_CitywithMultipleCountry_Search_RQ = {};
            DC_CitywithMultipleCountry_Search_RQ["CountryIdList"] = selected;

            $("[id*=txtCityLookup]").autocomplete({

                source: function (request, response) {
                    DC_CitywithMultipleCountry_Search_RQ["source"] = 'CityMap';
                    DC_CitywithMultipleCountry_Search_RQ["CityName"] = request.term;
                    $.ajax({
                        type: 'POST',
                        url: '../../../Service/MultipleCountrywiseCityAutoComplete.ashx',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(DC_CitywithMultipleCountry_Search_RQ),
                        responseType: "json",
                        success: function (result) {
                            if (result != null && result.length > 0) {
                                var data = [];
                                for (var i = 0; i < result.length; i++) {
                                    if (result[i].City_Name != null) {
                                        var cityname = result[i].City_Name;
                                        data.push(cityname);
                                    }
                                }
                                response(data);
                            }
                            else {
                                var data = [];
                                var NoDataFound = "No Data Found";
                                data.push(NoDataFound);
                                response(data);
                            }
                        }
                    });
                },

                select: function (event, ui) {
                    $("#btnAdd").removeAttr("disabled");
                },

                min_length: 3,
                delay: 300
            });
        }
    </script>

    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Hotel Mapping Report</h1>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlRegion" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCountry" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="rdoIsAllCities" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="rdoIsSelectiveCities" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="repSelectedCity"  />
            <asp:AsyncPostBackTrigger ControlID="btnViewReport" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">

                                <div class="col-md-1">
                                    <label class="control-label" for="ddlRegion">Region</label>
                                </div>

                                <div class="col-md-3">
                                    <asp:ListBox runat="server" ID="ddlRegion" AutoPostBack="true" ClientIDMode="Static" SelectionMode="multiple" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"></asp:ListBox>
                                </div>
                                <div class="col-md-8">
                                </div>

                            </div>
                            <br />
                            <br />
                            <div class="col-md-12">

                                <div class="col-md-1">
                                    <label class="control-label" for="ddlCountry">Country</label>
                                </div>

                                <div class="col-md-3">
                                    <asp:ListBox runat="server" ID="ddlCountry" AutoPostBack="true" ClientIDMode="Static" SelectionMode="multiple" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:ListBox>
                                </div>

                                <div class="col-md-2">
                                </div>
                                <div class="col-md-1">
                                </div>

                                <div class="col-md-2">
                                    <%--<label class="control-label" for="ddlAccoPriority">Accomodation Priority</label>--%>
                                </div>
                                <div class="col-md-2">
                                    <%--<asp:ListBox runat="server" ID="ddlAccoPriority" ClientIDMode="Static" SelectionMode="multiple"></asp:ListBox>--%>
                                </div>

                            </div>
                            <br />
                            <br />
                            <div class="col-md-12">

                                <div class="col-md-1">
                                    <label class="control-label" for="ddlCity">City</label>
                                </div>

                                <div class="col-md-1">
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdoIsAllCities" runat="server" Text="All Cities" GroupName="SelectedByCities" OnCheckedChanged="rdoIsAllCities_CheckedChanged" AutoPostBack="true" />
                                        <label class="radio-inline">
                                </div>

                                <div class="col-md-1">
                                </div>

                                <div class="col-md-2">
                                </div>

                                <div class="col-md-2">
                                </div>

                                <div class="col-md-2">
                                </div>
                                <div class="col-md-2">
                                </div>

                            </div>

                            <div class="col-md-12">

                                <div class="col-md-1">
                                </div>

                                <div class="col-md-1">
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdoIsSelectiveCities" runat="server" Text="Selective Cities" GroupName="SelectedByCities" OnCheckedChanged="rdoIsSelectiveCities_CheckedChanged" AutoPostBack="true" />
                                        <label class="radio-inline">
                                </div>

                                <div class="col-md-2">
                                    <asp:TextBox ID="txtCityLookup" onClientClick="callajax();" runat="server" class="form-control" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-sm btn-primary" ClientIDMode="Static" Text="Add" Enabled="false" OnClick="btnAdd_Click"></asp:Button>
                                </div>
                            </div>



                            <div class="col-md-12">
                                <asp:Repeater ID="repSelectedCity" runat="server" OnItemCommand="repSelectedCity_ItemCommand">
                                    <HeaderTemplate>
                                        <table class="table table-stripped table-hover" id="tblSelectedCity">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="row">
                                            <td class="col-md-2">
                                                <asp:Label ID="lblCityId" runat="server" Style="display: none;" Text='<%# DataBinder.Eval(Container.DataItem, "City_Id") %>'></asp:Label>
                                                <asp:Label ID="lblCityCode" runat="server" Style="display: none;" Text='<%# DataBinder.Eval(Container.DataItem, "City_Code") %>'></asp:Label>
                                            </td>
                                            <td class="col-md-5">
                                                <asp:Label ID="lblCityName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CityName") %>'></asp:Label>
                                            </td>
                                            <td class="col-md-5">
                                                <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnRemoveCity" ToolTip="Delete" CommandName="RemoveCity" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "City_Id") %>'>
                                                        <i class="glyphicon glyphicon-minus"></i>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table> 
                                    </FooterTemplate>
                                </asp:Repeater>

                            </div>
                            <br />
                            <br />
                            <div class="col-md-12">

                                <div class="col-md-1">
                                    <label class="control-label" for="ddlPriorities">Priority</label>
                                </div>

                                <div class="col-md-3">
                                    <asp:ListBox runat="server" ID="ddlPriorities" ClientIDMode="Static" SelectionMode="multiple"></asp:ListBox>
                                </div>

                                <div class="col-md-2">
                                </div>
                                <div class="col-md-1">
                                </div>

                                <div class="col-md-2">
                                    <%--<label class="control-label" for="ddlAccoPriority">Accomodation Priority</label>--%>
                                </div>
                                <div class="col-md-2">
                                    <%--<asp:ListBox runat="server" ID="ddlAccoPriority" ClientIDMode="Static" SelectionMode="multiple"></asp:ListBox>--%>
                                </div>

                            </div>

                        </div>

                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <asp:Button runat="server" Text="View Report" CssClass="btn btn-sm btn-primary pull-right" ID="btnViewReport" OnClick="btnViewReport_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />

                        <div class="col-md-12" id="HotelMappingreport" runat="server">
                            <div style="width: 100%; height: 100%; overflow-x: scroll">

                                <rsweb:ReportViewer ID="RptHotelMapping" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="true" ZoomMode="FullPage" ShowFindControls="False">

                                    <LocalReport ReportPath="staticdata/HotelMappingRDLCReport.rdlc">
                                        <DataSources>
                                            <rsweb:ReportDataSource Name="DsHotelReport" />
                                        </DataSources>
                                    </LocalReport>
                                </rsweb:ReportViewer>

                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>


