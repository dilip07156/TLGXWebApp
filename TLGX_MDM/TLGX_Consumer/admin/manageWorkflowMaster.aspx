<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manageWorkflowMaster.aspx.cs" Inherits="TLGX_Consumer.admin.manageWorkflowMaster" %>
<%@ Register Src="~/controls/masters/ApprovalRoleMaster.ascx" TagPrefix="uc1" TagName="ApprovalRoleMaster" %>
<%@ Register Src="~/controls/masters/ApprovalStatusMaster.ascx" TagPrefix="uc1" TagName="ApprovalStatusMaster" %>
<%@ Register Src="~/controls/masters/ActivityMaster.ascx" TagPrefix="uc1" TagName="ActivityMaster" %>
<%@ Register Src="~/controls/masters/workflowmessagecomposer.ascx" TagPrefix="uc1" TagName="workflowmessagecomposer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header">Manage Workflow Masters</h1>

     <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="tab" href="#panApprovalRoleMaster">Approval Role Masters</a></li>
                    <li><a data-toggle="tab" href="#panApprovalStatusMasters">Approval Status Masters</a></li>
                    <li><a data-toggle="tab" href="#panActivityMaster">Approval Activity Masters</a></li>
                    <li><a data-toggle="tab" href="#panMessageMaster">Workflow Message Masters</a></li>
     </ul>

               <div class="tab-content">
                    <div id="panApprovalRoleMaster" class="tab-pane fade in active">
                        <uc1:ApprovalRoleMaster runat="server" ID="ApprovalRoleMaster" />
                    </div>

                    <div id="panApprovalStatusMasters" class="tab-pane fade in">
                        <uc1:ApprovalStatusMaster runat="server" ID="ApprovalStatusMaster" />              
                    </div>

                    <div id="panActivityMaster" class="tab-pane fade in">
                        <uc1:ActivityMaster runat="server" ID="ActivityMaster" />
                    </div>
                   
                   <div id="panMessageMaster" class="tab-pane fade in">
                       <uc1:workflowmessagecomposer runat="server" ID="workflowmessagecomposer" />
                    </div>
                </div>

</asp:Content>
