<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="inandaround.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.inandaround" %>
<%@ Register Src="~/controls/hotel/googlePlacesLookup.ascx" TagPrefix="uc1" TagName="googlePlacesLookup" %>
<script type="text/javascript">
    function closeAddNewLookUPModal() {
        $("#moAddnearbyplace").modal('destroy');
    }
    function showAddNewLookUPModal() {
        debugger;
        var elementsddlPlaceCategory = document.getElementById("MainContent_inandaround_googlePlacesLookup_ddlPlaceCategory").options;
        for (var i = 0; i < elementsddlPlaceCategory.length; i++) {
            elementsddlPlaceCategory[i].selected = false;
        }
        var elementsddlNoOfItem = document.getElementById("MainContent_inandaround_googlePlacesLookup_ddlNoOfItem").options;
        for (var i = 0; i < elementsddlNoOfItem.length; i++) {
            elementsddlNoOfItem[i].selected = false;
        }
        elementsddlNoOfItem[3].selected = true;
        var elementsddlRadius = document.getElementById("MainContent_inandaround_googlePlacesLookup_ddlRadius").options;
        for (var i = 0; i < elementsddlRadius.length; i++) {
            elementsddlRadius[i].selected = false;
        }
        elementsddlRadius[3].selected = true;
        $('#divResult').hide();
        $("#moAddnearbyplace").modal('show');
    }
    function pageLoad(sender, args) {
        var hv = $('#hdnFlagFornearbyPlace').val();
        if (hv == "true") {
            closeAddUpdateUserModal();
        }
        $('#hdnFlagFornearbyPlace').val("false");
    }
</script>
<style>
    .hide {
        display: none;
    }
</style>

