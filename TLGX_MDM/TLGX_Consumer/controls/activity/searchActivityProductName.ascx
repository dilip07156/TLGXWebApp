<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchActivityProductName.ascx.cs" Inherits="TLGX_Consumer.controls.activity.searchActivityProductName" %>




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

                    <div class="form-group">
                        <div class="col-sm-12">
                            <br />
                            <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="HotelSearch" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                        </div>
                    </div>

                    <asp:UpdatePanel ID="udpSearchDllChange" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <label class="control-label col-sm-6" for="ddlCountry">Country</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true" On>
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
                        <label class="control-label col-sm-6" for="txtHotelName">Product Name</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtHotelName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                     <div class="form-group">
                        <label class="control-label col-sm-6" for="ddlStatus">
                            Activity Type
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select status." ControlToValidate="ddlStatus"
                                            InitialValue="" CssClass="text-danger" ValidationGroup="HotelSearch"
                                            Text="*"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>



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
                        <div class="col-sm-6">
                            <asp:LinkButton ID="btnAdd2" runat="server" CausesValidation="false" CommandName="AddProduct" Text="Add New" CssClass="btn btn-primary btn-sm pull-right" />
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
                    <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control" AutoPostBack="true"  Width="100px">
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


    <!-- dummy table to produce UI for USe Case, replace with data control and replace with a data grid -->

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
      <asp:Button ID="Button8" runat="server" CssClass="btn btn-default " Text="2" ValidationGroup="HotelSearch" />
  </fieldset>



<!-- Add Product Modal -->





