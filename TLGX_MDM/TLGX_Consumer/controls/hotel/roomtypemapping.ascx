<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="roomtypemapping.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.roomtypemapping" %>




<div class="container">
    <div class="row">
        <div class="col-lg-12">



            <div class="panel-group" id="accordion">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapExistingMaps">Existing Room Type Maps</a>
                        </div>
                    </div>

                    <div id="collapExistingMaps" class="panel-collapse collapse in">

<asp:GridView ID="grdExistingMaps" runat="server" DataKeyNames="Accommodation_SupplierRoomTypeMapping_Id" AutoGenerateColumns="false" 
    CssClass="table table-hover table-striped" EmptyDataText="No Supplier Rooms for this Product">
    <Columns>
        <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" />
        <asp:BoundField DataField="SupplierRoomCategory" HeaderText="Category" />
        <asp:BoundField DataField="SupplierRoomTypeCode" HeaderText="Room Type Code" />
        <asp:BoundField DataField="SupplierRoomId" HeaderText="Supplier Room Id" />
        <asp:BoundField HeaderText="System Room" />
        <asp:ButtonField CommandName="Update" Text="Update" />
        
    </Columns>
</asp:GridView>



<br />
                <asp:LinkButton ID="lnkUnMap" runat="server" CommandName="UnMap" Text="UnMap" CssClass="btn btn-primary btn-sm" />
            </div>

                    </div>
</div>
            </div>
</div>
</div>
<h4>Unmapped Supplier Data</h4>
<hr />
<div class="container">
 <div class="row">

            <div class="col-md-4">
                <h4>Filter</h4>
                <p>Filter Supplier Room Types</p>
                <div class="form-group form-inline">
                    <asp:DropDownList ID="ddlSelectSupplier" runat="server" CssClass="form-control input-sm" DataTextField="SupplierName" DataValueField="SupplierName" AppendDataBoundItems="True" >
                        <asp:ListItem Value="%" Selected="True">Select Supplier</asp:ListItem>
                    </asp:DropDownList>

                    
                </div>
                <div class="form-group form-inline">
                    <asp:TextBox ID="txtRoomTypeNameFilter" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-primary btn-sm" Text="Filter" />
                </div>

            </div>

            <div class="col-md-4">
                <h4>Create Base Rooms</h4>
                <p>From Supplier</p>
                <div class="form-group form-inline">

                    <asp:DropDownList ID="ddlSelectBaseSupplier" runat="server" CssClass="form-control input-sm" DataTextField="SupplierName" DataValueField="SupplierName" AppendDataBoundItems="True" >
                        <asp:ListItem Value="%" Selected="True">Select Supplier</asp:ListItem>
                    </asp:DropDownList>

                    <asp:Button ID="btnBuildBaseRooms" runat="server" CssClass="btn btn-primary btn-sm" Text="Straight Build" />
                    <asp:Button ID="btnSuggestBaseRooms" runat="server" CssClass="btn btn-primary btn-sm" Text="Suggest" />
                </div>
            </div>
            
            <div class="col-md-4">
                <h4>Auto Map against Base Rooms</h4>
                <p>Confirmed Against Hotel / Preferred Supplier</p>
                <div class="form-group form-inline">
                <asp:DropDownList ID="ddlBaseRoomType" runat="server" CssClass="form-control input-sm" AppendDataBoundItems="True" DataTextField="RoomCategory" DataValueField="idBaseRooms">
                <asp:ListItem Value="%" Selected="True">Select Base</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnMapAgainstBase" runat="server" CssClass="btn btn-primary btn-sm" Text="Map vs Base" />
                </div>
            </div>

            
            
        </div>

<asp:GridView ID="grdUnMappedSupplierRooms" runat="server" DataKeyNames="Accommodation_SupplierRoomTypeMapping_Id" AutoGenerateColumns="false" CssClass="table table-hover table-striped" EmptyDataText="No Supplier Rooms for this Product">
    <Columns>
        <asp:BoundField DataField="SupplierRoomCategory" HeaderText="Category" />
        <asp:BoundField DataField="SupplierRoomTypeCode" HeaderText="Room Type Code" />
        <asp:BoundField DataField="SupplierRoomId" HeaderText="Supplier Room Id" />
        <asp:ButtonField CommandName="Compare" Text="Compare" />
        
    </Columns>
</asp:GridView>


