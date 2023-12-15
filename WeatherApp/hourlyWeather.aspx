<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Header.master" CodeBehind="hourlyWeather.aspx.cs" Inherits="WeatherApp.hourlyWeather" %>

<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>

<asp:Content ID="mainContent" ContentPlaceHolderID="MainContent" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<body>

        <div>
            <p>This is the hourly page for the webapp</p>
        </div>
</body>
</html>
</asp:Content>
<asp:Content ID="search" ContentPlaceHolderID="PageButton" runat="server">
    <asp:Button ID="city" runat="server" Text="Go" />
</asp:Content>
