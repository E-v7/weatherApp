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