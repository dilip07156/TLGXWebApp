<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="bulkCityMapping.ascx.cs" Inherits="TLGX_Consumer.controls.staticdata.bulkCityMapping" %>


<style type="text/css">
    .HeaderFreez {
        position: relative;
        top: expression(this.offsetParent.scrollTop);
        z-index: 10;
    }
</style>

<asp:UpdatePanel ID="updCityBulkUpd" runat="server">
    <ContentTemplate>
        <div class="panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Supplier Search</a>
                    </h4>
                </div>

                <div id="collapseSearch" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div class="container">


                            <div class="form-group">
                                <label class="control-label col-sm-4" for="ddlSupplierName">Supplier Name</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <h4>Search Results (Total Count: <asp:Label ID="lblSupDumpCount" runat="server" Text="0"></asp:Label>)</h4>
        <br />
        <div class="panel panel-default" id="pnlSuppDump" runat="server">

            <div class="panel-heading">Supplier Dump</div>

            <div class="panel-body">

                <div class="row">
                    <div class="form-group">
                        <label class="control-label col-sm-1" for="ddlPageSize">Page Size</label>
                        <div class="col-sm-1">
                            <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control col-lg-2" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="grdSupplierDump" runat="server" DataKeyNames="Country_Id,CityMapping_Id" CssClass="table table-hover table-striped" AutoGenerateColumns="False"
                    AllowPaging="true" AllowCustomPaging="true" EmptyDataText="No UNMAPPED cities found" OnPageIndexChanging="grdSupplierDump_PageIndexChanging"
                    OnRowCommand="grdSupplierDump_RowCommand" OnSelectedIndexChanged="grdSupplierDump_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField HeaderText="CountryName" DataField="CountryName" />
                        <asp:BoundField HeaderText="CountryCode" DataField="CountryCode" />
                        <asp:BoundField HeaderText="State" />
                        <asp:BoundField HeaderText="StateCode" />
                        <asp:BoundField HeaderText="CityName" DataField="CityName" />
                        <asp:BoundField HeaderText="CityCode" DataField="CityCode" />
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                    Enabled="true" CommandArgument='<%# Bind("Country_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Select
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>

        <div class="panel panel-default" id="pnlCityMaster" runat="server">
            <div class="panel-heading">TLGX Masters (Total Count: <asp:Label ID="lblMasterDataCount" runat="server" Text="0"></asp:Label>)</div>

            <div class="panel-body">
                <div class="row">
                    <div class="form-group">
                        <label class="control-label col-sm-1" for="ddlPageSize">Page Size</label>
                        <div class="col-sm-1">
                            <asp:DropDownList ID="ddlPageSizeMaster" runat="server" CssClass="form-control col-lg-2" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSizeMaster_SelectedIndexChanged">
                                <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <label class="control-label col-sm-1" for="txtCityName">Search by City</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control col-lg-2" AutoPostBack="true" OnTextChanged="txtCityName_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <div class="col-sm-10">
                            <asp:Repeater ID="rptSupplierDump" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Alpha")%>' OnClick="lnkPage_Click" />&nbsp;
                                </ItemTemplate>

                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div style="height: 200px; overflow: scroll;">
                    <asp:GridView ID="grdTLGXMasters" runat="server" DataKeyNames="City_Id" CssClass="table table-hover table-striped" AutoGenerateColumns="False"
                        AllowPaging="true" AllowCustomPaging="true" EmptyDataText="No cities found" OnPageIndexChanging="grdTLGXMasters_PageIndexChanging"
                        PagerSettings-Position="TopAndBottom" OnRowCommand="grdTLGXMasters_RowCommand">
                        <Columns>
                            <asp:BoundField HeaderText="CountryName" DataField="CountryName" />
                            <asp:BoundField HeaderText="CountryCode" />
                            <asp:BoundField HeaderText="State" DataField="StateName" />
                            <asp:BoundField HeaderText="StateCode" DataField="StateCode" />
                            <asp:BoundField HeaderText="CityName" DataField="Name" />
                            <asp:BoundField HeaderText="CityCode" DataField="Code" />
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                        Enabled="true" CommandArgument='<%# Bind("City_Id") %>'>
                                            <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Map
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>


        <div class="panel panel-default" id="pnlCityMap" runat="server">
            <div class="panel-heading">Mapped Cities (Total Count: <asp:Label ID="lblMappedCount" runat="server" Text="0"></asp:Label>)</div>

            <div class="panel-body">
                <div class="row">
                    <div class="form-group">
                        <label class="control-label col-sm-1" for="ddlPageSize">Page Size</label>
                        <div class="col-sm-1">
                            <asp:DropDownList ID="ddlPageSizeMapped" runat="server" CssClass="form-control col-lg-2" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSizeMapped_SelectedIndexChanged">
                                <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <asp:GridView ID="grdCityMaps" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False"
                        EmptyDataText="No Static Updates" CssClass="table table-hover table-striped" DataKeyNames="CityMapping_Id"
                        OnSelectedIndexChanged="grdCityMaps_SelectedIndexChanged" OnPageIndexChanging="grdCityMaps_PageIndexChanging"
                        OnRowCommand="grdCityMaps_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="MapId" HeaderText="Map Id" />
                            <asp:BoundField DataField="SupplierName" HeaderText="Name" />
                            <asp:BoundField DataField="CountryCode" HeaderText="Country Code" />
                            <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                            <asp:BoundField DataField="CityCode" HeaderText="City Code" />
                            <asp:BoundField DataField="CityName" HeaderText="City Name" />


                            <asp:BoundField DataField="MasterCountryCode" HeaderText="Country Code" />
                            <asp:BoundField DataField="MasterCountryName" HeaderText="Country Name" />
                            <asp:BoundField HeaderText="State" />
                            <asp:BoundField DataField="MasterCityCode" HeaderText="City Code" />
                            <asp:BoundField DataField="Master_CityName" HeaderText="City Name" />

                            <asp:BoundField DataField="Status" HeaderText="Status" />
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Remove" CssClass="btn btn-default"
                                        Enabled="true" CommandArgument='<%# Bind("CityMapping_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Remove Map
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>



                    </asp:GridView>
                </div>
            </div>
        </div>


    </ContentTemplate>
</asp:UpdatePanel>
