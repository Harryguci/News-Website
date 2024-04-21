let page = 1;
let pageSize = 20;
const types = ["", "tall", "wide", "big"]
const category = {
    display: "Khám phá",
    name: "khampha"
};
let idFetchDataTimeout = null;

function NewsBoardItem(title, url, urlToImage) {
    const num = parseInt(Math.random() * 10 % 4);
    title = title.replace(/(['"])/g, "\\$1");

    return `<div class="news-board__item ${types[num]}" onClick="RememberAction({ title : '${title}', url : '${url}', urlToImage: '${urlToImage}'})">
            <a target="_blank" href="${url}" class="thumbnail">
                <img loading="lazy" src="${urlToImage}" alt="" />
            </a>
            <div class="content">
                <a target="_blank" href="${url}"><p>${title}</p></a>
            </div>
        </div>`
}
function NavComponent(category, className) {
    return `
        <div class="news-board__nav__item">
            <button type="button" class="btn custom-btn ${className ?? ""}" id="news-board__nav__item__${category.name}"
                onClick="handlechangecategory('${category.name}', '${category.display}')">${category.display}</button>
        </div>`
}

window.addEventListener('load', (event) => {
    window.scrollTo(0, 0);
});

window.handlechangecategory = async function handlechangecategory(categoryName, categoryDisplay) {
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

    document.querySelector(`#news-board__nav__item__${categoryName}`).classList.add('active');

    const newsBoardMain = document.querySelector('.news-board-main');
    await fetch(`https://newsapi.org/v2/everything?q=${categoryDisplay}&apiKey=cd426c550ab842e1b03296aaa5e666e4`)
        .then(response => response.json())
        .then(data => {
            newsBoardMain.innerHTML = '';
            var articles = data.articles;
            Array.from(articles).map(item => {
                newsBoardMain.innerHTML += NewsBoardItem(item.title, item.url, item.urlToImage)
            });
        }).catch(err => swal(err.message));
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

function RememberAction(post) {
    var recentPost = localStorage.getItem('recent-posts');
    recentPost = recentPost ? JSON.parse(recentPost) : Array.from([]);

    recentPost.push(post);
    if (recentPost.length > 5) recentPost.shift();
    localStorage.setItem('recent-posts', JSON.stringify(recentPost));
}

async function RefreshData(cb) {
    const newsBoardMain = document.querySelector('.news-board-main');
    if (idFetchDataTimeout) clearTimeout(idFetchDataTimeout);

    if (category.name)
        idFetchDataTimeout = setTimeout(async () => {
            await fetch(`https://newsapi.org/v2/everything?q=${category.display}&page=${page}&pageSize=${pageSize}&apiKey=cd426c550ab842e1b03296aaa5e666e4`)
                .then(response => response.json())
                .then(data => {
                    var articles = data.articles;
                    Array.from(articles).map(item => {
                        newsBoardMain.innerHTML += NewsBoardItem(item.title, item.url, item.urlToImage)
                    });
                    page = page + 1;
                }).catch(err => swal(err.message));
        }, 300);
    else
        idFetchDataTimeout = setTimeout(async () => {
            await fetch(`https://newsapi.org/v2/top-headlines?country=us&page=${page}&pageSize=${pageSize}&apiKey=cd426c550ab842e1b03296aaa5e666e4`)
                .then(response => response.json())
                .then(data => {
                    var articles = data.articles;
                    Array.from(articles).map(item => {
                        newsBoardMain.innerHTML += NewsBoardItem(item.title, item.url, item.urlToImage)
                    });
                    page = page + 1;
                }).catch(err => swal(err.message));
        }, 300);

    return cb && cb();
}

$(async function () {
    const fetchDoneHandle = () => {
        var defaultItems = document.querySelectorAll('.news-board__item-default');

        var container = document.querySelector('.news-board-main');

        Array.from(defaultItems).map(item => {
            container.removeChild(item);
        });
    }
    return await RefreshData(fetchDoneHandle);
})

$(function () {
    const checkScrollEnd = async () => {
        if (document.body.offsetHeight + document.body.getBoundingClientRect().y - window.innerHeight === 0) {
            await RefreshData();
        }
    };
    // Listen for scroll events
    window.addEventListener('scroll', checkScrollEnd);
})

