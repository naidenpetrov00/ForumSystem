function getWeather(apiKey) {
    let latitude = 0;
    let longitude = 0;

    if ("geolocation" in navigator) {
        navigator.geolocation.getCurrentPosition(function (position) {
            latitude = position.coords.latitude;
            longitude = position.coords.longitude;

            const url = `https://api.openweathermap.org/data/2.5/weather?lat=${latitude}&lon=${longitude}&appid=${apiKey}&units=metric`;

            let weather = Object();
            $.get(url, function (response) {
                $("#name").text(response.name);
                $("#temp").text(response.main.temp);
                $("#weather").text(response.weather[0].main);
                console.log(response.main)
            });
        });
    }
    else {
        console.log("Geolocation is not available in this browser.");
    }
}