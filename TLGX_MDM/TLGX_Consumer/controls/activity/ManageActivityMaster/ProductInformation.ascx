<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductInformation.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityMaster.ProductInformation" %>
<%@ Register Src="~/controls/activity/ManageActivityMaster/contacts.ascx" TagPrefix="uc1" TagName="contacts" %>
<%@ Register Src="~/controls/activity/ManageActivityMaster/GeneralInfo.ascx" TagPrefix="uc2" TagName="GeneralInfo" %>

<div class="row">
    <div class="container">
        <div class="col-lg-12 row">
            <div id="dvMsg" runat="server" style="display: none"></div>
        </div>
    </div>

    <asp:FormView ID="frmProductInfo" runat="server" DefaultMode="Edit" OnItemCommand="frmProductInfo_ItemCommand" DataKeyNames="Activity_Id">

        <HeaderTemplate>
            <div class="container">
                <div class="col-lg-12 row">
                    <asp:ValidationSummary ID="vlSum" runat="server" ValidationGroup="productInformation" DisplayMode="BulletList" ShowSummary="true" ShowMessageBox="false" CssClass="alert alert-danger" />
                </div>
            </div>
        </HeaderTemplate>

        <EditItemTemplate>
            <div class="container">
                <div class="row">

                    <div class="col-lg-8">
                        <div class="panel panel-default">

                            <div class="panel-heading">Name and Key Facts</div>

                            <div class="panel-body">

                                <div class="form-group row" style="display: none">
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtActivityID" runat="server" Text='<%# Bind("Activity_Id") %>' class="form-control" />
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-md-4" for="txtProductName">
                                        Product Name
                                    <%--<asp:RequiredFieldValidator ID="vtxtDisplayName" runat="server" ErrorMessage="Please enter Hotel Display Name" Text="*" ControlToValidate="txtDisplayName" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator>--%>
                                    </label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" />
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-md-4" for="txtDisplayName">
                                        Display Name
                                    </label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-default">

                            <div class="panel-heading">Address Details</div>
                            <div class="panel-body">

                                <div class="form-group row">
                                    <label class="control-label-mand col-sm-4" for="txtStreet">
                                        Street
                                    <asp:RequiredFieldValidator ID="vtxtStreet" runat="server" ErrorMessage="Please enter Street" Text="*" ControlToValidate="txtStreet" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtStreet" runat="server" class="form-control" />
                                        <%--Text='<%# Bind("StreetName") %>'--%>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label-mand col-sm-4" for="txtPostalCode">
                                        Postal Code
                                    <asp:RequiredFieldValidator ID="vtxtPostalCode" runat="server" ErrorMessage="Please enter Postal Code" Text="*" ControlToValidate="txtPostalCode" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtPostalCode" runat="server" class="form-control" /><%-- Text='<%# Bind("PostalCode") %>'--%>
                                    </div>
                                </div>

                                <asp:UpdatePanel ID="updDDLChange" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group row">
                                            <label class="control-label col-md-4" for="txtDisplayName">
                                                Country
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                                    <asp:ListItem Text="--select--" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label col-md-4" for="txtDisplayName">
                                                City
                                            </label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--select--" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div class="form-group row">
                                    <label class="control-label col-sm-4" for="btnAddressLookUp">
                                        &nbsp;
                                    </label>
                                    <div class="col-sm-8">
                                        <asp:LinkButton ID="btnAddressLookUp" runat="server" CausesValidation="True"
                                            CommandName="CheckAddress" Text="Check Address" CssClass="btn btn-primary btn-sm" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-heading">Contact Details</div>
                            <div class="panel-body">
                                <uc1:contacts runat="server" ID="contacts" />
                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-heading">General Information</div>
                            <div class="panel-body">
                                <uc2:GeneralInfo runat="server" ID="GeneralInfo" />
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4">

                        <div class="panel panel-default">
                            <div class="panel-heading">Classification</div>
                            <div class="panel-body">
                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="ddlProductCategory">
                                        Product Category
                                    <%--<asp:RequiredFieldValidator ID="vtxtHotelName" runat="server" ErrorMessage="Please enter Hotel Name" Text="*" ControlToValidate="txtHotelName" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator>--%>
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlProductCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="ddlProductCatSubType">
                                        Product Category Sub Type
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlProductCatSubType" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="ddlProductType">
                                        Product Type
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-heading">Key facts</div>
                            <div class="panel-body">
                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="ddlModeOfTransport">
                                        Mode Of Transport
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlModeOfTransport" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="ddlCompanyRating">
                                        Company Rating
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlCompanyRating" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="ddlProductRating">
                                        Product Rating
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlProductRating" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="ddlAffiliation">
                                        Affiliation
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlAffiliation" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-heading">System IDs</div>
                            <div class="panel-body">
                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="txtCommPID">
                                        Common Product ID
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtCommPID" runat="server" CssClass="form-control" Text='<%# Bind("CommonProductID") %>' />
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="txtCompanyProductID">
                                        Company Product ID
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtCompanyProductID" runat="server" CssClass="form-control" Text='<%# Bind("CompanyProductID") %>' />
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="txtFinanceControlID">
                                        Finance Control ID
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtFinanceControlID" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="True" CommandName="SaveProduct" Text="Update Product" CssClass="btn btn-primary btn-sm" ValidationGroup="HotelOverView" />
                        <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="False" CommandName="CancelProduct" Text="Cancel" CssClass="btn btn-primary btn-sm" />

                    </div>

                </div>
            </div>
        </EditItemTemplate>

    </asp:FormView>

    <div id="dvPageSize" runat="server" class="pull-right">
        <div class="col-sm-12">
            <div class="form-group">
                <div class="input-group">
                    <label class="input-group-addon" for="ddlPageSize"><strong>Page Size</strong></label>
                    <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control" AutoPostBack="true" Width="100px" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="25" Value="25" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>

    <div class="form-group">
        <div id="gvProductInfo" runat="server" class="control-label col-sm-12">
            <asp:GridView ID="grdSearchResults" runat="server" AllowPaging="false" AllowCustomPaging="false" AutoGenerateColumns="False" DataKeyNames="Activity_Id" CssClass="table table-hover table-striped">
                <Columns>
                    <asp:BoundField DataField="" HeaderText="From" />
                    <asp:BoundField DataField="" HeaderText="To" />
                    <asp:BoundField DataField="" HeaderText="Short Description" />
                    <asp:BoundField DataField="" HeaderText="Long Description" />
                    <asp:HyperLinkField Text="Edit" ControlStyle-CssClass="btn btn-default" />
                    <asp:HyperLinkField Text="Delete" ControlStyle-CssClass="btn btn-default" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</div>
