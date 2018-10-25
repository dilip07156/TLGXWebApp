<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="manageStaticDataConfig.ascx.cs" Inherits="TLGX_Consumer.controls.staticdataconfig.manageStaticDataConfig1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

    /*.alert {
        display: none;
    }*/

    .hide {
        display: none;
    }

    .inputTypeForFilter {
        width: 80% !important;
    }
</style>
<script type="text/javascript">
    //var prm = Sys.WebForms.PageRequestManager.getInstance();
    //prm.add_beginRequest(BeginRequestHandler);
    //prm.add_endRequest(EndRequestHandler);

    function showManageModal() {
        $("#moActivityManage").modal('show');
    }
    function closeManageModal() {
        $("#moActivityManage").modal('hide');
    }
    function pageLoad(sender, args) {
        $("#btnAddValue").click(function () {
            var Contain = "";
            $("#MainContent_manageStaticDataConfig_frmAddConfig_dvValueForFilter input[type=text]").each(function () {
                Contain += $(this).val() + ",";
            });
            $('#hdnValueWithCommaSeprated').val(Contain);
            $("#MainContent_manageStaticDataConfig_frmAddConfig_dvValueForFilter").append('<div class="con"><input id="MainContent_manageStaticDataConfig_frmAddConfig_txtValueForFilter" type="text" class="form-control col-md-8 inputTypeForFilter" /><div class="input-group-btn  col-md-4" style="padding-left: 0px !important;"><button class="btn btn-default btnRemove" id="btnAddValue" type="button"><i class="glyphicon glyphicon-minus"></i></button></div></div>');
        });
        $('body').on('click', '.btnRemove', function () {
            //debugger;
            $(this).parent('div').parent('div.con').remove()

        });



        var hv = $('#hdnFlag').val();
        if (hv == "true") {
            closeManageModal();
            $('#hdnFlag').val("false");
        }

    }

    function ddlForChanged(ddl) {
        var FOR = ddl.options[ddl.selectedIndex].text;
        var myVal = document.getElementById("<%=vddlSupplierName.ClientID%>");
        if (FOR == 'MAPPING') {
            ValidatorEnable(myVal, true);
        }
        else {
            ValidatorEnable(myVal, false);
        }
    }

    function computeValue() {
        var Contain = "";
        $("#MainContent_manageStaticDataConfig_frmAddConfig_dvValueForFilter input[type=text]").each(function () {
            Contain += $(this).val() + ",";
        });
        $('#hdnValueWithCommaSeprated').val(Contain);
    }



