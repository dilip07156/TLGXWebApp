<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="manageRoomTypeMapping.ascx.cs" Inherits="TLGX_Consumer.controls.roomtype.manageRoomTypeMapping" %>

<script>
    $(document).ready(function () {
        $('[data-toggle="popover"]').popover();
    });
</script>

<div class="row">
    <div class="col-lg-6">

        <div class="panel panel-default">
            <div class="panel-heading">Hotel Name and Key Facts</div>
            <div class="panel-body">
                <div class="form-group" style="display: none">
                    <div class="col-sm-8">
                        <asp:TextBox ID="TextBox1" runat="server" class="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4" for="txtHotelName">
                        Name
                    </label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="TextBox2" runat="server" class="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4" for="txtDisplayName">
                        Display</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
            </div>
        </div>


    </div>

    <div class="col-lg-6">
        <div class="panel panel-default">
            <div class="panel-heading">Details</div>
            <div class="panel-body">
                <div class="form-group" style="display: none">
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtHotelID" runat="server" class="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4" for="txtHotelName">
                        Hotel Id
                    </label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtHotelName" runat="server" class="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4" for="txtDisplayName">
                        Edit Date</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-sm-4" for="txtDisplayName">
                        Website</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
            </div>
        </div>



    </div>

</div>

<div class="navbar">
    <div class="navbar-inner">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#panStaticRoomTypes" data-toggle="tab">Room Types: Static Data Dumps</a></li>
            <li><a href="#panProductRoomTypes" data-toggle="tab">Room Types: Product Definition</a></li>
            <li><a href="#panDynamicRoomTypes" data-toggle="tab">Room Types: Dynamic Room Misses </a></li>
        </ul>
    </div>
</div>


<div class="tab-content">

    <div class="tab-pane active" id="panStaticRoomTypes">

        <div class="row">
            <div class="col-lg-6">
                <div class="panel panel-default">
                    <div class="panel-heading">Filters</div>
                    <div class="panel-body">

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="ddlSupplierName">
                                Supplier Name
                            </label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="control-label col-sm-4" for="ddlMappingStatus">
                                Mapping Status
                            </label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlMappingStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtRoomTypeName">
                                Supplier Room Name
                            </label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtRoomTypeName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" />
                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" />

                    </div>
                </div>


            </div>

            <div class="col-lg-3">


                <div class="panel panel-default">
                    <div class="panel-heading">Create Bases Rooms</div>
                    <div class="panel-body">
                        <asp:Button ID="Button5" runat="server" CssClass="btn btn-primary btn-sm" Text="Create All Visible" />
                        <asp:Button ID="Button6" runat="server" CssClass="btn btn-primary btn-sm" Text="Create Selected Only" />
                    </div>
                </div>



            </div>


            <div class="col-lg-3">
                <div class="panel panel-default">
                    <div class="panel-heading">AutoMap</div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="control-label col-sm-4" for="ddlMapType">
                                Against
                            </label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlMapType" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                                    <asp:ListItem Value="0" Selected="True">-Select-</asp:ListItem>
                                    <asp:ListItem Value="0">Supplier Name</asp:ListItem>
                                    <asp:ListItem Value="0">Transformed Name</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtMatchThreshold">
                                Level
                            </label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtMatchThreshold" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                                        
                            <asp:Button ID="Button8" runat="server" CssClass="btn btn-primary btn-sm" Text="Likely Match" />
                           <asp:Button ID="Button7" runat="server" CssClass="btn btn-primary btn-sm" Text="Exact Match" />
                 
                    </div>
                </div>
            </div>
        </div>









        <div class="row">
            <div class="col-lg-6">
                <h4>Supplier Static Room Types</h4>
            </div>

            <div class="col-lg-4">

                <div class="form-group">
                    <div class="input-group">
                        <label class="input-group-addon" for="ddlPageSize"><strong>Page Size</strong></label>
                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" AutoPostBack="true" Width="100px">
                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="25" Value="25" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            </div>

            <div class="col-lg-2">

                <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary btn-sm" Text="Map Selected" />
                <asp:Button ID="Button4" runat="server" CssClass="btn btn-primary btn-sm" Text="Map All" CausesValidation="false" />

            </div>



        </div>





        <hr />

        <table class="table table-hover table-striped">
            <thead>
                <tr>
                    <th>Supplier Name</th>
                    <th>Supplier Room Id</th>
                    <th>Supplier Room Name</th>
                    <th>Room Category Id</th>
                    <th>Room Category Name</th>
                    <th>Tx Room Name</th>
                    <th>St Room Name</th>
                    <th>Match Level</th>
                    <th>System Room Type Name</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>DOTW</td>
                    <td>TYUt6568</td>
                    <td><a href="#" data-toggle="popover" title="Room Description" data-content="<strong>1 Queen Bed</strong><br />517-sq-foot (48-sq-meter) room with pool views<br /><br /><b>Layout</b> - Separate bedroom <br /><b>Entertainment</b> - Free WiFi and cable channels <br /><b>Food & Drink</b> - Kitchen with refrigerator, stovetop, cookware/dishware, and coffee/tea maker<br /><b>Bathroom</b> - Private bathroom, free toiletries, and a shower<br /><b>Comfort</b> - Ceiling fan<br /><b>Need to Know</b> - Weekly housekeeping<br />Non-Smoking<br />">2 Rm Prem Suite, 1 King Bed, Hearing Accessible</a></td>


                    <td>Cat8743848
                    </td>
                    <td>Cat Code Name</td>
                    <td>Two Room Premium Suite, One King Bed, Hearing Accessible</td>
                    <td>78%</td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                            <asp:ListItem Value="0">Standard Double</asp:ListItem>
                            <asp:ListItem Value="0">Superior Cityview Accessible Twin</asp:ListItem>
                            <asp:ListItem Value="0">Double Room with Private Pool</asp:ListItem>

                        </asp:DropDownList>
                    </td>

                    <td>
                        <asp:CheckBox ID="chkMapMe" runat="server" />
                    </td>



                </tr>


            </tbody>

        </table>


        <asp:Button ID="Button9" runat="server" CssClass="btn btn-default " Text="1" ValidationGroup="HotelSearch" />
        <asp:Button ID="Button10" runat="server" CssClass="btn btn-default " Text="2" ValidationGroup="HotelSearch" />








    </div>


    <div class="tab-pane fade in" id="panProductRoomTypes">

        i will show the existing Product Rooms defined on the accommodation table


    </div>


    <div class="tab-pane fade in" id="panDynamicRoomTypes">
        panDynamicRoomTypes
    </div>


</div>


