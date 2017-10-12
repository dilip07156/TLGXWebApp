<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Exclusion.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.Exclusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<div class="col-lg-3 pull-right row">
    <div class="form-group pull-right">
        <div class="input-group" runat="server" id="divDropdownForEntries">
            <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
            <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control">
                <%--OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged"--%>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>25</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>100</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
</div>
<div class="row">
    <div class="panel-body">
        <asp:GridView ID="gvActInclusionSearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
            EmptyDataText="No Activity Exclusion Found" CssClass="table table-hover table-striped"
            AutoGenerateColumns="false" OnPageIndexChanging="gvActInclusionSearch_PageIndexChanging"
            OnRowCommand="gvActInclusionSearch_RowCommand"  DataKeyNames="Activity_Inclusions_Id"><%--OnRowDataBound="gvActInclusionSearch_RowDataBound"--%>
            <Columns>
                <asp:BoundField HeaderText="Exclusion Type" DataField="InclusionType" />
                <asp:BoundField HeaderText="Exclusion Name" DataField="InclusionName" />
                <asp:BoundField HeaderText="Exclusion For" DataField="InclusionFor" />
                <asp:BoundField HeaderText="Description" DataField="InclusionDescription" />
                <asp:BoundField HeaderText="Is Inclusion" DataField="IsInclusion" />
                <asp:BoundField HeaderText="Upload Date" DataField="Create_Date" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}" />

                <%--<asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Editing" CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_Inclusions_Id") %>'><%--Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Activity_Inclusions_Id") %>'>
                                  <span aria-hidden="true">Edit</span>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <%--<asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Editing" CssClass="btn btn-default"
                            Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Activity_Inclusions_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
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
    </div>
</div>


