<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Flavours.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.Flavours" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityContactDetails.ascx" TagPrefix="uc1" TagName="ActivityContactDetails" %>--%>
<%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityStatus.ascx" TagPrefix="uc1" TagName="ActivityStatus" %>

<script type="text/javascript">
    $(function () {
        $('[id*=lst]').multiselect({
            includeSelectAllOption: true
        });
    });

    //function pageLoad(sender, args) {
    //    debugger;
    //    $("#btnAddValue").click(function () {
    //        var Contain = "";
    //        $("#MainContent_Flavours_frmActivityFlavour_dvValueForFilter input[:selected]").each(function () {
    //            Contain += $(this).val() + ",";
    //        });
    //        $('#hdnValueWithCommaSeprated').val(Contain);
    //        $("#MainContent_Flavours_frmActivityFlavour_dvValueForFilter").append('<div class="con"><input id="MainContent_Flavours_frmActivityFlavour_ddlProductType" type="text" class="form-control col-md-8 inputTypeForFilter" /><div class="input-group-btn  col-md-4" style="padding-left: 0px !important;"><button class="btn btn-default btnRemove" id="btnAddValue" type="button"><i class="glyphicon glyphicon-minus"></i></button></div></div>');
    //    });
    //    $('body').on('click', '.btnRemove', function () {
    //        //debugger;
    //        $(this).parent('div').parent('div.con').remove()

    //    });

    //}
</script>


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
            <div class="col-lg-12 row">
                <div class="col-lg-12">
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
                            <div class="form-group">
                                <label class="control-label col-sm-4" for="txtShortDescription">
                                    Short Description
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtShortDescription" runat="server" Text="" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-4" for="txtLongDescription">
                                    Long Description
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtLongDescription" runat="server" TextMode="MultiLine" Text="" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12 row">
                <div class="col-lg-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">TLGX Classification Mapping</div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="up2" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="ddlCountry">
                                            Country
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="ddlCity">
                                            City
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="ddlProdCategory">
                                            Category
                                    <asp:RequiredFieldValidator ID="vddlProdCategory" runat="server" ErrorMessage="Please select Product Category" Text="*" ControlToValidate="ddlProdCategory" InitialValue="0" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlProdCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                                <asp:ListItem>Activity</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="ddlProdcategorySubType">
                                            Activity Category 
                                     <asp:RequiredFieldValidator ID="vddlProdcategorySubType" runat="server" ErrorMessage="Please select Activity Category" Text="*" ControlToValidate="ddlProdcategorySubType" InitialValue="0" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlProdcategorySubType" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlProdcategorySubType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="ddlProductType">
                                            Product Type
                                        </label>
                                        <div class="col-sm-6">

                                            <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>

                                            <%-- Button control to 'add' dropdown --%>
                                            <%--<asp:HiddenField runat="server" ID="hdnValueWithCommaSeprated" ClientIDMode="Static" />
                                            <div id="dvValueForFilter" runat="server" class="input-group col-md-12" style="display: none;">

                                                <input id="txtValueForFilter" runat="server" type="text" class="form-control col-md-8 inputTypeForFilter" />
                                                <div class="input-group-btn  col-md-4" style="padding-left: 0px !important;">
                                                    <button class="btn btn-default" id="btnAddValue" type="button">
                                                        <i class="glyphicon glyphicon-plus"></i>
                                                    </button>
                                                </div>
                                            </div>--%>

                                            <%--<asp:ListBox ID="lstboxProductType" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        <asp:Button ID="btnAddToListBox" runat="server" CssClass="btn btn-primary btn-sm" CommandName="FillListBox2" Text="v" />
                                        <asp:Button ID="btnRemoveFromListBox" runat="server" CssClass="btn btn-primary btn-sm" CommandName="EmptyListBox2" Text="^" />
                                        <asp:Literal ID="lblmsg2" runat="server" />
                                        <asp:ListBox ID="lstboxSelectedProductType" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>--%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="ddlProdNameSubType">
                                            Product SubType
                                    <asp:RequiredFieldValidator ID="vddlProdNameSubType" runat="server" ErrorMessage="Please select Product  Sub Type" Text="*" ControlToValidate="ddlProdNameSubType" InitialValue="0" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
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
                </div>
                <%-- Key Facts --%>
                <div class="col-lg-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">Key Facts</div>
                        <div class="panel-body">
                            <div class="form-group row">
                                <label class="control-label col-sm-6" for="chklstSuitableFor">Suitable For</label>
                                <div class="col-sm-6">
                                    <asp:CheckBoxList ID="chklstSuitableFor" runat="server" RepeatLayout="Table" RepeatColumns="2" CssClass="row"></asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="control-label col-sm-6" for="chklstPhysicalIntensity">Physical Intensity</label>
                                <div class="col-sm-6">
                                    <asp:CheckBoxList ID="chklstPhysicalIntensity" runat="server" RepeatLayout="Table" RepeatColumns="2" CssClass="row"></asp:CheckBoxList>
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
        </div>
    </EditItemTemplate>
</asp:FormView>



