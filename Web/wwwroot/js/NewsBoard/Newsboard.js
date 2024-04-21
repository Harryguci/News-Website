window.handlechangecategory = function handlechangecategory(category) {
    //swal({
    //    title: "Success!",
    //    text: category,
    //    icon: "success",
    //    button: "Ok",
    //});

    var navItems = document.querySelectorAll('.news-board__nav__item .custom-btn');

    Array.from(navItems).map(item => {
        item.classList.remove('active');
    })

    document.querySelector(`#news-board__nav__item__${category}`).classList.add('active');
}

function NavComponent(category, className) {
    return `
        <div class="news-board__nav__item">
            <button type="button" class="btn custom-btn ${className ?? ""}" id="news-board__nav__item__${category.name}"
                onClick="handlechangecategory('${category.name}')">${category.display}</button>
        </div>`
}




$(async function () {
    const navContainer = document.querySelector('.news-board__nav')
    navContainer.innerHTML = '';

    await fetch(`https://localhost:7024/api/Categories`)
        .then(response => response.json())
        .then(data => {
            data.map(item => {
                if (item.name === 'khampha')
                    navContainer.innerHTML += NavComponent(item, 'active');
                else
                    navContainer.innerHTML += NavComponent(item)
            })
        });
});

$(async function () {
    const types = ["", "tall", "wide", "big"]
    let page = 1;
    let pageSize = 20;

    const newsBoardMain = document.querySelector('.news-board-main');
    const NewsBoardItem = (title, url, urlToImage) => {
        var num = parseInt(Math.random() * 10 % 4);
        return `<div class="news-board__item ${types[num]}">
            <a target="_blank" href="${url}" class="thumbnail">
                <img src="${urlToImage}" alt="" />
            </a>
            <div class="content">
                <a target="_blank" href="${url}"><p>${title}</p></a>
            </div>
        </div>`
    }

    await fetch(`https://newsapi.org/v2/top-headlines?country=us&page=${page}&pageSize=${pageSize}&apiKey=cd426c550ab842e1b03296aaa5e666e4`)
        .then(response => response.json())
        .then(data => {
            newsBoardMain.innerHTML = '';
            var articles = data.articles;
            Array.from(articles).map(item => {
                newsBoardMain.innerHTML += NewsBoardItem(item.title, item.url, item.urlToImage)
            });
        }).catch(err => swal(err.message));
})


$(function () {

    let page = 2;
    let pageSize = 20;
    let idTimeout = null;

    const NewsBoardItem = (title, url, urlToImage) => {
        const types = ["", "tall", "wide", "big"]
        var num = parseInt(Math.random() * 10 % 4);
        return `<div class="news-board__item ${types[num]}">
            <a href="${url}" class="thumbnail">
                <img src="${urlToImage}" alt="" />
            </a>
            <div class="content">
                <a href="${url}"><p>${title}</p></a>
            </div>
        </div>`
    };

    const checkScrollEnd = async () => {
        const newsBoardMain = document.querySelector('.news-board-main');

        if (document.body.offsetHeight + document.body.getBoundingClientRect().y - window.innerHeight === 0) {
            if (idTimeout)
                clearTimeout(idTimeout);

            idTimeout = setTimeout(async () => {
                await fetch(`https://newsapi.org/v2/top-headlines?country=us&page=${page}&pageSize=${pageSize}&apiKey=cd426c550ab842e1b03296aaa5e666e4`)
                    .then(response => response.json())
                    .then(data => {
                        //newsBoardMain.innerHTML = '';
                        var articles = data.articles;
                        Array.from(articles).map(item => {
                            newsBoardMain.innerHTML += NewsBoardItem(item.title, item.url, item.urlToImage)
                        });
                        page = page + 1;
                    }).catch(err => swal(err.message));
            }, 300);
        }
    };
    // Listen for scroll events
    window.addEventListener('scroll', checkScrollEnd);

})

