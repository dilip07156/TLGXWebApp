<%@ Page Title="Activity Flavour" Culture="en-GB" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageActivityFlavour.aspx.cs" Inherits="TLGX_Consumer.activity.ManageActivityFlavour" MaintainScrollPositionOnPostback="true" %>

<%@ PreviousPageType VirtualPath="SearchActivityMaster.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <%@ Register Src="~/controls/activity/ManageActivityFlavours/Flavours.ascx" TagPrefix="uc1" TagName="Flavours"%>
    <%@ Register Src="~/controls/activity/ManageActivityFlavours/Inclusion.ascx" TagPrefix="uc1" TagName="Inclusion" %>
    <%@ Register Src="~/controls/activity/ManageActivityFlavours/ClassificationAttributes.ascx" TagPrefix="uc1" TagName="ClassificationAttributes" %>
    <%--<%@ Register Src="~/controls/activity/ManageActivityFlavours/Policy.ascx" TagPrefix="uc1" TagName="Policy"%>--%>
    <%@ Register Src="~/controls/activity/ManageActivityFlavours/PricesNDeals.ascx" TagPrefix="uc1" TagName="PricesNDeals"%>
    <%@ Register Src="~/controls/activity/ManageActivityFlavours/ReviewsNScores.ascx" TagPrefix="uc1" TagName="ReviewsNScores"%>
    <%@ Register Src="~/controls/activity/ManageActivityFlavours/SupplierProductMappings.ascx" TagPrefix="uc1" TagName="SupplierProductMappings"%>

    <div class="container" id="myWizard">
        <div class="row">
            <br />
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
        <div class="navbar">
            <%--<div class="navbar-inner">--%>
            <ul class="nav nav-tabs">
                <li class="active"><a href="#panFlavours" data-toggle="tab">Flavours</a></li>
                <li><a href="#panInclusion" data-toggle="tab">Inclusion</a></li>
                <li><a href="#panClassificationAttributes" data-toggle="tab">ClassificationAttributes</a></li>
                <%--<li><a href="#panMedia" data-toggle="tab">Media</a></li>--%>
                <li><a href="#panPolicy" data-toggle="tab">Policy</a></li>
                <li><a href="#panPricesNDeals" data-toggle="tab">PricesNDeals</a></li>
                <li><a href="#panReviewsNScores" data-toggle="tab">ReviewsNScores</a></li>
                <li><a href="#panSupplierProductMappings" data-toggle="tab">SupplierProductMappings</a></li>
            </ul>
            <%--</div>--%>
        </div>

        <div class="tab-content">
            <div class="tab-pane" id="panFlavours">
                <uc1:Flavours runat="server" ID="Flavours" />
            </div>

            <div class="tab-pane" id="panInclusion">
                <uc1:Inclusion runat="server" ID="Inclusion" />
            </div>

            <div class="tab-pane" id="panClassificationAttributes">
                <uc1:ClassificationAttributes runat="server" ID="ClassificationAttributes" />
            </div>

            <%--<div class="tab-pane" id="panMedia">
                <uc1:Media runat="server" ID="Media" />
            </div>--%>

            <div class="tab-pane" id="panPolicy">
                <uc1:Policy runat="server" ID="Policy" />
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

    </script>
</asp:Content>
