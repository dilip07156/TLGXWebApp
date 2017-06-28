<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnAuthorized.aspx.cs" Inherits="TLGX_Consumer.UnAuthorized" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="form-group">&nbsp;</div>
        </div>
        <div class="row">
            <div class="form-group">
                <div class="col-sm-12 alert alert-danger"">
                <h3> <span class="glyphicon glyphicon-exclamation-sign"></span> You are not authorized to see this page ! </h3>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
