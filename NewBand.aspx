<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewBand.aspx.cs" Inherits="Bendio.New_band" %>

<%@ Register Src="~/Controls/Navigation_bar.ascx" TagPrefix="uc1" TagName="Navigation_bar" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>New band</title>
    <link href="css/All.css" rel="stylesheet" />
    <link href="css/New_band.css" rel="stylesheet" />
    <link href="css/Navigation_bar.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Navigation_bar runat="server" ID="Navigation_bar" />
        <div class="container">
            <h1>Create a new band</h1>

            <asp:Label runat="server" ID="already_have_a_band" Visible="false"> You already have a band. <br /> This page works for just one band. <br /> You can delete your current band to create another.</asp:Label>

            <asp:Panel runat="server" CssClass="band-info" ID="band_info">
                <p>Band name </p>
                <asp:TextBox runat="server" ID="band_name" placeholder="My band"></asp:TextBox>

                <p>Number of band members </p>
                <asp:TextBox runat="server" TextMode="Number" ID="members_num" placeholder="Number of members"></asp:TextBox>
                <br /><br />
                <asp:Label runat="server" ID="zero_members" Text="The band can't have 0 members." Visible="false"></asp:Label>

                <p>Description</p>
                <asp:TextBox runat="server" CssClass="desc" TextMode="MultiLine" ID="description" placeholder="About your band"></asp:TextBox>

                <asp:Button runat="server" CssClass="btn" OnClick="Submit" Text="Submit" />
            </asp:Panel>
        </div>
    </form>
</body>
</html>
