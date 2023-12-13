<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="weatherMap.aspx.cs" Inherits="WeatherApp.weatherMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Weather Map</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.7.1/dist/leaflet.css" />
    <link href="styles/style.css" rel="stylesheet" />
    <script src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js"></script>
<style>
    #map {
        height: 500px;
        width: 100%;
    }
</style>
<script>
    function initMap(latitude, longitude, apiKey, description) {
        var map = L.map('map');

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 9,
        }).addTo(map);

        var weatherLayer = L.tileLayer('https://tile.openweathermap.org/map/{layer}/{z}/{x}/{y}.png?appid=' + apiKey, {
            layer: 'temp_new',
            maxZoom: 9
        }).addTo(map);

        map.setView([latitude, longitude], 5);  //zoom in to the location
        L.marker([latitude, longitude]).addTo(map).bindPopup(description).openPopup(); //set a marker
    }
</script>
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
