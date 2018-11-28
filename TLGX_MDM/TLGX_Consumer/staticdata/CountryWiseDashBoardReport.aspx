<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CountryWiseDashBoardReport.aspx.cs" Inherits="TLGX_Consumer.staticdata.CountryWiseDashBoardReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">

     <link href="../Scripts/JqueryUI/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/JqueryUI/jquery-ui.js"></script>
    <script src="../Scripts/MultiSelectJS/jquery.sumoselect.min.js"></script>
    <link href="../Scripts/MultiSelectJS/sumoselect.min.css" rel="stylesheet" />
      <script type="text/javascript">

        $(document).ready(function () {
            SumoSelectdropdown();
           
        });


        function pageLoad() {
            SumoSelectdropdown();
        }

        function SumoSelectdropdown() {
          
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
            SumoSelectdropdown();
        });


      
    </script>

    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Hotel Mapping Country Report</h1>
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
                                        <label class="control-label col-md-4" for="ddlKeys">Key</label>
                                        <div class="col-md-8">
                                            <asp:ListBox runat="server" ID="ddlKeys" ClientIDMode="Static" SelectionMode="multiple"></asp:ListBox>
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
                            <br />
                            <div class="row">
                                

                              

                            </div>
                            <br />
                            <div class="row">

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
                                <%--  View REPORT Button--%>
                                <div class="col-md-12">
                                    
                                    <asp:Button runat="server" Text="View Report" CssClass="btn btn-sm btn-primary pull-right" ID="btnViewReport" OnClick="btnViewReport_Click" ></asp:Button>
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
    <div class="container">
        <div style="width: 100%; height: 100% ; overflow-x:scroll">
            <rsweb:ReportViewer ID="CountryReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="True" ZoomMode="FullPage" ShowFindControls="False">
                <LocalReport ReportPath="staticdata\HotelMappingCountryReport.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </div>


</asp:Content>
