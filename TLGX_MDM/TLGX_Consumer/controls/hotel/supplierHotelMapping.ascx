<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="supplierHotelMapping.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.supplierHotelMapping" %>




<div class="container">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel-group" id="accordion">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Existing Product Maps</a>
                        </div>
                    </div>

                    <div id="collapseSearch" class="panel-collapse collapse in">


        
                        <asp:GridView ID="grdExistingMaps" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped" DataKeyNames="Accommodation_ProductMapping_Id">
                            <Columns>
                                <asp:BoundField HeaderText="Supplier" DataField="SupplierName" />
                                <asp:BoundField HeaderText="Code" DataField="SupplierProductReference" />
                                <asp:BoundField HeaderText="HotelName" DataField="ProductName" />
                                <asp:BoundField HeaderText="Address" DataField="CityName" />
                                <asp:TemplateField HeaderText="Un Map">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <asp:LinkButton ID="lnkUnMap" runat="server" CommandName="UnMap" Text="UnMap" CssClass="btn btn-primary btn-sm" />

                    </div>
                </div>
            </div>
        </div>
    </div>


<div class="row">

    <div class="col-lg-3">
        <div class="panel panel-default">
            <div class="panel-heading">Search</div>
            <div class="panel-body">

                <div class="form-group">
                    <label for="txtHotelName">Hotel ID</label>
                    <asp:TextBox ID="txtHotelName" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <label for="txtHotelCity">City</label>
                    <asp:TextBox ID="txtHotelCity" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <label for="txtAddressLine1">Address</label>
                    <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <label for="txtPostalCode">Postal Code</label>
                    <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <label for="txtTelephone">Phone</label>
                    <asp:TextBox ID="txtTelephone" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <label for="ddlSupplierName">Supplier Name</label>
                    <asp:DropDownList runat="server" ID="ddlSupplierName" CssClass="form-control" AppendDataBoundItems="true" ValidationGroup="vldgrpFacility">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <asp:LinkButton ID="btnSearch" runat="server" CommandName="Search" Text="Search" CssClass="btn btn-primary btn-sm" OnCommand="btnSearch_Command" />
                <asp:LinkButton ID="btnReset" runat="server" CommandName="Reset" Text="Reset" CssClass="btn btn-primary btn-sm" />


            </div>
        </div>






    </div>


    <div class="col-lg-9">
        <div class="panel panel-default">
            <div class="panel-heading">Pending Maps</div>
            <div class="panel-body">

                <asp:GridView ID="grdPendingMaps" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped">
                    <Columns>
                                <asp:BoundField HeaderText="Supplier" DataField="SupplierName" />
                                <asp:BoundField HeaderText="Code" DataField="SupplierProductReference" />
                                <asp:BoundField HeaderText="HotelName" DataField="ProductName" />
                                <asp:BoundField HeaderText="Address" DataField="CityName" />
                                <asp:TemplateField HeaderText="Un Map">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:LinkButton ID="lnkAddMapping" runat="server" CommandName="UnMap" Text="Map" CssClass="btn btn-primary btn-sm" />






            </div>
        </div>



    </div>

</div>



</div>

