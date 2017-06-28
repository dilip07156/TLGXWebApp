<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_portSearch.ascx.cs" Inherits="TLGX_Consumer.controls.geography.portSearch" %>
<script type="text/javascript">
    function showPortModal() {
        //alert('show');
        $("#moAddUpdatePort").modal('show');
    }
    function closePortModal() { $("#moAddUpdatePort").modal('hide'); }
    function pageLoad(sender, args) {
        var hv = $('#hdnFlag').val();
        if (hv == "true") {
            closePortModal();
            $('#hdnFlag').val("false");
        }
    }
</script>
<asp:Panel ID="panSearchConditions" runat="server">
    <div class="panel-group" id="accordion">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Ports</a>
                </h4>
            </div>
            <div id="collapseSearch" class="panel-collapse collapse in">
                <div class="panel-body">
                    <div class="container">
                        <div class="col-sm-6">
                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="ddlMasterCountry">System Country</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList EnableViewState="true" ID="ddlMasterCountry" AutoPostBack="true" runat="server" ClientIDMode="Static" CssClass="form-control" OnSelectedIndexChanged="ddlMasterCountry_SelectedIndexChanged">
                                        <asp:ListItem Text="-ALL-" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="ddlMasterCity">System City</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlMasterCity" runat="server" EnableViewState="true" CssClass="form-control" ClientIDMode="Static" AppendDataBoundItems="true">
                                        <asp:ListItem Text="-ALL-" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="ddlStatus">Mapping Status</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="txtSuppName">Port Country Name</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtSuppCountry" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="ddlShowEntries">Entries</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlShowEntries" AutoPostBack="true" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged" runat="server" CssClass="form-control">
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>35</asp:ListItem>
                                        <asp:ListItem>40</asp:ListItem>
                                        <asp:ListItem>45</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row btn-group">
                                <div class="col-sm-12 row">
                                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" />
                                    <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" />
                                    <asp:UpdatePanel ID="updbtnCreateNew" runat="server">
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="btnNewPort" OnClick="btnNewPort_Click" OnClientClick="showPortModal();" CssClass="btn btn-primary btn-sm" Text="Add New Port" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-sm-12">&nbsp; </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <h4>Search Results</h4>
    <hr />

    <asp:UpdatePanel ID="UpdPortSearch" runat="server">
        <ContentTemplate>
            <div id="dvMsg" runat="server" style="display: none;"></div>
            <asp:GridView ID="grdPortList" Style="height: 200px; overflow: auto" runat="server" AllowPaging="True" AllowSorting="True" AllowCustomPaging="true" AutoGenerateColumns="False" DataKeyNames="Port_Id" CssClass="table table-hover table-striped" OnPageIndexChanging="grdPortList_PageIndexChanging" OnRowCommand="grdPortList_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Oag_portname" HeaderText="Oag_portname" SortExpression="Oag_portname" />
                    <asp:BoundField DataField="Oag_name" HeaderText="Oag_name" SortExpression="Oag_name" />
                    <asp:BoundField DataField="CountryName" HeaderText="CountryName" SortExpression="CountryName" />
                    <asp:HyperLinkField DataNavigateUrlFields="Port_Id" Text="Select" DataNavigateUrlFormatString="~/geography/portManage?Port_Id={0}" NavigateUrl="~/geography/portManage" HeaderText="Manage" />
                </Columns>
                <PagerStyle CssClass="pagination-ys" BorderStyle="None" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
