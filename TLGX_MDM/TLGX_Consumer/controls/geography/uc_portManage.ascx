<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_portManage.ascx.cs" Inherits="TLGX_Consumer.controls.geography.uc_portManage" %>

<%--<h1 class="page-header">Port Details</h1>--%>
 <div class="row page-header">
                <div class="col-md-8">
                    <h1>Port Details</h1>
                </div>
                <div class="col-md-4">
                    <div class="pull-right" style="margin-top: 25px !important;">
                        <asp:Button runat="server" ID="btnRedirectToSearch" OnClick="btnRedirectToSearch_Click" CssClass="btn btn-link" Text="Go Back to Port Search Page" />
                    </div>
                </div>
            </div>

<div class="col-lg-12">
    <asp:UpdatePanel runat="server" ID="upPnlPortManage">
    <ContentTemplate>
    <div class="panel panel-default">
        <div class="panel-heading">Port Information</div>
        <div class="panel-body">
            <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
            <div id="dvMsg" runat="server" style="display: none;"></div>
            <div class="container">
                <div class="row">
                    <div class="form-group">
                        <div class="col-sm-11">
                            <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="Port" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">
                <div class="col-lg-6">
                    <div class="form-group row">
                        <label for="txtoag_portname" class="col-md-4 col-form-label">
                            oag_portname
                            <asp:RequiredFieldValidator ValidationGroup="Port" runat="server" ControlToValidate="txtoag_portname"
                                CssClass="text-danger" ErrorMessage="The OAG Port name is required." Text="*" />
                        </label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtoag_portname" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" AssociatedControlID="txtOAG_loc" CssClass="col-md-4 control-label">OAG Loc
                                        <asp:RequiredFieldValidator ValidationGroup="Port" runat="server" ControlToValidate="txtOAG_loc"
                                                                                CssClass="text-danger" ErrorMessage="The OAG Loc field is required." Text="*" />
                        </asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtOAG_loc" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" AssociatedControlID="txtOAG_multicity" CssClass="col-md-4 control-label">OAG Multicity
                                       <%--<asp:RequiredFieldValidator ValidationGroup="Port" runat="server" ControlToValidate="txtOAG_multicity"
                                             CssClass="text-danger" ErrorMessage="The OAG Multicity field is required."  Text="*"/>--%>
                        </asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtOAG_multicity" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group row">
                        <label for="txtoag_inactive" class="col-md-4 control-label">oag_inactive</label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtoag_inactive" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txtMappingStatus" class="col-md-4 col-form-label">MappingStatus</label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtMappingStatus" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txtoag_ctryname" class="col-md-4 col-form-label">oag_ctryname</label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtoag_ctryname" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txtoag_state" class="col-md-4 col-form-label">oag_state</label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtoag_state" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txtoag_substate" class="col-md-4 col-form-label">oag_substate</label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtoag_substate" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txtoag_timediv" class="col-md-4 col-form-label">oag_timediv</label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtoag_timediv" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txtoag_lat" class="col-md-4 col-form-label">oag_lat</label>
                        <div class="col-md-8">
                              <asp:TextBox runat="server" ID="txtoag_lat" CssClass="form-control" />
                        </div>
                    </div>

                </div>
                <div class="col-lg-6">
                    <div class="form-group row">
                        <asp:Label runat="server" AssociatedControlID="ddlCountryEdit" CssClass="col-md-4 control-label">Country
                                <asp:RequiredFieldValidator ValidationGroup="Port" runat="server" ControlToValidate="ddlCountryEdit" CssClass="text-danger"
                                     ErrorMessage="The Country is required." InitialValue ="0"  Text="*"/>
                        </asp:Label>
                        <div class="col-md-8">
                            <asp:DropDownList runat="server" ID="ddlCountryEdit" CssClass="form-control">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" AssociatedControlID="ddlStateEdit" CssClass="col-md-4 control-label">State
                        </asp:Label>
                        <div class="col-md-8">
                            <asp:DropDownList runat="server" ID="ddlStateEdit" CssClass="form-control">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" AssociatedControlID="ddlCityEdit" CssClass="col-md-4 control-label">City
                                    <asp:RequiredFieldValidator ValidationGroup="Port" runat="server" ControlToValidate="ddlCityEdit"
                                      CssClass="text-danger" ErrorMessage="The City is required." InitialValue ="0"  Text="*"/>
                        </asp:Label>
                        <div class="col-md-8">
                            <asp:DropDownList runat="server" ID="ddlCityEdit" CssClass="form-control">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <%--<div class="form-group row">
                        <asp:Label runat="server" AssociatedControlID="ddlStatus" CssClass="col-md-4 control-label">Status
                                    <asp:RequiredFieldValidator ValidationGroup="Port" runat="server" ControlToValidate="ddlStatus"
                                          CssClass="text-danger" ErrorMessage="The status is required." InitialValue ="0"  Text="*"/>
                        </asp:Label>
                        <div class="col-md-8">
                            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                <asp:ListItem Value="0">--Select --</asp:ListItem>
                                <asp:ListItem Value="Active">Active</asp:ListItem>
                                <asp:ListItem Value="InActive">InActive</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>--%>
                    <div class="form-group row">
                        <div class="form-group">
                            <label for="txtOAG_typeC" class="col-md-4 control-label">
                                OAG Type
                                 <asp:RegularExpressionValidator Text="*" ControlToValidate="txtOAG_type" ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{0,1}$" runat="server"
                                     ValidationGroup="Port" ErrorMessage="Maximum 1 characters allowed for OAG Type."></asp:RegularExpressionValidator>
                            </label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtOAG_type" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="form-group">
                            <label for="txtOAG_subtype" class="col-md-4 control-label">
                                OAG_subtype
                                <asp:RegularExpressionValidator Text="*" ControlToValidate="txtOAG_subtype" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{0,1}$" runat="server"
                                    ValidationGroup="Port" ErrorMessage="Maximum 1 characters allowed for OAG Sub type."></asp:RegularExpressionValidator>
                            </label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtOAG_subtype" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txtoag_name" class="col-md-4 col-form-label">oag_name</label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtoag_name" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group row">
                        <label for="txtoag_ctry" class="col-md-4 col-form-label">oag_ctry</label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtoag_ctry" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txtoag_subctry" class="col-md-4 col-form-label">oag_subctry</label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtoag_subctry" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txtoag_lon" class="col-md-4 control-label">oag_lon</label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtoag_lon" CssClass="form-control" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row pull-right col-md-2">
                <asp:Button ID="btnEditPort" OnClick="btnEditPort_Click" CommandName="Add" runat="server" Text="Save" CssClass="btn btn-primary btn-md" ValidationGroup="Port" />
            </div>
        </div>
    </div>
        </ContentTemplate>
        </asp:UpdatePanel>
</div>
