let resultInput = document.querySelector('#add-img-urls');

let profileImageInput = document.querySelector('.profile-image input');
let profileImageBtn = document.querySelector('.profile-image a');
let container = document.querySelector('.additional-images');

if (resultInput.value != null) {
    let gotUrls = resultInput.value.split(' || ');

    if (gotUrls.length > 0) {
        gotUrls.forEach(url => {
            let imgDiv = document.createElement('div');
            imgDiv.classList.add('image');

            let imgEl = document.createElement('img');
            let spanEl = document.createElement('span');



            spanEl.innerHTML +=
                `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                    class="bi bi-x-square-fill" viewBox="0 0 16 16">
                    <path
                    d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm3.354 4.646L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 1 1 .708-.708z" />
                </svg>`;

            spanEl.addEventListener('click', (e) => {
                let elToRemove = e.currentTarget.parentElement;
                let url = elToRemove.children[0].src;

                resultInput.value = resultInput.value.replace(url, '');

                container.removeChild(elToRemove);
            });

            imgEl.src = url;

            imgDiv.appendChild(imgEl);
            imgDiv.appendChild(spanEl);

            container.appendChild(imgDiv);
        });
    }
}


profileImageBtn.addEventListener('click', (e) => {
    let imgEl = e.currentTarget.parentElement.previousElementSibling;

    imgEl.src = profileImageInput.value;
});

let additionalImagesInput = document.querySelector('.bottom input');
let additionalImagesBtn = document.querySelector('.bottom a');

additionalImagesBtn.addEventListener('click', (e) => {
    let imgDiv = document.createElement('div');
    imgDiv.classList.add('image');

    let imgEl = document.createElement('img');
    imgEl.src = additionalImagesInput.value;

    let spanEl = document.createElement('span');

    spanEl.innerHTML +=
        `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                class="bi bi-x-square-fill" viewBox="0 0 16 16">
                <path
                d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm3.354 4.646L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 1 1 .708-.708z" />
            </svg>`;

    spanEl.addEventListener('click', (e) => {
        let elToRemove = e.currentTarget.parentElement;
        let url = elToRemove.children[0].src;

        resultInput.value = resultInput.value.replace(url + ' || ', '');

        container.removeChild(elToRemove);
    });

    imgDiv.appendChild(imgEl);
    imgDiv.appendChild(spanEl);

    container.appendChild(imgDiv);

    resultInput.value += ' || ' + additionalImagesInput.value + ' || '

    additionalImagesInput.value = '';
});

