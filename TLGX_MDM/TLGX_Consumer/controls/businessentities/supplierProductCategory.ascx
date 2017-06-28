<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="supplierProductCategory.ascx.cs" Inherits="TLGX_Consumer.controls.businessentities.supplierProductCategory" %>
<asp:UpdatePanel runat="server" ID="supplierProduct" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="dvMsg" runat="server" style="display: none;"></div>
        <div class="col-md-12">
            <div class="col-sm-12">
                <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="supplierProductCategory" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
            </div>
            <asp:FormView ID="frmSupplierProductCategory" runat="server" DataKeyNames="Supplier_ProductCategory_Id" OnItemCommand="frmSupplierProductCategory_ItemCommand" DefaultMode="Insert">
                <InsertItemTemplate>
                    <div class="form-inline">
                        <div class="form-group">
                            <label for="ddlProductCategory">
                                Product Category
                                 <asp:RequiredFieldValidator ValidationGroup="supplierProductCategory" runat="server" ControlToValidate="ddlProductCategory"
                                     CssClass="text-danger" ErrorMessage="The Product Category field is required." InitialValue="0" Text="*" />
                            </label>
                            <asp:DropDownList ID="ddlProductCategory" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlProductCategory_SelectedIndexChanged" CssClass="form-control">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="ddlProductCategorySubType">
                                Product Category Sub Type
                                <asp:RequiredFieldValidator ValidationGroup="supplierProductCategory" runat="server" ControlToValidate="ddlProductCategorySubType"
                                    CssClass="text-danger" ErrorMessage="The Product Category Sub Type field is required." InitialValue="0" Text="*" />
                            </label>
                            <asp:DropDownList ID="ddlProductCategorySubType" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" runat="server" id="chckbIsDefaultSupplier">
                                Is Default Supplier</label>
                        </div>
                        <div class="form-group"></div>
                        <asp:LinkButton ID="lnkButton" CommandName="Add" runat="server" CssClass="btn btn-primary btn-sm" ValidationGroup="supplierProductCategory" CausesValidation="true">Add</asp:LinkButton>
                    </div>
                </InsertItemTemplate>
                <EditItemTemplate>
                    <div class="form-inline">
                        <div class="form-group">
                            <label for="ddlProductCategory">
                                Product Category
                                 <asp:RequiredFieldValidator ValidationGroup="supplierProductCategory" runat="server" ControlToValidate="ddlProductCategory"
                                     CssClass="text-danger" ErrorMessage="The Product Category field is required." InitialValue="0" Text="*" />
                            </label>
                            <asp:DropDownList ID="ddlProductCategory" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlProductCategory_SelectedIndexChanged" CssClass="form-control">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="ddlProductCategorySubType">
                                Product Category Sub Type
                                 <asp:RequiredFieldValidator ValidationGroup="supplierProductCategory" runat="server" ControlToValidate="ddlProductCategorySubType"
                                     CssClass="text-danger" ErrorMessage="The Product Category Sub Type field is required." InitialValue="0" Text="*" />
                            </label>
                            <asp:DropDownList ID="ddlProductCategorySubType" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" runat="server" id="chckbIsDefaultSupplier">
                                Is Default Supplier</label>
                        </div>
                        <div class="form-group"></div>
                        <asp:LinkButton ID="lnkButton" CommandName="Modify" runat="server" CssClass="btn btn-primary btn-sm" ValidationGroup="supplierProductCategory" CausesValidation="true">Update</asp:LinkButton>
                    </div>
                </EditItemTemplate>
            </asp:FormView>
            <br />
        </div>
        <asp:GridView ID="grdSupplierProductCategory" runat="server" AutoGenerateColumns="False" DataKeyNames="Supplier_ProductCategory_Id" EmptyDataText="No Supplier Product Categories specified" CssClass="table table-hover table-striped"
            OnRowCommand="grdSupplierProductCategory_RowCommand" OnRowDataBound="grdSupplierProductCategory_RowDataBound">
            <Columns>
                <asp:BoundField DataField="ProductCategory" HeaderText="ProductCategory" />
                <asp:BoundField DataField="ProductCategorySubType" HeaderText="ProductCategorySubType" />
                <asp:BoundField DataField="IsDefaultSupplier" HeaderText="Is Default Supplier" />
                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default" Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Supplier_ProductCategory_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>' CssClass="btn btn-default" CommandArgument='<%# Bind("Supplier_ProductCategory_Id") %>'>
                      <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                            <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>
