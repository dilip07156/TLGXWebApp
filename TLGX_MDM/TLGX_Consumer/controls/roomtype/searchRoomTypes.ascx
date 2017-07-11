<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchRoomTypes.ascx.cs" Inherits="TLGX_Consumer.controls.roomtype.searchRoomTypes" %>
<script>
$(document).ready(function(){
    $('[data-toggle="tooltip"]').tooltip(); 
});
</script>



<div class="navbar">
    <div class="navbar-inner">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#panSupplierSearch" data-toggle="tab">Search by Supplier</a></li>
            <li><a href="#panProductSearch" data-toggle="tab">Search by Product</a></li>
        </ul>
    </div>
</div>

<div class="tab-content">
    <div class="tab-pane active" id="panSupplierSearch">

<!-- search filters panel -->
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
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="HotelSearch" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-6">

                                        <div class="form-group">
                                            <label class="control-label col-sm-6" for="ddlProductCategorySubType">
                                                Select Supplier
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select a Supplier."
                                            ControlToValidate="ddlSupplierName" InitialValue="" CssClass="text-danger" ValidationGroup="HotelSearch"
                                            Text="*"></asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="control-label col-sm-6" for="ddlStatus">
                                                Status
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select status." ControlToValidate="ddlStatus"
                                            InitialValue="" CssClass="text-danger" ValidationGroup="HotelSearch"
                                            Text="*"></asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="control-label col-sm-6" for="txtPRoductName">
                                                Product                                       
                                            </label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPRoductName" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                        </div>



                                        <div class="form-group">
                                            <div class="col-sm-6">
                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" ValidationGroup="HotelSearch" />
                                                <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6">

                                        <div class="form-group">
                                            <label class="control-label col-sm-6" for="ddlCountry">Country</label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" AutoPostBack="true">
                                                    <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="control-label col-sm-6" for="ddlCity">City</label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
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

 <!-- Search Results Panel -->


  
        <div class="panel-group" id="accordionProductSearchResult">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-lg-6">
                            <h4>Supplier Room Types (%totalrowcount%)</h4>
                        </div>

                        <div class="col-lg-2">

                            <div class="form-group">
                                <label class="control-label col-sm-4" for="ddlProductBasedPageSize">Page</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlProductBasedPageSize" runat="server" CssClass="form-control col-lg-3">
                                        <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-4  pull right">

                            <div class="form-group">
                                <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary btn-sm" Text="Map Selected" />
                                <asp:Button ID="Button5" runat="server" CssClass="btn btn-primary btn-sm" Text="Map All" />
                   
                                <asp:Button ID="Button6" runat="server" CssClass="btn btn-success btn-sm" Text="TTFU Selected" />
                                <asp:Button ID="Button7" runat="server" CssClass="btn btn-success btn-sm" Text="TTFU All" />
                            </div>

                        </div>


                    </div>

                </div>

                <div class="panel-body">
                    <div class="row">


                        <div class="col-lg-12">
                            <table class="table table-hover table-striped">
                                <thead>
                                    <tr>
                                        <th>Supplier</th>
                                        <th>TLGX Id</th>
                                        <th>Product Name</th>
                                        <th>Location</th>
                                        <th>Has Rooms</th>
                                        <th>S Room Id</th>
                                        <th class="col-lg-3">Supplier Room Type Name</th>
                                        <th class="col-lg-3">Suggested Room Info</th>
                                        <th>A?</th>
                                        <th class="col-lg-1">Status</th>
                                        <th></th>
                                    </tr>
                                </thead>

                                <tbody>


                                    <!-- sample add format, with no room on product -->
                                    <tr>
                                        <td>EXPEDIA</td>
                                        <td>192346</td>
                                        <td>Abba Queensgate Hotel</td>
                                        <td>London (UK)</td>
                                        <td><span class="label label-danger">No</span></td>
                                        <td>ABC123</td>
                                        <td>Business Room, 1 King Bed - Stay 2 and Save 10 Percent. Our hotel has no rooms so we need to create them</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtSuggestedName" CssClass="form-control">Business Room, 1 King Bed</asp:TextBox>                                       
                                        </td>
                                        <td><span class="glyphicon glyphicon-bed" data-toggle="tooltip" title="Bed Type: King Bed"></span></td>
                                        <td><asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="ADD" Value=""></asp:ListItem>
                                                </asp:DropDownList></td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="chkRows" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>EXPEDIA</td>
                                        <td>192346</td>
                                        <td>Abba Queensgate Hotel</td>
                                        <td>London (UK)</td>
                                        <td><span class="label label-danger">No</span></td>
                                        <td>ABC123</td>
                                        <td>Executive Room, 1 King Bed with fridge and Wifi (RO) - Stay 6 and Save 10 Percent. Our hotel has no rooms so we need to create them</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control">Executive Room, 1 King Bed</asp:TextBox>                                       
                                        </td>
                                        <td>
                                                <span class="glyphicon glyphicon-bed" data-toggle="tooltip" title="Bed Type: King Bed"></span>
                                                <span class="glyphicon glyphicon-wrench" data-toggle="tooltip" title="Amenities: Fridge, Wireless Internet"></span>
                                                <span class="glyphicon glyphicon-cutlery" data-toggle="tooltip" title="Room Only"></span>
                                        </td>
                                        <td><asp:DropDownList ID="DropDownList6" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="ADD" Value=""></asp:ListItem>
                                                </asp:DropDownList></td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="CheckBox1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>EXPEDIA</td>
                                        <td>192346</td>
                                        <td>Abba Queensgate Hotel</td>
                                        <td>London (UK)</td>
                                        <td><span class="label label-danger">No</span></td>
                                        <td>ABC123</td>
                                        <td>N/S DELUXE OCEANFRONT 'DISCOUNT 10% BOOK 30 DAYS IN ADVANCE (WHE30D10)'</td>
                                        <td class="col-lg-3">
                                            <asp:TextBox runat="server" ID="TextBox2" CssClass="form-control">Deluxe Oceanfront</asp:TextBox>                                       
                                        </td>
                                        <td>
                                            <span class="glyphicon glyphicon-fire" data-toggle="tooltip" title="No Smoking"></span>
                                            <span class="glyphicon glyphicon-usd" data-toggle="tooltip" title="Offer"></span>

                                        </td>
                                        <td><asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="ADD" Value=""></asp:ListItem>
                                                </asp:DropDownList></td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="CheckBox2" />
                                        </td>
                                    </tr>

                                    <!-- sample suggested map format, with room on product -->
                                    <tr>
                                        <td>EXPEDIA</td>
                                        <td>192346</td>
                                        <td>Abba Queensgate Hotel</td>
                                        <td>London (UK)</td>
                                        <td><span class="label label-success">9</span></td>
                                        <td>ABC123</td>
                                        <td>Standard Room, 2 Queen Beds, Accessible, Smoking - Stay 2 Nights Save $10. This is match for our room type</td>
                                        <td class="col-lg-3">

                                            <asp:DropDownList ID="DropDownList8" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="-Select-"></asp:ListItem>
                                                    <asp:ListItem Text="Standard Room, Two Queen Beds" Value="" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Executive Room" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Sea-facing Junior Suite Room" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                        <%--    <asp:TextBox runat="server" ID="TextBox3" CssClass="form-control"></asp:TextBox>                         --%>              
                                        </td>
                                        <td>
                                            <span class="glyphicon glyphicon-fire" data-toggle="tooltip" title="Smoking"></span>
                                            <span class="glyphicon glyphicon-usd" data-toggle="tooltip" title="Offer"></span>
                                            <span class="glyphicon glyphicon-thumbs-up" data-toggle="tooltip" title="Accesible"></span> 
                                            <span class="glyphicon glyphicon-bed" data-toggle="tooltip" title="Bed Type: Queen Bed"></span>                                         
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList7" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="REVIEW" Selected="True" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="ADD" Value=""></asp:ListItem>
                                                </asp:DropDownList></td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="CheckBox3" />
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>EXPEDIA</td>
                                        <td>192346</td>
                                        <td>Abba Queensgate Hotel</td>
                                        <td>London (UK)</td>
                                        <td><span class="label label-success">9</span></td>
                                        <td>ABC123</td>
                                        <td>Superdeluxe Room, Accessible, Smoking - Stay 2 Nights Save $10. This is NO match for our room type</td>
                                        <td class="col-lg-3">

                                            <asp:DropDownList ID="DropDownList9" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="-Select-"  Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Standard Room, Two Queen Beds" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Executive Room" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Sea-facing Junior Suite Room" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                        <asp:TextBox runat="server" ID="TextBox3" CssClass="form-control">Superdeluxe Room</asp:TextBox>                              
                                        </td>
                                        <td>
                                            <span class="glyphicon glyphicon-fire" data-toggle="tooltip" title="Smoking"></span>
                                            <span class="glyphicon glyphicon-usd" data-toggle="tooltip" title="Offer"></span>
                                            <span class="glyphicon glyphicon-thumbs-up" data-toggle="tooltip" title="Accesible"></span> 
                                            <span class="glyphicon glyphicon-bed" data-toggle="tooltip" title="Bed Type: Queen Bed"></span>                                         
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList10" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="-?-" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="MAPPED" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="ADD" Value=""></asp:ListItem>
                                                </asp:DropDownList></td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="CheckBox4" />
                                        </td>
                                    </tr>



                                       <!-- sample suggested map format, mapped to ARI -->

                                    <tr>
                                        <td>EXPEDIA</td>
                                        <td>192346</td>
                                        <td>Abba Queensgate Hotel</td>
                                        <td>London (UK)</td>
                                        <td><span class="label label-success">9</span></td>
                                        <td>ABC123</td>
                                        <td>Double Room Single Use - EB - Non refundable BB 10%. I am a previously mapped row</td>
                                        <td class="col-lg-3">

                                            <asp:DropDownList ID="DropDownList11" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="-Select-"></asp:ListItem>
                                                    <asp:ListItem Text="Double Room" Selected="True" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Executive Room" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Sea-facing Junior Suite Room" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                      <%--  <asp:TextBox runat="server" ID="TextBox4" CssClass="form-control">Superdeluxe Room</asp:TextBox>        --%>                      
                                        </td>
                                        <td>
            <%--                                <span class="glyphicon glyphicon-fire" data-toggle="tooltip" title="Smoking"></span>--%>
                                            <span class="glyphicon glyphicon-usd" data-toggle="tooltip" title="Non Refundable"></span>
                              <%--              <span class="glyphicon glyphicon-thumbs-up" data-toggle="tooltip" title="Accesible"></span> --%>
                                  <span class="glyphicon glyphicon-user" data-toggle="tooltip" title="Single Occupancy"></span>    
                                            <span class="glyphicon glyphicon-cutlery" data-toggle="tooltip" title="Includes Breakfast"></span>                                     
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList12" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="-?-" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="MAPPED" Value="" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="ADD" Value=""></asp:ListItem>
                                                </asp:DropDownList></td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="CheckBox5" />
                                        </td>
                                    </tr>




                                </tbody>


                            </table>
                                    <asp:Button ID="Button9" runat="server" CssClass="btn btn-default " Text="1" ValidationGroup="HotelSearch" />
        <asp:Button ID="Button10" runat="server" CssClass="btn btn-default " Text="2" ValidationGroup="HotelSearch" />
                        </div>
                    </div>




                </div>

                </div>
            </div>
        
    
    
    
    
    
    
    
    </div>
        
    
    
    
    
    
    
    
    
    <!-- Product Based Search -->
    <div class="tab-pane fade in" id="panProductSearch">


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
                                                    <div class="col-sm-6">
                                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" ValidationGroup="HotelSearch" />
                                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" />
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-lg-6">

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



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <h4>Products</h4>
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

            </div>
     </div>
