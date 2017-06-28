<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="head.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.head" %>
<asp:FormView ID="FormView1" runat="server" DataKeyNames="idSTG_Accommodation" DefaultMode="Edit">
  
    <ItemTemplate>

        <div class="form-inline">

           

            <div class="form-group">
                <label for="idSTG_AccommodationLabel">Hotel ID</label>
                <asp:Label ID="idSTG_AccommodationLabel" runat="server" Text='<%# Eval("idSTG_Accommodation") %>' CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="CompanyNameLabel">Company Name</label>
                <asp:Label ID="CompanyNameLabel" runat="server" Text='<%# Bind("CompanyName") %>' CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="CompanyHotelIDLabel">Company Hotel ID</label>
                <asp:Label ID="CompanyHotelIDLabel" runat="server" Text='<%# Bind("CompanyHotelID") %>' CssClass="form-control"  />
            </div>
            
    <!--        <div class="form-group">
                <label for="CommonHotelIDLabel">Common Hotel ID</label>
                <asp:Label ID="CommonHotelIDLabel" runat="server" Text='<%# Bind("CommonHotelID") %>'  CssClass="form-control" />
            </div>-->

            <div class="form-group">
                <label for="HotelNameLabel">
                    Name</label>
                <asp:Label ID="HotelNameLabel" runat="server" Text='<%# Bind("HotelName") %>' CssClass="form-control" />
            </div>


</div>



    </ItemTemplate>
</asp:FormView>
