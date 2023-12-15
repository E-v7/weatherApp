﻿<%@ Page Title="Weather App Home" Language="C#" MasterPageFile="~/Header.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WeatherApp.index" %>

<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
     <script type="text/javascript" src="scripts/script.js"></script>
     <link href="styles/style.css" rel="stylesheet" />  
</asp:Content>

<asp:Content ID="mainContent" ContentPlaceHolderID="MainContent" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<body>
        <header>
            <h1 id="title">The Weather App</h1>
            <div id="navigation_bar">
                <a href="index.aspx">Home</a>
                <a href="hourlyWeather.aspx">Current</a>
                <a href="5DayWeather.aspx">5 Day</a>
                <a href="#">OTHER</a>
                <input id="location" placeholder="Enter a location" type="text" runat="server"/>
                <asp:Button runat="server" id="Search" Text="Search" CssClass="search-button" OnClick="Search_Click" />

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
                    <asp:Label runat="server" id="WeatherInfo"  CssClass="weather-info" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="right-container">
                <asp:Label runat="server" id="Greeting" Visible="False" CssClass="weather-info"></asp:Label>

                <div id="loginContainer" class="login-container">
                    <label for="userName">Username or Email</label>
                    <asp:TextBox runat="server" id="userName" CssClass="input-field"></asp:TextBox>

                    <label for="passWord">Password</label>
                    <asp:TextBox runat="server" id="passWord" TextMode="Password" CssClass="input-field"></asp:TextBox>

                    <asp:Button runat="server" id="login" Text="Login" CssClass="login-button" OnClick="Login_Click"/>

                    <a href="#" class="link-style" onclick="forgotCredentials(); return false;">Forgot username or password?</a>
                    <a href="javascript:void(0);" class="link-style" onclick="toggleRegistrationForm(); return false;">Register for an account now!</a>
                </div>
                <div id="registrationCont" class="registration-container" style="display:none;">
                    <label for="firstName">First Name</label>
                    <asp:TextBox runat="server" id="firstName" CssClass="input-field "></asp:TextBox>
                    <label for="lastName">Last Name</label>
                    <asp:TextBox runat="server" id="lastName" CssClass="input-field "></asp:TextBox>
                    <label for="emailAddress">Email</label>
                    <asp:TextBox runat="server" id="emailAddress" CssClass="input-field "></asp:TextBox>
                    <label for="regUsername">Username</label>
                    <asp:TextBox runat="server" id="regUsername" CssClass="input-field "></asp:TextBox>
                    <label for="regPassword">Password</label>
                    <asp:TextBox runat="server" id="regPassword" CssClass="input-field "></asp:TextBox>
                    <label for="confirmPassword">Confirm Password</label>
                    <asp:TextBox runat="server" id="confirmPassword" CssClass="input-field "></asp:TextBox>

                    <asp:Button runat="server" id="register" Text="Register" CssClass="register-button" OnClick="Register_Click"/>
                </div>                    
            </div>
        </div>
</body>
</html>
</asp:Content>
