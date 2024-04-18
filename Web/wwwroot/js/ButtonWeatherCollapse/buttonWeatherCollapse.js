﻿$(function () {
    document.getElementById('btn-weather-collapse').addEventListener('click', async (event) => {
        var detail = document.querySelector('.weather-collapse-detail');

        detail.style.display = detail.style.display != 'block' ? 'block' : 'none';
        const openning = detail.style.display === 'block';
        if (!openning) return;

        const key = await fetch('./secret.json')
            .then((response) => response.json())
            .then((json) => json.WeatherApiKey);

        const success = async (pos) => {
            const { latitude, longitude } = pos.coords;
            let location = {}

            console.log("Your current position is:");
            console.log(`Latitude : ${latitude}`);
            console.log(`Longitude: ${longitude}`);

            location.lat = latitude;
            location.lng = longitude;

            var response = await fetch(`https://api.weatherapi.com/v1/current.json?q=${location.lat},${location.lng}&lang=vi&key=${key}`)
                .then(response => response.json())
                .catch(err => window.alert(JSON.stringify(err)));

            var detailElement = document.querySelector('.weather-collapse-detail');
            detailElement.innerHTML = '';

            var tempElem = document.createElement('div');
            tempElem.classList.add('d-flex');
            tempElem.classList.add('gap-2');
            tempElem.innerHTML = `<b class="fs-3 text-center">${response.current.temp_c}<span>‎°C‎</span></b><span>Feel like <b>${response.current.feelslike_c}<span>‎°C‎</span></b></span>`;

            var conditionElem = document.createElement('div');
            conditionElem.innerHTML = `<img src=${response.current.condition.icon} width="30" height="30" /> ${response.current.condition.text}`;

            var uvElem = document.createElement('div');
            uvElem.innerHTML = `<b style="margin-right: 10px;">UV</b> ${response.current.uv}`

            detailElement.appendChild(tempElem)
            detailElement.appendChild(conditionElem)
            detailElement.appendChild(uvElem)
        }

        function error(err) {
            //console.warn(`ERROR(${err.code}): ${err.message}`);
            swal({
                title: "ERROR!",
                text: err.message,
                icon: "error",
                button: "Ok",
            });
        }

        const options = {
            enableHighAccuracy: true,
            timeout: 10000,
            maximumAge: 0,
        };

        navigator.geolocation.getCurrentPosition(success, error, options);
    });
});

// https://api.weatherapi.com/v1/current.json?q=21.027763,105.834160&lang=vi&key=fde44beedad740c882c182022241704