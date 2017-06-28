<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="manageProjects.ascx.cs" Inherits="TLGX_Consumer.controls.projects.manageProjects" %>
<%@ Register Src="~/controls/projects/productsinproject.ascx" TagPrefix="uc1" TagName="productsinproject" %>
<%@ Register Src="~/controls/projects/teammembersinproject.ascx" TagPrefix="uc1" TagName="teammembers" %>
<%@ Register Src="~/controls/projects/rolesinproject.ascx" TagPrefix="uc1" TagName="rolesinproject" %>
<%@ Register Src="~/controls/projects/workflowinproject.ascx" TagPrefix="uc1" TagName="workflowinproject" %>

<asp:FormView ID="frmProject" runat="server" DefaultMode="Insert" >
    <InsertItemTemplate>    
        <div class="panel panel-default">
            <div class="panel-heading">Add New Project</div>
            <div class="panel-body">
                <div class="form-group">
                    <div class="form-group">
                        <label for="txtProjectname">Project Name</label>
                        <asp:TextBox ID="txtProjectName" runat="server" CssClass="form-control" />
                    </div>                                   
                </div>
                <asp:Button ID="btnAddProject" runat="server" Text="Add Project" CssClass="btn btn-primary btn-sm" OnClick="btnAddProject_Click" />
            </div>
        </div>
    </InsertItemTemplate>

    <EditItemTemplate>

  
   <div class="container">            
        <div class="row">

            <div class="col-lg-3">


                <div class="panel panel-default">
                    <div class="panel-heading">Project Details</div>
                    <div class="panel-body">

                        <label for="txtProjectname">Name</label>
                        <asp:TextBox ID="txtProjectname" runat="server" Text='<%# Bind("Project_Name") %>' CssClass="form-control" />
                        <br />

                          <label for="ddlProjectStatus"">Status</label>
                        <asp:DropDownList ID="ddlProjectStatus" CssClass="form-control" runat="server" CausesValidation="false" AutoPostBack="false" ></asp:DropDownList>


<%--                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Status") %>' CssClass="form-control" />--%>
                        <br />

                        <asp:Button ID="btnUpdate" runat="server" Text="Update Project" CssClass="btn btn-primary btn-sm" />
                    </div>
                </div>




            </div>

            <div class="col-lg-9">

                <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="tab" href="#Products">Products</a></li>
                    <li><a data-toggle="tab" href="#TeamMembers">Team</a></li>
                    <li><a data-toggle="tab" href="#ProjectRoles">Roles</a></li>
                    <li><a data-toggle="tab" href="#Workflow">Workflow</a></li>
                    <li><a data-toggle="tab" href="#NextOne">Next Tab</a></li>
                </ul>

                <div class="tab-content">
                    <div id="Products" class="tab-pane fade in active">
                        <uc1:productsinproject runat="server" ID="productsinproject" />
                    </div>

                    <div id="TeamMembers" class="tab-pane fade in">
                        <uc1:teammembers runat="server" ID="teammembers1" />
                    </div>

                    <div id="ProjectRoles" class="tab-pane fade in">
                        <uc1:rolesinproject runat="server" ID="rolesinproject" />
                    </div>

                    <div id="Workflow" class="tab-pane fade in">
                        <uc1:workflowinproject runat="server" ID="workflowinproject" />
                    </div>

                    <div id="NextOne" class="tab-pane fade in">
                        NExtOne
                    </div>

                </div>
            </div>
        </div>
              
    </div>

    </EditItemTemplate>



</asp:FormView>


