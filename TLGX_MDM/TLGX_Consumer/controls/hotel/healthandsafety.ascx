<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="healthandsafety.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.healthandsafety" %>

<asp:UpdatePanel ID="updPanHandS" runat="server">
    <ContentTemplate>
        <div class="container">
            <div id="dvMsg" runat="server" style="display: none;"></div>
        </div>
        <asp:FormView ID="frmHealthAndSafety" runat="server" DataKeyNames="Accommodation_HealthAndSafety_Id" DefaultMode="Insert" OnItemCommand="frmHealthAndSafety_ItemCommand">

            <HeaderTemplate>
                <div class="container">
                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldgrpHealthAndSafety" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                </div>
            </HeaderTemplate>

            <InsertItemTemplate>
                <div class="container">
                    <div class="panel panel-default">
                        <div class="panel-heading">Add Health and Safety</div>
                        <div class="panel-body">

                            <div class="col-lg-6">

                                <label class="control-label-mand" for="txtHSValue">HS Value</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHSValue" ErrorMessage="Please enter hotel safety value" Text="*" ValidationGroup="vldgrpHealthAndSafety" CssClass="text-danger"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtHSValue" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control" />



                                <label class="control-label-mand" for="txtRemarks">Remarks</label>
                                <asp:RequiredFieldValidator ID="vldtxtRemarks" runat="server" ControlToValidate="txtRemarks" ErrorMessage="Please enter hotel safety remarks" Text="*" ValidationGroup="vldgrpHealthAndSafety" CssClass="text-danger"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtRemarks" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control" />


                                <br />
                                <asp:LinkButton ID="btnSave" runat="server" CausesValidation="True" CommandName="Add" Text="Add Health and Safety" CssClass="btn btn-primary btn-sm" ValidationGroup="vldgrpHealthAndSafety" />



                            </div>

                            <div class="col-lg-6">

                                <label class="control-label-mand" for="ddlHSCategory">H/S Category</label>
                                <asp:RequiredFieldValidator ID="vldUpdateSource" runat="server" ControlToValidate="ddlHSCategory" ErrorMessage="Please select hotel safety category" Text="*" ValidationGroup="vldgrpHealthAndSafety" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>

                                <asp:DropDownList ID="ddlHSCategory" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>


                                <label class="control-label-mand" for="ddlHSName">H/S Name</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlHSName" ErrorMessage="Please select hotel safety name" Text="*" ValidationGroup="vldgrpHealthAndSafety" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>

                                <asp:DropDownList ID="ddlHSName" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>

                            </div>

                        </div>
                    </div>
                </div>
            </InsertItemTemplate>

            <EditItemTemplate>

                <div class="container">
                    <div class="panel panel-default">
                        <div class="panel-heading">Update Health and Safety</div>
                        <div class="panel-body">

                            <div class="col-lg-6">

                                <label class="control-label-mand" for="txtHSValue">HS Value</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHSValue" ErrorMessage="Please enter hotel safety value" Text="*" ValidationGroup="vldgrpHealthAndSafety" CssClass="text-danger"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtHSValue" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control" Text='<%# Bind("Description") %>' />



                                <label class="control-label-mand" for="txtRemarks">Remarks</label>
                                <asp:RequiredFieldValidator ID="vldtxtRemarks" runat="server" ControlToValidate="txtRemarks" ErrorMessage="Please enter hotel safety remarks" Text="*" ValidationGroup="vldgrpHealthAndSafety" CssClass="text-danger"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtRemarks" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control" Text='<%# Bind("Remarks") %>' />


                                <br />
                                <asp:LinkButton ID="btnSave" runat="server" CausesValidation="True" CommandName="Modify" Text="Update" CssClass="btn btn-primary btn-sm" ValidationGroup="vldgrpHealthAndSafety" />



                            </div>

                            <div class="col-lg-6">

                                <label class="control-label-mand" for="ddlHSCategory">H/S Category</label>
                                <asp:RequiredFieldValidator ID="vldUpdateSource" runat="server" ControlToValidate="ddlHSCategory" ErrorMessage="Please select hotel safety category" Text="*" ValidationGroup="vldgrpHealthAndSafety" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>

                                <asp:DropDownList ID="ddlHSCategory" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>


                                <label class="control-label-mand" for="ddlHSName">H/S Name</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlHSName" ErrorMessage="Please select hotel safety name" Text="*" ValidationGroup="vldgrpHealthAndSafety" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>

                                <asp:DropDownList ID="ddlHSName" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>

                                <label for="txtFrom">Updated</label>
                                <asp:TextBox ID="txtEditDate" runat="server" CssClass="form-control" Text='<%# Bind("Edit_Date", "{0:dd/MM/yyyy}") %>' Enabled="false" />

                            </div>

                        </div>
                    </div>
                </div>


            </EditItemTemplate>


        </asp:FormView>



        <br />
        <asp:GridView ID="grdHealthAndSafety" runat="server" DataKeyNames="Accommodation_HealthAndSafety_Id" AutoGenerateColumns="false" EmptyDataText="No H/S updates for this hotel" OnRowCommand="grdHealthAndSafety_RowCommand" CssClass="table table-hover table-striped" OnRowDataBound="grdHealthAndSafety_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Category" HeaderText="Category" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Description" HeaderText="HS Value" />
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                <asp:BoundField DataField="Edit_Date" HeaderText="Last Updates" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                            Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Accommodation_HealthAndSafety_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                            CssClass="btn btn-default" CommandArgument='<%# Bind("Accommodation_HealthAndSafety_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>


    </ContentTemplate>

</asp:UpdatePanel>
