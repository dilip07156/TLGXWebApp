<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="APIData.aspx.cs" Inherits="TLGX_Consumer.staticdata.files.APIData" %>

<%@ Register Src="~/controls/staticdataconfig/manageFIleUpload.ascx" TagPrefix="uc1" TagName="manageFIleUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header">File Uploads</h1>


    <uc1:manageFIleUpload runat="server" id="manageFIleUpload" />

</asp:Content>
