<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RoomAmenities.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.RoomAmenities" %>

        <div id="dvMsg" runat="server" style="display: none;"></div>
<asp:FormView ID="frmRoomAmenity" runat="server" DataKeyNames="Accommodation_RoomFacility_Id" DefaultMode="Insert" OnDataBound="frmRoomAmenity_DataBound" OnItemCommand="frmRoomAmenity_ItemCommand">
    <HeaderTemplate>
        <div class="form-inline">
            <div class="col-sm-12">
                <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldRoomAmenities" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
            </div>
        </div>
    </HeaderTemplate>

    <InsertItemTemplate>
        <div class="form-inline">
            <label class="control-label-mand" for="ddlFacilityCategory">Type</label>
            <asp:RequiredFieldValidator ID="vldddlFacilityCategory" ControlToValidate="ddlFacilityCategory" runat="server" 
                ValidationGroup="vldRoomAmenities" InitialValue="0" ErrorMessage="Please select facility category" Text="*" CssClass="text-danger"></asp:RequiredFieldValidator>
            <asp:DropDownList runat="server" ID="ddlFacilityCategory" CssClass="form-control" AppendDataBoundItems="true">
                <asp:ListItem Value="0">-Select-</asp:ListItem>
            </asp:DropDownList>
            <label for="ddlFacilityCategory">Name</label>
            <asp:TextBox ID="txtAmenityName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:LinkButton ID="lnkAddNewAmenity" runat="server" CssClass="btn btn-primary btn-sm" CommandName="AddAmenity" CausesValidation="true" ValidationGroup="vldRoomAmenities">Add New</asp:LinkButton>
        </div>
    </InsertItemTemplate>
    <EditItemTemplate>
        <div class="form-inline">
            <label class="control-label-mand" for="ddlFacilityCategory">Type<asp:RequiredFieldValidator ID="vldddlFacilityCategory" runat="server" 
                ValidationGroup="vldRoomAmenities" InitialValue="0" ControlToValidate="ddlFacilityCategory" 
                ErrorMessage="Please select facility category" Text="*" CssClass="text-danger"></asp:RequiredFieldValidator></label>
            <asp:DropDownList runat="server" ID="ddlFacilityCategory" CssClass="form-control" AppendDataBoundItems="true">
                <asp:ListItem Value="0">-Select-</asp:ListItem>
            </asp:DropDownList>
            <label for="ddlFacilityCategory">Name</label>
            <asp:TextBox ID="txtAmenityName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:LinkButton ID="lnkUpdateNewAmenity" runat="server" CssClass="btn btn-primary btn-sm" CommandName="UpdateAmenity" ValidationGroup="vldRoomAmenities" CausesValidation="true">Update</asp:LinkButton>
        </div>
    </EditItemTemplate>
</asp:FormView>
<br />
<asp:GridView ID="grdRoomAmenities" runat="server" DataKeyNames="Accommodation_RoomFacility_Id" EmptyDataText="No Room Amenities" CssClass="table table-hover table-striped" AutoGenerateColumns="False" OnRowCommand="grdRoomAmenities_RowCommand" OnRowDataBound="grdRoomAmenities_RowDataBound">
    <Columns>
        <asp:BoundField DataField="AmenityType" HeaderText="Type" />
        <asp:BoundField DataField="AmenityName" HeaderText="Name" />

        <asp:TemplateField ShowHeader="false">
            <ItemTemplate>
                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                    Enabled='<%# (bool)Eval("IsActive") && (bool)Eval("IsRoomActive") %>' CommandArgument='<%# Bind("Accommodation_RoomFacility_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField ShowHeader="false">
            <ItemTemplate>
                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                    CssClass="btn btn-default" CommandArgument='<%# Bind("Accommodation_RoomFacility_Id") %>' Enabled='<%# Eval("IsRoomActive") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>
