<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApprovalStatusMaster.ascx.cs" Inherits="TLGX_Consumer.controls.masters.ApprovalStatusMaster" %>



<asp:UpdatePanel ID="UpdatePanel1" runat="server">

<ContentTemplate>
<br />
<asp:Button ID="btnAddNewApprovalStatusMaster" runat="server" Text="Add Approval Role Master" CssClass="btn btn-primary btn-sm" />
<br /><br />
   
                <asp:GridView ID="grdApprovalStatusMasters" runat="server" DataKeyNames="Appr_status_id" AutoGenerateColumns="False" CssClass="table table-hover table-striped" EmptyDataText="No Approval Status set" OnSelectedIndexChanged="grdApprovalStatusMasters_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField HeaderText="Status" DataField="Status" />
                        <asp:BoundField HeaderText="Type" DataField="Object_type" />
                        <asp:BoundField HeaderText="Object" DataField="Object_id" />
                        <asp:BoundField HeaderText ="Hierarchy " DataField="Status_hierarchy" />
                        <asp:CommandField ShowSelectButton="True">
                        <ControlStyle CssClass="btn btn-primary btn-sm" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>


    <br />



                    <asp:FormView ID="frmApprovalStatusMasters" runat="server" DefaultMode="Insert" DataKeyNames="Appr_status_id" OnItemInserting="frmApprovalStatusMasters_ItemInserting" OnItemUpdating="frmApprovalStatusMasters_ItemUpdating"  >
                    <InsertItemTemplate>

                                <div class="panel panel-default">
                                    <div class="panel-heading">Add New Approval Role</div>
                                        <div class="panel-body">

                                            <div class="form-group">
                                                <label for="txtStatus">Status</label>
                                                <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control" />

                                                <label for="txtObjectType">Object Type</label>
                                                <asp:TextBox ID="txtObjectType" runat="server" CssClass="form-control" />

                                                <label for="txtObject_id">Object Id</label><!--not sure how you want to bind this  -->
                                                <asp:TextBox ID="txtObject_id" runat="server" CssClass="form-control" />

                                                <label for="txtHierarchy">Hierarchy</label><!--not sure how you want to handle the integer here -->
                                                <asp:TextBox ID="txtHierarchy" runat="server" CssClass="form-control" />                                                                                            

                                            </div>

                                             <asp:Button ID="btnAddApprovalRoleMaster" runat="server" Text="Add" CssClass="btn btn-primary btn-sm" CommandName="Insert" />
                                        </div>
                                    </div>
                        </InsertItemTemplate>

                        <EditItemTemplate>
                                    <div class="row">
                                       <div class="col-lg-6">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Add New Approval Role</div>
                                                <div class="panel-body">

                                                    <div class="form-group">
                                                        <label for="txtStatus">Status</label>
                                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control" Text='<%# Bind("Status") %>' />

                                                        <label for="txtObjectType">Object Type</label>
                                                        <asp:TextBox ID="txtObjectType" runat="server" CssClass="form-control" Text='<%# Bind("Object_type") %>' />

                                                        <label for="txtObject_id">Object Id</label><!--not sure how you want to bind this  -->
                                                        <asp:TextBox ID="txtObject_id" runat="server" CssClass="form-control" Text='<%# Bind("Object_id") %>' />

                                                        <label for="txtHierarchy">Hierarchy</label>
                                                        <asp:TextBox ID="txtHierarchy" runat="server" CssClass="form-control" Text='<%# Bind("Status_hierarchy") %>' />



                                                    </div>

                                                    <asp:Button ID="btnAddApprovalRoleMaster" runat="server" Text="Update" CssClass="btn btn-primary btn-sm" CommandName="Update" />
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
