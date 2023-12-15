<%@ Page Title="Weather Map" Language="C#" MasterPageFile="~/Header.master" AutoEventWireup="true" CodeBehind="weatherMap.aspx.cs" Inherits="WeatherApp.weatherMap" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="scripts/weatherMap_script.js"></script>
    <script src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js"></script>
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.7.1/dist/leaflet.css" />
    <style>
        #map {
            height: 420px;
            width: 100%;
        }
    </style>
</asp:Content>

<asp:Content ID="search" ContentPlaceHolderID="PageButton" runat="server">
    <asp:Button ID="city" runat="server" Text="Go" OnClick="GoToLocation" />
</asp:Content>

<asp:Content ID="mainContent" ContentPlaceHolderID="MainContent" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<body> 
   <div id="map"></div>  
</body>
</html>
</asp:Content>

