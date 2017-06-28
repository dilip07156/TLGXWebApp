<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="productsinproject.ascx.cs" Inherits="TLGX_Consumer.controls.projects.productsinproject" %>

<br />
<h4>Current Products </h4>

<!-- grid to show list of current members --><!-- currently not in entity -->
<asp:GridView ID="grdProductsInProject" runat="server" CssClass="table table-hover table-striped" EmptyDataText="No Products Currently in Project">
    <Columns>
        <asp:BoundField HeaderText="ID" DataField="CompanyHotelID" />
        <asp:BoundField HeaderText="Name" DataField="HotelName" />
        <asp:BoundField HeaderText="Address" DataField="StreetName" />
        <asp:BoundField HeaderText="City" DataField="City" />
        <asp:BoundField HeaderText="Country" DataField="Country" />
        <asp:BoundField HeaderText="Status" DataField="CurrentStatus" />
    </Columns>
</asp:GridView>



<br />

    <div class="panel panel-default">
            <div class="panel-heading">Add New Products</div>
            <div class="panel-body">
            
                <!-- drop down list shows Accounts Names that are NOT IN PROJECT -->


                <div class="row">

                    <div class="col-lg-6">
                        <label for="ddlProductType">Product Type</label>
                                <asp:DropDownList runat="server" ID="ddlProductType" CssClass="form-control">
                                    <asp:ListItem Selected="True">-Select-</asp:ListItem>
                                    <asp:ListItem>Hotels</asp:ListItem>
                                </asp:DropDownList>

                                <label for="txtNameFilter">Name</label>
                <asp:TextBox ID="txtNameFilter" runat="server" CssClass="form-control"></asp:TextBox>
                
                <br />
                            <asp:Button ID="btnSearchProducts" runat="server" Text="Search Products" CssClass="btn btn-primary btn-sm" />
                        <!-- search should NOT SHOW products already in project -->
                    </div>

                    <div class="col-lg-6">


                       <!-- contents of this column will be PRODUCT SPECIFIC only hotels shown statically at the moment, but this will need to be changed -->
                        <label for="ddlCountry">Country</label>
                                <asp:DropDownList runat="server" ID="ddlCountry" CssClass="form-control">
                                    <asp:ListItem Selected="True">-Select-</asp:ListItem>
                                  </asp:DropDownList>

                        <label for="ddlCity">City</label>
                                <asp:DropDownList runat="server" ID="ddlCity" CssClass="form-control">
                                    <asp:ListItem Selected="True">-Select-</asp:ListItem>    
                                </asp:DropDownList>

                        <label for="ddlStars">Stars</label>
                                <asp:DropDownList runat="server" ID="ddlStars" CssClass="form-control">
                                    <asp:ListItem Selected="True">-Select-</asp:ListItem>   
                                </asp:DropDownList>

                        <label for="ddlChain">Chain</label>
                                <asp:DropDownList runat="server" ID="ddlChain" CssClass="form-control">
                                    <asp:ListItem Selected="True">-Select-</asp:ListItem>   
                                </asp:DropDownList>


                    </div>

                    <br />


                </div>


                <!-- needs tick box to add to project -->
                <asp:GridView ID="grdProductSearchResults" runat="server" CssClass="table table-hover table-striped" AllowPaging="true" PageSize="20">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="CompanyHotelID" />
                        <asp:BoundField HeaderText="Name" DataField="HotelName" />
                        <asp:BoundField HeaderText="Address" DataField="StreetName" />
                        <asp:BoundField HeaderText="City" DataField="City" />
                        <asp:BoundField HeaderText="Country" DataField="Country" />
                        <asp:CheckBoxField HeaderText="Add" />
                    </Columns>
                </asp:GridView>

                <br /> 
                    <asp:Button ID="btnAddSelected" runat="server" Text="Add Selected" CssClass="btn btn-primary btn-sm" /> 
                    <asp:Button ID="btnAddAll" runat="server" Text="Add All Products" CssClass="btn btn-primary btn-sm" /> 
                

                


             </div>
     </div>