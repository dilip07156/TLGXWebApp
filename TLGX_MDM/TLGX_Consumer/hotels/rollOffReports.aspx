<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="rollOffReports.aspx.cs" Inherits="TLGX_Consumer.hotels.rollOffReports" EnableEventValidation="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #ctl00_MainContent_ReportViewer1_ReportViewer{
            height:100%;
            width:100%;
        }
    </style>
    <%--  <script>
        function validatedate() {
            $("#dateerror").hide();
            var startDate = new Date($('#MainContent_fromDate').val());
            var endDate = new Date($('#MainContent_toDate').val());
            days = (endDate- startDate) / (1000 * 60 * 60 * 24);
            if (days > 90 || startDate > endDate) {
                $("#dateerror").show();
                $("#dateerror").append("Please select date greater than From Date and between 90 days..!!!")
            }
          
        }
    </script>
    <br />/<br />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
            <h1 class="page-header">ROLL OFF REPORTS</h1>
             </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblfromdate" runat="server" Text="From Date :"></asp:Label>
                <input type="date"  id="fromDate" runat="server"/>
                  <%--<asp:calender runat="server" id="fromDate"> </asp:calender>--%>
           <%--  </div>
            <div class="col-md-6">
                <asp:Label ID="lbltodate" runat="server" Text="To Date :"></asp:Label>
                <input type="date" id="toDate" onblur="validatedate()"  runat="server"/>--%>
                <%--<asp:Calendar runat="server" id="toDate" onselection="validatedate()"></asp:Calendar>--%>
               <%-- <span id="dateerror" style="color:red"></span>
            </div> 
      </div>
        <br /><hr /><br />
      <div class="row">
          <div class="col-sm-4">
                 <asp:Label ID="lblrule" runat="server" Text="Rules report" CssClass="font-weight: bold;"></asp:Label>&nbsp:&nbsp
                 <asp:Button runat="server" Text="View Status" ></asp:Button>
                 <asp:Button runat="server" Text="Download CSV" ID="btnRuleCsv" OnClick="btnRuleCsv_Click" ></asp:Button>
          </div>
           <div class="col-sm-4">
                 <asp:Label ID="lblstatus" runat="server" Text="Status report"></asp:Label>&nbsp:&nbsp
                 <asp:Button runat="server" Text="View Status" ></asp:Button>
                 <asp:Button runat="server" Text="Download CSV" ID="btnStatusCsv" ></asp:Button>
          </div>
           <div class="col-sm-4">
                 <asp:Label ID="lblupdate" runat="server" Text="Updates report"></asp:Label>&nbsp:&nbsp
                 <asp:Button runat="server" Text="View Status" ></asp:Button>
                 <asp:Button runat="server" Text="Download CSV" ID="btnUpdateCsv"></asp:Button>
          </div>
      </div>
    </div>--%>
  <%--   <div id="collapseSearchResult" class="panel-collapse collapse in">
                            <div class="panel-body">
                                <div class="form-group">
                                    <div id="dvMsg1" runat="server" style="display: none;"></div>
                                </div>
                                <asp:GridView runat="server" ID="grdrule"></asp:GridView>
                            </div>
    </div>--%>
     <div class="row">
        <div class="col-md-6">
            <h1 class="page-header">Suppliers Status</h1>
        </div>

        <div class="col-md-6 ">
            <div class="form-inline">
                <br />
                <br />
                <div class="form-group pull-right ">
                    <asp:DropDownList runat="server" ID="ddlSupplierName" CssClass="form-control" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">--All Suppliers--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnUpdateSupplier" runat="server" CssClass="btn btn-primary btn-sm" Text="View Status" OnClick="btnUpdateSupplier_Click"  />
                </div>
            </div> 
        </div>
    </div>
       <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
           <LocalReport ReportPath="staticdata\Report1.rdlc">
               <DataSources>
                   <rsweb:ReportDataSource Name="DataSet1" />
               </DataSources>
           </LocalReport>
       </rsweb:ReportViewer>
         <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
</asp:Content>
