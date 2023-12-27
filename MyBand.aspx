<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyBand.aspx.cs" Inherits="Bendio.MyBand" %>
<%@ Register Src="~/Controls/Navigation_bar.ascx" TagPrefix="uc1" TagName="Navigation_bar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>My band</title>
    <link href="css/All.css" rel="stylesheet" />
    <link href="css/My_band.css" rel="stylesheet" />
    <link href="css/Navigation_bar.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Navigation_bar runat="server" ID="Navigation_bar" />
        <div class="my-band-container">
            <h1>My band</h1>

            <div class="band-info">
                <asp:Panel CssClass="default-settings" runat="server" ID="band_default_settings">
                    <div class="band-info-parts">
                        <b>Name: </b>
                        <asp:Label runat="server" ID="band_name"></asp:Label>
                    </div>

                    <div class="band-info-parts">
                        <b>Number of members: </b>
                        <asp:Label runat="server" ID="band_members"></asp:Label>
                    </div>

                    <div class="band-info-parts">
                        <b>Description: </b>
                        <asp:Label runat="server" ID="band_description"></asp:Label>
                    </div>

                    <div class="band-info-parts">
                        <b>Band code: </b>
                        <asp:Label runat="server" ID="band_code"></asp:Label>
                    </div>
                    <p> Give this code to other band members so they can join your band.</p>
                    <asp:Button runat="server" OnClick="Change_settings" Text="Change settings" CssClass="btn"/>
                </asp:Panel>

                <asp:Panel class="change-settings" runat="server" ID="change_settings">
                    <div class="band-info-parts">
                        <b>Name: </b>
                        <asp:TextBox runat="server" ID="change_name"></asp:TextBox>
                    </div>

                    <div class="band-info-parts">
                        <b>Number of members: </b>
                        <asp:TextBox runat="server" ID="change_members"></asp:TextBox>
                    </div>

                    <div class="band-info-parts">
                        <b>Description: </b>
                        <asp:TextBox runat="server" TextMode="MultiLine" CssClass="desc" placeholder="About your band" ID="change_description"></asp:TextBox>
                    </div>
                    <asp:Button runat="server" OnClick="Save_settings" Text="Save" CssClass="btn"/>
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
