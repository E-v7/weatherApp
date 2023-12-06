<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WeatherApp.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Weather App Home</title>
    <link href="styles/style.css" rel="stylesheet" />
</head>
<body>
    <form id="MainPageForm" runat="server">
        <h1>The Weather App</h1>
        <div id="navigation_bar">
            <a href="#">Home</a>
            <a href="#">Hourly</a>
            <a href="#">...</a>
        </div>
    </form>

    <script type="text/javascript" src="scripts/script.js"></script>
</body>
</html>
