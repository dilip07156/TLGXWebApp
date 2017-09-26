<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OtherInformation.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityMaster.OtherInformation" %>


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

                            <div class="panel-heading">Other Information</div>

                            <div class="panel-body">

                                <div class="col-md-6">

                                    <div class="form-group row">
                                        <label class="control-label col-md-4" for="txtDisplayName">
                                           Type of Information
                                        </label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtMerchandiseType" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-md-4" for="txtDisplayName">
                                           Name of Information
                                        </label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtShopName" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group row">
                                        <label class="control-label col-md-4" for="txtDisplayName">
                                           Description
                                        </label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtDescription" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <asp:LinkButton ID="btnSave" runat="server" CommandName="SaveProduct" Text="Add" CssClass="btn btn-primary btn-sm pull-right" />
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
                            <asp:BoundField DataField="" HeaderText="Type of Information" />
                            <asp:BoundField DataField="" HeaderText="Name of Information" />
                            <asp:BoundField DataField="" HeaderText="Description" />
                            <asp:HyperLinkField Text="Edit" ControlStyle-CssClass="btn btn-default" />
                            <asp:HyperLinkField Text="Delete" ControlStyle-CssClass="btn btn-default" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</div>