<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hourlyWeather.aspx.cs" Inherits="WeatherApp.hourlyWeather" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <h1 id="title">The Weather App</h1>
            <div id="navigation_bar">
                <a href="index.aspx">Home</a>
                <a href="hourlyWeather.aspx">Hourly</a>
                <a href="#">OTHER</a>
            </div>
        </header>

        <div id="testing" runat="server"></div>
    </form>
</body>
</html>
