<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Flavours.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.Flavours" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityContactDetails.ascx" TagPrefix="uc1" TagName="ActivityContactDetails" %>


        <div class="panel panel-default">
            <div class="panel-heading">Contact Details</div>
            <div class="panel-body">
                <uc1:ActivityContactDetails runat="server" id="ActivityContactDetails" />
            </div>
        </div>

