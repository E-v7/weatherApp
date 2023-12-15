<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Header.master" CodeBehind="hourlyWeather.aspx.cs" Inherits="WeatherApp.hourlyWeather" %>

<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>

<asp:Content ID="mainContent" ContentPlaceHolderID="MainContent" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <form id="form1" runat="server">
        <header>
            <h1 id="title">The Weather App</h1>
            <div id="navigation_bar">
                <a href="index.aspx">Home</a>
                <a href="hourlyWeather.aspx">Current</a>
                <a href="#">OTHER</a>
            </div>
        </header>

        <div id="testing" runat="server"></div>
    </form>
        <div>
            <p>This is the hourly page for the webapp</p>
        </div>
</body>
</html>
</asp:Content>
<asp:Content ID="search" ContentPlaceHolderID="PageButton" runat="server">
    <asp:Button ID="city" runat="server" Text="Go" />
</asp:Content>
