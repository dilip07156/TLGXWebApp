<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchMasterTeams.ascx.cs" Inherits="TLGX_Consumer.controls.projects.searchMasterTeams" %>

<asp:UpdatePanel ID="updPanTeams" runat="server">
    <ContentTemplate>
        <asp:FormView ID="frmTeamMasters" runat="server" DataKeyNames="Team_Id" DefaultMode="Insert" OnDataBinding="frmTeamMasters_DataBinding" OnItemCommand="frmTeamMasters_ItemCommand">
            <InsertItemTemplate>
                <div class="container">
                    <div class="panel panel-default">
                        <div class="panel-heading">Add Team Masters</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-inline">
                                        <label for="txtTeamName">Name</label>
                                        <asp:RequiredFieldValidator ID="vtxtTeamName" ControlToValidate="txtTeamName" runat="server" ValidationGroup="TeamMasters" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtTeamName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <label for="ddlStatus">Status</label>
                                        <asp:RequiredFieldValidator ID="vddlStatus" ControlToValidate="ddlStatus" runat="server" ValidationGroup="TeamMasters" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnkAddTeam" runat="server" CssClass="btn btn-primary btn-sm" CommandName="AddTeam" CausesValidation="true" ValidationGroup="TeamMasters">Add New</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </InsertItemTemplate>
            <EditItemTemplate>
                <div class="container">
                    <div class="panel panel-default">
                        <div class="panel-heading">Add Team Masters</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-inline">
                                        <label for="txtTeamName">Name</label>
                                        <asp:RequiredFieldValidator ID="vtxtTeamName" ControlToValidate="txtTeamName" runat="server" ValidationGroup="TeamMasters" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtTeamName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <label for="ddlStatus">Status</label>
                                        <asp:RequiredFieldValidator ID="vddlStatus" ControlToValidate="ddlStatus" runat="server" ValidationGroup="TeamMasters" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnkUpdateTeam" runat="server" CssClass="btn btn-primary btn-sm" CommandName="UpdateTeam" CausesValidation="true" ValidationGroup="TeamMasters">Update</asp:LinkButton>
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

<asp:HyperLink ID="lnkAddNewProject" runat="server" NavigateUrl="~/projects/masterteams/manage" CssClass="btn btn-primary btn-sm">New Team</asp:HyperLink>
<br />
<br />
<asp:GridView ID="grdTeamList" runat="server" AutoGenerateColumns="false" DataKeyNames="Team_Id" CssClass="table table-hover table-striped" PageSize="10" EmptyDataText="There are no active Master Teams">

    <Columns>
        <asp:BoundField DataField="Team_Name" HeaderText="Name" />
        <asp:BoundField DataField="Status" HeaderText="Status" />
        <asp:HyperLinkField DataNavigateUrlFields="Team_Id" DataNavigateUrlFormatString="~/projects/masterteams/manage?Team_Id={0}" DataTextField="Team_Id" NavigateUrl="~/projects/masterteams/manage.aspx" HeaderText="Manage" />
    </Columns>

</asp:GridView>