<asp:UpdatePanel runat="server" ID="updPanNEaryby">
    <ContentTemplate>
        <div id="msgSuccessful" style="display: none;">
            <script type="text/javascript">
                //setTimeout(function () { $("#msgSuccessful").fadeTo(500, 0).slideUp(500) }, 3000);
            </script>
            <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a><strong>Success!</strong> <span>In adn Around place has been added successfully.
        </div>
        <div id="dvMsg" runat="server" style="display: none;"></div>
        <asp:FormView ID="frmLandmark" runat="server" DataKeyNames="Accommodation_NearbyPlace_Id" DefaultMode="Insert" OnItemCommand="frmLandmark_ItemCommand">
            <HeaderTemplate>
                <div class="container">
                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldInAndAround" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                </div>
            </HeaderTemplate>
            <InsertItemTemplate>
                <div class="container">
                    <div class="panel panel-default">
                        <div class="panel-heading">Add Nearby Place</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-group row">
                                        <label class="control-label-mand col-sm-6" for="ddlPlaceCategory">
                                            Place Category
                                            <asp:RequiredFieldValidator ID="vldddlPlaceCategory" CssClass="text-danger" ControlToValidate="ddlPlaceCategory"
                                                InitialValue="0" runat="server" ErrorMessage="Please select place category" Text="*" ValidationGroup="vldInAndAround"></asp:RequiredFieldValidator></label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlPlaceCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="control-label col-sm-6" for="txtDescription">Description</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-6" for="txtPlaceName">Place Name</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtPlaceName" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="control-label col-sm-6" for="txtDistance">Distance</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDistance" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="control-label-mand col-sm-6" for="ddlPlaceCategory">
                                            Distance Measure
                                            <asp:RequiredFieldValidator ID="vldddlUnitOfMeasure" InitialValue="0" ControlToValidate="ddlUnitOfMeasure" runat="server"
                                                ErrorMessage="Please select unit of measure" Text="*" ValidationGroup="vldInAndAround" CssClass="text-danger"></asp:RequiredFieldValidator></label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlUnitOfMeasure" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <asp:LinkButton ID="btnAdd" runat="server" CausesValidation="true" ValidationGroup="vldInAndAround" CommandName="Add" Text="Add" CssClass="btn btn-primary btn-sm" />
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </InsertItemTemplate>
            <EditItemTemplate>
                <div class="container">
                    <div class="panel panel-default">
                        <div class="panel-heading">Update Nearby Place</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-group row">
                                        <label class="control-label-mand col-sm-6" for="ddlPlaceCategory">
                                            Place Category
                                            <asp:RequiredFieldValidator ID="vldddlPlaceCategory" ControlToValidate="ddlPlaceCategory" runat="server" ErrorMessage="Please select place category" Text="*"
                                                ValidationGroup="vldInAndAround"></asp:RequiredFieldValidator></label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlPlaceCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-6" for="txtDescription">Description</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Text='<%# Bind("Description") %>' />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-6" for="txtPlaceName">Place Name</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtPlaceName" runat="server" CssClass="form-control" Text='<%# Bind("PlaceName") %>' />
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-6" for="txtDistance">Distance</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDistance" runat="server" CssClass="form-control" Text='<%# Bind("DistanceFromProperty") %>' />
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label-mand col-sm-6" for="ddlPlaceCategory">
                                            Distance Measure
                                            <asp:RequiredFieldValidator ID="vldddlUnitOfMeasure" ControlToValidate="ddlUnitOfMeasure" runat="server" ErrorMessage="Please select unit od measure" Text="*"
                                                InitialValue="0" ValidationGroup="vldInAndAround"></asp:RequiredFieldValidator></label>

                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlUnitOfMeasure" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <asp:LinkButton ID="btnAdd" runat="server" CausesValidation="true" ValidationGroup="vldInAndAround" CommandName="Modify" Text="Modify" CssClass="btn btn-primary btn-sm" />
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

            </EditItemTemplate>

        </asp:FormView>

        <div class="container">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-sm-6">
                            <strong>In and Around</strong>
                        </div>
                        <div class="col-sm-6">
                            <asp:Button runat="server" ID="btnAddNewLookUP" OnClick="btnAddNewLookUP_Click" OnClientClick="showAddNewLookUPModal();" CssClass="btn btn-sm btn-primary pull-right" Text="Add Near By Places" />
                        </div>
                    </div>
                </div>
                <%--<asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>--%>
                <div class="panel-body">
                    <asp:Button ID="btnRefreshGrid" runat="server" CssClass="hide" OnClick="btnRefreshGrid_Click" />
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
                    <asp:GridView ID="grdInAndAround" runat="server" AutoGenerateColumns="False" AllowPaging="true" AllowCustomPaging="true" OnPageIndexChanging="grdInAndAround_PageIndexChanging" DataKeyNames="Accommodation_NearbyPlace_Id" CssClass="table table-hover table-striped" EmptyDataText="There are no Nearby Places for this property" OnRowCommand="grdInAndAround_RowCommand" OnRowDataBound="grdInAndAround_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="PlaceCategory" HeaderText="PlaceCategory" SortExpression="PlaceCategory" />
                            <asp:BoundField DataField="PlaceName" HeaderText="PlaceName" SortExpression="PlaceName" />
                            <asp:BoundField DataField="DistanceFromProperty" HeaderText="DistanceFromProperty" SortExpression="DistanceFromProperty" />
                            <asp:BoundField DataField="DistanceUnit" HeaderText="DistanceUnit" SortExpression="DistanceUnit" />
                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                        Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Accommodation_NearbyPlace_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                        CssClass="btn btn-default" CommandArgument='<%# Bind("Accommodation_NearbyPlace_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pagination-ys" BorderStyle="None" />
                    </asp:GridView>
                </div>
                <%--   </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="modal fade" id="moAddnearbyplace" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">Near By Places Lookup</h4>
            </div>
            <div class="modal-body">
                <asp:HiddenField ID="hdnFlagFornearbyPlace" runat="server" ClientIDMode="Static" />
                <uc1:googlePlacesLookup runat="server" ID="googlePlacesLookup" />
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
