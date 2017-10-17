<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PricesNDeals.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.PricesNDeals" %>
<%@ Register Src="~/controls/activity/ManageActivityFlavours/Prices.ascx" TagPrefix="uc1" TagName="Prices" %>



<script type="text/javascript">
    function showAddNewActivityModal() {
        $("#moAddNewActivityModal").modal('show');
    }
    function closeAddNewActivityModal() {
        $("#moAddNewActivityModal").modal('hide');
    }
    //function page_load(sender, args) {
    //    closeAddNewActivityModal();
    //}
    //for changing the tab of Inclusion / Exclusion
    $(function () {
        var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "pricess";
        $('#Tabs a[href="#' + tabName + '"]').tab('show');
        $("#Tabs a").click(function () {
            $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
        });
    });
</script>

<asp:UpdatePanel ID="updMedia" runat="server">
    <ContentTemplate>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4>Prices And Deals</h4>
            </div>
            <div class="panel panel-body"  id="searchResult">
                <div id="Tabs" role="tabpanel">
                <!-- Nav tabs -->

                <ul class="nav nav-tabs" role="tablist">
                    <li><a role="tab" data-toggle="tab" href="#pricess">Prices</a></li>
                    <li><a role="tab" data-toggle="tab" href="#dealss">Deals</a></li>
                </ul>
                <!-- Tab panes -->
                <div class="tab-content">
                    <div role="tabpanel" class="tab-pane active" id="pricess" >
                        <uc1:Prices runat="server" id="Prices" />
                    </div>
                    <div role="tabpanel" class="tab-pane" id="dealss">
                        <%--<uc1:Exclusion runat="server" ID="Exclusion1" />--%>
                    </div>
                </div>

            </div>
                <asp:HiddenField ID="HiddenField1PnD" runat="server" />
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>


