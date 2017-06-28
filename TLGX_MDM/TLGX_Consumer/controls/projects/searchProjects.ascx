<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchProjects.ascx.cs" Inherits="TLGX_Consumer.controls.projects.searchProjects" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">

    <ContentTemplate>
        <div class="row">
            <div class="col-lg-12">
                <asp:HyperLink ID="lnkAddNewProject" runat="server" NavigateUrl="~/projects/Manage" CssClass="btn btn-primary btn-sm">New Project</asp:HyperLink>
                <br />
                <br />
                <asp:GridView ID="grdProjectList" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Project_Id" CssClass="table table-hover table-striped" PageSize="10" EmptyDataText="There are no active projects"
                    OnPageIndexChanging="grdProjectList_PageIndexChanging" Width="50%">
                    <Columns>
                        <asp:BoundField DataField="Project_Name" HeaderText="Name" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:HyperLinkField DataNavigateUrlFields="Project_Id" DataNavigateUrlFormatString="~/projects/Manage?Project_Id={0}" Text="Select" NavigateUrl="~/projects/manage.aspx" HeaderText="" />
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </ContentTemplate>


</asp:UpdatePanel>
