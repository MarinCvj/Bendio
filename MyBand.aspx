<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyBand.aspx.cs" Inherits="Bendio.MyBand" %>

<%@ Register Src="~/Controls/Navigation_bar.ascx" TagPrefix="uc1" TagName="Navigation_bar" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>My band</title>
    <link href="css/My_band.css" rel="stylesheet" />
    <link href="css/Navigation_bar.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Navigation_bar runat="server" ID="Navigation_bar" />
        <div class="my-band-container">
            <asp:TextBox runat="server" ID="test"></asp:TextBox>
        </div>
    </form>
</body>
</html>
