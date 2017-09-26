<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeneralInfo.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityMaster.GeneralInfo" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        <div class="form-group">
            <div id="dvMsgDynamicAttributesForHotel" runat="server" style="display: none;"></div>
        </div>

        <asp:FormView ID="frmDynamicAttributeDetail" runat="server" DataKeyNames="DynamicAttribute_Id" DefaultMode="Insert" > <%--OnItemCommand="frmDynamicAttributeDetail_ItemCommand"--%>

            <HeaderTemplate>
                <div class="form-inline">
                    <div class="col-sm-12">
                        <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldAttributes" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                    </div>
                </div>
            </HeaderTemplate>

            <InsertItemTemplate>

                <div class="form-inline">
                    <label class="control-label-mand" for="txtAttributeName">Name</label>
                    <asp:RequiredFieldValidator ID="vldName" runat="server" ControlToValidate="txtAttributeName" ErrorMessage="Please enter attribute name" Text="*"
                        ValidationGroup="vldAttributes" CssClass="text-danger"></asp:RequiredFieldValidator>

                    <asp:TextBox ID="txtAttributeName" runat="server" CssClass="form-control" ValidationGroup="vldAttributes" />


                    <label class="control-label-mand" for="txtAttributeDescription">Description</label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAttributeDescription" ErrorMessage="Please enter attribute description" Text="*"
                        ValidationGroup="vldAttributes" CssClass="text-danger"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtAttributeDescription" runat="server" CssClass="form-control" ValidationGroup="vldAttributes" />

                    <asp:LinkButton ID="lnkbutton" runat="server" CommandName="Add" CssClass="btn btn-primary btn-sm" ValidationGroup="vldAttributes" CausesValidation="true">Add New</asp:LinkButton>
                </div>


            </InsertItemTemplate>


            <EditItemTemplate>



                <div class="form-inline">
                    <label class="control-label-mand" for="txtAttributeName">Name</label>
                    <asp:RequiredFieldValidator ID="vldName" runat="server" ControlToValidate="txtAttributeName" ErrorMessage="Please enter attribute name" Text="*"
                        ValidationGroup="vldAttributes" CssClass="text-danger"></asp:RequiredFieldValidator>

                    <asp:TextBox ID="txtAttributeName" runat="server" CssClass="form-control" ValidationGroup="vldAttributes" Text='<% Bind("AttributeName") %>' />


                    <label class="control-label-mand" for="txtAttributeDescription">Description</label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAttributeDescription" ErrorMessage="Please enter attribute description" Text="*"
                        ValidationGroup="vldAttributes" CssClass="text-danger"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtAttributeDescription" runat="server" CssClass="form-control" ValidationGroup="vldAttributes" Text='<% Bind("AttributeValue") %>' />

                    <asp:LinkButton ID="lnkbutton" runat="server" CommandName="Save" CssClass="btn btn-primary btn-sm" ValidationGroup="vldAttributes" CausesValidation="true">Update</asp:LinkButton>
                </div>

            </EditItemTemplate>

        </asp:FormView>

        <br />


        <asp:GridView ID="grdDynamicAttributeList" runat="server" AutoGenerateColumns="False" DataKeyNames="DynamicAttribute_Id"
            CssClass="table table-hover table-striped" EmptyDataText="No Dynamic Attributes Set for this product" > <%-- OnRowCommand="grdDynamicAttributeList_RowCommand" OnRowDataBound="grdDynamicAttributeList_RowDataBound" --%>
            <Columns>
                <asp:BoundField DataField="AttributeName" HeaderText="Name" SortExpression="AttributeName" />
                <asp:BoundField DataField="AttributeValue" HeaderText="Description" SortExpression="AttributeValue" />

                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                            Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("DynamicAttribute_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                            CssClass="btn btn-default" CommandArgument='<%# Bind("DynamicAttribute_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </ContentTemplate>
</asp:UpdatePanel>





