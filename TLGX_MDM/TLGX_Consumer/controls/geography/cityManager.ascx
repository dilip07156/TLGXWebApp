<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cityManager.ascx.cs" Inherits="TLGX_Consumer.controls.geography.cityMapper" %>

<asp:Panel ID="panSearchConditions" runat="server">

    <div class="col-lg-12">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="row">
                    <div class="form-group">
                        <div class="col-sm-12">
                            <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="CityManager" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <label for="ddlCountry">
                                Country
                            <asp:RequiredFieldValidator ValidationGroup="CityManager" runat="server" ControlToValidate="ddlCountry"
                                Text="*" CssClass="text-danger" InitialValue="0" ErrorMessage="Please select a country before search !" />
                            </label>
                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>

                            <label for="txtCityName">CityName</label>
                            <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control" />

                            <asp:LinkButton ID="btnGetCities" runat="server" CommandName="Search" Text="Search" CssClass="btn btn-primary btn-sm" ValidationGroup="CityManager" OnClick="btnGetCities_Click" />
                        </div>
                    </div>



                    <%--<asp:LinkButton ID="btnGetCities" runat="server" CommandName="Search" Text="Search" CssClass="btn btn-primary btn-sm" ValidationGroup="vldgrpHealthAndSafety" OnClick="btnGetCities_Click" />--%>
                </div>
            </div>
            <div class="panel-body">
                <div runat="server" id="cityResult" style="display: none;">
                    <div class="panel-group" id="accordionSupplierSearch">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title"><a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Results</a></h4>
                            </div>
                            <div id="collapseSearch" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="form-group pull-right col-md-2 " id="divEntries" runat="server">
                                        <div class="input-group">
                                            <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                                            <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged" CssClass="form-control">
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

                                    <asp:GridView ID="grdCityList" EmptyDataText="No Data Found." runat="server" AllowPaging="True" AllowCustomPaging="true" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="City_Id" CssClass="table table-hover table-striped" OnPageIndexChanging="grdCityList_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                            <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                                            <asp:BoundField DataField="CountryName" HeaderText="CountryName" SortExpression="CountryName" />
                                            <asp:BoundField DataField="Edit_Date" HeaderText="Edit_Date" SortExpression="Edit_Date" />
                                            <asp:BoundField DataField="Edit_User" HeaderText="Edit_User" SortExpression="Edit_User" />
                                            <asp:BoundField HeaderText="# Hotels" />
                                            <asp:BoundField HeaderText="# Attractions" />
                                            <asp:BoundField HeaderText="# Holidays" />
                                            <asp:HyperLinkField DataNavigateUrlFields="City_Id" DataNavigateUrlFormatString="~/geography/city?City_Id={0}" DataTextField="City_Id" NavigateUrl="~/geography/city" HeaderText="Manage" />

                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Panel>







