<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="supplierApiLocation.ascx.cs" Inherits="TLGX_Consumer.controls.businessentities.supplierApiLocation" %>
<script type="text/javascript">

    function showDetailsModal() {
        $("#moApiInfo").modal('show');
    }
</script>

  <asp:GridView ID="gvNewTabData" runat="server" AllowPaging="True" AllowCustomPaging="true"
                            EmptyDataText="No Data Found" CssClass="table table-hover table-striped"
                            AutoGenerateColumns="false" OnPageIndexChanging="gvNewTabData_PageIndexChanging"
                            OnRowCommand="gvNewTabData_RowCommand" DataKeyNames="SupplierImportFile_Id,Supplier_Id" OnRowDataBound="gvNewTabData_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="Entity" DataField="Supplier" />
                                <asp:BoundField HeaderText="Path" DataField="Entity" />
                                <asp:BoundField HeaderText="Audit Files" DataField="Files" />
                                <asp:BoundField HeaderText="Status" DataField="STATUS" />
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Edit" CssClass="btn btn-default" Enabled="true"  OnClientClick='<%# "showDetailsModal('\''"+ Convert.ToString(Eval("SupplierImportFile_Id")) + "'\'');" %>'>
                                            <span aria-hidden="true">Edit</span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                            CssClass="btn btn-default" CommandArgument='<%# Bind("SupplierImportFile_Id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat" %>'></span>
                                                    <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
<div class="modal fade" id="moApiInfo" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <div class="input-group">
                    <h4 class="input-group-addon"><strong>Add / Reset data </strong></h4>
                </div>
            </div>
            <div class="modal-body">
              <div class="col-sm-12">
                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="ddlEntityList"> Entity </label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlEntityList" runat="server"  OnSelectedIndexChanged="ddlEntityList_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                                        <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                              <div class="form-group row">
                                <label class="control-label col-sm-4" for="txtpath">
                                    Path
                                </label>
                                <div class="col-sm-8">
                                  <input type="text" id="txtpath" value=""" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="ddlStatusList">
                                    Status
                                </label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlStatusList" runat="server"  OnSelectedIndexChanged="ddlSupplierList_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                                        <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                  <div class="col-sm-6">
                      <input  type="button" id="btnSave" value="Save"/>
                  </div>
                  <div class="col-sm-6">
                     <input  type="button" id="btnReset" value="Reset"/> 
                  </div>
              </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
