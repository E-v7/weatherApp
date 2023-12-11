<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WeatherApp.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Weather App Home</title>
    <link href="styles/style.css" rel="stylesheet" />
</head>
<body>
    <form id="MainPageForm" runat="server">
        <header>
            <h1 id="title">The Weather App</h1>
            <div id="navigation_bar">
                <a href="index.aspx">Home</a>
                <a href="hourlyWeather.aspx">Hourly</a>
                <a href="#">OTHER</a>
                <div class="searchbox">
                    <input class="textbox" id="city" name="city" type="text"/>
                    <asp:Button ID="getWeatherButton" runat="server" Text="Go" OnClick="GetWeather" />
                </div>
            </div>
        </header>
        
      <%--   <div id="weather_card_collection">
            <div class="weather_card">
                <h2 class="weather_card_location">Waterloo</h2>
                <p class="weather_card_description">Mon 1:00 PM, Mostly Sunny</p>
                <div class="weather_card_temperature">
                    <h1>23<sup>&deg;C</sup></h1>
                    <img src="images/clear-sky-day.png" />
                </div>
                <div class="weather_card_extras">
                    <div class="weather_card_precipitation">
                        <img src="images/rain-day.png" />
                        <p>2% Precp</p>
                    </div>
                    <div class="weather_card_wind">
                        <img src="images/mist-day.png" />
                        <p>23 km/h Winds</p>
                    </div>
                </div>
            </div>

            <div class="weather_card">
                <h2 class="weather_card_location">Waterloo</h2>
                <p class="weather_card_description">Mon 1:00 PM, Mostly Sunny</p>
                <div class="weather_card_temperature">
                    <h1>23<sup>&deg;C</sup></h1>
                    <img src="images/clear-sky-day.png" />
                </div>
                <div class="weather_card_extras">
                    <div class="weather_card_precipitation">
                        <img src="images/rain-day.png" />
                        <p>2% Precp</p>
                    </div>
                    <div class="weather_card_wind">
                        <img src="images/mist-day.png" />
                        <p>23 km/h Winds</p>
                    </div>
                </div>
            </div>

            <div class="weather_card">
                <h2 class="weather_card_location">Waterloo</h2>
                <p class="weather_card_description">Mon 1:00 PM, Mostly Sunny</p>
                <div class="weather_card_temperature">
                    <h1>23<sup>&deg;C</sup></h1>
                    <img src="images/clear-sky-day.png" />
                </div>
                <div class="weather_card_extras">
                    <div class="weather_card_precipitation">
                        <img src="images/rain-day.png" />
                        <p>2% Precp</p>
                    </div>
                    <div class="weather_card_wind">
                        <img src="images/mist-day.png" />
                        <p>23 km/h Winds</p>
                    </div>
                </div>
            </div>

            <div class="weather_card">
                <h2 class="weather_card_location">Waterloo</h2>
                <p class="weather_card_description">Mon 1:00 PM, Mostly Sunny</p>
                <div class="weather_card_temperature">
                    <h1>23<sup>&deg;C</sup></h1>
                    <img src="images/clear-sky-day.png" />
                </div>
                <div class="weather_card_extras">
                    <div class="weather_card_precipitation">
                        <img src="images/rain-day.png" />
                        <p>2% Precp</p>
                    </div>
                    <div class="weather_card_wind">
                        <img src="images/mist-day.png" />
                        <p>23 km/h Winds</p>
                    </div>
                </div>
            </div>

            <div class="weather_card">
                <h2 class="weather_card_location">Waterloo</h2>
                <p class="weather_card_description">Mon 1:00 PM, Mostly Sunny</p>
                <div class="weather_card_temperature">
                    <h1>23<sup>&deg;C</sup></h1>
                    <img src="images/clear-sky-day.png" />
                </div>
                <div class="weather_card_extras">
                    <div class="weather_card_precipitation">
                        <img src="images/rain-day.png" />
                        <p>2% Precp</p>
                    </div>
                    <div class="weather_card_wind">
                        <img src="images/mist-day.png" />
                        <p>23 km/h Winds</p>
                    </div>
                </div>
            </div>
        </div>

        <%--<div class="weather_card">
            <h2 class="weather_card_location">Waterloo</h2>
            <p class="weather_card_description">Mon 1:00 PM, Mostly Sunny</p>
            <div class="weather_card_temperature">
                <h1>23<sup>&deg;C</sup></h1>
                <img src="images/clear-sky-day.png" />
            </div>
            <div class="weather_card_extras">
                <div class="weather_card_precipitation">
                    <img src="images/rain-day.png" />
                    <p>2% Precp</p>
                </div>
                <div class="weather_card_wind">
                    <img src="images/mist-day.png" />
                    <p>23 km/h Winds</p>
                </div>
            </div>
        </div>--%>
        <div>
            <asp:Literal ID="WeatherOutput" runat="server"></asp:Literal>
        </div>
    </form>

    <script type="text/javascript" src="scripts/script.js"></script>
</body>
</html>
