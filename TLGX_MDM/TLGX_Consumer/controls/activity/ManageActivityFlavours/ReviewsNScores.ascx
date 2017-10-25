<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReviewsNScores.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.ReviewsNScores" %>


<script type="text/javascript">
    function showAddNewRevAndScoreModal() {
        $("#moAddReviewAndScore").modal('show');
    }
    //function closeAddNewActivityModal() {
    //    $("#moAddNewActivityModal").modal('hide');
    //}
    //function page_load(sender, args) {
    //    closeAddNewActivityModal();
    //}
</script>
<asp:UpdatePanel ID="updPanRevAndScore" runat="server">
    <ContentTemplate>
        <div id="dvMsg" runat="server" style="display: none;"></div>

        <div class="container">
            <div class="form-group col-md-12">
                <asp:LinkButton ID="btnNewActivity" runat="server" Text="Add New" CssClass="btn btn-primary btn-sm pull-right" OnClientClick="showAddNewRevAndScoreModal();" /><%--OnClick="btnNewActivity_Click"--%>
            </div>
        </div>

        <headertemplate>
            <div class="container">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vldGrpRules" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
            </div>
        </headertemplate>

        <h4 class="panel-title pull-left">
            <a data-toggle="collapse" data-parent="#searchResult" href="#collapseSearchResult">Review And Score (Total Count:
                            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</a></h4>
        <%--<asp:Button CssClass="pull-right btn btn-primary" runat="server" ID="btnNewUpload" Text="Add New" OnClientClick="showAddNewRevAndScoreModal()" />--%>
        <div class="col-lg-3 pull-right">
            <div class="form-group pull-right">
                <div class="input-group" runat="server" id="div1">
                    <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <%--<div class="panel panel-default">--%>
        <%--<div class="panel-body">--%>

        <asp:GridView ID="grdRevAndScore" runat="server" AllowPaging="true" AllowCustomPaging="true" AutoGenerateColumns="False" DataKeyNames="Activity_Flavour_Id"
            EmptyDataText="No Data Found" CssClass="table table-hover table-striped" OnRowCommand="grdRevAndScore_RowCommand"
            OnRowDataBound="grdRevAndScore_RowDataBound">

            <Columns>
                <asp:BoundField DataField="Review_Type" HeaderText="Review Type" SortExpression="Review_Type" />
                <asp:BoundField DataField="Review_Title" HeaderText="Review Title" SortExpression="Review_Title" />
                <asp:BoundField DataField="Review_Description" HeaderText="Review Description" SortExpression="Review_Description" />
                <asp:BoundField DataField="Review_Score" HeaderText="Review Score" SortExpression="Review_Score" />
                <asp:BoundField DataField="Review_Author" HeaderText="Review Author" SortExpression="Review_Author" />
                <asp:TemplateField ShowHeader="false">

                    <ItemTemplate>
                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                            Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Activity_ReviewsAndScores_Id") %>' OnClientClick="showAddNewRevAndScoreModal();">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                            CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_ReviewsAndScores_Id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat" %>'></span>
                                                    <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>

        <%--</div>--%>
        <%--</div>--%>
    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="moAddReviewAndScore" role="dialog">
    <div class="modal-dialog modal-xl">

        <div class="modal-content">

            <div class="modal-header">
                <h4 class="modal-title">Add/Edit</h4>
            </div>

            <div class="modal-body">

                <asp:UpdatePanel ID="updNewActivity" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:ValidationSummary ID="vldSumActivity" runat="server" ValidationGroup="NewActivity" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    <div id="divMsgAlertRevAndScore" runat="server" style="display: none"></div>
                                </div>
                            </div>
                        </div>

                        <asp:FormView ID="frmRevAndScore" runat="server" DataKeyNames="Activity_ReviewsAndScores_Id" DefaultMode="Insert" CssClass="col-md-12"
                            OnItemCommand="frmRevAndScore_ItemCommand">

                            <InsertItemTemplate>

                                <div class="col-sm-12 row">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Add New Review And Score</div>
                                        <div class="panel-body">

                                            <div class="form-group">
                                                <div class="col-sm-6">

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="ddlReviewType">Review Type</label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList runat="server" ID="ddlReviewType" CssClass="form-control">
                                                                <asp:ListItem Text="-select-" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="txtReviewName">Review Name</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtReviewName" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="txtReviewDescription">Review Description</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtReviewDescription" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="txtReviewAuthorName">Review Author</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtReviewAuthorName" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-sm-6">

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="ddlReviewScore">Review Score</label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList runat="server" ID="ddlReviewScore" CssClass="form-control">
                                                                <asp:ListItem Text="-select-" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="ddlReviewSource">Review Source</label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList runat="server" ID="ddlReviewSource" CssClass="form-control">
                                                                <asp:ListItem Text="-select-" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="chkIsActive">Active</label>
                                                        <div class="col-sm-6">
                                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="chkIsCustomerReview">Allow</label>
                                                        <div class="col-sm-6">
                                                            <asp:CheckBox ID="chkIsCustomerReview" runat="server" Checked='<%# Bind("IsCustomerReview") %>' />
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-sm-6 pull-right">
                                                            <asp:Button ID="btnAdd" runat="server" Text="Add" CommandName="Add" CssClass="btn btn-primary" />
                                                            <asp:Button ID="btnReset" runat="server" Text="Reset" CommandName="Reset" CssClass="btn btn-primary" />
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </InsertItemTemplate>

                            <EditItemTemplate>

                                <div class="col-sm-12 row">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Edit Review And Score</div>
                                        <div class="panel-body">

                                            <div class="form-group">
                                                <div class="col-sm-6">

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="ddlReviewType">Review Type</label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList runat="server" ID="ddlReviewType" CssClass="form-control">
                                                                <asp:ListItem Text="-select-" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="txtReviewName">Review Name</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtReviewName" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="txtReviewDescription">Review Description</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtReviewDescription" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="txtReviewAuthorName">Review Author</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtReviewAuthorName" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-sm-6">

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="ddlReviewScore">Review Score</label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList runat="server" ID="ddlReviewScore" CssClass="form-control">
                                                                <asp:ListItem Text="-select-" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="ddlReviewSource">Review Source</label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList runat="server" ID="ddlReviewSource" CssClass="form-control">
                                                                <asp:ListItem Text="-select-" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="chkIsActive">Active</label>
                                                        <div class="col-sm-6">
                                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="chkIsCustomerReview">Allow</label>
                                                        <div class="col-sm-6">
                                                            <asp:CheckBox ID="chkIsCustomerReview" runat="server" Checked='<%# Bind("IsCustomerReview") %>' />
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-sm-6 pull-right">
                                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update" CssClass="btn btn-primary" />
                                                            <asp:Button ID="btnReset" runat="server" Text="Reset" CommandName="Reset" CssClass="btn btn-primary" />
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </EditItemTemplate>

                        </asp:FormView>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
            </div>

        </div>

    </div>

</div>
