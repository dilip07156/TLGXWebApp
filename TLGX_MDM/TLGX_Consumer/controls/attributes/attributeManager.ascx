<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="attributeManager.ascx.cs" Inherits="TLGX_Consumer.controls.attributes.attributeManager" %>
<script type="text/javascript">
    function closeCreateAttributeModal() {
        $("#moAddUpdateAttribte").modal('hide');
    }
    function showCreateAttributeModal() {
        $("#moAddUpdateAttribte").modal('show');
    }
    function pageLoad(sender, args) {
        var hv = $('#hdnFlag').val();

        if (hv == "add" || hv == "edit") {
            closeCreateAttributeModal();
        }
        $('#hdnFlag').val("false");

    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <%-- <div class="loading" style="text-align: center;">
            Loading. Please wait.<br />
            <br />
            <img src="../../images/ajax-loader.gif" alt="" />
        </div>--%>
        <div class="container">
            <div id="msgAlertStatus" runat="server" style="display: none;"></div>
            <div class="panel-group" id="accordion">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Attributes</a>
                        </h4>
                    </div>
                    <div id="collapseSearch" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <form class="form-horizontal">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtAttributeNameSearch">Attribute Name</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtAttributeNameSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlMasterFor">MasterFor</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlMasterForSearch" AutoPostBack="true" OnSelectedIndexChanged="ddlMasterForSearch_SelectedIndexChanged" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlParentSearch">Parent</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlParentSearch" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-sm-10">
                                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" />
                                                <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtOTATableCodeSearch">OTA Table Code</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtOTATableCodeSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtOTATableNameSearch">OTA Table Name</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtOTATableNameSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlStatus">Active</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true">
                                                    <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="N" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-10">
                                                <asp:Button runat="server" ID="btnNewCreate" OnClick="btnNewCreate_Click" OnClientClick="showCreateAttributeModal();" CssClass="btn btn-sm btn-primary pull-right" Text="Create New Attribute" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="form-group pull-right">
                        <div class="input-group">
                            <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                            <asp:DropDownList ID="ddlShowEntries" runat="server" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
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
                </div>
                <asp:GridView ID="grdMasterAttributeList" runat="server" AutoGenerateColumns="False" AllowPaging="true" AllowCustomPaging="true" OnPageIndexChanging="grdMasterAttributeList_PageIndexChanging" CssClass="table table-hover table-striped" DataKeyNames="MasterAttribute_Id" EmptyDataText="No Data Found" OnSelectedIndexChanged="grdMasterAttributeList_SelectedIndexChanged" OnRowCommand="grdMasterAttributeList_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="Name" DataField="Name" />
                        <asp:BoundField HeaderText="Parent" DataField="ParentAttributeName" />
                        <asp:BoundField HeaderText="MasterFor" DataField="MasterFor" />
                        <asp:BoundField HeaderText="OTA Table Code" DataField="OTA_CodeTableCode" />
                        <asp:BoundField HeaderText="OTA Table Name" DataField="OTA_CodeTableName" />
                        <asp:BoundField DataField="IsActive" HeaderText="Active" />
                        <%--<asp:CommandField ShowSelectButton="True" HeaderText="Select" />--%>
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" Text="Select"
                                    CssClass="btn btn-primary btn-sm" OnClientClick="showCreateAttributeModal();" CommandArgument='<%# Bind("MasterAttribute_Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <PagerStyle CssClass="pagination-ys" BorderStyle="None" />
                </asp:GridView>


            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="modal fade" id="moAddUpdateAttribte" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <strong>Add / Update Attribute </strong>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpdAddAttributeModal" runat="server">
                    <ContentTemplate>
                        <div class="row col-lg-12">
                            <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="Attribute" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                            <div id="msgAlert" runat="server" style="display: none;"></div>
                            <asp:HiddenField ID="hdnFlag" runat="server" ClientIDMode="Static" Value="" EnableViewState="false" />
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <asp:FormView ID="frmAttributedetail" OnItemCommand="frmAttributedetail_ItemCommand" runat="server" CssClass="row col-lg-12" DefaultMode="Insert">
                                    <EditItemTemplate>
                                        <div class="col-lg-5">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Attribute Detail</div>
                                                <div class="panel-body">
                                                    <div id="msgAlertAttributeDetail" runat="server" style="display: none;"></div>
                                                    <div class="form-group">
                                                        <label for="chkActiveMaster">Active</label>
                                                        <asp:CheckBox ID="chkActiveMaster" runat="server" Checked="true" /><br />

                                                        <label for="txtNewAttribute">
                                                            Attribute Name  
                                                                <asp:RequiredFieldValidator ID="rfvNewAttribute" runat="server" ControlToValidate="txtNewAttribute" CssClass="text-warning" ErrorMessage="Please enter Attribute name." Text="*" ValidationGroup="Attribute"></asp:RequiredFieldValidator></label>
                                                        <asp:TextBox ID="txtNewAttribute" runat="server" CssClass="form-control"></asp:TextBox>

                                                        <label for="ddlMasterFor">
                                                            Attribute For
                                                                      <asp:RequiredFieldValidator ID="rqdNewAttributeInsert" runat="server" ControlToValidate="ddlMasterFor" CssClass="text-warning" ErrorMessage="Please select Attribute for." Text="*" ValidationGroup="Attribute"></asp:RequiredFieldValidator>
                                                        </label>
                                                        <asp:DropDownList ID="ddlMasterFor" AutoPostBack="true" OnSelectedIndexChanged="ddlMasterFor_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>

                                                        <label for="ddlParent">Parent</label>
                                                        <asp:DropDownList ID="ddlParent" CssClass="form-control" runat="server" AppendDataBoundItems="true">
                                                        </asp:DropDownList>


                                                        <br />
                                                        <label for="txtOTATableCode">OTA Table Code</label>
                                                        <asp:TextBox ID="txtOTATableCode" runat="server" CssClass="form-control"></asp:TextBox>

                                                        <label for="txtOTATableName">OTA Table Name</label>
                                                        <asp:TextBox ID="txtOTATableName" runat="server" CssClass="form-control"></asp:TextBox>

                                                        <asp:LinkButton ID="lnkAddNewAttribute" runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm" ValidationGroup="Attribute" OnClick="lnkAddNewAttribute_Click">Update Base Attribute</asp:LinkButton>


                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-7">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Attribute Values</div>
                                                <div class="panel-body">
                                                    <asp:GridView ID="grdAttributeValues" runat="server" AllowCustomPaging="true" AllowPaging="true" AutoGenerateColumns="False" EmptyDataText="No Attribute Values" CssClass="table table-hover table-striped" DataKeyNames="MasterAttributeValue_Id" OnRowCommand="grdAttributeValues_RowCommand" OnSelectedIndexChanged="grdAttributeValues_SelectedIndexChanged" OnPageIndexChanging="grdAttributeValues_PageIndexChanging">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="AttributeValue" DataField="AttributeValue" />
                                                            <asp:BoundField HeaderText="Parent Attr Value" DataField="ParentAttributeValue" />
                                                            <asp:BoundField DataField="OTA_CodeTableValue" HeaderText="OTA Code Value" />
                                                            <asp:BoundField DataField="IsActive" HeaderText="Active" />
                                                            <asp:BoundField DataField="MasterAttribute_Id" HeaderText="MasterAttribute_Id" Visible="False" />
                                                            <asp:BoundField DataField="MasterAttributeValue_Id" HeaderText="MasterAttributeValue_Id" Visible="False" />

                                                            <asp:TemplateField ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" Text="Select"
                                                                        CssClass="btn btn-primary btn-sm" CommandArgument='<%# Bind("MasterAttributeValue_Id") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <%--<asp:CommandField HeaderText="Select" ShowSelectButton="True"  />--%>
                                                        </Columns>
                                                        <PagerStyle CssClass="pagination-ys" BorderStyle="None" />
                                                    </asp:GridView>
                                                    <br />
                                                    <div id="msgAlertAttributeValue" runat="server" style="display: none;"></div>
                                                    <div class="form-group" id="divAttrValData" runat="server">

                                                        <label for="chkActiveValue">Active</label>
                                                        <asp:CheckBox ID="chkActiveValue" runat="server" Checked="true" /><br />

                                                        <label for="ddlParentAttrValue">Parent Attr Value</label>
                                                        <asp:DropDownList ID="ddlParentAttrValue" CssClass="form-control" runat="server" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue ="0" runat="server" ControlToValidate="ddlParentAttrValue" CssClass="text-warning" ErrorMessage="*" ValidationGroup="vldAlias"></asp:RequiredFieldValidator>--%>

                                                        <label for="txtValueName">Attribute Value</label>
                                                        <asp:TextBox ID="txtValueName" runat="server" CssClass="form-control" ValidationGroup="vldAlias"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValueName" CssClass="text-warning" ErrorMessage="*" ValidationGroup="vldAlias"></asp:RequiredFieldValidator>

                                                        <label for="txtOTATableValue">OTA Table Value</label>
                                                        <asp:TextBox ID="txtOTATableValue" runat="server" CssClass="form-control" ValidationGroup="vldAlias"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOTATableValue" CssClass="text-warning" ErrorMessage="*" ValidationGroup="vldAlias"></asp:RequiredFieldValidator>

                                                        <br />
                                                        <asp:LinkButton ID="btnCreateAlias" runat="server" CommandName="AddAttributeValue" CssClass="btn btn-primary btn-sm" ValidationGroup="vldAliasRoom" OnClick="btnCreateAlias_Click">Add Value</asp:LinkButton>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <div class="row col-lg-12">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Attribute Detail</div>
                                                <div class="panel-body">
                                                    <div class="row col-lg-12">
                                                        <asp:HiddenField ID="hdnFlagAttributeDetail" ClientIDMode="Static" runat="server" Value="" EnableViewState="false" />
                                                        <div id="msgAlertAttributeDetail" runat="server" style="display: none;"></div>
                                                        <div class="form-group">
                                                            <label for="chkActiveMaster">Active</label>
                                                            <asp:CheckBox ID="chkActiveMaster" runat="server" Checked="true" /><br />

                                                            <label for="txtNewAttribute">
                                                                Attribute Name  
                                                                <asp:RequiredFieldValidator ID="rfvNewAttribute" runat="server" ControlToValidate="txtNewAttribute" CssClass="text-warning" ErrorMessage="Please enter Attribute name." Text="*" ValidationGroup="Attribute"></asp:RequiredFieldValidator></label>
                                                            <asp:TextBox ID="txtNewAttribute" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label for="ddlMasterFor">
                                                                Attribute For
                                                                      <asp:RequiredFieldValidator ID="rqdNewAttributeInsert" runat="server" ControlToValidate="ddlMasterFor" CssClass="text-warning" ErrorMessage="Please select Attribute for." Text="*" ValidationGroup="Attribute"></asp:RequiredFieldValidator>
                                                            </label>
                                                            <asp:DropDownList ID="ddlMasterFor" AutoPostBack="true" OnSelectedIndexChanged="ddlMasterFor_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                            </asp:DropDownList>

                                                            <label for="ddlParent">Parent</label>
                                                            <asp:DropDownList ID="ddlParent" CssClass="form-control" runat="server">
                                                            </asp:DropDownList>


                                                            <br />
                                                            <label for="txtOTATableCode">OTA Table Code</label>
                                                            <asp:TextBox ID="txtOTATableCode" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <label for="txtOTATableName">OTA Table Name</label>
                                                            <asp:TextBox ID="txtOTATableName" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <asp:LinkButton ID="lnkAddNewAttribute" runat="server" CommandName="Add" CssClass="btn btn-primary btn-sm" ValidationGroup="Attribute" OnClick="lnkAddNewAttribute_Click">Add New Base Attribute</asp:LinkButton>


                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </InsertItemTemplate>
                                </asp:FormView>
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


