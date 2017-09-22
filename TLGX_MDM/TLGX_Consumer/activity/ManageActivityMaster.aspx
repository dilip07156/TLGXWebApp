<%@ Page Title="Manage Activity Master" Culture="en-GB" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageActivityMaster.aspx.cs" Inherits="TLGX_Consumer.activity.ManageActivityMaster" MaintainScrollPositionOnPostback="true" %>

<%--<%@ Register Src="~/controls/activity/ManageActivityMaster/ProductInformation.aspx" TagPrefix="uc1" TagName="ProductInformation" %>--%>

<%@ PreviousPageType VirtualPath="SearchActivityMaster.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%@ Register Src="~/controls/activity/ManageActivityMaster/Overview.ascx" TagPrefix="uc1" TagName="Overview" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/AdditionalInfo.ascx" TagPrefix="uc1" TagName="AdditionalInfo" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/Ancilary.ascx" TagPrefix="uc1" TagName="Ancilary" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/AttachedProductPolicy.ascx" TagPrefix="uc1" TagName="AttachedProductPolicy" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/CustomDefField_WeatherInfo.ascx" TagPrefix="uc1" TagName="CustomDefField_WeatherInfo" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/Facilities.ascx" TagPrefix="uc1" TagName="Facilities" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/GeneralInfo.ascx" TagPrefix="uc1" TagName="GeneralInfo" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/Inclusion.ascx" TagPrefix="uc1" TagName="Inclusion" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/Media.ascx" TagPrefix="uc1" TagName="Media" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/MiscellaneousInformation.ascx" TagPrefix="uc1" TagName="MiscellaneousInformation" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/OtherInformation.ascx" TagPrefix="uc1" TagName="OtherInformation" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/PickUpDropOffAndDaysOfOperation.ascx" TagPrefix="uc1" TagName="PickUpDropOffAndDaysOfOperation" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/PreArrivalTips.ascx" TagPrefix="uc1" TagName="PreArrivalTips" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/ProductInformation.ascx" TagPrefix="uc1" TagName="ProductInformation" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/ProductNameSubTypeInfo.ascx" TagPrefix="uc1" TagName="ProductNameSubTypeInfo" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/ProductNameSubTypeStatus.ascx" TagPrefix="uc1" TagName="ProductNameSubTypeStatus" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/ProductStatus.ascx" TagPrefix="uc1" TagName="ProductStatus" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/ShoppingInfo.ascx" TagPrefix="uc1" TagName="ShoppingInfo" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/SuggestedAcco.ascx" TagPrefix="uc1" TagName="SuggestedAcco" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/SupplierUpdate.ascx" TagPrefix="uc1" TagName="SupplierUpdate" %>
    <%@ Register Src="~/controls/activity/ManageActivityMaster/TourIteneraryAndRouting.ascx" TagPrefix="uc1" TagName="TourIteneraryAndRouting" %>

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
                <li class="active"><a href="#panProductInformation" data-toggle="tab">Product Information</a></li>
                <li><a href="#panOverview" data-toggle="tab">Overview</a></li>
                <li><a href="#panGeneralInfo" data-toggle="tab">General Information</a></li>
                <li><a href="#panMiscellaneousInformation" data-toggle="tab">Miscellaneous Information</a></li>
                <li><a href="#panFacilities" data-toggle="tab">Facilities</a></li>
                <li><a href="#panShoppingInfo" data-toggle="tab">Shopping Information</a></li>
                <li><a href="#panOtherInformation" data-toggle="tab">Other Information</a></li>
                <li><a href="#panPreArrivalTips" data-toggle="tab">PreArrival Tips</a></li>
                <li><a href="#panSuggestedAcco" data-toggle="tab">Suggested Accomodation</a></li>
                <li><a href="#panSupplierUpdate" data-toggle="tab">Supplier Update</a></li>
                <li><a href="#panMedia" data-toggle="tab">Media</a></li>
                <li><a href="#panAttachedProductPolicy" data-toggle="tab">Attached Product Policy</a></li>
                <li><a href="#panCustomDefField_WeatherInfo" data-toggle="tab">Custom Definition Field Weather Information</a></li>
                <li><a href="#panProductStatus" data-toggle="tab">Product Status</a></li>
                <li><a href="#panProductNameSubTypeInfo" data-toggle="tab">Product Name SubType Information</a></li>
                <li><a href="#panPickUpDropOffAndDaysOfOperation" data-toggle="tab">PickUp DropOff And Days Of Operation</a></li>
                <li><a href="#panInclusion" data-toggle="tab">Inclusion</a></li>
                <li><a href="#panTourIteneraryAndRouting" data-toggle="tab">Tour Itenerary And Routing</a></li>
                <li><a href="#panAdditionalInfo" data-toggle="tab">Additional Information</a></li>
                <li><a href="#panAncilary" data-toggle="tab">Ancilary</a></li>
                <li><a href="#panProductNameSubTypeStatus" data-toggle="tab">Product Name SubType Status</a></li>
            </ul>
            <%--</div>--%>
        </div>

        <div class="tab-content">
            <div class="tab-pane active" id="panOverview">
                <uc1:Overview runat="server" ID="Overview" />
            </div>

            <div class="tab-pane fade in" id="panAdditionalInfo">
                <uc1:AdditionalInfo runat="server" ID="AdditionalInfo" />
            </div>

            <div class="tab-pane fade in" id="panAncilary">
                <uc1:Ancilary runat="server" ID="Ancilary" />
            </div>

            <div class="tab-pane" id="panAttachedProductPolicy">
                <uc1:AttachedProductPolicy runat="server" ID="AttachedProductPolicy" />
            </div>

            <div class="tab-pane" id="panCustomDefField_WeatherInfo">
                <uc1:CustomDefField_WeatherInfo runat="server" ID="CustomDefField_WeatherInfo" />
            </div>

            <div class="tab-pane" id="panFacilities">
                <uc1:Facilities runat="server" ID="Facilities" />
            </div>

            <div class="tab-pane" id="panGeneralInfo">
                <uc1:GeneralInfo runat="server" ID="GeneralInfo" />
            </div>

            <div class="tab-pane" id="panInclusion">
                <uc1:Inclusion runat="server" ID="Inclusion" />
            </div>

            <div class="tab-pane" id="panMedia">
                <uc1:Media runat="server" ID="Media" />
            </div>

            <div class="tab-pane" id="panMiscellaneousInformation">
                <uc1:MiscellaneousInformation runat="server" ID="MiscellaneousInformation" />
            </div>

            <div class="tab-pane" id="panOtherInformation">
             <uc1:OtherInformation runat="server" id="OtherInformation" />          
            </div>

            <div class="tab-pane" id="panPickUpDropOffAndDaysOfOperation">
                <uc1:PickUpDropOffAndDaysOfOperation runat="server" ID="PickUpDropOffAndDaysOfOperation" />
            </div>

            <div class="tab-pane" id="panPreArrivalTips">
                <uc1:PreArrivalTips runat="server" ID="PreArrivalTips" />
            </div>

            <div class="tab-pane" id="panProductInformation">
                <uc1:ProductInformation runat="server" ID="ProductInformation" />
            </div>

            <div class="tab-pane" id="panProductNameSubTypeInfo">
                <uc1:ProductNameSubTypeInfo runat="server" ID="ProductNameSubTypeInfo" />
            </div>

            <div class="tab-pane" id="panProductNameSubTypeStatus">
                <uc1:ProductNameSubTypeStatus runat="server" ID="ProductNameSubTypeStatus" />
            </div>

            <div class="tab-pane" id="panProductStatus">
                <uc1:ProductStatus runat="server" ID="ProductStatus" />
            </div>

            <div class="tab-pane" id="panShoppingInfo">
                <uc1:ShoppingInfo runat="server" ID="ShoppingInfo" />
            </div>

            <div class="tab-pane" id="panSuggestedAcco">
                <uc1:SuggestedAcco runat="server" ID="SuggestedAcco" />
            </div>

            <div class="tab-pane" id="panSupplierUpdate">
                <uc1:SupplierUpdate runat="server" ID="SupplierUpdate" />
            </div>

            <div class="tab-pane" id="panTourIteneraryAndRouting">
                <uc1:TourIteneraryAndRouting runat="server" ID="TourIteneraryAndRouting" />
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