<div class="modal fade" id="moAddUpdatePort" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <div class="input-group">
                    <h4 class="input-group-addon">Add Port </h4>
                </div>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpdUserAddModal" runat="server">
                    <ContentTemplate>
                        <div class="container">
                            <div class="form-group row">
                                <div class="col-sm-6">
                                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="Port" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    <div id="msgAlert" runat="server" style="display: none;"></div>
                                    <asp:HiddenField ID="hdnFlag" ClientIDMode="Static" runat="server" Value="" EnableViewState="false" />

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Port Information</div>
                                    <div class="panel-body">
                                        <asp:FormView ID="frmPortdetail" CssClass="col-lg-12" runat="server" OnItemCommand="frmPortdetail_ItemCommand" DefaultMode="Insert">
                                            <InsertItemTemplate>
                                                <div class="col-lg-12">
                                                    <div class="col-lg-6">
                                                        <div class="form-group row">
                                                            <label for="txtoag_portname" class="col-md-4 col-form-label">
                                                                oag_portname
                            <asp:RequiredFieldValidator ValidationGroup="Port" runat="server" ControlToValidate="txtoag_portname"
                                CssClass="text-danger" ErrorMessage="The OAG Port name is required." Text="*" />
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtoag_portname" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <asp:Label runat="server" AssociatedControlID="txtOAG_loc" CssClass="col-md-4 control-label">OAG Loc
                                        <asp:RequiredFieldValidator ValidationGroup="Port" runat="server" ControlToValidate="txtOAG_loc"
                                                                                CssClass="text-danger" ErrorMessage="The OAG Loc field is required." Text="*" />
                                                            </asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtOAG_loc" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <asp:Label runat="server" AssociatedControlID="txtOAG_multicity" CssClass="col-md-4 control-label">OAG Multicity
                                       <%--<asp:RequiredFieldValidator ValidationGroup="Port" runat="server" ControlToValidate="txtOAG_multicity"
                                             CssClass="text-danger" ErrorMessage="The OAG Multicity field is required."  Text="*"/>--%>
                                                            </asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtOAG_multicity" CssClass="form-control" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            <label for="txtoag_inactive" class="col-md-4 control-label">oag_inactive</label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtoag_inactive" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtMappingStatus" class="col-md-4 col-form-label">MappingStatus</label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtMappingStatus" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtoag_ctryname" class="col-md-4 col-form-label">oag_ctryname</label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtoag_ctryname" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtoag_state" class="col-md-4 col-form-label">oag_state</label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtoag_state" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtoag_substate" class="col-md-4 col-form-label">oag_substate</label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtoag_substate" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtoag_timediv" class="col-md-4 col-form-label">oag_timediv</label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtoag_timediv" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtoag_lat" class="col-md-4 col-form-label">oag_lat</label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtoag_lat" CssClass="form-control" />
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="form-group row">
                                                            <asp:Label runat="server" AssociatedControlID="ddlCountryEdit" CssClass="col-md-4 control-label">Country
                                <asp:RequiredFieldValidator ValidationGroup="Port" runat="server" ControlToValidate="ddlCountryEdit" CssClass="text-danger"
                                     ErrorMessage="The Country is required." InitialValue ="0"  Text="*"/>
                                                            </asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlCountryEdit" AutoPostBack="true" OnSelectedIndexChanged="ddlCountryEdit_SelectedIndexChanged" CssClass="form-control">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <asp:Label runat="server" AssociatedControlID="ddlStateEdit" CssClass="col-md-4 control-label">State
                                 <asp:RequiredFieldValidator ValidationGroup="Port" runat="server" ControlToValidate="ddlStateEdit"
                                      CssClass="text-danger" ErrorMessage="The State is required." InitialValue ="0"  Text="*"/>
                                                            </asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlStateEdit" CssClass="form-control">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <asp:Label runat="server" AssociatedControlID="ddlCityEdit" CssClass="col-md-4 control-label">City
                                    <asp:RequiredFieldValidator ValidationGroup="Port" runat="server" ControlToValidate="ddlCityEdit" CssClass="text-danger" ErrorMessage="The City is required." InitialValue ="0"  Text="*"/>
                                                            </asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlCityEdit" CssClass="form-control">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <div class="form-group">
                                                                <label for="txtOAG_typeC" class="col-md-4 control-label">OAG Type
                                                                  <asp:RegularExpressionValidator Text="*" ControlToValidate = "txtOAG_type" ID="RegularExpressionValidator1" ValidationExpression = "^[\s\S]{0,1}$" runat="server" 
                                                                     ValidationGroup="Port"  ErrorMessage="Maximum 1 characters allowed for OAG Type."></asp:RegularExpressionValidator>
                                                                </label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox runat="server" ID="txtOAG_type" CssClass="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <div class="form-group">
                                                                <label for="txtOAG_subtype" class="col-md-4 control-label">OAG_subtype
                                                                     <asp:RegularExpressionValidator Text="*" ControlToValidate = "txtOAG_subtype" ID="RegularExpressionValidator2" ValidationExpression = "^[\s\S]{0,1}$" runat="server" 
                                                                     ValidationGroup="Port"  ErrorMessage="Maximum 1 characters allowed for OAG Sub type."></asp:RegularExpressionValidator>
                                                                </label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox runat="server" ID="txtOAG_subtype" CssClass="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtoag_name" class="col-md-4 col-form-label">oag_name</label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtoag_name" CssClass="form-control" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            <label for="txtoag_ctry" class="col-md-4 col-form-label">oag_ctry</label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtoag_ctry" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtoag_subctry" class="col-md-4 col-form-label">oag_subctry</label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtoag_subctry" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtoag_lon" class="col-md-4 control-label">oag_lon</label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtoag_lon" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row pull-right col-md-2">
                                                    <asp:Button ID="btnEditUser" CommandName="Add" runat="server" CausesValidation="true" Text="Save" CssClass="btn btn-primary btn-md" ValidationGroup="Port" />
                                                </div>
                                            </InsertItemTemplate>
                                        </asp:FormView>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
