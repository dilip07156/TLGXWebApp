<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchCountryMap.ascx.cs" Inherits="TLGX_Consumer.controls.staticdata.CountryMap" %>
<style type="text/css">
    .modalPopup {
        background-color: #696969;
        filter: alpha(opacity=40);
        opacity: 0.7;
        xindex: -1;
    }

    .progress, .alert {
        margin: 15px;
    }

    .alert {
        display: none;
    }

    .hideColumn {
        display: none;
    }
</style>
<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);
    function showCountryMappingModal() {
        $("#moCountryMapping").modal('show');
    }
    function closeCountryMappingModal() {
        $("#moCountryMapping").modal('hide');
    }
    function pageLoad(sender, args) {
        var hv = $('#MainContent_searchCountryMap_hdnFlag').val();
        //alert($('.alert-dismissable').css('display'));
        //if (!isNaN($('.alert-dismissable').css('display')))
        //{ $('.alert-dismissable').css('display', 'none'); }
        if (hv == "true") {
            closeCountryMappingModal();
            $('#MainContent_searchCountryMap_hdnFlag').val("false");
        }
    }

    function SelectedRow(element) {
        var ddlStatus = $('#MainContent_searchCountryMap_ddlStatus option:selected').html();
        if (ddlStatus == "REVIEW") {
            element.parentNode.parentNode.nextSibling.childNodes[9].lastElementChild.focus();
        }
        else if (ddlStatus == "UNMAPPED") {
            element.parentNode.parentNode.nextSibling.childNodes[6].lastElementChild.focus();
        }
    }
    function MatchedSelect(elem) {
        elem.parentNode.parentNode.nextSibling.childNodes[8].lastElementChild.focus();
    }
    //Fill City dropdown in Grid
    function fillDropDown(record, onClick) {
        if (onClick) {
            //Getting Dropdown
            var currentRow = $(record).parent().parent();
            var CountryDDL = currentRow.find("td:eq(5)").find('select');
            var selectedText = CountryDDL.find("option:selected").text();
            var selectedOption = CountryDDL.find("option");
            var selectedVal = CountryDDL.val();
            if (CountryDDL != null && CountryDDL.is("select")) {
                $.ajax({
                    url: '../../../Service/ToFillDDL.ashx',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: { 'EntityType': 'country' },
                    responseType: "json",
                    success: function (result) {
                        CountryDDL.find("option:not(:first)").remove();
                        var value = JSON.stringify(result);
                        var listItems = '';
                        if (result != null) {
                            for (var i = 0; i < result.length; i++) {
                                listItems += "<option value='" + result[i].Country_ID + "'>" + result[i].NameWithCode + "</option>";
                            }
                            CountryDDL.append(listItems);
                        }

                        CountryDDL.find("option").prop('selected', false).filter(function () {
                            return $(this).text() == selectedText;
                        }).attr("selected", "selected");
                    },
                    failure: function () {
                    }
                });
            }
        }

    }
    function RemoveExtra(record, onClick) {
        //debugger;
        if (!onClick) {
            var currentRow = $(record).parent().parent();
            var CityDDL = currentRow.find("td:eq(5)").find('select');
            var selectedText = CityDDL.find("option:selected").text();
            var selectedVal = CityDDL.val();
            CityDDL.find("option:not(:first)").remove();
            var listItems = "<option selected = 'selected' value='" + selectedVal + "'>" + selectedText + "</option>";
            CityDDL.append(listItems);
            var country_id = record.parentNode.parentNode.childNodes[10].firstElementChild;
            country_id.value = selectedVal;
        }
    }

    function ddlStatusChanged(ddl) {
        //debugger;
        var ddlStatus = $('#MainContent_searchCountryMap_frmEditCountryMap_ddlStatus option:selected').html();
        var myVal = document.getElementById("MainContent_searchCountryMap_frmEditCountryMap_vddlSystemCountryName");
        //var myVal = $('#vddlSystemCountryName').val();
        if (ddlStatus == 'DELETE') {
            ValidatorEnable(myVal, false);
        }
        else {
            ValidatorEnable(myVal, true);
        }
    }
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Country Mapping Search</a>
                    </h4>
                </div>

                <div id="collapseSearch" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div class="container">
                            <div class="col-sm-6">
                                <div class="form-group row">
                                    <label class="control-label col-sm-4" for="ddlSupplierName">Supplier Name</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-sm-4" for="ddlMasterCountry">System Country</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlMasterCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-sm-4" for="ddlStatus">Mapping Status</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-sm-4" for="txtSuppName">Supplier Country Name</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtSuppCountry" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-sm-6">
                                <div class="form-group row">
                                    <label class="control-label col-sm-4" for="ddlShowEntries">Entries</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlShowEntries" runat="server" CssClass="form-control">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" OnClick="btnReset_Click" />
                                    </div>
                                    <div class="col-sm-12">&nbsp; </div>
                                </div>
                                <%-- <div class="form-group">
                                        <div class="col-sm-12">
                                        </div>
                                    </div>--%>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <div class="panel-group" id="accordionResult">
            <div class="panel panel-default">
                <div class="panel-heading clearfix">
                    <h4 class="panel-title pull-left">
                        <a data-toggle="collapse" data-parent="#accordionResult" href="#collapseSearchResult">Search Results (Total Count:<asp:Label ID="lblTotalCount" runat="server" Text="0"></asp:Label>)</a>
                    </h4>

                    <div class="form-group pull-right">
                        <asp:Button ID="btnMapSelected" runat="server" CssClass="btn btn-primary btn-sm" Text="Map Selected" OnClick="btnMapSelected_Click" />
                        <asp:Button ID="btnMapAll" runat="server" CssClass="btn btn-primary btn-sm" Text="Map All" OnClick="btnMapAll_Click" />
                    </div>
                </div>

                <div id="collapseSearchResult" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <!-- search results need to be wired in -->




                        <div class="form-group">
                            <div id="dvMsg1" runat="server" style="display: none;"></div>
                        </div>
                        <!-- need to override the default page size -->
                        <!-- ON ROW COMMAND select will populate 
    -- > frmEditCountryMap for APPROVAL or MAPPED status 
    -- > frmAddSupplierCountryMapping for MANUAL MAPPING 
 -->
                        <asp:GridView ID="grdCountryMaps" runat="server" AllowPaging="True" AutoGenerateColumns="False" AllowCustomPaging="True"
                            EmptyDataText="No Mappings for search conditions" CssClass="table table-hover table-striped" PagerSettings-Position="TopAndBottom"
                            DataKeyNames="CountryMapping_ID,Supplier_Id,Country_Id,MasterCountry_Id,MasterNameWithCode" OnSelectedIndexChanged="grdCountryMaps_SelectedIndexChanged" OnPageIndexChanging="grdCountryMaps_PageIndexChanging"
                            OnRowCommand="grdCountryMaps_RowCommand" OnSorting="grdCountryMaps_Sorting" OnDataBound="grdCountryMaps_DataBound"
                            OnRowDataBound="grdCountryMaps_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="Map Id" DataField="MapId" SortExpression="MapId" />
                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" SortExpression="SupplierName" />
                                <asp:BoundField DataField="CountryCode" HeaderText="Supp Country Code" SortExpression="CountryCode" />
                                <asp:BoundField DataField="CountryName" HeaderText="Supp Country Name" SortExpression="CountryName" />
                                <asp:BoundField HeaderText="System Country Code" DataField="Code" SortExpression="Code">
                                    <HeaderStyle BackColor="Turquoise" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="System Country Name" DataField="Name" SortExpression="Name">
                                    <HeaderStyle BackColor="Turquoise" />
                                </asp:BoundField>
                                <asp:TemplateField ShowHeader="true" HeaderText="Master Country Name">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlGridCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="false" onfocus="fillDropDown(this,true);" onchange="RemoveExtra(this,false);" onclick="fillDropDown(this,true);">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Status" HeaderText="Mapping Status" SortExpression="Status" />
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                            Enabled="true" CommandArgument='<%# Bind("CountryMapping_ID") %>' OnClientClick="showCountryMappingModal();">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Select
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <%--  <asp:CheckBox ID="chkSelect" runat="server" CausesValidation="false" CommandName="Select"
                                            Enabled="true" HeaderText="Select" CssClass="CheckboxClick" />--%>
                                        <input type="checkbox" runat="server" id="chkSelect" onclick="SelectedRow(this);" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="hideColumn" HeaderStyle-CssClass="hideColumn">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnMasterCountryId" Value="" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="moCountryMapping" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">

            <div class="modal-header">
                <div class="panel-default">
                    <h4 class="modal-title">Update Supplier Mapping</h4>
                </div>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpdCountryMapModal" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
                        <!-- need to wrap this in a modal and pop from selectedindexchanged and change default mode to EDIT  -->
                        <asp:FormView ID="frmEditCountryMap" runat="server" DefaultMode="Insert" DataKeyNames="CountryMapping_ID" OnItemCommand="frmEditCountryMap_ItemCommand">

                            <EditItemTemplate>

                                <div class="row">

                                    <div class="col-sm-4">

                                        <div class="panel panel-default">
                                            <div class="panel-heading">Supplier</div>
                                            <div class="panel-body">
                                                <table class="table table-condensed">
                                                    <tbody>
                                                        <tr>
                                                            <td><strong>Supplier</strong></td>
                                                            <td>
                                                                <asp:Label ID="lblSupplierName" runat="server" Text=""></asp:Label>&nbsp;
                                                                     <asp:Label ID="lblSupplierCode" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><strong>Country</strong></td>
                                                            <td>
                                                                <asp:Label ID="lblSupCountryName" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><strong>Code</strong></td>
                                                            <td>
                                                                <asp:Label ID="lblSupCountryCode" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">System</div>
                                            <div class="panel-body">

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="ddlSystemCountryName">
                                                        Country
                                                        <asp:RequiredFieldValidator ID="vddlSystemCountryName" runat="server" ErrorMessage="*" ControlToValidate="ddlSystemCountryName" InitialValue="0" CssClass="text-danger" ValidationGroup="MappingPop" Enabled="false"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlSystemCountryName" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSystemCountryName_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="txtSystemCountryCode">Code</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtSystemCountryCode" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <!-- bind to ISO3166-1-Alpha-2 -->
                                                    <label class="control-label col-sm-4" for="txtISO2CHAR">ISO 2CHR</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtISO2CHAR" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <!-- bind to ISO3166-1-Alpha-3 -->
                                                    <label class="control-label col-sm-4" for="txtISO3CHAR">ISO 3CHR</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtISO3CHAR" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-sm-4">

                                        <div class="panel panel-default">
                                            <div class="panel-heading">Status</div>
                                            <div class="panel-body">

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="ddlStatus">
                                                        Status
                                                        <asp:RequiredFieldValidator ID="vddlStatus" runat="server" ErrorMessage="*" ControlToValidate="ddlStatus" InitialValue="0" CssClass="text-danger" ValidationGroup="MappingPop"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true" onchange="ddlStatusChanged(this);">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="txtSystemRemark">Remark</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtSystemRemark" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">&nbsp; </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" CommandName="Add" ValidationGroup="MappingPop" CausesValidation="true" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" Text="Cancel" CommandName="Cancel" data-dismiss="modal" CausesValidation="false" />
                                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm" Text="Submit" CommandName="Submit" data-dismiss="modal" Visible="false" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnMatchedMapSelected" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" Text="Map Selected" CommandName="MapSelected" CausesValidation="false" />
                                                    <asp:Button ID="btnMatchedMapAll" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" Text="Map All" CommandName="MapAll" CausesValidation="false" />

                                                </div>
                                            </div>
                                        </div>

                                    </div>
                            </EditItemTemplate>
                        </asp:FormView>
                        <div class="row form-group">
                            <div class="col-lg-12">
                                <div id="dvMsgForDelete" runat="server" style="display: none;"></div>
                            </div>
                        </div>
                        <div class="row" runat="server" id="dvMatchingRecords">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <div id="dvMsg" runat="server" style="display: none;"></div>
                                </div>

                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group col-sm-3">
                                            <div class="input-group">
                                                <label class="input-group-addon" for="ddlProductBasedPageSize">Page Size</label>
                                                <asp:DropDownList ID="ddlMatchingPageSize" runat="server" CssClass="form-control col-lg-3" AutoPostBack="true" OnSelectedIndexChanged="ddlMatchingPageSize_SelectedIndexChanged">
                                                    <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:GridView ID="grdMatchingCountry" runat="server" AllowPaging="True" AutoGenerateColumns="False" AllowCustomPaging="true"
                                                EmptyDataText="No Mappings for search conditions" CssClass="table table-hover table-striped"
                                                DataKeyNames="CountryMapping_ID,Supplier_Id" OnSelectedIndexChanged="grdMatchingCountry_SelectedIndexChanged" OnPageIndexChanging="grdMatchingCountry_PageIndexChanging"
                                                OnRowCommand="grdMatchingCountry_RowCommand" AllowSorting="false" OnSorting="grdMatchingCountry_Sorting">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Map Id" DataField="MapId" SortExpression="MapId" />
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" SortExpression="SupplierName" />
                                                    <asp:BoundField DataField="CountryCode" HeaderText="Supp Country Code" SortExpression="CountryCode" />
                                                    <asp:BoundField DataField="CountryName" HeaderText="Supp Country Name" SortExpression="CountryName" />
                                                    <asp:BoundField HeaderText="System Country Code" DataField="Code" SortExpression="Code">
                                                        <HeaderStyle BackColor="Turquoise" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="System Country Name" DataField="Name" SortExpression="Name">
                                                        <HeaderStyle BackColor="Turquoise" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Status" HeaderText="Mapping Status" SortExpression="Status" />
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemTemplate>
                                                            <input type="checkbox" runat="server" id="chkMatchedSelect" onclick="MatchedSelect(this);" />

                                                            <%--<asp:CheckBox ID="chkMatchedSelect" runat="server" CausesValidation="false" CommandName="Select" AutoPostBack="false"
                                                                Enabled="true" HeaderText="Select" OnCheckedChanged="chkMatchedSelect_CheckedChanged" />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pagination-ys" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>













