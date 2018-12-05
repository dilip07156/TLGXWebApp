<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HotelMappingCityWiseReport.aspx.cs" Inherits="TLGX_Consumer.staticdata.HotelMappingCityWiseReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Scripts/JqueryUI/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/JqueryUI/jquery-ui.js"></script>
    <script src="../Scripts/MultiSelectJS/jquery.sumoselect.min.js"></script>
    <link href="../Scripts/MultiSelectJS/sumoselect.min.css" rel="stylesheet" />

    <script type="text/javascript">

        $(document).ready(function () {
            SumoSelectdropdown();
            callajax();
        });


        function pageLoad() {
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
                search: true, searchText: 'Enter Priority.', okCancelInMulti: true, triggerChangeCombined: true,
                forceCustomRendering: true, selectAll: true
            });

            $('#<%=ddlKeys.ClientID %>').SumoSelect({
                search: true, searchText: 'Enter Key.', okCancelInMulti: true, triggerChangeCombined: true,
                forceCustomRendering: true, selectAll: true
            });


            $('#<%=ddlRanks.ClientID %>').SumoSelect({
                search: true, searchText: 'Enter Ranks.', okCancelInMulti: true, triggerChangeCombined: true,
                forceCustomRendering: true, selectAll: true
            });

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            callajax();
            SumoSelectdropdown();
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
                        //data: JSON.stringify(selected),
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
                    //$("#hdnLookupCity").val(ui.item.label);
                    //// $("#hdnLookupCity_Id").val(ui.item.value);
                    $("#btnAdd").removeAttr("disabled");
                },


                min_length: 3,
                delay: 300
            });
        }

    </script>

    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Hotel Mapping City Report</h1>
        </div>
    </div>
    <%-- SEARCH FILTERS--%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>

            <div class="container">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Filters</a>
                        </h4>
                    </div>
                    <div id="collapseSearch" class="panel-collapse collapse in" aria-expanded="true">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-4" for="ddlRegion">Region Name</label>
                                        <div class="col-md-8">
                                            <asp:ListBox runat="server" ID="ddlRegion" AutoPostBack="true" ClientIDMode="Static" SelectionMode="multiple" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-4" for="ddlKeys">Key</label>
                                        <div class="col-md-8">
                                            <asp:ListBox runat="server" ID="ddlKeys" ClientIDMode="Static" SelectionMode="multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-4" for="ddlCountry">Country Name</label>
                                        <div class="col-md-8">
                                            <asp:ListBox runat="server" ID="ddlCountry" AutoPostBack="true" ClientIDMode="Static" SelectionMode="multiple" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-4" for="ddlPriorities">Priority</label>
                                        <div class="col-md-8">
                                            <asp:ListBox runat="server" ID="ddlPriorities" ClientIDMode="Static" SelectionMode="multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label  col-md-3" for="ddlCity">City</label>
                                        <div class="col-md-9">
                                            <div class="col-md-12">
                                                <div class="col-md-6">
                                                    <asp:RadioButton ID="rdoIsAllCities" runat="server" Text="All Cities" GroupName="SelectedByCities" OnCheckedChanged="rdoIsAllCities_CheckedChanged" AutoPostBack="true" />
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    <asp:RadioButton ID="rdoIsSelectiveCities" runat="server" Text="Selective Cities" GroupName="SelectedByCities" OnCheckedChanged="rdoIsSelectiveCities_CheckedChanged" AutoPostBack="true" />
                                                </div>
                                                <div class="col-md-8">
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtCityLookup" runat="server" class="form-control" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                                    </div>

                                                </div>

                                            </div>
                                        
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-4" for="ddlRanks">Rank</label>
                                        <div class="col-md-8">
                                            <asp:ListBox runat="server" ID="ddlRanks" ClientIDMode="Static" SelectionMode="multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <%--  Display Cities--%>
                                <div class="col-md-6">
                                    <asp:Repeater ID="repSelectedCity" runat="server" OnItemCommand="repSelectedCity_ItemCommand">
                                        <HeaderTemplate>
                                            <table class="table table-condensed table-striped table-hover table-bordered" id="tblSelectedCity">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="display: none;">
                                                    <asp:Label ID="lblCityId" runat="server" Style="display: none;" Text='<%# DataBinder.Eval(Container.DataItem, "City_Id") %>'></asp:Label>
                                                    <asp:Label ID="lblCityCode" runat="server" Style="display: none;" Text='<%# DataBinder.Eval(Container.DataItem, "City_Code") %>'></asp:Label>
                                                </td>
                                                <td class="col-md-8">
                                                    <asp:Label ID="lblCityName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CityName") %>'></asp:Label>
                                                </td>
                                                <td class="col-md-1">
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
                                <%--  END Display Cities--%>
                            </div>

                            <div class="row">
                                <%--  View REPORT Button--%>
                                <div class="col-md-12">
                                    <div class="col-md-3 col-sm-push-4">
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-sm btn-primary" ClientIDMode="Static" Text="Add" Enabled="false" OnClick="btnAdd_Click"></asp:Button>
                                    </div>
                                    <asp:Button runat="server" Text="View Report" CssClass="btn btn-sm btn-primary pull-right" ID="btnViewReport" OnClick="btnViewReport_Click"></asp:Button>
                                </div>
                                <%-- END  View REPORT Button--%>
                            </div>


                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- ERROR DIV--%>
    <div id="errordiv" runat="server" class="col-md-12 alert alert-info" style="display: none;">
        <p id="nulldate">NO DATA FOUND...!!</p>
    </div>
    <%--   ReportViewer--%>
    <div class="container" id="HotelMappingCityreport" runat="server">
        <div style="width: 100%; height: 100%; overflow-x: scroll">
            <rsweb:ReportViewer ID="CityReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="True" ZoomMode="FullPage" ShowFindControls="False">
                <LocalReport ReportPath="staticdata\HotelMappingCityReport.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </div>
</asp:Content>
