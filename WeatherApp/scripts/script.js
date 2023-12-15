// jQuery ready function to attach event listeners
$(document).ready(function () {
    $('#consentToggle').change(function () {
        if (this.checked) {
            getLocationAndWeather();
        } else {
            // Hide the weather details if user toggles off consent
            $('#weatherDetails').hide();
        }
    });
});


/*
 * FUNCTION      :function getLocationAndWeather()
 *
 * DESCRIPTION   :This function uses HTML geolocation api built into webforms if HTTPS is selected
 *                initially 
 * 
 * PARAMETERS    :NONE 
 * 
 * RETURNS       :NONE
 * 
 */ 
function getLocationAndWeather() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var lat = position.coords.latitude;
            var lon = position.coords.longitude;
            fetchWeatherDetailsFromServer(lat, lon);
        }, showError);
    } else {
        alert("Geolocation is not supported by this browser.");
    }
}

/*
 * FUNCTION      : fetchWeatherDetailsFromServer(lat, lon, cityName)
 *
 * DESCRIPTION   : Function makes an AJAX POST request to the web method 'GetWeather' in index.aspx
 *                 Take lat or long or city name as parameters and then calls display weather  
 *                 to only change the weather details being displayed in the UI. 
 *   
 * PARAMETERS    : lat - the latitude, lon - the longitude, cityName - city name 
 * 
 * RETURNS       : None
 */
function fetchWeatherDetailsFromServer(lat, lon, cityName = null) {
    var data = cityName ? { cityName: cityName } : { lat: lat, lon: lon };

    $.ajax({
        type: "POST",
        url: "index.aspx/GetWeather", 
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            displayWeather(response.d); 
        },
        error: function (error) {
            console.error('Error:', error);
            alert("Location-Specific Forecast was not Retrieved");
        }
    });
}


/*
 * FUNCTION      : displayWeather(weatherData)
 *
 * DESCRIPTION   : This function processes the weather data received from the server.
 *                 It formats the data into HTML and displays it on the webpage.
 * 
 * PARAMETERS    : weatherData - the JSON string containing weather information taken from 
 *                 WeatherWizard class method 
 * 
 * RETURNS       : NONE
 */
function displayWeather(weatherData) {
    // Parse the JSON data
    var weather = JSON.parse(weatherData);

    // Create HTML content for the weather details
    var content = '<h2>Current Weather for ' + weather.name + '</h2>';
    content += '<div><strong>Temperature:</strong> ' + weather.main.temp + '°K</div>';
    content += '<div><strong>Weather:</strong> ' + weather.weather[0].main + ' (' + weather.weather[0].description + ')</div>';
    content += '<div><strong>Humidity:</strong> ' + weather.main.humidity + '%</div>';
    content += '<div><strong>Wind Speed:</strong> ' + weather.wind.speed + ' m/s</div>';
    content += '<div><strong>Pressure:</strong> ' + weather.main.pressure + ' hPa</div>';

    // Display the weather details
    var weatherDetailsDiv = document.getElementById('weatherDetails');
    weatherDetailsDiv.style.display = 'block';
    weatherDetailsDiv.innerHTML = content;
}

/*
 * FUNCTION      : showError(error)
 *
 * DESCRIPTION   : This function displays an appropriate alert message based on the 
 *                 error code received from the geolocation API.
 * 
 * PARAMETERS    : error - the error object from the geolocation API
 * 
 * RETURNS       : NONE
 */
function showError(error) {
    switch (error.code) {
        case error.PERMISSION_DENIED:
            alert("Geolocation request from User was denied.");
            break;
        case error.POSITION_UNAVAILABLE:
            alert("Location information is unavailable.");
            break;
        case error.TIMEOUT:
            alert("The request to get user location timed out.");
            break;
        case error.UNKNOWN_ERROR:
            alert("An unknown error occurred.");
            break;
    }
}

/*
 * FUNCTION      : toggleRegistrationForm()
 *
 * DESCRIPTION   : Function toggles the visibility of the registration container
 *                 login form. The registration container and then hides the login part. 
 * 
 * PARAMETERS    : NONE 
 *  
 * RETURNS       : NONE 
 * 
 */ 
function toggleRegistrationForm() {
    var loginContainer = document.getElementById('loginContainer');
    var regContainer = document.getElementById('registrationCont');
    if (regContainer.style.display == 'none') {
        regContainer.style.display = 'block'; // Show the registration form
        loginContainer.style.display = 'none'; // Hide the login form
    } else {
        regContainer.style.display = 'none'; // Hide the registration form
        loginContainer.style.display = 'block'; // Show the login form
    }
}

/*
 * FUNCTION      : hideLoginUI()
 *
 * DESCRIPTION   : Function hides login only once Login_Click verifies user credentials 
 *                 against the database and the user successfully logs in. 
 * 
 * PARAMETERS    : NONE
 * 
 * RETURNS       : NONE
 * 
 */
function hideLoginUI() {
    var loginContainer = document.getElementById('loginContainer');
    if (loginContainer) {
        loginContainer.style.display = 'none';
    }

}

/*
* FUNCTION      : performSearch() 
*
* DESCRIPTION   : Function performs a search when the user inputs in the search bar 
                  this function validates the search performed and calls the above 
                  function fetchWeatherDetailsFromServer to fetch weather details.
* 
* PARAMETERS    : NONE
* 
* RETURNS       : NONE
* 
*/
function performSearch() {
    var searchQuery = document.getElementById('location').value;
    if (searchQuery.trim() == "") {
        alert("Please enter a location to search.");
        return;
    }
    fetchWeatherDetailsFromServer(null, null, searchQuery);
}

$(document).on('click', '#Search', function () {
    performSearch();
});
