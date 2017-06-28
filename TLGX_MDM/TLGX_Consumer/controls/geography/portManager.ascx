<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="portManager.ascx.cs" Inherits="TLGX_Consumer.controls.geography.portManager" %>

<asp:Panel ID="panSearchConditions" runat="server">


    <div class="panel-group" id="accordion">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Ports</a>
                </h4>
            </div>

            <div id="collapseSearch" class="panel-collapse collapse in">
                <div class="panel-body">
                    <div class="container">
                        <div class="row">

                            <div class="col-sm-6">

                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="ddlMasterCountry">System Country</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlMasterCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="ddlMasterCity">System City</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlMasterCity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="ddlStatus">Mapping Status</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="txtPortCountryName">Port Country Name</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtPortCountryName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="txtPortCityName">Port City Name</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtPortCityName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="col-sm-6">
                                <div class="form-group">
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
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" />
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" />
                                    </div>
                                    <div class="col-sm-12">&nbsp; </div>
                                </div>

                            </div>

                        </div>


                    </div>
                </div>
            </div>

        </div>
    </div>

    <br />
    <h4>Search Results</h4>
    <hr />
    <asp:GridView ID="grdCityList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="City_Id" CssClass="table table-hover table-striped">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
            <asp:BoundField DataField="CountryName" HeaderText="CountryName" SortExpression="CountryName" />
            <asp:HyperLinkField DataNavigateUrlFields="City_Id" DataNavigateUrlFormatString="~/geography/city?City_Id={0}" DataTextField="City_Id" NavigateUrl="~/geography/city" HeaderText="Manage" />

        </Columns>
    </asp:GridView>


</asp:Panel>




<div class="container">
    <div class="panel panel-default">
        <div class="panel-heading">Port Details</div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-8">

                    <div class="panel panel-default">
                        <div class="panel-heading">OAG Port Reference Data</div>
                        <div class="panel-body">

                            <div class="col-sm-6">

                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="txtOAG_loc">Port Code</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtOAG_loc" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="txtOAG_multicity">Multicity Port</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtOAG_multicity" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="txtOAG_type">Port Type</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtOAG_type" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="txtOAG_subtype">Port Subtype</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtOAG_subtype" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="txtoag_name">Port Name</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtoag_name" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="txtoag_inactive">OAG Status</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtoag_inactive" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>


                            </div>

                            <div class="col-sm-6">

                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="txtoag_ctry">OAG Country</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtoag_ctry" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="txtoag_subctry">OAG SubCtry</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtoag_subctry" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                 <div class="form-group">
                                    <label class="control-label col-sm-4" for="txtoag_ctryname">OAG Country</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtoag_ctryname" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="txtoag_state">OAG State</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtoag_state" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="txtoag_substate">OAG SubState</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtoag_substate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                 <div class="form-group">
                                    <label class="control-label col-sm-4" for="txtoag_lat">OAG Lat</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtoag_lat" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                  <div class="form-group">
                                    <label class="control-label col-sm-4" for="txtoag_lon">OAG Lon</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtoag_lon" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>



                            </div>

                        </div>
                    </div>

                 

                </div>

                <div class="col-lg-4">

                    <div class="panel panel-default">
                        <div class="panel-heading">System Mapping</div>
                        <div class="panel-body">

                            <div class="form-group">
                                <label class="control-label col-sm-4" for="ddlMasterCountry">Country</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-4" for="DropDownList2">Main City</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>


                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="DropDownList3">Multi City</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>



                                <br />
                                <br />
                                <asp:LinkButton ID="lnkUpdatePort" CommandName="Save" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="true" ValidationGroup="vldRoomInfo">Save Port Definition</asp:LinkButton>



                            
                        </div>


                    </div>


                </div>
           
            </div>
             </div>
        </div>
    </div>
