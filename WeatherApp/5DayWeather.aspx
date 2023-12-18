<%@ Page Title="5 Day Weather" Language="C#" MasterPageFile="~/Header.master" AutoEventWireup="true" CodeBehind="5DayWeather.aspx.cs" Inherits="WeatherApp._5DayWeather" %>

<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
     <link href="styles/style.css" rel="stylesheet" />
     <link href="styles/5Day.css" rel="stylesheet" />  
</asp:Content>

<asp:Content id="search" ContentPlaceHolderID="PageButton" runat="server">
    <asp:Button id="Search_Button" runat="server" Text="Search" OnClick="Search_Button_Click" />
</asp:Content>

<asp:Content id="mainContent" ContentPlaceHolderID="MainContent" runat="server">
        <main id="page_main" runat="server">

        </main>
</asp:Content>


