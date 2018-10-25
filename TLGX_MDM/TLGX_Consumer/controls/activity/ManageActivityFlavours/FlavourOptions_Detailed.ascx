<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlavourOptions_Detailed.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.FlavourOptions_Detailed" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<link href="maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
<script src="maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>
<script src="code.jquery.com/jquery-1.11.1.min.js"></script>

<style>
    /*
snippet from Animate.css - zoomIn effect
*/
    .black {
        color: #333333;
    }

    .central {
        vertical-align: middle !important;
        text-align: center !important;
    }

    .vl {
        border-left: 6px solid black;
        height: 5px;
    }

    .animated {
        -webkit-animation-duration: 1s;
        animation-duration: 1s;
        -webkit-animation-fill-mode: both;
        animation-fill-mode: both
    }

        .animated.infinite {
            -webkit-animation-iteration-count: infinite;
            animation-iteration-count: infinite
        }

        .animated.hinge {
            -webkit-animation-duration: 2s;
            animation-duration: 2s
        }

    @-webkit-keyframes zoomIn {
        0% {
            opacity: 0;
            -webkit-transform: scale3d(.3,.3,.3);
            transform: scale3d(.3,.3,.3)
        }

        50% {
            opacity: 1
        }
    }

    @keyframes zoomIn {
        0% {
            opacity: 0;
            -webkit-transform: scale3d(.3,.3,.3);
            transform: scale3d(.3,.3,.3)
        }

        50% {
            opacity: 1
        }
    }

    .zoomIn {
        -webkit-animation-name: zoomIn;
        animation-name: zoomIn
    }

    @-webkit-keyframes zoomOut {
        0% {
            opacity: 1
        }

        50% {
            opacity: 0;
            -webkit-transform: scale3d(.3,.3,.3);
            transform: scale3d(.3,.3,.3)
        }

        100% {
            opacity: 0
        }
    }

    @keyframes zoomOut {
        0% {
            opacity: 1
        }

        50% {
            opacity: 0;
            -webkit-transform: scale3d(.3,.3,.3);
            transform: scale3d(.3,.3,.3)
        }

        100% {
            opacity: 0
        }
    }

    .zoomOut {
        -webkit-animation-name: zoomOut;
        animation-name: zoomOut
    }

    #accordion .panel-title i.glyphicon {
        -moz-transition: -moz-transform 0.5s ease-in-out;
        -o-transition: -o-transform 0.5s ease-in-out;
        -webkit-transition: -webkit-transform 0.5s ease-in-out;
        transition: transform 0.5s ease-in-out;
    }

    .rotate-icon {
        -webkit-transform: rotate(-225deg);
        -moz-transform: rotate(-225deg);
        transform: rotate(-225deg);
    }

    .panel1 {
        border: 0px;
        border-bottom: 3px solid #163572 !important;
    }

    .panel-group .panel1 + .panel {
        margin-top: 0px;
    }

    .panel-group .panel1 {
        border-radius: 0px;
    }

    .panel-heading {
        border-radius: 0px;
        color: white;
        padding: 25px 15px;
    }

    .panel-custom > .panel-heading {
        background-color: Highlight;
    }

    .panel-group .panel1:last-child {
        border-bottom: 5px solid #163572;
    }

    panel-collapse .collapse.in {
        border-bottom: 0;
    }
</style>

<script>
    $(function () {

        function toggleChevron(e) {
            $(e.target)
                .prev('.panel-heading')
                .find("i")
                .toggleClass('rotate-icon');
            $('.panel-body.animated').toggleClass('zoomIn zoomOut');
        }

        $('#accordion').on('hide.bs.collapse', toggleChevron);
        $('#accordion').on('show.bs.collapse', toggleChevron);
    })


    function toggleChevron(e) {
        $(e.target)
            .prev('.panel-heading')
            .find("i")
            .toggleClass('rotate-icon');
        $('.panel-body.animated').toggleClass('zoomIn zoomOut');

        //$(e).find('i').toggleClass('rotate-icon');
        $(e).find('i').toggleClass("glyphicon-plus glyphicon-minus");;


        $('#accordion').on('hide.bs.collapse', toggleChevron);
        $('#accordion').on('show.bs.collapse', toggleChevron);
    }


