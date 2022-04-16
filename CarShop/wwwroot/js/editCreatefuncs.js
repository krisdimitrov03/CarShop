let imagesEl = document.getElementById('images');

document.getElementById('add-img').addEventListener('click', () => {
    let url = document.getElementById('imageUrls');

    if (url.value === '') {
        return;
    }

    document.getElementById('submitUrls').value += url.value + ' || ';

    let divEl = document.createElement('div');
    let imgEl = document.createElement('img');
    imgEl.src = url.value;
    let buttonEl = document.createElement('span');
    buttonEl.classList.add('remove-img-btn');
    buttonEl.textContent = '✖';

    buttonEl.addEventListener('click', (e) => {
        let div = e.currentTarget.parentElement;
        let url = e.currentTarget.previousSibling.src;

        let urls = document.getElementById('submitUrls').value;

        urls = urls.replace(`${url} || `, '');

        document.getElementById('submitUrls').value = urls;

        document.getElementById('images').removeChild(div);
    });

    divEl.appendChild(imgEl);
    divEl.appendChild(buttonEl);

    imagesEl.appendChild(divEl);

    url.value = '';
});

Array.from(imagesEl.querySelectorAll('.remove-img-btn')).forEach(b => {
    b.addEventListener('click', (e) => {
        let div = e.currentTarget.parentElement;
        let url = e.currentTarget.parentElement.children[0].src;

        let urls = document.getElementById('submitUrls').value;

        urls = urls.replace(`${url} || `, '');

        document.getElementById('submitUrls').value = urls;

        document.getElementById('images').removeChild(div);
    });
});