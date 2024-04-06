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
        <asp:Panel runat="server" ID="container" class="container">
            <h1>My band</h1>

            <div class="band-info">
                <asp:Panel CssClass="default-settings" runat="server" ID="band_default_settings">
                    <div class="band-info-parts">
                        <p>Name: </p>
                        <asp:Label runat="server" ID="band_name"></asp:Label>
                    </div>

                    <div class="band-info-parts">
                        <p>Number of members: </p>
                        <asp:Label runat="server" ID="band_members"></asp:Label>
                    </div>

                    <div class="band-info-parts">
                        <p>Description: </p>
                        <asp:Label runat="server" ID="band_description"></asp:Label>
                    </div>

                    <div class="band-info-parts">
                        <p>Band code: </p>
                        <asp:Label runat="server" ID="band_code"></asp:Label>
                    </div>

                    <div class="band-info-parts">
                        <p>Band owner: </p>
                        <asp:Label runat="server" ID="band_owner"></asp:Label>
                    </div>
                    <p class="label"> Give the band code number to other band members so they can join the band. </p>
                    <asp:Label runat="server" CssClass="label" ID="rule"> Only the band owner can make changes. </asp:Label>
                    <asp:Button runat="server" ID="change_settings_btn" OnClick="Change_settings" Text="Change settings" CssClass="btn"/>
                    <asp:Button runat="server" ID="delete_band_btn" OnClick="Delete_band" Text="Delete band" CssClass="btn"/>
                    <asp:Button runat="server" ID="calendar_btn" OnClick="Calendar_btn" Text="Rehersal calendar" CssClass="btn"/>
                </asp:Panel>

                <asp:Panel class="change-settings" runat="server" ID="change_settings">
                    <div class="band-info-parts">
                        <b>Name: </b>
                        <asp:TextBox runat="server" ID="change_name" MaxLength="50"></asp:TextBox>
                    </div>

                    <div class="band-info-parts">
                        <b>Number of members: </b>
                        <asp:TextBox runat="server" ID="change_members" TextMode="Number"></asp:TextBox>
                    </div>

                    <div class="band-info-parts">
                        <b>Description: </b>
                        <asp:TextBox runat="server" TextMode="MultiLine" MaxLength="100" CssClass="desc" placeholder="About your band" ID="change_description"></asp:TextBox>
                    </div>
                    <asp:Button runat="server" OnClick="Save_settings" Text="Save" CssClass="btn"/>
                    <asp:Button runat="server" OnClick="Cancel" Text="Cancel" CssClass="btn"/>
                </asp:Panel>

                <asp:Panel runat="server" CssClass="calendar" ID="calendar">
                    <div class="rehersal-info col-names">
                        <b>ID</b>
                        <b>Date</b>
                        <b>Time</b>
                    </div>
                    <asp:Repeater runat="server" ID="rehersal">
                        <ItemTemplate>
                            <div class="rehersal-info">
                                <span><%# DataBinder.Eval(Container.DataItem,"ID") %></span>
                                <span><%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"date")).ToString("yyyy-MM-dd") %></span>
                                <span><%# DataBinder.Eval(Container.DataItem,"time") %></span>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Button runat="server" OnClick="New_rehersal" ID="new_rehersal_btn" Text="Add a new rehersal" CssClass="btn" style="margin-top: 5rem;"/>
                    <asp:Button runat="server" OnClick="Cancel" Text="Back" CssClass="btn"/>
                    <asp:Button runat="server" OnClick="Delete_rehersal" ID="del_rehersal_btn" Text="Delete rehersal" CssClass="btn" />

                    <asp:Panel runat="server" CssClass="delete-rehersal" ID="del_rehersal">
                        <p>To delete a rehesal you need to input the ID of a rehersal you would like to delete.</p>
                        <div>
                            <p>ID: </p>
                            <asp:TextBox runat="server" ID="id_of_rehersal" placeholder="0" TextMode="Number"></asp:TextBox>
                        </div>
                        <asp:Button runat="server" OnClick="Submit_ID_of_rehersal" Text="Submit" CssClass="btn"/>
                        <asp:Button runat="server" OnClick="Cancel_del_rehersal_btn" Text="Cancel" CssClass="btn"/>
                        <asp:Label runat="server" ID="successfull_del" CssClass="del-info" Visible="false" Text="You successfully deleted a rehersal!"></asp:Label>
                        <asp:Label runat="server" ID="unsuccessfull_del" CssClass="del-info" Visible="false" Text="Please check your ID."></asp:Label>
                    </asp:Panel>
                </asp:Panel>

                <asp:Panel runat="server" CssClass="new-rehersal" ID="new_rehersal_container">
                    <div>
                        <p>Date</p>
                        <asp:TextBox runat="server" ID="new_rehersal_date" TextMode="Date"></asp:TextBox>
                    </div>
                    <div>
                        <p>Time</p>
                        <asp:TextBox runat="server" ID="new_rehersal_time" TextMode="Time"></asp:TextBox>
                    </div>
                    <asp:Button runat="server" OnClick="Add_rehersal" Text="Add" CssClass="btn"/>
                    <asp:Button runat="server" OnClick="Back_of_add_rehersal" Text="Back" CssClass="btn"/>
                </asp:Panel>

                <asp:Panel CssClass="join-make-band" runat="server" ID="join_make_band">
                    <h3>You don't have a band</h3>
                    <b>Create a new band or join one</b>
                    <asp:Button runat="server" OnClick="New_band" Text="Create a new band" CssClass="btn"/>
                    <asp:Button runat="server" OnClick="Join_band" Text="Join a band" CssClass="btn"/>
                </asp:Panel>
            </div>
        </asp:Panel>
        <asp:Panel CssClass="band-code" runat="server" ID="enter_band_code">
            <asp:Button runat="server" CssClass="close" OnClick="Close" Text="+"></asp:Button>
            <b>Enter the band code that the owner gave to you:</b>
            <asp:Label runat="server" ID="invalid_band_code" CssClass="invalid_code" Text="Band code is invalid." Visible="false"></asp:Label>
            <asp:TextBox runat="server" ID="code" placeholder="000000" style="text-transform:uppercase" MaxLength="6"></asp:TextBox>
            <asp:Button runat="server" OnClick="Enter_band_code" Text="Enter" CssClass="btn"/>
        </asp:Panel>
    </form>
</body>
</html>
