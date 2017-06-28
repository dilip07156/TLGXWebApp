<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchCKISMasters.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ckis.searchCKISMasters" %>
<style type="text/css">
    .modalPopup {
        background-color: #696969;
        filter: alpha(opacity=40);
        opacity: 0.7;
        xindex: -1;
    }

    .progress, .alert {
        margin: 15px;
    }

    .alert {
        display: none;
    }
</style>
<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);

    function showManageModal() {
        $("#moActivityManage").modal('show');
    }
    function closeManageModal() {
        $("#moActivityManage").modal('hide');
    }
    function pageLoad(sender, args) {
        var hv = $('#MainContent_searchCKISMasters_hdnFlag').val();
        if (hv == "true") {
            closeManageModal();
            $('#MainContent_searchCKISMasters_hdnFlag').val("false");
        }
    }
</script>


<asp:UpdatePanel ID="udpSearchDllChange" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel-group" id="accordion">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Filters</a>
                            </h4>
                        </div>

                        <div id="collapseSearch" class="panel-collapse collapse in">

                            <div class="panel-body">

                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <br />
                                        <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="HotelSearch" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="ddlCountry">Country</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="ddlCity">City</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="txtHotelName">Product Name</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtHotelName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="ddlCKISType">
                                            CKIS Type
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlCKISType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="ddlCKISActivityType">
                                            Activity Type
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlCKISActivityType" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="ddlStatus">
                                            Status
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="ddlPageSize">
                                            Status
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                                <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <br />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" ValidationGroup="HotelSearch" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" OnClick="btnReset_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>


        <div class="panel-group" id="accordionSearchResult">
            <div class="panel panel-default">

                <div class="panel-heading">
                    <h4 class="panel-title">
                        <!-- search results need to be wired in -->
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearchResult">Search Results (Total Count:
            <asp:Label ID="lblTotalCount" runat="server" Text="0"></asp:Label>)
                        </a></h4>

                </div>

                <div id="collapseSearchResult" class="panel-collapse collapse in">

                    <div class="panel-body">
                        <div class="form-group">
                            <div id="dvMsg" runat="server" style="display: none;"></div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:GridView ID="grdCKISData" runat="server" AllowPaging="True" AutoGenerateColumns="False" AllowCustomPaging="True"
                                    EmptyDataText="No data found" CssClass="table table-hover table-striped"
                                    DataKeyNames="Activity_Id" OnSelectedIndexChanged="grdCKISData_SelectedIndexChanged" OnPageIndexChanging="grdCKISData_PageIndexChanging"
                                    OnRowCommand="grdCKISData_RowCommand" OnSorting="grdCKISData_Sorting" OnDataBound="grdCKISData_DataBound"
                                    OnRowDataBound="grdCKISData_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="CKIS Type" DataField="ProductSubType" SortExpression="ProductSubType" />
                                        <asp:BoundField HeaderText="Country" DataField="Country" SortExpression="Country" />
                                        <asp:BoundField HeaderText="City" DataField="City" SortExpression="City" />
                                        <asp:BoundField HeaderText="Id" DataField="CommonProductId" SortExpression="CommonProductId" />
                                        <asp:BoundField HeaderText="Name" DataField="Product_Name" SortExpression="Product_Name" />
                                        <asp:BoundField HeaderText="Status" DataField="Product_Name" SortExpression="Product_Name" />
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Manage" CssClass="btn btn-default"
                                                    Enabled="true" CommandArgument='<%# Bind("Activity_Id") %>' OnClientClick="showManageModal();">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Manage
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="Delete" CssClass="btn btn-default"
                                                    Enabled="true" CommandArgument='<%# Bind("Activity_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Delete
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
      
    </ContentTemplate>
</asp:UpdatePanel>
<div class="modal fade" id="moActivityManage" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">

            <div class="modal-header">
                <div class="panel panel-default">
                    <h4 class="modal-title">CKIS Master Detail (Read Only)</h4>
                </div>
            </div>
            <div class="modal-body">
                <asp:UpdateProgress ID="pUpdatePanel1" runat="server" AssociatedUpdatePanelID="UpdActivityManageModal">
                    <ProgressTemplate>
                        <h4 style="color: red;"><b>Processing... </b>&nbsp;</h4>
                        <div class="progress">
                            <div class="progress-bar" role="progressbar" aria-valuenow="70"
                                aria-valuemin="0" aria-valuemax="100" style="width: 70%">
                                <span class="sr-only">70% Complete</span>
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdActivityManageModal" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
                        <!-- need to wrap this in a modal and pop from selectedindexchanged and change default mode to EDIT  -->
                        <asp:FormView ID="frmCKISMasterData" runat="server" DefaultMode="Insert" DataKeyNames="Activity_Id" OnItemCommand="frmCKISMasterData_ItemCommand">

                            <EditItemTemplate>

                                <div class="row">

                                    <div class="col-lg-4">

                                        <div class="panel panel-default">
                                            <div class="panel-heading">Product Detail</div>
                                            <div class="panel-body">
                                                <table class="table table-condensed">
                                                    <tbody>
                                                        <tr>
                                                            <td><strong>CKIS Id</strong></td>
                                                            <td>
                                                                <asp:Label ID="lblCommonProductID" runat="server" Text='<%# Bind("CommonProductID") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><strong>CKIS Type</strong></td>
                                                            <td>
                                                                <asp:Label ID="lblProductSubType" runat="server" Text='<%# Bind("ProductSubType") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><strong>Product Name</strong></td>
                                                            <td>
                                                                <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("Product_Name") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><strong>Country</strong></td>
                                                            <td>
                                                                <asp:Label ID="lblCountry" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><strong>City</strong></td>
                                                            <td>
                                                                <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><strong>CKIS Category</strong></td>
                                                            <td>
                                                                <asp:Label ID="lblProductCategory" runat="server" Text='<%# Bind("ProductCategory") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><strong>Create Date</strong></td>
                                                            <td>
                                                                <asp:Label ID="lblCreateDate" runat="server" Text='<%# Bind("Create_Date") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><strong>Edit Date</strong></td>
                                                            <td>
                                                                <asp:Label ID="lblEditDate" runat="server" Text='<%# Bind("Edit_Date") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><strong>Duration</strong></td>
                                                            <td>
                                                                <asp:Label ID="lblDuration" runat="server" Text='<%# Bind("Duration") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><strong>Description</strong></td>
                                                            <td>
                                                                <asp:Label ID="lblLongDescription" runat="server" Text='<%# Bind("LongDescription") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-4">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">Inclusions</div>
                                            <div class="panel-body">
                                                <table class="table table-condensed table-striped">
                                                    <asp:Repeater ID="rptInclusions" runat="server">
                                                        <HeaderTemplate>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblInclusion" runat="server" Text='<%# Eval("Content_Text") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-lg-4">

                                        <div class="panel panel-default">
                                            <div class="panel-heading">Exclusions</div>
                                            <div class="panel-body">
                                                <table class="table table-condensed  table-striped">
                                                    <asp:Repeater ID="rptExclusion" runat="server">
                                                        <HeaderTemplate>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblExclusion" runat="server" Text='<%# Eval("Content_Text") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-8">

                                        <div class="panel panel-default">
                                            <div class="panel-heading">Notes</div>
                                            <div class="panel-body">
                                                <table class="table table-condensed  table-striped">
                                                    <asp:Repeater ID="rptNotes" runat="server">
                                                        <HeaderTemplate>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblNotes" runat="server" Text='<%# Eval("Content_Text") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </div>

                                    </div>
                            </EditItemTemplate>
                        </asp:FormView>
                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

