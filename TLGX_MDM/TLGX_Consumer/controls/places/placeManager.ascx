<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="placeManager.ascx.cs" Inherits="TLGX_Consumer.controls.places.placeManager" %>
 <div class="row">
            <div class="col-sm-12">
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
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="ddlCountry">Country</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" >
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="ddlCity">City</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="txtHotelName">Product Name</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtHotelName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="ddlPlaceType">
                                            Place Type
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlPlaceType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="ddlStatus">
                                            Status
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="ddlPageSize">
                                            Page Size
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                                <asp:ListItem Value="100" Text="100"></asp:ListItem>
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
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>



 <div class="panel-group" id="accordionSearchResult">
            <div class="panel panel-default">

                <div class="panel-heading">
                    <h4 class="panel-title">
                        <!-- search results need to be wired in -->
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearchResult">Search Results (Total Count:
            <asp:Label ID="lblTotalCount" runat="server" Text="0"></asp:Label>)
                        </a></h4>

                </div>

                <div id="collapseSearchResult" class="panel-collapse collapse in">

                    <div class="panel-body">


                        <table class="table table-hover table-striped">
                            <thead>
                                <tr>
                                    
                                    <th>Country</th>
                                    <th>City</th>
                                    <th>Name</th>
                                    <th>Place Type</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>United Kingdom</td>
                                    <td>London</td>
                                    <td>Kensington Gardens</td>
                                    <td>Museum, Gardens</td>
                                    <td>
                                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Manage" CssClass="btn btn-default"
                                                    Enabled="true" CommandArgument='<%# Bind("Activity_Id") %>' >
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp View
                                                </asp:LinkButton>

                                    </td>
                                </tr>


                            </tbody>


                        </table>



                        </div>
                    </div>

                </div>

     </div>

<div id="moActivityManage" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">

            <div class="modal-header">
                <div class="panel panel-default">
                    <h4 class="modal-title">Place Details</h4>
                </div>
            </div>
            <div class="modal-body">


                <div class="row">
                    <div class="col-lg-6">
                            
                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtPlaceName">Name</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtLat">Lat</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtLat" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtLon">Lon</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtLon" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtLon">Place Id</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                                   <div class="form-group">
                            <label class="control-label col-sm-4" for="txtPlaceType">Place Types</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtPlaceType" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>


                    </div>

                     <div class="col-lg-6">

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtAddressName">Address</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtAddressName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtCity">City</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtState">State</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtState" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtCountry">Country</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtPostalCode">Code</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                         </div>

      

                </div>
            </div>




                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
