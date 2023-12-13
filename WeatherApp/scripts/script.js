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
 * FUNCTION      :
 *
 * DESCRIPTION   :
 * 
 * PARAMETERS    :
 * 
 * RETURNS       :
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
 * FUNCTION      :
 *
 * DESCRIPTION   :
 * 
 * PARAMETERS    :
 * 
 * RETURNS       :
 * 
 */ 
function fetchWeatherDetailsFromServer(lat, lon) {
    $.ajax({
        type: "POST",
        url: "index.aspx/GetWeather",
        data: JSON.stringify({ lat: lat, lon: lon }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            displayWeather(response.d); // '.d' accesses the actual result in the response object from ASP.NET
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}


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
 * FUNCTION      :
 *
 * DESCRIPTION   :
 * 
 * PARAMETERS    :
 * 
 * RETURNS       :
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