</script>
<asp:UpdatePanel ID="updPanFlavourOptions" runat="server">
    <ContentTemplate>
        <div id="dvMsg" runat="server" style="display: none;"></div>

        <div class="form-group">
            <h4 class="panel-title pull-left">Flavour Option (Total Count:
            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</h4>
        </div>

        <div class="row col-lg-3 pull-right">
            <div class="form-group pull-right">
                <div class="input-group" runat="server" id="divDropdownForEntries">
                    <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                    <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <asp:GridView ID="gvActFlavourOptins" runat="server" AllowPaging="True" AllowCustomPaging="true"
            EmptyDataText="No Data Found" CssClass="table table-hover table-striped"
            AutoGenerateColumns="false" OnPageIndexChanging="gvActFlavourOptins_PageIndexChanging"
            OnRowCommand="gvActFlavourOptins_RowCommand" DataKeyNames="Activity_FlavourOptions_Id"
            OnRowDataBound="gvActFlavourOptins_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="Flavour Name" DataField="Activity_FlavourName" />
                <asp:BoundField HeaderText="Option Name" DataField="Activity_OptionName" />
                <asp:BoundField HeaderText="Option Code" DataField="Activity_OptionCode" />
                <asp:BoundField HeaderText="Option Description" DataField="Activity_OptionDescription" />
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>

        <asp:Repeater ID="rptCustomers" runat="server" OnItemDataBound="rptCustomers_ItemDataBound">
            <HeaderTemplate>
                <table cellspacing="0" rules="all" class="table table-hover table-striped" border="1">
                    <tr>
                        <th scope="col" class="col-sm-2">Flavour Name
                        </th>
                        <th scope="col" class="col-sm-2">Option Name
                        </th>
                        <th scope="col" class="col-sm-3">Option Code
                        </th>
                        <th scope="col" class="col-sm-5">Option Description
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td colspan="4" style="padding: 1px;">
                        <div class="container">
                            <div class="row">
                                <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                    <div class="panel1 panel-custom">
                                        <div class="panel-heading" role="tab" id="heading<%# Container.ItemIndex%>name" style="padding-top: 20px; padding-bottom: 40px; height: 40px;">
                                            <h4 class="panel-title">
                                                <div class="col-sm-2" style="padding-left: 5px; padding-right: 5px">
                                                    <a onclick="toggleChevron(this);" class="black" data-toggle="collapse" data-parent="#accordion" data-target="#collapse<%# Container.ItemIndex%>name" href="#collapse<%# Container.ItemIndex%>name" aria-expanded="true" aria-controls="collapse<%# Container.ItemIndex%>name">
                                                        <i class="glyphicon glyphicon-plus"></i><%# Eval("Activity_FlavourName") %></a>
                                                    <div class="vl" style="float: right;"></div>
                                                </div>
                                                <div class="col-sm-2 black" style="padding-left: 5px; padding-right: 5px">
                                                    <%# Eval("Activity_OptionName") %><div class="vl" style="float: right;"></div>
                                                </div>
                                                <div class="col-sm-3 black" style="padding-left: 5px; padding-right: 5px">
                                                    <%# Eval("Activity_OptionCode") %><div class="vl" style="float: right;"></div>
                                                </div>
                                                <div class="col-sm-5 black" style="padding-left: 5px; padding-right: 5px"><%# Eval("Activity_OptionDescription") %></div>
                                            </h4>


                                        </div>
                                        <div id="collapse<%# Container.ItemIndex%>name" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading<%# Container.ItemIndex%>name">
                                            <div class="panel-body animated zoomOut">

                                                <asp:Label runat="server" ID="lblId" Visible="false" Text='<%# Eval("Activity_FlavourOptions_Id") %>' />
                                                <asp:GridView ID="gvattribute" runat="server"
                                                    EmptyDataText="No Data Found" CssClass="table table-hover table-striped"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-CssClass="col-sm-2" HeaderText="Attribute Type" DataField="AttributeType" />
                                                        <asp:BoundField ItemStyle-CssClass="col-sm-3" HeaderText="Attribute Subtype" DataField="AttributeSubType" />
                                                        <asp:BoundField ItemStyle-CssClass="col-sm-6" HeaderText="Attribute Value" DataField="AttributeValue" />
                                                        <%--<asp:TemplateField ItemStyle-CssClass="central" ItemStyle-BackColor="LightGray">
                                                            <ItemTemplate>
                                                                <i style="color: yellow;" class="glyphicon glyphicon-edit"></i>
                                                                &nbsp;&nbsp;
                                                                <i style="color: red;" class="glyphicon glyphicon-trash"></i>
                                                                &nbsp;&nbsp;
                                                                <i style="color: green;" class="glyphicon glyphicon-refresh"></i>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </ContentTemplate>
</asp:UpdatePanel>
