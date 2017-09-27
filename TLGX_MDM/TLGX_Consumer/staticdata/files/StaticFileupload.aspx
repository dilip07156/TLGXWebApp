<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaticFileupload.aspx.cs" Inherits="TLGX_Consumer.staticdata.files.StaticFileupload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../Scripts/jquery-3.1.1.js"></script>
    <link href="../../Scripts/Styles/bootstrap.css" rel="stylesheet" />
    <title></title>
    <style>
        .Uploading {
            background-color: #777171;
        }

        .progressbar-content {
            background-color: #fefefe;
            margin: 5% auto; /* 15% from the top and centered */
            padding: 20px;
            width: 20%; /* Could be more or less, depending on screen size */
        }

        .progress {
            position: fixed; /* Stay in place */
            z-index: 2050; /* Sit on top */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }

        .displaynone {
            display: none;
        }
    </style>
    <script>
        function SetBlur() {
            $("#divProgress").removeClass("displaynone").addClass("progress");
            $("divContent").addClass("Uploading");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="displaynone" id="divProgress">
            <div class="progressbar">
                <div class="progressbar-content">
                    <img alt="Loading..." src="../../images/ajax-loader.gif" />&nbsp;&nbsp; Uploading...
                </div>
            </div>
        </div>
        <div id="divContent">
            <div class="col-sm-12">
                <div class="form-group row">
                    <div id="dvmsgUploadCompleted" runat="server" enableviewstate="false" style="display: none;">
                    </div>
                </div>
            </div>
            <div class="col-sm-12">
                <div class="form-group row">
                    <label class="control-label col-sm-4" for="ddlSupplierList">
                        Supplier
                    </label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="ddlSupplierList" runat="server"
                            AppendDataBoundItems="true" CssClass="form-control">
                            <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group row">
                    <label class="control-label col-sm-4" for="ddlEntityList">
                        Entity
                    </label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="ddlEntityList" runat="server"
                            AppendDataBoundItems="true" CssClass="form-control">
                            <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group row">
                    <label class="control-label col-sm-4" for="FileUpload1">
                        File Path
                    </label>
                    <div class="col-sm-6">
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="" AllowMultiple="false" />
                    </div>
                    <div class="col-sm-2">
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" class="btn btn-primary btn-sm" OnClick="btnUpload_Click" OnClientClick="SetBlur();" />
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default btn-sm" Text="Reset" OnClick="btnReset_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
