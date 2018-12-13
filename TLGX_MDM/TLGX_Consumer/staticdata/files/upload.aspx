<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="upload.aspx.cs" Inherits="TLGX_Consumer.staticdata.files.upload" %>

<%@ Register Src="~/controls/staticdataconfig/manageFIleUpload.ascx" TagPrefix="uc1" TagName="manageFIleUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <h1 class="page-header">File Uploads</h1>

        <uc1:manageFIleUpload runat="server" ID="manageFIleUpload" />

    </div>

</asp:Content>