</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" OnUnload="UpdatePanel1_Unload">
    <ContentTemplate>
        <div class="panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Mapping Settings</a>
                    </h4>
                </div>

                <div id="collapseSearch" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div class="container">
                            <div class="row">

                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlSupplierName">
                                            For 
                                            <asp:RequiredFieldValidator ID="vddlFor" runat="server" ErrorMessage="*" ControlToValidate="ddlFor" InitialValue="0"
                                                CssClass="text-danger" ValidationGroup="UpdateConfig"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlFor" runat="server" CssClass="form-control" AppendDataBoundItems="true" onchange="ddlForChanged(this);">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlSupplierName">
                                            Supplier 
                                            <asp:RequiredFieldValidator ID="vddlSupplierName" runat="server" ErrorMessage="*" ControlToValidate="ddlSupplierName" InitialValue="0"
                                                CssClass="text-danger" ValidationGroup="UpdateConfig"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlMasterCountry">
                                            Entity
                                            <asp:RequiredFieldValidator ID="vddlEntity" runat="server" ErrorMessage="*" ControlToValidate="ddlEntity" InitialValue="0"
                                                CssClass="text-danger" ValidationGroup="UpdateConfig"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlEntity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlStatus">
                                            Mapping Status
                                            <asp:RequiredFieldValidator ID="vddlStatus" runat="server" ErrorMessage="*" ControlToValidate="ddlStatus" InitialValue="0"
                                                CssClass="text-danger" ValidationGroup="UpdateConfig"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="btnUpdate_Click" CausesValidation="true" ValidationGroup="UpdateConfig" />
                                            <%--<asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" OnClick="btnReset_Click" />--%>
                                        </div>
                                        <div class="col-sm-12">&nbsp; </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div id="dvMsg" runat="server" style="display: none;"></div>
        </div>
        <div class="panel-group" id="accordionResult">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="panel-title row">
                        <div class="row">
                            <div class="panel-group" id="accordionSearchResult">
                                <div class="col-md-3">
                                    <a data-toggle="collapse" data-parent="#accordionResult" href="#collapseSearchResult">
                                        <h4 class="panel-title" style="padding-left: 10px;">Search Results (Total Count:
                                            <asp:Label ID="lblTotalUploadConfig" runat="server" Text="0"></asp:Label>)</h4>
                                    </a>

                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <div class="input-group" runat="server" id="div1">
                                                <label class="input-group-addon" for="ddlShowEntries">Attribute Type</label>
                                                <asp:DropDownList ID="ddlFilterAttributeType" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlFilterAttributeType_SelectedIndexChanged">
                                                    <asp:ListItem Value="-1">All</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <div class="input-group" runat="server" id="div2">
                                                <label class="input-group-addon" for="ddlShowEntries">Priority</label>
                                                <asp:DropDownList ID="ddlFilterPriority" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlFilterPriority_SelectedIndexChanged">
                                                    <asp:ListItem Value="-1">All</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <div class="input-group" runat="server" id="divDropdownForEntries">
                                                <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                                                <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
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
                                </div>
                                <div class="col-lg-1">
                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm" Text="Add" OnClick="btnAdd_Click" OnClientClick="showManageModal();" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="collapseSearchResult" class="panel-collapse collapse in">
                    <div class="panel-body">

                        <asp:GridView ID="grdMappingAttrValues" runat="server" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="No Attribute Values Found"
                            CssClass="table table-hover table-striped" OnDataBound="grdMappingAttrValues_DataBound" OnRowCommand="grdMappingAttrValues_RowCommand"
                            AllowCustomPaging="true" OnPageIndexChanging="grdMappingAttrValues_PageIndexChanging" DataKeyNames="SupplierImportAttributeValue_Id,SupplierImportAttribute_Id"
                            OnRowDataBound="grdMappingAttrValues_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="AttributeType" HeaderText="Attribute Type" />
                                <asp:BoundField DataField="AttributeName" HeaderText="Attribute Name" />
                                <asp:BoundField DataField="AttributeValue" HeaderText="Attribute Value" />
                                <asp:BoundField DataField="STATUS" HeaderText="Status" />
                                <asp:BoundField DataField="Priority" HeaderText="Priority" />
                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                <asp:BoundField DataField="AttributeValueType" HeaderText="AttributeValueType" />
                                <asp:BoundField DataField="Comparison" HeaderText="Comparison" />
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                            Enabled="true" CommandArgument='<%# Bind("SupplierImportAttributeValue_Id") %>' OnClientClick="showManageModal();">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Select
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("STATUS").ToString() == "ACTIVE" ? "SoftDelete" : "UnDelete"   %>' CssClass="btn btn-default" CommandArgument='<%# Bind("SupplierImportAttributeValue_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("STATUS").ToString() == "ACTIVE" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("STATUS").ToString() == "ACTIVE" ? "Delete" : "UnDelete"   %>
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
        <hr />
    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="moActivityManage" role="dialog">
    <div class="modal-dialog modal-md">
        <div class="modal-content">

            <div class="modal-header ">
                <div class="panel-title">
                    <h4 class="modal-title">Add/Update Mapping </h4>
                </div>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpnlAddConfig" runat="server">
                    <ContentTemplate>

                        <div class="row col-lg-12">
                            <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="AddConfig" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                            <div id="dvModalMsg" runat="server" style="display: none;"></div>
                            <asp:HiddenField ID="hdnFlag" runat="server" ClientIDMode="Static" Value="" EnableViewState="false" />
                            <asp:Label runat="server" ID="lblSelectedSupplierImportAttributeValue_Id" Visible="false" />
                            <asp:Label runat="server" ID="lblconfigresultCount" Visible="false" Text="0" />

                        </div>

                        <asp:FormView ID="frmAddConfig" runat="server" DefaultMode="Insert" DataKeyNames="SupplierImportAttributeValue_Id,SupplierImportAttribute_Id"
                            OnItemCommand="frmAddConfig_ItemCommand">
                            <InsertItemTemplate>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="ddlSupplierName">
                                                        Type 
                                                        <asp:RequiredFieldValidator ID="vddlSupplierName" ValidationGroup="AddConfig" runat="server" ControlToValidate="ddlAttributeType"
                                                            CssClass="text-warning" InitialValue="0" ErrorMessage="The Type field is required." Text="*" />
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlAttributeType" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAttributeType_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row" id="dvddlAttributeName" runat="server">
                                                    <label class="control-label col-sm-4" for="ddlAttributeName">
                                                        Name
                                                        <asp:RequiredFieldValidator ID="vddlAttributeName" ValidationGroup="AddConfig" runat="server" ControlToValidate="ddlAttributeName"
                                                            CssClass="text-danger" InitialValue="-1" ErrorMessage="The Name field is required." Text="*" />
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <%--For Dropdown Values--%>
                                                        <asp:DropDownList ID="ddlAttributeName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAttributeName_SelectedIndexChanged" CssClass="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Text="---ALL---" Value="-1"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnddlAttributeTableName" runat="server" />

                                                        <asp:TextBox ID="txtAttributeName" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="axfte_txtAttributeName" runat="server" Enabled="false" FilterType="Numbers" TargetControlID="txtAttributeName" />

                                                    </div>
                                                </div>

                                                <div class="form-group row" id="dvMatchByColumnOrValue" runat="server" visible="false">
                                                    <div class="col-sm-8">
                                                        <label class="radio-inline">
                                                            <asp:RadioButton ID="rdoIsMatchByColumn" runat="server" Checked="true" Text="Column" GroupName="MatchBy" OnCheckedChanged="rdoIsMatchByColumn_CheckedChanged" AutoPostBack="true" />
                                                        </label>
                                                        <label class="radio-inline">
                                                            <asp:RadioButton ID="rdoIsMatchByValue" runat="server" Text="Value" GroupName="MatchBy" OnCheckedChanged="rdoIsMatchByValue_CheckedChanged" AutoPostBack="true" />
                                                        </label>
                                                    </div>
                                                </div>

                                                <div class="form-group row" id="dvAttributeValue" runat="server">
                                                    <label class="control-label col-sm-4" for="ddlAttributeValue">
                                                        Value
                                                        <asp:RequiredFieldValidator ID="rqfvddlAttributeValuedll" ValidationGroup="AddConfig" runat="server" ControlToValidate="ddlAttributeValue"
                                                            CssClass="text-danger" InitialValue="0" ErrorMessage="The Value field is required1." Text="*" />
                                                        <asp:RequiredFieldValidator ID="rqfvddlAttributeValue" ValidationGroup="AddConfig" runat="server" ControlToValidate="txtAttributeValue"
                                                            CssClass="text-danger" ErrorMessage="The Value field is required2." Text="*" Enabled="false" />
                                                        <asp:RequiredFieldValidator ID="rqfvddlAttributeValueFrom" ValidationGroup="AddConfig" runat="server" ControlToValidate="txtReplaceFrom"
                                                            CssClass="text-danger" ErrorMessage="The Value field is required3." Text="*" Enabled="false" />
                                                        <asp:RequiredFieldValidator ID="rqfvddlAttributeValueTo" ValidationGroup="AddConfig" runat="server" ControlToValidate="txtReplaceTo"
                                                            CssClass="text-danger" ErrorMessage="The Value field is required4." Text="*" Enabled="false" />
                                                        <asp:RequiredFieldValidator ID="rqfvddlAttributeValueFilter" ValidationGroup="AddConfig" runat="server" ControlToValidate="txtValueForFilter"
                                                            CssClass="text-danger" ErrorMessage="The Value field is required5." Text="*" Enabled="false" />
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <%--For Dropdown Values--%>
                                                        <asp:DropDownList ID="ddlAttributeValue" runat="server" AutoPostBack="true" CssClass="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnddlAttributeTableValueName" runat="server" />
                                                        <%--For Comparison operator--%>
                                                        <asp:DropDownList ID="ddlComparisonValue" runat="server" CssClass="form-control " AppendDataBoundItems="true" Visible="false">
                                                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;
                                                        <%--For TextBox Values--%>
                                                        <asp:TextBox ID="txtAttributeValue" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="axfte_txtAttributeValue" runat="server" Enabled="false" FilterType="Numbers" TargetControlID="txtAttributeValue" />

                                                        <%--For For Multi TextBox Values--%>
                                                        <asp:HiddenField runat="server" ID="hdnValueWithCommaSeprated" ClientIDMode="Static" />
                                                        <div id="dvValueForFilter" runat="server" class="input-group col-md-12" style="display: none;">
                                                            <input id="txtValueForFilter" runat="server" type="text" class="form-control col-md-8 inputTypeForFilter" />
                                                            <div class="input-group-btn  col-md-4" style="padding-left: 0px !important;">
                                                                <button class="btn btn-default" id="btnAddValue" type="button">
                                                                    <i class="glyphicon glyphicon-plus"></i>
                                                                </button>
                                                            </div>
                                                        </div>

                                                        <%--For For Multi TextBox Values--%>
                                                        <asp:HiddenField runat="server" ID="hdnIsReplaceWith" ClientIDMode="Static" />
                                                        <div runat="server" id="divReplaceValue" style="display: none;">
                                                            <div class="form-group col-md-12">
                                                                <div class="col-md-6">
                                                                    <label>From</label><br />
                                                                    <asp:TextBox ID="txtReplaceFrom" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label>To</label><br />
                                                                    <asp:TextBox ID="txtReplaceTo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group row" id="dvtxtPriority" runat="server" style="display: none;">
                                                    <label class="control-label col-sm-4" for="txtPriority">
                                                        Priority
                                                    <asp:RequiredFieldValidator ID="vtxtPriority" runat="server" ErrorMessage="*" ControlToValidate="txtPriority" CssClass="text-danger" ValidationGroup="AddConfigValues"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtPriority" runat="server" class="form-control" MaxLength="3" />
                                                        <cc1:FilteredTextBoxExtender ID="axfte_txtPriority" runat="server" FilterType="Numbers" TargetControlID="txtPriority" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="txtDescription">Description</label>
                                                    <div class="col-sm-8">
                                                        <textarea maxlength="510" id="txtDescription" runat="server" class="form-control"></textarea>
                                                        <%--<asp:TextBox ID="txtDescription" runat="server" class="form-control" MaxLength="255" />--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-sm-6">
                                                    <div class="form-group row">
                                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm" OnClientClick="computeValue();" Text="Add" CommandName="Add" CausesValidation="true" ValidationGroup="AddConfig" />
                                                        <asp:Button ID="btnAddReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CommandName="ResetAdd" CausesValidation="false" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </InsertItemTemplate>
                            <EditItemTemplate>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="ddlSupplierName">Type </label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlAttributeType" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAttributeType_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row" id="dvAttributeName" runat="server">
                                                    <label class="control-label col-sm-4" for="txtAttributeName">Name</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtAttributeName" runat="server" CssClass="form-control"></asp:TextBox>

                                                        <asp:DropDownList ID="ddlAttributeName" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAttributeName_SelectedIndexChanged">
                                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnddlAttributeTableName" runat="server" />
                                                    </div>
                                                </div>

                                                <div class="form-group row" id="dvMatchByColumnOrValue" runat="server" visible="false">
                                                    <div class="col-sm-8">
                                                        <label class="radio-inline">
                                                            <asp:RadioButton ID="rdoIsMatchByColumn" runat="server" Checked="true" Text="Column" GroupName="MatchBy" OnCheckedChanged="rdoIsMatchByColumn_CheckedChanged" name="IsMatchByColumnOrValue" AutoPostBack="true" />
                                                        </label>
                                                        <label class="radio-inline">
                                                            <asp:RadioButton ID="rdoIsMatchByValue" runat="server" Text="Value" GroupName="MatchBy" OnCheckedChanged="rdoIsMatchByValue_CheckedChanged" name="IsMatchByColumnOrValue" AutoPostBack="true" />
                                                        </label>
                                                    </div>
                                                </div>

                                                <div class="form-group row" id="dvAttributeValue" runat="server">
                                                    <label class="control-label col-sm-4" for="txtAttributeName">
                                                        Value
                                                        <%--<asp:RequiredFieldValidator ID="rqfvddlAttributeValuedll" ValidationGroup="UpdateConfigValues" runat="server" ControlToValidate="ddlAttributeValue"
                                                            CssClass="text-danger" InitialValue="0" ErrorMessage="The Value field is required." Text="*" />
                                                        <asp:RequiredFieldValidator ID="rqfvddlAttributeValue" ValidationGroup="UpdateConfigValues" runat="server" ControlToValidate="txtAttributeValue"
                                                            CssClass="text-danger" ErrorMessage="The Value field is required." Text="*"  Enabled="false"/>
                                                        <asp:RequiredFieldValidator ID="rqfvddlAttributeValueFrom" ValidationGroup="UpdateConfigValues" runat="server" ControlToValidate="txtReplaceFrom"
                                                            CssClass="text-danger" ErrorMessage="The Value field is required." Text="*" Enabled="false" />
                                                        <asp:RequiredFieldValidator ID="rqfvddlAttributeValueTo" ValidationGroup="UpdateConfigValues" runat="server" ControlToValidate="txtReplaceTo"
                                                            CssClass="text-danger" ErrorMessage="The Value field is required." Text="*" Enabled="false" />
                                                        <asp:RequiredFieldValidator ID="rqfvddlAttributeValueFilter" ValidationGroup="UpdateConfigValues" runat="server" ControlToValidate="txtValueForFilter"
                                                            CssClass="text-danger" ErrorMessage="The Value field is required." Text="*" Enabled="false" />--%>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <%--For Comparison Values--%>
                                                        <asp:DropDownList ID="ddlComparisonValue" runat="server" CssClass="form-control " AppendDataBoundItems="true" Visible="false">
                                                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;
                                                             <%--For TextBox Values--%>
                                                        <asp:TextBox ID="txtAttributeValue" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="axfte_txtAttributeName" runat="server" Enabled="false" FilterType="Numbers" TargetControlID="txtAttributeValue" />
                                                        <%--For Dropdown Values--%>
                                                        <asp:DropDownList ID="ddlAttributeValue" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:HiddenField ID="hdnddlAttributeTableValueName" runat="server" />
                                                        <asp:HiddenField runat="server" ID="hdnValueWithCommaSeprated" ClientIDMode="Static" />
                                                        <div id="dvValueForFilter" runat="server" class="input-group col-md-12" style="display: none;">
                                                            <input id="txtValueForFilter" type="text" class="form-control col-md-8 inputTypeForFilter" />
                                                            <div class="input-group-btn  col-md-4" style="padding-left: 0px !important;">
                                                                <button class="btn btn-default" id="btnAddValue" type="button">
                                                                    <i class="glyphicon glyphicon-plus"></i>
                                                                </button>
                                                            </div>
                                                        </div>

                                                        <asp:HiddenField runat="server" ID="hdnIsReplaceWith" ClientIDMode="Static" />
                                                        <div runat="server" id="divReplaceValue" style="display: none;">
                                                            <div class="form-group col-md-12">
                                                                <div class="col-md-6">
                                                                    <label>From</label><br />
                                                                    <asp:TextBox ID="txtReplaceFrom" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label>To</label><br />
                                                                    <asp:TextBox ID="txtReplaceTo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="ddlAddStatus">Status</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlAddStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group row" id="dvtxtPriority" runat="server">
                                                    <label class="control-label col-sm-4" for="txtPriority">
                                                        Priority
                                                    <asp:RequiredFieldValidator ID="vtxtPriority" runat="server" ErrorMessage="*" ControlToValidate="txtPriority" CssClass="text-danger" ValidationGroup="UpdateConfigValues"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtPriority" runat="server" class="form-control" MaxLength="3" />
                                                        <cc1:FilteredTextBoxExtender ID="axfte_txtPriority" runat="server" FilterType="Numbers" TargetControlID="txtPriority" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="txtDescription">Description</label>
                                                    <div class="col-sm-8">
                                                        <textarea maxlength="510" id="txtDescription" runat="server" class="form-control"></textarea>
                                                        <%--<asp:TextBox ID="txtDescription" runat="server" class="form-control" MaxLength="255" />--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="col-sm-6">
                                                <div class="form-group row">
                                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" CommandName="Save" OnClientClick="computeValue(); return true;" ValidationGroup="UpdateConfigValues" CausesValidation="true" />
                                                    <asp:Button ID="btnAddReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CommandName="ResetUpdate" CausesValidation="false" />
                                                </div>
                                            </div>
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
