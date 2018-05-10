<%@ Page Title="Manage Hotel" Culture="en-GB" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="TLGX_Consumer.hotels.manage" MaintainScrollPositionOnPostback="true" %>

<%@ PreviousPageType VirtualPath="search.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%@ Register Src="~/controls/hotel/overview.ascx" TagPrefix="uc1" TagName="overview" %>
    <%@ Register Src="~/controls/hotel/facilities.ascx" TagPrefix="uc1" TagName="facilities" %>
    <%@ Register Src="~/controls/hotel/roominfo.ascx" TagPrefix="uc1" TagName="roominfo" %>
    <%@ Register Src="~/controls/hotel/inandaround.ascx" TagPrefix="uc1" TagName="inandaround" %>
    <%@ Register Src="~/controls/hotel/rules.ascx" TagPrefix="uc1" TagName="rules" %>
    <%@ Register Src="~/controls/hotel/media.ascx" TagPrefix="uc1" TagName="media" %>
    <%@ Register Src="~/controls/hotel/passengeroccupancy.ascx" TagPrefix="uc1" TagName="passengeroccupancy" %>
    <%@ Register Src="~/controls/hotel/route.ascx" TagPrefix="uc1" TagName="route" %>
    <%@ Register Src="~/controls/hotel/updates.ascx" TagPrefix="uc1" TagName="updates" %>
    <%@ Register Src="~/controls/hotel/healthandsafety.ascx" TagPrefix="uc1" TagName="healthandsafety" %>
    <%--<%@ Register Src="~/controls/hotel/ancilary.ascx" TagPrefix="uc1" TagName="ancilary" %>--%>
    <%@ Register Src="~/controls/hotel/head.ascx" TagPrefix="uc1" TagName="head" %>
    <%@ Register Src="~/controls/hotel/descriptions.ascx" TagPrefix="uc1" TagName="descriptions" %>
    <%@ Register Src="~/controls/hotel/status.ascx" TagPrefix="uc1" TagName="status" %>
    <%@ Register Src="~/controls/hotel/AddressCheck.ascx" TagPrefix="uc1" TagName="AddressCheck" %>
    <%@ Register Src="~/controls/hotel/supplierHotelMapping.ascx" TagPrefix="uc1" TagName="supplierHotelMapping" %>
    <%@ Register Src="~/controls/hotel/ClassificationAttributes.ascx" TagPrefix="uc1" TagName="ClassificationAttributes" %>
    <%@ Register Src="~/controls/hotel/roomtypemapping.ascx" TagPrefix="uc1" TagName="roomtypemapping" %>
    
    <div class="container" id="myWizard">
        <div class="row">
            <br />
            <div class="col-lg-12">
                <div style="text-align: right;">
                    * Mandatory fields are marked as underlined
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <uc1:head runat="server" ID="head" />
            </div>
        </div>
        <br />
        <div class="navbar">
            <%--<div class="navbar-inner">--%>
            <ul class="nav nav-tabs">
                <li class="active"><a href="#panOverview" data-toggle="tab">Key Facts</a></li>
                <li><a href="#panFacilities" data-toggle="tab" style="display:none">Facilities</a></li>
                <li><a href="#panDescriptions" data-toggle="tab">Descriptions</a></li>
                <li><a href="#panRooms" data-toggle="tab">Rooms</a></li>
                <li><a href="#panOccupancy" data-toggle="tab" style="display:none">Occupancy</a></li>
                <li><a href="#panInAndAround" data-toggle="tab" style="display:none">In/Around</a></li>
                <li><a href="#panReach" data-toggle="tab" style="display:none">Reach</a></li>               
                <li><a href="#panRules" data-toggle="tab" style="display:none">Rules</a></li>
                <li><a href="#panMedia" data-toggle="tab" style="display:none">Media</a></li>
                <li><a href="#panUpdates" data-toggle="tab" style="display:none">Updates</a></li>
                <li><a href="#panHealthAndSafety" data-toggle="tab" style="display:none">H/S</a></li>
                <%--<li><a href="#panAncil" data-toggle="tab">Ancillary</a></li>--%>
                <li><a href="#panStatus" data-toggle="tab">Status</a></li>
                <li><a href="#panclassificationAttributes" data-toggle="tab" style="display:none">Attributes</a></li>
                <%--<li><a href="#panSupplierMapping" data-toggle="tab">Map Product</a></li>
                <li><a href="#panRoomMapping" data-toggle="tab">Map RoomType</a></li>--%>
            </ul>
            <%--</div>--%>
        </div>

        <div class="tab-content">
            <div class="tab-pane active" id="panOverview">
                <uc1:overview runat="server" ID="overview" />
                <%--<a class="btn btn-default next" href="#">Next</a>--%>
            </div>

            <div class="tab-pane fade in" id="panDescriptions">
                <uc1:descriptions runat="server" ID="descriptions" />
            </div>


          <%--  <div class="tab-pane fade in" id="panFacilities">
                <uc1:facilities runat="server" ID="facilities1" />
            </div>--%>

            <div class="tab-pane" id="panRooms">
                <uc1:roominfo runat="server" ID="roominfo" />
            </div>

           <%-- <div class="tab-pane" id="panOccupancy">
                <uc1:passengeroccupancy runat="server" ID="passengeroccupancy" />
            </div>

            <div class="tab-pane" id="panReach">
                <uc1:route runat="server" ID="route" />
            </div>

            <div class="tab-pane" id="panInAndAround">
                <uc1:inandaround runat="server" ID="inandaround" />
            </div>

            <div class="tab-pane" id="panRules">
                <uc1:rules runat="server" ID="rules" />
            </div>

            <div class="tab-pane" id="panMedia">
                <uc1:media runat="server" ID="media" />
            </div>

            <div class="tab-pane" id="panUpdates">
                <uc1:updates runat="server" ID="updates" />
            </div>

            <div class="tab-pane" id="panHealthAndSafety">
                <uc1:healthandsafety runat="server" ID="healthandsafety" />
            </div>--%>

            <%-- <div class="tab-pane" id="panAncil">
             <uc1:ancilary runat="server" id="ancilary" />          
            </div>--%>

            <div class="tab-pane" id="panStatus">
                <uc1:status runat="server" ID="status" />
            </div>

        <%--    <div class="tab-pane" id="panclassificationAttributes">
                <uc1:ClassificationAttributes runat="server" ID="ClassificationAttributes" />
            </div>--%>


            <%--<div class="tab-pane" id="panSupplierMapping">
                <uc1:supplierHotelMapping runat="server" ID="supplierHotelMapping" />
            </div>


            <div class="tab-pane" id="panRoomMapping">
                <uc1:roomtypemapping runat="server" ID="roomtypemapping" />
            </div>--%>



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
