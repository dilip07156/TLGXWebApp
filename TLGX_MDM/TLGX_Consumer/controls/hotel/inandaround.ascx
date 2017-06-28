<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="inandaround.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.inandaround" %>
<%@ Register Src="~/controls/hotel/googlePlacesLookup.ascx" TagPrefix="uc1" TagName="googlePlacesLookup" %>
<script type="text/javascript">
    function closeAddNewLookUPModal() {
        $("#moAddnearbyplace").modal('hide');
    }
    function showAddNewLookUPModal() {
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

<asp:UpdatePanel runat="server" ID="updPanNEaryby">
    <ContentTemplate>
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
                                    <div class="form-group">
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

                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="txtDescription">Description</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Text='<%# Bind("Description") %>' />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="txtPlaceName">Place Name</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtPlaceName" runat="server" CssClass="form-control" Text='<%# Bind("PlaceName") %>' />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="txtDistance">Distance</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDistance" runat="server" CssClass="form-control" Text='<%# Bind("DistanceFromProperty") %>' />
                                        </div>
                                    </div>

                                    <div class="form-group">
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
                <div class="panel-body">
                    <asp:GridView ID="grdInAndAround" runat="server" AutoGenerateColumns="False" DataKeyNames="Accommodation_NearbyPlace_Id" CssClass="table table-hover table-striped" EmptyDataText="There are no Nearby Places for this property" OnRowCommand="grdInAndAround_RowCommand" OnRowDataBound="grdInAndAround_RowDataBound">
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
                    </asp:GridView>
                </div>
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
