<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="keywordManager.ascx.cs" Inherits="TLGX_Consumer.controls.keywords.keywordManager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
    var count;
    function showModal() {
        $("#moKeywordMapping").modal('show');
        count = 1;
    }
    function closeModal() {
        $("#moKeywordMapping").modal('hide');
    }
    function AddTextBox() {
        document.getElementById('<%=hdnFieldTotalTextboxes.ClientID%>').value = count;
        $("#AliasTextBox").append("<div><input type='text' class='form-control' id='DynamicTextBox" + count +"' name='DynamicTxt"+ count +"'/><br></div>");
        count++;
    };
    function RemoveTextBox() {
        $("#AliasTextBox").children().last().remove();
        count--;
        alert('count=' + count);
    };

</script>


<asp:UpdatePanel ID="updatepnl" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hdnFieldTotalTextboxes" Value="1" runat="server" />
        <div class="container">
            <div class="panel-group" id="accordion">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Filters</a>
                        </h4>
                    </div>
                    <div id="collapseSearch" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <form class="form-horizontal">
                                <div class="row">
                                    <div class="col-lg-6">

                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtKeyword">System Word</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtKeyword" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtAlias">Alias</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtAlias" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlStatus">Status</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true">
                                                    <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>


                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-10">
                                            <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary btn-sm pull-right" Text="Search" OnClick="btnSearch_Click" />
                                            <asp:Button runat="server" ID="btnReset" CssClass="btn btn-primary btn-sm pull-right" Text="Reset" OnClick="btnReset_Click" />
                                            <asp:Button runat="server" ID="btnAddNew" CssClass="btn btn-primary btn-sm pull-right" Text="Add New" OnClick="btnAddNew_Click" OnClientClick="showModal();" />
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div id="dvMsg" runat="server" style="display: none;"></div>
                <div class="form-group pull-right">
                    <div class="input-group" runat="server" id="divDropdownForEntries">
                        <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                        <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                            <asp:ListItem Value="10" >10</asp:ListItem>
                            <asp:ListItem Value="15" >15</asp:ListItem>
                            <asp:ListItem Value="20" >20</asp:ListItem>
                            <asp:ListItem Value="25" >25</asp:ListItem>
                            <asp:ListItem Value="30" >30</asp:ListItem>
                            <asp:ListItem Value="35" >35</asp:ListItem>
                            <asp:ListItem Value="40" >40</asp:ListItem>
                            <asp:ListItem Value="45" >45</asp:ListItem>
                            <asp:ListItem Value="50" >50</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <div class="container">
            <div class="panel-group" id="accordion1">
                <div class="panel panel-default">
                    <div class="panel-heading clearfix">
                        <h4 class="panel-title pull-left">
                            <a data-toggle="collapse" data-parent="#accordion1" href="#collapseSearchResult">Search Results (Total Count:<asp:Label ID="lblTotalCount" runat="server" Text="0"></asp:Label>)</a>
                            <%--<asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</a>--%>
                        </h4>
                    </div>

                    <div id="collapseSearchResult" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <div class="col-lg-12">
                                <asp:GridView ID="gvSearchResult" runat="server" 
                                    EmptyDataText="No data for search conditions" CssClass="table table-striped" AllowPaging="true" AllowCustomPaging="True" AutoGenerateColumns="false"
                                    DataKeyNames="Keyword_Id,KeywordAlias_Id" OnRowCommand="gvSearchResult_RowCommand" OnPageIndexChanging="gvSearchResult_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="Keyword" HeaderText="Keyword" />
                                        <asp:BoundField DataField="Value" HeaderText="Alias" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-default" Enabled="true" CommandArgument='<%# Bind("KeywordAlias_Id")%>' OnClientClick="showModal();" CommandName="EditKeyWordMgmr">
                                            <span aria-hidden="true">Edit</span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-default" Enabled="true">
                                            <span aria-hidden="true">Delete</span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </div>
                            <%--<table class="table table-striped">
                        <thead>
                            <tr>
                                <th>Keyword</th>
                                <th>Alias</th>
                                <th>Status</th>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>

                        <tbody>
                            <tr>
                                <td>Single</td>
                                <td>SNG</td>
                                <td>Active</td>
                                <td>
                                    <asp:Button ID="Button5" runat="server" CssClass="btn btn-default " Text="Edit" />
                                </td>
                                <td>
                                    <asp:Button ID="Button6" runat="server" CssClass="btn btn-default " Text="Delete" />
                                </td>
                            </tr>
                            <tr>
                                <td>Single</td>
                                <td>SING</td>
                                <td>Active</td>
                                <td>
                                    <asp:Button ID="Button7" runat="server" CssClass="btn btn-default " Text="Edit" />
                                </td>
                                <td>
                                    <asp:Button ID="Button9" runat="server" CssClass="btn btn-default " Text="Delete" />
                                </td>

                            </tr>

                            <tr>
                                <td>Single</td>
                                <td>SGL</td>
                                <td>Active</td>
                                <td>
                                    <asp:Button ID="Button8" runat="server" CssClass="btn btn-default " Text="Edit" />
                                </td>
                                <td>
                                    <asp:Button ID="Button10" runat="server" CssClass="btn btn-default " Text="Delete" />
                                </td>

                            </tr>

                        </tbody>
                    </table>
                        <asp:Button ID="Button11" runat="server" CssClass="btn btn-default " Text="1" ValidationGroup="HotelSearch" />
                    <asp:Button ID="Button12" runat="server" CssClass="btn btn-default " Text="2" ValidationGroup="HotelSearch" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>





