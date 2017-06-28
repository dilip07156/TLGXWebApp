<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApprovalRoleMaster.ascx.cs" Inherits="TLGX_Consumer.controls.masters.ApprovalRoleMaster" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">

    <ContentTemplate>
<br />
<asp:Button ID="btnAddNewApprovalRoleMaster" runat="server" Text="Add Approval Role Master" CssClass="btn btn-primary btn-sm" OnClick="btnAddNewApprovalRoleMaster_Click" />
<br /><br />
   
                <asp:GridView ID="grdApprovalRoleMasters" runat="server" DataKeyNames="Appr_Role_ID" AutoGenerateColumns="False" CssClass="table table-hover table-striped" OnSelectedIndexChanged="grdApprovalRoleMasters_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField HeaderText="Name" DataField="Role_Name" />
                        <asp:BoundField HeaderText="Description" DataField="Description" />
                        <asp:BoundField HeaderText="Status" DataField="Status" />
                        <asp:CommandField ShowSelectButton="True">
                        <ControlStyle CssClass="btn btn-primary btn-sm" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>

     
     

                <asp:FormView ID="frmApprovalRoleMaster" runat="server" DefaultMode="Insert" DataKeyNames="Appr_Role_ID" OnItemUpdating="frmApprovalRoleMaster_ItemUpdating" >
                    <InsertItemTemplate>

                                <div class="panel panel-default">
                                    <div class="panel-heading">Add New Approval Role</div>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <label for="txtRoleName">Role Name</label>
                                                <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control" />

                                                <label for="ddlStatus">Status</label><!--Status needs to be bound -->
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem>-Select-</asp:ListItem>
                                                    <asp:ListItem Value="Active">Active</asp:ListItem>
                                                </asp:DropDownList>

                                                <label for="txtDescription">Description</label>
                                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />


                                            </div>
                                        </div>
                <asp:Button ID="btnAddApprovalRoleMaster" runat="server" Text="Add" CssClass="btn btn-primary btn-sm" OnClick="btnAddApprovalRoleMaster_Click" />
            </div>
                                </div>

                    </InsertItemTemplate>

                    <EditItemTemplate>

                        <div class="row">

                            <div class="col-lg-6">

                        <div class="panel panel-default">
                                    <div class="panel-heading">Approval Role Details</div>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <label for="txtRoleName">Role Name</label>
                                                <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control" Text='<%# Bind("Role_Name") %>' />

                                                <label for="ddlStatus">Status</label><!--Status needs to be bound -->
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem>-Select-</asp:ListItem>
                                                </asp:DropDownList>

                                                <label for="txtDescription">Role Name</label>
                                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Text='<%# Bind("Description") %>' />


                                            </div>
                                        </div>
            </div>
                                </div>

                            </div>

                            <div class="col-lg-6">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Audit Trail</div>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <label for="txtCreateDate">Create Date</label>
                                                <asp:TextBox ID="txtCreateDate" runat="server" CssClass="form-control" Enabled="false" Text='<%# Bind("CREATE_DATE") %>' />

                                                <label for="txtCreateUser">Create User</label>
                                                <asp:TextBox ID="txtCreateUser" runat="server" CssClass="form-control" Enabled="false" Text='<%# Bind("CREATE_USER") %>' />

                                                <label for="txtEditDate">Edit Date</label>
                                                <asp:TextBox ID="txtEditDate" runat="server" CssClass="form-control" Enabled="false" Text='<%# Bind("UPDATE_DATE") %>' />
                                                
                                                <label for="txtEditUser">Edit User</label>
                                                <asp:TextBox ID="txtEditUSer" runat="server" CssClass="form-control" Enabled="false" Text='<%# Bind("UPDATE_USER") %>' />


                                                <asp:Button ID="btnUpdateApprovalRoleMaster" runat="server" CssClass="btn btn-primary btn-sm" CommandName="Update" Text="Update" />


                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>



                    </EditItemTemplate>

                </asp:FormView>












        


    </ContentTemplate>

</asp:UpdatePanel>
