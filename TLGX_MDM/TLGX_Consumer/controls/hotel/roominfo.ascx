<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="roominfo.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.roominfo" %>
<%@ Register Src="~/controls/hotel/RoomAmenities.ascx" TagPrefix="uc1" TagName="RoomAmenities" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script src="../../Scripts/JqueryUI/jquery-ui.js"></script>
<link href="../../Scripts/JqueryUI/jquery-ui.css" rel="stylesheet" />
<script type="text/javascript">
    $(document).ready(function () {
        callajax();
    });
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        callajax();
    });
    function callajax() {
        $("#MainContent_roominfo_frmRoomInfo_txtRoomCategory").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '../../Service/RoomCategoryAutoComplete.ashx',
                    dataType: "json",
                    data: {
                        term: request.term
                    },
                    success: function (data) {
                        response(data);
                    }
                });
            },
            min_length: 3,
            delay: 300
        });
    }
    function closemoNewRoomDefinition() {
        $("#moNewRoomDefinition").modal('hide');
    }
    function showmoNewRoomDefinition() {
        $("#moNewRoomDefinition").modal('show');
    }
    function InIEventNewRoomDefinition() {
        CloseNewRoomDefinition();
    }
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEventNewRoomDefinition);
    function CloseNewRoomDefinition() {
        var hv = $('#MainContent_roominfo_hdnFlagCopyRoomInfo').val();
        if (hv == "true") {
            closemoNewRoomDefinition();
        }
        $('#MainContent_roominfo_hdnFlagCopyRoomInfo').val("false");
    }
