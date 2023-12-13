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
                <a href="hourlyWeather.aspx">Hourly</a>
                <a href="#">OTHER</a>
                <div class="searchbox">
                    <input class="textbox" id="city" name="city" type="text"/>
                </div>
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
                <div class="login-container">
                    <label for="userName">Username or Email</label>
                    <asp:TextBox runat="server" id="username" CssClass="input-field"></asp:TextBox>

                    <label for="password">Password</label>
                    <asp:TextBox runat="server" id="password" TextMode="Password" CssClass="input-field"></asp:TextBox>

                    <asp:Button runat="server" id="login" Text="Login" CssClass="login-button" OnClick="Login_Click"/>

                    <a href="#" class="link-style">Forgot username or password?</a>
                    <a href="#" class="link-style">Register for an account now!</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
