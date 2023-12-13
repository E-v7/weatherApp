<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="weatherMap.aspx.cs" Inherits="WeatherApp.weatherMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Weather Map</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.7.1/dist/leaflet.css" />
    <link href="styles/style.css" rel="stylesheet" />
    <script src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js"></script>
    <script type="text/javascript" src="scripts/weatherMap_script.js"></script>
<style>
    #map {
        height: 600px;
        width: 100%;
    }
</style>
</head>
<body>
    <form id="weatherMap" runat="server">
        <header>
            <h1 id="title">The Weather App</h1>
            <div id="navigation_bar">
                <a href="index.aspx">Home</a>
                <a href="hourlyWeather.aspx">Hourly</a>
                <a href="weatherMap.aspx">WeatherMap</a>
                <div class="searchbox">
                    <input class="textbox" id="city" name="city" type="text"/>
                    <asp:Button ID="getWeatherButton" runat="server" Text="Go" />
                </div>
            </div>
        </header>
        <div id="map"></div>
    </form>
</body>
</html>
