<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="contacts.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.contacts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript">
    function TrimEmailText() {
        $("#MainContent_overview_frmHotelOverview_contacts_frmContactDetaiil_txtEmail").val($.trim($("#MainContent_overview_frmHotelOverview_contacts_frmContactDetaiil_txtEmail").val()));
    }
</script>
<asp:UpdatePanel ID="updPanContacts" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        <div class="form-group">
            <div id="dvMsgContact" runat="server" style="display: none;"></div>
        </div>

        <asp:FormView ID="frmContactDetaiil" runat="server" DataKeyNames="Accommodation_Contact_Id" DefaultMode="Insert" OnItemCommand="frmContactDetaiil_ItemCommand">

            <HeaderTemplate>
                <div class="form-group">
                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="HotelContacts" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                </div>
            </HeaderTemplate>

            <InsertItemTemplate>

                <div class="form-group row">
                    <div class="col-xs-2">
                        <br />
                        <label id="lblTel">Telephone</label>
                    </div>

                    <div class="col-xs-2">
                        <label class="control-label-mand" for="txtTelCountryCode">
                            Country
                            <asp:RequiredFieldValidator ID="vtxtTelCountryCode" runat="server" ErrorMessage="Please enter Tel Country Code" Text="*" ControlToValidate="txtTelCountryCode" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rvtxtTelCountryCode" runat="server" ErrorMessage="Invalid Tel Country Code" Text="*" ControlToValidate="txtTelCountryCode" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RegularExpressionValidator>
                        </label>
                        <asp:TextBox ID="txtTelCountryCode" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtTelCountryCode" />
                    </div>

                    <div class="col-xs-2">
                        <label class="control-label-mand" for="txtTelCityCode">
                            City
                            <asp:RequiredFieldValidator ID="vtxtTelCityCode" runat="server" ErrorMessage="Please enter Tel City Code" Text="*" ControlToValidate="txtTelCityCode" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rvtxtTelCityCode" runat="server" ErrorMessage="Invalid Tel City Code" Text="*" ControlToValidate="txtTelCityCode" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RegularExpressionValidator>
                        </label>
                        <asp:TextBox ID="txtTelCityCode" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtTelCityCode" />
                    </div>

                    <div class="col-xs-6">
                        <label class="control-label-mand" for="txtTelLocalNUmber">
                            Local
                            <asp:RequiredFieldValidator ID="vtxtTelLocalNUmber" runat="server" ErrorMessage="Please enter Tel Number" Text="*" ControlToValidate="txtTelLocalNUmber" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rvtxtTelLocalNUmber" runat="server" ErrorMessage="Invalid Tel Number" Text="*" ControlToValidate="txtTelLocalNUmber" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RegularExpressionValidator>
                        </label>
                        <asp:TextBox ID="txtTelLocalNUmber" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtTelLocalNUmber" />
                    </div>

                </div>

                <div class="form-group row">

                    <div class="col-xs-2">
                        <br />
                        <label id="lblFax">Fax</label>
                    </div>

                    <div class="col-xs-2">
                        <label for="txtFaxCountryCode">
                            Country
                            <%--<asp:RequiredFieldValidator ID="vtxtFaxCountryCode" runat="server" ErrorMessage="Please enter Fax Country Code" Text="*" ControlToValidate="txtFaxCountryCode" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="rvtxtFaxCountryCode" runat="server" ErrorMessage="Invalid Fax Country Code" Text="*" ControlToValidate="txtFaxCountryCode" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RegularExpressionValidator>
                        </label>
                        <asp:TextBox ID="txtFaxCountryCode" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtFaxCountryCode" />
                    </div>

                    <div class="col-xs-2">
                        <label for="txtFaxCityCode">
                            City
                            <%--<asp:RequiredFieldValidator ID="vtxtFaxCityCode" runat="server" ErrorMessage="Please enter Fax City Code" Text="*" ControlToValidate="txtFaxCityCode" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="rvtxtFaxCityCode" runat="server" ErrorMessage="Invalid Fax City Code" Text="*" ControlToValidate="txtFaxCityCode" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RegularExpressionValidator>
                        </label>
                        <asp:TextBox ID="txtFaxCityCode" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtFaxCityCode" />
                    </div>

                    <div class="col-xs-6">
                        <label for="txtFaxLocalNUmber">
                            Local
                            <%--<asp:RequiredFieldValidator ID="vtxtFaxLocalNUmber" runat="server" ErrorMessage="Please enter Fax Number" Text="*" ControlToValidate="txtFaxLocalNUmber" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="rvtxtFaxLocalNUmber" runat="server" ErrorMessage="Invalid Fax Number" Text="*" ControlToValidate="txtFaxLocalNUmber" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RegularExpressionValidator>
                        </label>
                        <asp:TextBox ID="txtFaxLocalNUmber" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers" TargetControlID="txtFaxLocalNUmber" />
                    </div>
                </div>

                <div class="form-group row">
                    <div class="col-md-10">
                        <form id="uriForm" class="form-horizontal">

                            <div class="form-group row">
                                <label class="control-label col-sm-2" for="txtWebsite">
                                    Website
                                </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" name="website"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="control-label col-sm-2" for="txtEmail">
                                    Email
                            <asp:RegularExpressionValidator ID="vtxtEmail" runat="server" ErrorMessage="Invalid Email" Text="*" ControlToValidate="txtEmail" CssClass="text-danger" ValidationGroup="HotelContacts" ValidationExpression="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$"></asp:RegularExpressionValidator>
                                </label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtEmail" runat="server" onkeyup="TrimEmailText()" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row" style="display: none;">
                                <label class="control-label col-sm-2" for="txtEmail">Hotel ID</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtLegacyHtlId" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div class="col-md-2">
                        <asp:LinkButton ID="btnAdd" runat="server" CommandName="Add" Text="Add New Contact" CssClass="btn btn-primary btn-sm" ValidationGroup="HotelContacts" CausesValidation="true" />

                    </div>
                </div>

            </InsertItemTemplate>

            <EditItemTemplate>

                <div class="form-group row">

                    <div class="col-xs-2">
                        <br />
                        <label id="lblTel">Telephone</label>
                    </div>

                    <div class="col-xs-2">
                        <label for="txtTelCountryCode">
                            Country
                            <asp:RequiredFieldValidator ID="vtxtTelCountryCode" runat="server" ErrorMessage="Please enter Tel Country Code" Text="*" ControlToValidate="txtTelCountryCode" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rvtxtTelCountryCode" runat="server" ErrorMessage="Invalid Tel Country Code" Text="*" ControlToValidate="txtTelCountryCode" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RegularExpressionValidator>
                        </label>
                        <asp:TextBox ID="txtTelCountryCode" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtTelCountryCode" />
                    </div>

                    <div class="col-xs-2">
                        <label for="txtTelCityCode">
                            City
                            <asp:RequiredFieldValidator ID="vtxtTelCityCode" runat="server" ErrorMessage="Please enter Tel City Code" Text="*" ControlToValidate="txtTelCityCode" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rvtxtTelCityCode" runat="server" ErrorMessage="Invalid Tel City Code" Text="*" ControlToValidate="txtTelCityCode" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RegularExpressionValidator>
                        </label>
                        <asp:TextBox ID="txtTelCityCode" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtTelCityCode" />
                    </div>

                    <div class="col-xs-6">
                        <label for="txtTelLocalNUmber">
                            Local
                            <asp:RequiredFieldValidator ID="vtxtTelLocalNUmber" runat="server" ErrorMessage="Please enter Tel Number" Text="*" ControlToValidate="txtTelLocalNUmber" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rvtxtTelLocalNUmber" runat="server" ErrorMessage="Invalid Tel Number" Text="*" ControlToValidate="txtTelLocalNUmber" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RegularExpressionValidator>
                        </label>
                        <asp:TextBox ID="txtTelLocalNUmber" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtTelLocalNUmber" />
                    </div>

                </div>

                <div class="form-group row">

                    <div class="col-xs-2">
                        <br />
                        <label id="lblFax">Fax</label>
                    </div>

                    <div class="col-xs-2">
                        <label for="txtFaxCountryCode">
                            Country
                            <%--<asp:RequiredFieldValidator ID="vtxtFaxCountryCode" runat="server" ErrorMessage="Please enter Fax Country Code" Text="*" ControlToValidate="txtFaxCountryCode" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="rvtxtFaxCountryCode" runat="server" ErrorMessage="Invalid Fax Country Code" Text="*" ControlToValidate="txtFaxCountryCode" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RegularExpressionValidator>
                        </label>
                        <asp:TextBox ID="txtFaxCountryCode" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtFaxCountryCode" />
                    </div>

                    <div class="col-xs-2">
                        <label for="txtFaxCityCode">
                            City
                            <%--<asp:RequiredFieldValidator ID="vtxtFaxCityCode" runat="server" ErrorMessage="Please enter Fax City Code" Text="*" ControlToValidate="txtFaxCityCode" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="rvtxtFaxCityCode" runat="server" ErrorMessage="Invalid Fax City Code" Text="*" ControlToValidate="txtFaxCityCode" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RegularExpressionValidator>
                        </label>
                        <asp:TextBox ID="txtFaxCityCode" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtFaxCityCode" />
                    </div>

                    <div class="col-xs-6">
                        <label for="txtFaxLocalNUmber">
                            Local
                            <%--<asp:RequiredFieldValidator ID="vtxtFaxLocalNUmber" runat="server" ErrorMessage="Please enter Fax Number" Text="*" ControlToValidate="txtFaxLocalNUmber" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="rvtxtFaxLocalNUmber" runat="server" ErrorMessage="Invalid Fax Number" Text="*" ControlToValidate="txtFaxLocalNUmber" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelContacts"></asp:RegularExpressionValidator>
                        </label>
                        <asp:TextBox ID="txtFaxLocalNUmber" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers" TargetControlID="txtFaxLocalNUmber" />
                    </div>
                </div>

                <form id="uriForm" class="form-horizontal">

                    <br />
                    <div class="form-group row">
                        <label class="control-label col-sm-2" for="txtWebsite">
                            Website
                        </label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" name="website"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="control-label col-sm-2" for="txtEmail">
                            Email
                            <asp:RegularExpressionValidator ID="vtxtEmail" runat="server" ErrorMessage="Invalid Email" Text="*" ControlToValidate="txtEmail" CssClass="text-danger" ValidationGroup="HotelContacts" ValidationExpression="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$"></asp:RegularExpressionValidator>

                        </label>

                        <div class="col-sm-10">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                    </div>
                    <div class="form-group row" style="visibility: hidden">
                        <label class="control-label col-sm-2" for="txtEmail">Hotel ID</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtLegacyHtlId" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                    </div>
                </form>
                <br />
                <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Edit" Text="Update" CssClass="btn btn-primary btn-sm" ValidationGroup="HotelContacts" CausesValidation="true" />

            </EditItemTemplate>

        </asp:FormView>

        <br />

        <asp:GridView ID="grdContactDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="Accommodation_Contact_Id" CssClass="table table-hover table-striped" EmptyDataText="No Contact details" OnRowCommand="grdContactDetails_RowCommand" OnRowDataBound="grdContactDetails_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Telephone" HeaderText="Telephone" SortExpression="Telephone" />
                <asp:BoundField DataField="fax" HeaderText="fax" SortExpression="fax" />
                <asp:BoundField DataField="WebSiteURL" HeaderText="WebSiteURL" SortExpression="WebSiteURL" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="Legacy_Htl_Id" HeaderText="Hotel ID" SortExpression="Legacy_Htl_Id" Visible="false" />
                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                            Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Accommodation_Contact_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                            CssClass="btn btn-default" CommandArgument='<%# Bind("Accommodation_Contact_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </ContentTemplate>
</asp:UpdatePanel>

<%--<script>
    $(document).ready(function () {
        $('#frmContactDetaiil').formValidation({
            framework: 'bootstrap',
            icon: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            fields: {
                website: {
                    validators: {
                        uri: {
                            message: 'The website address is not valid'
                        }
                    }
                }
            }
        });
    });
</script>--%>
