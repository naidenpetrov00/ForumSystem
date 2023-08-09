let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/weatherHub")
        .build();

    connection.on("UpdateWeather", (apiKey) => {
        getWeather(apiKey);
    })

    connection.start().catch(function (err) {
        console.log(err.toString());
    });
}

setupConnection();

function getWeather(apiKey) {
    let latitude = 0;
    let longitude = 0;

    if ("geolocation" in navigator) {
        navigator.geolocation.getCurrentPosition(function (position) {
            latitude = position.coords.latitude;
            longitude = position.coords.longitude;

            const url = `https://api.openweathermap.org/data/2.5/weather?lat=${latitude}&lon=${longitude}&appid=${apiKey}&units=metric`;

            let weather = Object();
            let currentTime = new Date();
            let day = currentTime.getDate();
            let month = currentTime.getMonth();
            let year = currentTime.getFullYear();
            let date = `${day}/${month}/${year}`;
            let hours = currentTime.getHours();
            let minutes = currentTime.getMinutes();
            let time = `${hours}:${minutes}`;
            $.get(url, function (response) {
                $("#name").text(response.name);
                $("#temp").text(response.main.temp);
                $("#weather").text(response.weather[0].main)
                $("#date").text(date)
                $("#time").text(time)
            });
        });
    }
    else {
        console.log("Geolocation is not available in this browser.");
    }
}