<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Bendio.Home" %>

<%@ Register Src="~/Controls/Navigation_bar.ascx" TagPrefix="uc1" TagName="Navigation_bar" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Home</title>
    <link href="css/All.css" rel="stylesheet" />
    <link href="css/Home.css" rel="stylesheet" />
    <link href="css/Navigation_bar.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Navigation_bar runat="server" id="Navigation_bar" />
        <div class="container">
            <h1>Welcome to Bendio</h1>
            <h2>Here you can make your band better.</h2>
            <h2>Here is how. </h2>

            <div class="content">
                <h3>1. Make your account.</h3>
                <a href="Account.aspx">Go to account</a>

                <h3>2. Create a new band.</h3>
                <p>Go to new band page in the navigation bar and create a new band.</p>

                <h3>3. Add some members to your band.</h3>
                <p>The band code is unique for every band. <br />Give it to your bandmate so they can join your band. <br />Only the band owner/creator of the band can make some changes.</p>

                <h3>4. Put your rehersals on a schedule</h3>
                <p>Keep track of your rehearsals in our rehearsal calendar, add and delete them as you wish.</p>

                <h3>5. Put some files in your sharing space</h3>
                <p>Bands often need a file sharing space, we got it. <br />Put your lyrics for songs and have everything lined up as you like it.</p>
            </div>
            
            <h2>About us</h2>
            <div class="content">
                <p>I am Marin, 18 year old drummer who is in high school.</p>
                <p>This project started as a school project, but I wanted to connect my two passions, programming and music.</p>
                <p>Since I am in a band, I wondered what do I need for my band. A sharing space and a rehersal schedule.</p>
                <p>After a few months of hard work I made Bendio.</p>
                <p>This website will help you manage your band and make things a lot easier.</p>
            </div>
        </div>
    </form>
</body>
</html>
