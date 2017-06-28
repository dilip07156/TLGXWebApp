<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="facilities.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.facilities" %>

<asp:UpdatePanel ID="updPanDescriptions" runat="server">
    <ContentTemplate>
        <div class="panel panel-default">
        <div id="dvMsg" runat="server" style="display: none;"></div>
            <div class="panel-heading">Hotel Facilities</div>
            <div class="panel-body">

                <div class="form-group">
                    <div class="col-sm-12">
                        
                        <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldgrpFacility" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                    </div>
                </div>

                <asp:FormView ID="frmFacilityDetail" runat="server" DataKeyNames="Accommodation_Facility_Id" DefaultMode="Insert" OnItemCommand="frmFacilityDetail_ItemCommand">

                    <InsertItemTemplate>
                        <div class="form-inline">
                            <label class="control-label-mand" for="ddlFacilityCategory">Category</label>
                            <asp:DropDownList runat="server" ID="ddlFacilityCategory" CssClass="form-control" AppendDataBoundItems="true" ValidationGroup="vldgrpFacility" AutoPostBack="true" OnSelectedIndexChanged="ddlFacilityCategory_SelectedIndexChanged">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="vldFacilityCategory" runat="server" ControlToValidate="ddlFacilityCategory" ErrorMessage="Please select facility category" Text="*" ValidationGroup="vldgrpFacility" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>

                            <label class="control-label-mand" for="ddlFacilityType">Type</label>
                            <asp:DropDownList runat="server" ID="ddlFacilityType" CssClass="form-control" AppendDataBoundItems="true" ValidationGroup="vldgrpFacility">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="vldFacilityType" runat="server" ControlToValidate="ddlFacilityType" ErrorMessage="Please select facility type" Text="*" ValidationGroup="vldgrpFacility" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>

                            <label for="txtFacilityName">Name</label>
                            <asp:TextBox ID="txtFacilityName" runat="server" CssClass="form-control" />

                            <label for="txtFacilityDescription">Description</label>
                            <asp:TextBox ID="txtFacilityDescription" runat="server" CssClass="form-control" />

                            <asp:LinkButton ID="lnkButton" CommandName="Add" runat="server" CssClass="btn btn-primary btn-sm" ValidationGroup="vldgrpFacility" CausesValidation="true">Add</asp:LinkButton>
                        </div>


                    </InsertItemTemplate>

                    <EditItemTemplate>
                        <div class="form-inline">
                            <label class="control-label-mand" for="ddlFacilityCategory">Category</label>
                            <asp:DropDownList runat="server" ID="ddlFacilityCategory" CssClass="form-control" AppendDataBoundItems="true" ValidationGroup="vldgrpFacility">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="vldFacilityCategory" runat="server" ControlToValidate="ddlFacilityCategory" ErrorMessage="Please select facility category" Text="*" ValidationGroup="vldgrpFacility" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>

                            <label class="control-label-mand" for="ddlFacilityType">Type</label>
                            <asp:DropDownList runat="server" ID="ddlFacilityType" CssClass="form-control" AppendDataBoundItems="true" ValidationGroup="vldgrpFacility">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="vldFacilityType" runat="server" ControlToValidate="ddlFacilityType" ErrorMessage="Please select facility type" Text="*" ValidationGroup="vldgrpFacility" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>

                            <label for="txtFacilityName">Name</label>
                            <asp:TextBox ID="txtFacilityName" runat="server" CssClass="form-control" />

                            <label for="txtFacilityDescription">Description</label>
                            <asp:TextBox ID="txtFacilityDescription" runat="server" CssClass="form-control" />
                            <asp:LinkButton ID="lnkButton" CommandName="Save" runat="server" CssClass="btn btn-primary btn-sm" ValidationGroup="vldgrpFacility" CausesValidation="true">Save</asp:LinkButton>
                        </div>
                    </EditItemTemplate>


                </asp:FormView>

                <br />

                <asp:GridView ID="grdFacilityList" runat="server" AutoGenerateColumns="False" DataKeyNames="Accommodation_Facility_Id" CssClass="table table-hover table-striped" OnRowCommand="grdFacilityList_RowCommand" OnRowDataBound="grdFacilityList_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="FacilityCategory" HeaderText="FacilityCategory" SortExpression="FacilityCategory" />
                        <asp:BoundField DataField="FacilityType" HeaderText="FacilityType" SortExpression="FacilityType" />
                        <asp:BoundField DataField="FacilityName" HeaderText="Name" SortExpression="FacilityName" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />

                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default" Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Accommodation_Facility_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>' CssClass="btn btn-default" CommandArgument='<%# Bind("Accommodation_Facility_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>



                    </Columns>
                </asp:GridView>

            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
