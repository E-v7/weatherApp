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
                    <asp:TextBox runat="server" id="userName" CssClass="input-field"></asp:TextBox>

                    <label for="passWord">Password</label>
                    <asp:TextBox runat="server" id="passWord" TextMode="Password" CssClass="input-field"></asp:TextBox>

                    <asp:Button runat="server" id="login" Text="Login" CssClass="login-button" OnClick="Login_Click"/>

                    <div id="registrationCont" class="registration-container" >
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

                    <a href="javascript:void(0);" class="link-style" onclick="toggleRegistrationForm(); return false;">Register for an account now!</a>
                    <a href="#" class="link-style">Forgot username or password?</a>
                    
                </div>
            </div>
        </div>

    </form>
</body>
</html>
