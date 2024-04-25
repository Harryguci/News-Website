$(function () {
    var btnShow = document.querySelector('#btn-toggle-main-menu');
    
    btnShow.addEventListener('click', (event) => {
        document.querySelector('#main-menu').classList.toggle('show');
    });
});