<asp:UpdatePanel ID="updatePanel1" runat="server">
    <ContentTemplate>



        <div class="container">
            <div class="row">
                <div class="col-lg-3">
                    <div class="panel panel-default">
                        <div class="panel-heading">City Details</div>
                        <div class="panel-body">

                            <asp:FormView ID="frmCityMaster" runat="server" DataKeyNames="City_Id" DefaultMode="Edit" CssClass="form-group">
                                <EditItemTemplate>

                                    <label for="txtCityName">City Name</label>
                                    <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control" Text='<%# Bind("Name") %>' Enabled="false" />

                                    <label for="txtCityCode">City Code</label>
                                    <asp:TextBox ID="txtCityCode" runat="server" CssClass="form-control" Text='<%# Bind("Code") %>' />

                                    <label for="ddlState">State</label>
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    </asp:DropDownList>

                                    <label for="txtCityCode">State Code</label>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Text='<%# Bind("Code") %>' />



                                    <label for="txtEditUser">Edit User</label>
                                    <asp:TextBox ID="txtEditUser" runat="server" CssClass="form-control" Text='<%# Bind("Edit_User") %>' Enabled="false" />

                                    <label for="txtEditDate">Edit Date</label>
                                    <asp:TextBox ID="txtEditDate" runat="server" CssClass="form-control" Text='<%# Bind("Edit_Date") %>' Enabled="false" />

                                    <br />
                                    <asp:Button ID="btnUpdateCityMaster" runat="server" Text="Update" CommandName="Update" CssClass="btn btn-primary btn-sm" />


                                </EditItemTemplate>
                            </asp:FormView>
                        </div>
                    </div>

                </div>

                <div class="col-lg-9">

                    <ul class="nav nav-tabs">
                        <li class="active"><a data-toggle="tab" href="#CityAreas">Below City Areas</a></li>
                        <li><a data-toggle="tab" href="#SupplierCityMapping">Supplier City Mapping</a></li>
                    </ul>

                    <div class="tab-content">
                        <div id="CityAreas" class="tab-pane fade in active">
                            <h4>City Areas</h4>
                            <div class="row">
                                <div class="col-lg-8">

                                    <asp:GridView ID="grdCityAreas" DataKeyNames="CityArea_Id" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="False" EmptyDataText="No City Areas defined" OnSelectedIndexChanged="grdCityAreas_SelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name" />
                                            <asp:BoundField HeaderText="Code" DataField="Code" SortExpression="Code" />
                                            <asp:CommandField ShowSelectButton="True" />
                                        </Columns>
                                    </asp:GridView>
                                    <br />

                                </div>
                                <div class="col-lg-4">

                                    <asp:FormView ID="frmCityArea" runat="server" DataKeyNames="CityArea_Id" DefaultMode="Insert" OnItemCommand="frmCityArea_ItemCommand" OnItemInserting="frmCityArea_ItemInserting">
                                        <InsertItemTemplate>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Add City Area</div>
                                                <div class="panel-body">

                                                    <label for="txtCityAreaName">Area Name</label>
                                                    <asp:TextBox ID="txtCityAreaName" runat="server" CssClass="form-control" />

                                                    <label for="txtCityAreaCode">Area Code</label>
                                                    <asp:TextBox ID="txtCityAreaCode" runat="server" CssClass="form-control" />


                                                    <br />
                                                    <asp:Button ID="btnCityArea" runat="server" Text="Add" CommandName="Add" CssClass="btn btn-primary btn-sm" />
                                                </div>
                                            </div>


                                        </InsertItemTemplate>

                                        <EditItemTemplate>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Update City Area</div>
                                                <div class="panel-body">

                                                    <label for="txtCityAreaName">Name</label>
                                                    <asp:TextBox ID="txtCityAreaName" runat="server" CssClass="form-control" Text='<%# Bind("Name") %>' />

                                                    <label for="txtCityAreaCode">Code</label>
                                                    <asp:TextBox ID="txtCityAreaCode" runat="server" CssClass="form-control" Text='<%# Bind("Code") %>' />


                                                    <br />
                                                    <asp:Button ID="btnCityArea" runat="server" Text="Save" CommandName="Save" CssClass="btn btn-primary btn-sm" />
                                                    <asp:Button ID="btnresetCityAreaLocation" runat="server" Text="Reset" CssClass="btn btn-primary btn-sm" OnClick="btnresetCityAreaLocation_Click" />

                                                </div>
                                            </div>

                                        </EditItemTemplate>

                                    </asp:FormView>


                                </div>
                            </div>

                            <h4>Area Locations</h4>

                            <div class="row" runat="server" id="dvCityAreaLocations" visible="false">
                                <div class="col-lg-8">

                                    <asp:GridView ID="grdCityAreaLocation" DataKeyNames="CityAreaLocation_Id" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="false" EmptyDataText="No City Areas Locations defined" OnSelectedIndexChanged="grdCityAreaLocation_SelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name" />
                                            <asp:BoundField HeaderText="Code" DataField="Code" SortExpression="Code" />
                                            <asp:CommandField ShowSelectButton="True" />
                                        </Columns>
                                    </asp:GridView>




                                </div>
                                <div class="col-lg-4" runat="server" id="dvCityAreaLocationDetail">

                                    <asp:FormView ID="frmCityAreaLocation" runat="server" DataKeyNames="CityAreaLocation_Id" DefaultMode="Insert" OnItemCommand="frmCityAreaLocation_ItemCommand">
                                        <InsertItemTemplate>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Add City Area Location </div>
                                                <div class="panel-body">

                                                    <label for="txtCityAreaName">Area Name</label>
                                                    <asp:TextBox ID="txtCityAreaName" runat="server" CssClass="form-control" />

                                                    <label for="txtCityAreaCode">Area Code</label>
                                                    <asp:TextBox ID="txtCityAreaCode" runat="server" CssClass="form-control" />


                                                    <br />
                                                    <asp:Button ID="btnCityAreaLocation" runat="server" Text="Add" CommandName="Add" CssClass="btn btn-primary btn-sm" />
                                                </div>
                                            </div>


                                        </InsertItemTemplate>

                                        <EditItemTemplate>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Update City Area Location</div>
                                                <div class="panel-body">

                                                    <label for="txtCityAreaName">Name</label>
                                                    <asp:TextBox ID="txtCityAreaName" runat="server" CssClass="form-control" Text='<%# Bind("Name") %>' />

                                                    <label for="txtCityAreaCode">Code</label>
                                                    <asp:TextBox ID="txtCityAreaCode" runat="server" CssClass="form-control" Text='<%# Bind("Code") %>' />


                                                    <br />
                                                    <asp:Button ID="btnCityAreaLocation" runat="server" Text="Save" CommandName="Save" CssClass="btn btn-primary btn-sm" />
                                                </div>
                                            </div>

                                        </EditItemTemplate>

                                    </asp:FormView>

                                </div>
                            </div>

                        </div>
                        <div id="SupplierCityMapping" class="tab-pane fade in">
                        </div>
                    </div>
                </div>
            </div>


        </div>



    </ContentTemplate>
</asp:UpdatePanel>
