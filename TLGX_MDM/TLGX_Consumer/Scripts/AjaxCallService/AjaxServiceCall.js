function AjaxCall(WebConfigKeyWord,serviceType) {

    var serviceUrl = '<%=ConfigurationManager.AppSettings["' + WebConfigKeyWord + '"] %>';
    var _serviceType = serviceType;

    $.ajax({
        type: _serviceType,
        contentType: "application/json; charset=utf-8",
        url: serviceUrl,
        success: function (data) {
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    listItems += "<option value='" + data[i].RoleID + "'>" + data[i].RoleName + "</option>";
                }
                $("#ddlRole").html(listItems);
            }
        },
        error: function (result) {
        }
    });
}