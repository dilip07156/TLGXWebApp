<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TLGX_Consumer.Account.Login" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="../../Scripts/Styles/bootstrap.css" rel="stylesheet" />--%>
    <link href="../../Scripts/Styles/Login/core.css" rel="stylesheet" />
    <link href="../../Scripts/Styles/Login/login.css" rel="stylesheet" />
    <link href="../../Scripts/Styles/Login/responsive.css" rel="stylesheet" />
    <%-- <script src="js/custom.js" type="text/javascript"></script>--%>
    <link href="../../Scripts/Styles/Login/googlefontapis.css" rel="stylesheet" />
</head>
<body>

    <main>
        <div class="main_left">
            <div class="login_overlay"></div>
            <div class="main_left_container box_size">
                <div class="login_date_block">
                    <div class="date_block_left"><%= DateTime.Now.ToString("dd") %></div>
                    <div class="date_block_right">
                        <%= DateTime.Now.ToString("MMM") %><br>
                        <%= DateTime.Now.ToString("yyyy") %>
                    </div>
                </div>
                <div class="login_info_block">
                    <h4>nakshatra</h4>
                    <p>
                       <ul>
                           <li>a group of stars forming a recognizable pattern that is traditionally named after its apparent form or identified with a mythological figure</li>
                           <li>a group of associated or similar people or things.</li>
                       </ul> 
                    </p>
                </div>
            </div>
        </div>

        <div class="main_right box_size">
            <div class="main_right_container v_center relative">
                <div class="login_logo">
                    <img src="../images/login_logo.png" />
                </div>
                <h2>welcome to nakshatra</h2>
                <div class="login_container">
                   <%-- <h3>Member Login</h3>--%>
                    <form id="form1" runat="server">
                        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                            <p class="text-danger">
                                <asp:Literal runat="server" ID="FailureText" />
                            </p>
                        </asp:PlaceHolder>
                        <div class="input_block">
                            <i class="user"></i>
                            <input type="Email" runat="server" id="Email" placeholder="User Name" class="box_size" />
                        </div>

                        <div class="input_block">
                            <i class="password"></i>
                            <input type="password" runat="server" id="Password" placeholder="Password" class="box_size" />
                        </div>

                        <div class="input_block">
                            <%--<button>Login</button>--%>
                            <asp:Button runat="server" Text="Login" OnClick="LogIn" CssClass="button" />
                            <div class="forgot" style="display: none;"><a href="#">Forgot Password?</a></div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </main>

</body>
</html>
