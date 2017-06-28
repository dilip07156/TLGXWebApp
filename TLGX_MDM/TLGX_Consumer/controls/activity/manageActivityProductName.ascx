<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="manageActivityProductName.ascx.cs" Inherits="TLGX_Consumer.controls.activity.manageActivityProductName" %>

<h1 class="page-header">Hell Fire Caves, High Wycombe (United Kingdom)</h1>


<div class="navbar">

    <ul class="nav nav-tabs">
        <li class="active"><a href="#panOverview" data-toggle="tab">Product Name Overview</a></li>
        <li><a href="#panDescriptions" data-toggle="tab">Product Name Descriptions</a></li>
        <li><a href="#panClassification" data-toggle="tab">Product Name Descriptions</a></li>
        <li><a href="#panCKISMapping" data-toggle="tab">CKIS Mapping</a></li>


    </ul>
</div>



<div class="tab-content">
    <div class="tab-pane active" id="panOverview">


        <div class="panel panel-default">
            <div class="panel-heading">
                
                
       <div class="row">
                        <div class="col-sm-6 ">
                           Activity Product Name
                        </div>
                        <div class="col-sm-6">
                            <asp:Button runat="server" ID="btnNewCreate" CssClass="btn btn-sm btn-primary pull-right" Text="Update" />
                        </div>
                    </div>



            </div>
            <div class="panel-body">

                <div class="container">

                    <div class="row">

                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txtProductCategoryName">
                                    Product Category
                                </label>
                                <asp:TextBox ID="txtProductCategoryName" CssClass="form-control" runat="server" Text="Activities" Enabled="false" />
                            </div>



                            <div class="form-group">
                                <label for="txtActivityName">
                                    Product Name
                                </label>
                                <asp:TextBox ID="txtActivityName" CssClass="form-control" runat="server" Text="Hellfure Caves" />
                            </div>



                            <div class="form-group">
                                <label for="txtDisplayName">
                                    Display Name
                                </label>
                                <asp:TextBox ID="txtDisplayName" CssClass="form-control" runat="server" Text="Entrance to Hellfire Caves" />
                            </div>


                            <div class="form-group">
                                <label for="ddlProductCategorySubType">
                                    Country
                                </label>
                                <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label for="ddlProductCategorySubType">
                                    City
                                </label>
                                <asp:DropDownList runat="server" ID="DropDownList2" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>


                        </div>
                        <div class="col-lg-4">

                            <div class="form-group">
                                <label for="ddlProductCategorySubType">
                                    Product Category Sub Type
                                </label>
                                <asp:DropDownList runat="server" ID="ddlProductCategorySubType" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label for="ddlActivityType">
                                    Activity Type
                                </label>
                                <asp:DropDownList runat="server" ID="ddlActivityType" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label for="ddlAffilication">
                                    Affiliation
                                </label>
                                <asp:DropDownList runat="server" ID="ddlAffilication" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                                                        <div class="form-group">
                                <label for="ddlAffilication">
                                    Mode of Transport
                                </label>
                                <asp:DropDownList runat="server" ID="DropDownList3" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>




                        </div>
                        <div class="col-lg-4">

                            <div class="form-group">
                                <label for="txtProductCategoryName">
                                    Create Date
                                </label>
                                <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" Text="Activities" Enabled="false" />
                            </div>

                            <div class="form-group">
                                <label for="txtProductCategoryName">
                                    Create User
                                </label>
                                <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" Text="Activities" Enabled="false" />
                            </div>

                            <div class="form-group">
                                <label for="txtProductCategoryName">
                                    Edit Date
                                </label>
                                <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server" Text="Activities" Enabled="false" />
                            </div>

                            <div class="form-group">
                                <label for="txtProductCategoryName">
                                    Edit User
                                </label>
                                <asp:TextBox ID="TextBox4" CssClass="form-control" runat="server" Text="Activities" Enabled="false" />
                            </div>


                        </div>

                    </div>
                </div>

            </div>
        </div>


    </div>

    <div class="tab-pane fade in" id="panDescriptions">
        

    </div>

    <div class="tab-pane fade in" id="panClassification">
    </div>

    <div class="tab-pane fade in" id="panCKISMapping">
    </div>

</div>




