<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="managetask.ascx.cs" Inherits="TLGX_Consumer.controls.workflow.managetask" %>



<div class="row">

<div class="col-lg-4">
    <div class="panel panel-default">
            <div class="panel-heading">Tasks Detail</div>
            <div class="panel-body">

                <asp:FormView ID="frmTaskDetail" runat="server" DefaultMode="Insert">
                    
                    
                    <InsertItemTemplate>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtTaskType" CssClass="col-md-4 control-label">Type</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtTaskType" CssClass="form-control"  enabled="false"   />

                            </div>
                        </div>
                    
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtName" CssClass="col-md-4 control-label">Name</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtName" CssClass="form-control"  enabled="false"  />

                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtPriority" CssClass="col-md-4 control-label">Priority</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtPriority" CssClass="form-control"  enabled="false" />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtStatus" CssClass="col-md-4 control-label">Status</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtStatus" CssClass="form-control"  enabled="false" />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtAssignedTo" CssClass="col-md-4 control-label">Owner</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtAssignedTo" CssClass="form-control"  enabled="false"  />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtCreateDate" CssClass="col-md-4 control-label">Create Date</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtCreateDate" CssClass="form-control"  enabled="false" />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtCreateUser" CssClass="col-md-4 control-label">Create By</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtCreateUser" CssClass="form-control"  enabled="false"  />
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-12">
                            <asp:Label runat="server" AssociatedControlID="txtDescription" >Description</asp:Label>
                                <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine" Rows="5"  enabled="false" />
                            </div>
                        </div>



                    </InsertItemTemplate>
                </asp:FormView>



            </div>
    </div>

</div>

<div class="col-lg-8">
    <div class="panel panel-default">
            <div class="panel-heading">Comments</div>
            <div class="panel-body">


            </div>
    </div>


</div>


</div>