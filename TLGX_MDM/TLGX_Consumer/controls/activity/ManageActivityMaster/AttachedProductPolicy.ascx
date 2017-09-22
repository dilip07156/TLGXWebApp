<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachedProductPolicy.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityMaster.AttachedProductPolicy" %>

<div class="container">
    <asp:UpdatePanel ID="updNewActivity" runat="server">
        <ContentTemplate>

            <div class="container">
                <div class="col-lg-12 row">
                    <div id="dvMsg" runat="server" style="display: none"></div>
                </div>
            </div>

            <%--<asp:FormView ID="frmProductInfo" runat="server" DefaultMode="Edit">

    <HeaderTemplate>
        <div class="container">
            <div class="col-lg-12 row">
                <asp:ValidationSummary ID="vlSum" runat="server" ValidationGroup="productInformation" DisplayMode="BulletList" ShowSummary="true" ShowMessageBox="false" CssClass="alert alert-danger" />
            </div>
        </div>
    </HeaderTemplate>

    <EditItemTemplate>--%>
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">

                            <div class="panel-heading">Attached Product Policy</div>

                            <div class="panel-body">

                                <div class="col-md-6">

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlProductType">
                                            Product Type
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlPolicyType">
                                            Policy Type
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlPolicyType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlPolicyCategory">
                                            Policy Category
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlPolicyCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12 row">
                                        <label class="control-label col-sm-6" for="txtProductName">Policy Name</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtPolicyName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12 row">
                                        <asp:LinkButton ID="btnNewActivity" runat="server" Text="Add New" CssClass="btn btn-primary btn-sm pull-right" OnClientClick="showAddNewActivityModal();" />
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <%--</EditItemTemplate>

</asp:FormView>--%>

            <div class="form-group">
                <div id="dvGrid" runat="server" class="control-label col-sm-12">
                    <asp:GridView ID="grdSearchResults" runat="server" AllowPaging="false" AllowCustomPaging="false" AutoGenerateColumns="False" DataKeyNames="Activity_Id" CssClass="table table-hover table-striped">
                        <Columns>
                            <asp:BoundField DataField="" HeaderText="Policy ID" />
                            <asp:BoundField DataField="" HeaderText="Product Type" />
                            <asp:BoundField DataField="" HeaderText="Policy Type" />
                            <asp:BoundField DataField="" HeaderText="Policy Category" />
                            <asp:BoundField DataField="" HeaderText="Policy Name" />
                            <asp:HyperLinkField Text="Edit" ControlStyle-CssClass="btn btn-default" />
                            <asp:HyperLinkField Text="Delete" ControlStyle-CssClass="btn btn-default" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</div>
