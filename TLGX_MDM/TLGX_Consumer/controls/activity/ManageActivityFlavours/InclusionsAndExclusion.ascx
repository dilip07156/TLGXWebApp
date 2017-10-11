<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InclusionsAndExclusion.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.InclusionsAndExclusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script>
    function showFileUpload() {
        $("#moAddInclusions").modal('show');
    }
</script>
<style>
    @media (min-width: 768px) {
        .modal-xl {
            width: 80%;
            max-width: 1200px;
        }
    }
</style>
<asp:UpdatePanel ID="updMedia" runat="server">
    <ContentTemplate>
        <div class="panel-group" id="searchResult">
            <div class="panel panel-default">
                <div class="panel-heading clearfix">
                    <h4 class="panel-title pull-left">
                        <a data-toggle="collapse" data-parent="#searchResult" href="#collapseSearchResult">Activity Media (Total Count:
                            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</a></h4>
                    <asp:Button CssClass="pull-right btn btn-primary" runat="server" ID="btnNewUpload" OnClick="btnNewUpload_Click" Text="Add New" OnClientClick="showFileUpload();" />
                </div>

                <%@ Register Src="~/controls/activity/ManageActivityFlavours/Inclusion.ascx" TagPrefix="uc1" TagName="Inclusion" %>
                <%@ Register Src="~/controls/activity/ManageActivityFlavours/Exclusion.ascx" TagPrefix="uc1" TagName="Exclusion" %>

                <div class="container" id="myWizard1">
                    <div class="navbar">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#panInclusion" data-toggle="tab">Inclusion</a></li>
                            <li><a href="#panExclusion" data-toggle="tab">Exclusion</a></li>
                        </ul>
                    </div>

                    <div class="tab-content">
                        <div class="tab-pane active" id="panInclusion">
                            <uc1:Inclusion runat="server" ID="Inclusion" />
                        </div>
                        <div class="tab-pane" id="panExclusion">
                            <uc1:Exclusion runat="server" ID="Exclusion" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<script type='text/javascript'>
    $('.next').click(function () {
        var nextId = $(this).parents('.tab-pane').next().attr("id");
        $('[href=#' + nextId + ']').tab('show');

    })

    $('.first').click(function () {
        $('#myWizard1 a:first').tab('show')

    })

</script>

<div class="modal fade" id="moAddInclusions" role="dialog">
    <div class="modal-dialog modal-xl">

        <div class="modal-content">

            <div class="modal-header">
                <div class="panel-title">
                    <h4 class="modal-title">Add New</h4>
                </div>
            </div>

            <div class="modal-body">
                <asp:UpdatePanel ID="updNewActivity" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:ValidationSummary ID="vldSumActivity" runat="server" ValidationGroup="NewActivity" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    <div id="divMsgAlertIncExc" runat="server" style="display: none"></div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 row">
                            <div class="panel panel-default">
                                <div class="panel-heading">Add New</div>
                                <div class="panel-body">

                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="chkIsInclusion">Is Inclusion</label>
                                        <div class="col-sm-6">
                                            <asp:CheckBox ID="chkIsInclusion" runat="server" CssClass="form-control" Checked='<%# Bind("IsInclusion") %>' />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="ddlInclusionFor">Inclusion For</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList runat="server" ID="ddlInclusionFor" CssClass="form-group"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="ddlInclusionType">Inclusion Type</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList runat="server" ID="ddlInclusionType" CssClass="form-group"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="txtName">Inclusion Name</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ID="txtName" CssClass="form-group"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-sm-6" for="txtDescription">Inclusion Description</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ID="txtDescription" CssClass="form-group" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <label class="control-label col-sm-6" for="txtFrom">From</label>
                                            <div class="col-sm-6">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" />
                                                    <span class="input-group-btn">
                                                        <button class="btn btn-default" type="button" id="iCalFrom">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </button>
                                                    </span>

                                                </div>
                                                <cc1:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iCalFrom"></cc1:CalendarExtender>
                                                <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFrom" />
                                                <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To date can't be less than from date." ControlToCompare="txtFrom" CultureInvariantValues="true" ControlToValidate="txtTo" ValidationGroup="vldgrpFileSearch" Text="*" CssClass="text-danger" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>--%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <label class="control-label col-sm-6" for="txtTo">To</label>
                                            <div class="col-sm-6">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" />
                                                    <span class="input-group-btn">
                                                        <button class="btn btn-default" type="button" id="iCalTo">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </button>
                                                    </span>

                                                </div>
                                                <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" PopupButtonID="iCalTo"></cc1:CalendarExtender>
                                                <cc1:FilteredTextBoxExtender ID="axfte_txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtTo" />
                                                <%--<asp:CompareValidator ID="vldCmpDateFromTo" runat="server" ErrorMessage="To date can't be less than from date." ControlToCompare="txtFrom" CultureInvariantValues="true" ControlToValidate="txtTo" ValidationGroup="vldgrpFileSearch" Text="*" CssClass="text-danger" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>

        </div>

    </div>

</div>
