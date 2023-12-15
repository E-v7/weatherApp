<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="5DayWeather.aspx.cs" Inherits="WeatherApp._5DayWeather" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>5 Day Weather</title>
    <link href="styles/style.css" rel="stylesheet" />
    <link href="styles/5Day.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
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

        <main id="page_main" runat="server">

        </main>
    </form>
</body>
</html>
