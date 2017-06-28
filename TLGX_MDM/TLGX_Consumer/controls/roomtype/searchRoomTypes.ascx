<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchRoomTypes.ascx.cs" Inherits="TLGX_Consumer.controls.roomtype.searchRoomTypes" %>

<div class="row">
    <div class="col-lg-12">
        <div class="panel-group" id="accordion">
            <div class="panel panel-default">

                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Filters</a>
                    </h4>
                </div>

                <div id="collapseSearch" class="panel-collapse collapse in">

                    <div class="panel-body">

                        <div class="form-group">
                            <div class="col-sm-12">
                                <br />
                                <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="HotelSearch" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-6">

                                <div class="form-group">
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

                                <div class="form-group">
                                    <label class="control-label col-sm-6" for="txtCommon">Common Hotel ID</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtCommon" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-6" for="txtHotelName">Hotel Name</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtHotelName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <br />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-6">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" ValidationGroup="HotelSearch" />
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>


                            <div class="col-lg-6">
                                <asp:UpdatePanel ID="udpSearchDllChange" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <label class="control-label col-sm-6" for="ddlCountry">Country</label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true">
                                                    <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="control-label col-sm-6" for="ddlCity">City</label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div class="form-group">
                                    <label class="control-label col-sm-6" for="ddlStatus">
                                        Status
                                        <asp:RequiredFieldValidator ID="vddlStatus" runat="server" ErrorMessage="Please select status." ControlToValidate="ddlStatus"
                                            InitialValue="" CssClass="text-danger" ValidationGroup="HotelSearch"
                                            Text="*"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


<h4>Results</h4>
<hr />

<table class="table table-hover table-striped">
    <thead>
        <tr>
            <th>Product Id</th>
            <th>Product Name</th>
            <th>Country</th>
            <th>City</th>
            <th>Product Status</th>            
            <th>Mapping Status</th>
            <th>Has Product Room Types?</th>
            <th>Static Pending</th>
            <th>Dynamic Pending</th>
            <th></th>

        </tr>
    </thead>


    <tbody>
        <tr>
            <td>10123</td>
            <td>Abba Queensgate Hotel</td>
            <td>United Kingdom</td>
            <td>London</td>
            <td>Active</td>
            <td>Pending</td>
            <td>Yes (3) </td>
            <td>3</td>
            <td>4</td>
            <td>
                <asp:Button ID="Button4" runat="server" CssClass="btn btn-default " Text="Select" ValidationGroup="HotelSearch" />
            </td>
        </tr>


    </tbody>


</table>
