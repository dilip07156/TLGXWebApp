<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="manageMasterTeam.ascx.cs" Inherits="TLGX_Consumer.controls.projects.manageMasterTeam" %>

<asp:FormView ID="frmMasterTeam" runat="server" DefaultMode="Insert" >

    <InsertItemTemplate>    
        <div class="panel panel-default">
            <div class="panel-heading">Add New Master Team</div>
            <div class="panel-body">
                <div class="form-group">
                    <div class="form-group">
                        <label for="txtTeamname">Team Name</label>
                        <asp:TextBox ID="txtTeamname" runat="server" CssClass="form-control" />
                    </div>                                   
                </div>
                <asp:Button ID="btnAddProject" runat="server" Text="Add Team" CssClass="btn btn-primary btn-sm" OnClick="btnAddTeam_Click" />
            </div>
        </div>
    </InsertItemTemplate>


    <EditItemTemplate>
<div class="container">
            <div class="row">

                <div class="col-lg-3">
                            <div class="panel panel-default">
                                <div class="panel-heading">Team Details</div>
                                <div class="panel-body">

                                    <label for="txtProjectname">Name</label>
                                    <asp:TextBox ID="txtProjectname" runat="server" Text='<%# Bind("Team_Name") %>' CssClass="form-control" />
                                
                                    <label for="ddlProjectStatus"">Status</label><!-- you'll need to bind status to selected value -->
                                    <asp:DropDownList runat="server" ID="ddlProjectStatus" CssClass="form-control" ></asp:DropDownList>
                                    <br />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update Team" CssClass="btn btn-primary btn-sm" />
                                </div>
                            </div>
                </div>

                <div class="col-lg-9">

<h4>Current Team Members</h4>
                    <!-- bind to team members in project -->
                    <asp:GridView ID="grdTeamMembers" runat="server" AllowPaging="true" PageSize="10" EmptyDataText="There are no Members in this team" CssClass="table table-hover table-striped" >
                        <Columns>
                            <asp:BoundField HeaderText="Member" />
                     <asp:CheckBoxField HeaderText="Remove" />
                        </Columns>


                    </asp:GridView>

                    <asp:Button ID="btnRemoveSelected" runat="server" Text="Remove Selected Members" CssClass="btn btn-primary btn-sm" OnClick="btnAddTeam_Click" />
                    <br />
                    <br />

                    <div class="panel panel-default">
                          <div class="panel-heading">Add New Team Member</div>
                                <div class="panel-body">

                                <!-- Bind to System Users not in Project -->
                                    <label for="ddlTeamMember"">Select Team Member</label>
                                    <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control" ></asp:DropDownList>
                                    <br />
                                    <asp:Button ID="btnAddTeamMember" runat="server" Text="Add Team Member" CssClass="btn btn-primary btn-sm" />
                                </div>
                            </div>
                </div>


                </div>

</div>
</div>
    </EditItemTemplate>

</asp:FormView>




