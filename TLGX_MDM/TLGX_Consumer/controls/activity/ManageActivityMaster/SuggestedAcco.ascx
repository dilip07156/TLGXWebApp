<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuggestedAcco.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityMaster.SuggestedAcco" %>

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

                            <div class="panel-heading">Suggested Accomodation</div>

                            <div class="panel-body">

                                <div class="col-md-6">

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlProductCategorySubType">
                                            Product Category
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlProductCategoryType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlProductCategorySubType">
                                            Category Sub Type
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlProductCategorySubType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlCountry">Country</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true" >
                                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlCity">City</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group col-sm-12 row">
                                        <label class="control-label col-sm-6" for="txtProductName">Product Name</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                    <div class="form-group col-sm-12 row">
                                        <label class="control-label col-sm-6" for="txtDescription">
                                            Description
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12 row">
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
                            <asp:BoundField DataField="" HeaderText="Description" />
                            <asp:BoundField DataField="" HeaderText="Product Category" />
                            <asp:BoundField DataField="" HeaderText="Country" />
                            <asp:BoundField DataField="" HeaderText="Product Name" />
                            <asp:BoundField DataField="" HeaderText="Product Category Sub Type" />
                            <asp:BoundField DataField="" HeaderText="City" />
                            <asp:HyperLinkField Text="Edit" ControlStyle-CssClass="btn btn-default" />
                            <asp:HyperLinkField Text="Delete" ControlStyle-CssClass="btn btn-default" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</div>
