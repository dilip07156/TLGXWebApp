<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manageDistributionLayer.aspx.cs" Inherits="TLGX_Consumer.admin.manageDistributionLayer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="page-header">Refresh Distribution Layer</h2>
    <asp:UpdatePanel ID="UpdDistributionLayer" runat="server">
         <ContentTemplate>
    <div class="row">
        <div class="col-lg-12">
            <div id="dvMsg" runat="server" style="display: none;"></div>
        </div>
    </div>
    
    <div class="row">

        <div class="col-lg-4">

            <div class="panel panel-default">
                <div class="panel-heading">Geography Masters & Mapping</div>
                <div class="panel-body">

                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>Element</th>
                                <th>Type</th>
                                <th>Last Updated</th>
                                <th></th>
                            </tr>
                        </thead>

                        <tbody>
                            <tr>
                                <td>Countries</td>
                                <td>Master</td>
                               <!-- <td>01/01/1900</td>-->
                                <td><asp:Label Text="" ID="LastUpdatedCountryMaster" runat="server" ></asp:Label></td>
                                <td>
                                    <!-- <button type="button" class="btn btn-default">
                                        <span class="glyphicon glyphicon-cog"></span>Update
                                    </button>-->

                                    <asp:Button ID="btnRefreshCountryMaster" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="btnRefreshCountryMaster_Click" />
                                </td>
                            </tr>

                            <tr>
                                <td>Countries</td>
                                <td>Mapping</td>
                                <!--<td>01/01/1900</td>-->
                                <td><asp:Label Text="" ID="LastUpdatedCountryMapping" runat="server" ></asp:Label></td>
                                <td>
                                    <!--  <button type="button" class="btn btn-default">
                                        <span class="glyphicon glyphicon-globe"></span>Update
                                    </button>-->
                                    <asp:Button ID="btnRefreshCountryMapping" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="btnRefreshCountryMapping_Click" />
                                </td>
                            </tr>

                            <tr>
                                <td>Cities</td>
                                <td>Master</td>
                              <!--  <td>01/01/1900</td>-->
                                 <td><asp:Label Text="" ID="LastUpdatedCityMaster" runat="server" ></asp:Label></td>
                                <td>
                                    <!--<button type="button" class="btn btn-default">
                                        <span class="glyphicon glyphicon-cog"></span>Update
                                    </button>-->
                                    <asp:Button ID="btnRefreshCityMaster" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="btnRefreshCityMaster_Click"  />
                                </td>
                            </tr>

                            <tr>
                                <td>Cities</td>
                                <td>Mapping</td>
                                <!--<td>01/01/1900</td>-->
                                <td><asp:Label Text="" ID="LastUpdatedCityMapping" runat="server" ></asp:Label></td>
                                <td>
                                    <!--<button type="button" class="btn btn-default">
                                        <span class="glyphicon glyphicon-globe"></span>Update
                                    </button>-->
                                     <asp:Button ID="btnRefreshCityMapping" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="btnRefreshCityMapping_Click"  />
                                </td>
                            </tr>

                             <tr>
                                <td>Port</td>
                                <td>Master</td>
                                <td><asp:Label Text="" ID="LastUpdatedPortMaster" runat="server" ></asp:Label></td>
                                <td>
                                    <asp:Button ID="btnRefreshPortMaster" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="btnRefreshPortMaster_Click"/>
                                </td>
                            </tr>
                            <tr>
                                <td>State</td>
                                <td>Master</td>
                                <td><asp:Label Text="" ID="LastUpdatedStateMaster" runat="server" ></asp:Label></td>
                                <td>
                                    <asp:Button ID="btnRefreshStateMaster" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="btnRefreshStateMaster_Click"/>
                                </td>
                            </tr>

                        </tbody>
                    </table>

                </div>
            </div>



        </div>
        
        <div class="col-lg-4">

            <div class="panel panel-default">
                <div class="panel-heading">Product Masters & Mapping</div>
                <div class="panel-body">

                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>Element</th>
                                <th>Type</th>
                                <th>Last Updated</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Hotels</td>
                                <td>Master</td>
                                <td>01/01/1900</td>
                                <td>
                                    <button type="button" class="btn btn-default">
                                        <span class="glyphicon glyphicon-cog"></span>Update
                                    </button>

                                </td>
                            </tr>

                            <tr>
                                <td>Hotels</td>
                                <td>Mapping</td>
                                <!--<td>01/01/1900</td>-->
                                <td><asp:Label Text="" ID="LastUpdatedHotelMapping" runat="server" ></asp:Label></td>
                                <td>
                                <!--    <button type="button" class="btn btn-default">
                                        <span class="glyphicon glyphicon-globe"></span>Update
                                    </button>-->

                                     <asp:Button ID="btnRefreshHotelMapping" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="btnRefreshHotelMapping_Click"   />
                                </td>
                            </tr>

                            <tr>
                                <td>Activities</td>
                                <td>Master</td>
                                <td>01/01/1900</td>
                                <td>
                                    <button type="button" class="btn btn-default">
                                        <span class="glyphicon glyphicon-cog"></span>Update
                                    </button>
                                </td>
                            </tr>

                            <tr>
                                <td>Activities</td>
                                <td>Mapping</td>
                               <!-- <td>01/01/1900</td>-->
                                <td><asp:Label Text="" ID="LastUpdatedActivityMapping" runat="server" ></asp:Label></td>
                                <td>
                                   <!-- <button type="button" class="btn btn-default">
                                        <span class="glyphicon glyphicon-globe"></span>Update
                                    </button>-->
                                    <asp:Button ID="btnRefreshActivityMapping" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="btnRefreshActivityMapping_Click"   />
                                </td>
                            </tr>

                            <tr>
                                <td>Holidays</td>
                                <td>Master</td>
                                <td>01/01/1900</td>
                                <td>
                                    <button type="button" class="btn btn-default">
                                        <span class="glyphicon glyphicon-cog"></span>Update
                                    </button>
                                </td>
                            </tr>

                            <tr>
                                <td>Holidays</td>
                                <td>Mapping</td>
                                <td>01/01/1900</td>
                                <td>
                                    <button type="button" class="btn btn-default">
                                        <span class="glyphicon glyphicon-globe"></span>Update
                                    </button>
                                </td>
                            </tr>

                        </tbody>
                    </table>

                </div>
            </div>




        </div>


        <div class="col-lg-4">
            <div class="panel panel-default">
                <div class="panel-heading">System</div>
                <div class="panel-body">

                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>Element</th>
                                <th>Type</th>
                                <th>Last Updated</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Users</td>
                                <td>Master</td>
                                <td>01/01/1900</td>
                                <td>
                                    <button type="button" class="btn btn-default">
                                        <span class="glyphicon glyphicon-cog"></span>Update
                                    </button>
                                </td>
                            </tr>

                            <tr>
                                <td>Permissions</td>
                                <td>Master</td>
                                <td>01/01/1900</td>
                                <td>
                                    <button type="button" class="btn btn-default">
                                        <span class="glyphicon glyphicon-cog"></span>Update
                                    </button>
                                </td>
                            </tr>

                            <tr>
                                <td>Suppliers</td>
                                <td>Master</td>
                                <!--<td>01/01/1900</td>-->
                                <td><asp:Label Text="" ID="LastUpdatedSupplierMapping" runat="server" ></asp:Label></td>
                                <td>
                                  <!-- <button type="button" class="btn btn-default">
                                        <span class="glyphicon glyphicon-cog"></span>Update
                                    </button>-->
                                    <asp:Button ID="btnRefreshSupplyMaster" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="btnRefreshSupplyMaster_Click"   />

                                </td>
                            </tr>

                        </tbody>
                    </table>


                </div>
            </div>


        </div>

    </ContentTemplate>
    </asp:UpdatePanel>


    </div>

</asp:Content>
