<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Flavours.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.Flavours" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityContactDetails.ascx" TagPrefix="uc1" TagName="ActivityContactDetails" %>--%>
<%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityStatus.ascx" TagPrefix="uc1" TagName="ActivityStatus" %>

<asp:UpdatePanel ID="upActivityFlavour" runat="server">
    <ContentTemplate>

        <div class="row">
            <div class="col-lg-12">
                <div id="dvMsg" runat="server" style="display: none;"></div>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="ProductOverView" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3>
                            <strong>
                                <asp:Label ID="lblProductName" runat="server"></asp:Label>
                            </strong>

                            <div class="pull-right">
                                <asp:LinkButton ID="btnSave" runat="server" CausesValidation="false" Text="Update Flavour Info" CssClass="btn btn-primary btn-sm" ValidationGroup="ProductOverView" OnClick="btnSave_Click" />
                            </div>
                        </h3>

                    </div>
                    <div class="panel-body">


                        <div class="col-lg-12 row">
                            <div class="col-lg-7">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Classification Mapping</div>
                                    <div class="panel-body">

                                        <div class="form-group row">
                                            <label class="control-label col-sm-2" for="txtProdCategory">
                                                Product Category
                                            </label>
                                            <em>
                                                <asp:Label ID="lblSuppProductCategory" runat="server" Text=" " class="control-label col-sm-2"></asp:Label></em>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtProdCategory" runat="server" Text="ACTIVITIES" CssClass="form-control" Enabled="false">
                                                </asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label col-sm-2" for="ddlProdcategorySubType">
                                                Product Sub Category 
                                            </label>
                                            <em>
                                                <asp:Label ID="lblSuppProductSubCategory" runat="server" Text="" class="control-label col-sm-2"></asp:Label></em>
                                            <div class="col-sm-8">

                                                <asp:DropDownList ID="ddlProdcategorySubType" runat="server" CssClass="col-sm-8 form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnAddSubCategory" OnClick="btnAddSubCategory_Click">
                                                    <i class="glyphicon glyphicon-plus"></i>
                                                </asp:LinkButton>

                                                <asp:Repeater ID="repSubCategory" runat="server" OnItemCommand="repSubCategory_ItemCommand">
                                                    <HeaderTemplate>
                                                        <table class="table table-bordered table-hover">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSubCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubCategory") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnRemoveSubCategory" CommandName="RemoveSubCategory" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SubCategory_Id") %>'>
                                                        <i class="glyphicon glyphicon-minus"></i>
                                                                </asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table> 
                                                    </FooterTemplate>
                                                </asp:Repeater>

                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label col-sm-2" for="ddlProductType">
                                                Product Name Type
                                            </label>
                                            <em>
                                                <asp:Label ID="lblSuppProductType" runat="server" class="control-label col-sm-2"></asp:Label></em>
                                            <div class="col-sm-8">

                                                <asp:DropDownList ID="ddlProductType" runat="server" CssClass="col-sm-8 form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnAddProductType" OnClick="btnAddProductType_Click">
                                                        <i class="glyphicon glyphicon-plus"></i>
                                                </asp:LinkButton>

                                                <asp:Repeater ID="repProductType" runat="server" OnItemCommand="repProductType_ItemCommand">
                                                    <HeaderTemplate>
                                                        <table class="table table-bordered table-hover">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblProductType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnRemoveProductType" CommandName="RemoveProductType" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductType_Id") %>'>
                                                        <i class="glyphicon glyphicon-minus"></i>
                                                                </asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table> 
                                                    </FooterTemplate>
                                                </asp:Repeater>

                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label col-sm-2" for="ddlProdNameSubType">
                                                Product Name SubType
                                            </label>
                                            <em>
                                                <asp:Label ID="lblSuppProdNameSubType" runat="server" class="control-label col-sm-2"></asp:Label></em>
                                            <div class="col-sm-8">

                                                <asp:DropDownList ID="ddlProdNameSubType" runat="server" CssClass="col-sm-8 form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnAddProductSubType" OnClick="btnAddProductSubType_Click">
                                         <i class="glyphicon glyphicon-plus"></i>
                                                </asp:LinkButton>

                                                <asp:Repeater ID="repProductSubType" runat="server" OnItemCommand="repProductSubType_ItemCommand">
                                                    <HeaderTemplate>
                                                        <table class="table table-bordered table-hover">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblProductSubType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductSubType") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnRemoveProductSubType" CommandName="RemoveProductSubType" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductSubType_Id") %>'>
                                                        <i class="glyphicon glyphicon-minus"></i>
                                                                </asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table> 
                                                    </FooterTemplate>
                                                </asp:Repeater>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-5">

                                <div class="panel panel-default">
                                    <div class="panel-heading">Geography</div>
                                    <div class="panel-body">
                                        <div class="form-group row">
                                            <label class="control-label col-sm-2" for="ddlCountry">
                                                Country
                                            </label>
                                            <em>
                                                <asp:Label ID="lblSuppCountry" runat="server" class="control-label col-sm-2"></asp:Label></em>
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
                                            <em>
                                                <asp:Label ID="lblSuppCity" runat="server" class="control-label col-sm-2"></asp:Label></em>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel panel-default">
                                    <div class="panel-heading">Key Facts</div>
                                    <div class="panel-body">
                                        <div class="form-group row">
                                            <div class="col-sm-6 row">
                                                <label class="control-label col-sm-4" for="chklstSuitableFor">Suitable For</label>
                                                <em>
                                                    <asp:Label ID="lblSuppSuitableFor" runat="server" class="control-label col-sm-2"></asp:Label></em>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:CheckBoxList ID="chklstSuitableFor" runat="server"></asp:CheckBoxList>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-sm-6 row">
                                                <label class="control-label col-sm-4" for="chklstPhysicalIntensity">Physical Intensity</label>
                                                <em>
                                                    <asp:Label ID="lblSuppPhysicalIntensity" runat="server" class="control-label col-sm-2"></asp:Label></em>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlPhysicalIntensity" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="col-lg-12 row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Operating Days & Days of Week</div>
                                    <div class="panel-body">

                                        <div class="form-group row">
                                            <asp:Repeater ID="repSupplierInformation" runat="server">
                                                <HeaderTemplate>
                                                    <button type="button" class="btn btn-link" data-toggle="collapse" data-target="#dvSupplierInfo">Supplier Level Info</button>
                                                    <div id="dvSupplierInfo" class="collapse">
                                                        <table class="table table-hover table-striped">
                                                            <tr>
                                                                <th>Type</th>
                                                                <th>SubType</th>
                                                                <th>Value</th>
                                                            </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <strong><%# DataBinder.Eval(Container.DataItem, "AttributeType") %></strong>
                                                        </td>
                                                        <td>
                                                            <em><%# DataBinder.Eval(Container.DataItem, "AttributeSubType") %></em>
                                                        </td>
                                                        <td>
                                                            <em><%# DataBinder.Eval(Container.DataItem, "AttributeValue") %></em>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>

                                        <div class="form-group row">
                                            <asp:Repeater ID="repOperatingDays" runat="server" OnItemCommand="repOperatingDays_ItemCommand" OnItemDataBound="repOperatingDays_ItemDataBound">

                                                <HeaderTemplate>

                                                    <div class="form-group">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading">
                                                                <div class="form-group row">
                                                                    <strong>Add Operating Days</strong>
                                                                </div>

                                                                <div class="form-group row">

                                                                    <div class="col-sm-3">
                                                                        <label class="control-label col-sm-6" for="chkSpecificOperatingDays">Specific Operating Days</label>
                                                                        <asp:CheckBox ID="chkSpecificOperatingDays" runat="server" CssClass="col-sm-6" />
                                                                    </div>

                                                                    <div class="col-sm-4">
                                                                        <label class="control-label col-sm-6" for="txtFrom">
                                                                            From Date
                                                                        </label>
                                                                        <div class="col-sm-6">
                                                                            <div class="input-group">
                                                                                <asp:TextBox ID="txtFromAdd" runat="server" CssClass="form-control" />
                                                                                <span class="input-group-btn">
                                                                                    <button class="btn btn-default" type="button" id="iCalFromAdd">
                                                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                                                    </button>
                                                                                </span>
                                                                            </div>
                                                                            <cc1:CalendarExtender ID="calFromDateAdd" runat="server" TargetControlID="txtFromAdd" Format="dd/MM/yyyy" PopupButtonID="iCalFromAdd"></cc1:CalendarExtender>
                                                                            <cc1:FilteredTextBoxExtender ID="axfte_txtFromAdd" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFromAdd" />

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-4">
                                                                        <label class="control-label col-sm-6" for="txtTo">
                                                                            To Date
                                                                        </label>
                                                                        <div class="col-sm-6">
                                                                            <div class="input-group">
                                                                                <asp:TextBox ID="txtToAdd" runat="server" CssClass="form-control" />
                                                                                <span class="input-group-btn">
                                                                                    <button class="btn btn-default" type="button" id="iCalToAdd">
                                                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                                                    </button>
                                                                                </span>
                                                                            </div>
                                                                            <cc1:CalendarExtender ID="calToDateAdd" runat="server" TargetControlID="txtToAdd" Format="dd/MM/yyyy" PopupButtonID="iCalToAdd"></cc1:CalendarExtender>
                                                                            <cc1:FilteredTextBoxExtender ID="axfte_txtToAdd" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtToAdd" />
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-1">
                                                                        <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnAddOperatingDays" CommandName="AddOperatingDays">
                                                    <i class="glyphicon glyphicon-plus"></i>
                                                                        </asp:LinkButton>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <div class="panel-body">
                                                </HeaderTemplate>

                                                <ItemTemplate>

                                                    <div class="form-group row">

                                                        <div class="col-sm-3">
                                                            <label class="control-label col-sm-6" for="chkSpecificOperatingDays">Specific Operating Days</label>
                                                            <asp:CheckBox ID="chkSpecificOperatingDays" runat="server" CssClass="col-sm-6" Checked='<%# DataBinder.Eval(Container.DataItem, "IsOperatingDays") %>' />
                                                        </div>

                                                        <div class="col-sm-4">
                                                            <label class="control-label col-sm-6" for="txtFrom">
                                                                From Date
                                                            </label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "FromDate", "{0:dd/MM/yyyy}") %>' />
                                                                    <span class="input-group-btn">
                                                                        <button class="btn btn-default" type="button" id="iCalFrom" runat="server">
                                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                                <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iCalFrom"></cc1:CalendarExtender>
                                                                <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFrom" />

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-4">
                                                            <label class="control-label col-sm-6" for="txtTo">
                                                                To Date
                                                            </label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "EndDate", "{0:dd/MM/yyyy}") %>' />
                                                                    <span class="input-group-btn">
                                                                        <button class="btn btn-default" type="button" id="iCalTo" runat="server">
                                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                                <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" PopupButtonID="iCalTo"></cc1:CalendarExtender>
                                                                <cc1:FilteredTextBoxExtender ID="axfte_txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtTo" />
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-1">
                                                            <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnRemoveOperatingDays" CommandName="RemoveOperatingDays" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Activity_DaysOfOperation_Id") %>'>
                                                                <i class="glyphicon glyphicon-minus"></i>
                                                            </asp:LinkButton>
                                                        </div>

                                                    </div>

                                                    <div class="form-group row">

                                                        <asp:Repeater ID="repDaysOfWeek" runat="server" DataSource='<%# DataBinder.Eval(Container.DataItem, "DaysOfWeek") %>' OnItemCommand="repDaysOfWeek_ItemCommand" OnItemDataBound="repDaysOfWeek_ItemDataBound">
                                                            <HeaderTemplate>
                                                                <div class="form-group col-sm-11">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel-heading">

                                                                            <div class="form-group row">
                                                                                <strong>Add Week Days</strong>
                                                                            </div>

                                                                            <div class="form-group row">

                                                                                <div class="col-xs-2">
                                                                                    <label class="control-label-mand" for="txtStartTime">
                                                                                        Start Time (HH:mm)
                                                         <%--<asp:RequiredFieldValidator ID="vtxtStartTime" runat="server" ErrorMessage="Please enter Start Time" Text="*" ControlToValidate="txtStartTime" CssClass="text-danger" ValidationGroup=""></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revtxtCheckinTime" runat="server" ErrorMessage="Invalid Start Time." Text="*" ControlToValidate="txtStartTime" CssClass="text-danger" ValidationGroup="" ValidationExpression="^(?:[01][0-9]|2[0-3]):[0-5][0-9]$"></asp:RegularExpressionValidator>--%>
                                                                                    </label>
                                                                                    <asp:TextBox ID="txtStartTime" runat="server" Text='' CssClass="form-control"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="txtStartTime_MaskEditExtender" runat="server" AcceptAMPM="false"
                                                                                        Mask="99:99" MaskType="Time" PromptCharacter="_" TargetControlID="txtStartTime"
                                                                                        UserTimeFormat="TwentyFourHour" InputDirection="LeftToRight"></cc1:MaskedEditExtender>
                                                                                </div>

                                                                                <div class="col-xs-3">
                                                                                    <label class="control-label-mand" for="txtDuration">
                                                                                        Duration (d.HH:mm)
                                                        <%--<asp:RequiredFieldValidator ID="vtxtDuration" runat="server" ErrorMessage="Please Duaration" Text="*" ControlToValidate="txtDuration" CssClass="text-danger" ValidationGroup=""></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revtxtDuration" runat="server" ErrorMessage="Invalid Duration." Text="*" ControlToValidate="txtDuration" CssClass="text-danger" ValidationGroup="" ValidationExpression="^(?:[01][0-9]|2[0-3]):[0-5][0-9]$"></asp:RegularExpressionValidator>--%>
                                                                                    </label>
                                                                                    <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="txtDuration_MaskEditExtender" runat="server" AcceptAMPM="false"
                                                                                        Mask="9.99:99" MaskType="None" PromptCharacter="_" TargetControlID="txtDuration"
                                                                                        UserTimeFormat="None" InputDirection="LeftToRight"></cc1:MaskedEditExtender>
                                                                                </div>

                                                                                <div class="col-xs-3">
                                                                                    <label class="control-label-mand" for="txtSession">
                                                                                        Session
                                                                                    </label>
                                                                                    <%--<asp:TextBox ID="txtSession" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtSession" />--%>
                                                                                    <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                </div>

                                                                                <div class="col-xs-3">
                                                                                    <label class="control-label-mand" for="txtSession">
                                                                                        Applicable On
                                                                                    </label>
                                                                                    <%--<asp:TextBox ID="txtSession" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtSession" />--%>
                                                                                    <div>
                                                                                        <label class="control-label">
                                                                                            M
                                                                <div>
                                                                    <input type="checkbox" id="chkMon" runat="server" name="Monday">
                                                                </div>
                                                                                        </label>
                                                                                        <label class="control-label">
                                                                                            T
                                                                <div>
                                                                    <input type="checkbox" id="chkTues" runat="server" name="Tuesday">
                                                                </div>
                                                                                        </label>
                                                                                        <label class="control-label">
                                                                                            W
                                                                <div>
                                                                    <input type="checkbox" id="chkWed" runat="server" name="Wednesday">
                                                                </div>
                                                                                        </label>
                                                                                        <label class="control-label">
                                                                                            TH
                                                                <div>
                                                                    <input type="checkbox" id="chkThurs" runat="server" name="Thursday">
                                                                </div>
                                                                                        </label>
                                                                                        <label class="control-label">
                                                                                            F
                                                                <div>
                                                                    <input type="checkbox" id="chkFri" runat="server" name="Friday">
                                                                </div>
                                                                                        </label>
                                                                                        <label class="control-label">
                                                                                            S
                                                                <div>
                                                                    <input type="checkbox" id="chkSat" runat="server" name="Saturday">
                                                                </div>
                                                                                        </label>
                                                                                        <label class="control-label">
                                                                                            SU
                                                                <div>
                                                                    <input type="checkbox" id="chkSun" runat="server" name="Sunday">
                                                                </div>
                                                                                        </label>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-xs-1">
                                                                                    <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnAddDaysOfWeek" CommandName="AddDaysOfWeek">
                                                                        <i class="glyphicon glyphicon-plus"></i>
                                                                                    </asp:LinkButton>
                                                                                </div>

                                                                            </div>

                                                                        </div>
                                                                        <div class="panel-body">
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="form-group row">

                                                                    <div class="col-xs-2">
                                                                        <label class="control-label-mand" for="txtStartTime">
                                                                            Start Time
                                                     <%--<asp:RequiredFieldValidator ID="vtxtStartTime" runat="server" ErrorMessage="Please enter Start Time" Text="*" ControlToValidate="txtStartTime" CssClass="text-danger" ValidationGroup=""></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revtxtCheckinTime" runat="server" ErrorMessage="Invalid Start Time." Text="*" ControlToValidate="txtStartTime" CssClass="text-danger" ValidationGroup="" ValidationExpression="^(?:[01][0-9]|2[0-3]):[0-5][0-9]$"></asp:RegularExpressionValidator>--%>
                                                                        </label>
                                                                        <em>&nbsp;(<asp:Label ID="lblSupplierStartTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierStartTime") %>'></asp:Label>)</em>
                                                                        <asp:TextBox ID="txtStartTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartTime") %>' CssClass="form-control"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="txtStartTime_MaskEditExtender" runat="server" AcceptAMPM="false"
                                                                            Mask="99:99" MaskType="Time" PromptCharacter="_" TargetControlID="txtStartTime"
                                                                            UserTimeFormat="TwentyFourHour" InputDirection="LeftToRight"></cc1:MaskedEditExtender>
                                                                    </div>

                                                                    <div class="col-xs-3">
                                                                        <label class="control-label-mand" for="txtDuration">
                                                                            Duration
                                                    <%--<asp:RequiredFieldValidator ID="vtxtDuration" runat="server" ErrorMessage="Please Duaration" Text="*" ControlToValidate="txtDuration" CssClass="text-danger" ValidationGroup=""></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revtxtDuration" runat="server" ErrorMessage="Invalid Duration." Text="*" ControlToValidate="txtDuration" CssClass="text-danger" ValidationGroup="" ValidationExpression="^(?:[01][0-9]|2[0-3]):[0-5][0-9]$"></asp:RegularExpressionValidator>--%>
                                                                        </label>
                                                                        <em>&nbsp;(<asp:Label ID="lblSupplierDuration" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierDuration") %>'></asp:Label>)</em>
                                                                        <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "Duration") %>'></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="txtDuration_MaskEditExtender" runat="server" AcceptAMPM="false"
                                                                            Mask="9.99:99" MaskType="None" PromptCharacter="_" TargetControlID="txtDuration"
                                                                            UserTimeFormat="None" InputDirection="LeftToRight"></cc1:MaskedEditExtender>
                                                                    </div>

                                                                    <div class="col-xs-3">
                                                                        <label class="control-label-mand" for="txtSession">
                                                                            Session
                                                                        </label>
                                                                        <em>&nbsp;(<asp:Label ID="lblSupplierSession" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierSession") %>'></asp:Label>)</em>
                                                                        <asp:HiddenField ID="hdnSession" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Session") %>' />
                                                                        <%--<asp:TextBox ID="txtSession" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtSession" />--%>
                                                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="col-xs-3">
                                                                        <label class="control-label-mand" for="txtSession">
                                                                            Applicable On
                                                                        </label>
                                                                        <%--<asp:TextBox ID="txtSession" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtSession" />--%>
                                                                        <em>&nbsp;(<asp:Label ID="lblSupplierFrequency" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierFrequency") %>'></asp:Label>)</em>
                                                                        <div>
                                                                            <label class="control-label">
                                                                                M
                                                            <div>
                                                                <input type="checkbox" id="chkMon" runat="server" name="Monday" checked='<%# DataBinder.Eval(Container.DataItem, "Mon") %>'>
                                                            </div>
                                                                            </label>
                                                                            <label class="control-label">
                                                                                T
                                                            <div>
                                                                <input type="checkbox" id="chkTues" runat="server" name="Tuesday" checked='<%# DataBinder.Eval(Container.DataItem, "Tues") %>'>
                                                            </div>
                                                                            </label>
                                                                            <label class="control-label">
                                                                                W
                                                            <div>
                                                                <input type="checkbox" id="chkWed" runat="server" name="Wednesday" checked='<%# DataBinder.Eval(Container.DataItem, "Wed") %>'>
                                                            </div>
                                                                            </label>
                                                                            <label class="control-label">
                                                                                TH
                                                            <div>
                                                                <input type="checkbox" id="chkThurs" runat="server" name="Thursday" checked='<%# DataBinder.Eval(Container.DataItem, "Thur") %>'>
                                                            </div>
                                                                            </label>
                                                                            <label class="control-label">
                                                                                F
                                                            <div>
                                                                <input type="checkbox" id="chkFri" runat="server" name="Friday" checked='<%# DataBinder.Eval(Container.DataItem, "Fri") %>'>
                                                            </div>
                                                                            </label>
                                                                            <label class="control-label">
                                                                                S
                                                            <div>
                                                                <input type="checkbox" id="chkSat" runat="server" name="Saturday" checked='<%# DataBinder.Eval(Container.DataItem, "Sat") %>'>
                                                            </div>
                                                                            </label>
                                                                            <label class="control-label">
                                                                                SU
                                                            <div>
                                                                <input type="checkbox" id="chkSun" runat="server" name="Sunday" checked='<%# DataBinder.Eval(Container.DataItem, "Sun") %>'>
                                                            </div>
                                                                            </label>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-xs-1">
                                                                        <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnRemoveDaysOfWeek" CommandName="RemoveDaysOfWeek" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Activity_DaysOfWeek_ID") %>'>
                                                            <i class="glyphicon glyphicon-minus"></i>
                                                                        </asp:LinkButton>
                                                                    </div>

                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </div>
                                                </div>
                                            </div>
                                                            </FooterTemplate>
                                                        </asp:Repeater>

                                                        <div class="form-group col-sm-1"></div>
                                                    </div>

                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    </div>
                                                        </div>
                                                    </div>
                                                </FooterTemplate>

                                            </asp:Repeater>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
