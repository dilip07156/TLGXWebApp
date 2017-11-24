<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Flavours.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.Flavours" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityContactDetails.ascx" TagPrefix="uc1" TagName="ActivityContactDetails" %>--%>
<%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityStatus.ascx" TagPrefix="uc1" TagName="ActivityStatus" %>

<style type="text/css">
    .TextWidth
    {
        width:281px;
        resize:none;
    }
</style>
<script type="text/javascript">
    $(function () {
        $('[id*=lst]').multiselect({
            includeSelectAllOption: true
        });
    });

    $(document).ready()
    {
        var countForProductType = 0;
        var countForProductSubType = 0;
    };

    //For Multiple dropdown of Product Type
    function GetDdlProductType(value) {
        countForProductType = countForProductType + 1;
        var div = $("<div />");

        var textBox = $("<select />").attr("id", "DynamicDdlProductType" + countForProductType).attr("class", "col-sm-8 form-control");
        textBox.val(value);
        div.append(textBox);

        var button = $("<input />").attr("type", "button").attr("value", "-").attr("style", "font-weight:bold;").attr("class", "btn btn-default");
        button.attr("onclick", "RemoveDdlProductType(this)");
        div.append(button);

        return div;
    }
    function AddDdlProductType() {
        var div = GetDdlProductType("");
        $("#DynamicControls").append(div);

        $('#DynamicDdlProductType' + countForProductType).html($('#MainContent_Flavours_frmActivityFlavour_ddlProductType').html());
    }
    function RemoveDdlProductType(button) {
        $(button).parent().remove();
    }
    $(function () {
        var values = eval('@Html.Raw(ViewBag.Values)');
        if (values != null) {
            $("#DynamicControls").html("");
            $(values).each(function () {
                $("#DynamicControls").append(GetDdlProductType(this));
            });
        }
    });

    //For Multiple dropdown of Product Sub Type
    function GetDdlProductSubType(value) {
        countForProductSubType = countForProductSubType + 1;
        var div = $("<div />");

        var textBox = $("<select />").attr("id", "DynamicDdlProductSubType" + countForProductSubType).attr("class", "col-sm-8 form-control");
        textBox.val(value);
        div.append(textBox);

        var button = $("<input />").attr("type", "button").attr("value", "-").attr("style", "font-weight:bold;").attr("class", "btn btn-default");
        button.attr("onclick", "RemoveDdlProductSubType(this)");
        div.append(button);

        return div;
    }
    function AddDdlProductSubType() {
        var div = GetDdlProductSubType("");
        $("#DynamicControls2").append(div);

        $('#DynamicDdlProductSubType' + countForProductSubType).html($('#MainContent_Flavours_frmActivityFlavour_ddlProdNameSubType').html());
    }
    function RemoveDdlProductSubType(button) {
        $(button).parent().remove();
    }
    $(function () {
        var values = eval('@Html.Raw(ViewBag.Values)');
        if (values != null) {
            $("#DynamicControls2").html("");
            $(values).each(function () {
                $("#DynamicControls2").append(GetDdlProductSubType(this));
            });
        }
    });
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
                            <div class="form-group row" style="display: none">
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtActivity_Flavour_Id" runat="server" Text='<%# Bind("Activity_Flavour_Id") %>' class="form-control" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="control-label-mand col-sm-4" for="txtProductName">
                                    Product Name
                                    <asp:RequiredFieldValidator ID="vtxtProductName" runat="server" ErrorMessage="Please enter Hotel Name" Text="*" ControlToValidate="txtProductName" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtProductName" runat="server" Text='<%# Bind("ProductName") %>' class="form-control" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="txtShortDescription">
                                    Short Description
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtShortDescription" runat="server" TextMode="MultiLine" CssClass="form-control TextWidth" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="txtLongDescription">
                                    Long Description
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtLongDescription" runat="server" TextMode="MultiLine" CssClass="form-control TextWidth" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12 row">
                <div class="col-lg-8">
                    <div class="panel panel-default">
                        <div class="panel-heading">TLGX Classification Mapping</div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="up2" runat="server">
                                <ContentTemplate>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-2" for="ddlCountry">
                                            Country
                                        </label>
                                        <asp:Label ID="lblSuppCountry" runat="server" class="control-label col-sm-2"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-2" for="ddlCity">
                                            City
                                        </label>
                                        <asp:Label ID="lblSuppCity" runat="server" class="control-label col-sm-2"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-2" for="ddlProdCategory">
                                            Category
                                    <asp:RequiredFieldValidator ID="vddlProdCategory" runat="server" ErrorMessage="Please select Product Category" Text="*" ControlToValidate="ddlProdCategory" InitialValue="0" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-2"></div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlProdCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                                <asp:ListItem>Activity</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-2" for="ddlProdcategorySubType">
                                            Activity Category 
                                     <asp:RequiredFieldValidator ID="vddlProdcategorySubType" runat="server" ErrorMessage="Please select Activity Category" Text="*" ControlToValidate="ddlProdcategorySubType" InitialValue="0" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-2"></div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlProdcategorySubType" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlProdcategorySubType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-2" for="ddlProductType">
                                            Product Type
                                        </label>
                                        <asp:Label ID="lblSuppProductType" runat="server" class="control-label col-sm-2"></asp:Label>
                                        <div class="col-sm-8">

                                            <asp:DropDownList ID="ddlProductType" runat="server" CssClass="col-sm-8 form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>

                                            <button class="btn btn-default" id="btnAdd" type="button" onclick="AddDdlProductType()">
                                                <i class="glyphicon glyphicon-plus"></i>
                                            </button>
                                            <br />
                                            <div id="DynamicControls">
                                                <!--Dropdowns will be added here -->
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-2" for="ddlProdNameSubType">
                                            Product SubType
                                    <asp:RequiredFieldValidator ID="vddlProdNameSubType" runat="server" ErrorMessage="Please select Product  Sub Type" Text="*" ControlToValidate="ddlProdNameSubType" InitialValue="0" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:Label ID="lblSuppProdNameSubType" runat="server" class="control-label col-sm-2"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlProdNameSubType" runat="server" CssClass="col-sm-8 form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                            <button class="btn btn-default" id="btnAddProdSubType" type="button" onclick="AddDdlProductSubType()">
                                                <i class="glyphicon glyphicon-plus"></i>
                                            </button>
                                            <br />
                                            <div id="DynamicControls2">
                                                <!--Dropdowns will be added here -->
                                            </div>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="panel panel-default">
                        <div class="panel-heading">Key Facts</div>
                        <div class="panel-body">
                            <div class="form-group row">
                                <div class="col-sm-6">
                                    <label class="control-label col-sm-4" for="chklstSuitableFor">Suitable For</label>
                                    <asp:Label ID="lblSuppSuitableFor" runat="server" class="control-label col-sm-2"></asp:Label>
                                </div>
                                <div class="col-sm-6" style="padding: 0px 10px 0px 5px;">
                                    <asp:CheckBoxList ID="chklstSuitableFor" runat="server" RepeatLayout="Table" RepeatColumns="2" CssClass="row"></asp:CheckBoxList>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-sm-6 row">
                                    <label class="control-label col-sm-4" for="chklstPhysicalIntensity">Physical Intensity</label>
                                    <asp:Label ID="lblSuppPhysicalIntensity" runat="server" class="control-label col-sm-2"></asp:Label>
                                </div>
                                <div class="col-sm-6 row" style="padding: 0px 10px 0px 5px;">
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

<%--<asp:ListBox ID="lstboxProductType" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
    <asp:Button ID="btnAddToListBox" runat="server" CssClass="btn btn-primary btn-sm" CommandName="FillListBox2" Text="v" />
    <asp:Button ID="btnRemoveFromListBox" runat="server" CssClass="btn btn-primary btn-sm" CommandName="EmptyListBox2" Text="^" />
    <asp:Literal ID="lblmsg2" runat="server" />
   <asp:ListBox ID="lstboxSelectedProductType" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>--%>


