<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddressCheck.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.AddressCheck" %>

<div class="row">

    <div class="col-lg-6">
        <div class="panel panel-default">

            <div class="panel-body">

                <div class="form-group">
                    <label class="control-label col-sm-6" for="txtStreet">
                        Street
                    </label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtStreet" runat="server" class="form-control" Enabled="false" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-sm-6" for="txtStreet2">Street 2</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtStreet2" runat="server" class="form-control" Enabled="false" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-sm-6" for="txtStreet3">Street 3</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtStreet3" runat="server" class="form-control" Enabled="false" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-sm-6" for="txtStreet4">Street 4</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtStreet4" runat="server" class="form-control" Enabled="false" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-sm-6" for="txtStreet5">Street 5</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtStreet5" runat="server" class="form-control" Enabled="false" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-sm-6" for="txtSuburbs">Suburbs/Downtown</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtSuburbs" runat="server" class="form-control" Enabled="false" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-sm-6" for="txtPostalCode">
                        Postal Code
                    </label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtPostalCode" runat="server" class="form-control" Enabled="false" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-sm-6" for="txtCountry">
                        Country
                    </label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtCountry" runat="server" class="form-control" Enabled="false" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-sm-6" for="txtState">State / Region</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtState" runat="server" class="form-control" Enabled="false" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-sm-6" for="txtCity">
                        City
                    </label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtCity" runat="server" class="form-control" Enabled="false" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-sm-6" for="txtArea">
                        Area
                    </label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtArea" runat="server" class="form-control" Enabled="false" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-sm-6" for="txtLocation">
                        Location
                    </label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtLocation" runat="server" class="form-control" Enabled="false" />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div class="col-lg-6">

        <div class="panel panel-default">

            <div class="panel-heading">Geo Lookup</div>

            <div class="panel-body">

                <div class="form-group">

                    <label class="control-label col-sm-4" for="txtStreet">
                        Country
                    </label>

                    <div class="col-sm-4">
                        <asp:TextBox ID="TextBox1" runat="server" class="form-control" Enabled="false" />
                    </div>

                    <div class="col-sm-4">
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                            <asp:ListItem>Location</asp:ListItem>
                            <asp:ListItem>Area</asp:ListItem>
                            <asp:ListItem>Something else</asp:ListItem>
                        </asp:DropDownList>
                    </div>


                </div>

                <div class="form-group">
                    <label class="control-label col-sm-4" for="txtStreet">
                        Sub Locaility 2
                    </label>

                    <div class="col-sm-4">
                        <asp:TextBox ID="TextBox2" runat="server" class="form-control" Enabled="false" />
                    </div>

                    <div class="col-sm-4">
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                            <asp:ListItem>Location</asp:ListItem>
                            <asp:ListItem>Area</asp:ListItem>
                            <asp:ListItem>Something else</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <asp:LinkButton ID="btnUpadteAddress" runat="server" CausesValidation="True" CommandName="UpdateAddress" Text="Update Address" CssClass="btn btn-primary btn-sm" />

                </div>

            </div>
        </div>
    </div>

    

</div>
