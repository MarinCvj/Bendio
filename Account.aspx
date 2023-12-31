<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="Bendio.Account" %>

<%@ Register Src="~/Controls/Navigation_bar.ascx" TagPrefix="uc1" TagName="Navigation_bar" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Account</title>
    <link href="css/All.css" rel="stylesheet" />
    <link href="css/Account.css" rel="stylesheet" />
    <link href="css/Navigation_bar.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Navigation_bar runat="server" ID="Navigation_bar" />
        <div class="container">
            <h1>Create account or Log in</h1>
            <h4>You need to have an account to start a band.</h4>

            <div class="account">
                <div class="create-acc">
                    <h2>Create account</h2>
                    <p>Email Address</p>
                    <asp:TextBox runat="server" TextMode="Email" ID="email_create" placeholder="you@example.com"/>
                    <p>Password</p>
                    <asp:TextBox runat="server" TextMode="Password" ID="password_create" placeholder="********"/>

                    <asp:Button CssClass="btn" runat="server" OnClick="Create_Account" Text="Create Account"/>
                </div>
            
                <div class="log-in">
                    <h2>Log in</h2>
                    <p>Email Address</p>
                    <asp:TextBox runat="server" TextMode="Email" id="email_log" placeholder="you@example.com"/>
                    <p>Password</p>
                    <asp:TextBox runat="server" TextMode="Password" ID="password_log" placeholder="********"/>

                    <asp:Label runat="server" ID="wrong" Text="Wrong email or password." Visible="false"></asp:Label>

                    <asp:Button CssClass="btn" runat="server" OnClick="Log_in" Text="Log in" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
