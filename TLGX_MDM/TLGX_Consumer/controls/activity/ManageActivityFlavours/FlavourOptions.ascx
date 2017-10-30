<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlavourOptions.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.FlavourOptions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:UpdatePanel ID="updPanFlavourOptions" runat="server">
    <ContentTemplate>
        <div id="dvMsg" runat="server" style="display: none;"></div>

        <%--<div class="container">
            <div class="form-group col-md-12">
                <asp:LinkButton ID="btnNewActivity" runat="server" Text="Add New" CssClass="btn btn-primary btn-sm pull-right" OnClientClick="showAddEditModal();"  />
            </div>
        </div>--%>

        <%--<headertemplate>
            <div class="container">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vldGrpRules" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
            </div>
        </headertemplate>--%>

        <div class="form-group">
            <h4 class="panel-title pull-left">Flavour Options (Total Count:
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
                <asp:BoundField HeaderText="Activity Type" DataField="Activity_Type" />
                <asp:BoundField HeaderText="Deal Text" DataField="Activity_DealText" />
                <asp:BoundField HeaderText="Status" DataField="Status" />
                <asp:BoundField HeaderText="" DataField="Create_Date" DataFormatString="{0:dd/MM/yyyy}" />

                <%--<asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CssClass="btn btn-default"
                            CommandArgument='<%# Bind("Activity_Inclusions_Id") %>' Enabled='<%# (bool)Eval("IsActive") %>' OnClientClick="showAddEditModal();"> 
                                  <span aria-hidden="true" class="glyphicon glyphicon-edit"> Edit</span>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <%--<asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                            CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_Inclusions_Id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat" %>'></span>
                                                    <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>

            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>

    </ContentTemplate>
</asp:UpdatePanel>
