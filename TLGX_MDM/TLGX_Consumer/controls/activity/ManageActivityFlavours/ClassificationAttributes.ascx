<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassificationAttributes.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.ClassificationAttributes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script lang="javascript" type="text/javascript">
    function showClassificationAttributesModal() {
        //alert("Hi");
        $("#moClassificationAttributes").modal('show');
    }
    function closeClassificationAttributesModal() {
        //alert("Hi");
        $("#moClassificationAttributes").modal('hide');
    }
    function pageLoad(sender, args) {
        var hv = $('#MainContent_ClassificationAttributes_hdnFlag').val();
        if (hv == "true") {
            closeClassificationAttributesModal();
            $('#MainContent_ClassificationAttributes_hdnFlag').val("false");
        }
    }

</script>
<script src="../../../Scripts/bootbox.min.js"></script>

<asp:UpdatePanel ID="upClassificationAttributes" runat="server">
    <ContentTemplate>

        <div id="dvMsg" runat="server" style="display: none;"></div>
        <div class="container">
            <asp:Button ID="btnAddNewAttribute" runat="server" Text="Add New" CssClass="btn btn-primary btn-sm" OnClientClick="showClassificationAttributesModal();" OnClick="btnAddNewAttribute_Click" />
        </div>

        <br></br>

        <div class="container">

            <asp:Repeater ID="repCAType" runat="server">
                <HeaderTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>Attribute Types</strong>
                        </div>
                        <div class="panel-body">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="row">
                        <div class="col-sm-2">
                            <strong>
                                <asp:Label ID="lblType" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "AttributeType") %>'></asp:Label>
                            </strong>
                        </div>
                        <div class="col-sm-10">
                            <asp:Repeater ID="repCASubType" runat="server" DataSource='<%# DataBinder.Eval(Container.DataItem, "SubType") %>'>
                                <HeaderTemplate>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <strong>Attribute SubTypes</strong>
                                        </div>
                                        <div class="panel-body">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <strong>
                                                <asp:Label ID="lblSubType" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "SubAttributeType") %>'></asp:Label>
                                            </strong>
                                        </div>
                                        <div class="col-sm-10">
                                            <asp:GridView ID="grdClassificationAttributes" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped" DataKeyNames="Activity_ClassificationAttribute_Id" DataSource='<%# DataBinder.Eval(Container.DataItem, "ListCA") %>' EmptyDataText="There are no classifications set for this product" OnRowCommand="grdClassificationAttributes_RowCommand" OnRowDataBound="grdClassificationAttributes_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="AttributeType" HeaderText="AttributeType" />
                                                    <asp:BoundField DataField="AttributeSubType" HeaderText="AttributeSubType" />
                                                    <asp:BoundField DataField="AttributeValue" HeaderText="Value" />
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandArgument='<%# Bind("Activity_ClassificationAttribute_Id") %>' CommandName="Select" CssClass="btn btn-default"
                                                                Enabled='<%# Eval("IsActive") %>' OnClientClick="showClassificationAttributesModal();">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp; Edit
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandArgument='<%# Bind("Activity_ClassificationAttribute_Id") %>' CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>' CssClass="btn btn-default">
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </div>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                    </div>
                </FooterTemplate>
            </asp:Repeater>

        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="moClassificationAttributes" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">Attribute Details</h4>
                <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
            </div>
            <div class="modal-body centered">

                <asp:UpdatePanel ID="upClassificationAttributesAddEdit" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
                        <asp:FormView ID="frmClassificationAttribute" runat="server" DataKeyNames="Activity_ClassificationAttribute_Id" DefaultMode="Insert" OnItemCommand="frmClassificationAttribute_ItemCommand">

                            <InsertItemTemplate>

                                <div class="panel panel-default">
                                    <div class="panel-heading">Add New Attribute</div>
                                    <div class="panel-body">


                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <asp:ValidationSummary ID="vlsSummAdd" runat="server" ValidationGroup="vldGrpCAAdd" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                            </div>
                                        </div>

                                        <div class="col-lg-12">

                                            <div class="col-lg-6">

                                                <div class="form-group">
                                                    <label class="control-label-mand" for="ddlAttributeType">Attribute Category</label>

                                                    <asp:DropDownList ID="ddlAttributeType" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlAttributeType_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="vldReqddlAttributeTypeAdd" runat="server" ControlToValidate="ddlAttributeType"
                                                        InitialValue="0" ValidationGroup="vldGrpCAAdd" ErrorMessage="Please select Attribute Type."
                                                        Text="*" CssClass="text-danger"></asp:RequiredFieldValidator>

                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label-mand" for="ddlAttributeSubType">Attribute Sub Category</label>

                                                    <asp:DropDownList ID="ddlAttributeSubType" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="vldReqddlAttributeSubTypeAdd" runat="server" ControlToValidate="ddlAttributeSubType"
                                                        InitialValue="0" ValidationGroup="vldGrpCAAdd" ErrorMessage="Please select Attribute Sub Type."
                                                        Text="*" CssClass="text-danger"></asp:RequiredFieldValidator>

                                                </div>

                                                <div class="form-group">
                                                    <label for="chkInternalOnly">Internal Only</label>

                                                    <asp:CheckBox ID="chkInternalOnly" runat="server" />

                                                </div>

                                            </div>

                                            <div class="col-lg-6">

                                                <div class="form-group">
                                                    <label class="control-label-mand" for="txtAttributeValue">Description / Value</label>

                                                    <asp:TextBox ID="txtAttributeValue" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ID="vldReqtxtAttributeValueAdd" runat="server" ControlToValidate="txtAttributeValue"
                                                        ValidationGroup="vldGrpCAAdd" ErrorMessage="Please enter Attribute Value." Text="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAttributeValue" Text="*" ID="rfvtxtAttributeValueAdd" ValidationExpression="^[\s\S]{0,255}$" runat="server" CssClass="text-danger" ValidationGroup="vldGrpCAAdd" ErrorMessage="Maximum 255 Characters allowed in Description / Value."></asp:RegularExpressionValidator>
                                                </div>

                                                <div class="form-group">
                                                    <asp:Button ID="btnAddClassificationAttribute" runat="server" Text="Add" CommandName="Add" CssClass="btn btn-primary btn-sm" CausesValidation="true" ValidationGroup="vldGrpCAAdd" />
                                                </div>

                                            </div>

                                        </div>

                                    </div>

                                </div>


                            </InsertItemTemplate>

                            <EditItemTemplate>

                                <div class="panel panel-default">
                                    <div class="panel-heading">Add New Attribute</div>
                                    <div class="panel-body">

                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <asp:ValidationSummary ID="vlsSummModify" runat="server" ValidationGroup="vldGrpCAModify" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                            </div>
                                        </div>

                                        <div class="col-lg-12">

                                            <div class="col-lg-6">

                                                <div class="form-group">
                                                    <label class="control-label-mand" for="ddlAttributeType">Attribute Category</label>
                                                    <asp:DropDownList ID="ddlAttributeType" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlAttributeType_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="vldReqddlAttributeTypeModify" runat="server" ControlToValidate="ddlAttributeType"
                                                        InitialValue="0" ValidationGroup="vldGrpCAModify" ErrorMessage="Please select Attribute Type."
                                                        CssClass="text-danger" Text="*"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label-mand" for="ddlAttributeSubType">Attribute Sub Category</label>
                                                    <asp:DropDownList ID="ddlAttributeSubType" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="vldReqddlAttributeSubTypeModify" runat="server" ControlToValidate="ddlAttributeSubType"
                                                        InitialValue="0" ValidationGroup="vldGrpCAModify" ErrorMessage="Please select Attribute Sub Type."
                                                        CssClass="text-danger" Text="*"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group">
                                                    <label for="chkInternalOnly">Internal Only</label>
                                                    <asp:CheckBox ID="chkInternalOnly" runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-lg-6">

                                                <div class="form-group">
                                                    <label class="control-label-mand" for="txtAttributeValue">Description / Value</label>
                                                    <asp:TextBox ID="txtAttributeValue" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control" Text='<%# Bind("AttributeValue") %>' />
                                                    <asp:RequiredFieldValidator ID="vldReqtxtAttributeValueModify" runat="server" ControlToValidate="txtAttributeValue"
                                                        ValidationGroup="vldGrpCAModify" ErrorMessage="Please enter Attribute Value." CssClass="text-danger" Text="*"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAttributeValue" Text="*" ID="rfvtxtAttributeValueAdd" ValidationExpression="^[\s\S]{0,255}$" runat="server" CssClass="text-danger" ValidationGroup="vldGrpCAModify" ErrorMessage="Maximum 255 Characters allowed in Description / Value."></asp:RegularExpressionValidator>

                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnAddClassificationAttribute" runat="server" Text="Modify" CommandName="Modify" CssClass="btn btn-primary btn-sm" CausesValidation="true" ValidationGroup="vldGrpCAModify" />
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

