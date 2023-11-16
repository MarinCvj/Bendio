<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navigation_bar.ascx.cs" Inherits="Bendio.Controls.Navigation_bar" %>
<nav class="navbar">
    <a href="Home.aspx" class="logo">Bendio</a>
    <ul class="nav-links">
        <li><a href="Home.aspx">Home</a></li>
        <li><a href="Account.aspx">Account</a></li>
        <li><a href="NewBand.aspx">New band</a></li>
        <li><a href="MyBand.aspx">My band</a></li>
    </ul>
    <div class="burger" id="burger">
        <div class="line1"></div>
        <div class="line2"></div>
        <div class="line3"></div>
    </div>
</nav>
<script type="text/javascript">
    const burger = document.querySelector('.burger');
    const navLinks = document.querySelector('.nav-links');

    burger.addEventListener('click', () => {
        navLinks.classList.toggle('active');
        burger.classList.toggle('active');
    });
</script>
