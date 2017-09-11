<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchActivityMasterControl.ascx.cs" Inherits="TLGX_Consumer.controls.activity.SearchActivityMasterControl" %>


<div class="row">
    <div class="col-sm-12">
        <div class="panel-group" id="accordion">
            <div class="panel panel-default">

                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Activities</a>
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

                        <div class="form-group col-sm-6">

                            <asp:UpdatePanel ID="updSearchDDLChange" runat="server">
                                <ContentTemplate>
                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlCountry">Country</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlCity">City</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group col-sm-12">
                                <label class="control-label col-sm-6" for="ddlProductCategorySubType">
                                    Product Category
                                        <%--<asp:RequiredFieldValidator ID="vddlProductCategoryType" runat="server" ErrorMessage="Please select product category."
                                            ControlToValidate="ddlProductCategoryType" CssClass="text-danger" ValidationGroup="HotelSearch"
                                            Text="*"></asp:RequiredFieldValidator>--%>
                                </label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlProductCategoryType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group col-sm-12">
                                <label class="control-label col-sm-6" for="ddlProductCategorySubType">
                                    Category Sub Type
                                        <%--<asp:RequiredFieldValidator ID="vddlProductCategorySubType" runat="server" ErrorMessage="Please select product sub category."
                                            ControlToValidate="ddlProductCategorySubType" InitialValue="" CssClass="text-danger" ValidationGroup="HotelSearch"
                                            Text="*"></asp:RequiredFieldValidator>--%>
                                </label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlProductCategorySubType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group col-sm-6">

                            <div class="form-group col-sm-12 row">
                                <label class="control-label col-sm-6" for="txtProductName">Product Name</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-sm-12 row">
                                <label class="control-label col-sm-6" for="ddlProductType">
                                    Product Type
                                        <%--<asp:RequiredFieldValidator ID="vddlProductType" runat="server" ErrorMessage="Please select status." ControlToValidate="ddlProductType"
                                            InitialValue="" CssClass="text-danger" ValidationGroup="HotelSearch"
                                            Text="*"></asp:RequiredFieldValidator>--%>
                                </label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group col-sm-12 row">
                                <label class="control-label col-sm-6" for="ddlProductSubType">
                                    Product Sub Type
                                        <%--<asp:RequiredFieldValidator ID="vddlProductSubType" runat="server" ErrorMessage="Please select status." ControlToValidate="ddlProductSubType"
                                            InitialValue="" CssClass="text-danger" ValidationGroup="HotelSearch"
                                            Text="*"></asp:RequiredFieldValidator>--%>
                                </label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlProductSubType" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group col-sm-12 row">
                                <label class="control-label col-sm-6" for="ddlStatus">
                                    Status
                                        <%--<asp:RequiredFieldValidator ID="vddlStatus" runat="server" ErrorMessage="Please select status." ControlToValidate="ddlStatus"
                                            InitialValue="" CssClass="text-danger" ValidationGroup="HotelSearch"
                                            Text="*"></asp:RequiredFieldValidator>--%>
                                </label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group col-sm-12 row">
                                <div class="form-group col-sm-6">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" /><%--ValidationGroup="HotelSearch"--%>
                                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" OnClick="btnReset_Click"/>
                                </div>
                                <div class="form-group col-sm-6">
                                    <asp:Button ID="btnAddNew" runat="server" CssClass="btn btn-primary btn-sm pull-right" Text="Add New" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

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
</div>




<%--<fieldset>

    <legend></legend>


    <!-- dummy table to produce UI for USe Case, replace with data control and replace with a data grid 

<table class="table table-striped">
    <thead>
      <tr>
        <th>Sub Type</th>
        <th>Country</th>
        <th>City</th>
        <th>Id</th>
        <th>Name</th>
        <th>Status</th>
        <th></th>

      </tr>
    </thead>
    <tbody>
      <tr>
        <td>Sightseeing</td>
        <td>United Kingdom</td>
        <td>High Wycombe</td>  
        <td>UKHW001</td>
        <td>Hellfire Caves</td>
        <td>Active</td>
        <td>
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-default " Text="Manage" ValidationGroup="HotelSearch" />
        </td>
            <td>
            <asp:Button ID="Button2" runat="server" CssClass="btn btn-default " Text="Delete" ValidationGroup="HotelSearch" />
        </td>

      </tr>
      <tr>
        <td>MEal</td>
        <td>United Kingdom</td>
        <td>High Wycombe</td>  
        <td>UKHW001</td>
        <td>Matt's Restautant</td>
        <td>Active</td>
        <td>
            <asp:Button ID="Button3" runat="server" CssClass="btn btn-default " Text="Manage" ValidationGroup="HotelSearch" />
        </td>
            <td>
            <asp:Button ID="Button4" runat="server" CssClass="btn btn-default " Text="Delete" ValidationGroup="HotelSearch" />
        </td>

      </tr>
        <tr>
        <td>Cruise</td>
        <td>United Kingdom</td>
        <td>High Wycombe</td>  
        <td>UKHW001</td>
        <td>Boat Trip on the Wye</td>
        <td>Active</td>
        <td>
            <asp:Button ID="Button5" runat="server" CssClass="btn btn-default " Text="Manage" ValidationGroup="HotelSearch" />
        </td>
            <td>
            <asp:Button ID="Button6" runat="server" CssClass="btn btn-default " Text="Delete" ValidationGroup="HotelSearch" />
        </td>

      </tr>
    </tbody>
  </table>
      <asp:Button ID="Button7" runat="server" CssClass="btn btn-default " Text="1" ValidationGroup="HotelSearch" />
      <asp:Button ID="Button8" runat="server" CssClass="btn btn-default " Text="2" ValidationGroup="HotelSearch" />-->
</fieldset>--%>

<div class="panel-group" id="searchResult">
    <div class="panel panel-default">

        <div class="panel-heading clearfix">
            <h4 class="panel-title pull-left">
                <a data-toggle="collapse" data-parent="#searchResult" href="#collapseSearchResult">Search Results (Total Count:
                            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</a>
            </h4>
        </div>

        <div id="collapseSearchResult" class="panel-collapse collapse in">
            <div class="panel-body">

                <%--<div class="row">
                            <div id="dvMsg" runat="server" enableviewstate="false" style="display: none;">
                            </div>
                        </div>--%>

                <asp:GridView ID="gvActivitySearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
                    EmptyDataText="No data for search conditions" CssClass="table table-hover table-striped"
                    AutoGenerateColumns="false" DataKeyNames="Activity_Id" OnPageIndexChanging="gvActivitySearch_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="CommonProductID" HeaderText="Common Product ID" />
                        <asp:BoundField DataField="Product_Name" HeaderText="Product Name" />
                        <asp:BoundField DataField="ProductCategory" HeaderText="Product Category" />
                        <asp:BoundField DataField="" HeaderText="Category Sub Type" />
                        <asp:BoundField DataField="ProductType" HeaderText="Product Type" />
                        <asp:BoundField DataField="ProductSubType" HeaderText="Product Sub Type" />
                        <asp:BoundField DataField="Country" HeaderText="Country" />
                        <asp:BoundField DataField="City" HeaderText="City" />
                        <asp:BoundField DataField="" HeaderText="Status" />
                        <asp:HyperLinkField Text="Manage" ControlStyle-CssClass="btn btn-default"/>
                        <asp:HyperLinkField Text="Delete" ControlStyle-CssClass="btn btn-default"/>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </div>

    </div>
</div>