<!-- Add MODAL -->
<div class="modal fade" id="moKeywordMapping" role="dialog">
    <div class="modal-dialog modal-md">
        <div class="modal-content">

            <div class="modal-header">
                <h4 class="panel-title">Add New Keyword Mapping</h4>
            </div>

            <div class="modal-body">
                <asp:UpdatePanel ID="pnlupdate" runat="server">
                    <ContentTemplate>
                        <asp:FormView ID="frmAddKeyword" runat="server" DefaultMode="Insert" DataKeyNames="Keyword_Id,AliasKeywordAlias_Id">
                            <InsertItemTemplate>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">System Keyword</div>
                                            <div class="panel-body">

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="txtAddKeyword">System Word</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtAddKeyword" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">System Alias</div>
                                            <div class="panel-body">
                                                <%--<div class="form-group row">
                                                    <table class="table" id="maintable">
                                                        <thead>
                                                            <tr>
                                                                <th>Alias</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr class="data-keyword-Alias">
                                                                <td>
                                                                    <input type="text" name="Alias1" class="form-control Alias01" /></td>
                                                                    <textarea id="txtAddAlias" runat="server" class="form-control"></textarea>
                                                                <td>
                                                                    <button type="button" id="btnAddRow" class="btn btn-xs classAdd">+</button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>--%>
                                                <%--   <label class="control-label col-sm-4" for="txtAddAlias">Alias</label>
                                                    <div id="dvAlias" class="col-sm-4">
                                                        <asp:TextBox ID="txtAddAlias" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>--%>

                                                <%-- <div class="col-sm-4">
                                                        <button id="addButton" type="button" onclick="AddTextBox();" class="btn btn-default">
                                                            <span class="glyphicons glyphicon-plus"></span>
                                                        </button>

                                                        <button id="removeButton" type="button" class="btn btn-default">
                                                            <span class="glyphicons glyphicon-minus"></span>
                                                        </button>

                                                    </div>
                                                </div>--%>
                                                <asp:Repeater ID="RepeatTextbox" runat="server">
                                                    <HeaderTemplate>
                                                        <table>
                                                            <th>System Alias</th>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <input type="text" class="form-control" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4">System Alias</label>
                                                    <input id="btnAddNewTextBox" type="button" value="+" class="btn btn-xs" onclick="AddTextBox();" />
                                                    <input id="btnRemoveNewTextBox" type="button" value="-" class="btn btn-xs" onclick="RemoveTextBox();" />
                                                    <input id="DynamicTextBox" runat="server" type="text" class="form-control" />
                                                    <br />

                                                    <div id="AliasTextBox">
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class=" pull-right">
                                            <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-sm btn-primary" Text="Add" OnClick="btnAdd_Click" />
                                            <asp:Button runat="server" ID="btnClose" CssClass="btn btn-sm btn-primary" Text="Close" data-dismiss="modal" />
                                        </div>

                                    </div>

                                </div>
                            </InsertItemTemplate>
                            <EditItemTemplate>
                                <div class="row">

                                    <div class="col-lg-6">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">System Keyword</div>
                                            <div class="panel-body">

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="txtAddKeyword">System Word</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtAddKeyword" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">System Alias</div>
                                            <div id="dvpnlbody" class="panel-body">

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="txtAddAlias">Alias</label>
                                                    <div id="dvAlias" class="col-sm-4">
                                                        <textarea id="txtAddAlias" runat="server" class="form-control"></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class=" pull-right">
                                            <asp:Button runat="server" ID="btnUpdate" CssClass="btn btn-sm btn-primary" Text="Update" OnClick="btnUpdate_Click" />
                                            <asp:Button runat="server" ID="btnClose" CssClass="btn btn-sm btn-primary" Text="Close" data-dismiss="modal" />
                                        </div>
                                    </div>
                                </div>
                            </EditItemTemplate>
                        </asp:FormView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>


<!-- Edit MODAL -->
<%--<div id="moCityMapping">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">

            <div class="modal-header">
                <div class="panel panel-default">
                    <h4 class="modal-title">Edit Keyword Mapping</h4>
                </div>
            </div>
            <div class="modal-body">

                <div class="row">

                    <div class="col-lg-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">System Keyword</div>
                            <div class="panel-body">

                                <div class="form-group row">
                                    <label class="control-label col-sm-4" for="txtAddKeyword">System Word</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Text="Sample Keyword"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>



                    <div class="col-lg-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">System Alias</div>
                            <div class="panel-body">

                                <div class="form-group row">
                                    <label class="control-label col-sm-4" for="txtAddAlias">Alias</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Text="Sample Alias"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-4">




                                        <button type="button" class="btn btn-default">
                                            <span class="glyphicons glyphicon-plus"></span>
                                        </button>

                                        <button type="button" class="btn btn-default">
                                            <span class="glyphicons glyphicon-minus"></span>
                                        </button>

                                    </div>
                                </div>



                            </div>
                        </div>

                        <div class=" pull-right">
                            <asp:Button runat="server" ID="Button3" CssClass="btn btn-sm btn-primary" Text="Update" />
                            <asp:Button runat="server" ID="Button4" CssClass="btn btn-sm btn-primary" Text="Close" />
                        </div>

                    </div>

                </div>

            </div>
        </div>
    </div>
</div>--%>



