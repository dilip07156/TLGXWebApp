<%@ Page Title="Activity Flavour" Culture="en-GB" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageActivityFlavour.aspx.cs" Inherits="TLGX_Consumer.activity.ManageActivityFlavour" MaintainScrollPositionOnPostback="true" %>

<%@ PreviousPageType VirtualPath="SearchActivityMaster.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%@ Register Src="~/controls/activity/ManageActivityFlavours/Flavours.ascx" TagPrefix="uc1" TagName="Flavours" %>
    <%@ Register Src="~/controls/activity/ManageActivityFlavours/InclusionsAndExclusion.ascx" TagPrefix="uc1" TagName="Inclusions" %>
    <%@ Register Src="~/controls/activity/ManageActivityFlavours/ClassificationAttributes.ascx" TagPrefix="uc1" TagName="ClassificationAttributes" %>
    <%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityMedia.ascx" TagPrefix="uc1" TagName="ActivityMedia" %>
    <%--<%@ Register Src="~/controls/activity/ManageActivityFlavours/Policy.ascx" TagPrefix="uc1" TagName="Policy" %>--%>
    <%@ Register Src="~/controls/activity/ManageActivityFlavours/PricesNDeals.ascx" TagPrefix="uc1" TagName="PricesNDeals" %>
    <%@ Register Src="~/controls/activity/ManageActivityFlavours/ReviewsNScores.ascx" TagPrefix="uc1" TagName="ReviewsNScores" %>
    <%@ Register Src="~/controls/activity/ManageActivityFlavours/SupplierProductMappings.ascx" TagPrefix="uc1" TagName="SupplierProductMappings" %>
    <%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityDescription.ascx" TagPrefix="uc1" TagName="ActivityDescription" %>
    <%@ Register Src="~/controls/activity/ManageActivityFlavours/FlavourOptions.ascx" TagPrefix="uc1" TagName="FlavourOptions" %>
    <%--<%@ Register Src="~/controls/activity/ManageActivityFlavours/ContactDetails.ascx" TagPrefix="uc1" TagName="ContactDetails" %>--%>
    <%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityContactDetails.ascx" TagPrefix="uc1" TagName="ActivityContactDetails" %>


    <asp:UpdatePanel ID="UpdActivity_Flavour_StatusModal" runat="server">
        <ContentTemplate>
            <div class="container" id="myWizard">
                <br />
                <div class="row">
                    <div class="col-lg-12">
                        <div style="text-align: right;">
                            * Mandatory fields are marked as underlined
                        </div>
                    </div>
                </div>
                <%--<div class="row">
            <div class="col-lg-12">
                <uc1:head runat="server" ID="head" />
            </div>
        </div>--%>
                <br />
                <div class="row" id="dvproductheader" runat="server">
                    <div class="col-lg-9">

                        <h3>
                            <strong>
                                <asp:Label ID="lblProductName" runat="server"></asp:Label>
                            </strong>
                        </h3>
                    </div>
                    <div class="col-lg-3 ">
                        <div class="pull-right" style="margin-top: 25px !important;">
                            <strong>
                                <asp:Label ID="lblActivityStatus" runat="server"></asp:Label>
                                (
                        <button class="btn btn-link" style="padding: 0px;" onclick="showmoActivity_Flavour_Status();">Change</button>

                                )
                            </strong>
                        </div>
                    </div>
                </div>

                <br />
                <div class="navbar">
                    <%--<div class="navbar-inner">--%>
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#panFlavours" data-toggle="tab">
                            <h5>Product Details</h5>
                        </a></li>
                        <li><a href="#panDescription" data-toggle="tab">
                            <h5>Descriptions</h5>
                        </a></li>
                        <li><a href="#panInclusion" data-toggle="tab">
                            <h5>Inclusion/Exclusion</h5>
                        </a></li>
                        <li><a href="#panClassificationAttributes" data-toggle="tab">
                            <h5>Classification</h5>
                        </a></li>
                        <li><a href="#panActivityMedia" data-toggle="tab">
                            <h5>Media</h5>
                        </a></li>
                        <%--<li><a href="#panPolicy" data-toggle="tab"><h5>Policy</h5></a></li>--%>
                        <li><a href="#panFlavourOptions" data-toggle="tab">
                            <h5>Options</h5>
                        </a></li>
                        <li><a href="#panPricesNDeals" data-toggle="tab">
                            <h5>Prices And Deals</h5>
                        </a></li>
                        <li><a href="#panReviewsNScores" data-toggle="tab">
                            <h5>Reviews And Scores</h5>
                        </a></li>
                        <li><a href="#panSupplierProductMappings" data-toggle="tab">
                            <h5>Supplier Mappings</h5>
                        </a></li>
                        <li><a href="#panContactDetails" data-toggle="tab">
                            <h5>Contact Details</h5>
                        </a></li>
                    </ul>
                    <%--</div>--%>
                </div>

                <div class="tab-content">
                    <div class="tab-pane active" id="panFlavours">
                        <uc1:Flavours runat="server" ID="Flavours" />
                    </div>

                    <div class="tab-pane" id="panDescription">
                        <uc1:ActivityDescription runat="server" ID="ActivityDescription" />
                    </div>

                    <div class="tab-pane" id="panInclusion">
                        <uc1:Inclusions runat="server" ID="Inclusions" />
                    </div>

                    <div class="tab-pane" id="panClassificationAttributes">
                        <uc1:ClassificationAttributes runat="server" ID="ClassificationAttributes" />
                    </div>

                    <div class="tab-pane" id="panActivityMedia">
                        <uc1:ActivityMedia runat="server" ID="ActivityMedia" />
                    </div>

                    <%--<div class="tab-pane" id="panPolicy">
                <uc1:Policy runat="server" ID="Policy" />
            </div>--%>

                    <div class="tab-pane" id="panFlavourOptions">
                        <uc1:FlavourOptions runat="server" ID="FlavourOptions" />
                    </div>

                    <div class="tab-pane" id="panPricesNDeals">
                        <uc1:PricesNDeals runat="server" ID="PricesNDeals" />
                    </div>

                    <div class="tab-pane" id="panReviewsNScores">
                        <uc1:ReviewsNScores runat="server" ID="ReviewsNScores" />
                    </div>

                    <div class="tab-pane" id="panSupplierProductMappings">
                        <uc1:SupplierProductMappings runat="server" ID="SupplierProductMappings" />
                    </div>

                    <div class="tab-pane" id="panContactDetails">
                        <uc1:ActivityContactDetails runat="server" ID="ActivityContactDetails" />
                    </div>

                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" id="moActivity_Flavour_Status" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Update Activity Status</h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12">
                            <div id="dvMsgStatusUpdate" runat="server" style="display: none;"></div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdActivity_Flavour_StatusModalpopup" runat="server">
                        <ContentTemplate>


                            <div class="row">
                                <label class="control-label col-md-4" for="ddlStatus">Status</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlActivity_Flavour_Status" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <label class="control-label col-md-4" for="txtActivity_Flavour_StatusNotes">Notes/Comments:</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtActivity_Flavour_StatusNotes" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Button ID="btnChangeActivityStatus" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnClick="btnChangeActivityStatus_Click" OnClientClick="closemoActivityStatusModal();" />
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

    <script type='text/javascript'>
        $('.next').click(function () {
            var nextId = $(this).parents('.tab-pane').next().attr("id");
            $('[href=#' + nextId + ']').tab('show');

        })

        $('.first').click(function () {
            $('#myWizard a:first').tab('show')

        })


        function showmoActivity_Flavour_Status() {
            document.getElementById('MainContent_dvMsgStatusUpdate').style.display = 'none';
            $("#moActivity_Flavour_Status").modal('show');
        }
        function closemoActivityStatusModal() {
            $("#moActivity_Flavour_Status").modal('hide');
        }
        function pageLoad(sender, args) {
        }

    </script>

</asp:Content>
