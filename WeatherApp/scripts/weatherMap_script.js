function initMap(latitude, longitude, apiKey, description) {
    var map = L.map('map');

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 9,
    }).addTo(map);

    var weatherLayer = L.tileLayer('https://tile.openweathermap.org/map/{layer}/{z}/{x}/{y}.png?appid=' + apiKey, {
        layer: 'temp_new',
        maxZoom: 9
    }).addTo(map);

    map.setView([latitude, longitude], 5);  //zoom in to the location
    L.marker([latitude, longitude]).addTo(map).bindPopup(description).openPopup(); //set a marker
}