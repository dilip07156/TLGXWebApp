<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="passengeroccupancy.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.passengeroccupancy" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
    function closeAddUpdateOccupancy() {
        $("#moOccupancy").modal('hide');
    }
    function showAddUpdateOccupancy() {
        $("#moOccupancy").modal('show');
    }
    function InIEventOccupancy() {
        ClosePopupOccupancy();
    }
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEventOccupancy);
    function ClosePopupOccupancy() {
        var hv = $('#MainContent_passengeroccupancy_hdnFlag').val();
        if (hv == "true") {
            closeAddUpdateOccupancy();
        }
        $('#MainContent_passengeroccupancy_hdnFlag').val("false");
    }
</script>
<asp:UpdatePanel ID="udpPaxOccupancy" runat="server">
    <ContentTemplate>
        <div id="dvMsg" runat="server" style="display: none;"></div>
        <div class="panel panel-default">
            <div class="panel-heading">

                <div class="row">
                    <div class="col-sm-6">
                        <strong>Room Occupancy</strong>
                    </div>
                    <div class="col-sm-6">
                        <asp:Button runat="server" ID="btnNewCreate" OnClick="btnNewCreate_Click" OnClientClick="showAddUpdateOccupancy();" CssClass="btn btn-sm btn-primary pull-right" Text="Create New Occupancy" />
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <asp:GridView ID="grdOccupanyDetail" runat="server" AutoGenerateColumns="False" DataKeyNames="Accommodation_PaxOccupancy_Id" CssClass="table table-hover table-striped" EmptyDataText="No Passenger Occupancy Set" OnRowCommand="grdOccupanyDetail_RowCommand" OnDataBound="grdOccupanyDetail_DataBound" OnRowDataBound="grdOccupanyDetail_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="category" HeaderText="Category" SortExpression="category" />
                        <asp:BoundField DataField="RoomType" HeaderText="RoomType" SortExpression="RoomType" />
                        <asp:BoundField DataField="MaxAdults" HeaderText="MaxAdults" SortExpression="MaxAdults" />
                         <asp:BoundField DataField="FromAgeForExtraBed" HeaderText="From Age" SortExpression="FromAgeForExtraBed" />
                        <asp:BoundField DataField="ToAgeForExtraBed" HeaderText="To Age" SortExpression="ToAgeForExtraBed" />
                        <asp:BoundField DataField="MaxPaxWithExtraBed" HeaderText="Max Pax" SortExpression="MaxPaxWithExtraBed" />
                        <asp:BoundField DataField="MaxCNB" HeaderText="Max Pax" SortExpression="MaxCNB" />
                        <asp:BoundField DataField="FromAgeForCNB" HeaderText="From Age" SortExpression="FromAgeForCNB" />
                        <asp:BoundField DataField="ToAgeForCNB" HeaderText="To Age" SortExpression="ToAgeForCNB" />
                        <asp:BoundField DataField="MaxChild" HeaderText="Max Pax" SortExpression="MaxChild" />
                        <asp:BoundField DataField="FromAgeForCIOR" HeaderText="From Age" SortExpression="FromAgeForCIOR" />
                        <asp:BoundField DataField="ToAgeForCIOR" HeaderText="To Age" SortExpression="ToAgeForCIOR" />
                        <asp:BoundField DataField="MaxPax" HeaderText="Total Pax" SortExpression="MaxPax" />
                        <asp:BoundField DataField="Legacy_Htl_Id" HeaderText="Hotel ID" SortExpression="Legacy_Htl_Id" Visible="false" />
                        <asp:BoundField DataField="Accommodation_RoomInfo_Id" HeaderText="RoomInfo ID" SortExpression="Accommodation_RoomInfo_Id" Visible="false" />
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default" OnClientClick="showAddUpdateOccupancy();"
                                    Enabled='<%# (bool)Eval("IsActive") && (bool)Eval("IsRoomActive") %>' CommandArgument='<%# Bind("Accommodation_PaxOccupancy_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                    CssClass="btn btn-default" CommandArgument='<%# Bind("Accommodation_PaxOccupancy_Id") %>' Enabled='<%# Eval("IsRoomActive") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="modal fade" id="moOccupancy" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                Add Update Occupancy
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpdCountryMapModal" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
                            <div class="row">
                                <div class="col-md-9">
                                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="HotelOcc" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                </div>
                            </div>
                        <asp:FormView ID="frmPassengerOCcupancy" runat="server" DataKeyNames="Accommodation_PaxOccupancy_Id" DefaultMode="Insert" OnItemCommand="frmPassengerOCcupancy_ItemCommand">
                            <InsertItemTemplate>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Room</div>
                                                <div class="panel-body">
                                                    <div class="form-group row">
                                                        <label class="control-label-mand col-sm-4" for="ddlRoomCategory">
                                                            Category
                                            <asp:RequiredFieldValidator ID="vddlRoomCategory" runat="server" ErrorMessage="Please select room category" Text="*"
                                                ControlToValidate="ddlRoomCategory" InitialValue="0" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlRoomCategory" runat="server" class="form-control" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label-mand col-sm-4" for="ddlRoomType">
                                                            Room Type
                                            <asp:RequiredFieldValidator ID="vddlRoomType" runat="server" ErrorMessage="Please select room type" Text="*"
                                                ControlToValidate="ddlRoomType" InitialValue="0" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlRoomType" runat="server" class="form-control" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label-mand col-sm-4" for="txtMaxAdults">
                                                            Max Adults
                                            <asp:RequiredFieldValidator ID="vtxtMAxAdults" runat="server" ErrorMessage="Please enter max adults count" Text="*"
                                                ControlToValidate="txtMAxAdults" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rvtxtMAxAdults" runat="server" ErrorMessage="Invalid max adults count" Text="*"
                                                                ControlToValidate="txtMAxAdults" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtMAxAdults" runat="server" class="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftxtMAxAdults" runat="server" FilterType="Numbers" TargetControlID="txtMAxAdults" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label-mand col-sm-4" for="txtPassengers">
                                                            Max Pax
                                            <asp:RequiredFieldValidator ID="vtxtPassengers" runat="server" ErrorMessage="Please enter max passenger count" Text="*"
                                                ControlToValidate="txtPassengers" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rvtxtPassengers" runat="server" ErrorMessage="Invalid max passenger count" Text="*"
                                                                ControlToValidate="txtPassengers" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtPassengers" runat="server" class="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftxtPassengers" runat="server" FilterType="Numbers" TargetControlID="txtPassengers" />
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Extra Bed</div>
                                                <div class="panel-body">

                                                    <div class="form-group row">
                                                        <label class="control-label-mand col-sm-4" for="txtFromCXBAge">
                                                            Child From
                                            <asp:RequiredFieldValidator ID="vtxtFromCXBAge" runat="server" ErrorMessage="Please enter extra bed child from age" Text="*" ControlToValidate="txtFromCXBAge"
                                                CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rvtxtFromCXBAge" runat="server" ErrorMessage="Invalid extra bed child from age" Text="*" ControlToValidate="txtFromCXBAge"
                                                                ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtFromCXBAge" runat="server" class="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftxtFromCXBAge" runat="server" FilterType="Numbers" TargetControlID="txtFromCXBAge" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label-mand col-sm-4" for="txtToCXBAge">
                                                            Child To
                                            <asp:RequiredFieldValidator ID="vtxtToCXBAge" runat="server" ErrorMessage="Please enter extra bed child to age" Text="*"
                                                ControlToValidate="txtToCXBAge" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rvtxtToCXBAge" runat="server" ErrorMessage="Invalid extra bed child to age" Text="*"
                                                                ControlToValidate="txtToCXBAge" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtToCXBAge" runat="server" class="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftxtToCXBAge" runat="server" FilterType="Numbers" TargetControlID="txtToCXBAge" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label-mand col-sm-4" for="txtMaxCXB">
                                                            Max Pax
                                            <asp:RequiredFieldValidator ID="vtxtMaxCXB" runat="server" ErrorMessage="Please enter extra bed max pax count" Text="*"
                                                ControlToValidate="txtMaxCXB" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rvtxtMaxCXB" runat="server" ErrorMessage="Invalid extra bed max pax count" Text="*"
                                                                ControlToValidate="txtMaxCXB" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtMaxCXB" runat="server" class="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftxtMaxCXB" runat="server" FilterType="Numbers" TargetControlID="txtMaxCXB" />
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Child in Own Room</div>
                                                <div class="panel-body">

                                                    <div class="form-group row">
                                                        <label class="control-label-mand col-sm-4" for="txtFromCIORAge">
                                                            Child From
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please enter child in own room from age" Text="*"
                                                ControlToValidate="txtFromCIORAge" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid child in own room from age" Text="*"
                                                                ControlToValidate="txtFromCIORAge" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtFromCIORAge" runat="server" class="form-control" />
                                                            <cc1:FilteredTextBoxExtender ID="ftxtFromCIORAge" runat="server" FilterType="Numbers" TargetControlID="txtFromCIORAge" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label-mand col-sm-4" for="txtToCIORAge">
                                                            Child To
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please enter child in own room to age" Text="*"
                                                ControlToValidate="txtToCIORAge" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Invalid child in own room to age" Text="*"
                                                                ControlToValidate="txtToCIORAge" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtToCIORAge" runat="server" class="form-control" />
                                                            <cc1:FilteredTextBoxExtender ID="ftxtToCIORAge" runat="server" FilterType="Numbers" TargetControlID="txtToCIORAge" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label-mand col-sm-4" for="txtMaxCIOR">
                                                            Max CIOR
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please enter child in own room max pax count" Text="*"
                                                ControlToValidate="txtMaxCIOR" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Invalid child in own room max pax count" Text="*"
                                                                ControlToValidate="txtMaxCIOR" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtMaxCIOR" runat="server" class="form-control" />
                                                            <cc1:FilteredTextBoxExtender ID="ftxtMaxCIOR" runat="server" FilterType="Numbers" TargetControlID="txtMaxCIOR" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Child No Bed</div>
                                                <div class="panel-body">

                                                    <div class="form-group row">
                                                        <label class="control-label-mand col-sm-4" for="txtFromCNBAge">
                                                            Child From
                                            <asp:RequiredFieldValidator ID="vtxtFromCNBAge" runat="server" ErrorMessage="Please enter child no bed from age" Text="*"
                                                ControlToValidate="txtFromCNBAge" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rvtxtFromCNBAge" runat="server" ErrorMessage="Invalid child no bed from age" Text="*"
                                                                ControlToValidate="txtFromCNBAge" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtFromCNBAge" runat="server" class="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftxtFromCNBAge" runat="server" FilterType="Numbers" TargetControlID="txtFromCNBAge" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label-mand col-sm-4" for="txtToCNBAge">
                                                            Child To
                                            <asp:RequiredFieldValidator ID="vtxtToCNBAge" runat="server" ErrorMessage="Please enter child no bed to age" Text="*" ControlToValidate="txtToCNBAge"
                                                CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rvtxtToCNBAge" runat="server" ErrorMessage="Invalid child no bed to age" Text="*"
                                                                ControlToValidate="txtToCNBAge" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtToCNBAge" runat="server" class="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftxtToCNBAge" runat="server" FilterType="Numbers" TargetControlID="txtToCNBAge" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label-mand col-sm-4" for="txtMaxCNB">
                                                            Max CNB
                                            <asp:RequiredFieldValidator ID="vtxtMaxCNB" runat="server" ErrorMessage="Please enter child no bed max pax count" Text="*"
                                                ControlToValidate="txtMaxCNB" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rvtxtMaxCNB" runat="server" ErrorMessage="Invalid child no bed max pax count" Text="*"
                                                                ControlToValidate="txtMaxCNB" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtMaxCNB" runat="server" class="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftxtMaxCNB" runat="server" FilterType="Numbers" TargetControlID="txtMaxCNB" />
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <asp:LinkButton ID="btnAdd" runat="server" ValidationGroup="HotelOcc" CausesValidation="True" CommandName="AddOccupancy" Text="Add Occupancy" CssClass="btn btn-primary btn-sm" />
                                    </div>
                                </div>
                            </InsertItemTemplate>
                            <EditItemTemplate>
                                <div class="row col-md-12">
                                    <div class="col-md-6">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">Room</div>
                                            <div class="panel-body">
                                                <div class="form-group" style="visibility: hidden">
                                                    <label class="control-label col-sm-4" for="txtRoomInfoId">
                                                        Room Info
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtRoomInfoId" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label-mand col-sm-4" for="ddlRoomCategory">
                                                        Category
                                            <asp:RequiredFieldValidator ID="vddlRoomCategory" runat="server" ErrorMessage="Please select room category" Text="*"
                                                ControlToValidate="ddlRoomCategory" InitialValue="0" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlRoomCategory" runat="server" class="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Text="-Select-"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label-mand col-sm-4" for="ddlRoomType">
                                                        Type
                                            <asp:RequiredFieldValidator ID="vddlRoomType" runat="server" ErrorMessage="Please select room type" Text="*"
                                                ControlToValidate="ddlRoomType" InitialValue="0" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlRoomType" runat="server" class="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Text="-Select-"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label-mand col-sm-4" for="txtMaxAdults">
                                                        Max Adults
                                            <asp:RequiredFieldValidator ID="vtxtMAxAdults" runat="server" ErrorMessage="Please enter max adults count" Text="*"
                                                ControlToValidate="txtMAxAdults" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rvtxtMAxAdults" runat="server" ErrorMessage="Invalid max adults count" Text="*"
                                                            ControlToValidate="txtMAxAdults" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtMAxAdults" runat="server" class="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="ftxtMAxAdults" runat="server" FilterType="Numbers" TargetControlID="txtMAxAdults" />
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label-mand col-sm-4" for="txtPassengers">
                                                        Max Passengers
                                            <asp:RequiredFieldValidator ID="vtxtPassengers" runat="server" ErrorMessage="Please enter max passenger count" Text="*"
                                                ControlToValidate="txtPassengers" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rvtxtPassengers" runat="server" ErrorMessage="Invalid max passenger count" Text="*"
                                                            ControlToValidate="txtPassengers" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtPassengers" runat="server" class="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="ftxtPassengers" runat="server" FilterType="Numbers" TargetControlID="txtPassengers" />
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">Extra Bed</div>
                                            <div class="panel-body">

                                                <div class="form-group row">
                                                    <label class="control-label-mand col-sm-4" for="txtFromCXBAge">
                                                        Child From
                                            <asp:RequiredFieldValidator ID="vtxtFromCXBAge" runat="server" ErrorMessage="Please enter extra bed child from age" Text="*" ControlToValidate="txtFromCXBAge"
                                                CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rvtxtFromCXBAge" runat="server" ErrorMessage="Invalid extra bed child from age" Text="*" ControlToValidate="txtFromCXBAge"
                                                            ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtFromCXBAge" runat="server" class="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="ftxtFromCXBAge" runat="server" FilterType="Numbers" TargetControlID="txtFromCXBAge" />
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label-mand col-sm-4" for="txtToCXBAge">
                                                        Child To
                                            <asp:RequiredFieldValidator ID="vtxtToCXBAge" runat="server" ErrorMessage="Please enter extra bed child to age" Text="*"
                                                ControlToValidate="txtToCXBAge" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rvtxtToCXBAge" runat="server" ErrorMessage="Invalid extra bed child to age" Text="*"
                                                            ControlToValidate="txtToCXBAge" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtToCXBAge" runat="server" class="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="ftxtToCXBAge" runat="server" FilterType="Numbers" TargetControlID="txtToCXBAge" />
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label-mand col-sm-4" for="txtMaxCXB">
                                                        Max Pax
                                            <asp:RequiredFieldValidator ID="vtxtMaxCXB" runat="server" ErrorMessage="Please enter extra bed max pax count" Text="*"
                                                ControlToValidate="txtMaxCXB" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rvtxtMaxCXB" runat="server" ErrorMessage="Invalid extra bed max pax count" Text="*"
                                                            ControlToValidate="txtMaxCXB" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtMaxCXB" runat="server" class="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="ftxtMaxCXB" runat="server" FilterType="Numbers" TargetControlID="txtMaxCXB" />
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">Child in Own Room</div>
                                            <div class="panel-body">

                                                <div class="form-group row">
                                                    <label class="control-label-mand col-sm-4" for="txtFromCIORAge">
                                                        Child From
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please enter child in own room from age" Text="*"
                                                ControlToValidate="txtFromCIORAge" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid child in own room from age" Text="*"
                                                            ControlToValidate="txtFromCIORAge" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtFromCIORAge" runat="server" class="form-control" />
                                                        <cc1:FilteredTextBoxExtender ID="ftxtFromCIORAge" runat="server" FilterType="Numbers" TargetControlID="txtFromCIORAge" />
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label-mand col-sm-4" for="txtToCIORAge">
                                                        Child To
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please enter child in own room to age" Text="*"
                                                ControlToValidate="txtToCIORAge" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Invalid child in own room to age" Text="*"
                                                            ControlToValidate="txtToCIORAge" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtToCIORAge" runat="server" class="form-control" />
                                                        <cc1:FilteredTextBoxExtender ID="ftxtToCIORAge" runat="server" FilterType="Numbers" TargetControlID="txtToCIORAge" />
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label-mand col-sm-4" for="txtMaxCIOR">
                                                        Max CIOR
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please enter child in own room max pax count" Text="*"
                                                ControlToValidate="txtMaxCIOR" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Invalid child in own room max pax count" Text="*"
                                                            ControlToValidate="txtMaxCIOR" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtMaxCIOR" runat="server" class="form-control" />
                                                        <cc1:FilteredTextBoxExtender ID="ftxtMaxCIOR" runat="server" FilterType="Numbers" TargetControlID="txtMaxCIOR" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">Child No Bed</div>
                                            <div class="panel-body">

                                                <div class="form-group row">
                                                    <label class="control-label-mand col-sm-4" for="txtFromCNBAge">
                                                        Child From
                                            <asp:RequiredFieldValidator ID="vtxtFromCNBAge" runat="server" ErrorMessage="Please enter child no bed from age" Text="*"
                                                ControlToValidate="txtFromCNBAge" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rvtxtFromCNBAge" runat="server" ErrorMessage="Invalid child no bed from age" Text="*"
                                                            ControlToValidate="txtFromCNBAge" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtFromCNBAge" runat="server" class="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="ftxtFromCNBAge" runat="server" FilterType="Numbers" TargetControlID="txtFromCNBAge" />
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label-mand col-sm-4" for="txtToCNBAge">
                                                        Child To
                                            <asp:RequiredFieldValidator ID="vtxtToCNBAge" runat="server" ErrorMessage="Please enter child no bed to age" Text="*" ControlToValidate="txtToCNBAge"
                                                CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rvtxtToCNBAge" runat="server" ErrorMessage="Invalid child no bed to age" Text="*"
                                                            ControlToValidate="txtToCNBAge" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtToCNBAge" runat="server" class="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="ftxtToCNBAge" runat="server" FilterType="Numbers" TargetControlID="txtToCNBAge" />
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label-mand col-sm-4" for="txtMaxCNB">
                                                        Max CNB
                                            <asp:RequiredFieldValidator ID="vtxtMaxCNB" runat="server" ErrorMessage="Please enter child no bed max pax count" Text="*"
                                                ControlToValidate="txtMaxCNB" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rvtxtMaxCNB" runat="server" ErrorMessage="Invalid child no bed max pax count" Text="*"
                                                            ControlToValidate="txtMaxCNB" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOcc"></asp:RegularExpressionValidator>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtMaxCNB" runat="server" class="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="ftxtMaxCNB" runat="server" FilterType="Numbers" TargetControlID="txtMaxCNB" />
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <asp:LinkButton ID="btnUpdate" runat="server" ValidationGroup="HotelOcc" CausesValidation="True" CommandName="UpdateOccupancy" Text="Update" CssClass="btn btn-primary btn-sm" />
                                </div>
                            </EditItemTemplate>
                        </asp:FormView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

