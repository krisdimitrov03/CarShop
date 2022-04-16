Array.from(document.querySelectorAll('.group-el')).forEach(el => {
    el.addEventListener('mouseover', (e) => {
        e.currentTarget.children[0].style.padding = '5px 10px';
    });

    el.addEventListener('mouseout', (e) => {
        e.currentTarget.children[0].style.padding = '5px';
    });
});

document.querySelector('#filter h3').addEventListener('click',() => {
    if(document.getElementById('filters-area').style.display === 'none') {
        document.getElementById('filters-area').style.display = 'block';
    }
    else {
        document.getElementById('filters-area').style.display = 'none';
    }
})