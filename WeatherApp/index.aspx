<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WeatherApp.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Weather App Home</title>
    <link href="styles/style.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script type="text/javascript" src="scripts/script.js"></script>
</head>
<body>
    <form id="MainPageForm" runat="server">
        <header>
            <h1 id="title">The Weather App</h1>
            <div id="navigation_bar">
                <a href="index.aspx">Home</a>
                <a href="hourlyWeather.aspx">Current</a>
                <a href="5DayWeather.aspx">5 Day</a>
                <a href="#">OTHER</a>
                <input id="location" placeholder="Enter a location" type="text" runat="server"/>
            </div>
        </header>

        <div class="main-container">
            <div class="left-container">
                <h2>Do you consent to sharing your location?</h2>
                <label class="switch">
                    <input type="checkbox" id="consentToggle"/>
                    <span class="slider round"></span>
                </label>
                <div id="weatherDetails" class="weather-details">
                    <!-- Weather details will be displayed here -->
                </div>
            </div>
            <div class="right-container">
                <input type="text" id="username" />
                <input type="password" id="password" />
                <button id="login">Login</button>
                <a href="#" id="registerLink">Register online now</a>
            </div>
        </div>

    </form>
</body>
</html>
