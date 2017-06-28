<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CityMap.ascx.cs" Inherits="TLGX_Consumer.controls.staticdata.CityMap" %>



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
                    <div class="row">

                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="control-label col-sm-4" for="ddlSupplierName">Supplier Name</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-4" for="ddlMasterCountry">System Country</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlMasterCountry" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-4" for="ddlCity">System City</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-4" for="ddlStatus">Mapping Status</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                    </asp:DropDownList>
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
                            <br /><br />                          
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" />
                                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" />                          
                        </div>
                        </div>                 
                </div>
            </div>
        </div>

</div></div>




<br />
<h4>Search Results</h4>

<asp:GridView ID="grdCityMaps" runat="server" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="No Static Updates" CssClass="table table-hover table-striped" OnDataBound="grdCityMaps_DataBound" >
    
    <Columns>
        <asp:BoundField DataField="SupplierName" HeaderText="Name" />
        <asp:BoundField DataField="CountryCode" HeaderText="Country Code" />
        <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
        <asp:BoundField DataField="CityCode" HeaderText="City Code" />
        <asp:BoundField DataField="CityName" HeaderText="City Name" />
        

        <asp:BoundField DataField="CountryCode" HeaderText="Country Code" />
        <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
        <asp:BoundField HeaderText="State" />
        <asp:BoundField DataField="CityCode" HeaderText="City Code" />
        <asp:BoundField DataField="CityName" HeaderText="City Name" />
        
        <asp:BoundField HeaderText="Status" />         
        <asp:ButtonField CommandName="Manage" Text="Manage" /> 
        
    </Columns>



</asp:GridView>

