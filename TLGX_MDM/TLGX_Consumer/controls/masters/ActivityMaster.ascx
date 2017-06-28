<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityMaster.ascx.cs" Inherits="TLGX_Consumer.controls.masters.ActivityMaster" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">

    <ContentTemplate>
<br />
<asp:Button ID="btnAddNewActivityMaster" runat="server" Text="Add Activity Master" CssClass="btn btn-primary btn-sm" OnClick="btnAddNewActivityMasterMaster_Click" />
<br /><br />
   
                <asp:GridView ID="grdActivityMasters" runat="server" DataKeyNames="Activity_Master_ID" AutoGenerateColumns="False" CssClass="table table-hover table-striped" OnSelectedIndexChanged="grdActivityMasters_SelectedIndexChanged" EmptyDataText="No Activity Masters created">
                    <Columns>
                        <asp:BoundField HeaderText="Name" DataField="Activity_Name" />
                        <asp:BoundField HeaderText="Class Name" DataField="Activity_Class_Name" />
                        <asp:BoundField HeaderText="Method Name" DataField="Activity_Method_Name" />
                        <asp:BoundField HeaderText="Status" DataField="Status" />
                        <asp:BoundField HeaderText="Message" DataField="Status_Message" />
                        <asp:BoundField HeaderText="Message" DataField="Description" />
                        <asp:CommandField ShowSelectButton="True">
                        <ControlStyle CssClass="btn btn-primary btn-sm" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>

     
     

                <asp:FormView ID="frmActivityMaster" runat="server" DefaultMode="Insert" DataKeyNames="Activity_Master_ID" OnItemUpdating="frmActivityMaster_ItemUpdating" >
                    <InsertItemTemplate>

                                <div class="panel panel-default">
                                    <div class="panel-heading">Add New Activity Master</div>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <label for="txtActivityName">Activity Name</label>
                                                <asp:TextBox ID="txtActivityName" runat="server" CssClass="form-control" />

                                                <label for="txtActivityClassName">Class Name</label>
                                                <asp:TextBox ID="txtActivityClassName" runat="server" CssClass="form-control" />

                                                <label for="txtActivityMethodName">ActivityMethodName</label>
                                                <asp:TextBox ID="txtActivityMethodName" runat="server" CssClass="form-control" />

                                                <label for="txtStatus">Status</label>
                                                <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control" />

                                                <label for="txtStatusMessage">Message</label>
                                                <asp:TextBox ID="txtStatusMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                                                
                                                <label for="txtDescription">Description</label>
                                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />


                                            </div>
                                        </div>
                <asp:Button ID="btnAddActivityMaster" runat="server" Text="Add" CssClass="btn btn-primary btn-sm" OnClick="btnAddActivityMaster_Click"   />
            </div>
                                </div>

                    </InsertItemTemplate>

                    <EditItemTemplate>

                        <div class="row">

                            <div class="col-lg-6">

                        <div class="panel panel-default">
                                    <div class="panel-heading">Activity Details</div>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="form-group">
                                                
                                                <label for="txtActivityName">Activity Name</label>
                                                <asp:TextBox ID="txtActivityName" runat="server" CssClass="form-control" Text='<%# Bind("Activity_Name") %>'/>

                                                <label for="txtActivityClassName">Class Name</label>
                                                <asp:TextBox ID="txtActivityClassName" runat="server" CssClass="form-control" Text='<%# Bind("Activity_Class_Name") %>'/>

                                                <label for="txtActivityMethodName">ActivityMethodName</label>
                                                <asp:TextBox ID="txtActivityMethodName" runat="server" CssClass="form-control" Text='<%# Bind("Activity_Method_Name") %>' />

                                                <label for="txtStatus">Status</label>
                                                <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control" Text='<%# Bind("Status") %>'/>

                                                <label for="txtStatusMessage">Message</label>
                                                <asp:TextBox ID="txtStatusMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Text='<%# Bind("Status_Message") %>' />
                                                
                                                <label for="txtDescription">Description</label>
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


                                                <asp:Button ID="btnUpdateActivityMaster" runat="server" CssClass="btn btn-primary btn-sm" CommandName="Update" Text="Update" />


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


