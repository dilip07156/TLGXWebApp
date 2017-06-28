<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="teammembersinproject.ascx.cs" Inherits="TLGX_Consumer.controls.projects.teammembersinproject" %>
<br />
<h4>Current Team Members</h4>

<!-- grid to show list of current members in the team level entity -->
<!-- business logic will be required to handle the removal of a member and to re-handle any prroducts in workflow state-->
<asp:GridView ID="grdCurrentTeam" runat="server" CssClass="table table-hover table-striped" EmptyDataText="There are no members of this Project" AllowPaging="True" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField HeaderText="User" DataField="Username" />
        <asp:CheckBoxField HeaderText="Remove" />
        </Columns>
</asp:GridView>

<br />

    <div class="panel panel-default">
            <div class="panel-heading">Add Master Team</div>
            <div class="panel-body">



                <!-- drop down list shows Team Names that are NOT IN PROJECT -->
                <label for="ddlAddTeam">Master Teams</label>
                <asp:DropDownList runat="server" ID="ddlAddTeam" CssClass="form-control"></asp:DropDownList>
                <br />

                <asp:GridView ID="grdNewTeamMembers" runat="server" CssClass="table table-hover table-striped" EmptyDataText="Please select a Master Team" AllowPaging="True" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField HeaderText="User" DataField="Username" />
                        <asp:CheckBoxField HeaderText="Include" />
                    </Columns>
                </asp:GridView>


                <asp:Button ID="btnAddSelected" runat="server" Text="Add Selected" CssClass="btn btn-primary btn-sm" /><!-- Adds boolean checked  -->
                <asp:Button ID="btnAddAll" runat="server" Text="Add All" CssClass="btn btn-primary btn-sm" /><!--Adds All -->
                <!-- ADD OF MEMBER TO PROJECT SHOULD HANDLE THE CREATION OF THE LINK TABLE FOR ROLE MANAGEMENT-->

            </div>
     </div>