</script>
<asp:UpdatePanel ID="updPanRoomInfo" runat="server">

    <ContentTemplate>
        <div id="dvMsg" runat="server" style="display: none;"></div>
        <div class="container">
            <div class="form-group">
                <div class="col-sm-12">
                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldRoomInfo" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                </div>
            </div>
        </div>

        <asp:FormView ID="frmRoomInfo" runat="server" DataKeyNames="Accommodation_RoomInfo_Id" DefaultMode="Insert" OnItemCommand="frmRoomInfo_ItemCommand" OnDataBound="frmRoomInfo_DataBound">
            <InsertItemTemplate>
                <div class="container">
                    <div class="panel panel-default">
                        <div class="panel-heading">Add Room Definition</div>
                        <div class="panel-body">
                            <div class="row">

                                <div class="col-lg-8">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Room Details</div>
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <label class="control-label-mand col-sm-4" for="txtRoomCategory">
                                                    Room Category
                                                    <asp:RequiredFieldValidator ID="vldtxtFrom" runat="server" ControlToValidate="txtRoomCategory" ErrorMessage="Please enter room category" Text="*" ValidationGroup="vldRoomInfo" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtRoomCategory" runat="server" CssClass="form-control" />
                                                </div>


                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-4" for="txtRoomName">Room Name</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtRoomName" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-4" for="txtNumberOfRooms">Number of Rooms</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtNumberOfRooms" runat="server" CssClass="form-control" />
                                                    <cc1:FilteredTextBoxExtender ID="axfte_txtNumberOfRooms" runat="server" FilterType="Numbers" TargetControlID="txtNumberOfRooms" />
                                                </div>

                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-4" for="ddlCompanyRoomCategory">Company Room Category</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlCompanyRoomCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="-Select-"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-4" for="txtRoomDescription">Room Description</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtRoomDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                                                </div>
                                            </div>
                                            <div class="form-group" style="visibility: hidden;">
                                                <label class="control-label col-sm-4" for="txtRoomInfo_Id"></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtRoomInfo_Id" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="panel panel-default">
                                        <div class="panel-heading">Room Amenities</div>
                                        <div class="panel-body">

                                            <p>Please save the Room before adding Amenities</p>


                                        </div>

                                    </div>
                                    <br />
                                    <asp:LinkButton ID="lnkAddRoom" CommandName="Add" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="true" ValidationGroup="vldRoomInfo">Add New Definition</asp:LinkButton>

                                </div>

                                <div class="col-lg-4">

                                    <div class="panel panel-default">
                                        <div class="panel-heading">Room Location</div>
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="txtFloorName">Floor Name</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtFloorName" runat="server" class="form-control" />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="txtFloorNumber">
                                                    Floor Number
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtFloorNumber" runat="server" class="form-control" />
                                                    <cc1:FilteredTextBoxExtender ID="axfte_txtFloorNumber" runat="server" FilterType="Numbers" TargetControlID="txtFloorNumber" />
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="panel panel-default">
                                        <div class="panel-heading">Room Attributes</div>
                                        <div class="panel-body">

                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="txtRoomView">Room View</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtRoomView" runat="server" class="form-control" />
                                                </div>
                                            </div>


                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="txtRoomDecor">Room Decor</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtRoomDecor" runat="server" class="form-control" />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="ddlBedType">Bed Type</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlBedType" runat="server" class="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="-Select-"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="ddlBathRoomType">Bathroom Type</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlBathRoomType" runat="server" class="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="-Select-"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="ddlSmoking">Smoking?</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlSmoking" runat="server" class="form-control">
                                                        <asp:ListItem Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="txtRoomSize">Room Size</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtRoomSize" runat="server" class="form-control" />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="txtInterconnectRooms"># Inter/Rooms</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtInterconnectRooms" runat="server" class="form-control" />
                                                    <cc1:FilteredTextBoxExtender ID="axfte_txtInterconnectRooms" runat="server" FilterType="Numbers" TargetControlID="txtInterconnectRooms" />
                                                </div>
                                            </div>


                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </InsertItemTemplate>

            <EditItemTemplate>

                <div class="container">
                    <div class="panel panel-default">
                        <div class="panel-heading">Add Room Definition</div>
                        <div class="panel-body">
                            <div class="row">

                                <div class="col-lg-8">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Room Details</div>
                                        <div class="panel-body">

                                            <div class="form-group">
                                                <label class="control-label-mand col-sm-4" for="txtRoomCategory">Room Category</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtRoomCategory" runat="server" CssClass="form-control" Text='<%# Bind("RoomCategory") %>' />
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:RequiredFieldValidator ID="vldtxtFrom" runat="server" ControlToValidate="txtRoomCategory" ErrorMessage="Please enter room category" Text="*" ValidationGroup="vldRoomInfo" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                </div>


                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-4" for="txtRoomName">Room Name</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtRoomName" runat="server" CssClass="form-control" Text='<%# Bind("RoomName") %>' />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label-mand col-sm-4" for="txtNumberOfRooms">
                                                    Number of Rooms
                                                    <%--<asp:RequiredFieldValidator ID="vldtxtNumberOfRooms" runat="server" ControlToValidate="txtNumberOfRooms" ErrorMessage="Please enter number of rooms" Text="*" ValidationGroup="vldRoomInfo" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtNumberOfRooms" runat="server" CssClass="form-control" TextMode="Number" Text='<%# Bind("NoOfRooms") %>' />
                                                    <cc1:FilteredTextBoxExtender ID="axfte_txtNumberOfRooms" runat="server" FilterType="Numbers" TargetControlID="txtNumberOfRooms" />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-4" for="ddlCompanyRoomCategory">Company Room Category</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlCompanyRoomCategory" runat="server" class="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="-Select-"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-4" for="txtRoomDescription">Room Description</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtRoomDescription" runat="server" class="form-control" TextMode="MultiLine" Rows="5" Text='<%# Bind("Description") %>' />
                                                </div>
                                            </div>

                                            <div class="form-group" style="visibility: hidden;">
                                                <label class="control-label col-sm-4" for="txtRoomInfo_Id"></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtRoomInfo_Id" runat="server" CssClass="form-control" Text='<%# Bind("Accommodation_RoomInfo_Id") %>' />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="panel panel-default">
                                        <div class="panel-heading">Room Amenities</div>
                                        <div class="panel-body">

                                            <uc1:RoomAmenities runat="server" ID="myRoomAmenities" />

                                        </div>

                                    </div>
                                    <br />
                                    <asp:LinkButton ID="lnkAddRoom" CommandName="Save" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="true" ValidationGroup="vldRoomInfo">Save Room Definition</asp:LinkButton>

                                </div>

                                <div class="col-lg-4">

                                    <div class="panel panel-default">
                                        <div class="panel-heading">Room Location</div>
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="txtFloorName">Floor Name</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtFloorName" runat="server" class="form-control" Text='<%# Bind("FloorName") %>' />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="txtFloorNumber">Floor Number</label>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtFloorNumber" runat="server" class="form-control" Text='<%# Bind("FloorNumber") %>' />
                                                    <cc1:FilteredTextBoxExtender ID="axfte_txtFloorNumber" runat="server" FilterType="Numbers" TargetControlID="txtFloorNumber" />
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="panel panel-default">
                                        <div class="panel-heading">Room Attributes</div>
                                        <div class="panel-body">

                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="txtRoomView">Room View</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtRoomView" runat="server" class="form-control" Text='<%# Bind("RoomView") %>' />
                                                </div>
                                            </div>


                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="txtRoomDecor">Room Decor</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtRoomDecor" runat="server" class="form-control" Text='<%# Bind("RoomDecor") %>' />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="ddlBedType">Bed Type</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlBedType" runat="server" class="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="-Select-"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="ddlBathRoomType">Bathroom Type</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlBathRoomType" runat="server" class="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="-Select-"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="ddlSmoking">Smoking?</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlSmoking" runat="server" class="form-control">
                                                        <asp:ListItem Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="false"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="txtRoomSize">Room Size</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtRoomSize" runat="server" class="form-control" Text='<%# Bind("RoomSize") %>' />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-sm-6" for="txtInterconnectRooms"># Inter/Rooms</label>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtInterconnectRooms" runat="server" class="form-control" Text='<%# Bind("NoOfInterconnectingRooms") %>' />
                                                    <cc1:FilteredTextBoxExtender ID="axfte_txtInterconnectRooms" runat="server" FilterType="Numbers" TargetControlID="txtInterconnectRooms" />
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>


            </EditItemTemplate>


        </asp:FormView>

        <br />

        <div class="container">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Existing Hotel Rooms
                      
                </div>
                <div class="panel-body">
                    <div class="form-group pull-right">
                        <div class="input-group">
                            <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                            <asp:DropDownList ID="ddlShowEntries" runat="server" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                <asp:ListItem>5</asp:ListItem>
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
                    <asp:GridView ID="grdRoomTypes" runat="server" AutoGenerateColumns="False" DataKeyNames="Accommodation_RoomInfo_Id" CssClass="table table-hover table-striped" AllowPaging="True" EmptyDataText="Not Hotel Rooms defined for this Product" PageSize="5" AllowCustomPaging="true" OnPageIndexChanging="grdRoomTypes_PageIndexChanging" OnRowCommand="grdRoomTypes_SelectedIndexChanged" OnRowDataBound="grdRoomTypes_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="RoomId" HeaderText="Id" SortExpression="RoomId" />
                            <asp:BoundField DataField="RoomCategory" HeaderText="Category" SortExpression="RoomCategory" />
                            <asp:BoundField DataField="RoomSize" HeaderText="Size" SortExpression="RoomSize" />
                            <asp:BoundField DataField="BathRoomType" HeaderText="BathRoom" SortExpression="BathRoomType" />
                            <asp:BoundField DataField="RoomDecor" HeaderText="Decor" SortExpression="RoomDecor" />
                            <asp:BoundField DataField="Smoking" HeaderText="Smoking" SortExpression="Smoking" />

                            <%--                            <asp:ButtonField Text="Select" CommandName="Select">
                                <ControlStyle CssClass="btn btn-primary btn-sm" />
                            </asp:ButtonField>--%>

                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default" Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Accommodation_RoomInfo_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>' CssClass="btn btn-default" CommandArgument='<%# Bind("Accommodation_RoomInfo_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnCopy" runat="server" CausesValidation="false" CommandName="CopyRoomTypes" CssClass="btn btn-default" Enabled='<%# Eval("IsActive") %>' OnClientClick="showmoNewRoomDefinition();" CommandArgument='<%# Bind("Accommodation_RoomInfo_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Copy
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <PagerStyle CssClass="pagination-ys" HorizontalAlign="Left" />
                    </asp:GridView>

                </div>
            </div>
        </div>
    </ContentTemplate>

