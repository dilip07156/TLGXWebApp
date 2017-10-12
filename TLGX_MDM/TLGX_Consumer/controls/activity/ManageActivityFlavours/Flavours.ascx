<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Flavours.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.Flavours" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityContactDetails.ascx" TagPrefix="uc1" TagName="ActivityContactDetails" %>
<%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityStatus.ascx" TagPrefix="uc1" TagName="ActivityStatus" %>

<div class="container">
    <div class="row">
        <div class="col-lg-12">
            <div id="dvMsg" runat="server" style="display: none;"></div>
        </div>
    </div>
</div>

<asp:FormView ID="frmActivityFlavour" runat="server" DataKeyNames="Activity_Flavour_Id" DefaultMode="Edit" OnItemCommand="frmActivityFlavour_ItemCommand">
    <HeaderTemplate>
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="ProductOverView" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                </div>
            </div>
        </div>
    </HeaderTemplate>

    <EditItemTemplate>
        <div class="row">
            <div class="col-lg-8">
                <div class="panel panel-default">
                    <div class="panel-heading">Product Name</div>
                    <div class="panel-body">
                        <div class="form-group" style="display: none">
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtActivity_Flavour_Id" runat="server" Text='<%# Bind("Activity_Flavour_Id") %>' class="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label-mand col-sm-4" for="txtProductName">
                                Product Name
                                    <asp:RequiredFieldValidator ID="vtxtProductName" runat="server" ErrorMessage="Please enter Hotel Name" Text="*" ControlToValidate="txtProductName" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                            </label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtProductName" runat="server" Text='<%# Bind("ProductName") %>' class="form-control" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="panel-heading">Address Info</div>
                    <div class="panel-body">

                        <div class="form-group">
                            <label class="control-label-mand col-sm-4" for="txtStreet">
                                Street
                                    <asp:RequiredFieldValidator ID="vtxtStreet" runat="server" ErrorMessage="Please enter Street" Text="*" ControlToValidate="txtStreet" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator></label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtStreet" runat="server" class="form-control" Text='<%# Bind("Street") %>' />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtStreet2">Street 2</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtStreet2" runat="server" class="form-control" Text='<%# Bind("Street2") %>' />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtStreet3">Street 3</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtStreet3" runat="server" class="form-control" Text='<%# Bind("Street3") %>' />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtStreet4">Street 4</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtStreet4" runat="server" class="form-control" Text='<%# Bind("Street4") %>' />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtStreet5">Street 5</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtStreet5" runat="server" class="form-control" Text='<%# Bind("Street5") %>' />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label-mand col-sm-4" for="txtPostalCode">
                                Postal Code
                                    <asp:RequiredFieldValidator ID="vtxtPostalCode" runat="server" ErrorMessage="Please enter Postal Code" Text="*" ControlToValidate="txtPostalCode" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator></label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtPostalCode" runat="server" class="form-control" Text='<%# Bind("PostalCode") %>' />
                            </div>
                        </div>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label class="control-label-mand col-sm-4" for="ddlCountry">
                                        Country
                                            <asp:RequiredFieldValidator ID="vddlCountry" runat="server" ErrorMessage="Please select Country" Text="*" ControlToValidate="ddlCountry" InitialValue="0" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label-mand col-sm-4" for="ddlCity">
                                        City
                                            <asp:RequiredFieldValidator ID="vddlCity" runat="server" ErrorMessage="Please select City" Text="*" ControlToValidate="ddlCity" InitialValue="0" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator></label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" >
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="ddlArea">
                                        Area
                                    </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlArea" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" >
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="ddlLocation">
                                        Location
                                    </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" AppendDataBoundItems="true" >
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>

                                        <br />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>

            <div class="col-lg-4">
                <div class="panel panel-default">
                    <div class="panel-heading">Classification Attributes</div>
                    <div class="panel-body">
                        <asp:UpdatePanel ID="up2" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label class="control-label col-sm-6" for="ddlProdCategory">
                                        Product Category
                                    <asp:RequiredFieldValidator ID="vddlProdCategory" runat="server" ErrorMessage="Please select Product Category" Text="*" ControlToValidate="ddlProdCategory" InitialValue="0" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlProdCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                            <asp:ListItem>Activity</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-6" for="ddlProdcategorySubType">
                                        Product Category SubType
                                     <asp:RequiredFieldValidator ID="vddlProdcategorySubType" runat="server" ErrorMessage="Please select Product Sub Category" Text="*" ControlToValidate="ddlProdcategorySubType" InitialValue="0" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlProdcategorySubType" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlProdcategorySubType_SelectedIndexChanged" AutoPostBack="true">
                                             <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-6" for="ddlProductType">
                                        Product Type
                                    <asp:RequiredFieldValidator ID="vddlProductType" runat="server" ErrorMessage="Please select Product Type" Text="*" ControlToValidate="ddlProductType" InitialValue="0" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-6" for="ddlProdNameSubType">
                                        ProductName SubType
                                    <asp:RequiredFieldValidator ID="vddlProdNameSubType" runat="server" ErrorMessage="Please select Product Name Sub Type" Text="*" ControlToValidate="ddlProdNameSubType" InitialValue="0" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlProdNameSubType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                             <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="panel-heading">Key Facts</div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="control-label col-sm-6" for="blnCompanyReccom">Company Reccom</label>
                            <div class="col-sm-6">
                                <asp:CheckBox ID="blnCompanyReccom" runat="server" Checked='<%# Bind("CompanyReccom") %>' />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-sm-6" for="blnMustSeeInCountry">MustSeeInCountry</label>
                            <div class="col-sm-6">
                                <asp:CheckBox ID="blnMustSeeInCountry" runat="server" Checked='<%# Bind("MustSeeInCountry") %>' />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="panel-heading">General Information</div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="control-label-mand col-sm-4" for="txtEventPlace">Place of Event</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtEventPlace" runat="server" Text='<%# Bind("PlaceOfEvent") %>' class="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label-mand col-sm-4" for="txtStartingPoint">Starting Point</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtStartingPoint" runat="server" Text='<%# Bind("StartingPoint") %>' class="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label-mand col-sm-4" for="txtEndingPoint">Ending Point</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtEndingPoint" runat="server" Text='<%# Bind("EndingPoint") %>' class="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label-mand col-sm-4" for="txtDuration">Duration</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtDuration" runat="server" Text='<%# Bind("Duration") %>' class="form-control" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="pull-left">
                    <asp:LinkButton ID="btnSave" runat="server" CausesValidation="True" CommandName="SaveProduct" Text="Update Flavour Info" CssClass="btn btn-primary btn-sm" ValidationGroup="ProductOverView" />
                    <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="False" CommandName="CancelProduct" Text="Cancel" CssClass="btn btn-primary btn-sm" />
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading">Contact Details</div>
                    <div class="panel-body">
                        <uc1:ActivityContactDetails runat="server" ID="ActivityContactDetails" />
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading">Status Details</div>
                    <div class="panel-body">
                        <uc1:ActivityStatus runat="server" ID="ActivityStatus" />
                    </div>
                </div>
            </div>
        </div>
    </EditItemTemplate>
</asp:FormView>



