$(async function () {
    const key = await fetch('./secret.json')
        .then((response) => response.json())
        .then((json) => json.RealTimeWebSearch)
        .catch(err =>
            swal({
                title: "ERROR!",
                text: err.message,
                icon: "error",
                button: "Ok",
            }));

    const handleSearchBar = (query) => {
        if (!query)
            return document.querySelector('.search-results').innerHTML = '';

        const settings = {
            async: true,
            crossDomain: true,
            url: `https://real-time-web-search.p.rapidapi.com/search?q=${query}&limit=5`,
            method: 'GET',
            headers: {
                'X-RapidAPI-Key': key,
                'X-RapidAPI-Host': 'real-time-web-search.p.rapidapi.com'
            }
        };

        $.ajax(settings).done(function (response) {
            console.log(response);
            if (response.status != 'OK') {
                return swal(JSON.stringify(response.status))
            }

            //show response.data
            var listGroup = document.querySelector('.search-results');
            listGroup.innerHTML = '';

            var data = response.data;

            data.map((item, index) => {
                var liHtml = `
                <li key="${index}" class="list-group-item">
                    <a href="${item.url}">
                        <p class="fw-bold m-0 text-primary">${item.title}</p>
                        <p class="m-0" style="font-size: 10px">${item.snippet.substring(0, 100)}...</p>
                    </a>
                </li>`
                listGroup.innerHTML += liHtml;
            });
        });
    };


    var searchControl = document.querySelector('#search-bar-top .search-input');
    var idTimeout = null;

    searchControl.addEventListener('change', (event) => {
        if (idTimeout) clearTimeout(idTimeout);
        console.log(searchControl.value)

        idTimeout = setTimeout(() => {
            handleSearchBar(searchControl.value);
        }, 1000);
    })
});