</asp:UpdatePanel>

<div class="modal fade" id="moNewRoomDefinition" role="dialog">
    <div class="modal-dialog modal-md">
        <div class="modal-content">
            <div class="modal-header">
                Copy and Create New Room Definition 
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpdCountryMapModal" runat="server">
                    <ContentTemplate>
                        <div id="dvmsgCopyStatus" runat="server" style="display: none;"></div>

                        <asp:HiddenField ID="hdnFlagCopyRoomInfo" runat="server" Value="" EnableViewState="false" />
                        <div class="row">
                            <div class="col-md-9">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="VGRoomDef" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="control-label-mand col-sm-4" for="txtRoomCategory">
                                New Room Category
                                            <asp:RequiredFieldValidator ID="vddlRoomCategory" runat="server" ErrorMessage="Please enter room category" Text="*"
                                                ControlToValidate="txtRoomCategory" CssClass="text-danger" ValidationGroup="VGRoomDef"></asp:RequiredFieldValidator>
                            </label>
                            <div class="col-sm-8">
                                <asp:HiddenField ID="hdnAccommodation_RoomInfo_Id" runat="server" Value="" />
                                <asp:TextBox ID="txtRoomCategory" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <asp:LinkButton ID="btnCopyRoomDef" runat="server" ValidationGroup="VGRoomDef" CausesValidation="True" Text="Create" OnClick="btnCopyRoomDef_Click" CssClass="btn btn-primary btn-sm" />
                        <asp:LinkButton ID="btnResetRoomDef" runat="server" CausesValidation="false" Text="Reset" OnClick="btnResetRoomDef_Click" CssClass="btn btn-primary btn-sm" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

