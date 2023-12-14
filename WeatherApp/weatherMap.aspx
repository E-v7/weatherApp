<%@ Page Title="Weather Map" Language="C#" MasterPageFile="~/Header.master" AutoEventWireup="true" CodeBehind="weatherMap.aspx.cs" Inherits="WeatherApp.weatherMap" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>

<asp:Content ID="mainContent" ContentPlaceHolderID="MainContent" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<body> 
   <div id="map"></div>  
    <div id="text"></div>
</body>
</html>
</asp:Content>
<asp:Content ID="search" ContentPlaceHolderID="PageButton" runat="server">
    <asp:Button ID="city" runat="server" Text="Go" OnClick="GoToLocation" />
</asp:Content>
