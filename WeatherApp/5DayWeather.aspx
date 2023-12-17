<%@ Page Title="5 Day Weather" Language="C#" MasterPageFile="~/Header.master" AutoEventWireup="true" CodeBehind="5DayWeather.aspx.cs" Inherits="WeatherApp._5DayWeather" %>

<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
     <link href="styles/style.css" rel="stylesheet" />
     <link href="styles/5Day.css" rel="stylesheet" />  
</asp:Content>

<asp:Content id="search" ContentPlaceHolderID="PageButton" runat="server">
<asp:Button id="Button1" runat="server" Text="Search" />
</asp:Content>
<asp:Content id="mainContent" ContentPlaceHolderID="MainContent" runat="server">

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
        <main id="page_main" runat="server">

        </main>
</body>
</html>
</asp:Content>


