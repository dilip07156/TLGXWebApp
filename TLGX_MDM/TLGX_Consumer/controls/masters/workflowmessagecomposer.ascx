<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="workflowmessagecomposer.ascx.cs" Inherits="TLGX_Consumer.controls.workflow.workflowmessagecomposer" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">

    <ContentTemplate>

<br />
<h4>Current Workflow Messages</h4>

<asp:Button ID="btnAddNewWorkFlowMessage" runat="server" Text="Add Workflow Message" CssClass="btn btn-primary btn-sm" OnClick="btnAddNewWorkFlowMessage_Click" />
<br /><br />
   
                <asp:GridView ID="grdWorkFlowMessage" runat="server" DataKeyNames="WorkFlowMessage_Id" AutoGenerateColumns="False" CssClass="table table-hover table-striped"  EmptyDataText="No Workflow Messages Created" OnSelectedIndexChanged="grdWorkFlowMessage_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField HeaderText="From" DataField="From" />
                        <asp:BoundField HeaderText="To" DataField="To" />
                        <asp:BoundField HeaderText="Subject" DataField="Subject" />
                        <asp:CommandField ShowSelectButton="True">
                        <ControlStyle CssClass="btn btn-primary btn-sm" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>


 <asp:FormView ID="frmWorkFlowMessage" runat="server" DefaultMode="Insert" DataKeyNames="m_WorkFlowMessage_Id"  OnItemUpdating="frmWorkFlowMessage_ItemUpdating"  >
                    <InsertItemTemplate>

                        <div class="panel panel-default">
                            <div class="panel-heading">Add New Workflow Message</div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label for="txtSubject">Subject</label>
                                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" />

                                        <label for="txtTo">To</label>
                                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" />

                                        <label for="txtFrom">From</label>
                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" />

                                        <label for="txtCC">CC</label>
                                        <asp:TextBox ID="txtCC" runat="server" CssClass="form-control" />

                                        <label for="txtMessage">Message</label>
                                        <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />


                                    </div>
                                </div>
                                <asp:Button ID="btnAddWorkFlowMessage" runat="server" Text="Add"  CssClass="btn btn-primary btn-sm" OnClick="btnAddWorkFlowMessage_Click"   />
                            </div>
                        </div>

                    </InsertItemTemplate>

                    <EditItemTemplate>

                        <div class="row">

                            <div class="col-lg-6">

                       <div class="panel panel-default">
                            <div class="panel-heading">Add New Workflow Message</div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label for="txtSubject">Subject</label>
                                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" Text='<%# Bind("Subject") %>' />

                                        <label for="txtTo">To</label>
                                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" Text='<%# Bind("To") %>' />

                                        <label for="txtFrom">From</label>
                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" Text='<%# Bind("From") %>' />

                                        <label for="txtCC">CC</label>
                                        <asp:TextBox ID="txtCC" runat="server" CssClass="form-control" Text='<%# Bind("CC") %>' />

                                        <label for="txtMessage">Message</label>
                                        <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Text='<%# Bind("Text") %>' />


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
                                                <asp:TextBox ID="txtCreateDate" runat="server" CssClass="form-control" Enabled="false" Text='<%# Bind("Create_Date") %>' />

                                                <label for="txtCreateUser">Create User</label>
                                                <asp:TextBox ID="txtCreateUser" runat="server" CssClass="form-control" Enabled="false" Text='<%# Bind("Create_User") %>' />

                                                <label for="txtEditDate">Edit Date</label>
                                                <asp:TextBox ID="txtEditDate" runat="server" CssClass="form-control" Enabled="false" Text='<%# Bind("Edit_Date") %>' />
                                                
                                                <label for="txtEditUser">Edit User</label>
                                                <asp:TextBox ID="txtEditUSer" runat="server" CssClass="form-control" Enabled="false" Text='<%# Bind("Edit_User") %>' />


                                                <asp:Button ID="btnUpdateWorkFlowMessage" runat="server" CssClass="btn btn-primary btn-sm" CommandName="Update" Text="Update" />


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