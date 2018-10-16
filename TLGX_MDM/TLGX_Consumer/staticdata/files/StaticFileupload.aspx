<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaticFileupload.aspx.cs" Inherits="TLGX_Consumer.staticdata.files.StaticFileupload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../Scripts/jquery-3.3.1.js"></script>

    <link href="../../Content/bootstrap.css" rel="stylesheet" />

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
        <asp:ScriptManager ID="scrManager" runat="server"></asp:ScriptManager>
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
                        <asp:DropDownList ID="ddlSupplierList" AutoPostBack="true" runat="server"
                            AppendDataBoundItems="true" CssClass="form-control" OnSelectedIndexChanged="ddlSupplierList_SelectedIndexChanged">
                            <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group row">
                    <label class="control-label col-sm-4" for="ddlEntityList">
                        Entity
                    </label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="ddlEntityList" AutoPostBack="true" runat="server"
                            AppendDataBoundItems="true" CssClass="form-control" OnSelectedIndexChanged="ddlEntityList_SelectedIndexChanged">
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


                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="form-group row">

                            <div class="col-sm-2 col-sm-offset-10">
                                <asp:Button ID="btnUploadCompleted" runat="server" Text="All Upload Completed" Visible="false" class="btn btn-primary btn-sm" OnClick="btnUploadComplete_Click"  />
                            </div>
                        </div>
                        <div class="row">
                            <div id="dvMsg" runat="server" enableviewstate="false" style="display: none;">
                            </div>
                        </div>
                        <%--OnPageIndexChanging="gvFileUploadSearch_PageIndexChanging"   --%>
                        <div class="form-group row">
                           <%-- <table class="table table-hover table-striped">
                                <tr>
                                    <td>Supplier Name
                                    </td>
                                    <td>Entity
                                    </td>
                                    <td>File Name
                                    </td>
                                    <td>Status
                                    </td>
                                    <td>Upload Date</td>
                                    <td></td>

                                </tr>
                            </table>--%>
                            <div style="height: 300px; overflow: auto;">

                                <asp:GridView ID="gvFileUploadSearchForNew" runat="server" AllowPaging="True" AllowCustomPaging="true"
                                    EmptyDataText="No Files found for search conditions" CssClass="table table-hover table-striped" OnRowDataBound="gvFileUploadSearchForNew_RowDataBound"
                                    OnRowCommand="gvFileUploadSearchForNew_RowCommand" 
                                    AutoGenerateColumns="false" DataKeyNames="SupplierImportFile_Id,Supplier_Id">
                                    <Columns>
                                        <asp:BoundField HeaderText="Supplier Name" DataField="Supplier" />
                                        <asp:BoundField HeaderText="Entity" DataField="Entity" />
                                        <asp:BoundField HeaderText="File Name" DataField="OriginalFilePath" />
                                        <asp:TemplateField ShowHeader="true" HeaderText="Status" ItemStyle-Width="175px">
                                            <ItemTemplate>
                                                <div class="form-inline">
                                                    <div class="form-group">
                                                        <img style="height: 25px; width: 25px" src='<%# Eval("STATUS").ToString() == "PROCESSED" ? "../../images/148767.png" : (Eval("STATUS").ToString() == "ERROR" ? "../../images/148766.png" : ((Eval("STATUS").ToString() == "NEW") ? "../../images/148764.png" : "../../images/148853.png")) %>' />
                                                        <label><%# Eval("STATUS").ToString() %></label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField HeaderText="Upload Date" DataField="CREATE_DATE" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}" />
                                        <asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                                    CssClass="btn btn-default" CommandArgument='<%# Bind("SupplierImportFile_Id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat" %>'></span>
                                                    <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>

                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlEntityList" />
                        <asp:AsyncPostBackTrigger ControlID="ddlSupplierList" />
                        <asp:AsyncPostBackTrigger ControlID="btnUploadCompleted" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>


        </div>
    </form>
</body>
</html>
