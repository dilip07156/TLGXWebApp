<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchHotelascx.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.searchHotelascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.23/jquery-ui.min.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/Blitzer/jquery-ui.css" rel="stylesheet" type="text/css" />--%>

<script type="text/javascript">

    function showModal() {
        $("#myModal").modal('show');
    }

    $(function () {
        $("#btnAddNew").click(function () {
            showModal();
        });
    });

    function formreset() {
        var elements = document.getElementsByTagName("input");

        for (var ii = 0; ii < elements.length; ii++) {
            if (elements[ii].type == "text") {
                elements[ii].value = "";
            }
        }
    }

    function WebForm_OnSubmit() {
        if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) {
            $("#validation_dialog").dialog({
                title: "Validation Error!",
                modal: true,
                resizable: false,
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                }
            });
            return false;
        }
        return true;
    }

</script>

<asp:UpdatePanel ID="udpSearchDllChange" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-sm-6">
                <div class="panel-group" id="accordion">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Filters</a>
                            </h4>
                        </div>

                        <div id="collapseSearch" class="panel-collapse collapse in">

                            <div class="panel-body">
                                <div class="form-group row">
                                    <div class="col-sm-12">
                                        <br />
                                        <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="HotelSearch" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="ddlProductCategorySubType">
                                        Sub Category
                                        <asp:RequiredFieldValidator ID="vddlProductCategorySubType" runat="server" ErrorMessage="Please select product sub category."
                                            ControlToValidate="ddlProductCategorySubType" InitialValue="" CssClass="text-danger" ValidationGroup="HotelSearch"
                                            Text="*"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlProductCategorySubType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Value="">-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="txtCommon">Common Hotel ID</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtCommon" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="txtHotelName">Hotel Name</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtHotelName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="ddlCountry">Country</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                            <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="ddlCity">City</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="form-group row">
                                    <label class="control-label col-sm-6" for="ddlStatus">
                                        Status
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-sm-12">
                                        <br />
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-sm-6">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" CausesValidation="true" ValidationGroup="HotelSearch" />
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" OnClientClick="formreset();" OnClick="btnReset_Click" />
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:LinkButton ID="btnAdd2" runat="server" CausesValidation="false" CommandName="AddProduct" Text="Add New" CssClass="btn btn-primary btn-sm pull-right" PostBackUrl="~/hotels/addnew.aspx" />
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <fieldset>

            <legend></legend>

            <div id="dvPageSize" runat="server" class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <div class="input-group">
                            <label class="input-group-addon" for="ddlPageSize"><strong>Page Size</strong></label>
                            <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" Width="100px">
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

            <div id="dvGrid" runat="server" class="row">
                <div class="col-sm-12">
                    <asp:GridView ID="grdSearchResults" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False" DataKeyNames="AccomodationId"
                        CssClass="table table-responsive table-hover table-striped table-bordered" OnPageIndexChanging="grdSearchResults_PageIndexChanging" PagerStyle-CssClass="Page navigation">
                        <Columns>
                            <asp:BoundField DataField="CompanyHotelId" HeaderText="Hotel Id" InsertVisible="False" ReadOnly="True" SortExpression="CompanyHotelId" />
                            <asp:BoundField DataField="Country" HeaderText="Country Name" SortExpression="Country" />
                            <asp:BoundField DataField="City" HeaderText="City Name" SortExpression="City" />
                            <asp:BoundField DataField="HotelName" HeaderText="Hotel Name" SortExpression="HotelName" />
                            <asp:HyperLinkField DataNavigateUrlFields="AccomodationId" DataNavigateUrlFormatString="~/hotels/manage.aspx?Hotel_Id={0}" Text="Select" ControlStyle-Font-Bold="true" NavigateUrl="~/hotels/manage.aspx" ControlStyle-CssClass="btn btn-primary btn-sm" />
                        </Columns>
                        <PagerStyle CssClass="pagination-ys" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>

        </fieldset>


    </ContentTemplate>
</asp:UpdatePanel>
