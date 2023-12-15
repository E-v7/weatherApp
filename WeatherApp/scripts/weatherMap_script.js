function initMap(latitude, longitude, apiKey, description) {
   // document.getElementById("text").innerHTML = "lat" + latitude + "long" + longitude;
    var map = L.map('map');   

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 9,
    }).addTo(map);

    var weatherLayer = L.tileLayer('https://tile.openweathermap.org/map/{layer}/{z}/{x}/{y}.png?appid=' + apiKey, {
        layer: 'temp_new',
        maxZoom: 9
    }).addTo(map);

    map.setView([latitude, longitude], 12);  //zoom in to the location
    if (description != "") {
        L.marker([latitude, longitude]).addTo(map).bindPopup(description).openPopup(); //set a marker
    }
    
}
function getLocationAndWeather(apiKey) {
    //Zoom in to current location
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var lat = position.coords.latitude;
            var lon = position.coords.longitude;

            initMap(lat, lon, apiKey, "YourLocation");
        }, function (error) {
            showError(error, apiKey);
        });
    } else {
        alert("Geolocation is not supported by this browser.");
    }
}

function showError(error, apiKey) {

    initMap(40.7143, -74.006, apiKey, "");  //if location is not valid map will reload to default coordinates

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

